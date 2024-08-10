using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Infrastructure.Helpers
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 20;
    }
}
