using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using DeltoneCRM.Classes;
using System.Web.Script.Serialization;

namespace DeltoneCRM.Fetch
{
    public partial class FetchAllOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var status = "3";
            if (Request["s"] != null)
            {
                status = Request["s"];
                if (status == "")
                    status = "3";
                Response.Write(ReturnOrders(status));

            }
            else
                Response.Write(ReturnOrders(status));
        }

        protected String ReturnOrders(string param)
        {
            String strOutput = "{\"data\":[";
            List<ApprovedOrders> ApprovedOrdersList = new List<ApprovedOrders>();
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (param == "3")
                    {
                        //Modification done here 
                        if (Session["USERPROFILE"].ToString() == "ADMIN")
                        {
                            cmd.CommandText = "SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate, OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID AND  OD.Status='PENDING' ORDER BY OD.OrderID DESC";
                        }
                        else if (Session["USERPROFILE"].ToString() == "STANDARD")
                        {
                            cmd.CommandText = "SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate, OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID AND  OD.Status='PENDING' AND  CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                        }
                    }
                    else
                    {
                        if (param == "1")
                        {
                            if (Session["USERPROFILE"].ToString() == "ADMIN")
                            {
                                cmd.CommandText = "SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate, OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID AND  OD.Completed='Y' ORDER BY OD.OrderID DESC";
                            }
                            else if (Session["USERPROFILE"].ToString() == "STANDARD")
                            {
                                cmd.CommandText = "SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate, OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID AND  OD.Completed='Y' AND  CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                            }
                        }
                        else
                        {
                            if (param == "2")
                            {
                                if (Session["USERPROFILE"].ToString() == "ADMIN")
                                {
                                    cmd.CommandText = "SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID, OD.Completed, CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate, OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID  AND OD.Status='APPROVED'  ORDER BY OD.OrderID DESC";
                                }
                                else if (Session["USERPROFILE"].ToString() == "STANDARD")
                                {
                                    cmd.CommandText = "SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate, OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID AND OD.Status='APPROVED'  AND  CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                                }
                            }
                            else
                            {
                                if (param == "4")
                                {
                                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                                    {
                                        cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID, 
                            CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, 
                           OD.OrderedDateTime, OD.DueDate ,OD.Completed, OD.SupplierNotes FROM dbo.Orders OD, 
                          dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID 
                           ORDER BY OD.OrderID DESC";
                                    }
                                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                                    {
                                        cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID, CT.ContactID, CT.FirstName,
                                              CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.DueDate ,OD.Completed, 
                                     OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND 
                                OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID  AND  CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                                    }
                                }
                            }
                        }
                    }
                    cmd.Connection = conn;
                    conn.Open();
                    var status = "";

                    if (param == "1")
                        status = "COMPLETED";
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                //EditOrder(OrderID, CompanyID, ContactID)
                                //   String InvoiceNum = String.Empty;

                                //   //Modified Here 
                                //   InvoiceNum =  sdr["XeroInvoiceNumber"].ToString();

                                //   String strEditOrder = "<img src='../Images/Edit.png'  onclick='openWin(" + sdr["OrderID"].ToString() + "," + sdr["ContactID"] + "," + sdr["CompanyID"] + ");'>";
                                //   String Total = sdr["Total"].ToString();
                                //   Decimal ConvertedTotal = Math.Round(Convert.ToDecimal(Total), 2);
                                ////   String OrderDate = sdr["OrderedDateTime"].ToString();
                                // //  String DueDate = sdr["DueDate"].ToString();
                                // //  String CreatedBy = sdr["CreatedBy"].ToString();
                                //   String Link = "<a class='details' href='#'>DETAILS</a>";
                                //   String finalSupNotes = String.Empty;
                                //   status = (sdr["Completed"] != DBNull.Value) ? status = "COMPLETED" : sdr["Status"].ToString();
                                //   if (sdr["SupplierNotes"].ToString() != "")
                                //   {
                                //       finalSupNotes = sdr["SupplierNotes"].ToString().Substring(0, 3);
                                //   }
                                //   else
                                //   {
                                //       finalSupNotes = "";
                                //   }



                                //   strOutput = strOutput + "[\"" + Link + "\"," + "\"" + sdr["OrderID"] + "\","
                                //       + "\"" + sdr["CompanyName"] + "\"," + "\"" + sdr["FirstName"] + " "
                                //       + sdr["LastName"] + "\"," + "\"" + sdr["XeroInvoiceNumber"].ToString() + "\"," + "\"" + "$" + ConvertedTotal
                                //       + "\"," + "\"" + sdr["OrderedDateTime"].ToString() + "\"," + "\"" + sdr["DueDate"].ToString() + "\"," + "\"" + sdr["CreatedBy"].ToString()
                                //       + "\"," + "\"" + status + "\"," + "\"" + finalSupNotes + "\"," + "\"" + strEditOrder + "\"],";

                                String finalSuppNotes = String.Empty;
                                ApprovedOrders item = new ApprovedOrders();
                                item.OrderNo = sdr["OrderID"].ToString();
                                item.CompanyName = sdr["CompanyName"].ToString();
                                item.ContactPerson = sdr["FirstName"].ToString();
                                item.InvNumber = sdr["XeroInvoiceNumber"].ToString();
                                item.OrderTotal = float.Parse(sdr["Total"].ToString());
                                item.OrderedDate = sdr["OrderedDateTime"].ToString();
                                item.DueDate = sdr["DueDate"].ToString();
                                item.CreatedBy = sdr["CreatedBy"].ToString();
                                item.Status = sdr["Status"].ToString();
                                if (sdr["SupplierNotes"].ToString() != "")
                                {
                                    finalSuppNotes = sdr["SupplierNotes"].ToString().Substring(0, 3);
                                }
                                else
                                {
                                    finalSuppNotes = "";
                                }

                                item.SupplierNotes = finalSuppNotes;

                                item.ViewEdit = "<img src='../Images/Edit.png'  onclick='openWin(" + sdr["OrderID"].ToString() + "," + sdr["ContactID"] + "," + sdr["CompanyID"] + ");'>";
                                ApprovedOrdersList.Add(item);

                            }
                            //int Length = strOutput.Length;
                            //strOutput = strOutput.Substring(0, (Length - 1));
                        }

                    }
                    // strOutput = strOutput + "]}";
                    conn.Close();
                }





            }
            var result = new
               {
                   iTotalRecords = ApprovedOrdersList.Count(),
                   iTotalDisplayRecords = 25,
                   aaData = ApprovedOrdersList
               };
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 5000000;
            return js.Serialize(result);
        }
    }
}