using NeighbourhoodHelp.Model.DTOs;
using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Data.IRepository
{
    public interface IAgentRepository
    {
        Task<string> CreateAgentAsync(string id, CreateAgentDto agentDto);
        Task<ErrandDto> GetAgentByErrandIdAsync(Guid errandId);
        Task<List<GetAgentDto>> GetAllAgents();
        Task<Agent> AssignAgentAsync(Errand errand);
        Task<string> UpdateAgentProfile(UpdateAgentProfileDto agentProfileDto);

    }
}
