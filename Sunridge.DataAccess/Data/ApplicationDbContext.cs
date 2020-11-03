using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // **** DbSets go here ****
        public DbSet<Lot> Lot { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<ClassifiedsItem> Classifieds { get; set; }
        public DbSet<ViewCount> ViewCount { get; set; }
        public DbSet<ClassifiedsCategory> ClassifiedsCategory { get; set; }
        public DbSet<ClassifiedsSubcategory> ClassifiedsSubcategory{ get; set; }
    }
}
