using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchSearchOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String searchTerm = Request.QueryString["term"].ToString();
            //String searchTerm = "In";
            Response.Write(ReturnOrders(searchTerm));
        }

        protected String ReturnOrders(string searche)
        {
            String strOutput = "[";
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                   CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, OD.SupplierNotes 
                      FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                AND OD.CompanyID = CP.CompanyID    AND SUBSTRING(XeroInvoiceNumber, 5, 15) like " + "'" + searche + "%'";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                            CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, OD.SupplierNotes 
                   FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                       AND OD.CompanyID = CP.CompanyID  AND  CP.OwnershipAdminID = " + Session["LoggedUserID"] + " AND  SUBSTRING(XeroInvoiceNumber, 5, 15) Like " + "'" + searche + "%'";
                    }
                   
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //EditOrder(OrderID, CompanyID, ContactID)
                            String CombinedName = sdr["OrderID"] + " ," + sdr["CompanyID"].ToString() + " ," + sdr["ContactID"].ToString();
                            strOutput = strOutput + "{\"id\": \"" + CombinedName + "\", \"value\": \"" + sdr["CompanyName"] + "\"},";

                        }
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));

                    }
                    strOutput = strOutput + "]";
                    conn.Close();
                }

            }
            return strOutput;
        }
    }
}