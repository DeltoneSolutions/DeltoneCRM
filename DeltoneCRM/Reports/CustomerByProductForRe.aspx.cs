using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Reports
{
    public partial class CustomerByProductForRe : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillRepsList();
                var list = GetALLOrderedOrders("");
                GridViewProduct.DataSource = list;
                GridViewProduct.DataBind();

                var total = list.Sum(x => x.itemQty);
                GridViewProduct.FooterRow.Cells[7].Text = "Total";
                GridViewProduct.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                GridViewProduct.FooterRow.Cells[8].Text = total.ToString();
                GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
                GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
                GridViewProduct.FooterRow.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;


                var bobyTemplate = "I just thought I’d let you know that we are running a deal on the Brother #REPLACEPRODUCTCODE# range." +
    "On an order of 6 toners we are including a free #REPLACEDRUM# drum unit. " +
    "The special is running for the next two weeks so please let me know if you need any. " +
    "Thanks and have a great day. We will be in touch again the next couple of month.";
                subjectMessage.Text = "Printer Cartridges Offer";
                bodycontentTextArea.Text = bobyTemplate;

            }
            else
            {
                //var list = GetALLOrderedOrders("");
                //GridViewProduct.DataSource = list;
                //GridViewProduct.DataBind();
                //var total = list.Sum(x => x.itemQty);
                //GridViewProduct.FooterRow.Cells[6].Text = "Total";
                //GridViewProduct.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                //GridViewProduct.FooterRow.Cells[7].Text = total.ToString();
                //GridViewProduct.FooterRow.Cells[6].BackColor = System.Drawing.Color.Bisque;
                //GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
                //GridViewProduct.FooterRow.Cells[7].ForeColor = System.Drawing.Color.DarkBlue;
            }
        }

        public void fillRepsList()
        {
            if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            {
            }
            else
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    String strLoggedUsers = String.Empty;
                    DataTable subjects = new DataTable();

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    SqlDataAdapter adapter = new SqlDataAdapter("select FirstName + ' ' + LastName AS FullName, LoginID from dbo.Logins WHERE LoginID NOT IN (4, 9, 8, 19,20,21,22) AND Active = 'Y' ", conn);
                    adapter.Fill(subjects);
                    ddlRepList.DataSource = subjects;
                    ddlRepList.DataTextField = "FullName";
                    ddlRepList.DataValueField = "LoginID";
                    ddlRepList.DataBind();
                    ddlRepList.Items.Insert(0, new ListItem("ALL", "9999"));
                }
            }
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            var comListObj = new List<CompanyProduct>();
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                bodycontentTextArea.Text = bodycontentTextArea.Text.Replace("#REPLACEPRODUCTCODE#", searchTextBox.Text);
                comListObj = GetALLOrderedOrders(searchTextBox.Text);
            }
            else
                comListObj = GetALLOrderedOrders("");

            GridViewProduct.DataSource = comListObj;
            GridViewProduct.DataBind();
            var total = comListObj.Sum(x => x.itemQty);
            if (GridViewProduct.FooterRow != null)
            {
                GridViewProduct.FooterRow.Cells[7].Text = "Total";
                GridViewProduct.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                GridViewProduct.FooterRow.Cells[8].Text = total.ToString();
                GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
                GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
                GridViewProduct.FooterRow.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;
            }
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("~/dashboard1.aspx");


        }

        protected void btnbackupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("RepReports.aspx");


        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProduct.PageIndex = e.NewPageIndex;

            var comListObj = new List<CompanyProduct>();
            if (Session["sorList"] != null)
            {
                var sessionLit = Session["sorList"] as List<CompanyProduct>;
                comListObj = sessionLit;
                GridViewProduct.DataSource = sessionLit;
                GridViewProduct.DataBind();
            }
            else
            {
                if (!string.IsNullOrEmpty(searchTextBox.Text))
                {
                    comListObj = GetALLOrderedOrders(searchTextBox.Text);
                }
                else
                    comListObj = GetALLOrderedOrders("");

                GridViewProduct.DataSource = comListObj;
                GridViewProduct.DataBind();
            }





            var total = comListObj.Sum(x => x.itemQty);
            GridViewProduct.FooterRow.Cells[7].Text = "Total";
            GridViewProduct.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[8].Text = total.ToString();
            GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;


        }





        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> GetProductCode(string pre)
        {
            List<string> productCode = new List<string>();
            DataTable dt = new DataTable();
            //String strOutput = "{\"data\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"select it.SupplierItemCode from
	
	                                  Items it
                                  WHERE ( it.SupplierItemCode LIKE '%" + pre + "%' OR  it.Description LIKE '%" + pre + "%') order by SupplierItemCode asc";

                    cmd.Connection = conn;
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    dt.Load(dr);
                    foreach (DataRow row in dt.Rows)
                    {
                        productCode.Add(row["SupplierItemCode"].ToString());
                    }

                    conn.Close();
                }

            }

            return productCode;
        }

        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var stringSearch = "";
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                stringSearch = searchTextBox.Text;
            }
            var comList = GetALLOrderedOrders(stringSearch);
            if (Session["sorList"] == null)
                Session["sorList"] = comList;
            var sessionLit = Session["sorList"] as List<CompanyProduct>;


            string SortDirection = "DESC";
            if (ViewState["SortExpression"] != null)
            {
                if (ViewState["SortExpression"].ToString() == e.SortExpression)
                {
                    ViewState["SortExpression"] = null;
                    SortDirection = "ASC";
                }
                else
                {
                    ViewState["SortExpression"] = e.SortExpression;
                }
            }
            else
            {
                ViewState["SortExpression"] = e.SortExpression;
            }

            var sortDirection = e.SortDirection;
            var sortExpression = e.SortExpression;
            if (Session["sortDire"] == null)
                Session["sortDire"] = "Asc," + sortExpression;

            var splitDir = "";
            if (Session["sortDire"] != null)
                splitDir = Session["sortDire"].ToString().Split(',')[1];
            if (SortDirection == "ASC")
            {
                if (sortExpression == "CompanyName")
                    sessionLit = sessionLit.OrderBy(x => x.CompanyName).ToList();
                else if (sortExpression == "contactName")
                    sessionLit = sessionLit.OrderBy(x => x.contactName).ToList();
                else if (sortExpression == "tlenumber")
                    sessionLit = sessionLit.OrderBy(x => x.tlenumber).ToList();
                else if (sortExpression == "OrderID")
                    sessionLit = sessionLit.OrderBy(x => x.OrderID).ToList();
                else if (sortExpression == "CreatedDateTime")
                    sessionLit = sessionLit.OrderBy(x => x.OrderID).ToList();
                else if (sortExpression == "SupplierItemCode")
                    sessionLit = sessionLit.OrderBy(x => x.SupplierItemCode).ToList();
                else if (sortExpression == "Description")
                    sessionLit = sessionLit.OrderBy(x => x.Description).ToList();
                else if (sortExpression == "itemQty")
                    sessionLit = sessionLit.OrderBy(x => x.itemQty).ToList();
                else if (sortExpression == "AccountOwner")
                    sessionLit = sessionLit.OrderBy(x => x.AccountOwner).ToList();
                else if (sortExpression == "UnitAmount")
                    sessionLit = sessionLit.OrderBy(x => x.UnitAmount).ToList();
                else if (sortExpression == "CreatedBy")
                    sessionLit = sessionLit.OrderBy(x => x.CreatedBy).ToList();


            }
            else
            {
                if (sortExpression == "CompanyName")
                    sessionLit = sessionLit.OrderByDescending(x => x.CompanyName).ToList();
                else if (sortExpression == "contactName")
                    sessionLit = sessionLit.OrderByDescending(x => x.contactName).ToList();
                else if (sortExpression == "tlenumber")
                    sessionLit = sessionLit.OrderByDescending(x => x.tlenumber).ToList();
                else if (sortExpression == "OrderID")
                    sessionLit = sessionLit.OrderByDescending(x => x.OrderID).ToList();
                else if (sortExpression == "CreatedDateTime")
                    sessionLit = sessionLit.OrderByDescending(x => x.OrderID).ToList();
                else if (sortExpression == "SupplierItemCode")
                    sessionLit = sessionLit.OrderByDescending(x => x.SupplierItemCode).ToList();
                else if (sortExpression == "Description")
                    sessionLit = sessionLit.OrderByDescending(x => x.Description).ToList();
                else if (sortExpression == "itemQty")
                    sessionLit = sessionLit.OrderByDescending(x => x.itemQty).ToList();
                else if (sortExpression == "AccountOwner")
                    sessionLit = sessionLit.OrderByDescending(x => x.AccountOwner).ToList();
                else if (sortExpression == "UnitAmount")
                    sessionLit = sessionLit.OrderByDescending(x => x.UnitAmount).ToList();
                else if (sortExpression == "CreatedBy")
                    sessionLit = sessionLit.OrderByDescending(x => x.CreatedBy).ToList();

            }

            Session["sorList"] = sessionLit;

            GridViewProduct.DataSource = sessionLit;
            GridViewProduct.DataBind();

            var total = sessionLit.Sum(x => x.itemQty);
            GridViewProduct.FooterRow.Cells[7].Text = "Total";
            GridViewProduct.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[8].Text = total.ToString();
            GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;

        }

        protected class CompanyProduct
        {
            public string CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string contactName { get; set; }
            public string tlenumber { get; set; }
            public int OrderID { get; set; }
            public string CreatedDateTime { get; set; }
            public string SupplierItemCode { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
            public string UnitAmount { get; set; }
            public string AccountOwner { get; set; }
            public int itemQty { get; set; }
        }

        protected void PageSize_Changed(object sender, EventArgs e)
        {
            GridViewProduct.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            var comListObj = new List<CompanyProduct>();
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                bodycontentTextArea.Text = bodycontentTextArea.Text.Replace("#REPLACEPRODUCTCODE#", searchTextBox.Text);
                comListObj = GetALLOrderedOrders(searchTextBox.Text);
            }
            else
                comListObj = GetALLOrderedOrders("");

            GridViewProduct.DataSource = comListObj;
            GridViewProduct.DataBind();
            var total = comListObj.Sum(x => x.itemQty);
            GridViewProduct.FooterRow.Cells[7].Text = "Total";
            GridViewProduct.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[8].Text = total.ToString();
            GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;
            
            GridViewProduct.DataBind();

        }

        protected List<CompanyProduct> GetALLOrderedOrders(string sear)
        {
            DataTable dt = new DataTable();
            //String strOutput = "{\"data\":[";
            var accoutOwnerID = Session["LoggedUserID"].ToString();


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"select co.CompanyID , co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.CreatedDateTime,ord.OrderID,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,lc.FirstName + ' ' + lc.LastName AS AccountOwner
                                          from Ordered_Items OT INNER JOIN
	                          
	                                  Items IT ON IT.ItemID = OT.ItemCode 
	                                   INNER JOIN  Orders ord ON ord.OrderID = OT.OrderID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompanyID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
inner join Logins lc on lc.LoginID=co.OwnershipAdminID 
                                  WHERE ( it.SupplierItemCode LIKE '%" + sear + "%' OR  it.Description LIKE '%" + sear + "%') order by CompanyName asc";

                    if (accoutOwnerID != "9999")
                    {
                        cmd.CommandText = @"select co.CompanyID , co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.CreatedDateTime,ord.OrderID,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,lc.FirstName + ' ' + lc.LastName AS AccountOwner
                                          from Ordered_Items OT INNER JOIN
	                          
	                                  Items IT ON IT.ItemID = OT.ItemCode 
	                                   INNER JOIN  Orders ord ON ord.OrderID = OT.OrderID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompanyID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
                                   inner join Logins lc on lc.LoginID=co.OwnershipAdminID 
                                  WHERE  co.OwnershipAdminID= " + accoutOwnerID + " and ( it.SupplierItemCode LIKE '%" + sear + "%' OR  it.Description LIKE '%" + sear + "%')   order by CompanyName asc";
                    }


                    if (!string.IsNullOrEmpty(StartDateSTxt.Value) && !string.IsNullOrEmpty(EndDateSTxt.Value))
                    {
                        if (accoutOwnerID != "9999")
                        {
                            var comString = @"select co.CompanyID , co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.CreatedDateTime,ord.OrderID,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,lc.FirstName + ' ' + lc.LastName AS AccountOwner
                                          from Ordered_Items OT INNER JOIN
	                          
	                                  Items IT ON IT.ItemID = OT.ItemCode 
	                                   INNER JOIN  Orders ord ON ord.OrderID = OT.OrderID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompanyID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
 inner join Logins lc on lc.LoginID=co.OwnershipAdminID 
                                  WHERE  co.OwnershipAdminID=@accID and ( it.SupplierItemCode LIKE '%" + sear + "%' OR  it.Description LIKE '%" + sear + "%' ) and (ord.CreatedDateTime >=@startDa and ord.CreatedDateTime <=@endtDa)   order by CompanyName asc";
                            cmd.CommandText = comString;
                            var st = Convert.ToDateTime(StartDateSTxt.Value).ToString("yyyy-MM-dd");
                            var end = Convert.ToDateTime(EndDateSTxt.Value).ToString("yyyy-MM-dd");
                            cmd.Parameters.AddWithValue("@accID", accoutOwnerID);
                            cmd.Parameters.AddWithValue("@startDa", st);
                            cmd.Parameters.AddWithValue("@endtDa", end);
                        }
                        else
                        {
                            var comString = @"select co.CompanyID , co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.CreatedDateTime,ord.OrderID,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,lc.FirstName + ' ' + lc.LastName AS AccountOwner
                                          from Ordered_Items OT INNER JOIN
	                          
	                                  Items IT ON IT.ItemID = OT.ItemCode 
	                                   INNER JOIN  Orders ord ON ord.OrderID = OT.OrderID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompanyID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
inner join Logins lc on lc.LoginID=co.OwnershipAdminID 
                                  WHERE ( it.SupplierItemCode LIKE '%" + sear + "%' OR  it.Description LIKE '%" + sear + "%' ) and (ord.CreatedDateTime >=@startDa and ord.CreatedDateTime <=@endtDa) order by CompanyName asc";
                            cmd.CommandText = comString;
                            var st = Convert.ToDateTime(StartDateSTxt.Value).ToString("yyyy-MM-dd");
                            var end = Convert.ToDateTime(EndDateSTxt.Value).ToString("yyyy-MM-dd");
                            cmd.Parameters.AddWithValue("@startDa", st);
                            cmd.Parameters.AddWithValue("@endtDa", end);
                        }



                    }

                    cmd.Connection = conn;
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    dt.Load(dr);

                    //if (dt.Rows.Count > 0)
                    //{
                    //    foreach (DataRow drow in dt.Rows)
                    //    {

                    //        strOutput = strOutput + "[\""
                    //           + drow["CompanyName"].ToString() + "\","
                    //            + "\"" + (drow["contactName"].ToString()) + "\","
                    //             + "\"" + (drow["tlenumber"].ToString()) + "\","
                    //             + "\"" + (drow["OrderID"].ToString()) + "\","
                    //              + "\"" + (drow["OrderedDateTime"].ToString()) + "\","
                    //               + "\"" + (drow["SupplierItemCode"].ToString()) + "\","
                    //                 + "\"" + (drow["Description"].ToString()) + "\","
                    //                     + "\"" + drow["itemQty"].ToString() + "\"],";
                    //    }
                    //    int Length = strOutput.Length;
                    //    strOutput = strOutput.Substring(0, (Length - 1));
                    //}



                    //using (SqlDataReader sdr = cmd.ExecuteReader())
                    //{
                    //    if (sdr.HasRows)
                    //    {

                    //        while (sdr.Read())
                    //        {
                    //            strOutput = strOutput + "[\""
                    //                + sdr["CompanyName"].ToString() + "\","
                    //                 + "\"" + (sdr["contactName"].ToString()) + "\","
                    //                  + "\"" + (sdr["tlenumber"].ToString()) + "\","
                    //                  + "\"" + (sdr["OrderID"].ToString()) + "\","
                    //                   + "\"" + (sdr["OrderedDateTime"].ToString()) + "\","
                    //                    + "\"" + (sdr["SupplierItemCode"].ToString()) + "\","
                    //                      + "\"" + (sdr["Description"].ToString()) + "\","
                    //                          + "\"" + sdr["itemQty"].ToString() + "\"],";

                    //        }
                    //        int Length = strOutput.Length;
                    //        strOutput = strOutput.Substring(0, (Length - 1));
                    //    }

                    //}
                    //strOutput = strOutput + "]}";
                    conn.Close();
                }

            }
            var comListObj = new List<CompanyProduct>();

            comListObj = (from DataRow row in dt.Rows

                          select new CompanyProduct
                          {
                              CompanyId = row["CompanyID"].ToString(),
                              CompanyName = row["CompanyName"].ToString(),
                              contactName = row["contactName"].ToString(),
                              CreatedDateTime = row["CreatedDateTime"].ToString(),
                              Description = row["Description"].ToString(),
                              itemQty = Convert.ToInt32(row["itemQty"].ToString()),
                              OrderID = Convert.ToInt32(row["OrderID"].ToString()),
                              SupplierItemCode = row["SupplierItemCode"].ToString(),
                              tlenumber = row["tlenumber"].ToString(),
                              CreatedBy = row["CreatedBy"].ToString(),
                              UnitAmount = row["UnitAmount"].ToString(),
                              AccountOwner = row["AccountOwner"].ToString(),

                          }).ToList();
            return comListObj;

        }

        protected void sendEmail(object sender, EventArgs e)
        {
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var Cdal = new ContactDAL(conn);
            foreach (GridViewRow row in GridViewProduct.Rows)
            {
                CheckBox chkSelect = (CheckBox)(row.Cells[0].FindControl("SelectCheckBox"));
                if (chkSelect.Checked)
                {
                    string hiddenFieldValue = (row.Cells[0].FindControl("comId") as HiddenField).Value;
                    var orderId = (row.Cells[0].FindControl("OrderID") as Label).Text;
                    var producTCode = (row.Cells[0].FindControl("SupplierItemCode") as Label).Text;
                    var AccountOwner = (row.Cells[0].FindControl("AccountOwner") as Label).Text;
                    var contactName = (row.Cells[0].FindControl("contactName") as Label).Text;
                    var conectID = Cdal.getContactByCompanyBasedOnLastOrder(Convert.ToInt32(hiddenFieldValue));
                    var conEmail = Cdal.GetContactByContactId(Convert.ToInt32(conectID));
                    contactName = conEmail.FirstName;
                    if (!string.IsNullOrEmpty(conEmail.Email))
                        SendMEmail(conEmail.Email, contactName, AccountOwner, hiddenFieldValue, orderId);

                }
            }
            messagelable.Text = "Successfully sent";

        }


        private void AuditCustomerEmail(string compId, string body, string orderID)
        {
            var previousValues = "";
            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var newvalues = body;
            newvalues = Regex.Replace(newvalues, @"(<img\/?[^>]+>)", @"",
RegexOptions.IgnoreCase);
            if (newvalues.Length > 1000)
                newvalues = newvalues.Substring(0, 1000);
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = strConnectionString;
            var ActionType = "Email Customer Product";
            var primaryKey = Convert.ToInt32(orderID);
            var columnName = "Email Order";
            var talbeName = "CustomerOrder";
            new DeltoneCRM_DAL.CompanyDAL(strConnectionString).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(compId));
        }

        private void SendMEmail(string contactEmail, string contactName, string repName, string comId, string orderId)
        {
            contactEmail = contactEmail.Trim();
            var subject = subjectMessage.Text;
            String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";

            //  var bodytitle = "Hi  " + contactName + " , <br/>";
            if (repName == "House Account")
                repName = "Taras Selemba";

            var bodytitle = "Hi  " + contactName + " , <br/><br/>" + bodycontentTextArea.Text;


            var body = CreateEmailTemplateForContact(bottomBanner, bodytitle, repName);

            var from = "info@deltonesolutions.com.au";
            var fromName = "Deltonesolutions";
            var sendEmail = new EmailSender();
            var BccAddress = "sentemails@deltonesolutions.com.au";


            sendEmail.SendEmailAlternativeView(from, fromName, contactEmail,
                            BccAddress, "", subject, body, true, null);


            AuditCustomerEmail(comId, bodytitle, orderId);


        }

        private static string CreateEmailTemplateForContact(string bottomBanner, string title, string repName)
        {
            String output = String.Empty;
            var listItems = new List<string>();

            output = "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 3.2//EN'><html>";

            output = output + "<body style='font-family:Calibri;'><table align='center' cellpadding='0' cellspacing='0' width='100%'><tr><td><table  cellpadding='0' cellspacing='0' width='780px' height='85px'>";
            output = output + title + "</td></tr><tr><td>&nbsp;</td></tr><tr><td><table style='width:720px;'><tr>";


            Char spacer = (char)13;

            output = output + @"<tr><td style='font-family:Calibri;'>Kind Regards,</td></tr><tr> <tr> <td>" + repName +
     "<tr> <td>Account Manager</td></tr></td></tr><tr><td style='font-family:Calibri;'></td></tr><tr><td style='font-family:Calibri;'>"
                                                      + bottomBanner + "</td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";

            return output;
        }

        protected void CreateDownloadFile(object sender, EventArgs e)
        {
            CreateExcelFile();
        }

        private void CreateExcelFile()
        {
            String StringBuilder = String.Empty;
            var result = GetALLOrderedOrders("");
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {

                result = GetALLOrderedOrders(searchTextBox.Text);
            }
            if (Session["sorList"] != null)
            {
                result = Session["sorList"] as List<CompanyProduct>;
            }

            string nameFile = "";

            //  var filDirctory = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/RMA/");
            var filePath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\productsearch\\createdFile\\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            nameFile = "orderedproductitems_" + DateTime.Now.Ticks + ".csv";
            var fullFilePath = filePath + nameFile;
            StreamWriter SW = File.CreateText(fullFilePath);
            SW.Write("Company,Contact,Telephone,Order No,Ordered Date,Product Code,Description,Quantity,Unit Amount,Accout Owner\r\n");
            foreach (var item in result)
            {
                StringBuilder = String.Empty;
                StringBuilder = item.CompanyName + "," + item.contactName + "," + item.tlenumber + ","
                    + item.OrderID + "," + item.CreatedDateTime + "," + item.SupplierItemCode
                    + "," + item.Description + "," + item.itemQty + "," + item.UnitAmount + "," + item.AccountOwner;

                // int Length = StringBuilder.Length;
                //StringBuilder = StringBuilder.Substring(0, (Length - 1));
                StringBuilder = StringBuilder + "\r\n";
                SW.Write(StringBuilder);
            }
            SW.Close();


            Response.ContentType = "text/csv";

            Response.AddHeader("Content-Disposition", "attachment;filename=" + nameFile);

            Response.TransmitFile(fullFilePath);
            Response.End();
        }
    
    }
}