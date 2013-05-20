using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //http://www.techrepublic.com/blog/programming-and-development/embrace-the-json-standard-in-your-c-code/6684

        bool pass = false;
        FindGoogleLocation(out pass, "sdfsd");
    }

    public void FindGoogleLocation(out bool pass, string queue)
    {
        using (var wc = new System.Net.WebClient())
        {
            pass = false;

            try
            {
                //http://www.seomoz.org/ugc/everything-you-never-wanted-to-know-about-google-maps-parameters
             //   var jsonString = wc.DownloadString("http://maps.google.co.in/maps?q=@ViewBag.TenantGoogleMap&amp;z=13&amp;output=embed");


               var jsonString = wc.DownloadString("http://maps.google.co.in/maps?q=6108 cortez street, Merriam, KS &amp;z=13&amp;output=embed");
                var p = JsonHelper.JsonDeserialize<ZipTastic>(jsonString);
                Response.Write("result" + " --> " + p.state + " : " + p.country + "<br>");
                pass = true;
            }
            catch (Exception)
            {

                pass = false;
            }

        }
    }

    protected void Check_Click(object sender, EventArgs e)
    {
        //You have 404
        //Try with address or City, State, counrty or default

    }
}