using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Model.DTOs
{
    public class PriceNegotiationDto
    {
        public decimal CounterPrice { get; set; }
        public Guid ErrandId { get; set; }
    }
}
