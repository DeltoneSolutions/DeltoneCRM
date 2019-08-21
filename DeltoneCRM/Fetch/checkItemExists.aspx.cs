using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DeltoneCRM_DAL;

namespace DeltoneCRM.Fetch
{
    public partial class checkItemExists : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ItemDAL item = new ItemDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
            String ItemCode = Request.QueryString["IC"].ToString();
            Response.Write(item.verifyItemSupplierCodeExists(ItemCode));
        }
    }
}