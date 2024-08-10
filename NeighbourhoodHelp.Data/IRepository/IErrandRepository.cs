
using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Infrastructure.Helpers;
using NeighbourhoodHelp.Model.DTOs;

namespace NeighbourhoodHelp.Data.IRepository
{
    public interface IErrandRepository
    {

        Task<ErrandAssignmentDto> CreateErrand(CreateErrandDto createErrand);
        Task<IList<GetErrandDto>> GetAllErrandsByAppUserIdAsync(Guid userId, PaginationParameters paginParams);
        Task<IList<GetErrandDto>> GetAllErrandsByAgentIdAsync(Guid agentId, PaginationParameters paginParams);
        Task<PendingErrandDto> GetPendingErrandByAgentId(Guid agentId);
        Task<bool> CompleteErrand(Guid errandId);
        Task<int> GetTotalCompletedErrandsForAgentAsync(Guid agentId);
        Task<decimal> GetTotalAmountEarnedByAgentAsync(Guid agentId);

    }
}
