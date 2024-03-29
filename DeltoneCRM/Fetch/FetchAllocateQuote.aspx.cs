﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchAllocateQuote : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            var rep = Request["rep"];
            var list6Month = GetLast6MonthQuote(rep);
            var str = FetchLast6monthQuotes(list6Month);
            Response.Write(str
                
                );
        }


        private List<QuoteAssign> GetLast6MonthQuote(string rep)
        {


            var comList = new List<QuoteAssign>();

            var dateLastMonth = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
            var qtCat = 0;//status new
            //            var query = @"SELECT QuoteID,QuoteByID,QuoteDateTime,QuoteBy,Total,Status,Flag FROM QUOTE 
            //                              where convert(varchar(10),QuoteDateTime, 120) >@last6month and  QuoteCategory=@qtCat ";

            var query = @"SELECT QT.QuoteID,QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,CT.ContactID,'Existing Customer' as Type," +
                    " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy  FROM dbo.Quote QT INNER JOIN " +
                    "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB' and QT.Status<>'CANCELLED'  and  convert(varchar(10),QT.QuoteDateTime, 120) > @last6month and  QuoteCategory=@qtCat " +
                     " Union SELECT QT.QuoteID, QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID , CT.ContactID,'New Customer' as Type," +
                    " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                    "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and QT.Status<>'CANCELLED' and convert(varchar(10),QT.QuoteDateTime, 120) > @last6month and  QuoteCategory=@qtCat ";

            if (rep == "0")
            {
                query = @"SELECT QT.QuoteID,QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,CT.ContactID,'Existing Customer' as Type," +
                    " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy , LOS.FirstName + ' ' + LOS.LastName as quoteAssigned FROM dbo.Quote QT INNER JOIN " +
                    "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID " +
                      " INNER JOIN dbo.QuoteAllocate QTA on  QT.QuoteID=QTA.QuoteId INNER JOIN LOGINS LOS ON QTA.UserId =LOS.LoginID  AND convert(varchar(10),QTA.ExpiryDate, 120) >= CAST(getdate() as date) " +  
                " where QT.Flag='LiveDB' " +
                     " Union SELECT QT.QuoteID, QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID , CT.ContactID,'New Customer' as Type," +
                    " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy , LOS.FirstName + ' ' + LOS.LastName as quoteAssigned FROM dbo.Quote QT INNER JOIN " +
                    "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID " +
                    " INNER JOIN dbo.QuoteAllocate QTA on  QT.QuoteID=QTA.QuoteId INNER JOIN LOGINS LOS ON QTA.UserId =LOS.LoginID  AND convert(varchar(10),QTA.ExpiryDate, 120) >= CAST(getdate() as date) " +
                   " where QT.Flag='QuoteDB' ";
            }
            else
            {
                if (rep != "-1")
                {
                    query = @"SELECT QT.QuoteID,QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,CT.ContactID,'Existing Customer' as Type," +
                     " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy, LOS.FirstName + ' ' + LOS.LastName as quoteAssigned  FROM dbo.Quote QT INNER JOIN " +
                     "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID " +
                       " INNER JOIN dbo.QuoteAllocate QTA on  QT.QuoteID=QTA.QuoteId INNER JOIN LOGINS LOS ON QTA.UserId =LOS.LoginID  AND convert(varchar(10),QTA.ExpiryDate, 120) >= CAST(getdate() as date) " +
                 " where QT.Flag='LiveDB' and QTA.UserId=@userID " +
                      " Union SELECT QT.QuoteID, QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID , CT.ContactID,'New Customer' as Type," +
                     " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy, LOS.FirstName + ' ' + LOS.LastName as quoteAssigned FROM dbo.Quote QT INNER JOIN " +
                     "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID " +
                     " INNER JOIN dbo.QuoteAllocate QTA on  QT.QuoteID=QTA.QuoteId INNER JOIN LOGINS LOS ON QTA.UserId =LOS.LoginID AND convert(varchar(10),QTA.ExpiryDate, 120) >= CAST(getdate() as date) " +
                    " where QT.Flag='QuoteDB' and QTA.UserId=@userID ";
                }

            }

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var conRep = Convert.ToInt32(rep);
                    if (conRep == -1)
                    {
                        cmd.Parameters.AddWithValue("@last6month", dateLastMonth);
                        cmd.Parameters.AddWithValue("@qtCat", qtCat);
                    }
                    else
                        if (conRep > 0)
                        {
                            cmd.Parameters.AddWithValue("@userID", conRep);
                        }


                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                 //   var cnVer= conn.ServerVersion;
                 //  var cnVerdd= conn.State;

                    ReadExecuteReader(comList, cmd, conRep);
                    conn.Close();

                }

            }


            return comList;

        }

        private static void ReadExecuteReader(List<QuoteAssign> comList, SqlCommand cmd,int repReference)
        {
            if (repReference >= 0)
            {
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            var sbj = new QuoteAssign();
                            sbj.QuoteId = Convert.ToInt32(sdr["QuoteID"].ToString());
                            sbj.QuotedById = Convert.ToInt32(sdr["QuoteByID"].ToString());
                            sbj.QuotedDate = Convert.ToDateTime(sdr["QuoteDate"].ToString()).ToShortDateString();
                            sbj.QuotedByName = sdr["QuoteBy"].ToString();
                            sbj.QuoteTotal = sdr["Total"].ToString();
                            sbj.CompanyName = sdr["CompanyName"].ToString();
                            sbj.QuotedAssignedName = sdr["quoteAssigned"].ToString();
                            sbj.ContactName = sdr["ContactName"].ToString();
                            sbj.Status = sdr["Status"].ToString();
                            sbj.CustomerType = sdr["Type"].ToString();
                            comList.Add(sbj);

                        }
                    }
                }
            }
            else
            {
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            var sbj = new QuoteAssign();
                            sbj.QuoteId = Convert.ToInt32(sdr["QuoteID"].ToString());
                            sbj.QuotedById = Convert.ToInt32(sdr["QuoteByID"].ToString());
                            sbj.QuotedDate = Convert.ToDateTime(sdr["QuoteDate"].ToString()).ToShortDateString();
                            sbj.QuotedByName = sdr["QuoteBy"].ToString();
                            sbj.QuoteTotal = sdr["Total"].ToString();
                            sbj.CompanyName = sdr["CompanyName"].ToString();
                            sbj.ContactName = sdr["ContactName"].ToString();
                            sbj.Status = sdr["Status"].ToString();
                            sbj.CustomerType = sdr["Type"].ToString();
                            comList.Add(sbj);

                        }
                    }
                }

            }
        }


        private string FetchLast6monthQuotes(List<QuoteAssign> listObj)
        {

            String strOutput = "{\"aaData\":[";

            foreach (var item in listObj)
            {
                strOutput = strOutput + "[\"" + item.QuoteId + "\"," + "\"" + item.QuotedDate + "\","
                                        + "\"" + item.CompanyName + "\","
                                        + "\"" + item.ContactName + "\","
                                          + "\"" + item.QuoteTotal + "\","
                                            + "\"" + item.QuotedByName + "\","
                                            + "\"" + item.QuotedAssignedName + "\","
                                              + "\"" + item.CustomerType + "\","
                                        + "\"" + item.Status + "\"],";
            }

            if (listObj.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }

            strOutput = strOutput + "]}";
            return strOutput;
        }

        protected class QuoteAssign
        {
            public int QuoteId { get; set; }
            public int QuotedById { get; set; }
            public string QuotedDate { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
            public string QuoteTotal { get; set; }
            public string QuotedByName { get; set; }
            public string QuotedAssignedName { get; set; }
            public string QuotedAssignedId { get; set; }
            public string CustomerType { get; set; }
            public string Status { get; set; }
        }

    }
}