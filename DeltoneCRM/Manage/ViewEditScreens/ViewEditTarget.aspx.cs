using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Data;

namespace DeltoneCRM.Manage.ViewEditScreens
{

    public static class ExceptionHelper
    {
        public static int LineNumber(this Exception e)
        {
           
            int linenum = 0;
            try
            {
                //linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5));

                //For Localized Visual Studio ... In other languages stack trace  doesn't end with ":Line 12"
                linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(' ')));

            }


            catch
            {
                //Stack trace is not available!
            }
            return linenum;
        }
    }

    public partial class ViewEditTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                loadAllUsers();
                returnAllItems();
            }
            catch (Exception ex)
            {
                
                int linenum = ex.LineNumber();
                
            }
        }

        private void loadAllUsers()
        {
            DataTable accountOwners = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT LoginID, FirstName, LastName FROM dbo.Logins WHERE Username != 'Admin'", con);

                    adapter.Fill(accountOwners);
                    accountOwners.Columns.Add("FullName", typeof(string), "FirstName + ' ' + LastName");

                    DropDownList1.DataSource = accountOwners;
                    DropDownList1.DataTextField = "FullName";
                    DropDownList1.DataValueField = "LoginID";
                    DropDownList1.DataBind();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        protected string returnAllItems()
        {
            String strOutput = "";
            String LoginID=String.Empty;
            String Month = String.Empty;


            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT TG.TargetID,LG.FirstName,LG.LoginID, LG.LastName, TG.TargetCommission, TG.TargetMonth, TG.TargetYear, TG.WorkingDays FROM dbo.Targets TG, dbo.Logins LG WHERE TG.LoginID = LG.LoginID AND TG.TargetID = " + Request.QueryString["cid"];
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            string Fullname = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                            NewUser.Value = Fullname;
                            NewCommission.Value = sdr["TargetCommission"].ToString();
                            NewDays.Value = sdr["WorkingDays"].ToString();
                            NewMonth.Value = sdr["TargetMonth"].ToString();
                            NewYear.Value = sdr["TargetYear"].ToString();
                            hdnEditTarget.Value = sdr["TargetID"].ToString();


                            LoginID = sdr["LoginID"].ToString();
                            Month = sdr["TargetMonth"].ToString();
                            //DropDownList2.Items.FindByValue(Month).Selected = true;


                        }

                    }
                    conn.Close();
                    //Set the User and  the Month
                    DropDownList1.Items.FindByValue(LoginID).Selected = true;
                    DropDownList2.Items.FindByValue(Month).Selected = true;

                }

            }
            return strOutput;
        }
    }
}