using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class updatecompanyxeroreference : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string companyname = Request.Form["Companyname"].ToString();
            string companyxero = Request.Form["companyXero"].ToString();
            UpdateCompanyReference(companyname, companyxero);
            Response.Write("1");
        }

        public void UpdateCompanyReference(string companyName,string xeroReference)
        {
            companyName = companyName.Trim();
            xeroReference = xeroReference.Trim();
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);
            var comId = 0;
            DeltoneCRMDAL dal = new DeltoneCRMDAL();
            var comIdByName = dal.GetCompanyIdByCompanyName(companyName);
            if (!string.IsNullOrEmpty(comIdByName))
            {
                comId = Convert.ToInt32(comIdByName);
                if (comId > 0)
                    cdal.UpdateWithXeroDetails(comId, xeroReference);
            }
        }
    }
}