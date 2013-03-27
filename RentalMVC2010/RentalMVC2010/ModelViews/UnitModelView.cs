using System.Collections.Generic;

namespace RentalMVC2010.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class UnitModelView 
    {

        public Unit Units { get; set; }
        public List<UnitGallery> UnitGalleries { get; set; }
    }
}
