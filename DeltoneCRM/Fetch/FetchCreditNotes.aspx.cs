using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeltoneCRM_DAL;
using System.Configuration;

namespace DeltoneCRM.Fetch
{
    public partial class FetchCreditNotes : System.Web.UI.Page
    {

        CreditNotesDAL notesDAL;
        protected void Page_Load(object sender, EventArgs e)
        {

            String strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            notesDAL = new CreditNotesDAL(strConnString);
            String strCompanyID = Request.QueryString["companyid"].ToString();

            Response.Write(notesDAL.getCreditNotes(Int32.Parse(strCompanyID)));

        }


       


    }
}