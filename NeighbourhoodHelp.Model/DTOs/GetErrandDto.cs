using NeighbourhoodHelp.Model.Entities;
using NeighbourhoodHelp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Model.DTOs
{
    public class GetErrandDto
    {
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string ItemName { get; set; }
        public string Weight { get; set; }
        public int Quantity { get; set; }
        public ErrandStatus ErrandStatus { get; set; }
        public decimal Price { get; set; }
        public Agent Agent { get; set; }
        public AppUser AppUser { get; set; }
    }
}
