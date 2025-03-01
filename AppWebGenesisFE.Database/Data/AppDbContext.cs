﻿using AppWebGenesisFE.Models.Entities;
using AppWebGenesisFE.Models.Entities.Catalog;
using AppWebGenesisFE.Models.Entities.Customer;
using AppWebGenesisFE.Models.Entities.Tenant;
using AppWebGenesisFE.Models.Interfaces;
using AppWebGenesisFE.ServiceDefaults;
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
        private readonly ITenantService _tenantService;

        public AppDbContext(DbContextOptions<AppDbContext> options,
        ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
            Database.EnsureCreated();
        }

        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<IdentificationTypeModel> IdentificationTypes { get; set; }
        public DbSet<ProvinceModel> Provinces { get; set; }
        public DbSet<CantonModel> Cantons { get; set; }
        public DbSet<DistrictModel> Districts { get; set; }
        public DbSet<RegionModel> Region { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<TenantModel> Tenants { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<RoleModel> Roles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var tenantId = _tenantService.GetCurrentTenantId();

                // Si tenantId es 0, significa que estamos en proceso de login/registro
                if (tenantId != 0)
                {
                    foreach (var entry in ChangeTracker.Entries<IHasTenant>().ToList())
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                            case EntityState.Modified:
                                entry.Entity.TenantId = tenantId;
                                break;
                        }
                    }
                }
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                // Manejar el caso donde no hay tenant (login/registro)
                return base.SaveChangesAsync(cancellationToken);
            }
        }
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


            modelBuilder.Entity<TenantModel>()
                  .HasMany(e => e.Users)
                  .WithOne(e => e.Tenant)
                  .OnDelete(DeleteBehavior.Restrict);


            // Aplicar filtro global por tenant
            modelBuilder.Entity<CustomerModel>()
                .HasQueryFilter(x => x.TenantId == _tenantService.GetCurrentTenantId());

        }
    }
}
