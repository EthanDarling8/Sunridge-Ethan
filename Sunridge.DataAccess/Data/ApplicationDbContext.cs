using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sunridge.Models;
using Key = Sunridge.Models.Key;

namespace Sunridge.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<Owner>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // **** DbSets go here ****
        public DbSet<Banner> Banner { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<FireInfo> FireInfo { get; set; }
        public DbSet<BoardMember> BoardMember { get; set; }

        //Photo Gallery DbSets
        public DbSet<PhotoCategory> PhotoCategory { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbum { get; set; }
        public DbSet<Photo> Photo { get; set; }

        // Key DbSets
        public DbSet<Key> Key { get; set; }
        public DbSet<KeyLot> KeyLot { get; set; }

        // Lot DbSets
        public DbSet<Lot> Lot { get; set; }
        public DbSet<LotFile> LotFile { get; set; }
        public DbSet<Lot_OwnerFile> Lot_OwnerFile { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Lot_Owner> Lot_Owner { get; set; }
        public DbSet<Lot_Inventory> Lot_Inventory { get; set; }
       

        // Lost & Found DBSets
        public DbSet<LostItem> LostItem { get; set; }
    }
}
