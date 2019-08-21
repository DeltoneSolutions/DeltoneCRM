using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Collections;
using Excel;
using System.Data.SqlClient;

namespace DeltoneCRM.Uploader
{
    public partial class Uploader : System.Web.UI.Page
    {
        String _path;
        ArrayList alSupplierItemCode = new ArrayList();
        ArrayList alItemPrice = new ArrayList();
        ArrayList fullItemList = new ArrayList();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable ExcelDT = new DataTable();
        
        int counter = 0;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("SupplierItemCode");
            dt.Columns.Add("ExistsInDB");
            dt.Columns.Add("Action");
            dt.Columns.Add("PricingInExcel");
            dt.Columns.Add("PricingInDB");

            ExcelDT.Columns.Add("SupplierID");
            ExcelDT.Columns.Add("SupplierItemCode");
            ExcelDT.Columns.Add("OEMCode");
            ExcelDT.Columns.Add("Description");
            ExcelDT.Columns.Add("COG");
            ExcelDT.Columns.Add("ManagerUnitPrice");

            BtnProcess.Style["visibility"] = "hidden";
           
        }

        protected void btnProcess_Click(Object sender, EventArgs e)
        {
            ProceedToUpdate();
        }

        protected void btnUpload_Click(Object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);
                ExcelData(FilePath);
                WriteData();
            }
        }

        public void ExcelData(string path)
        {
            _path = path;
        }

        public IExcelDataReader getExcelReader()
        {
            FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read);

            IExcelDataReader reader = null;
            try
            {
                if (_path.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                if (_path.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return reader;
        }

        public IEnumerable<string> getWorksheetNames()
        {
            var reader = this.getExcelReader();
            var workbook = reader.AsDataSet();
            var sheets = from DataTable sheet in workbook.Tables select sheet.TableName;
            return sheets;
        }

        public IEnumerable<DataRow> getData(string sheet, bool firstRowIsColumnNames = true)
        {
            var reader = this.getExcelReader();
            reader.IsFirstRowAsColumnNames = firstRowIsColumnNames;
            var worksheet = reader.AsDataSet().Tables[sheet];
            var rows = from DataRow a in worksheet.Rows select a;
            return rows;
        }

        void WriteData()
        {
            string path = _path;
            var rowValues = getData("Sheet1");
            int counter = 0;
            foreach (var row in rowValues)
            {
                Response.Write(Environment.NewLine);
                for (int i = 0; i <= row.ItemArray.GetUpperBound(0); i++)
                {
               
                    alSupplierItemCode.Add(row["SupplierItemCode"].ToString());
                    alItemPrice.Add(row["UnitPrice"].ToString());
                    
                }
                checkIfItemExists(row["SupplierItemCode"].ToString(), row["UnitPrice"].ToString());
                ExcelDT.Rows.Add();
                ExcelDT.Rows[counter]["SupplierID"] = 3;
                ExcelDT.Rows[counter]["SupplierItemCode"] = row["SupplierItemCode"].ToString();
                ExcelDT.Rows[counter]["OEMCode"] = row["OEMCode"].ToString();
                ExcelDT.Rows[counter]["Description"] = row["Description"].ToString();
                ExcelDT.Rows[counter]["COG"] = row["COG"].ToString();
                ExcelDT.Rows[counter]["ManagerUnitPrice"] = row["UnitPrice"].ToString();
                counter++;
            }
            DumpDBToExcel();
            BtnProcess.Style["visibility"] = "visible";
            
        }


        public void checkIfItemExists(string SIC, string ItemPrice)
        {
            string ItemStatus = "";
            
            

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Items WHERE SupplierItemCode = '" + SIC + "'";

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                ItemStatus = "YES";
                                string MUP = sdr["ManagerUnitPrice"].ToString();
                                string ItemPriceResult = GetItemPricing(float.Parse(MUP), float.Parse(ItemPrice));
                                dt.Rows.Add();
                                dt.Rows[counter]["SupplierItemCode"] = SIC;
                                dt.Rows[counter]["ExistsInDB"] = ItemStatus;
                                dt.Rows[counter]["Action"] = ItemPriceResult;
                                dt.Rows[counter]["PricingInExcel"] = string.Format("{0:C}",ItemPrice);
                                dt.Rows[counter]["PricingInDB"] = string.Format("{0:C}", MUP);
                                counter = counter + 1;
                            }
                        }
                        else
                        {
                            ItemStatus = "NO";
                            dt.Rows.Add();
                            dt.Rows[counter]["SupplierItemCode"] = SIC;
                            dt.Rows[counter]["ExistsInDB"] = ItemStatus;
                            dt.Rows[counter]["Action"] = "Item Will Be Added To Database";
                            dt.Rows[counter]["PricingInExcel"] = String.Format("{0:C}", ItemPrice);
                            dt.Rows[counter]["PricingInDB"] = "N/A";
                            counter = counter + 1;
                        }
                    }
                }
                ResultGrid.DataSource = dt;
                ResultGrid.DataBind();
                conn.Close();
                
            }

            
              
        }

        public void ProceedToUpdate()
        {
            Response.Write("Processing.....please wait");
            string insertSQL = "";

            foreach (DataRow row in dt.Rows)
            {
                string theConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                string inDB = row["ExistsInDB"].ToString();

                if (inDB == "NO")
                {
                    insertSQL = "INSERT INTO dbo.Items (SupplierID, SupplierItemCode, OEMCode, Description, COG, ManagerUnitPrice, RepUnitPrice)";
                    insertSQL += "VALUES (@supplierID, @SupplierItemCode, @OEMCOde, @Desciption, @COG, @RetailPrice)";

                    SqlConnection con = new SqlConnection(theConn);
                    SqlCommand cmd = new SqlCommand(insertSQL, con);

                    cmd.Parameters.AddWithValue("@supplierID", "3");
                    cmd.Parameters.AddWithValue("@SupplierItemCode", row["SupplierItemCode"].ToString());
                }
            }

        }

        private void DumpDBToExcel()
        {
            

            dt2.Columns.Add("ItemID");
            dt2.Columns.Add("SupplierID");
            dt2.Columns.Add("SupplierItemCode");
            dt2.Columns.Add("OEMCode");
            dt2.Columns.Add("Description");
            dt2.Columns.Add("COG");
            dt2.Columns.Add("ManagerUnitPrice");
            dt2.Columns.Add("RepUnitPrice");

            int counter = 0;

            string NowDateTime = DateTime.Now.ToString();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Items";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                dt2.Rows.Add();
                                dt2.Rows[counter]["ItemID"] = sdr["ItemID"].ToString();
                                dt2.Rows[counter]["SupplierID"] = sdr["SupplierID"].ToString();
                                dt2.Rows[counter]["SupplierItemCode"] = sdr["SupplierItemCode"].ToString();
                                dt2.Rows[counter]["OEMCode"] = sdr["OEMCode"].ToString();
                                dt2.Rows[counter]["Description"] = sdr["Description"].ToString();
                                dt2.Rows[counter]["COG"] = sdr["COG"].ToString();
                                dt2.Rows[counter]["ManagerUnitPrice"] = sdr["ManagerUnitPrice"].ToString();
                                dt2.Rows[counter]["RepUnitPrice"] = sdr["RepUnitPrice"].ToString();
                                counter = counter + 1;
                            }
                        }
                    }
                }
            }

            string basePath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\Exporter\\Dumps\\";
            string newFileNamae = AppendTimeStamp("db_dump_.xls");
            string SaveFilePath = Path.Combine(basePath, newFileNamae);
            StreamWriter wr = new StreamWriter(SaveFilePath);

            try
            {
                for (int i = 0; i < dt2.Columns.Count; i++)
                {
                    wr.Write(dt2.Columns[i].ToString().ToUpper() + "\t");
                }
                wr.WriteLine();

                for (int i = 0; i <(dt2.Rows.Count); i++)
                {
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        if (dt2.Rows[i][j] != null)
                        {
                            wr.Write(Convert.ToString(dt2.Rows[i][j]) + "\t");
                        }
                        else
                        {
                            wr.Write("\t");
                        }
                    }
                    wr.WriteLine();
                }
                wr.Close();
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string AppendTimeStamp(string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }

        public string GetItemPricing(float DbItemPrice, float ExcelItemPrice)
        {
            string result = "";
            if (DbItemPrice <= ExcelItemPrice)
            {
                result = "This record will keep the database price";
                return result;
            }
            else
            {
                result = "This record will be overwritten with the excel pricing";
                return result;
            }
        }
    }
}