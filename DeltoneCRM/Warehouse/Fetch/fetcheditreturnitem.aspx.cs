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

namespace DeltoneCRM.Warehouse.Fetch
{
    public partial class fetcheditreturnitem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var jsonString = String.Empty;

            Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }
            //  var serJsonDetails = (DisplayWShelfItemDetails)javaScriptSerializer.Deserialize(jsonString, typeof(DisplayWShelfItemDetails));
            // CreateShelfItems(serJsonDetails);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var serJsonDetails = (EditLoadCreditItem)javaScriptSerializer.Deserialize(jsonString, typeof(EditLoadCreditItem));
            var result = LoadEditShelfItems(serJsonDetails.ProductCode);
            Response.Headers.Add("Content-type", "application/json");
            var jsonConver = javaScriptSerializer.Serialize(result);
            Response.Write(jsonConver);
        }

        public DisplayWShelfItemDetails LoadEditShelfItems(string suppItemCode)
        {
            var obj = new DisplayWShelfItemDetails();
            WShelfItemDetailsDAL shelfDAl = new WShelfItemDetailsDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
            var supplierIdForInhouse = 8;

            var itemHouse = shelfDAl.GetItemBySupplierIdAndItemCode(suppItemCode.Trim(), supplierIdForInhouse);
            WareShelfDAL shelRow = new WareShelfDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
           


            if (itemHouse.ItemID > 0)
            {

                var listItmesShelf = shelfDAl.GetAllShelfItems();
                var itemSelected = (from sh in listItmesShelf where sh.ItemId == itemHouse.ItemID select sh).ToList();
                if (itemSelected.Count() > 0)
                {
                    obj.ItemId = itemSelected[0].ItemId;
                    obj.Boxing = listItmesShelf[0].Boxing;
                    obj.Brand = listItmesShelf[0].Brand;
                    obj.COG = listItmesShelf[0].COG;
                    obj.Damaged = listItmesShelf[0].Damaged;
                    obj.Description = listItmesShelf[0].Description;
                    obj.DSB = listItmesShelf[0].DSB;
                    obj.Height = listItmesShelf[0].Height;
                    obj.Id = listItmesShelf[0].Id;
                    obj.Length = listItmesShelf[0].Length;
                    obj.LocationColumnName = listItmesShelf[0].LocationColumnName;
                    obj.LocationId = listItmesShelf[0].LocationId;
                    obj.LocationRowNumber = listItmesShelf[0].LocationRowNumber;
                    obj.ManagerUnitPrice = listItmesShelf[0].ManagerUnitPrice;
                    obj.Name = listItmesShelf[0].Name;
                    obj.Notes = listItmesShelf[0].Notes;
                    obj.OEMCode = listItmesShelf[0].OEMCode;
                    obj.Quantity = listItmesShelf[0].Quantity;
                    obj.RepUnitPrice = listItmesShelf[0].RepUnitPrice;
                    obj.SupplierItemCode = listItmesShelf[0].SupplierItemCode;

                }
            }

            return obj;

        }


    }
    public class EditLoadCreditItem
    {
        public string ProductCode { get; set; }
    }

}