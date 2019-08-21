using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM
{
    public partial class CreateQuoteStep1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_CreateQuote_Click(object sender, EventArgs e)
        {
            String CompanyName = String.Empty;
            String ContactFirstName = String.Empty;
            String ContactLastName = String.Empty;
            String EmailAddress = String.Empty;
            String PhoneNumber = String.Empty;
            String ExistingCustomer = String.Empty;

            String LatestCompanyID = String.Empty;
            String LatestContactID = String.Empty;

            String ExisitingCompanyID = String.Empty;
            String WhichDB = String.Empty;


            CompanyName = txt_CompanyName.Text;
            ContactFirstName = txt_ContactFirstName.Text;
            ContactLastName = txt_ContactLastName.Text;
            EmailAddress = txt_EmailAddress.Text;
            PhoneNumber = txt_PhoneNumber.Text;

 
                ExistingCustomer = hdn_CustAlreadyExists.Value;
                WhichDB = hdn_QueryDB.Value;

                ExisitingCompanyID = hdn_ExistingCompanyID.Value;

            
           

            if (ExisitingCompanyID == "")
            {
                LatestCompanyID = CreateQuoteCustomer(CompanyName);
                LatestContactID = CreateQuoteContact(LatestCompanyID, ContactFirstName, ContactLastName, EmailAddress, PhoneNumber);
            }
            else
            {
                LatestCompanyID = ExisitingCompanyID;
                String theContactID = String.Empty;
                theContactID = FindCustomerDetails(ExisitingCompanyID, ContactFirstName, ContactLastName, WhichDB);

                if (theContactID == "NOONE")
                {
                    LatestContactID = CreateQuoteContact(LatestCompanyID, ContactFirstName, ContactLastName, EmailAddress, PhoneNumber);
                }
                else
                {
                    LatestContactID = theContactID;
                }
            }
            

            if (ExistingCustomer == "YES")
            {
                Response.Redirect("quote.aspx?cid=" + LatestContactID + "&CompID=" + LatestCompanyID + "&FLAG=Y&Exists=Y&DB=" + WhichDB);
            }
            else
            {
                Response.Redirect("quote.aspx?cid=" + LatestContactID + "&CompID=" + LatestCompanyID + "&FLAG=Y&Exists=N&DB=" + WhichDB);
            }

            
           
        }

        protected String FindCustomerDetails(String CompanyID, String FirstName, String LastName, String WhichDB)
        {
            String responseString = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                if (WhichDB == "QuoteDB" || WhichDB == "")
                {
                    cmd.CommandText = "SELECT ContactID FROM dbo.Quote_Contacts WHERE CompanyID = " + CompanyID + " AND FirstName = '" + FirstName + "' AND LastName = '" + LastName + "'";
                }
                else
                {
                    cmd.CommandText = "SELECT ContactID FROM dbo.Contacts WHERE CompanyID = " + CompanyID + " AND FirstName = '" + FirstName + "' AND LastName = '" + LastName + "'";
                }
                
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            responseString = sdr["ContactID"].ToString();
                        }
                    }
                    else
                    {
                        responseString = "NOONE";
                    }
                }
                conn.Close();
            }

            return responseString;
        }

        protected String CreateQuoteCustomer(String CompanyName)
        {
            int RowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlStmt = "INSERT INTO dbo.Quote_Companies (CompanyName, CreatedBy, CreatedDateTime) VALUES (@CompanyName, @CreatedBy, CURRENT_TIMESTAMP)";
            SqlCommand cmd = new SqlCommand(sqlStmt, conn);
            cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
            cmd.Parameters.AddWithValue("@CreatedBy", Session["LoggedUser"]);

            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
            }

            String sqlSelectStmt = "SELECT TOP 1 CompanyID FROM dbo.Quote_Companies WHERE CreatedBy = '" + Session["LoggedUser"].ToString() + "' ORDER BY CreatedDateTime DESC";
            String LatestCompanyID = String.Empty;
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = sqlSelectStmt;
                cmd2.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LatestCompanyID = sdr["CompanyID"].ToString();
                    }
                    
                }
            }
            conn.Close();

            return LatestCompanyID;
        }

        protected String CreateQuoteContact(String CompanyID, String FirstName, String LastName, String EmailAddress, String PhoneNumber)
        {
            int RowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var STREET_AddressLine1 = NewShipLine1.Text;
            var STREET_City = NewShipCity.Text;
            var STREET_Country = "Australia";
            var STREET_PostalCode = NewShipPostcode.Text;
            var STREETL_Region = NewShipState.SelectedValue;

            String sqlStmt = @"INSERT INTO dbo.Quote_Contacts (CompanyID, FirstName, LastName, DEFAULT_Number, Email, 
            CreatedBy, CreatedDateTime,STREET_AddressLine1,STREET_City,STREET_Country,STREET_PostalCode,STREET_Region) 
           VALUES (@CompanyID, @FirstName, @LastName, @DEFAULT_Number, @Email, @CreatedBy, CURRENT_TIMESTAMP,
           @STREET_AddressLine1,@STREET_City,@STREET_Country,@STREET_PostalCode,@POSTAL_Region)";

            SqlCommand cmd = new SqlCommand(sqlStmt, conn);
            cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DEFAULT_Number", PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", EmailAddress);
            cmd.Parameters.AddWithValue("@CreatedBy", Session["LoggedUser"]);
            cmd.Parameters.AddWithValue("@STREET_AddressLine1", STREET_AddressLine1);
            cmd.Parameters.AddWithValue("@STREET_City", STREET_City);
            cmd.Parameters.AddWithValue("@STREET_Country", STREET_Country);
            cmd.Parameters.AddWithValue("@STREET_PostalCode", STREET_PostalCode);
            cmd.Parameters.AddWithValue("@POSTAL_Region", STREETL_Region);

            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
            }

            String sqlSelectStmt = "SELECT TOP 1 ContactID FROM dbo.Quote_Contacts WHERE CreatedBy = '" + Session["LoggedUser"].ToString() + "' AND CompanyID = " + CompanyID + " ORDER BY CreatedDateTime DESC";
            String LatestCompanyID = String.Empty;
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = sqlSelectStmt;
                cmd2.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LatestCompanyID = sdr["ContactID"].ToString();
                    }
                    
                }
            }
            conn.Close();

            return LatestCompanyID;
        }
    }
}