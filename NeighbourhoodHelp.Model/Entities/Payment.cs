using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Model.Enums;

namespace NeighbourhoodHelp.Model.Entities
{
    public class Payment : BaseEntity
    {
        public string ReferenceNumber { get; set; }
        public Guid ErrandId { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal Amount { get; set; }
    }
}
