using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using DeltoneCRM_DAL;
using System.Data.SqlTypes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Collections;
using XeroApi.Model;
using XeroApi.Model.Reporting;
using XeroApi;
using XeroApi.OAuth;
using System.Security.Cryptography.X509Certificates;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth.Signing;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Core;
using Xero.Api.Example.Applications;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Serialization;
using DeltoneCRM_DAL;


namespace DeltoneCRM.process
{
    public partial class ProcessCancelCreditNote : System.Web.UI.Page
    {
        String strConnSitring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        OrderDAL orderdal;
       
        XeroIntergration xero;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.Form["creditnoteid"]))
            {
                int OrderID = Int32.Parse(Request.Form["creditnoteid"].ToString());
                CreditNotesDAL creditdal = new CreditNotesDAL(strConnSitring);
                CommissionDAL dal_Commision = new DeltoneCRM_DAL.CommissionDAL(strConnSitring);

                String pre_status = creditdal.getCurrentCreditNoteStatus(OrderID); //Fetch the PRevious Status 
                String strCreditNoteGuid = creditdal.getCreditNote_XeroGuid(OrderID);//Fetch the credit note Xero Guid

                String Result = String.Empty;

                if (!String.IsNullOrEmpty(strCreditNoteGuid))
                {

                    xero = new XeroIntergration();
                    Repository repos = xero.CreateRepository();
                    XeroApi.Model.CreditNote cancelCreditNote = xero.CancelCreditNote(OrderID, repos, pre_status, strCreditNoteGuid);
                    String connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    //CANCEL THE CREDIT NOTE IN DELTONE SYSTEM
                    Result = (cancelCreditNote != null) ? "SUCCESS" : "ERROR";
                    var previousValues = pre_status;
                    if (Result.Equals("SUCCESS"))
                    {
                        var columnName = "Status";
                        var talbeName = "CreditNote";
                        var ActionType = "CANCELLED";
                        int primaryKey = OrderID;

                        var orderIdForCreditNote = new CreditNotesDAL(connectionstring).getOrderIDFromCreditID(OrderID);

                        var newvalues = " CreditNote Id " + OrderID + " :Credit Ortder Status changed from " + previousValues + " to CANCELLED";



                        var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = connectionstring;
                        var strCompanyID = new CreditNotesDAL(connectionstring).getCompanyIDFromCreditID(OrderID);

                        new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
                      columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));

                        creditdal.CancelCreditNote(OrderID);


                        dal_Commision.RemoveCommissionEntry(OrderID,"CREDITNOTE");


                    }
 
                }
                else
                {
                    Result = "ERROR";
                }
                Response.Write(Result);
            }

        }


    }
}