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
    public partial class Process_FetchLOG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            LogginDAL logdal;
            String strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            logdal = new LogginDAL(strConnString);
            if (!String.IsNullOrEmpty(Request.Form["Order_ID"]))
            {
                String OrderID = Request.Form["Order_ID"].ToString();
                //Get the Latest  Log Message given by LogID
                String Message = logdal.FetchFromLog(Int32.Parse(OrderID));
                Response.Write(Message);
            }

        }
    }
}