using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Model.DTOs
{
    public class ErrandAssignmentDto
    {

        public AppUser User { get; set; } 
        public Agent Agent { get; set; } 

    }
}
