
using NeighbourhoodHelp.Model.Enums;

namespace NeighbourhoodHelp.Model.Entities
{
    public class PriceNegotiation
    {
        public AgentResponse AgentResponse { get; set; }
        public decimal CounterPrice { get; set; }
        public Guid ErrandId { get; set; }
        public Errand Errand { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}