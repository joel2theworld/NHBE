using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Model.DTOs
{
    public class UpdateAgentProfileDto
    {
        public Guid Id { get; set; }
        public string PostalCode { get; set; }
    }
}
