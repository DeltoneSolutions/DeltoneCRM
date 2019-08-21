using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using DeltoneCRM.Classes;

namespace DeltoneCRM
{
    /// <summary>
    /// Summary description for ItemsDataHandler
    /// </summary>
    public class ItemsDataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //int displayLength = int.Parse(context.Request["iDisplayLength"]);
            //int displayStart = int.Parse(context.Request["iDisplayStart"]);
            //int sortCol = int.Parse(context.Request["iSortCol_0"]);
            //string sortDir = context.Request["sSortDir_0"];
            //string search = context.Request["sSearch"];

            //string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            //List<AllItems> listItems = new List<AllItems>();
            //int filteredCount = 0;

            //using (SqlConnection conn = new SqlConnection(cs))
            //{
            //    SqlCommand cmd = new SqlCommand("getAllItems", conn);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    SqlParameter paramDisplayLength = new SqlParameter()
            //    {
            //        ParameterName = "@DisplayLength",
            //        Value = displayLength
            //    };
            //    cmd.Parameters.Add(paramDisplayLength);

            //    SqlParameter paramDisplayStart = new SqlParameter()
            //    {
            //        ParameterName = "@DisplayStart",
            //        Value = displayStart
            //    };
            //    cmd.Parameters.Add(paramDisplayStart);

            //    SqlParameter paramSortcol = new SqlParameter()
            //    {
            //        ParameterName = "@SortCol",
            //        Value = sortCol
            //    };
            //    cmd.Parameters.Add(paramSortcol);

            //    SqlParameter paramSortDir = new SqlParameter()
            //    {
            //        ParameterName = "@SortDir",
            //        Value = sortDir
            //    };
            //    cmd.Parameters.Add(paramSortDir);

            //    SqlParameter paramSearchString = new SqlParameter()
            //    {
            //        ParameterName = "@Search",
            //        Value = search
            //    };
            //    cmd.Parameters.Add(paramSearchString);

            //    conn.Open();
            //    SqlDataReader sdr = cmd.ExecuteReader();

            //    while (sdr.Read())
            //    {
            //        AllItems item = new AllItems();
            //        item.ItemID = Convert.ToInt32(sdr["ItemID"]);
            //        filteredCount = Convert.ToInt32(sdr["TotalCount"]);
            //        item.SupplierName = sdr["SupplierName"].ToString();
            //        item.ProductCode = sdr["SupplierItemCode"].ToString();
            //        item.Description = sdr["Description"].ToString();
            //        item.COG = float.Parse(sdr["COG"].ToString());
            //        item.ManagerUnitPrice = float.Parse(sdr["ManagerUnitPrice"].ToString());
            //        item.Active = sdr["Active"].ToString();
            //        item.Qty = getItemsQuantity(item.ItemID.ToString());
            //        item.ViewEdit = "<img src='../Images/Edit.png'  onclick='Edit(" + sdr["ItemID"] + ")'/>";
            //        listItems.Add(item);
            //    }
            //    conn.Close();
            //}

            var reId = "";
            var canCallmodel = false;
            if (context.Request.QueryString["r"] != null)
            {
                reId = context.Request.QueryString["r"];
                canCallmodel = true;
            }

            var listItems = GetAllItems();
            var result = new
            {
                iTotalRecords = listItems.Count(),
                iTotalDisplayRecords = 25,
                aaData = listItems
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 500000000;
            context.Response.Write(js.Serialize(result));
        }

        private List<AllItems> GetAllItems()
        {


            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            List<AllItems> listItems = new List<AllItems>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @" Select ItemID, SupplierName,SupplierItemCode,Description,DSB,PrinterCompatibility,COG,ManagerUnitPrice,Items.Active,Quantity from Items 
                            INNER JOIN Suppliers ON Items.SupplierID=Suppliers.SupplierID ";

                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                AllItems item = new AllItems();
                                item.ItemID = Convert.ToInt32(sdr["ItemID"]);
                                item.SupplierName = sdr["SupplierName"].ToString();
                                item.ProductCode = sdr["SupplierItemCode"].ToString();
                                item.Description = sdr["Description"].ToString();
                                if (sdr["PrinterCompatibility"] != DBNull.Value)
                                    item.PrinterCompatibility = sdr["PrinterCompatibility"].ToString();
                                if (sdr["DSB"] != DBNull.Value)
                                    item.DSB = float.Parse(sdr["DSB"].ToString());
                                item.COG = float.Parse(sdr["COG"].ToString());
                                item.ManagerUnitPrice = float.Parse(sdr["ManagerUnitPrice"].ToString());
                                item.Active = sdr["Active"].ToString();
                                item.Qty = 0;
                                if (sdr["Quantity"] != DBNull.Value)
                                    item.Qty = Convert.ToInt32(sdr["Quantity"].ToString());
                                item.ViewEdit = "<img src='../Images/Edit.png'  onclick='Edit(" + sdr["ItemID"] + ")'/>";
                                listItems.Add(item);
                            }
                        }
                    }
                    conn.Close();
                }
            }

            return listItems;
        }

        private int getItemsTotalCount()
        {
            int TotalItemCount = 0;
            String cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Items", conn);
                conn.Open();

                TotalItemCount = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            return TotalItemCount;
        }


        private int getItemsQuantity(string ItemCode)
        {
            int TotalItemCount = 0;
            String cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                var str = @"SELECT Quantity FROM dbo.Items where ItemID=" + ItemCode;
                SqlCommand cmd = new SqlCommand(str, conn);
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Quantity"] != DBNull.Value)
                            TotalItemCount = Convert.ToInt32(sdr["Quantity"].ToString());
                    }
                }
                conn.Close();
            }

            return TotalItemCount;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}