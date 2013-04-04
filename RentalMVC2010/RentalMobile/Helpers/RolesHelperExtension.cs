using System.Collections.Generic;
using System.Web.Mvc;

namespace RentalMobile.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IEnumerable<SelectListItem> GetRoles(this HtmlHelper helper)
        {
            return new[] {
                new SelectListItem() { Text="Tenant" },
                new SelectListItem() { Text="Landlord" },
            };
        }
    }
}

