using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class CompanyContactInfoHold : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["theCID"] = Request.QueryString["cid"].ToString();
            populateValues(Session["theCID"].ToString());
        }

        private void populateValues(String cid)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strCompanyInfo = "SELECT CP.*, LG.FirstName, LG.LastName FROM dbo.Companies CP, dbo.Logins LG WHERE CompanyID = " + cid + " AND CP.OwnershipAdminID = LG.LoginID";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strCompanyInfo;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CompanyName.InnerHtml = sdr["CompanyName"].ToString();
                        Website.InnerHtml = "Website: " + sdr["CompanyWebsite"].ToString() + "  |  Account Owner: " + sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                        CompID.Value = sdr["CompanyID"].ToString();
                        if (Session["USERPROFILE"].ToString() == "ADMIN")
                        {
                            EditCompDIV.InnerHtml = "<a href='#' onclick='window.parent.Edit(" + cid + ")'><img ID='Image2' src='Images/btn_02.png' width='24' height='24'/></a>";
                        }
                        else
                        {
                            EditCompDIV.InnerHtml = "<img ID='Image2' src='Images/btn_02.png' width='24' height='24'/>";
                        }

                    }
                }
                conn.Close();
            }
        }



        public String populateContacts()
        {
            String htmlStr = "";
            String contactName = "";
            String contactEmail = "";
            String editLink = "";
            String CreateOrderLink = "";
            string CreateDummyOrderLink = "";

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strContactInfo = "SELECT * FROM dbo.Contacts WHERE CompanyID = " + Session["theCID"].ToString() + " AND Active = 'Y'";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strContactInfo;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        contactName = sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString();
                        contactEmail = sdr["Email"].ToString() + " | " + sdr["DEFAULT_AreaCode"].ToString() + " " + sdr["DEFAULT_Number"].ToString() + " | " + sdr["STREET_AddressLine1"].ToString() + " " + sdr["STREET_AddressLine2"].ToString() + " " + sdr["STREET_City"].ToString() + " " + sdr["STREET_Region"].ToString() + " " + sdr["STREET_PostalCode"].ToString();
                        editLink = "<input name='EditContact' style='cursor:pointer' type='button' id='EditContact' class='contacts-btn-edit' value='EDIT' onclick='window.parent.EditContact(" + sdr["ContactID"].ToString() + ")'/>";
                        CreateOrderLink = "<input name='CreateOrder' style='cursor:pointer' type='button' id='CreateOrder' class='contacts-btn-create-ordr' value='CREATE ORDER' onclick='window.parent.CreateOrder(" + sdr["ContactID"].ToString() + ", " + Session["theCID"].ToString() + ")'/>";
                        CreateDummyOrderLink = "<input name='CreateOrderDummy' style='cursor:pointer' type='button' id='CreateOrderDummy' class='contacts-btn-edit' value='CDO' onclick='window.parent.CreateOrderDummy(" + sdr["ContactID"].ToString() + ", " + Session["theCID"].ToString() + ")'/>";
                        htmlStr += "<tr><td><table align='center' cellpadding='0' cellspacing='0' class='width-980px-style'><tr><td class='contacts-001-style'>" + contactName + "</td><td class='contacts-002-style'>&nbsp;</td><td class='contacts-002-style'>&nbsp;</td><td class='contacts-003-style'>&nbsp;</td></tr><tr><td class='contacts-004-style'>" + contactEmail
                            + "</td>  </tr></table></td></tr>";
                    }
                }
                conn.Close();
            }

            return htmlStr;
        }
    }
}