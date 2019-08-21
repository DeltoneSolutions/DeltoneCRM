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
    public partial class ProcessSetSuperCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetSuperAccount(Request.QueryString["lock"].ToString(), Request.QueryString["CompID"].ToString());
        }

        public void SetSuperAccount(string status, string comId)
        {
            var countAcounts = GetSuperAccountCount();

            if (status == "Y")
            {
                if (countAcounts == 20)
                {
                    Response.Write("No");
                }
                else
                {
                    UpdateCompanyStatus(Convert.ToInt32(comId), true);
                    Response.Write("OK");
                }
            }
            else
            {
                UpdateCompanyStatus(Convert.ToInt32(comId), false);
                Response.Write("OK");
            }


        }
        public void UpdateCompanyStatus(int comId, bool statusSuper)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            var comStatusMesage = "";
            var comLocked = "";

            try
            {

                String SQLStmt = "UPDATE dbo.Companies SET IsSupperAcount =@isSupperAcount   WHERE CompanyID =@comId ";
                SqlCommand cmd = new SqlCommand(SQLStmt, conn);
                conn.Open();

                cmd.Parameters.AddWithValue("@isSupperAcount", statusSuper);

                cmd.Parameters.AddWithValue("@comId", comId);
                cmd.ExecuteNonQuery();
                conn.Close();


                var comstatusMess = comStatusMesage + " - " + comLocked;
                CreateAuditRecord(comId
                    , "", comstatusMess);


                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void CreateAuditRecord(int primaryKey, string coluhmnpreVious, string columcurrnet)
        {
            var columnName = "Status";
            var talbeName = "Companies";
            var ActionType = "Changed";

            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var newvalues = "Company  Id " + primaryKey + " : " + columcurrnet;

            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(coluhmnpreVious, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, primaryKey);
        }

        private int GetSuperAccountCount()
        {
            var count = 0;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                using (SqlCommand cmd = new SqlCommand())
                {

                    var userID = Session["LoggedUserID"];

                    cmd.CommandText = @"SELECT COUNT ( COMPANYID) as coCount FROM Companies WHERE IsSupperAcount =1 AND OwnershipAdminID=@ownerID";
                    cmd.Parameters.AddWithValue("@ownerID", userID);

                    cmd.Connection = con;
                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                count = Convert.ToInt32(sdr["coCount"].ToString());
                            }
                        }
                    }

                }
                con.Close();
            }

            return count;
        }

    }
}