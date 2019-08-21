using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using DeltoneCRM_DAL;
using System.Configuration;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_EditCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            String strCompanyName = Request.Form[""];
            





          


        }
    }
}