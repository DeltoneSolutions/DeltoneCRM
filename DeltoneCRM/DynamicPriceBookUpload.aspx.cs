using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class DynamicPriceBookUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillSuppName();
                
            }
            if (Session["fileUploadpricebook"] != null)
            {
                maindatatableMain.Style.Add("display","block");
            }
        }
        private void FillSuppName()
        {
             String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String AddStr = "SELECT SupplierName, SupplierID FROM dbo.Suppliers";
            SqlCommand ADDcmd = new SqlCommand(AddStr, conn);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(ADDcmd);
            conn.Close();
            adapter.Fill(table);
            suppName.AppendDataBoundItems = true;
            suppName.DataSource = table;
            suppName.DataValueField = "SupplierID";
            suppName.DataTextField = "SupplierName";
            suppName.DataBind();
            
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (suppName.SelectedValue == "0")
            {
                messagelabel.Text = "Please select supplier.";
                return;
            }
            var supplierId = Convert.ToInt32( suppName.SelectedValue);

            try
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                var filePath = Server.MapPath("~/Com/temp/");
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                
                filePath=filePath + fileName;
                File.Delete(filePath);
                FileUpload1.PostedFile.SaveAs(filePath);

                string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx

                OleDbConnection connExcel = new OleDbConnection(connStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;

                oda.Fill(dt);
                connExcel.Close();

                StringBuilder sb = new StringBuilder();
                var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
                foreach (DataRow row in dt.Rows)
                {
                    var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                    obj.SupplierItemCode = row[0].ToString().Trim();
                    obj.PriceUpdate = row[2].ToString();
                    obj.SupplierName = suppName.SelectedItem.Text;
                    obj.Description = row[1].ToString();
                    obj.DSB = row[3].ToString();
                    obj.ResellPrice = row[4].ToString();
                    obj.PrinterCompatibility = row[5].ToString();
                    obj.PrinterCompatibility = obj.PrinterCompatibility.Replace(",", " ");
                    list.Add(obj);
                }
                var strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                // var str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForAdmin(list, supplierId);
                Session["supplierId"] = supplierId;
                Session["fileUploadpricebook"] = list;
                Response.Redirect("DynamicPriceBookUpload.aspx");
            }
            catch (Exception ex)
            {
                messagelabel.Text = "An Error occurred while reading the file.";
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Session["fileUploadpricebook"] != null)
            {
                var listattendanceList = Session["fileUploadpricebook"] as List<DeltoneCRM_DAL.ItemDAL.SupplierItem>;
                var suppId = Convert.ToInt32( Session["supplierId"]);

                var strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                var str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForAdmin(listattendanceList, suppId);
                Session["fileUploadpricebook"] = null;
                Session["supplierId"] = null;
                maindatatableMain.Style.Add("display", "none");
                messagelabel.Text = "Successfully Uploaded.";
            }
        }
    }
}