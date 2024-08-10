using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Model.DTOs
{
    public class CreateErrandDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Date { get; set; }
        public string ItemName { get; set; }
        public string Weight { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public string UserId { get; set;}
        public decimal Price { get; set; }
    }
}
