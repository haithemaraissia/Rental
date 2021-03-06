//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Tenant
    {
        public Tenant()
        {
            this.TenantShowings = new HashSet<TenantShowing>();
            this.TenantFavorites = new HashSet<TenantFavorite>();
            this.TenantSavedSearches = new HashSet<TenantSavedSearch>();
        }
    
        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<TenantShowing> TenantShowings { get; set; }
        public virtual ICollection<TenantFavorite> TenantFavorites { get; set; }
        public virtual ICollection<TenantSavedSearch> TenantSavedSearches { get; set; }
    }
}
