using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Warehouse.Fetch
{
    public partial class fetchitemsinshelf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            var result = GetAllAShelfItems();
            Response.Write(result);
        }


        private string GetAllAShelfItems()
        {
            string strOutput = "{\"aaData\":[";
            WShelfItemDetailsDAL shelfDAl = new WShelfItemDetailsDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            var listShelfItems = shelfDAl.GetAllShelfItems();
            foreach (var item in listShelfItems)
            {
               
                    String strEditOrder = "<img src='../Images/Edit.png'  onclick='openEditItems(" + item.Id.ToString() + ");'>";
                    String strEditQty = "<img src='../Images/Edit.png'  onclick='openQtyItems(" + item.ItemId.ToString() + "," + item.Quantity + ");'>";
                    strOutput = strOutput + "[\"" + item.SupplierItemCode + "\","
                         + "\"" + item.Brand + "\","
                          + "\"" + item.Name + "\","
                           + "\"" + item.Boxing + "\","
                          + "\"" + item.Description + "\","
                            + "\"" + item.COG + "\","
                            + "\"" + item.RepUnitPrice + "\","
                             + "\"" + item.OEMCode + "\","
                                                + "\"" + item.LocationColumnName + "  -  " + item.LocationRowNumber + "\","
                                         
                                           
                                             + "\"" + item.Quantity + "\","
                                                + "\"" + strEditQty + "\","
                                       + "\"" + strEditOrder + "\"],";
                
            }
            if (listShelfItems.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }

            strOutput = strOutput + "]}";

            return strOutput;
        }

        private string AllShelfs(IList<WShelf> listObj)
        {
            string strOutput = "{\"aaData\":[";
            foreach (var item in listObj)
            {
                strOutput = strOutput + "[\"" + item.Id + "\","
                                           + "\"" + item.ColumnName + "\","
                                        + "\"" + item.RowNumber + "\"],";
            }

            if (listObj.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }

            strOutput = strOutput + "]}";

            return strOutput;

        }
    }
}