using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Core.IServices;
using NeighbourhoodHelp.Data.IRepository;
using NeighbourhoodHelp.Model.DTOs;

namespace NeighbourhoodHelp.Core.Services
{
    public class PriceServices : IPriceServices
    {
        private readonly IPriceRepository _priceRepository;

        public PriceServices(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task<AgentDto> AgentAcceptPrice(Guid errandId)
        {
            // Add any business logic or validation here if needed
            return await _priceRepository.AgentAcceptPrice(errandId);
        }

        public async Task<PriceDto> AgentCounterPrice(PriceNegotiationDto request)
        {
            // Add any business logic or validation here if needed
            return await _priceRepository.AgentCounterPrice(request);
        }

        public async Task<AgentDto> AgentDeclinePrice(Guid errandId)
        {
            // Add any business logic or validation here if needed
            return await _priceRepository.AgentDeclinePrice(errandId);
        }

        public async Task<AgentDto> UserAcceptPrice(Guid errandId)
        {
            // Add any business logic or validation here if needed
            return await _priceRepository.UserAcceptPrice(errandId);
        }

        public async Task<PriceDto> UserCounterPrice(PriceNegotiationDto request)
        {
            // Add any business logic or validation here if needed
            return await _priceRepository.UserCounterPrice(request);
        }

        public async Task<AgentDto> UserDeclinePrice(Guid errandId)
        {
            // Add any business logic or validation here if needed
            return await _priceRepository.UserDeclinePrice(errandId);
        }
    }
}
