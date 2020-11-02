using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // **** DbSets go here ****
        public DbSet<Lot> Lot { get; set; }
        public DbSet<LotFile> LotFile { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Lot_ApplicationUser> Lot_ApplicationUser { get; set; }
        public DbSet<Lot_Inventory> Lot_Inventory { get; set; }
       
    }
}
