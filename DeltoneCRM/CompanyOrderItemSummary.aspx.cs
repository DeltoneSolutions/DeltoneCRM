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
    public partial class CompanyOrderItemSummary : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

        public string ComSName { get; set; }
        public string ContactBuilder { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
             var comId = Request.QueryString["coId"];

             if (!IsPostBack)
             {
                 getCustomerDetails(comId);
             }
        }

        protected void btnaccountDash_Click(object sender, EventArgs e)
        {
            var comId = Request.QueryString["coId"];
            Response.Redirect("ConpanyInfo.aspx?companyid=" + comId);
        }


        protected void getCustomerDetails(String CustID)
        {
            String strCompanyName = "";
            String CustomerName = "";
            String CustomerBillAddressLine1 = "";
            String CustomerBillAddressLine2 = "";
            String CustomerBillCity = "";
            String CustomerBillPostcode = "";
            String CustomerBillState = "";

            String CustomerShipAddressLine1 = "";
            String CustomerShipAddressLine2 = "";
            String CustomerShipCity = "";
            String CustomerShipPostcode = "";
            String CustomerShipState = "";

            String CustomerContactNumber = "";
            String CustomerEmail = "";


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CT.*, CP.CompanyName FROM dbo.Contacts CT, dbo.Companies CP WHERE CT.CompanyID = CP.CompanyID AND CP.CompanyID = " + CustID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strCompanyName = sdr["CompanyName"].ToString();
                            CustomerName = sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString();
                            CustomerBillAddressLine1 = sdr["STREET_AddressLine1"].ToString();
                            CustomerBillAddressLine2 = sdr["STREET_AddressLine2"].ToString();
                            CustomerBillCity = sdr["STREET_City"].ToString();
                            CustomerBillPostcode = sdr["STREET_PostalCode"].ToString();
                            CustomerBillState = sdr["STREET_Region"].ToString();

                            CustomerShipAddressLine1 = sdr["POSTAL_AddressLine1"].ToString();
                            CustomerShipAddressLine2 = sdr["POSTAL_AddressLine2"].ToString();
                            CustomerShipCity = sdr["POSTAL_City"].ToString();
                            CustomerShipPostcode = sdr["POSTAL_PostalCode"].ToString();
                            CustomerShipState = sdr["POSTAL_Region"].ToString();

                            CustomerContactNumber = sdr["DEFAULT_AreaCode"].ToString() + ' ' + sdr["DEFAULT_Number"].ToString();
                            CustomerEmail = sdr["Email"].ToString();

                        }
                    }
                    conn.Close();
                }
            }

            ComSName = strCompanyName.ToUpper();
            ContactBuilder = CustomerName;
            ContactBuilder = ContactBuilder + " <br/> " + CustomerBillAddressLine1 + ' ' + CustomerBillAddressLine2;
            ContactBuilder = ContactBuilder + " <br/> " + CustomerBillCity.ToUpper() + ' ' + CustomerBillState.ToUpper() + ' ' + CustomerBillPostcode.ToUpper();

            ContactBuilder = ContactBuilder + " <br/> <br/> " + "Telephone: " + CustomerContactNumber + "  |  Email: " + CustomerEmail + "<br/>";

            CompanyNameDIV.InnerHtml = strCompanyName.ToUpper();
            ContactInfo.InnerHtml = CustomerName;
            StreetAddressLine1.InnerHtml = CustomerBillAddressLine1 + ' ' + CustomerBillAddressLine2;
            StreetAddressLine2.InnerHtml = CustomerBillCity.ToUpper() + ' ' + CustomerBillState.ToUpper() + ' ' + CustomerBillPostcode.ToUpper();
           
            ContactandEmail.InnerHtml = "Telephone: " + CustomerContactNumber + "  |  Email: " + CustomerEmail;

        }
    }
}