using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NeighbourhoodHelp.Data.IRepository;
using NeighbourhoodHelp.Model.DTOs;
using NeighbourhoodHelp.Model.Entities;
using NeighbourhoodHelp.Model.Enums;

namespace NeighbourhoodHelp.Data.Repository
{
    public class PriceRepository : IPriceRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IAgentRepository _agentRepository;
    private readonly IMapper _mapper;


        public PriceRepository(ApplicationDbContext context, IAgentRepository agentRepository, IMapper mapper)
        {
            _context = context;
            _agentRepository = agentRepository;
            _mapper = mapper;
        }


        public async Task<AgentDto> AgentAcceptPrice(Guid errandId)
        {
            var errand = await _context.Errands.FindAsync(errandId);

            if (errand == null)
            {
                return null;
            }

            // Get the agent from the errand
            var agent = await _context.agents.FindAsync(errand.AgentId);

            if (agent != null)
            {
                // Set isActive to true
                agent.IsActive = true;

                // Retrieve the AppUser associated with the Agent
                var appUser = await _context.Users.FindAsync(agent.AppUserId);

                // Map agent and AppUser details to AgentDto
                var agentDetail = _mapper.Map<AgentDto>(agent);
                agentDetail.FirstName = appUser.FirstName;
                agentDetail.LastName = appUser.LastName;
                agentDetail.Email = appUser.Email;
                agentDetail.PhoneNumber = appUser.PhoneNumber;
                agentDetail.Price = errand.Price;


                await _context.SaveChangesAsync(); // Save the changes to update isActive property

                return agentDetail;
            }
            else
            {
                return null;
            }
        }




        public async Task<PriceDto> AgentCounterPrice(PriceNegotiationDto request)
        {
            var errand = await _context.Errands.FindAsync(request.ErrandId);

            if (errand == null)
            {
                return null; /*"Errand not found"*/
            }

            var agent = await _context.agents.FindAsync(errand.AgentId);

            if (errand.AgentCounterOffers < 2)
            {
                var appUser = await _context.Users.FindAsync(agent.AppUserId);
                var agentDetail = _mapper.Map<PriceDto>(agent);
                agentDetail.FirstName = appUser.FirstName;
                agentDetail.LastName = appUser.LastName;
                agentDetail.Email = appUser.Email;
                agentDetail.PhoneNumber = appUser.PhoneNumber;
                agentDetail.Price = request.CounterPrice;

                // Update price request on database
                errand.Price = request.CounterPrice;
                _context.Errands.Update(errand);
                errand.AgentCounterOffers++;
                await _context.SaveChangesAsync();
                return agentDetail;
            }
            else
            {
                return null; 
            }
        }

        public async Task<AgentDto> AgentDeclinePrice(Guid errandId)
        {
            var errand = await _context.Errands.FindAsync(errandId);

            if (errand == null)
            {
                return null; // or you can throw an exception or handle it as you prefer
            }

            // Soft delete the errand
            errand.IsDeleted = true;

            // Get the agent using errand.AgentId
            var agent = await _context.agents.FindAsync(errand.AgentId);

            if (agent != null)
            {
                // Mark the agent as deleted
                agent.IsDeleted = true;
            }

            await _context.SaveChangesAsync(); // Save changes to update errand and agent

            // Reassign the errand to another agent
            try
            {
                var assignedAgent = await _agentRepository.AssignAgentAsync(errand);
                var appUser = await _context.Users.FindAsync(assignedAgent.AppUserId);
                var agentDetail = _mapper.Map<AgentDto>(assignedAgent); // Assuming assignedAgent is an Agent entity
                agentDetail.FirstName = appUser.FirstName;
                agentDetail.LastName = appUser.LastName;
                agentDetail.Email = appUser.Email;
                agentDetail.PhoneNumber = appUser.PhoneNumber;
                agentDetail.Price = errand.Price;

                await _context.SaveChangesAsync();
                return agentDetail;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }



        public async Task<AgentDto> UserAcceptPrice(Guid errandId)
        {
            var errand = await _context.Errands.FindAsync(errandId);

            if (errand == null)
            {
                return null;
            }

            // Get the agent from the errand
            var agent = await _context.agents.FindAsync(errand.AgentId);

            if (agent != null)
            {
                // Set isActive to true
                agent.IsActive = true;

                // Retrieve the AppUser associated with the Agent
                var appUser = await _context.Users.FindAsync(agent.AppUserId);

                // Map agent and AppUser details to AgentDto
                var agentDetail = _mapper.Map<AgentDto>(agent);
                agentDetail.FirstName = appUser.FirstName;
                agentDetail.LastName = appUser.LastName;
                agentDetail.Email = appUser.Email;
                agentDetail.PhoneNumber = appUser.PhoneNumber;
                agentDetail.Price = errand.Price;

                await _context.SaveChangesAsync(); // Save the changes to update isActive property

                return agentDetail;
            }
            else
            {
                return null;
            }
        }



        public async Task<PriceDto> UserCounterPrice(PriceNegotiationDto request)
        {
            var errand = await _context.Errands.FindAsync(request.ErrandId);

            if (errand == null)
            {
                return null; /*"Errand not found"*/
            }

            var agent = await _context.agents.FindAsync(errand.AgentId);

            if (errand.UserCounterOffers < 2)
            {
                var appUser = await _context.Users.FindAsync(agent.AppUserId);
                var agentDetail = _mapper.Map<PriceDto>(agent);
                agentDetail.FirstName = appUser.FirstName;
                agentDetail.LastName = appUser.LastName;
                agentDetail.Email = appUser.Email;
                agentDetail.PhoneNumber = appUser.PhoneNumber;
                agentDetail.Price = request.CounterPrice;

                // Update price request on database
                errand.Price = request.CounterPrice;
                _context.Errands.Update(errand);
                errand.AgentCounterOffers++;
                await _context.SaveChangesAsync();
                return agentDetail;
            }
            else
            {
                return null;
            }
        }

        public async Task<AgentDto> UserDeclinePrice(Guid errandId)
        {
            var errand = await _context.Errands.FindAsync(errandId);

            if (errand == null)
            {
                return null; // or you can throw an exception or handle it as you prefer
            }

            // Soft delete the errand
            errand.IsDeleted = true;

            // Get the agent using errand.AgentId
            var agent = await _context.agents.FindAsync(errand.AgentId);

            if (agent != null)
            {
                // Mark the agent as deleted
                agent.IsDeleted = true;
            }

            await _context.SaveChangesAsync(); // Save changes to update errand and agent

            // Reassign the errand to another agent
            try
            {
                var assignedAgent = await _agentRepository.AssignAgentAsync(errand);
                var appUser = await _context.Users.FindAsync(assignedAgent.AppUserId);
                var agentDetail = _mapper.Map<AgentDto>(assignedAgent); // Assuming assignedAgent is an Agent entity
                agentDetail.FirstName = appUser.FirstName;
                agentDetail.LastName = appUser.LastName;
                agentDetail.Email = appUser.Email;
                agentDetail.PhoneNumber = appUser.PhoneNumber;
                agentDetail.Price = errand.Price;

                await _context.SaveChangesAsync();
                return agentDetail;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


    }
}

