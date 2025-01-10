using AppWebGenesisFE.Models.Entities.Catalog;
using AppWebGenesisFE.Models.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Database.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<IdentificationTypeModel> IdentificationTypes { get; set; }
        public DbSet<ProvinceModel> Provinces { get; set; }
        public DbSet<CantonModel> Cantons { get; set; }
        public DbSet<DistrictModel> Districts { get; set; }
        public DbSet<RegionModel> Region { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentificationTypeModel>()
                .HasMany(e => e.Customers)
                .WithOne(e => e.IdentificationType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DistrictModel>()
                .HasMany(e => e.CustomerModels)
                .WithOne(e => e.District)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProvinceModel>()
                .HasMany(e => e.Cantons)
                .WithOne(e => e.Province)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CantonModel>().HasKey(c => c.CantonID);

            modelBuilder.Entity<CantonModel>()
            .HasMany(e => e.Districts)
            .WithOne(e => e.Canton)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegionModel>()
                .HasMany(e => e.Districts)
                .WithOne(e => e.Region)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
