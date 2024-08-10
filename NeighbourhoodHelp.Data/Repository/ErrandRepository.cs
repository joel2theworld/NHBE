using Microsoft.EntityFrameworkCore;
using NeighbourhoodHelp.Data.IRepository;
using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NeighbourhoodHelp.Infrastructure.Helpers;
using NeighbourhoodHelp.Infrastructure.Interfaces;
using NeighbourhoodHelp.Model.DTOs;
using NeighbourhoodHelp.Model.Enums;

namespace NeighbourhoodHelp.Data.Repository
{
    public class ErrandRepository : IErrandRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ErrandRepository(ApplicationDbContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<ErrandAssignmentDto> CreateErrand(CreateErrandDto createErrand)
        {
            var agentsWithMatchingPostalCode = _context.agents
                .Include(a => a.AppUser)
                .Where(a => a.PostalCode == createErrand.PostalCode && !a.IsActive)
                .ToList();

            if (!agentsWithMatchingPostalCode.Any())
            {
                throw new InvalidOperationException("No agents found for the given postal code");
            }

            var random = new Random();
            var randomIndex = random.Next(0, agentsWithMatchingPostalCode.Count);
            var randomAgent = agentsWithMatchingPostalCode[randomIndex];

            var newErrand = new Errand
            {
                ItemName = createErrand.ItemName,
                State = createErrand.State,
                City = createErrand.City,
                Street = createErrand.Street,
                PostalCode = createErrand.PostalCode,
                Date = createErrand.Date,
                Weight = createErrand.Weight,
                Quantity = createErrand.Quantity,
                Note = createErrand.Note,
                Price = createErrand.Price,
                AppUserId = createErrand.UserId,
                AgentId = randomAgent.Id
            };

            _context.Errands.Add(newErrand);
            await _context.SaveChangesAsync();

            var emailToAgent = new EmailDto
            {
                To = randomAgent.AppUser.Email,
                Subject = "New Errand Assigned",
                Body = $"You have been assigned a new errand:\n\n" +
              $"Errand Details:\n" +
              $"Item: {createErrand.ItemName}\n" +
              $"Description: {createErrand.Note}\n" +
              $"Location: {createErrand.City}, {createErrand.State}, {createErrand.PostalCode}\n" +
              $"Date: {createErrand.Date}\n\n" +
              $"Please contact the app user for further details.\n"
            };

            await _emailService.SendEmailToAgentForErrandCreated(emailToAgent);

            var agentDetailsToUser = $"Assigned Agent Details:\n" +
                                     $"Name: {randomAgent.AppUser.FirstName} {randomAgent.AppUser.LastName}\n" +
                                     $"Phone Number: {randomAgent.AppUser.PhoneNumber}\n" +
                                     $"Email: {randomAgent.AppUser.Email}\n";

            var userCreatingErrand = await _context.Users.FirstOrDefaultAsync(u => u.Id == createErrand.UserId);

            var result = new ErrandAssignmentDto
            {
                User = userCreatingErrand,
                Agent = randomAgent
            };

            return result;
        }
        public async Task<IList<GetErrandDto>> GetAllErrandsByAppUserIdAsync(Guid userId, PaginationParameters paginParams)
        {
            try
            {
                var userRole = await _context.appUsers
                    .Where(u => u.Id.Equals(userId))
                    .Select(u => u.Role)
                    .FirstOrDefaultAsync();

                if (userRole != "user")
                {
                    return new List<GetErrandDto>();
                }

                var queryErrand = _context.Errands
                    .Where(q => q.AppUser.Id.Equals(userId))
                    .OrderByDescending(q => q.Date)
                    .Skip((paginParams.PageNumber - 1) * paginParams.PageSize)
                    .Take(paginParams.PageSize);

                var errands = await queryErrand.ToListAsync();

                var userErrandsDetailList = _mapper.Map<List<GetErrandDto>>(errands);
                return userErrandsDetailList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while retrieving errands.", ex);
            }
            
        }

        public async Task<IList<GetErrandDto>> GetAllErrandsByAgentIdAsync(Guid agentId, PaginationParameters paginParams)
        {
            try
            {
                var query = _context.Errands
                    .Where(e => e.Agent.Id.Equals(agentId))
                    .OrderByDescending(e => e.Date)
                    .Skip((paginParams.PageNumber - 1) * paginParams.PageSize)
                    .Take(paginParams.PageSize);

                var errands = await query.ToListAsync();

                var agentErandDetailsList = _mapper.Map<List<GetErrandDto>>(errands); // Use AutoMapper to map Errand entities to ErrandDto

                return agentErandDetailsList;
            }
            catch (Exception ex)
            {
                // Log and handle the exception
                throw new ApplicationException("Error occurred while retrieving errands.", ex);
            }

        }

        public async Task<PendingErrandDto> GetPendingErrandByAgentId(Guid agentId)
        {
            var pendingErrand = await _context.Errands
                .Include(e => e.Agent)
                .FirstOrDefaultAsync(e => e.Agent.Id == agentId);
            var ErrandsDetail = _mapper.Map<PendingErrandDto>(pendingErrand);
            return ErrandsDetail;
        }

        public async Task<bool> CompleteErrand(Guid errandId)
        {
            var errand = await _context.Errands.FindAsync(errandId);

            if (errand == null)
            {
                return false; // Errand not found
            }

            errand.ErrandStatus = ErrandStatus.Completed;
            await _context.SaveChangesAsync();

            return true; // Errand completed successfully
        }

        public async Task<int> GetTotalCompletedErrandsForAgentAsync(Guid agentId)
        {
            try
            {
                return await _context.Errands
                    .CountAsync(e => e.AgentId == agentId && e.ErrandStatus == ErrandStatus.Completed);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while getting total completed errands for agent.", ex);
            }
        }

        public async Task<decimal> GetTotalAmountEarnedByAgentAsync(Guid agentId)
        {
            try
            {
                var totalAmount = await _context.Errands
                    .Where(e => e.AgentId == agentId && e.ErrandStatus == ErrandStatus.Completed)
                    .SumAsync(e => e.Price);

                return totalAmount;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while getting total amount earned by agent.", ex);
            }

        }
    }
}
