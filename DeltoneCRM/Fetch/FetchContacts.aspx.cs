using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM.Fetch
{
    public partial class FetchContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Return  all the Contact according to the Company ID

            String strCompanyID = Request.QueryString["cid"].ToString();
            Response.Write(ReturnContacts(strCompanyID));
            

        }

        protected string ReturnContacts(String strCompanyID)
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT * FROM dbo.Contacts WHERE CompanyID=" + strCompanyID;

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["Active"] != DBNull.Value)
                            {
                                if (sdr["Active"].ToString().Trim() == "Y")
                                {
                                    String strContactName = sdr["FirstName"] + " " + sdr["LastName"];
                                    String strViewContact = "<img src='Images/Edit.png'  onclick='LoadView(" + sdr["ContactID"] + ")'/>";
                                    String strViewContactUp = "<img src='Images/Edit.png'  onclick='UpdateContact(" + sdr["ContactID"] + ")'/>";
                                    String strCreateOrder = "<img src='Images/Edit.png'  onclick='CreateOrder(" + sdr["ContactID"] + ")'/>";
                                    String strLoadOrders = "<img src='Images/Edit.png'  onclick='LoadOrders(" + strCompanyID + "," + sdr["ContactID"] + ")'/>";
                                    String strCreateQuote = "<img src='Images/Edit.png' onclick='CreateQuote(" + sdr["ContactID"] + ")'/>";
                                    String dummyorderLink = "<img src='Images/Edit.png'  onclick='CreateOrderDummy(" + sdr["ContactID"] + "," + strCompanyID + ")'/>";
                                    String addressBuilder = sdr["STREET_AddressLine1"] + " " + sdr["STREET_City"] + " " + sdr["STREET_Region"] + " " + sdr["STREET_PostalCode"] + " " + sdr["STREET_Country"];
                                    String PhoneBuilder = sdr["DEFAULT_AreaCode"] + "" + sdr["DEFAULT_CountryCode"] + "" + sdr["DEFAULT_Number"];

                                    //Modification Done here
                                    strOutput = strOutput + "[\"" + sdr["ContactID"] + "\"," + "\"" + strContactName + "\","
                                        + "\"" + addressBuilder + "\"," + "\"" + PhoneBuilder + "\"," + "\"" + strViewContact + "\","
                                        + "\"" + strCreateOrder + "\","
                                        + "\"" + strCreateQuote + "\","
                                           + "\"" + strViewContactUp + "\","
                                           + "\"" + dummyorderLink + "\"],";
                                }
                            }
                          
                            
                        }
                        int Length=strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length-1));

                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }
                
            }
            return strOutput;
        }
    }
}