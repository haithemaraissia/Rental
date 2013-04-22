using System.Collections.Generic;

namespace RentalMobile.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class TenantModelView 
    {

        public Tenant Tenants { get; set; }
        public List<TenantShowing> TenantShowings { get; set; }
        //public List<TenantMaintenance> TenantMaitenance { get; set; }
    }
}
