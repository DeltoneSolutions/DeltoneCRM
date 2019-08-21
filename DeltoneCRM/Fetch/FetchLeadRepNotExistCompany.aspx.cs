using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchLeadRepNotExistCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var rep = Session["LoggedUserID"].ToString();
            Response.Write(ReturnCompanies(rep));
        }


        public string ReturnCompanies(string rep)
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {


                    cmd.CommandText = @"SELECT Distinct CP.CompanyName, lo.Notes, CT.FirstName + ' ' + CT.LastName AS FullName,'Dim Lead' as ownercompany,
                                  CP.CompanyID ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode ,Hold
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email,lo.CreatedDate as companyAllocatedDate,lo.ExpiryDate, DATEDIFF(DAY,CAST(getdate() as date), convert(varchar(10),lo.ExpiryDate, 120)) as daysRemain FROM dbo.CompaniesNotExists CP
                               join dbo.ContactsNotExists CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompanyNotExists lo on CP.CompanyID=lo.CompanyId And CP.OwnershipAdminID <> " + rep + " And lo.UserId =" + rep + "  and convert(varchar(10),lo.ExpiryDate, 120) >= CAST(getdate() as date) " +
                    "   ";


                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var comId = sdr["CompanyID"].ToString();
                                var CoName = sdr["CompanyName"].ToString();
                                var contactName = sdr["FullName"].ToString();
                                var TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                                var MobileBuilder = "";//sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                                var Email = sdr["Email"].ToString();
                                var ViewEdit = "<img src='../Images/Edit.png'  onclick='OpenCompany(" + sdr["CompanyID"].ToString() + ");'>";
                                var message = "";
                                var notes = "";
                                var lastOrderDate = LoadLastOrderDateForCompany(comId);

                                var areaCode = sdr["DEFAULT_AreaCode"].ToString();

                                var defauNumber = sdr["DEFAULT_Number"].ToString();
                                var constructArea = areaCode;
                                // constructArea = sdr["DEFAULT_AreaCode"].ToString().Replace("(", string.Empty).Trim();
                                // constructArea = sdr["DEFAULT_AreaCode"].ToString().Replace(")", string.Empty);

                                if (constructArea == "02" || constructArea == "03" || constructArea == "07" || constructArea == "08")
                                {
                                    constructArea = constructArea + " " + defauNumber;
                                }

                                else
                                {
                                    constructArea = constructArea + defauNumber;
                                }

                                var orderString = LoadAllOrderForCompany(comId);

                                var ownercompany = sdr["ownercompany"].ToString() + " Lead ";
                                
                                if (sdr["Notes"] != DBNull.Value)
                                    notes = sdr["Notes"].ToString();

                                var res = CheckCompanyAllocated(comId);
                                if (!string.IsNullOrEmpty(res))
                                    ownercompany = res + " Lead";

                             

                                notes = Regex.Replace(notes, @"\t|\n|\r", "");
                                notes = notes.Replace(@"\", "-");
                                notes = notes.Replace(@"/", "-");
                                notes = notes.Replace("\"", "");
                                var pos = notes.LastIndexOf("--");
                               
                                var subNotes = notes;
                                if (subNotes.Length > 50)
                                    subNotes = subNotes.Substring(0, 50);
                                notes = Regex.Replace(notes, "<.*?>", String.Empty);
                                subNotes = subNotes.Replace("<br/>", "");
                                notes = notes.Replace("<br/>", "");

                                strOutput = strOutput + "[\"" + comId + "\","
                                     + "\"" + CoName + "\","
                                        + "\"" + ownercompany + "\","
                                        + "\"" + contactName + "\","
                                        + "\"" + constructArea + "\","
                                        + "\"" + lastOrderDate + "\","
                                          + "\"" + Convert.ToDateTime(sdr["companyAllocatedDate"]).ToString("dd-MMM-yyyy HH:mm") + "\","
                                           + "\"" + sdr["daysRemain"].ToString() + "\","

                                               + "\"" + subNotes + "\","
                                               + "\"" + notes + "\","
                                               + "\"" + ViewEdit + "\","
                                        + "\"" + orderString + "\"],";

                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }

                    }

                    conn.Close();

                }

            }
            strOutput = strOutput + "]}";
            return strOutput;
        }

        private string LoadLastOrderDateForCompany(string comId)
        {
            string resultString = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var query = @"
                Select Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].OrdersNotExists ORC
                 Where  ORC.CompanyID=@comId
				  Order By OrderID Desc";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@comId", comId);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                resultString = Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy");
                            }

                        }

                    }

                    conn.Close();
                }

            }

            return resultString;
        }


        private string LoadAllOrderForCompany(string comId)
        {
            string resultString = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var query = @"Select OrderedDateTime,Total,XeroInvoiceNumber from  [dbo].OrdersNotExists where companyId={0} and Status<>@status order by OrderedDateTime desc";

                    //                    if (!string.IsNullOrEmpty(starDate) && !string.IsNullOrEmpty(endDate))
                    //                    {
                    //                        query = @"Select OrderedDateTime,Total,XeroInvoiceNumber from  [dbo].orders where companyId={0} and 
                    //                             Status<>@status and (OrderedDateTime >=@startDa and OrderedDateTime <=@endtDa)  order by OrderedDateTime desc";
                    //                        var st = Convert.ToDateTime(starDate).ToString("yyyy-MM-dd");
                    //                        var end = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");

                    //                        cmd.Parameters.AddWithValue("@startDa", st);
                    //                        cmd.Parameters.AddWithValue("@endtDa", end);
                    //                    }

                    query = string.Format(query, comId);
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@status", "CANCELLED");
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {


                                if (resultString == "")
                                    resultString = Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy") + "," + sdr["Total"] + "," + sdr["XeroInvoiceNumber"];
                                else
                                {
                                    resultString = resultString + ";";
                                    resultString = resultString + Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy") + "," + sdr["Total"] + "," + sdr["XeroInvoiceNumber"];
                                }
                            }

                        }

                    }

                    conn.Close();
                }

            }

            return resultString;
        }
        private string CheckCompanyAllocated(string comId)
        {
            var comName = "";

            var query = @"SELECT COUNT(lo.CompanyId) as countCom  FROM LeadCompanyNotExists lo  where  
                              lo.CompanyID=@comID ";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@comID", comId);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                var counter = Convert.ToInt32(sdr["countCom"].ToString());
                                if (counter > 1)
                                {
                                    comName = GetLatestDateCompanyCreated(comId);


                                }


                            }
                        }
                    }
                    conn.Close();

                }

            }

            return comName;

        }

        private string GetLatestDateCompanyCreated(string comId)
        {
            var query = @"SELECT lo.CreatedDate as createddate  FROM LeadCompanyNotExists lo  where lo.CompanyID=@comID ORDER BY lo.Id DESC";
            string comName = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@comID", comId);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                comName = Convert.ToDateTime(sdr["createddate"].ToString()).ToString("MMMM");
                                return comName;


                            }
                        }
                    }
                    conn.Close();

                }
            }

            return comName;
        }
    }
}