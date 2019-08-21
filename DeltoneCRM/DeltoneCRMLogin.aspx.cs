using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using DeltoneCRM_DAL;

namespace DeltoneCRM
{
    public partial class DeltoneCRMLogin : System.Web.UI.Page
    {
        String strUserProfile = "ADMIN";//StandardUser

        String strUser = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Msg.Text = "";
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (!(txtUserName.Text.Equals(String.Empty)) && !(txtPassWord.Text.Equals(String.Empty)))
            {

                String strUser = FetchUser(txtUserName.Text.Trim(), txtPassWord.Text.Trim());
                String[] arr = strUser.Split('|');
                if (arr[0] == "Invalid")
                {
                    //Invalid Login Message
                    Response.Write("<script>alert('The login credentials you supplied are incorrect. Please try again. If you are unable login, please contact your administrator');</script>");
                }
                else
                {
                    Session["LoggedUser"] = arr[2].ToString() + " " + arr[3].ToString();

                    //Valid Login Check the User Profile
                    if (arr[1].Trim().Equals("1"))
                    {
                        //Admin User 
                        Session["USERPROFILE"] = "ADMIN";
                        Session["LoggedUserID"] = arr[4].ToString();
                        Session.Timeout = 60;
                    }
                    else
                    {
                        //Standard User
                        Session["USERPROFILE"] = "STANDARD";
                        Session["LoggedUserID"] = arr[4].ToString();
                        Session.Timeout = 60;
                       
                    }
                    moveAccountsIntoHouse(arr[4].ToString());
                    //Write to database for LoginAudit
                    SqlConnection LAconn = new SqlConnection();
                    LAconn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    String strSQLInsertStmt = "INSERT INTO dbo.LoginAudit (UserID, LoggedDateTime) VALUES (@LoggedUser, CURRENT_TIMESTAMP)";
                    SqlCommand LAcmd = new SqlCommand(strSQLInsertStmt, LAconn);
                    LAcmd.Parameters.AddWithValue("@LoggedUser", Session["LoggedUserID"].ToString());

                    try
                    {
                        LAconn.Open();
                        LAcmd.ExecuteNonQuery();
                        LAconn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message.ToString());
                    }

                    Response.Redirect("Dashboard1.aspx");
                }

            }


        }

        protected void moveAccountsIntoHouse(String UID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
           // new AccountMoveHouseDAL(connString).MoveRepAccounttoHouseAccount();
            //String strSqlStmt = "SELECT OwnershipPeriod, CompanyID FROM dbo.Companies WHERE OwnershipPeriod IS NOT NULL AND OwnershipPeriod <> '1900-01-01'  ";

            //using (SqlCommand cmd = new SqlCommand())
            //{
            //    cmd.CommandText = strSqlStmt;
            //    cmd.Connection = conn;
            //    conn.Open();
            //    using (SqlDataReader sdr = cmd.ExecuteReader())
            //    {
            //        if (sdr.HasRows)
            //        {
            //            while (sdr.Read())
            //            {
            //                DateTime theDateCheck = DateTime.MinValue;
            //                if (sdr["OwnershipPeriod"] != DBNull.Value)
            //                {
            //                    theDateCheck = Convert.ToDateTime(sdr["OwnershipPeriod"].ToString());
            //                    if (theDateCheck.AddDays(-1).Date <= DateTime.Now.Date )
            //                    {
            //                        performMove(sdr["CompanyID"].ToString());
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        protected void performMove(String CID)
        {
            SqlConnection LAconn = new SqlConnection();
          var conn=  ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
          LAconn.ConnectionString = conn;
          var comDal = new CompanyDAL(conn);
          var OwnerId = comDal.CheckCompanyOwnerByID(CID);
          if (!string.IsNullOrEmpty(OwnerId))
          {
              String strSQLInsertStmt = "UPDATE dbo.Companies SET OwnershipAdminID = @OwnershipAdminID, OwnershipPeriod = NULL WHERE CompanyID =@CompanyID";
              SqlCommand LAcmd = new SqlCommand(strSQLInsertStmt, LAconn);
              LAcmd.Parameters.AddWithValue("@OwnershipAdminID", OwnerId);
              LAcmd.Parameters.AddWithValue("@CompanyID", CID);
              try
              {
                  LAconn.Open();
                  LAcmd.ExecuteNonQuery();
                  LAconn.Close();
              }
              catch (Exception ex)
              {
                  Console.Write(ex.Message.ToString());
              }
          }
        }

        protected String FetchUser(String strUserName, String Password)
        {
            String strUser = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "select * from  dbo.Logins where Username='" + strUserName + "' and Password='" + Password + "' AND Active = 'Y'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {

                        while (sdr.Read())
                        {

                            strUser = "Valid" + "|" + sdr["AccessLevel"].ToString() + "|" + sdr["FirstName"].ToString() + "|" + sdr["LastName"].ToString() + "|" + sdr["LoginID"].ToString();

                        }
                    }
                    else
                    {
                        strUser = "Invalid" + "|" + String.Empty;
                    }
                }

            }
            conn.Close();

            return strUser;
        }
    }
}