using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////var r = GEOCodeAddress("6108 cortez street SM KS US");

        //string Address = "6108 cortez street Merriam KS ";
        //var address = String.Format("http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false", Address.Replace(" ", "+"));
        //var result = new System.Net.WebClient().DownloadString(address);
        //var test = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);

        ////
        //if (test.status == "OK")
        //{

        //    Response.Write(string.Format("Correct Formatted address is {0}", test.results[0].formatted_address));


        //    Response.Write("<br/>"); Response.Write("<br/>");
        //    Response.Write(
        //        string.Format("Mapped with google is http://maps.google.co.in/maps?q={0}&amp;z=13&amp;output=embed",
        //                      test.results[0].formatted_address));
        //}


        //GetRightLocation();
    }


    public static dynamic GEOCodeAddress(String Address)
    {
        var address = String.Format("http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false", Address.Replace(" ", "+"));
        var result = new System.Net.WebClient().DownloadString(address);
        var jss = new JavaScriptSerializer();
        return jss.Deserialize<dynamic>(result);
    }


    public bool ValidateLocation(string location)
    {
        var address = String.Format("http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false", location.Replace(" ", "+"));
        var result = new System.Net.WebClient().DownloadString(address);
        var test = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);
        return test.status == "OK";
    }


    public string GetFormattedAdress(string location)
    {
        var address = String.Format("http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false", location.Replace(" ", "+"));
        var result = new System.Net.WebClient().DownloadString(address);
        var test = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);
        return test.results[0].formatted_address;
    }


    public string GetRightLocation(string myAddress)
    {
        if (ValidateLocation(myAddress))
        {
            return GetFormattedAdress(myAddress);
        }

        myAddress = "KS";
        if (ValidateLocation(myAddress))
        {
            return GetFormattedAdress(myAddress);
        }

        myAddress = "USA";
        return GetFormattedAdress(ValidateLocation(myAddress) ? myAddress : "USA");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetRightLocation(TextBox1.Text);
    }
}