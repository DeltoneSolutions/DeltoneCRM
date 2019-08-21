using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes; 

namespace DeltoneCRM.process
{
    public partial class ProcessContactView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strCompanyID = Request.QueryString["contactid"].ToString();
            Response.Write(populateContact(strCompanyID));
        }


        //This Method populate the Contact given by ContactID
        protected String populateContact(String strContactID)
        {

            String strContact = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " SELECT * FROM dbo.Contacts WHERE ContactID=" + strContactID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            String PostalAddressBuilder = sdr["STREET_AddressLine1"].ToString() + sdr["STREET_City"] + sdr["STREET_Region"] + sdr["STREET_PostalCode"] + sdr["STREET_Country"];
                            String PhysicalAddressBuilder =sdr["POSTAL_AddressLine1"].ToString() + sdr["POSTAL_City"] + sdr["POSTAL_Region"] + sdr["POSTAL_PostalCode"]+ sdr["POSTAL_Country"] ;
                            String TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                            String MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                            String faxBuilder =sdr["FAX_AreaCode"].ToString() + sdr["FAX_CountryCode"] + sdr["FAX_Number"];
                            strContact = strContact + sdr["ContactName"]+"|" + sdr["FirstName"] + "|" + sdr["LastName"] + "|" + TeleBuilder + "|" + MobileBuilder + "|" + faxBuilder + "|" + sdr["Email"] + "|" + PostalAddressBuilder + "|" + PhysicalAddressBuilder;
                        }
                    }
                    
                    conn.Close();
                }
            }

            return strContact;

        }
        //End Method populateContact

    }
}