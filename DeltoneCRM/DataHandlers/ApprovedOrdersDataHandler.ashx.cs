using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using DeltoneCRM.Classes;
using DeltoneCRM_DAL;
using System.IO;

namespace DeltoneCRM.DataHandlers
{
    /// <summary>
    /// Summary description for ApprovedOrdersDataHandler
    /// </summary>
    public class ApprovedOrdersDataHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int displayLength = int.Parse(context.Request["iDisplayLength"]);
            int displayStart = int.Parse(context.Request["iDisplayStart"]);
            int sortCol = int.Parse(context.Request["iSortCol_0"]);
            string sortDir = context.Request["sSortDir_0"];
            string search = context.Request["sSearch"];
            String querylevel = String.Empty;
            querylevel = context.Session["USERPROFILE"].ToString();
            String loggeduser = String.Empty;
            loggeduser = context.Session["LoggedUserID"].ToString();

            String cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            List<ApprovedOrders> ApprovedOrdersList = new List<ApprovedOrders>();
            int filteredCount = 0;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd;
                if (querylevel == "ADMIN")
                {
                    cmd = new SqlCommand("getAllApprovedOrders", conn);
                }
                else
                {
                    cmd = new SqlCommand("getAllApprovedOrdersStandardUsers", conn);
                }

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDisplayLength = new SqlParameter()
                {
                    ParameterName = "@DisplayLength",
                    Value = displayLength
                };
                cmd.Parameters.Add(paramDisplayLength);

                SqlParameter paramDisplayStart = new SqlParameter()
                {
                    ParameterName = "@DisplayStart",
                    Value = displayStart
                };
                cmd.Parameters.Add(paramDisplayStart);

                SqlParameter paramSortCol = new SqlParameter()
                {
                    ParameterName = "@SortCol",
                    Value = sortCol
                };
                cmd.Parameters.Add(paramSortCol);

                SqlParameter paramSortDir = new SqlParameter()
                {
                    ParameterName = "@SortDir",
                    Value = sortDir
                };
                cmd.Parameters.Add(paramSortDir);

                SqlParameter paramSearchString = new SqlParameter()
                {
                    ParameterName = "@Search",
                    Value = search
                };
                cmd.Parameters.Add(paramSearchString);


                SqlParameter paramLoggedUser = new SqlParameter()
                {
                    ParameterName = "@LoggedUser",
                    Value = loggeduser
                };
                cmd.Parameters.Add(paramLoggedUser);


                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    String finalSuppNotes = String.Empty;
                    ApprovedOrders item = new ApprovedOrders();
                    item.OrderNo = sdr["OrderID"].ToString();
                    filteredCount = Convert.ToInt32(sdr["TotalCount"]);
                    item.CompanyName = sdr["CompanyName"].ToString();
                    item.ContactPerson = sdr["FullName"].ToString();

                    var contactNameOrder = GetContactOrderName(item.OrderNo);
                    if (!string.IsNullOrEmpty(contactNameOrder))
                        item.ContactPerson = contactNameOrder;

                    item.InvNumber = sdr["XeroInvoiceNumber"].ToString();
                    item.OrderTotal = float.Parse(sdr["Total"].ToString());
                    item.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString()).ToShortDateString();

                    var orderedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString());


                    item.OrderedDate = Convert.ToDateTime(sdr["OrderedDate"].ToString()).ToShortDateString();
                    item.DueDate = Convert.ToDateTime(sdr["DDate"].ToString()).ToShortDateString();
                    item.CanFlag = "0";
                    var canCAllFlag = CheckInvoiceDate(item.OrderNo, orderedDate, context);
                    if (canCAllFlag)
                        item.CanFlag = "1";
                    item.CreatedBy = sdr["OrderedBy"].ToString();
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
                conn.Close();

            }

            var result = new
            {
                iTotalRecords = getItemsTotalCount(querylevel, loggeduser),
                iTotalDisplayRecords = filteredCount,
                aaData = ApprovedOrdersList
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(result));
        }

        private string GetContactOrderName(string orderId)
        {
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            OrderDAL orderdal = new OrderDAL(connString);
          return  orderdal.GetOrderContactPersonName(Convert.ToInt32(orderId));
        }

        private bool CheckInvoiceDate(string orderId, DateTime orderCreatedDate, HttpContext context)
        {
            var currentDate = DateTime.Now;

            var InvoiceDate = CalculateBusinessDaysFromInputDate(orderCreatedDate, 3);
            var orderCreateDay = orderCreatedDate.DayOfWeek;
            if (currentDate.Date > InvoiceDate.Date)
            {
                var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                var suppNameWithItems = new PurchaseDAL(connString).getPurchasedSupplierByOrderId(Convert.ToInt32(orderId));
                var strPath = (context.Server.MapPath("~/Uploads/" + orderId));
                if (Directory.Exists(strPath))
                {
                    var allFiles = Directory.GetFiles(strPath).ToList();
                    if (allFiles.Count() == suppNameWithItems.Count())
                        return false;
                    else
                        return true;

                }
                return true;
            }



            return false;
        }

        public System.DateTime CalculateBusinessDaysFromInputDate
  (System.DateTime StartDate, int NumberOfBusinessDays)
        {
            //Knock the start date down one day if it is on a weekend.
            if (StartDate.DayOfWeek == DayOfWeek.Saturday |
                StartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                NumberOfBusinessDays -= 1;
            }

            int index = 0;

            for (index = 1; index <= NumberOfBusinessDays; index++)
            {
                switch (StartDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        StartDate = StartDate.AddDays(2);
                        break;
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        StartDate = StartDate.AddDays(1);
                        break;
                    case DayOfWeek.Saturday:
                        StartDate = StartDate.AddDays(3);
                        break;
                }
            }

            //check to see if the end date is on a weekend.
            //If so move it ahead to Monday.
            //You could also bump it back to the Friday before if you desired to. 
            //Just change the code to -2 and -1.
            if (StartDate.DayOfWeek == DayOfWeek.Saturday)
            {
                StartDate = StartDate.AddDays(2);
            }
            else if (StartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                StartDate = StartDate.AddDays(1);
            }

            return StartDate;
        }


        private int getItemsTotalCount(String QLevel, String LoggedUser)
        {
            int TotalitemCount = 0;
            String cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd;
                if (QLevel == "ADMIN")
                {
                    cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Orders WHERE Status = 'APPROVED'", conn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Orders WHERE Status = 'APPROVED' AND AccountOwnerID = " + LoggedUser, conn);
                }

                conn.Open();

                TotalitemCount = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            return TotalitemCount;
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