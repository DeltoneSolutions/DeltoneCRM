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
    public partial class AddContact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CtrlChanged(Object sender, EventArgs e)
        {
            if (chk.Checked == true)
            {
                pnlShippingAddres.Enabled = false;
                txtPhysicalStreetAddress.Text = txtPostalStreetAddress.Text;
                txtPhysicalTownCity.Text = txtPostalTownCity.Text;
                ddPhysicalState.SelectedValue = ddPostalState.SelectedValue;
                txtPhysicalPostalZipCode.Text = txtPostalZipCode.Text;
                txtPhysicalCountry.Text = txtPostalCountry.Text;    
            }
            if (chk.Checked == false)
            {
                pnlShippingAddres.Enabled = true;
                txtPhysicalStreetAddress.Text = String.Empty;
                txtPhysicalTownCity.Text = String.Empty;
                ddPhysicalState.SelectedValue = "select";
                txtPhysicalPostalZipCode.Text = String.Empty;
                txtPhysicalCountry.Text = String.Empty;
            }
        }

        protected void lbSameAsAbove_Clicked(object sender, EventArgs e)
        {
            pnlShippingAddres.Enabled = false;
        }

        protected String validateFields(String strContact, string strEmail)
        {
            RegexUtilities util = new RegexUtilities();
            string strError = String.Empty;
            if (strContact == String.Empty)
            {
                strError = strError + "Contact canno't be empty<br/>";
            }
            if (!(strEmail == String.Empty))
            {
                if (!util.IsValidEmail(strEmail))
                {
                    strError = strError + "Invalid Email Address";
                }
            }
            return strError;
        }

       
        protected void btnContactSubmit_Click(object sender, EventArgs e)
        {
            

            //int CreatedCompanyID = Int16.Parse(Request.QueryString["CreatedCompany"]);
            int CreatedCompanyID = 17472;
            String strPrimaryContact = "";
            if (chkPrimary.Checked == true)
            {
                strPrimaryContact = "YES";
            }
            else
            {
                strPrimaryContact = "NO";
            }

            String Sql_Query = "insert into dbo.Contacts(CompanyID,ContactName,FirstName,LastName,PrimaryContact,DEFAULT_AreaCode,DEFAULT_CountryCode,DEFAULT_Number,FAX_AreaCode,FAX_CountryCode,FAX_Number,MOBILE_AreaCode,MOBILE_CountryCode,MOBILE_Number,Email,STREET_AddressLine1,STREET_City,STREET_Country,STREET_PostalCode,STREET_Region,POSTAL_AddressLine1,POSTAL_City,POSTAL_Country,POSTAL_PostalCode,POSTAL_Region,Active,CreatedDateTime,CreatedBy,AlteredDateTime,AlteredBy)values(";
            Sql_Query = Sql_Query + "@CompanyID,@ContactName,@FirstName,@LastName,@PrimaryContact,@Def_areacode,@Def_countrycode,@def_number,@fax_areacode,@fax_countrycode,@fax_number,@mobile_areacode,@mobile_countrycode,@mobile_number,@Email,@street_addLined1,@street_city,@street_country,@street_postalcode,@street_region,@postal_addLine1,@postal_city,@postal_country,@postal_postalcode,@postal_region,'Y',current_timestamp,'Taras Selemba',current_timestamp,null)";

            using (SqlConnection connContactInsert = new SqlConnection())
            {
                connContactInsert.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                SqlCommand command = new SqlCommand(Sql_Query, connContactInsert);
                command.Parameters.AddWithValue("@CompanyID", CreatedCompanyID);
                command.Parameters.AddWithValue("@ContactName", txtContactName.Text);

                command.Parameters.AddWithValue("@FirstName", txtFistName.Text);
                command.Parameters.AddWithValue("@LastName", txtLastName.Text);


                command.Parameters.AddWithValue("@PrimaryContact", strPrimaryContact);
                command.Parameters.AddWithValue("@Def_areacode", TextTeleAreaCode.Text);
                command.Parameters.AddWithValue("@Def_countrycode",txtTeleCountryCode.Text);
                command.Parameters.AddWithValue("@def_number", TextTeleNumber.Text);
                

                //Fax Details 
                command.Parameters.AddWithValue("@fax_areacode",txtFaxAreaCode.Text);
                command.Parameters.AddWithValue("@fax_countrycode", txtFaxCountryCode.Text);
                command.Parameters.AddWithValue("@fax_number", txtFaxNumber.Text);
                //Mobile Details 

                command.Parameters.AddWithValue("@mobile_areacode",txtMobileAreaCode.Text);
                command.Parameters.AddWithValue("@mobile_countrycode", txtMobileCountryCode.Text);
                command.Parameters.AddWithValue("@mobile_number", txtMobileNumber.Text);
                command.Parameters.AddWithValue("@Email", txtContactEmail.Text);

                //Postal Address Details 
                
                command.Parameters.AddWithValue("@street_addLined1", txtPostalStreetAddress.Text);
                command.Parameters.AddWithValue("@street_city", txtPostalTownCity.Text);
                command.Parameters.AddWithValue("@street_country", txtPostalCountry.Text);
                command.Parameters.AddWithValue("@street_postalcode", txtPostalZipCode.Text);
                command.Parameters.AddWithValue("@street_region", (ddPostalState.SelectedItem.Value != "select") ? ddPostalState.SelectedItem.Value : String.Empty);

              
                //Physical Address Details 
                command.Parameters.AddWithValue("@postal_addLine1", txtPhysicalStreetAddress.Text);
                command.Parameters.AddWithValue("@postal_city", txtPhysicalTownCity.Text);
                command.Parameters.AddWithValue("@postal_country",txtPostalCountry.Text);
                command.Parameters.AddWithValue("@postal_postalcode", txtPhysicalPostalZipCode.Text);
                command.Parameters.AddWithValue("@postal_region", (ddPhysicalState.SelectedItem.Value != "select") ? ddPhysicalState.SelectedItem.Value : String.Empty);
                
                try
                {
                    connContactInsert.Open();

                    String str = validateFields(txtContactName.Text, txtContactEmail.Text);

                    if (str == String.Empty)
                    {

                        Int32 rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                            spContact.InnerText = txtContactName.Text + "has been Created";
                            string strScript = "<script language='javascript'>$(document).ready(function () {   dialog = $('#dialog-form').dialog({  autoOpen: false, width: 800, modal: true,close: function () {  dialog.dialog('close'); }  });  dialog.dialog('open'); });   </script>";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false); 

                        }
                        else
                        {
                            Response.Write("<b>Contact Couldn't Created</b>");

                        }
                    }
                    else
                    {
                        divErrorDisplay.Visible = true;
                        lblError.Text = str;
                    }
                   
                }
                catch (Exception ex)
                {
                    Response.Write("Coudn't create the Contact");
                }
            }
        }
        protected void btnContactCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DashBoard.aspx");
        }

     
    }
}