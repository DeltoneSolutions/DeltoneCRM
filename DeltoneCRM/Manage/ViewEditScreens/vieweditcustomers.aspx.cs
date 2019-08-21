using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Globalization;
using System.Data;

namespace DeltoneCRM.Manage.ViewEditScreens
{
    public partial class vieweditcustomers : System.Web.UI.Page
    {
        protected string superAccount = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ReLocation.Value = Request.QueryString["loc"].ToString();
            loadAccountOwners();
            ReturnAllItems();
        }

        protected string ReturnAllItems()
        {
            String strOutput = "";
            String OwnerShipAdminID = "";

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CP.CompanyID, CP.CompanyName,CP.OwnershipAdminID,CP.IsSupperAcount,LG.LoginID, LG.FirstName, LG.LastName, CP.CompanyWebsite, CP.Active, CP.OwnershipPeriod FROM dbo.Companies CP, dbo.Logins LG WHERE CP.OwnershipAdminID  = LG.LoginID AND CP.CompanyID = " + Request.QueryString["cid"];
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CompanyWebsite.Value = sdr["CompanyWebsite"].ToString();
                            CompanyName.Value = sdr["CompanyName"].ToString();
                            AccountOwner.Value = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                            // AccountOwnerList.SelectedValue = sdr["LoginID"].ToString();
                            hidden_company_id.Value = sdr["CompanyID"].ToString();
                            hidden_owner_id.Value = sdr["LoginID"].ToString();
                            OwnerShipAdminID = sdr["OwnershipAdminID"].ToString();
                            //if(sdr["OwnershipAdminID"]!=DBNull.Value)
                            //{
                            //    if (Convert.ToBoolean(sdr["OwnershipAdminID"].ToString()) == true)
                            //        superAccount = "yes";
                            //}
                            IFormatProvider culture = new CultureInfo("en-US", true);
                            if (sdr["OwnershipPeriod"].ToString() != "")
                            {
                                DateTime theDateOut = DateTime.Parse(sdr["OwnershipPeriod"].ToString().Substring(0, 9));
                                OwnershipPeriod.Value = theDateOut.Year.ToString() + "-" + theDateOut.Month.ToString().PadLeft(2, '0') + "-" + theDateOut.Day.ToString().PadLeft(2, '0');
                            }


                            //DateTime OutDate = DateTime.ParseExact(TempHolder.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

                        }

                    }
                    if (AccountOwnerList.Items.FindByValue(OwnerShipAdminID) != null)
                        AccountOwnerList.Items.FindByValue(OwnerShipAdminID).Selected = true;
                    else
                    {
                        var defaultValue = "7";
                        if (AccountOwnerList.Items.FindByValue(defaultValue) != null)
                            AccountOwnerList.Items.FindByValue(defaultValue).Selected = true;
                    }
                }
                conn.Close();

            }
            return strOutput;
        }



        private void LoadDDL()
        {
        }




        private void loadAccountOwners()
        {
            DataTable accountOwners = new DataTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT FirstName + ' ' +  LastName as FullName,LoginID FROM dbo.Logins WHERE Username != 'Admin' and Active='Y' and LoginID NOT IN (4, 9, 8, 19,21)", con);

                    adapter.Fill(accountOwners);
                    //accountOwners.Columns.Add("FullName", typeof(string), "FirstName + ' ' + LastName");
                    AccountOwnerList.DataSource = accountOwners;
                    AccountOwnerList.DataTextField = "FullName";
                    AccountOwnerList.DataValueField = "LoginID";

                    AccountOwnerList.DataBind();
                    AccountOwnerList.AppendDataBoundItems = true;
                    AccountOwnerList.Items.Add(new ListItem("Select", "0"));

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }





    }
}