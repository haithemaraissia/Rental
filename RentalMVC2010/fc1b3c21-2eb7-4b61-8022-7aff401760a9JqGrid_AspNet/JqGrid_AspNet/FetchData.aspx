<%@ Page Language="C#" %>
<%@ Import Namespace="System.Web.Script.Serialization"%>
<%@ Import Namespace="System.Collections.ObjectModel"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(GetData());
        Response.End();
        
    }
    
    protected string GetData()
    {
        //if (Equals(Request.Params.Get("oper"), Oper.edit.ToString()))
        //{
        //    list = new Collection<People>
        //               {
        //                   new People {id = 1, name = Request.Params.Get("name") , gender = Convert.ToInt32(Request.Params.Get("gender")), isClosed = Convert.ToBoolean(Request.Params.Get("isClosed"))},
        //                   new People {id = 2, name = "Monu", gender = 2, isClosed = false},
        //                   new People {id = 3, name = "Vijai", gender = 1, isClosed = true},
        //                   new People {id = 4, name = "Simi", gender = 2, isClosed = true}
        //               };
        //}
        //else
        //{
        var list = new Collection<People>
                              {
                                  new People {Id = 1, Name = "Shaitender", Gender = 1, IsClosed = false},
                                  new People {Id = 2, Name = "Monu", Gender = 1, IsClosed = false},
                                  new People {Id = 3, Name = "Jai", Gender = 1, IsClosed = true},
                                  new People {Id = 4, Name = "Swati", Gender = 2, IsClosed = true},
                                  new People {Id = 5, Name = "Shweta", Gender = 2, IsClosed = true}
                              };
       
        /// hard coded value passed for demo purpose
        /// Implement you logic :) !!
        return GridData(1, 1, list.Count, list);
    }

    public string GridData(int noOfPages, int startPage, int noOfRecords, Collection<People> list)
    {
        var gridData = new
                           {
                               total = noOfPages,
                               page = startPage,
                               records = noOfRecords,
                               rows = list,
                           };
        
        var jsonSerializer = new JavaScriptSerializer();
        return jsonSerializer.Serialize(gridData);

    }
    


    public class People
    {
        public People()
        {
            Name = string.Empty;
            Id = 0;
            Gender = 0;
            IsClosed = false;
        }



        public string Name { get; set; }
        public int Id { get; set; }
        public int Gender { get; set; }
        public bool IsClosed { get; set; }
    }
    
    public enum Oper
    {
        edit=1,
        del=2,
        add=3
    }





    

</script>

