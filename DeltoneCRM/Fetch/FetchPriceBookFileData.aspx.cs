using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchPriceBookFileData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnAttendanceFileData());
        }

        private string ReturnAttendanceFileData()
        {
            var strOutput = "{\"aaData\":[";

            if (Session["fileUploadpricebook"] != null)
            {
                var listattendanceList = Session["fileUploadpricebook"] as List<DeltoneCRM_DAL.ItemDAL.SupplierItem>;
               

                foreach (var item in listattendanceList)
                {
                    strOutput = strOutput + "[\"" + item.SupplierItemCode + "\","
             + "\"" + item.Description + "\","
             + "\"" + item.PriceUpdate + "\","
              + "\"" + item.DSB + "\","
              + "\"" + item.ResellPrice + "\","
             + "\"" + item.PrinterCompatibility + "\"],";
                }


                if (listattendanceList.Count() > 0)
                {
                    int Length = strOutput.Length;
                    strOutput = strOutput.Substring(0, (Length - 1));
                }
            }

            strOutput = strOutput + "]}";
            return strOutput;
        }
    }
}