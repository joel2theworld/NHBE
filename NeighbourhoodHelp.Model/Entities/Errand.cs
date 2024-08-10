using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NeighbourhoodHelp.Model.Enums;

namespace NeighbourhoodHelp.Model.Entities
{
    public class Errand : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Date { get; set; }
        public string ItemName { get; set; }
        public string Weight { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; } = string.Empty;
        public ErrandStatus ErrandStatus { get; set; }
        public decimal Price { get; set; }
        public int AgentCounterOffers { get; set; } = 0;
        public string AppUserId { get; set; }
 
        [JsonIgnore]
        public AppUser AppUser { get; set; }
        public Guid AgentId { get; set; }   
        public Agent Agent { get; set; }

        public Payment Payment { get; set; }
        public int UserCounterOffers { get; set; } = 0;

    }
}