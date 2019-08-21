using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DeltoneCRM.DataHandlers
{
    /// <summary>
    /// Summary description for PriceBookHandle
    /// </summary>
    public class PriceBookHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var listattendanceList = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            if (context.Session["fileUploadpricebook"] != null)
            {
                 listattendanceList = context.Session["fileUploadpricebook"] as List<DeltoneCRM_DAL.ItemDAL.SupplierItem>;

            }

            var result = new
            {
                iTotalRecords = listattendanceList.Count(),
                iTotalDisplayRecords = 25,
                aaData = listattendanceList
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 500000000;
            context.Response.Write(js.Serialize(result));
        }

        private void SetFilePriceBook()
        {

           
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}