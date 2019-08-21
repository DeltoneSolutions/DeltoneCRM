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
    public partial class FetchOrdersForApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Write(ReturnOrdersApproval());
        }

        protected String ReturnOrdersApproval()
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT OD.OrderID, OD.CompanyID, OD.ContactID, OD.XeroInvoiceNumber, OD.OrderedDateTime, CP.CompanyName, CT.FirstName + ', ' +  CT.LastName AS FullName, OD.CreatedBy, OD.Status FROM dbo.Orders OD, dbo.Companies CP, dbo.Contacts CT WHERE OD.CompanyID = CP.CompanyID AND OD.ContactID = CT.ContactID AND OD.Status = 'PENDING'";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                //EditOrder(OrderID, CompanyID, ContactID)
                                //String strEditOrder = "<img src='Images/Edit.png'  onclick='EditOrder(" + sdr["OrderID"] + "," + CompanyID + "," + ContactID + ")'/>";
                                String strApproveOrder = "<img src='Images/Edit.png'  onclick='ApproveOrder(" + sdr["OrderID"] + "," + sdr["CompanyID"] + "," + sdr["ContactID"] + ")'/>";

                                strOutput = strOutput + "[\"" + sdr["XeroInvoiceNumber"] + "\"," + "\"" + sdr["OrderedDateTime"] + "\"," + "\"" + sdr["CompanyName"] + "\"," + "\"" + sdr["FullName"] + "\"," + "\"" + sdr["CreatedBy"] + "\"," + "\"" + sdr["Status"] + "\"," + "\"" + strApproveOrder + "\"],";

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