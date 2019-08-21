using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Warehouse.Process
{
    public partial class addshelf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String rowNumber = Request.Form["RowNumber"].ToString().Trim();
            String columnName = Request.Form["ColumnName"].ToString().Trim();

            WareShelfDAL shelfDAl = new WareShelfDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            var checkAldearyExistObj = shelfDAl.ISRowAndColumnExist(columnName, rowNumber);

            if (checkAldearyExistObj.Id > 0)
            {
                Response.Write("-1");
            }
            else
            {
               // var  vsoid;
                var obj = new WShelf();
                obj.ColumnName = columnName;
                obj.RowNumber = rowNumber;
                shelfDAl.CreateShelf(obj);
                Response.Write("1");
            }
        }
    }
}