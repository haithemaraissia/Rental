//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jqGridAddEditDelete.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UnitGallery
    {
        public int UnitGalleryId { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }
        public int Rank { get; set; }
        public int UnitId { get; set; }
    
        public virtual Unit Unit { get; set; }
    }
}
