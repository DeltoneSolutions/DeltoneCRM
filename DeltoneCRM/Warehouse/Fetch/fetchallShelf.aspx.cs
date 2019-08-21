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
    public partial class fetchallShelf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WareShelfDAL shelfDAl = new WareShelfDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            var listAll = shelfDAl.GetAllShelfs();
            Response.Write(AllShelfs(listAll));
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