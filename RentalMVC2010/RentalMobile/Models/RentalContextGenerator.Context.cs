﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentalMobile.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_33736_rentalEntities : DbContext
    {
        public DB_33736_rentalEntities()
            : base("name=DB_33736_rentalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitGallery> UnitGalleries { get; set; }
        public DbSet<TenantShowing> TenantShowings { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantFavorite> TenantFavorites { get; set; }
        public DbSet<TenantSavedSearch> TenantSavedSearches { get; set; }
        public DbSet<CrewMaintenance> CrewMaintenances { get; set; }
        public DbSet<MaintenanceOrder> MaintenanceOrders { get; set; }
        public DbSet<MaintenancePhoto> MaintenancePhotoes { get; set; }
        public DbSet<OwnerMaintenance> OwnerMaintenances { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<TenantMaintenance> TenantMaintenances { get; set; }
        public DbSet<UrgencyType> UrgencyTypes { get; set; }
    }
}
