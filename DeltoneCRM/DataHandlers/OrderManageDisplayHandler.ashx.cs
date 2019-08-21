using DeltoneCRM.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DeltoneCRM.DataHandlers
{
    /// <summary>
    /// Summary description for OrderManageDisplayHandler
    /// </summary>
    public class OrderManageDisplayHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {


            var status = "3";
            if (context.Request["s"] != null)
            {
                status = context.Request["s"];
                if (status == "")
                    status = "3";
                context.Response.Write(ReturnOrders(status, context));

            }
            else
                context.Response.Write(ReturnOrders(status, context));
        }

        protected String ReturnOrders(string param, HttpContext context)
        {
            List<ApprovedOrders> ApprovedOrdersList = new List<ApprovedOrders>();
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (param == "5")
                    {
                        //Modification done here 
                        if (context.Session["USERPROFILE"].ToString() == "ADMIN")
                        {
                            cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                   CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, OD.SupplierNotes 
                      FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                AND OD.CompanyID = CP.CompanyID AND  OD.Status='BO' ORDER BY OD.OrderID DESC";
                        }
                        else
                        {
                            cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                            CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, OD.SupplierNotes 
                   FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                       AND OD.CompanyID = CP.CompanyID AND  OD.Status='BO' AND  CP.OwnershipAdminID = " + context.Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                        }
                    }
                    else
                        if (param == "6")
                        {
                            //Modification done here 
                            if (context.Session["USERPROFILE"].ToString() == "ADMIN")
                            {
                                cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                 CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, OD.SupplierNotes 
                   FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                 AND OD.CompanyID = CP.CompanyID AND  OD.Status='EOM' ORDER BY OD.OrderID DESC";
                            }
                            else
                            {
                                cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, OrderContactName,
                       CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, 
                         OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                         AND OD.CompanyID = CP.CompanyID AND  OD.Status='EOM' AND  CP.OwnershipAdminID = " + context.Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                            }
                        }
                        else
                            if (param == "7")
                            {
                                //Modification done here 
                                if (context.Session["USERPROFILE"].ToString() == "ADMIN")
                                {
                                    cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                              CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, 
                     OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                         AND OD.CompanyID = CP.CompanyID AND  ( OD.Status='INHOUSE' OR OD.Status='TW-INHOUSE') ORDER BY OD.OrderID DESC";
                                }
                                else
                                {
                                    cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                                CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate,
                        OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                            AND OD.CompanyID = CP.CompanyID AND  ( OD.Status='INHOUSE' OR OD.Status='TW-INHOUSE') AND  CP.OwnershipAdminID = " + context.Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                                }
                            }
                            else 
                                if (param == "8")
                            {
                                //Modification done here 
                                if (context.Session["USERPROFILE"].ToString() == "ADMIN")
                                {
                                    cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                              CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, 
                     OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                         AND OD.CompanyID = CP.CompanyID AND  OD.Status='TW-INHOUSE' ORDER BY OD.OrderID DESC";
                                }
                                else
                                {
                                    cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                                CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate,
                        OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                            AND OD.CompanyID = CP.CompanyID AND  OD.Status='TW-INHOUSE' AND  CP.OwnershipAdminID = " + context.Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                                }
                            }


                            else
                                if (param == "3")
                                {
                                    //Modification done here 
                                    if (context.Session["USERPROFILE"].ToString() == "ADMIN")
                                    {
                                        cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, OrderContactName,
                           CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, 
                      OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                     AND OD.CompanyID = CP.CompanyID AND  OD.Status='PENDING' ORDER BY OD.OrderID DESC";
                                    }
                                    else
                                    {
                                        cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, CT.ContactID, OrderContactName,
                            CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, OD.SupplierNotes 
                         FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                     AND OD.CompanyID = CP.CompanyID AND  OD.Status='PENDING' AND  CP.OwnershipAdminID = " + context.Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                                    }
                                }
                                else
                                {
                                    if (param == "1")
                                    {
                                        if (context.Session["USERPROFILE"].ToString() == "ADMIN")
                                        {
                                            cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, OrderContactName,
                               CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, 
                           OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                        AND OD.CompanyID = CP.CompanyID AND  OD.Status='COMPLETED' ORDER BY OD.OrderID DESC";
                                        }
                                        else
                                        {
                                            cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, OrderContactName,
                          CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.CreatedDateTime, OD.DueDate,
                         OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID 
                                AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID 
                        AND  OD.Status='COMPLETED' AND  CP.OwnershipAdminID = " + context.Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                                        }
                                    }
                                    else
                                    {
                                        if (param == "2")
                                        {
                                            if (context.Session["USERPROFILE"].ToString() == "ADMIN")
                                            {
                                                cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID, OD.Completed,OrderContactName,
                               CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate, 
                     OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
             AND OD.CompanyID = CP.CompanyID  AND OD.Status='APPROVED'  ORDER BY OD.OrderID DESC";
                                            }
                                            else if (context.Session["USERPROFILE"].ToString() == "STANDARD")
                                            {
                                                cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID,OD.Completed, OrderContactName,
                               CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime, OD.CreatedDateTime,OD.DueDate, 
                     OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID 
                                AND OD.CompanyID = CP.CompanyID AND OD.Status='APPROVED'  AND  CP.OwnershipAdminID = " + context.Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                                            }
                                        }
                                        else
                                        {
                                            if (param == "4")
                                            {
                                                if (context.Session["USERPROFILE"].ToString() == "ADMIN")
                                                {
                                                    cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID, OrderContactName,
                            CT.ContactID, CT.FirstName, CT.LastName, OD.CreatedBy,OD.Status, OD.Total, 
                           OD.OrderedDateTime, OD.CreatedDateTime,OD.DueDate ,OD.Completed, OD.SupplierNotes FROM dbo.Orders OD, 
                          dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID 
                           ORDER BY OD.OrderID DESC";
                                                }
                                                else
                                                {
                                                    cmd.CommandText = @"SELECT OD.OrderID,OD.XeroInvoiceNumber, CP.CompanyName, CP.CompanyID, CT.ContactID, CT.FirstName,OrderContactName,
                                              CT.LastName, OD.CreatedBy,OD.Status, OD.Total, OD.OrderedDateTime,OD.CreatedDateTime, OD.DueDate ,OD.Completed, 
                                     OD.SupplierNotes FROM dbo.Orders OD, dbo.Contacts CT, dbo.Companies CP WHERE OD.ContactID = CT.ContactID AND 
                                OD.CompanyID = CT.CompanyID AND OD.CompanyID = CP.CompanyID  AND  CP.OwnershipAdminID = " + context.Session["LoggedUserID"] + " ORDER BY OD.OrderID DESC";
                                                }
                                            }
                                        }
                                    }
                                }
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {


                                String finalSuppNotes = String.Empty;
                                ApprovedOrders item = new ApprovedOrders();
                                item.OrderNo = sdr["OrderID"].ToString();
                                item.CompanyName = sdr["CompanyName"].ToString();
                                item.ContactPerson = sdr["FirstName"].ToString();

                                if (sdr["OrderContactName"] != DBNull.Value)
                                    item.ContactPerson = sdr["OrderContactName"].ToString();

                                item.InvNumber = sdr["XeroInvoiceNumber"].ToString();
                                item.OrderTotal = float.Parse(sdr["Total"].ToString());
                                item.CreatedDate = Convert.ToDateTime(sdr["CreatedDateTime"].ToString()).ToShortDateString();
                                item.OrderedDate = Convert.ToDateTime(sdr["OrderedDateTime"].ToString()).ToShortDateString();
                                item.DueDate = Convert.ToDateTime(sdr["DueDate"].ToString()).ToShortDateString();
                                item.CreatedBy = sdr["CreatedBy"].ToString();
                                item.Status = sdr["Status"].ToString();
                                if (sdr["SupplierNotes"].ToString() != "")
                                {
                                    if (sdr["SupplierNotes"].ToString().Length > 3)
                                        finalSuppNotes = sdr["SupplierNotes"].ToString().Substring(0, 3);
                                    else
                                        finalSuppNotes = sdr["SupplierNotes"].ToString();
                                }
                                else
                                {
                                    finalSuppNotes = "";
                                }

                                item.SupplierNotes = finalSuppNotes;

                                item.ViewEdit = "<img src='../Images/Edit.png'  onclick='openWin(" + sdr["OrderID"].ToString() + "," + sdr["ContactID"] + "," + sdr["CompanyID"] + ");'>";
                                ApprovedOrdersList.Add(item);

                            }

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
            js.MaxJsonLength = 50000000;
            return js.Serialize(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}