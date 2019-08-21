using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.process
{
    public partial class ProcessLockCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SaveCompayLock(Request.QueryString["lock"].ToString(), Request.QueryString["CompID"].ToString());
        }

        //This Method Approve the Order Status 
        protected int SaveCompayLock(string lockval, string CompId)
        {
            int RowEffected = -1;


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var sqlStr = @"UPDATE dbo.Companies SET Hold =@hold  WHERE CompanyID =@comId ";
            var comLocked = "";

            var comId = Convert.ToInt32(CompId);
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sqlStr;
            cmd.Connection = conn;
            conn.Open();
            comLocked = " Not Locked";
            var holdStatus = "N";
            if (lockval == "Y")
            {
                holdStatus = "Y";
                comLocked = " Locked";
            }
            cmd.Parameters.AddWithValue("@hold", holdStatus);
            cmd.Parameters.AddWithValue("@comId", comId);

            cmd.ExecuteNonQuery();
            conn.Close();

            var comstatusMess = comLocked;
            CreateAuditRecord(comId
                , "", comstatusMess);


            return RowEffected;
        }

        private static void CreateAuditRecord(int primaryKey, string coluhmnpreVious, string columcurrnet)
        {
            var columnName = "Status";
            var talbeName = "Companies";
            var ActionType = "Changed";
            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var newvalues = "Company  Id " + primaryKey + " : " + columcurrnet;

            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(coluhmnpreVious, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, primaryKey);
        }



    }
}