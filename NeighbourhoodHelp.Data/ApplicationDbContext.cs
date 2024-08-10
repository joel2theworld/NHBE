using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        
        public virtual DbSet<Errand> Errands { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<AppUser> appUsers { get; set; }
        public virtual DbSet<Agent> agents { get; set; }


    }
}
