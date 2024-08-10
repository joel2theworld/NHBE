using NeighbourhoodHelp.Data.IRepository;
using NeighbourhoodHelp.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NeighbourhoodHelp.Model.Entities;
using AutoMapper;
using NeighbourhoodHelp.Infrastructure.Interfaces;

namespace NeighbourhoodHelp.Data.Repository
{
    public class AgentRepository : IAgentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICloudService _cloudService;

        public AgentRepository(ApplicationDbContext context, IMapper mapper, ICloudService cloudService)
        {
            _context = context;
            _mapper = mapper;
            _cloudService = cloudService;
            _mapper = mapper;
        }


        public async Task<string> CreateAgentAsync(string id, CreateAgentDto agentDto)
        {
            var existingUser = await _context.appUsers.FirstOrDefaultAsync(a => a.Id.Equals(id));
            if (existingUser == null)
            {
                return null;
            }

            var doc = await _cloudService.AddDocumentAsync(agentDto.Document);
            /*var newAgent = _mapper.Map<Agent>(agentDto);*/
            var newAgent = new Agent
            {
                NIN = agentDto.NIN,
                PostalCode = agentDto.PostalCode,
                DateOfBirth = agentDto.DateOfBirth,
                Document = doc.Url.ToString(),
                AppUser = existingUser,
                
            };

            _context.agents.Add(newAgent);
            await _context.SaveChangesAsync();

            return "Successful";
        }


        public async Task<ErrandDto> GetAgentByErrandIdAsync(Guid errandId)
        {
            var errand = new Errand
            {
                /*Id = new Guid(),
                Description = "Perfume",
                Street = "ParkLand Estate",
                City = "Port Harcourt",
                State = "Rivers State",
                PostalCode = "5002070",
                Time = "4:00pm",
                Date = "15/4/2024",
                ItemName = "Black Oud",
                Weight = "70",
                Note = "No note",
                UserId = Guid.Parse("0c1837ba-4a03-4dca-a868-26b2a5e4b73e")*/
            };
            _context.Errands.Add(errand);
            await _context.SaveChangesAsync();

            var Errands = await _context.Errands.Include(c => c.Agent).FirstOrDefaultAsync(c => c.Id == errandId);
            var agentErrandId = new ErrandDto
            {/*
                FirstName = Errands.Agent.FirstName,
                LastName = Errands.Agent.LastName,
                PostalCode = Errands.Agent.PostalCode,
                Street = Errands.Agent.Street,
                City = Errands.Agent.City,
                State = Errands.Agent.State,
                PhoneNumber = Errands.Agent.PhoneNumber,*/



            };
            return agentErrandId;


        }

        public async Task<List<GetAgentDto>> GetAllAgents()
        {
            var agents = await _context.agents.ToListAsync();
            return _mapper.Map<List<GetAgentDto>>(agents);


        }

        public async Task<Agent> AssignAgentAsync(Errand errand)
        {
            // Find matching agents based on PostalCode
            var matchingAgents = await _context.agents
                .Where(a => a.AppUser.PostalCode == errand.PostalCode && !a.IsDeleted)
                .ToListAsync();

            if (matchingAgents.Any())
            {
                // Randomly select an agent
                var random = new Random();
                var selectedAgent = matchingAgents[random.Next(matchingAgents.Count)];

                // Update the errand with the new agent
                var errandToUpdate = await _context.Errands.FirstOrDefaultAsync(e => e.Id == errand.Id);

                if (errandToUpdate != null)
                {
                    // Set the AgentId or Agent navigation property
                    errandToUpdate.Agent = selectedAgent;

                    await _context.SaveChangesAsync();
                    return selectedAgent;
                }
                else
                {
                    throw new ArgumentException($"Errand with ID {errand.Id} not found in the database.");
                }
            }
            else
            {
                throw new Exception("No agents available for the specified postal code.");
            }
        }

        public async Task<string> UpdateAgentProfile(UpdateAgentProfileDto agentProfileDto)
        {
            var existingAgent = await _context.agents.FirstOrDefaultAsync(a => a.Id.Equals(agentProfileDto.Id));
            if (existingAgent == null)
            {
                return null;
            }

            existingAgent.PostalCode = agentProfileDto.PostalCode;

            await _context.SaveChangesAsync();
            return ("Updated Successfully");
        }
    }
}

