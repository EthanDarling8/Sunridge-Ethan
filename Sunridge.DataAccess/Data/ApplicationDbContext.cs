﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sunridge.Models;
using Sunridge.Models.Models;

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
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<DocumentCategory> DocumentCategory { get; set; }
        public DbSet<Document> Document { get; set; }

    }
}
