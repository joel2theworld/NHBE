using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Model.DTOs
{
    public class LoggedInUserDto
    {

        public string token { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string id { get; set; }
    }
}
