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
    
    public partial class UnitPricing
    {
        public int UnitId { get; set; }
        public Nullable<double> Rent { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<System.DateTime> AvailableDate { get; set; }
        public Nullable<double> Deposit { get; set; }
        public Nullable<double> AppilcationFee { get; set; }
        public Nullable<bool> Section_8_Eligible { get; set; }
    }
}