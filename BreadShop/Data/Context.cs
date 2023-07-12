using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using BreadShop4Bili.BreadShop.Utils;
using BreadShop4Bili.BreadShop.Models;
using Microsoft.EntityFrameworkCore;
using Profile = BreadShop4Bili.BreadShop.Models.Profile;

namespace BreadShop4Bili.BreadShop.Data
{
    public class Context : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($@"Data Source={FileHelper.DatabaseDirectoryPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().ToTable("Profiles");
            base.OnModelCreating(modelBuilder);
        }
    }
}
