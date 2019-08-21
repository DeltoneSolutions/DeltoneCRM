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
    public partial class FetchApprovedOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(getApprovedOrders());
        }


        //Method returns all Approved Orders
        protected String getApprovedOrders()
        {
            String strOutput = "{\"data\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Modification done here 
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        cmd.CommandText = "SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID, CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID AND OD.Status='APPROVED' ORDER BY OD.OrderID DESC";
                    }
                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                    {
                        cmd.CommandText = "SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID, CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID AND OD.Status='APPROVED' AND  CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                    }
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                //EditOrder(OrderID, CompanyID, ContactID)
                                String InvoiceNum = String.Empty;

                                //Modified Here 
                                InvoiceNum = (sdr["XeroInvoiceNumber"] == DBNull.Value) ? "DTS-" + sdr["OrderID"] : sdr["XeroInvoiceNumber"].ToString();

                                String strEditOrder = "<img src='../Images/Edit.png'  onclick='openWin(" + sdr["OrderID"].ToString() + "," + sdr["ContactID"] + "," + sdr["CompanyID"] + ");'>";
                                String Total = sdr["Total"].ToString();
                                Decimal ConvertedTotal = Math.Round(Convert.ToDecimal(Total), 2);
                                String OrderDate = Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy");
                                String DueDate = (!DBNull.Value.Equals(sdr["DueDate"])) ? Convert.ToDateTime(sdr["DueDate"]).ToString("dd-MM-yyyy") : String.Empty;
                                String CreatedBy = sdr["CreatedBy"].ToString();
                                String Link = "<a class='details' href='#'>DETAILS</a>";


                                strOutput = strOutput + "[\"" + Link + "\"," + "\"" + sdr["OrderID"] + "\"," + "\"" + sdr["CompanyName"] + "\"," + "\"" + sdr["FirstName"] + " " + sdr["LastName"] + "\"," + "\"" + InvoiceNum + "\"," + "\"" + "$" + ConvertedTotal + "\"," + "\"" + OrderDate + "\"," + "\"" + DueDate + "\"," + "\"" + CreatedBy + "\"," + "\"" + sdr["Status"] + "\"," + "\"" + strEditOrder + "\"],";

                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }

                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }

            }

            return strOutput;

        }



    }
}