
using NeighbourhoodHelp.Model.DTOs;
using NeighbourhoodHelp.Model.Entities;

namespace NeighbourhoodHelp.Data.IRepository
{
    public interface IPriceRepository
    {
        Task<AgentDto> AgentAcceptPrice(Guid errandId);
        Task<PriceDto> AgentCounterPrice(PriceNegotiationDto request);
        Task<AgentDto> AgentDeclinePrice(Guid errandId);
        Task<AgentDto> UserAcceptPrice(Guid errandId);
        Task<PriceDto> UserCounterPrice(PriceNegotiationDto request);
        Task<AgentDto> UserDeclinePrice(Guid errandId);
    }
}