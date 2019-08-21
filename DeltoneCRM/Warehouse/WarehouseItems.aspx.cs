using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Warehouse
{
    public partial class WarehouseItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadShelfDetails();
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("..\\dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {

            Response.Redirect("..\\Manage\\ManageCentral.aspx");
        }

        protected void ShelfView(object sender, EventArgs e)
        {
            Response.Redirect("WarehouseShelfManage.aspx");
        }

         //this is called when a event is clicked on the calendar
        [System.Web.Services.WebMethod(true)]
        public static void UpdateQuantity(int itemId,int Quantity)
        {
            WShelfItemDetailsDAL shelfDAl = new WShelfItemDetailsDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
            var PrevioudQty = getItemIdByItemID(itemId.ToString());
            shelfDAl.UpdateItemQuantity(itemId, Quantity);

            UpdateProductAudit(PrevioudQty,Quantity, itemId);

        }

        private static int getItemIdByItemID(string itemId)
        {
            var strOutput = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Modification done here Add Supplier Name  //s.SupplierName //Write a Stored Proc for this
                    cmd.CommandText = "SELECT I.Quantity from dbo.Items I  WHERE I.ItemID=@supp";
                    cmd.Parameters.AddWithValue("@supp", itemId);
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["Quantity"] != DBNull.Value)
                                strOutput = Convert.ToInt32(sdr["Quantity"].ToString());
                        }


                    }
                    conn.Close();
                }
            }

            return strOutput;

        }


        private static void UpdateProductAudit(int databaseqty, int enteredQty, int itemID)
        {
          
            var oldvalues = " Quantity : " + databaseqty ;
            var newValues =  " Entered Quantity : " + enteredQty ;
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connString;

            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());

            var columnName = "Product Items";
            var talbeName = "Product Items";
            var ActionType = "Updated Product Items";
            int primaryKey = itemID;
            var strCompanyID = -1; // For product we do not use companyid refer -1 to identify the type for items
            new DeltoneCRM_DAL.CompanyDAL(connString).CreateActionONAuditLog(oldvalues, newValues, loggedInUserId, conn, 0,
       columnName, talbeName, ActionType, primaryKey, strCompanyID);


        }


        protected void LoadShelfDetails()
        {
            WareShelfDAL shelfDAl = new WareShelfDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            var listAll = shelfDAl.GetAllShelfs();
            var listRow = new List<LocationManageSearchRow>();
         
            foreach (var item in listAll)
            {
                var obj = new LocationManageSearchRow();
                obj.Id = item.Id.ToString();
                obj.RowName = item.RowNumber;
                obj.ColName = item.ColumnName;
                listRow.Add(obj);

            }

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var serJsonDetails = javaScriptSerializer.Serialize(listRow);

            rowlocationhidden.Value = serJsonDetails;

        }
    }

    public class LocationManageSearchRow
    {
        public string Id { get; set; }
        public string RowName { get; set; }
        public string ColName { get; set; }
    }
}