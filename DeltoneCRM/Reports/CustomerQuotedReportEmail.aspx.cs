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
    public partial class CustomerQuotedReportEmail : System.Web.UI.Page
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
        }

        public void fillRepsList()
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

        protected void reloadPage(object sender, EventArgs e)
        {
            var list = GetALLOrderedOrders("");
            searchTextBox.Text = "";
            StartDateSTxt.Value = "";
            EndDateSTxt.Value = "";
            GridViewProduct.DataSource = list;
            GridViewProduct.DataBind();
            Session["sorListQuoted"] = null;

            var total = list.Sum(x => x.itemQty);
            GridViewProduct.FooterRow.Cells[7].Text = "Total";
            GridViewProduct.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[8].Text = total.ToString();
            GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            // Session["orderIDfile"] = orderId;
            Response.Redirect("~/dashboard1.aspx");
        }

        protected void btnbackupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx");
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
            GridViewProduct.FooterRow.Cells[7].Text = "Total";
            GridViewProduct.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[8].Text = total.ToString();
            GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;
        }

        protected void CreateDownloadFile(object sender, EventArgs e)
        {
            CreateExcelFile();
        }
        protected void sendEmail(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridViewProduct.Rows)
            {
                CheckBox chkSelect = (CheckBox)(row.Cells[0].FindControl("SelectCheckBox"));
                if (chkSelect.Checked)
                {
                    string CompanyhiddenFieldValue = (row.Cells[0].FindControl("comId") as HiddenField).Value;
                    string contactEmailhiddenFieldValue = (row.Cells[0].FindControl("emailval") as HiddenField).Value;
                    var orderId = (row.Cells[0].FindControl("OrderID") as Label).Text;
                    var producTCode = (row.Cells[0].FindControl("SupplierItemCode") as Label).Text;
                    var AccountOwner = (row.Cells[0].FindControl("AccountOwner") as Label).Text;
                    var contactName = (row.Cells[0].FindControl("contactName") as Label).Text;
                    var contactFirstName = (row.Cells[0].FindControl("firstname") as HiddenField).Value;
                    // var conectID = Cdal.getContactByCompanyBasedOnLastOrder(Convert.ToInt32(hiddenFieldValue));
                    // var conEmail = Cdal.GetContactByContactId(Convert.ToInt32(conectID));
                    contactName = contactFirstName;
                    var email = contactEmailhiddenFieldValue;
                    if (!string.IsNullOrEmpty(email))
                        SendMEmail(email, contactName, AccountOwner, CompanyhiddenFieldValue, orderId);

                }
            }
            messagelable.Text = "Successfully sent";
        }

        private void SendMEmail(string contactEmail, string contactName, string repName, string comId, string orderId)
        {
            contactEmail = contactEmail.Trim();
            if (repName == "House Account")
                repName = "Taras Selemba";
            var subject = subjectMessage.Text;
            String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";

            //  var bodytitle = "Hi  " + contactName + " , <br/>";

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
            var columnName = "Email Quote";
            var talbeName = "CustomerOrder";
            new DeltoneCRM_DAL.CompanyDAL(strConnectionString).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(compId));
        }

        private void CreateExcelFile()
        {
            String StringBuilder = String.Empty;
            var result = GetALLOrderedOrders("");
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {

                result = GetALLOrderedOrders(searchTextBox.Text);
            }
            if (Session["sorListQuoted"] != null)
            {
                result = Session["sorListQuoted"] as List<CompanyProduct>;
            }

            string nameFile = "";

            //  var filDirctory = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/RMA/");
            var filePath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\productsearch\\createdFile\\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            nameFile = "quotedproductitems_" + DateTime.Now.Ticks + ".csv";
            var fullFilePath = filePath + nameFile;
            StreamWriter SW = File.CreateText(fullFilePath);
            SW.Write("Company,Contact,Telephone,Quote No,Quoted Date,Product Code,Description,Quantity,Unit Amount,Accout Owner\r\n");
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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProduct.PageIndex = e.NewPageIndex;

            var comListObj = new List<CompanyProduct>();
            if (Session["sorListQuoted"] != null)
            {
                var sessionLit = Session["sorListQuoted"] as List<CompanyProduct>;
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
                {
                    comListObj = GetALLOrderedOrders("");
                    GridViewProduct.DataSource = comListObj;
                    GridViewProduct.DataBind();
                }
            }

            var totalitems = comListObj.Sum(x => x.itemQty);
            GridViewProduct.FooterRow.Cells[7].Text = "Total";
            GridViewProduct.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[8].Text = totalitems.ToString();
            GridViewProduct.FooterRow.Cells[7].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;


        }

        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var stringSearch = "";
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                stringSearch = searchTextBox.Text;
            }
            var comList = GetALLOrderedOrders(stringSearch);
            if (Session["sorListQuoted"] == null)
                Session["sorListQuoted"] = comList;
            var sessionLit = Session["sorListQuoted"] as List<CompanyProduct>;


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
            if (Session["sortDireQuote"] == null)
                Session["sortDireQuote"] = "Asc," + sortExpression;

            var splitDir = "";
            if (Session["sortDireQuote"] != null)
                splitDir = Session["sortDireQuote"].ToString().Split(',')[1];
            if (SortDirection == "ASC")
            {
                if (sortExpression == "CompanyName")
                    sessionLit = sessionLit.OrderBy(x => x.CompanyName).ToList();
                else if (sortExpression == "contactName")
                    sessionLit = sessionLit.OrderBy(x => x.contactName).ToList();
                else if (sortExpression == "tlenumber")
                    sessionLit = sessionLit.OrderBy(x => x.Email).ToList();
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
                    sessionLit = sessionLit.OrderByDescending(x => x.Email).ToList();
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

            Session["sorListQuoted"] = sessionLit;

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



        protected List<CompanyProduct> GetALLOrderedOrders(string sear)
        {
            DataTable dt = new DataTable();
            //String strOutput = "{\"data\":[";
            var accoutOwnerID = ddlRepList.SelectedValue;


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    var querystring = "SELECT QT.QuoteID,CT.Email, QT.QuoteDateTime as  QuoteDate, QTIT.Description,QTIT.UnitAmount,CT.DEFAULT_AreaCode + '' + CT.DEFAULT_Number as tlenumber, QTIT.Quantity, QTIT.SupplierCode,CO.CompanyName,CO.CompanyID , QT.CallBackDate,CT.ContactID,'Existing Customer' as Type," +
                       " CT.FirstName + ' ' + CT.LastName AS ContactName,CT.FirstName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                       " Quote_Item QTIT  ON  QT.QuoteID=QTIT.QuoteID INNER JOIN " +
                       "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB' AND  (QTIT.SupplierCode LIKE '%" + sear + "%' OR  QTIT.Description LIKE '%" + sear + "%') " +
                        " Union SELECT QT.QuoteID,CT.Email, QT.QuoteDateTime as  QuoteDate, QTIT.Description,QTIT.UnitAmount,CT.DEFAULT_AreaCode + '' + CT.DEFAULT_Number as tlenumber,QTIT.Quantity,  QTIT.SupplierCode, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                       " CT.FirstName + ' ' + CT.LastName AS ContactName,CT.FirstName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                        " Quote_Item QTIT  ON  QT.QuoteID=QTIT.QuoteID INNER JOIN " +
                       "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  AND  (QTIT.SupplierCode LIKE '%" + sear + "%' OR  QTIT.Description LIKE '%" + sear + "%') order by CompanyName asc ";

                    cmd.CommandText = querystring;
                    if (accoutOwnerID != "9999")
                    {
                        querystring = "SELECT QT.QuoteID,CT.Email, QT.QuoteDateTime as  QuoteDate,QTIT.Description,QTIT.UnitAmount,CT.DEFAULT_AreaCode + '' + CT.DEFAULT_Number as tlenumber,QTIT.Quantity, QTIT.SupplierCode, CO.CompanyName,CO.CompanyID , QT.CallBackDate,CT.ContactID,'Existing Customer' as Type," +
                         " CT.FirstName + ' ' + CT.LastName AS ContactName,CT.FirstName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                         " Quote_Item QTIT  ON  QT.QuoteID=QTIT.QuoteID INNER JOIN " +
                         "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB' AND QT.QuoteByID=@qtID  AND   (QTIT.SupplierCode LIKE '%" + sear + "%' OR  QTIT.Description LIKE '%" + sear + "%')  " +
                          " Union SELECT QT.QuoteID,CT.Email, QT.QuoteDateTime as  QuoteDate,QTIT.Description,QTIT.UnitAmount,CT.DEFAULT_AreaCode + '' + CT.DEFAULT_Number as tlenumber,QTIT.Quantity, QTIT.SupplierCode, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                         " CT.FirstName + ' ' + CT.LastName AS ContactName,CT.FirstName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                          " Quote_Item QTIT  ON  QT.QuoteID=QTIT.QuoteID INNER JOIN " +
                         "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  AND QT.QuoteByID=@qtID  AND  (QTIT.SupplierCode LIKE '%" + sear + "%' OR  QTIT.Description LIKE '%" + sear + "%') order by CompanyName asc ";

                        cmd.CommandText = querystring;
                        cmd.Parameters.AddWithValue("@qtID", accoutOwnerID);
                    }


                    if (!string.IsNullOrEmpty(StartDateSTxt.Value) && !string.IsNullOrEmpty(EndDateSTxt.Value))
                    {
                        if (accoutOwnerID != "9999")
                        {


                            querystring = "SELECT QT.QuoteID,CT.Email, QT.QuoteDateTime as  QuoteDate,QTIT.Description,QTIT.UnitAmount,CT.DEFAULT_AreaCode + '' + CT.DEFAULT_Number as tlenumber,QTIT.Quantity, QTIT.SupplierCode, CO.CompanyName,CO.CompanyID , QT.CallBackDate,CT.ContactID,'Existing Customer' as Type," +
                      " CT.FirstName + ' ' + CT.LastName AS ContactName,CT.FirstName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                      " Quote_Item QTIT  ON  QT.QuoteID=QTIT.QuoteID INNER JOIN " +
                      "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB' AND QT.QuoteByID=@qtID  AND   ( QTIT.SupplierCode LIKE '%" + sear + "%' OR  QTIT.Description LIKE '%" + sear + "%') AND  (QT.QuoteDateTime >=@startDa and QT.QuoteDateTime <=@endtDa) " +
                       " Union SELECT QT.QuoteID,CT.Email, QT.QuoteDateTime as  QuoteDate,QTIT.Description,QTIT.UnitAmount,CT.DEFAULT_AreaCode + '' + CT.DEFAULT_Number as tlenumber,QTIT.Quantity, QTIT.SupplierCode, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                      " CT.FirstName + ' ' + CT.LastName AS ContactName,CT.FirstName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                       " Quote_Item QTIT  ON  QT.QuoteID=QTIT.QuoteID INNER JOIN " +
                      "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  AND QT.QuoteByID=@qtID  AND  (QTIT.SupplierCode LIKE '%" + sear + "%' OR  QTIT.Description LIKE '%" + sear + "%') AND  (QT.QuoteDateTime >=@startDa and QT.QuoteDateTime <=@endtDa)  order by CompanyName asc ";

                            cmd.CommandText = querystring;

                            var st = Convert.ToDateTime(StartDateSTxt.Value).ToString("yyyy-MM-dd");
                            var end = Convert.ToDateTime(EndDateSTxt.Value).ToString("yyyy-MM-dd");
                            cmd.Parameters.AddWithValue("@accID", accoutOwnerID);
                            cmd.Parameters.AddWithValue("@startDa", st);
                            cmd.Parameters.AddWithValue("@endtDa", end);
                        }
                        else
                        {

                            querystring = "SELECT QT.QuoteID,CT.Email, QT.QuoteDateTime as  QuoteDate,QTIT.Description,QTIT.UnitAmount,CT.DEFAULT_AreaCode + '' + CT.DEFAULT_Number as tlenumber,QTIT.Quantity,  QTIT.SupplierCode, CO.CompanyName,CO.CompanyID , QT.CallBackDate,CT.ContactID,'Existing Customer' as Type," +
                      " CT.FirstName + ' ' + CT.LastName AS ContactName,CT.FirstName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                      " Quote_Item QTIT  ON  QT.QuoteID=QTIT.QuoteID INNER JOIN " +
                      "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB'  AND   ( QTIT.SupplierCode LIKE '%" + sear + "%' OR  QTIT.Description LIKE '%" + sear + "%') AND  (QT.QuoteDateTime >=@startDa and QT.QuoteDateTime <=@endtDa)  " +
                       " Union SELECT QT.QuoteID,CT.Email, QT.QuoteDateTime as  QuoteDate, QTIT.Description,QTIT.UnitAmount,CT.DEFAULT_AreaCode + '' + CT.DEFAULT_Number as tlenumber,QTIT.Quantity, QTIT.SupplierCode, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                      " CT.FirstName + ' ' + CT.LastName AS ContactName,CT.FirstName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                         " Quote_Item QTIT  ON  QT.QuoteID=QTIT.QuoteID INNER JOIN " +
                      "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  AND  (QTIT.SupplierCode LIKE '%" + sear + "%' OR  QTIT.Description LIKE '%" + sear + "%') AND  (QT.QuoteDateTime >=@startDa and QT.QuoteDateTime <=@endtDa)  order by CompanyName asc ";

                            cmd.CommandText = querystring;

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
                              CreatedDateTime = row["QuoteDate"].ToString(),
                              Description = row["Description"].ToString(),
                              itemQty = Convert.ToInt32(row["Quantity"].ToString()),
                              OrderID = Convert.ToInt32(row["QuoteID"].ToString()),
                              SupplierItemCode = row["SupplierCode"].ToString(),
                              tlenumber = row["tlenumber"].ToString(),
                              CreatedBy = row["QuoteBy"].ToString(),
                              UnitAmount = row["UnitAmount"].ToString(),
                              AccountOwner = row["QuoteBy"].ToString(),
                              QuoteDb = row["Type"].ToString(),
                              Email = row["Email"].ToString(),
                              contactFirstName = row["FirstName"].ToString()

                          }).ToList();
            return comListObj;

        }

        protected class CompanyProduct
        {
            public string CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string contactName { get; set; }
            public string contactFirstName { get; set; }
            public string tlenumber { get; set; }
            public int OrderID { get; set; }
            public string CreatedDateTime { get; set; }
            public string SupplierItemCode { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
            public string UnitAmount { get; set; }
            public string AccountOwner { get; set; }
            public int itemQty { get; set; }
            public string QuoteDb { get; set; }
            public string Email { get; set; }
        }

    }
}