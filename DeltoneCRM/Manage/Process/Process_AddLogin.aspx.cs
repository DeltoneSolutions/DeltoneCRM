using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_AddLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strFirstName = Request.Form["NewFirstName"].ToString();
            String strLastName = Request.Form["NewLastName"].ToString();
            String strUsername = Request.Form["NewUsername"].ToString();
            String strPassword = Request.Form["NewPassword"].ToString();
            int AccessLevel = Int32.Parse(Request.Form["AccessLevel"].ToString());
            String strEmailAddy = Request.Form["NewEmailAddy"].ToString();
            int Department = Int32.Parse(Request.Form["Department"].ToString());

            DeltoneCRMDAL dal = new DeltoneCRMDAL();
            Response.Write(dal.AddNewLogin(strFirstName, strLastName, strUsername, strPassword, AccessLevel, strEmailAddy, Department));
        }
    }
}