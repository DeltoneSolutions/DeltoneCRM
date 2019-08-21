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
    public partial class FetchApprovedCreditNotes : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnCreditNotes());
        }


        protected String ReturnCreditNotes()
        {
            String strOutput = "{\"data\":[";

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        cmd.CommandText = "SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName, CP.CompanyID, CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN, dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.Status='APPROVED'  ORDER BY CN.CreditNote_ID DESC";
                    }
                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                    {
                        cmd.CommandText = "SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName, CP.CompanyID, CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN, dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.Status='APPROVED' AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + "ORDER BY CN.CreditNote_ID DESC";
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
                                InvoiceNum = (sdr["XeroCreditNoteID"] == DBNull.Value) ? "DTS-" + sdr["XeroCreditNoteID"] : sdr["XeroCreditNoteID"].ToString();

                                String strEditOrder = "<img src='../Images/Edit.png'  onclick='openWin(" + sdr["CreditNote_ID"].ToString() + "," + sdr["ContactID"] + "," + sdr["CompanyID"] + ");'>";
                                String Total = sdr["Total"].ToString();
                                Decimal ConvertedTotal = Math.Round(Convert.ToDecimal(Total), 2);
                                String OrderDate = Convert.ToDateTime(sdr["DateCreated"]).ToString("dd-MMM-yyyy");
                                String StrCreditReson = sdr["CreditNoteReason"].ToString();

                                //String DueDate = (!DBNull.Value.Equals(sdr["DueDate"])) ? Convert.ToDateTime(sdr["DueDate"]).ToString("dd-MM-yyyy") : String.Empty;
                                String CreatedBy = sdr["CreatedBy"].ToString();
                                String Link = "<a class='details' href='#'>DETAILS</a>";


                                strOutput = strOutput + "[\"" + Link + "\"," + "\"" + sdr["CreditNote_ID"] + "\"," + "\"" + sdr["CompanyName"] + "\"," + "\"" + sdr["FirstName"] + " " + sdr["LastName"] + "\"," + "\"" + InvoiceNum + "\"," + "\"" + "$" + ConvertedTotal + "\"," + "\"" + OrderDate + "\"," + "\"" + StrCreditReson + "\"," + "\"" + CreatedBy + "\"," + "\"" + sdr["Status"] + "\"," + "\"" + strEditOrder + "\"],";

                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }

                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }


            }

            // SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName, CP.CompanyID, CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN, dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID ORDER BY CN.CreditNote_ID DESC

            return strOutput;
        }



        


    }
}