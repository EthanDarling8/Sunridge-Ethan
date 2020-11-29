using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sunridge.Models;
using Sunridge.Models.ViewModels;
using System.Runtime.InteropServices.ComTypes;

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
        public DbSet<Forms> Forms { get; set; }

        // Document DbSets
        public DbSet<DocumentCategory> DocumentCategory { get; set; }
        public DbSet<DocumentFile> DocumentFile { get; set; }
        public DbSet<DocumentFileKeyword> DocumentFileKeyword { get; set; }
        public DbSet<DocumentSection> DocumentSection { get; set; }
        public DbSet<DocumentSectionText> DocumentSectionText { get; set; }

        // Classifieds
        public DbSet<ClassifiedsCategory> ClassifiedsCategory { get; set; }
        public DbSet<ClassifiedsItem> ClassifiedsItem { get; set; }

        // Fire Info
        public DbSet<FireInfo> FireInfo { get; set; }
        
        // Board Member DbSets
        public DbSet<OwnerBoardMember> OwnerBoardMember { get; set; }
        public DbSet<BoardMember> BoardMember { get; set; }

        // Common Area Asset DbSet
        public DbSet<Asset> Asset { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecord { get; set; }

        //Photo Gallery DbSets
        public DbSet<PhotoCategory> PhotoCategory { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbum { get; set; }
        public DbSet<Photo> Photo { get; set; }

        // Key DbSets
        public DbSet<Models.Key> Key { get; set; }
        public DbSet<KeyLot> KeyLot { get; set; }

        // Lot DbSets
        public DbSet<LotOwnerInvVM> LotOwnerInvVM { get; set; }
        public DbSet<Lot> Lot { get; set; }
        public DbSet<LotFile> LotFile { get; set; }
        public DbSet<Lot_OwnerFile> Lot_OwnerFile { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Lot_Owner> Lot_Owner { get; set; }
        public DbSet<Lot_Inventory> Lot_Inventory { get; set; }
       

        // Lost & Found DBSets
        public DbSet<LostItem> LostItem { get; set; }

        //Classifieds DbSets
        public DbSet<ClassifiedsCategory> ClassifiedsCategory { get; set; }
        public DbSet<ClassifiedsItem> ClassifiedsItem { get; set; }
        //public DbSet<ClassifiedsSubcategory> ClassifiedsSubcategory { get; set; }


        // Many to Many relationship handling
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LotOwnerInvVM>().HasNoKey().ToView(null);

            builder.Entity<Lot_Owner>()
                .HasKey(lo => new { lo.LotId, lo.OwnerId }); //Create a Composite Key for the Lot_Owner table based on their foreign keys.

            builder.Entity<Lot_Inventory>()
                .HasKey(li => new { li.LotId, li.InventoryId }); //Create a Composite Key for the Lot_Inventory table based on their foreign keys.
        }
        
    }

}
