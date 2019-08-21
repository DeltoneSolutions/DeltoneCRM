using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Data;
using DeltoneCRM_DAL;


namespace DeltoneCRM.Reports.Queries
{
    public partial class getAccountOwner : System.Web.UI.Page
    {

        LoginDAL logindal = new LoginDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        CompanyDAL companydal = new CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        ContactDAL contactdal = new ContactDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        OrderDAL orderdal = new OrderDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            String strOutput = String.Empty;

            String OperatorName = String.Empty;
            String OperatorID = Request.QueryString["repname"];
            OperatorName = logindal.getLoginNameFromID(OperatorID);
            List<String> AllAccountsByOwner = getAllCompaniesOwnedByOwner(OperatorID);
            foreach (String SingleCompanyID in AllAccountsByOwner)
            {
                strOutput = strOutput + OperatorName + "|";
                String CompanyName = companydal.getCompanyNameByID(SingleCompanyID);
                strOutput = strOutput + CompanyName + "|";
                String ContactID = contactdal.getContactByCompanyBasedOnLastOrder(Int32.Parse(SingleCompanyID));
                String ContactName = contactdal.getContactFullNameBasedOnContactID(ContactID);
                strOutput = strOutput + ContactName + "|";
                String LastOrderDate = orderdal.getLastOrderDateForCompany(SingleCompanyID);
                strOutput = strOutput + LastOrderDate + "~";
            }

            int Length = strOutput.Length;
            if (Length != 0)
            {
                strOutput = strOutput.Substring(0, (Length - 1));
            }

            Response.Write(strOutput);
        }

        protected List<String> getAllCompaniesOwnedByOwner(String LID)
        {
            List<String> CompanyList = new List<String>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CompanyID FROM dbo.Companies WHERE OwnershipAdminID = " + LID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CompanyList.Add(sdr["CompanyID"].ToString());
                        }

                    }
                }
            }

            return CompanyList;
        }
    }
}