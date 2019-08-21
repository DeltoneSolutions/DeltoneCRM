using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class OrderFileUpload : System.Web.UI.Page
    {
        protected string str_ORDERID { get; set; }
        static String connString;
        PurchaseDAL purchasedal;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUserID"] == null)
                return;

            if (Request["ordID"] != null)
            {
                str_ORDERID = Request["ordID"].ToString();
            }
            if (IsPostBack)
            {
                UploadFile(sender, e);
            }
            LoadUploadedFiles();
            var y = GetOrdersStatus(str_ORDERID);
            if (y == "Y")
            {
                ButtonApproveOrderPurchase.Visible = true;

            }

            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            purchasedal = new PurchaseDAL(connString);

            // var ttt= string.Format("openTab({0});", "test");
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("dashboard1.aspx");



        }

        protected String GetOrdersStatus(string orderID)
        {
            var strOutput = "";

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {


                    cmd.CommandText = "SELECT COMPLETED , Status FROM dbo.Orders WHERE OrderID=" + orderID;

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                if (sdr["COMPLETED"] != DBNull.Value)

                                    strOutput = "Y";
                                else
                                    if (sdr["Status"].ToString() == "COMPLETED")
                                        strOutput = "Y";
                            }

                        }

                    }

                    conn.Close();
                }

            }
            return strOutput;
        }

        protected void UploadFile(object sender, EventArgs e)
        {

            HttpFileCollection fileCollection = Request.Files;
            var fileNamecutom = "";
            if (Request["namechange"] != null)
                fileNamecutom = Request["namechange"].ToString();
            if (fileNamecutom == "null")
                fileNamecutom = "";
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile upload = fileCollection[i];

                var filDirctory = Server.MapPath("~/Uploads/" + str_ORDERID + "/");

                if (!Directory.Exists(filDirctory))
                {
                    Directory.CreateDirectory(filDirctory);
                }


                string fileName = Path.GetFileName(upload.FileName);
                if (fileNamecutom != "")
                {
                    var fileExtension = Path.GetExtension(upload.FileName);
                    var fileextensionContain = Path.GetExtension(fileNamecutom);
                    if (fileextensionContain != "")
                        fileName = fileNamecutom;
                    else
                        fileName = fileNamecutom + fileExtension;
                }
                upload.SaveAs(filDirctory + fileName);


            }
        }

        protected void LoadUploadedFiles()
        {

            if (Directory.Exists(Server.MapPath("~/Uploads/" + str_ORDERID)))
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/" + str_ORDERID));
                //new DirectoryInfo(@"D:\samples").GetFiles()
                //                              .OrderBy(f => f.LastWriteTime)
                //                              .ToList();
                List<System.Web.UI.WebControls.ListItem> files = new List<System.Web.UI.WebControls.ListItem>();
                DateTime[] creationTimes = new DateTime[filePaths.Length];
                for (int i = 0; i < filePaths.Length; i++)
                    creationTimes[i] = new FileInfo(filePaths[i]).CreationTime;
                Array.Sort(creationTimes, filePaths);
                foreach (string filePath in filePaths)
                {

                    files.Add(new System.Web.UI.WebControls.ListItem(Path.GetFileName(filePath), filePath));
                }
                GridView1.DataSource = files;
                GridView1.DataBind();
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void ButtonCreateBill_click(object sender, EventArgs e)
        {
            var Orderid = Convert.ToInt32(str_ORDERID);
            var purchasedXeroINfo = purchasedal.GetSupplierNameAndXeroNumber(Orderid);

            if (purchasedXeroINfo.Count() > 0)
            {
                //Xero Connection
                XeroIntergration xero = new XeroIntergration();
                xero.ChangeStatusPurchaseOrderBilled(purchasedXeroINfo);
                CreateAuditRecord("Purchase Order Billed", "Billed");
                messagelable.Text = "Successfully Updated";
            }
            else
                messagelable.Text = "Purchse Order was not created successfully";
        }

        protected void ButtonApproveOrderPurchase_click(object sender, EventArgs e)
        {
            var Orderid = Convert.ToInt32(str_ORDERID);
            var purchasedXeroINfo = purchasedal.GetSupplierNameAndXeroNumber(Orderid);
            if (purchasedXeroINfo.Count() > 0)
            {
                //Xero Connection
                XeroIntergration xero = new XeroIntergration();
                xero.ChangeStatusPurchaseOrderAuthorised(purchasedXeroINfo);
                CreateAuditRecord("Purchase Order Approved", " Draft to Authorised");
                xero.ChangeStatusPurchaseOrderBilled(purchasedXeroINfo);
                CreateAuditRecord("Purchase Order Billed", " Authorised to Billed");
                messagelable.Text = "Successfully Updated.Please check in Xero.";
            }
            else
                messagelable.Text = "Purchse Order was not created successfully";
        }

        protected void OpenTab(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            var filePathOri = Path.GetFileName(filePath);

            //   var domainNameWithFile = "http://localhost:65085//Uploads/";
            var domainNameWithFileServer = "http://delcrm//Uploads/";

            var filePathWithDomain = domainNameWithFileServer + str_ORDERID + "/" + filePathOri;


            string strJS = ("<script type='text/javascript'>window.open('" + filePathWithDomain + "','_blank');</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "strJSAlert", strJS);

            //     var blank="_blank";
            //     string newWin =

            //"window.open('" + lastPath + "','" + blank + "');";
            //     Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", newWin, true);
        }

        protected void changeOrder_click(object sender, EventArgs e)
        {
            updateOrderStatus();
            CreateAuditRecord("COMPLETED", "APPROVED to COMPLETED");
            messagelable.Text = "Successfully Updated";

            ButtonApproveOrderPurchase.Visible = true;


        }

        private void CreateAuditRecord(string stsMessage, string endmessage)
        {
            var columnName = "Status";
            var talbeName = "Order";
            var ActionType = stsMessage;
            int primaryKey = Convert.ToInt32(str_ORDERID);


            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;


            var newvalues = "Order  Id " + str_ORDERID + " :Order Status changed from " + endmessage;



            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var previousValues = "APPROVED";
            var strCompanyID = new OrderDAL(CONNSTRING).getCompanyIDFromOrder(primaryKey);

            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));
        }

        protected int updateOrderStatus()
        {

            int rowsEffected = -1;
            var completed = "Y";
            var userName = Session["LoggedUser"].ToString();
            var status = "COMPLETED";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLUpdateStmt = "UPDATE dbo.Orders SET Completed=@completed,AlteredBy=@AlteredBy ,AlteredDateTime=CURRENT_TIMESTAMP,Status=@status WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@completed", completed);
            var orderId = Convert.ToInt32(str_ORDERID);
            cmd.Parameters.AddWithValue("@OrderID", orderId);
            cmd.Parameters.AddWithValue("@AlteredBy", userName);
            cmd.Parameters.AddWithValue("@status", status);

            try
            {
                conn.Open();
                rowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }
            return rowsEffected;

        }

        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            System.IO.File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        private void SetBkColor()
        {
            GridViewRowCollection rows = GridView1.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                //set the background color for the 2nd row of Gridview
                if (i % 2 == 0)
                {
                    rows[i].BackColor = ColorTranslator.FromHtml("#C0C0C0");
                }
                else
                {
                    rows[i].BackColor = ColorTranslator.FromHtml("#D3D3D3");
                }
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            SetBkColor();
        }



    }
}