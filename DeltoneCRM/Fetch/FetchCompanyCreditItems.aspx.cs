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
    public partial class FetchCompanyCreditItems : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            var rep = Request["cId"];
            var listOrderItems = GetCompayItemsCredit(rep);
            var str = FetchAllCompanyItems(listOrderItems);
            Response.Write(str   );
        }


        private IList<OrderItems> GetCompayItemsCredit(string comId)
        {
            var listItems = new List<OrderItems>();
            var dateLast24Month = DateTime.Now.AddMonths(-24).ToString("yyyy-MM-dd");

            string query = @"SELECT  orls.CreditNote_ID
      ,orls.DateCreated ,
	  oitls.SupplierCode,
	  oitls.Quantity,
	  oitls.Description,
	  oitls.UnitAmount,
       oitls.SupplierName,
     oitls.COG,
orls.XeroCreditNoteID,
  orls.CreditNoteReason  
  FROM [dbo].[CreditNotes] orls
   join [dbo].[CreditNote_Item] oitls
	  on orls.CreditNote_ID=
	  oitls.CreditNoteID
	  where orls.CompID=@comId and  convert(varchar(10),orls.DateCreated, 120) >= @dateLast24Month
	  group by orls.CreditNote_ID,orls.DateCreated, oitls.Quantity,
	  oitls.SupplierCode,
	  oitls.Description,
	  oitls.UnitAmount, 
oitls.SupplierName,
oitls.COG,
orls.XeroCreditNoteID ,
 orls.CreditNoteReason ";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Parameters.AddWithValue("@dateLast24Month", dateLast24Month);
                    cmd.Parameters.AddWithValue("@comId", comId);


                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();

                    ReadExecuteReaderCredit(listItems, cmd);
                    conn.Close();

                }

            }
            var orderListItems = GetCompayItems(comId);
            listItems.AddRange(orderListItems);
            return listItems;
        }

        private static void ReadExecuteReaderCredit(List<OrderItems> comList, SqlCommand cmd)
        {

            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        var sbj = new OrderItems();
                        sbj.OrderId = (sdr["CreditNote_ID"].ToString());

                        sbj.OrderDate = Convert.ToDateTime(sdr["DateCreated"].ToString()).ToString("dd-MMM-yyyy");
                        sbj.SupplierCode = (sdr["SupplierCode"].ToString());
                        sbj.ItemDescription = (sdr["Description"].ToString());
                        sbj.Qty = sdr["Quantity"].ToString();
                        sbj.UnitPrice = sdr["UnitAmount"].ToString();
                        sbj.XeroId = sdr["XeroCreditNoteID"].ToString();
                        sbj.SupplierName = sdr["SupplierName"].ToString();
                        sbj.Cog = sdr["COG"].ToString();
                        sbj.XeroId = sbj.XeroId + "  -  " + (sdr["CreditNoteReason"].ToString());
                        sbj.Type = "2";
                        comList.Add(sbj);

                    }
                }
            }

        }

        private IList<OrderItems> GetCompayItems(string comId)
        {
            var listItems = new List<OrderItems>();
            var dateLast24Month = DateTime.Now.AddMonths(-24).ToString("yyyy-MM-dd");

            string query = @"SELECT  orls.OrderID
      ,orls.OrderedDateTime ,
	  oitls.SupplierCode,
	  oitls.Quantity,
	  oitls.Description,
	  oitls.UnitAmount,
       oitls.SupplierName,
     oitls.COGamount,
orls.XeroInvoiceNumber,
orls.Status
  FROM [dbo].[Orders] orls
   join [dbo].[Ordered_Items] oitls
	  on orls.OrderID=
	  oitls.OrderID
	  where orls.CompanyID=@comId and  convert(varchar(10),orls.OrderedDateTime, 120) >= @dateLast24Month
	  group by orls.OrderID,orls.OrderedDateTime, oitls.Quantity,
	  oitls.SupplierCode,
	  oitls.Description,
	  oitls.UnitAmount, 
oitls.SupplierName,
oitls.COGamount,
orls.XeroInvoiceNumber,
orls.Status";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Parameters.AddWithValue("@dateLast24Month", dateLast24Month);
                    cmd.Parameters.AddWithValue("@comId", comId);


                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();

                    ReadExecuteReader(listItems, cmd);
                    conn.Close();

                }

            }

            return listItems;
        }

        private static void ReadExecuteReader(List<OrderItems> comList, SqlCommand cmd)
        {

            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        var sbj = new OrderItems();
                        sbj.OrderId = (sdr["OrderID"].ToString());
                        sbj.OrderDate = Convert.ToDateTime(sdr["OrderedDateTime"].ToString()).ToString("dd-MMM-yyyy");
                        sbj.SupplierCode = (sdr["SupplierCode"].ToString());
                        sbj.ItemDescription = (sdr["Description"].ToString());
                        sbj.Qty = sdr["Quantity"].ToString();
                        sbj.UnitPrice = sdr["UnitAmount"].ToString();
                        sbj.XeroId = sdr["XeroInvoiceNumber"].ToString();
                        sbj.SupplierName = sdr["SupplierName"].ToString();
                        sbj.Cog = sdr["COGamount"].ToString();
                        sbj.Type = "1";
                        if (sdr["Status"].ToString() == "CANCELLED")
                        {
                            sbj.Type = "3";
                            sbj.XeroId = sdr["XeroInvoiceNumber"].ToString() + " - CANCELLED"  ;
                        }
                        comList.Add(sbj);

                    }
                }
            }

        }



        private string FetchAllCompanyItems(IList<OrderItems> listObj)
        {

            String strOutput = "{\"aaData\":[";

            foreach (var item in listObj)
            {
                strOutput = strOutput + "[\"" + item.OrderId + "\","
                                        + "\"" + item.XeroId + "\","
                                         + "\"" + item.OrderDate + "\","
                                         + "\"" + item.SupplierName + "\","
                                         + "\"" + item.SupplierCode + "\","
                                        + "\"" + item.ItemDescription + "\","
                                        + "\"" + item.Qty + "\","
                                         + "\"" + item.UnitPrice + "\","
                                          + "\"" + item.Cog + "\","
                                        + "\"" + item.Type + "\"],";
            }

            if (listObj.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }

            strOutput = strOutput + "]}";
            return strOutput;
        }


        protected class OrderItems
        {
            public string OrderId { get; set; }
            public string OrderDate { get; set; }
            public string SupplierName { get; set; }
            public string SupplierCode { get; set; }
            public string ItemDescription { get; set; }
            public string Qty { get; set; }
            public string UnitPrice { get; set; }
            public string XeroId { get; set; }
            public string Cog { get; set; }
            public string Type { get; set; }
        }
    }
}