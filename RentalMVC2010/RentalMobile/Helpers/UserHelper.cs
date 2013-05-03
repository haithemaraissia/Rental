using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RentalMobile.Helpers
{
    public static class UserHelper
    {


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

        public static void ISBNewsCategory(string category)
        {

        }
    }
}



