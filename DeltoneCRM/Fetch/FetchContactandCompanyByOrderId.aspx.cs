using DeltoneCRM_DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchContactandCompanyByOrderId : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var orderId = Request.QueryString["orderlinePage"].ToString();
            var result = GetCompanyContact(orderId);
            Response.Write(JsonConvert.SerializeObject(result));

        }

        public OrderAndCompanyContact GetCompanyContact(string orderId)
        {
            var obj = new OrderAndCompanyContact();
           var connString= ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
           var orderdal = new OrderDAL(connString);

           obj.ContactId = orderdal.getContactPersonID(Convert.ToInt32(orderId));
           obj.CompanyId = orderdal.getComapnyID(Convert.ToInt32(orderId));

           return obj;
        }

        public class OrderAndCompanyContact
        {
            public string ContactId { get; set; }
            public string CompanyId { get; set; }
        }
    }
}