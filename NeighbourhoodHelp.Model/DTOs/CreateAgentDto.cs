using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NeighbourhoodHelp.Model.DTOs
{
    public class CreateAgentDto 
    {
        
        [Required]
        [RegularExpression("^[0-9]*$")]
        [StringLength(11, ErrorMessage = "Eleven digits required")]
        public string NIN { get; set; } 
        [Required]
      /* [DataType(DataType.Date)]*/
        public string DateOfBirth { get; set; } 
        [Required]
        public IFormFile Document { get; set; } 
        [Required]
        public  string PostalCode { get; set; }

        
    }
}
