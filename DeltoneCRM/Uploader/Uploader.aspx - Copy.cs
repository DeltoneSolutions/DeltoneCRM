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
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
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
            foreach (var row in rowValues)
            {
                Response.Write(Environment.NewLine);
                for (int i = 0; i <= row.ItemArray.GetUpperBound(0); i++)
                {
               
                    alSupplierItemCode.Add(row["SupplierItemCode"].ToString());
                    alItemPrice.Add(row["UnitPrice"].ToString());
                    
                }
                Response.Write(row["SupplierItemCode"].ToString());
                checkIfItemExists(row["SupplierItemCode"].ToString(), row["UnitPrice"].ToString());
            }
            
        }


        public void checkIfItemExists(string SIC, string ItemPrice)
        {
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
                                Response.Write("Item Exsist");
                                string MUP = sdr["ManagerUnitPrice"].ToString();
                                GetItemPricing(float.Parse(MUP), float.Parse(ItemPrice));
                            }
                        }
                        else
                        {
                            Response.Write("Item Doesn't Exsist <br/>");
                        }
                    }
                }

                conn.Close();

            }

            
              
        }

        public void GetItemPricing(float DbItemPrice, float ExcelItemPrice)
        {
            if (DbItemPrice <= ExcelItemPrice)
            {
                Response.Write(" This record will keep the database price <br/>");
            }
            else
            {
                Response.Write(" This record will be overwritten with the excel pricing <br/>");
            }
        }
    }
}