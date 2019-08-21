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
    public partial class FetchAllOrdersForCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strCompanyID = Request.QueryString["companyid"].ToString();
            ReturnContacts(strCompanyID);
        }

        protected void ReturnContacts(String strCompanyID)
        {
            String strOutput = "{\"aaData\":[";
            String CommissionValue = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT OD.OrderID,OD.Completed, OD.ContactID, 
                        OD.OrderedDateTime, CT.FirstName, CT.LastName , CT.DEFAULT_AreaCode, CT.DEFAULT_CountryCode, 
                    CT.DEFAULT_Number, OD.Total, OD.Subtotal, OD.COGTotal, OD.Status, OD.CreatedBy, OD.XeroInvoiceNumber,OrderContactName 
                                 FROM dbo.Orders OD, dbo.Companies CP, dbo.Contacts CT WHERE OD.CompanyID = " + strCompanyID + " AND OD.CompanyID = CP.CompanyID AND OD.ContactID = CT.ContactID";

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                String strContactName = sdr["FirstName"] + " " + sdr["LastName"];

                                if (sdr["OrderContactName"] != DBNull.Value)
                                    strContactName = sdr["OrderContactName"].ToString();

                                String strViewContact = "<img src='Images/Edit.png'  onclick='EditOrder(" + sdr["OrderID"] + "," + strCompanyID + "," + sdr["ContactID"] + ")'/>";
                                String PhoneBuilder = sdr["DEFAULT_AreaCode"] + "" + sdr["DEFAULT_CountryCode"] + "" + sdr["DEFAULT_Number"];
                                String OrderTotal = sdr["Subtotal"].ToString();
                                String COGTotal = sdr["COGTotal"].ToString();

                                using (SqlConnection conn2 = new SqlConnection())
                                {
                                    conn2.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                                    using (SqlCommand cmd2 = new SqlCommand())
                                    {

                                        cmd2.CommandText = "SELECT value FROM dbo.Commision WHERE OrderID = " + sdr["OrderID"].ToString();

                                        cmd2.Connection = conn2;
                                        conn2.Open();
                                        using (SqlDataReader sdr2 = cmd2.ExecuteReader())
                                        {
                                            while (sdr2.Read())
                                            {

                                                CommissionValue = sdr2["value"].ToString();
                                            }
                                        }

                                    }
                                }



                                var orderStatus = sdr["Status"].ToString();
                                //if (sdr["Completed"] != DBNull.Value)
                                //    if (sdr["Completed"].ToString() == "Y")
                                //        orderStatus = "COMPLETED";




                                //Decimal Commission = Math.Round((((Convert.ToDecimal(OrderTotal) - Convert.ToDecimal(COGTotal)) * 40) / 100), 2);
                                String Commission = String.Format("{0:C2}", CommissionValue);
                                Decimal DisplayOrderTotal = Math.Round(Convert.ToDecimal(OrderTotal), 2);

                                //Modification Done here
                                strOutput = strOutput + "[\"" + sdr["OrderID"] + "\"," + "\"" + sdr["XeroInvoiceNumber"] + "\"," + "\"" 
                                    + sdr["OrderedDateTime"] + "\"," + "\"" + strContactName + "\"," + "\"" + PhoneBuilder + "\"," + "\"" 
                                    + "$" + DisplayOrderTotal + "\"," + "\"" + "$" + Commission + "\"," + "\""
                                    + orderStatus + "\"," + "\"" 
                                    + sdr["CreatedBy"] + "\"," + "\"" + strViewContact + "\"],";


                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }

                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }

            }
            Response.Write(strOutput);
        }
    }
}