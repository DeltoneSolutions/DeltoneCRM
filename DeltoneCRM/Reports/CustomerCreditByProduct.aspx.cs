using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Reports
{
    public partial class CustomerCreditByProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var list = GetALLOrderedOrders("");
                GridViewProduct.DataSource = list;
                GridViewProduct.DataBind();
                var total = list.Sum(x => x.itemQty);
                GridViewProduct.FooterRow.Cells[8].Text = "Total";
                GridViewProduct.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                GridViewProduct.FooterRow.Cells[9].Text = total.ToString();
                GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
                GridViewProduct.FooterRow.Cells[9].BackColor = System.Drawing.Color.Bisque;
                GridViewProduct.FooterRow.Cells[9].ForeColor = System.Drawing.Color.DarkBlue;
            }
            else
            {
                var list = GetALLOrderedOrders("");
                GridViewProduct.DataSource = list;
                GridViewProduct.DataBind();
                var total = list.Sum(x => x.itemQty);
                GridViewProduct.FooterRow.Cells[8].Text = "Total";
                GridViewProduct.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                GridViewProduct.FooterRow.Cells[9].Text = total.ToString();
                GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
                GridViewProduct.FooterRow.Cells[9].BackColor = System.Drawing.Color.Bisque;
                GridViewProduct.FooterRow.Cells[9].ForeColor = System.Drawing.Color.DarkBlue;
            }
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            var comListObj = new List<CompanyProduct>();
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                comListObj = GetALLOrderedOrders(searchTextBox.Text);
            }
            else
                comListObj = GetALLOrderedOrders("");

            GridViewProduct.DataSource = comListObj;
            GridViewProduct.DataBind();
            var total = comListObj.Sum(x => x.itemQty);
            GridViewProduct.FooterRow.Cells[8].Text = "Total";
            GridViewProduct.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[9].Text = total.ToString();
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[9].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[9].ForeColor = System.Drawing.Color.DarkBlue;
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("~/dashboard1.aspx");


        }

        protected void btnbackupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("Reports.aspx");


        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProduct.PageIndex = e.NewPageIndex;
            var comListObj = new List<CompanyProduct>();
            if (Session["sorList"] != null)
            {
                var sessionLit = Session["sorList"] as List<CompanyProduct>;
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
            GridViewProduct.FooterRow.Cells[8].Text = "Total";
            GridViewProduct.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[9].Text = total.ToString();
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[9].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[9].ForeColor = System.Drawing.Color.DarkBlue;

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
                    sessionLit = sessionLit.OrderBy(x => x.CreatedDateTime).ToList();
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
                    sessionLit = sessionLit.OrderByDescending(x => x.CreatedDateTime).ToList();
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



            GridViewProduct.DataSource = sessionLit;
            GridViewProduct.DataBind();

            var total = sessionLit.Sum(x => x.itemQty);
            GridViewProduct.FooterRow.Cells[8].Text = "Total";
            GridViewProduct.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            GridViewProduct.FooterRow.Cells[9].Text = total.ToString();
            GridViewProduct.FooterRow.Cells[8].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[9].BackColor = System.Drawing.Color.Bisque;
            GridViewProduct.FooterRow.Cells[9].ForeColor = System.Drawing.Color.DarkBlue;

        }

        protected class CompanyProduct
        {
            public string CompanyName { get; set; }
            public string contactName { get; set; }
            public string tlenumber { get; set; }
            public string OrderID { get; set; }
            public string CreatedDateTime { get; set; }
            public string SupplierItemCode { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
            public string UnitAmount { get; set; }
            public string AccountOwner { get; set; }
            public int itemQty { get; set; }
            public string CreditID { get; set; }
            public string Reason { get; set; }

        }


        protected List<CompanyProduct> GetALLOrderedOrders(string sear)
        {
            DataTable dt = new DataTable();
            //String strOutput = "{\"data\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"select co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.DateCreated,ord.XeroCreditNoteID,ord.CreditNoteReason,ocr.XeroInvoiceNumber,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,ord.AccountOwner
                                          from CreditNote_Item OT INNER JOIN
	                          
	                                  Items IT ON IT.SupplierItemCode = OT.SupplierCode 
	                                   INNER JOIN  CreditNotes ord ON ord.CreditNote_ID = OT.CreditNoteID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompID 
                                       INNER JOIN  Orders ocr ON ocr.OrderID = ord.OrderID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
                                  WHERE ( it.SupplierItemCode LIKE '%" + sear + "%' OR  it.Description LIKE '%" + sear + "%') order by CompanyName asc";
                    var selectedReasonrr = "";
                    selectedReasonrr = slTypeOfCredit.Value;
                    if (selectedReasonrr == "0")
                    {
                        if (!string.IsNullOrEmpty(StartDateSTxt.Value) && !string.IsNullOrEmpty(EndDateSTxt.Value))
                        {
                            var comString = @"select co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.DateCreated,ord.XeroCreditNoteID,ord.CreditNoteReason,ocr.XeroInvoiceNumber,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,ord.AccountOwner
                                          from CreditNote_Item OT INNER JOIN
	                          
	                                  Items IT ON IT.SupplierItemCode = OT.SupplierCode 
	                                   INNER JOIN  CreditNotes ord ON ord.CreditNote_ID = OT.CreditNoteID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompID 
                                       INNER JOIN  Orders ocr ON ocr.OrderID = ord.OrderID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
                                  WHERE ( it.SupplierItemCode LIKE '%" + sear + "%' OR  it.Description LIKE '%" + sear + "%') and (ord.DateCreated >=@startDa and ord.DateCreated <=@endtDa) order by CompanyName asc";
                            cmd.CommandText = comString;
                            var st = Convert.ToDateTime(StartDateSTxt.Value).ToString("yyyy-MM-dd");
                            var end = Convert.ToDateTime(EndDateSTxt.Value).ToString("yyyy-MM-dd");
                            cmd.Parameters.AddWithValue("@startDa", st);
                            cmd.Parameters.AddWithValue("@endtDa", end);
                        }

                    }
                    var selectedReason = "";
                    selectedReason = slTypeOfCredit.Value;
                    if (selectedReason != "0")
                    {

                        if (!string.IsNullOrEmpty(StartDateSTxt.Value) && !string.IsNullOrEmpty(EndDateSTxt.Value))
                        {
                            var comString = @"select co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.DateCreated,ord.XeroCreditNoteID,ord.CreditNoteReason,ocr.XeroInvoiceNumber,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,ord.AccountOwner
                                          from CreditNote_Item OT INNER JOIN
	                          
	                                  Items IT ON IT.SupplierItemCode = OT.SupplierCode 
	                                   INNER JOIN  CreditNotes ord ON ord.CreditNote_ID = OT.CreditNoteID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompID 
                                       INNER JOIN  Orders ocr ON ocr.OrderID = ord.OrderID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
                                  WHERE ( it.SupplierItemCode LIKE '%" + sear + "%' OR  it.Description LIKE '%" + sear + "%') and (ord.DateCreated >=@startDa and ord.DateCreated <=@endtDa) and (ord.CreditNoteReason = @reson) order by CompanyName asc";
                            cmd.CommandText = comString;
                            var st = Convert.ToDateTime(StartDateSTxt.Value).ToString("yyyy-MM-dd");
                            var end = Convert.ToDateTime(EndDateSTxt.Value).ToString("yyyy-MM-dd");
                            cmd.Parameters.AddWithValue("@startDa", st);
                            cmd.Parameters.AddWithValue("@endtDa", end);
                            cmd.Parameters.AddWithValue("@reson", selectedReason);

                        }
                        else
                        {

                            var comString = @"select co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.DateCreated,ord.XeroCreditNoteID,ord.CreditNoteReason,ocr.XeroInvoiceNumber,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,ord.AccountOwner
                                          from CreditNote_Item OT INNER JOIN
	                          
	                                  Items IT ON IT.SupplierItemCode = OT.SupplierCode 
	                                   INNER JOIN  CreditNotes ord ON ord.CreditNote_ID = OT.CreditNoteID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompID 
                                       INNER JOIN  Orders ocr ON ocr.OrderID = ord.OrderID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
                                  WHERE ( it.SupplierItemCode LIKE '%" + sear + "%' OR  it.Description LIKE '%" + sear + "%')  and (ord.CreditNoteReason = @reson) order by CompanyName asc";
                            cmd.CommandText = comString;

                            cmd.Parameters.AddWithValue("@reson", selectedReason);
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
                              CompanyName = row["CompanyName"].ToString(),
                              contactName = row["contactName"].ToString(),
                              CreatedDateTime = row["DateCreated"].ToString(),
                              Description = row["Description"].ToString(),
                              itemQty = Convert.ToInt32(row["itemQty"].ToString()),
                              CreditID = row["XeroCreditNoteID"].ToString(),
                              OrderID = row["XeroInvoiceNumber"].ToString(),
                              SupplierItemCode = row["SupplierItemCode"].ToString(),
                              tlenumber = row["tlenumber"].ToString(),
                              CreatedBy = row["CreatedBy"].ToString(),
                              UnitAmount = row["UnitAmount"].ToString(),
                              AccountOwner = row["AccountOwner"].ToString(),
                              Reason = row["CreditNoteReason"].ToString(),

                          }).ToList();
            return comListObj;

        }
    }
}