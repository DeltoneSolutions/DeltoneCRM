using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using DeltoneCRM_DAL;

namespace DeltoneCRM.Manage.Process
{
    public partial class ProcessEditItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ItemDAL itemdal = new ItemDAL(strConnString);
            int ItemID = Int32.Parse(Request.Form["ItemID"].ToString());
            String SupplierItemCode = Request.Form["SupplierItemCode"].Trim().ToString();
            String Description = Request.Form["Description"].Trim().ToString();
            float COG = float.Parse(Request.Form["COG"].ToString());
            float ManagedPrice = float.Parse(Request.Form["ManagedUnitPrice"].ToString());
            int SupplierID = Int32.Parse(Request.Form["SupplierID"].ToString());
            string ActInact = Request.Form["ActInact"].ToString();
            String BestPrice = Request.Form["BestPrice"].ToString();
            String Faulty = Request.Form["Faulty"].ToString();

            string finalActInact = "";
            String finalBestPrice = String.Empty;
            String finalFaulty = String.Empty;

            int quantity = 0;
            string qty = Request.Form["Quantity"].ToString();
            if (!string.IsNullOrEmpty(qty))
                quantity = Convert.ToInt32(qty);

            double? dsb = null;
            var dsbVal = Request.Form["DSBPrice"].ToString();
            if (!string.IsNullOrEmpty(dsbVal))
                dsb = Convert.ToDouble(dsbVal);

            if (ActInact == "false")
            {
                finalActInact = "N";
            }
            else
            {
                finalActInact = "Y";
            }

            if (BestPrice == "true")
            {
                finalBestPrice = "Y";
            }
            else
            {
                finalBestPrice = "N";
            }

            if (Faulty == "true")
            {
                finalFaulty = "Y";
            }
            else
            {
                finalFaulty = "N";
            }

            var Orderid = 0;
            var strCompanyID = -1; // For product we do not use companyid refer -1 to identify the type for items

            var newvalues = "";
            var oldvalues = "";

            // newvalues = " PRODUCT CODE  : " + SupplierItemCode;
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;


            var itemObj = PopulateForObject(ItemID.ToString(), connString);
            if (itemObj.description != Description)
            {
                newvalues = newvalues + " Description: " + Description;
                oldvalues = oldvalues + " Description: " + itemObj.description;
            }
            if (Decimal.Compare(Convert.ToDecimal(itemObj.cogPrice), Convert.ToDecimal(COG)) != 0)
            {
                newvalues = newvalues + " COST: " + COG;
                oldvalues = oldvalues + " COST: " + itemObj.cogPrice;
            }

            if (Decimal.Compare(Convert.ToDecimal(itemObj.resellPrice), Convert.ToDecimal(ManagedPrice)) != 0)
            {
                newvalues = newvalues + " RESELL PRICE: " + ManagedPrice;
                oldvalues = oldvalues + " RESELL PRICE: " + itemObj.resellPrice;
            }

            if (itemObj.active.Trim() != finalActInact)
            {
                newvalues = newvalues + " Active: " + finalActInact;
                oldvalues = oldvalues + " Active: " + itemObj.active;
            }

            if (itemObj.bestPrice.Trim() != finalBestPrice)
            {
                newvalues = newvalues + " BEST PRICE:" + finalBestPrice;
                oldvalues = oldvalues + " BEST PRICE: " + itemObj.bestPrice;
            }

            //  newvalues = newvalues + " Supplier Id: " + SupplierID;
            if (itemObj.quantity != quantity.ToString())
            {
                newvalues = newvalues + " Quantity: " + quantity;
                oldvalues = oldvalues + " Quantity: " + itemObj.quantity;
            }
            if (dsb != null)
            {
                if (Decimal.Compare(Convert.ToDecimal(itemObj.DSBPrice), Convert.ToDecimal(dsb)) != 0)
                {

                    newvalues = newvalues + " DSB: " + dsb;
                    oldvalues = oldvalues + " DSB: " + itemObj.DSBPrice;
                }
            }

            //var oldString = Populate(ItemID.ToString(), connString);

            var result = itemdal.UpdateItem(ItemID, Description, COG, ManagedPrice, SupplierID,
                SupplierItemCode, finalActInact, finalBestPrice, finalFaulty, quantity, dsb);

            Orderid = ItemID;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connString;

            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());

            var columnName = "Product Items";
            var talbeName = "Product Items";
            var ActionType = "Updated Product Items";
            int primaryKey = Orderid;
            var lastString = newvalues;
            if (newvalues != "")
                new DeltoneCRM_DAL.CompanyDAL(connString).CreateActionONAuditLog(oldvalues, lastString, loggedInUserId, conn, 0,
       columnName, talbeName, ActionType, primaryKey, strCompanyID);


            Response.Write(result);

        }

        protected string Populate(String strItemID, string strConnString)
        {

            ItemDAL itemdal = new ItemDAL(strConnString);
            String strItem = itemdal.getItem(Int32.Parse(strItemID));
            String[] arrItem = strItem.Split(':');
            var obj = new EditItemAudit();

            var comString = "";

            comString = " PRODUCT CODE  : " + arrItem[0].ToString();
            comString = comString + " Description: " + arrItem[1].ToString();
            comString = comString + " COST: " + arrItem[2].ToString();
            comString = comString + " RESELL PRICE: " + arrItem[4].ToString();
            comString = comString + " Active: " + arrItem[8].ToString();
            comString = comString + " BEST PRICE:" + arrItem[9].ToString();
            comString = comString + " SupplierName: " + arrItem[7].ToString();
            comString = comString + " Quantity: " + arrItem[11].ToString();
            comString = comString + " DSB: " + arrItem[12].ToString();

            return comString;
        }

        protected EditItemAudit PopulateForObject(String strItemID, string strConnString)
        {

            ItemDAL itemdal = new ItemDAL(strConnString);
            String strItem = itemdal.getItem(Int32.Parse(strItemID));
            String[] arrItem = strItem.Split(':');
            var obj = new EditItemAudit();

            obj.itemSupplierCode = arrItem[0].ToString();
            obj.description = arrItem[1].ToString();
            obj.cogPrice = arrItem[2].ToString();
            obj.resellPrice = arrItem[4].ToString();
            obj.active = arrItem[8].ToString();
            obj.bestPrice = arrItem[9].ToString();
            obj.txtSupplier = arrItem[7].ToString();
            obj.quantity = arrItem[11].ToString();
            obj.DSBPrice = arrItem[12].ToString();

            return obj;
        }

        protected class EditItemAudit
        {
            public string itemSupplierCode { get; set; }
            public string description { get; set; }
            public string cogPrice { get; set; }
            public string resellPrice { get; set; }
            public string suplierID { get; set; }
            public string itemID { get; set; }
            public string active { get; set; }
            public string bestPrice { get; set; }
            public string txtSupplier { get; set; }
            public string quantity { get; set; }
            public string DSBPrice { get; set; }
        }
    }
}