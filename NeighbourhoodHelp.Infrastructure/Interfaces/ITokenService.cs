using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
