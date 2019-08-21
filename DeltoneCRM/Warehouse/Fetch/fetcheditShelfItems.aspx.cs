using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Warehouse.Fetch
{
    public partial class fetcheditShelfItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var jsonString = String.Empty;
          
            Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }
          //  var serJsonDetails = (DisplayWShelfItemDetails)javaScriptSerializer.Deserialize(jsonString, typeof(DisplayWShelfItemDetails));
           // CreateShelfItems(serJsonDetails);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
          //  var serJsonDetails = (EditMode)javaScriptSerializer.Deserialize(jsonString, typeof(EditMode));
            var result = LoadEditShelfItems(jsonString);
            Response.Headers.Add("Content-type", "application/json");
            var jsonConver = javaScriptSerializer.Serialize(result);
            Response.Write(jsonConver);
        }

        public DisplayWShelfItemDetails LoadEditShelfItems(string shelfId)
        {
            var obj = new DisplayWShelfItemDetails();
            WShelfItemDetailsDAL shelfDAl = new WShelfItemDetailsDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
            obj = shelfDAl.GetDisplayWShelfItemDetailsBYId(Convert.ToInt32( shelfId));
            WareShelfDAL shelRow = new WareShelfDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
            var shelf = shelRow.GetShelfById(obj.LocationId);
            obj.LocationColumnName = shelf.ColumnName;
            return obj;

        }

        public class EditMode
        {
            public string Id { get; set; }
        }
    }
}