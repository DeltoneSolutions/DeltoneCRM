using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchAllQuotes : System.Web.UI.Page
    {
        string status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

            status = Request["category"];

            Response.Write(ReturnQuotes());
        }

        protected string ReturnQuotes()
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        var querystring = "SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,CT.Email , QT.CallBackDate,CT.ContactID,'Existing Customer' as Type," +
                        " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy,QT.QuoteCategory FROM dbo.Quote QT INNER JOIN " +
                        "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB'  {0} " +
                         " Union SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,CT.Email ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                        " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy,QT.QuoteCategory FROM dbo.Quote QT INNER JOIN " +
                        "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  {1}";



                        GetTabSelectionQuery(cmd, querystring);


                    }
                    else
                    {
                        var createdBy = Session["LoggedUser"].ToString();
                        var querystring = "SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID,CT.Email ,QT.CallBackDate, CT.ContactID,'Existing Customer' as Type," +
                       " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy,QT.QuoteCategory FROM dbo.Quote QT INNER JOIN " +
                       " dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB' and  QT.QuoteBy=@createdBy {0} " +
                        " Union SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,CT.Email,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                       "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy ,QT.QuoteCategory FROM dbo.Quote QT INNER JOIN " +
                       " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and  QT.QuoteBy=@createdBy {1} ";
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);
                        GetTabSelectionQuery(cmd, querystring);
                    }

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                String strViewQuote = "<img src='../Images/Edit.png' onclick='Edit(" + sdr["QuoteID"] + ", " + sdr["CompanyID"] + ", " + sdr["ContactID"] + ")'/>";

                                var total = String.Format("{0:C2}", float.Parse(sdr["Total"].ToString()));
                                if (status == "cb") //Call back
                                {
                                 
                                    var callBackdate = "";
                                    if (sdr["callBackdate"] != DBNull.Value)
                                        callBackdate = sdr["callBackdate"].ToString();
                                    //Modification Done here
                                    strOutput = strOutput + "[\"" + sdr["QuoteID"] + "\"," + "\"" + Convert.ToDateTime(sdr["QuoteDate"].ToString()).ToString("dd-MMM-yyyy") + "\","
                                        + "\"" + sdr["CompanyName"].ToString() + "\","
                                        + "\"" + sdr["ContactName"].ToString() + "\","
                                        + "\"" + callBackdate + "\","
                                          + "\"" + total + "\","

                                        + "\"" + sdr["QuoteBy"].ToString() + "\","
                                         + "\"" + sdr["Type"].ToString() + "\","


                                        + "\"" + sdr["Status"].ToString() + "\","
                                        + "\"" + strViewQuote + "\"],";
                                }
                                else
                                {
                                    var sold = "N";
                                    if (sdr["QuoteCategory"].ToString() == "3")
                                        sold = "Y";
                                    //Modification Done here
                                    strOutput = strOutput + "[\"" + sdr["QuoteID"] + "\"," + "\"" + Convert.ToDateTime(sdr["QuoteDate"].ToString()).ToString("dd-MMM-yyyy") + "\","
                                        + "\"" + sdr["CompanyName"].ToString() + "\","
                                        + "\"" + sdr["ContactName"].ToString() + "\","
                                          + "\"" + sdr["Email"].ToString() + "\","
                                           
                                        + "\"" + total + "\","
                                        + "\"" + sold + "\","
                                         + "\"" + sdr["QuoteBy"].ToString() + "\","
                                         + "\"" + sdr["Type"].ToString() + "\","


                                        + "\"" + sdr["Status"].ToString() + "\","
                                        + "\"" + strViewQuote + "\"],";
                                }


                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }


                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }

            }
            return strOutput;
        }

        private void GetTabSelectionQuery(SqlCommand cmd, string querystring)
        {
            var secondArg = "  order by QuoteDate desc";
            if (status == null || status == "" || status == "ini")
                cmd.CommandText = string.Format(querystring, string.Empty, secondArg);
            else
                if (status == "ne") //New
                {
                    secondArg = "  and QT.QuoteCategory=0 order by QuoteDate desc";
                    var firstarg = " and QT.QuoteCategory=0 ";
                    cmd.CommandText = string.Format(querystring, firstarg, secondArg);
                }
                else
                    if (status == "cb") //Call back
                    {
                        secondArg = " and QT.QuoteCategory=1 order by QuoteDate desc";
                        var firstarg = " and QT.QuoteCategory=1 ";
                        cmd.CommandText = string.Format(querystring, firstarg, secondArg);
                    }
                    else
                        if (status == "me") //May be
                        {
                            secondArg = "  and QT.QuoteCategory=2 order by QuoteDate desc";
                            var firstarg = " and QT.QuoteCategory=2 ";
                            cmd.CommandText = string.Format(querystring, firstarg, secondArg);
                        }
                        else
                            if (status == "no")  //No
                            {
                                secondArg = "  and QT.QuoteCategory=4 order by QuoteDate desc";
                                var firstarg = " and QT.QuoteCategory=4 ";
                                cmd.CommandText = string.Format(querystring, firstarg, secondArg);
                            }
                            else
                                if (status == "so")   //Sold
                                {
                                    secondArg = "  and QT.QuoteCategory=3 order by QuoteDate desc";
                                    var firstarg = " and QT.QuoteCategory=3";
                                    cmd.CommandText = string.Format(querystring, firstarg, secondArg);
                                }
                                else
                                    if (status == "existC")   //existing customer
                                    {
                                        secondArg = "  and QT.Flag='LiveDB' order by QuoteDate desc";
                                        var firstarg = " and  QT.Flag='LiveDB' ";
                                        cmd.CommandText = string.Format(querystring, firstarg, secondArg);
                                    }
                                    else
                                        if (status == "newc")   //new customer
                                        {
                                            secondArg = "  and QT.Flag='QuoteDB' order by QuoteDate desc";
                                            var firstarg = " and QT.Flag='QuoteDB' ";
                                            cmd.CommandText = string.Format(querystring, firstarg, secondArg);
                                        }

        }


    }
}