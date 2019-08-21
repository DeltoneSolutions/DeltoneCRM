using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Warehouse.Process
{
    public partial class addshelfitems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var jsonString = String.Empty;

            Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var serJsonDetails = (DisplayWShelfItemDetails)javaScriptSerializer.Deserialize(jsonString, typeof(DisplayWShelfItemDetails));
            CreateShelfItems(serJsonDetails);
            Response.Headers.Add("Content-type", "application/json");
            Response.Write("1");
        }

        public void CreateShelfItems(DisplayWShelfItemDetails obj)
        {
            if (obj != null)
            {
                var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                var itemDal = new ItemDAL(connString);
                var listItems = obj.DisplayWItemQuantity;
                WShelfItemDetailsDAL shelfDAl = new WShelfItemDetailsDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

                obj.CreatedUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());

                var supplierIdForInhouse = 30; //TW-INHOUSE
                var splitChar = obj.SupplierItemCode.Trim().Substring(0,2);
                if (splitChar.ToUpper() == "DS")
                    supplierIdForInhouse = 8;

                var itemHouse = shelfDAl.GetItemBySupplierIdAndItemCode(obj.SupplierItemCode.Trim(), supplierIdForInhouse);
                if (string.IsNullOrEmpty(obj.Length))
                    obj.Length = "0";
                if (string.IsNullOrEmpty(obj.Weight))
                    obj.Weight = "0";
                if (string.IsNullOrEmpty(obj.Height))
                    obj.Height = "0";
                if (string.IsNullOrEmpty(obj.Width))
                    obj.Width = "0";

                if (itemHouse.ItemID > 0)
                {
                    var cog = float.Parse(obj.COG);
                    var managerPrice = float.Parse(obj.RepUnitPrice);
                    int qty = 0;
                    if (!string.IsNullOrEmpty(obj.Quantity))
                        qty = Convert.ToInt32(obj.Quantity);
                    double? dsb = null;
                    if (!string.IsNullOrEmpty(obj.DSB))
                        dsb = Convert.ToDouble(obj.DSB);
                    var active = "Y";
                    if (obj.ActiveSts == false)
                        active = "N";
                    shelfDAl.UpdateItem(itemHouse.ItemID, obj.Description.Trim(), cog, managerPrice, supplierIdForInhouse,
                        obj.SupplierItemCode.Trim(), active, "Y", "N", qty, dsb, obj.OEMCode);
                    var itemIdINShelf = shelfDAl.CheckItemIdExistOrNotInShelf(itemHouse.ItemID);
                    obj.ItemId = itemHouse.ItemID;
                    if (itemIdINShelf > 0)
                    {
                       
                        obj.Id = itemIdINShelf;
                        shelfDAl.UpdateWShelfItemDetailsAndWItemQuantity(obj);
                        
                    }
                    else
                    {
                        shelfDAl.CreateWShelfItemDetailsAndWItemQuantity(obj);
                    }
                }
                else
                {
                    int qty = 0;
                    var cog = float.Parse(obj.COG);
                    var managerPrice = float.Parse(obj.RepUnitPrice);
                    if (!string.IsNullOrEmpty(obj.Quantity))
                        qty = Convert.ToInt32(obj.Quantity);
                    double? dsb = null;
                    if (!string.IsNullOrEmpty(obj.DSB))
                        dsb = Convert.ToDouble(obj.DSB);
                    shelfDAl.AddNewItem(supplierIdForInhouse.ToString(), obj.SupplierItemCode.Trim(), obj.Description.Trim(), 
                        cog, managerPrice, "Y", "N", qty, dsb, obj.OEMCode);
                    var user = "SYSTEM";
                    var itemId = shelfDAl.GetLastInsertedItemId(obj.SupplierItemCode, supplierIdForInhouse, user);
                    //insert item and create itemshelf
                    obj.ItemId = itemId;
                    shelfDAl.CreateWShelfItemDetailsAndWItemQuantity(obj);

                }

               
            }
        }
    }
}