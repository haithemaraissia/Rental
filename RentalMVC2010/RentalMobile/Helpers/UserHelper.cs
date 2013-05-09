using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using RentalMobile.Models;

namespace RentalMobile.Helpers
{
    public static class UserHelper
    {
        private static readonly DB_33736_rentalEntities _db = new DB_33736_rentalEntities();

        public static string Login()
        {
            return "~/NotAuthenticated/SignIn.aspx?ReturnUrl={0}" + HttpContext.Current.Request.Url.AbsoluteUri;

        }




        public static Guid? GetUserGUID()
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = Membership.GetUser(HttpContext.Current.User.Identity.Name);
                if (user != null && user.ProviderUserKey != null)
                {
                    return (Guid)user.ProviderUserKey;
                }
            }
            return null;
        }


        public static int? GetTenantID(Guid userID)
        {
            Tenant tenant = _db.Tenants.FirstOrDefault(x => x.GUID == userID);
            if (tenant != null) return tenant.TenantId;
            return null;
        }


        public static void ISBNewsCategory(string category)
        {

        }

    }
}



