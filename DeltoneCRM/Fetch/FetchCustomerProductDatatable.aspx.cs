using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchCustomerProductDatatable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sDate = Request["startdate"];
            var eDate = Request["enddate"];
            Response.Write(GetALLOrderedOrders(sDate, eDate));
        }

        protected string GetALLOrderedOrders(string startDate, string endDate)
        {
            DataTable dt = new DataTable();
            String strOutput = "{\"data\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"select co.CompanyID, co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.CreatedDateTime,ord.OrderID,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,ord.AccountOwner
                                          from Ordered_Items OT INNER JOIN
	                          
	                                  Items IT ON IT.ItemID = OT.ItemCode 
	                                   INNER JOIN  Orders ord ON ord.OrderID = OT.OrderID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompanyID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
                                  order by CompanyName asc";

                    if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(startDate))
                    {
                        var comString = @"select co.CompanyID,co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.CreatedDateTime,ord.OrderID,it.SupplierItemCode,it.Description, OT.Quantity as itemQty ,OT.UnitAmount,OT.CreatedBy,ord.AccountOwner
                                          from Ordered_Items OT INNER JOIN
	                          
	                                  Items IT ON IT.ItemID = OT.ItemCode 
	                                   INNER JOIN  Orders ord ON ord.OrderID = OT.OrderID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompanyID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID 
                                  WHERE  (ord.CreatedDateTime >=@startDa and ord.CreatedDateTime <=@endtDa) order by CompanyName asc";
                        cmd.CommandText = comString;
                        var st = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
                        var end = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
                        cmd.Parameters.AddWithValue("@startDa", st);
                        cmd.Parameters.AddWithValue("@endtDa", end);

                    }

                    cmd.Connection = conn;
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                   dt.Load(dr);

                    //if (dt.Rows.Count > 0)
                    //{
                    //    foreach (DataRow drow in dt.Rows)
                    //    {

                    //        strOutput = strOutput + "[\""
                    //           + drow["CompanyName"].ToString() + "\","
                    //            + "\"" + (drow["contactName"].ToString()) + "\","
                    //             + "\"" + (drow["tlenumber"].ToString()) + "\","
                    //             + "\"" + (drow["OrderID"].ToString()) + "\","
                    //              + "\"" + (drow["OrderedDateTime"].ToString()) + "\","
                    //               + "\"" + (drow["SupplierItemCode"].ToString()) + "\","
                    //                 + "\"" + (drow["Description"].ToString()) + "\","
                    //                     + "\"" + drow["itemQty"].ToString() + "\"],";
                    //    }
                    //    int Length = strOutput.Length;
                    //    strOutput = strOutput.Substring(0, (Length - 1));
                    //}



                    //using (SqlDataReader sdr = cmd.ExecuteReader())
                    //{
                    //    if (sdr.HasRows)
                    //    {

                    //        while (sdr.Read())
                    //        {
                    //            var Link = "<input name='selectchk'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";
                    //            strOutput = strOutput + "[\""
                    //                 + Link + "\","
                    //                + sdr["CompanyName"].ToString() + "\","
                    //                 + "\"" + (sdr["contactName"].ToString()) + "\","
                    //                  + "\"" + (sdr["tlenumber"].ToString()) + "\","

                    //                   + "\"" + (sdr["CreatedDateTime"].ToString()) + "\","
                    //                    + "\"" + (sdr["SupplierItemCode"].ToString()) + "\","
                    //                      + "\"" + (sdr["Description"].ToString()) + "\","
                    //                       + "\"" + (sdr["itemQty"].ToString()) + "\","
                    //                        + "\"" + (sdr["UnitAmount"].ToString()) + "\","
                    //                         + "\"" + (sdr["CreatedBy"].ToString()) + "\","
                    //                          + "\"" + sdr["AccountOwner"].ToString() + "\"],";

                    //        }
                    //        int Length = strOutput.Length;
                    //        strOutput = strOutput.Substring(0, (Length - 1));
                    //    }

                    //}
                    //strOutput = strOutput + "]}";
                    //conn.Close();
                }

            }


            var comListObj = new List<CompanyProduct>();

            comListObj = (from DataRow row in dt.Rows

                          select new CompanyProduct
                          {
                              CompanyId = row["CompanyID"].ToString(),
                              CompanyName = row["CompanyName"].ToString(),
                              contactName = row["contactName"].ToString(),
                              CreatedDateTime = row["CreatedDateTime"].ToString(),
                              Description = row["Description"].ToString(),
                              itemQty = Convert.ToInt32(row["itemQty"].ToString()),
                              OrderID = row["OrderID"].ToString(),
                              SupplierItemCode = row["SupplierItemCode"].ToString(),
                              tlenumber = row["tlenumber"].ToString(),
                              CreatedBy = row["CreatedBy"].ToString(),
                              UnitAmount = row["UnitAmount"].ToString(),
                              AccountOwner = row["AccountOwner"].ToString(),

                          }).ToList();
            //return comListObj;

            foreach (var item in comListObj)
            {

                var Link = "<input name='selectchk'  value='" + item.CompanyId + "' type='checkbox' />";
                            strOutput = strOutput + "[\""
                                 + Link + "\","
                                + item.CompanyName + "\","
                                 + "\"" + item.contactName + "\","
                                  + "\"" + item.tlenumber + "\","

                                   + "\"" + item.CreatedDateTime + "\","
                                    + "\"" + item.SupplierItemCode + "\","
                                      + "\"" + item.Description + "\","
                                       + "\"" + item.itemQty + "\","
                                        + "\"" + item.UnitAmount + "\","
                                         + "\"" + item.CreatedBy + "\","
                                          + "\"" + item.AccountOwner + "\"],";

                       
                      //  int Length = strOutput.Length;
                      //  strOutput = strOutput.Substring(0, (Length - 1));
            }

            if(comListObj.Count () > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }

            strOutput = strOutput + "]}";

            return strOutput;

        }

        protected class CompanyProduct
        {
            public string CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string contactName { get; set; }
            public string tlenumber { get; set; }
            public string OrderID { get; set; }
            public string CreatedDateTime { get; set; }
            public string SupplierItemCode { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
            public string UnitAmount { get; set; }
            public string AccountOwner { get; set; }
            public int itemQty { get; set; }
        }


        public string ReturnCompanies(string rep)
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {



                    if (rep == "0")
                        cmd.CommandText = @"SELECT   CP.CompanyID , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,
                                 lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID   inner join Logins lc on lc.LoginID=CP.OwnershipAdminID   
                              ";
                    else
                        cmd.CommandText = @"SELECT   CP.CompanyID , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,
                                lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID  
                            WHERE CP.OwnershipAdminID = " + rep + "  ";
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
                                var repName = sdr["createdUser"].ToString();
                                var active = sdr["Active"].ToString();
                                var locked = "N";
                                var IsSupperAcco = "N";
                                if (sdr["IsSupperAcount"] != DBNull.Value)
                                {
                                    if (Convert.ToBoolean(sdr["IsSupperAcount"].ToString()) == true)
                                        IsSupperAcco = "Y";
                                }
                                if (sdr["Hold"] != DBNull.Value)
                                {
                                    locked = sdr["Hold"].ToString();
                                }


                                var TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                                strOutput = strOutput + "[\"" + comId + "\"," + "\"" + CoName + "\","
                                        + "\"" + contactName + "\","
                                        + "\"" + Convert.ToDateTime(sdr["CreatedDateTime"]).ToString("dd-MMM-yyyy") + "\","
                                          + "\"" + TeleBuilder + "\","
                                           + "\"" + repName + "\","
                                            + "\"" + active + "\","
                                             + "\"" + locked + "\","
                                        + "\"" + IsSupperAcco + "\"],";
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


        public string ReturnCompaniesForLock(string rep)
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT   CP.CompanyID , CP.CompanyName, CP.Active,CP.Hold FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID   inner join Logins lc on lc.LoginID=CP.OwnershipAdminID   
                              ";

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

                                var active = sdr["Active"].ToString();
                                var locked = "N";

                                if (sdr["Hold"] != DBNull.Value)
                                {
                                    locked = sdr["Hold"].ToString();
                                }

                                var Link = "<input name='selectchk'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var LinkLocked = "<input id='" + sdr["CompanyID"].ToString() + "' name='selectlock'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                if (locked == "Y")
                                    LinkLocked = "<input name='selectlock' checked='true' value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                strOutput = strOutput + "[\"" + Link + "\"," + "\"" + comId + "\","
                                               + "\"" + CoName + "\","
                                            + "\"" + active + "\","
                                             + "\"" + locked + "\","
                                        + "\"" + LinkLocked + "\"],";
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
    }
}