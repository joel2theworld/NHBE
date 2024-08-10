using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Core.IServices;
using NeighbourhoodHelp.Data.IRepository;
using NeighbourhoodHelp.Infrastructure.Helpers;
using NeighbourhoodHelp.Model.DTOs;
using NeighbourhoodHelp.Model.Entities;

namespace NeighbourhoodHelp.Core.Services
{
    public class ErrandServices : IErrandServices
    {
        private readonly IErrandRepository _errandRepository;

        public ErrandServices(IErrandRepository errandRepository)
        {
            _errandRepository = errandRepository;
        }

        public async Task<ErrandAssignmentDto> CreateErrand(CreateErrandDto errandDto)
        {
            return await _errandRepository.CreateErrand(errandDto);
        }

        public async Task<IList<GetErrandDto>> GetAllErrandsByAppUserIdServiceAsync(Guid userId, PaginationParameters paginParams)
        {
            return await _errandRepository.GetAllErrandsByAppUserIdAsync(userId, paginParams);
        }

        public async Task<IList<GetErrandDto>> GetAllErrandsByAgentIdServiceAsync(Guid agentId, PaginationParameters paginParams)
        {
            return await _errandRepository.GetAllErrandsByAgentIdAsync(agentId, paginParams);
        }

        public async Task<PendingErrandDto> GetPendingErrandByAgentId(Guid agentId)
        {
            // Add any business logic or validation here if needed
            return await _errandRepository.GetPendingErrandByAgentId(agentId);
        }

        public async Task<bool> CompleteErrand(Guid errandId)
        {
            return await _errandRepository.CompleteErrand(errandId);
        }
        
        public Task<int> GetTotalCompletedErrandsForAgentAsync(Guid agentId)
        {
            return _errandRepository.GetTotalCompletedErrandsForAgentAsync(agentId);
        }

        public Task<decimal> GetTotalAmountEarnedByAgentAsync(Guid agentId)
        {
            return _errandRepository.GetTotalAmountEarnedByAgentAsync(agentId);
        }
    }
}
