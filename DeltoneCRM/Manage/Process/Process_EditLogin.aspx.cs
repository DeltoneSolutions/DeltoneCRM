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
    public partial class Process_EditLogin : System.Web.UI.Page
    {
        String strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginDAL logingdal = new LoginDAL(strConnString);
            int LoginID = Int32.Parse(Request.Form["loginid"].ToString());
            String strFirstName = Request.Form["firstname"].ToString();
            String LastName = Request.Form["lastname"].ToString();
            int AccessLevel = Int32.Parse(Request.Form["accesslevel"].ToString());
            string ActInact = Request.Form["ActInact"].ToString();
            String EmailAddy = Request.Form["EmailAddy"].ToString();
            String Username = Request.Form["Username"].ToString();
            String Password = Request.Form["Password"].ToString();
            int? Department =null;
            if (!string.IsNullOrEmpty(Request.Form["Department"].ToString()))
                Department = Int32.Parse(Request.Form["Department"].ToString());
            bool? canShow = null;
            if (!string.IsNullOrEmpty(Request.Form["donotshowstats"].ToString()))
            {
                var canShowddd = (Request.Form["donotshowstats"].ToString());
                if (canShowddd == "on")
                    canShow = true;
                else
                    canShow = false;
            }

            string finalActInact = "";

            if (ActInact == "false")
            {
                finalActInact = "N";
            }
            else
            {
                finalActInact = "Y";
            }

            Response.Write(logingdal.UpdateLogin(LoginID, strFirstName, LastName, AccessLevel, finalActInact, EmailAddy, Username, Password, Department, canShow));


        }
    }
}