using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchSalesRepCS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUserID"] == null)
                return;
            var status = Request["category"];
            Response.Write(FillAllSalesRepCS(status));
        }

        protected string FillAllSalesRepCS(string sta)
        {
            var connStr = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {
                var userID = Convert.ToInt32(Session["LoggedUserID"].ToString());
                var contactDal = new ContactDAL(connStr);
                var companyDal = new CompanyDAL(connStr);

                conn.ConnectionString = connStr;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var state = "Where rs.status <>'Y'";
                    if (sta == "co")
                        state = " Where rs.status='Y'";

                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        var strSqlContactStmt = @"SELECT rs.Id, rs.Complaint, rs.OutCome,rs.OrderId, rs.CompanyId,
                                   rs.ContactId,rs.CreatedDate,rs.Question , rs.CsType from RaiseCSSalesRep rs " + state;

                        cmd.CommandText = strSqlContactStmt;
                    }
                    else
                    {
                        state = " and rs.status <>'Y'";
                        if (sta == "co")
                            state = " and rs.status='Y'";
                        var strSqlContactStmt = @"SELECT rs.Id, rs.Complaint, rs.OutCome,rs.OrderId, rs.CompanyId, rs.ContactId,rs.CreatedDate,rs.Question , rs.CsType from 
                       RaiseCSSalesRep rs inner join Companies  c on rs.CompanyId=c.CompanyID 
                 where (rs.CreatedUserId=@userId or  c.OwnershipAdminID =@userId ) " + state;
                        cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = userID;
                        cmd.CommandText = strSqlContactStmt;
                    }


                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                string strViewQuote = "<img src='../Images/Edit.png' onclick='ViewCal(" + sdr["Id"] + ")'/>";
                               // string strdeleteCs = "<img src='../Images/error.png' onclick='DeleteCal(" + sdr["Id"] + ")'/>";
                                var complaint = sdr["Complaint"].ToString();
                                var outCome = "";
                                var question = "";
                                var type = "";
                                if (sdr["Complaint"] != DBNull.Value)
                                    outCome = sdr["OutCome"].ToString();
                                if (sdr["Question"] != DBNull.Value)
                                    question = sdr["Question"].ToString();
                                var csType = "Order";
                                  
                                if(sdr["CsType"] != DBNull.Value)
                                {
                                    var creditType=(int)CSType.CreditNote;
                                    type = sdr["CsType"].ToString();
                                    if (type == creditType.ToString())
                                        csType = "Credit";
                                }

                                var orderINfo = PopulateOrder(sdr["OrderId"].ToString(), type);
                                var contactINfo = PopulateContact(sdr["ContactId"].ToString());
                                var companyINfo = PopulateCompany(sdr["CompanyId"].ToString());

                                outCome = outCome.Replace(",", "");
                                outCome = outCome.Replace("\"", "");

                                question = question.Replace(",", "");
                                question = question.Replace("\"", "");

                                complaint = complaint.Replace(",", "");
                                complaint = complaint.Replace("\"", "");

                                strOutput = strOutput + "[\""
                                  + sdr["Id"] + "\","
                                   + "\"" + Convert.ToDateTime(sdr["CreatedDate"]).ToString("dd/MM/yyyy") + "\","
                                   + "\"" + csType + "\","
                                    + "\"" + orderINfo.OrderInvoice + "\","
                                    + "\"" + companyINfo.CompanyName + "\","
                                    + "\"" + contactINfo.Telephone + "\","
                                    + "\"" + contactINfo.Name + "\","
                                    + "\"" + complaint + "\","
                                     + "\"" + outCome + "\","
                                      + "\"" + question + "\","
                                       + "\"" + companyINfo.OwnerName + "\","
                                       + "\"" + orderINfo.InvoiceDueDate + "\","
                                      
                                            + "\"" + strViewQuote + "\"],";
                                
                                strOutput = Regex.Replace(strOutput, @"\t|\n|\r", "");
                                


                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }
                        strOutput = strOutput + "]}";
                        conn.Close();

                    }


                }

            }
            return strOutput;
        }


        protected ContactSalesRep PopulateContact(string strContactID)
        {
            var obj = new ContactSalesRep();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " SELECT * FROM dbo.Contacts WHERE ContactID=" + strContactID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            string TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                            //string MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                            //  string faxBuilder = sdr["FAX_AreaCode"].ToString() + sdr["FAX_CountryCode"] + sdr["FAX_Number"];
                            string contactBuilder = sdr["FirstName"].ToString() + " " + sdr["LastName"];

                            obj.Name = contactBuilder;
                            obj.Telephone = TeleBuilder;


                        }
                    }

                    conn.Close();
                }
            }

            return obj;

        }




        protected OrderSalesRep PopulateOrder(string strOrderID,string type)
        {
            var obj = new OrderSalesRep();

            if (!string.IsNullOrEmpty(type))
            {
                var creditType=(int)CSType.CreditNote;
                if (type == creditType.ToString())
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = " SELECT OrderID FROM dbo.CreditNotes WHERE CreditNote_ID=" + strOrderID;
                            cmd.Connection = conn;
                            conn.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    strOrderID = sdr["OrderID"].ToString();
                                }
                            }

                            conn.Close();
                        }
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " SELECT XeroInvoiceNumber, DueDate FROM dbo.Orders WHERE OrderID=" + strOrderID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            string xeroNumber = sdr["XeroInvoiceNumber"].ToString();
                            //  string MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                            //   string faxBuilder = sdr["FAX_AreaCode"].ToString() + sdr["FAX_CountryCode"] + sdr["FAX_Number"];
                            string duedate = sdr["DueDate"].ToString();

                            obj.OrderInvoice = xeroNumber;
                            obj.InvoiceDueDate = Convert.ToDateTime(duedate).ToString("dd/MM/yyyy");


                        }
                    }

                    conn.Close();
                }
            }

            return obj;

        }



        protected CompanySalesRep PopulateCompany(string strCompanyID)
        {
            var obj = new CompanySalesRep();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @" SELECT ce.CompanyName as comname ,lo.FirstName + ' ' + lo.LastName AS createdUser FROM dbo.Companies ce inner join
                                     Logins lo on ce.OwnershipAdminID=lo.LoginID WHERE CompanyID=" + strCompanyID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            string cratedUser = sdr["createdUser"].ToString();
                            //  string MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                            //   string faxBuilder = sdr["FAX_AreaCode"].ToString() + sdr["FAX_CountryCode"] + sdr["FAX_Number"];
                            string coName = sdr["comname"].ToString();

                            obj.CompanyName = coName;
                            obj.OwnerName = cratedUser;


                        }
                    }

                    conn.Close();
                }
            }

            return obj;

        }




        protected class ContactSalesRep
        {
            public string Name { get; set; }
            public string Telephone { get; set; }
        }

        protected class OrderSalesRep
        {
            public string OrderInvoice { get; set; }
            public string InvoiceDueDate { get; set; }
        }

        protected class CompanySalesRep
        {
            public string CompanyName { get; set; }
            public string OwnerName { get; set; }
        }
    }
}