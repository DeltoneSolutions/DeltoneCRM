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
    public partial class FetchContactandCompanyByCreditId : System.Web.UI.Page
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
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var orderdal = new CreditNotesDAL(connString);
            var creditObj = orderdal.getCreditNoteObj(Convert.ToInt32(orderId));
            obj.ContactId = creditObj.ContactId;
            obj.CompanyId =creditObj.CompID;

            return obj;
        }

        public class OrderAndCompanyContact
        {
            public string ContactId { get; set; }
            public string CompanyId { get; set; }
        }
    }
}