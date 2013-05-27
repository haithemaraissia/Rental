using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using RentalMobile.Models;


namespace RentalMobile.Helpers
{
    public static class UserHelper
    {
        private static readonly DB_33736_rentalEntities DB = new DB_33736_rentalEntities();

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
            var tenant = DB.Tenants.FirstOrDefault(x => x.GUID == userID);
            if (tenant != null) return tenant.TenantId;
            return null;
        }

        public  static int? GetTenantID()
        {
            var userID = GetUserGUID();
            var tenant = DB.Tenants.FirstOrDefault(x => x.GUID == userID);
            if (tenant != null) return tenant.TenantId;
            return null;
        }

        public static int? GetTenantID(int id)
        {
            var userID = DB.Tenants.FirstOrDefault(x => x.TenantId == id);
            if (userID != null)
            {
                var Tenant = DB.Tenants.FirstOrDefault(x => x.GUID == userID.GUID);
                if (Tenant != null) return Tenant.TenantId;
            }
            return null;
        }

        public static int? GetAgentID()
        {
            var userID = GetUserGUID();
            var agent = DB.Agents.FirstOrDefault(x => x.GUID == userID);
            if (agent != null) return agent.AgentId;
            return null;
        }

        public static int? GetAgentID(int id)
        {
            var userID = DB.Agents.FirstOrDefault(x => x.AgentId == id);
            if (userID != null)
            {
                var Agent = DB.Agents.FirstOrDefault(x => x.GUID == userID.GUID);
                if (Agent != null) return Agent.AgentId;
            }
            return null;
        }

        public static int? GetOwnerID()
        {
            var userID = GetUserGUID();
            var owner = DB.Owners.FirstOrDefault(x => x.GUID == userID);
            if (owner != null) return owner.OwnerId;
            return null;
        }

        public static int? GetOwnerID(int id)
        {
            var userID = DB.Owners.FirstOrDefault(x => x.OwnerId == id);
            if (userID != null)
            {
                var owner = DB.Owners.FirstOrDefault(x => x.GUID == userID.GUID);
                if (owner != null) return owner.OwnerId;
            }
            return null;
        }

        public static int? GetSpecialistID()
        {
            var userID = GetUserGUID();
            var specialist = DB.Specialists.FirstOrDefault(x => x.GUID == userID);
            if (specialist != null) return specialist.SpecialistId;
            return null;
        }

        public static int? GetSpecialistID(int id)
        {
            var userID = DB.Specialists.FirstOrDefault(x => x.SpecialistId == id);
            if (userID != null)
            {
            var specialist = DB.Specialists.FirstOrDefault(x => x.GUID == userID.GUID);
            if (specialist != null) return specialist.SpecialistId; 
            }
            return null;
        }

        public static bool ValidateLocation(string location)
        {
            var address = String.Format("http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false", location.Replace(" ", "+"));
            var result = new System.Net.WebClient().DownloadString(address);
            var test = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);
            return test.status == "OK";
        }

        public static string GetFormattedAdress(string location)
        {
            var address = String.Format("http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false", location.Replace(" ", "+"));
            var result = new System.Net.WebClient().DownloadString(address);
            var test = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);
            return test.results[0].formatted_address;
        }

        public static string GetFormattedLocation(string address, string city, string country)
        {
            if (ValidateLocation(address))
            {
                return GetFormattedAdress(address);
            }

            address = city;
            if (ValidateLocation(address))
            {
                return GetFormattedAdress(address);
            }

            address = country;
            return GetFormattedAdress(ValidateLocation(address) ? address : "USA");
        }

    }
}



