using System.Collections.Generic;

namespace RentalMobile.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class TenantMaintenanceViewModel
    {
        public Tenant Tenant;
        public List<MaintenanceOrder> MaintenanceOrders;

        //public int TenantID { get; set; }

        //public int MaintenanceID { get; set; }
        //public int UnitID { get; set; }
        //public System.DateTime MaintenanceDate { get; set; }
        //public int UrgencyID { get; set; }
        //public string Description { get; set; }
        //public int ServiceTypeID { get; set; }
        //public bool PetsinUnit { get; set; }
        //public bool TenantPresence { get; set; }

        //public  List<CrewMaintenance> CrewMaintenances { get; set; }
        //public ServiceType ServiceType { get; set; }
        //public UrgencyType UrgencyType { get; set; }
        //public  List<MaintenancePhoto> MaintenancePhotoes { get; set; }
        //public  List<OwnerMaintenance> OwnerMaintenances { get; set; }
        //public  List<TenantMaintenance> TenantMaintenances { get; set; }

    }
}
