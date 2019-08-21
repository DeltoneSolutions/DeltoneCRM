using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class SalesRepCS : System.Web.UI.Page
    {
        public string SampleVariable = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;


            if (Session["USERPROFILE"].ToString() == "ADMIN")
            {
                SampleVariable = "true";
            }

        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("dashboard1.aspx");



        }

        [System.Web.Services.WebMethod]
        public static RaiseSalesCsUI ReadSalesRepCS(int csId)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var obj = new RaiseSalesCsUI();
            obj.Id = csId;
            if (csId > 0)
            {

                var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());

                obj = new RaiseCsSalesRepDAL(cs).ReadSalesRepCsById(csId);

            }

            return obj;

        }
        [System.Web.Services.WebMethod]
        public static void UpdateRepCS(RaiseSalesCsUI obj)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            //var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            var change = "Status Not changed";
            var preval = "";
            if (obj.Status == false)
                preval = "Not Completed";
            if (obj.Status)
                change = "Completed";

            CreateAuditRecord(obj.Id, change, preval, obj);

            new RaiseCsSalesRepDAL(cs).UpdateSalesRepCsById(obj);
        }

        [System.Web.Services.WebMethod]
        public static void DeleteRecordCS(int csId)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            //var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            DeleteAuditRecord(csId);

            new RaiseCsSalesRepDAL(cs).DeleteRecordCS(csId);


        }

        private static void DeleteAuditRecord(int rsId)
        {
            var columnName = "All Column";
            var talbeName = "RaiseCS";
            var ActionType = "DELETED";
            int primaryKey = rsId;
            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());

            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var obj = new RaiseCsSalesRepDAL(CONNSTRING).ReadSalesRepCsById(rsId);


            var newvalues = "Customer Service  Id " + rsId + " Customer Service Deleted .Issue "
                + obj.Complaint + ". OutCome " + obj.OutCome + " . Question" + obj.Question;



            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var previousValues = "";
            var strCompanyID = new RaiseCsSalesRepDAL(CONNSTRING).GetCompanyIDByRaisedId(rsId);

            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));
        }


        private static void CreateAuditRecord(int rsId, string changed, string preValus, RaiseSalesCsUI newObj)
        {
            var columnName = "All Column";
            var talbeName = "RaiseCS";
            var ActionType = "Changed";
            int primaryKey = rsId;
            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());

            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var obj = new RaiseCsSalesRepDAL(CONNSTRING).ReadSalesRepCsById(rsId);

            var change = "Status Not changed";

            if (newObj.Status == false)
                change = "Not Completed";
            if (newObj.Status)
                change = "Completed"; 

            var newvalues = "Customer Service  Id " + rsId + " Customer Service Updated. Status " + changed + " . Issue "
              + newObj.Complaint + ". OutCome " + newObj.OutCome + " . Question " + newObj.Question;

          

       

            preValus = "Customer Service  Id " + rsId + "Status " + change + " . Issue "
            + obj.Complaint + ". OutCome " + obj.OutCome + " . Question " + obj.Question;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var previousValues = preValus;
            var strCompanyID = new RaiseCsSalesRepDAL(CONNSTRING).GetCompanyIDByRaisedId(rsId);

            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));
        }
    }
}