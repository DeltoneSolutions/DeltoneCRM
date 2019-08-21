using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeltoneCRM_DAL;
using Xero.Api.Core;
using Xero.Api.Example;
using Xero.Api.Core.Model;
using XeroApi;
using System.Configuration;

namespace DeltoneCRM
{
    public partial class ViewCreditNote : System.Web.UI.Page
    {
        String CREDITNOTEID = String.Empty;
        String COMPANYID = String.Empty;
        String STATUS = String.Empty;

        CreditNotesDAL creditDAL;
        protected void Page_Load(object sender, EventArgs e)
        {    
              String strConnectionString=ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
              creditDAL = new CreditNotesDAL(strConnectionString);
              String[] arrCreditNote;

             if(!String.IsNullOrEmpty(Request.QueryString["CreditNoteID"]))
             {
                 CREDITNOTEID = Request.QueryString["CreditNoteID"].ToString();
                 hdnCreditNoteID.Value = CREDITNOTEID.ToString();
                 hdnEditCreditNote.Value = creditDAL.getSavedCreditNote(Int32.Parse(CREDITNOTEID));
                 hdnEditCreditNoteItems.Value = creditDAL.getSavedCreditNoteItems(Int32.Parse(CREDITNOTEID));
                 arrCreditNote = creditDAL.getSavedCreditNote(Int32.Parse(CREDITNOTEID)).ToString().Split(':');
                 STATUS = arrCreditNote[8].ToString();


             }
             if(!String.IsNullOrEmpty(Request.QueryString["CompanyID"]))
             {
                 COMPANYID = Request.QueryString["CompanyID"].ToString();
             }
            
                 if (!String.IsNullOrEmpty(STATUS) && STATUS.Equals("PENDING"))
                 {
                     btnPublish.Visible = true;
                 }

                


        }

        protected void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ConpanyInfo.aspx?companyid=" + COMPANYID);
  
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {

            creditDAL.UpdateCreditNoteStatus( Int32.Parse(CREDITNOTEID));
            //Create the Xero Entry for the Credit Notes
            Response.Redirect("~/ConpanyInfo.aspx?companyid=" + COMPANYID);


        }

    }
}