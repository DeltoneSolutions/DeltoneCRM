using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using DeltoneCRM_DAL;
using System.Configuration;


namespace DeltoneCRM.Manage.Process
{
    public partial class CheckCustomerExsists : System.Web.UI.Page
    {
        static String ConnString;
        //static CompanyDAL dal;
        
        protected void Page_Load(object sender, EventArgs e)
        {
           ConnString= ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
           //dal = new CompanyDAL(ConnString);

           if (!String.IsNullOrEmpty(Request.Form["CompanyName"]))
           {
               String companyname = Request.Form["CompanyName"].ToString();
               String companyid = Request.Form["ID"].ToString();
               //Response.Write(dal.validateCompany(companyname, Int32.Parse(companyid)));
               Response.Write("-1");
           }

        }
    }
}