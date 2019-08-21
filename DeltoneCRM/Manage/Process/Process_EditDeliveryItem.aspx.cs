using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using DeltoneCRM_DAL;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_EditDeliveryItem : System.Web.UI.Page
    {
        String connString;

        protected void Page_Load(object sender, EventArgs e)
        {
            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            CusDeliveryDAL cusdal = new CusDeliveryDAL(connString);
             
            int itemid= Int32.Parse(Request.Form["itemid"].ToString());
            String delname=Request.Form["delname"].ToString();
            float delcost=float.Parse(Request.Form["delcost"].ToString());
            string ActInact = Request.Form["ActInact"].ToString();

            string finalActInact = "";

            if (ActInact == "false")
            {
                finalActInact = "N";
            }
            else
            {
                finalActInact = "Y";
            }

            Response.Write(cusdal.UpateDeliveryItems(itemid,delname,delcost, finalActInact));
        }
    }
}