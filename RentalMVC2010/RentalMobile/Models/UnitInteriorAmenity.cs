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
    
    public partial class UnitInteriorAmenity
    {
        public int UnitId { get; set; }
        public int CoolingTypeId { get; set; }
        public int HeatingTypeId { get; set; }
        public bool Fireplace { get; set; }
        public bool Hot_Tub_Spa { get; set; }
        public bool Cable_Ready { get; set; }
        public bool Attic { get; set; }
        public int BasementTypeId { get; set; }
        public bool Double_Pane_Storm_Windows { get; set; }
        public int FloorCoveringId { get; set; }
        public int FoundationTypeId { get; set; }
        public bool Kitchen_Island { get; set; }
        public bool Vaulted_Ceiling { get; set; }
        public bool Ceiling_Fan { get; set; }
        public bool Jetted_Tub { get; set; }
    
        public virtual Unit Unit { get; set; }
    }
}
