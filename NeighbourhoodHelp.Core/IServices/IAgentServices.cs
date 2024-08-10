using NeighbourhoodHelp.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Model.Entities;

namespace NeighbourhoodHelp.Core.IServices
{
    public interface IAgentServices
    {
        Task<string> CreateAgent(string id, CreateAgentDto agentDto);
        Task<List<GetAgentDto>> GetAllAgents();
        Task<ErrandDto> GetAgentByErrandIdAsync(Guid errandId);
        Task<string> UpdateAgentProfile(UpdateAgentProfileDto agentProfileDto);
        
    }
}
