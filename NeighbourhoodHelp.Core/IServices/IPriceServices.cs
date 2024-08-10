using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Model.DTOs;

namespace NeighbourhoodHelp.Core.IServices
{
    public interface IPriceServices
    {
        Task<AgentDto> AgentAcceptPrice(Guid errandId);
        Task<PriceDto> AgentCounterPrice(PriceNegotiationDto request);
        Task<AgentDto> AgentDeclinePrice(Guid errandId);
        Task<AgentDto> UserAcceptPrice(Guid errandId);
        Task<PriceDto> UserCounterPrice(PriceNegotiationDto request);
        Task<AgentDto> UserDeclinePrice(Guid errandId);
    }
}
