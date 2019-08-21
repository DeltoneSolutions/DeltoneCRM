using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using DeltoneCRM_DAL;

namespace DeltoneCRM.Fetch
{
    public partial class FetchItembyInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            Response.Write(LoadItems(Request.QueryString["term"].ToString()));
        }

        //This Method Load the Items given by StrQuery
        protected String LoadItems(String strQuery)
        {
            String strOutput = "[";


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Modification done here Add Supplier Name  //s.SupplierName //Write a Stored Proc for this
                    cmd.CommandText = @"SELECT TOP 25 I.SupplierID, I.ItemID,I.SupplierItemCode,s.SupplierName,I.OEMCode,
                                        I.Description,I.COG,I.ManagerUnitPrice,I.RepUnitPrice, I.OriComp, I.PriceLock, 
                                       I.ReportedFaulty,I.Quantity from dbo.Items I ,dbo.Suppliers s 
                                     
                                       WHERE (I.SupplierID=S.SupplierID and I.Active='Y' )  AND 
                                     (I.SupplierItemCode like '%" + strQuery + "%' OR I.OEMCode like '%" + strQuery + "%' OR I.Description like '%" + strQuery + "%' OR  s.SupplierName like '%" + strQuery + "%') "; 

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var boxing = GetItemBoxingByItemId(sdr["ItemID"].ToString());
                            strOutput = strOutput + "{\"ItemID\":\"" + sdr["ItemID"] + "\",\"SupplierItemCode\":\"" 
                                + sdr["SupplierItemCode"] + "\",\"OEMCode\":\"" + sdr["OEMCode"] + "\",\"Description\":\""
                                + sdr["Description"] + "\",\"COG\":\"" + sdr["COG"] + "\",\"ManagerUnitPrice\":\"" 
                                + sdr["ManagerUnitPrice"] + "\",\"RepUnitPrice\":\"" + sdr["RepUnitPrice"] + "\",\"SupplierName\":\"" 
                                + sdr["SupplierName"] + "\",\"BestPrice\":\"" + sdr["PriceLock"] 
                                + "\",\"ReportFault\":\"" + sdr["ReportedFaulty"]
                                + "\",\"Quantity\":\"" + sdr["Quantity"]
                                 + "\",\"Boxing\":\"" + boxing
                                + "\"},";         
                        }
                        
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));

                    }
                    conn.Close();
                }
            }

            strOutput = strOutput + "]";
            return strOutput;

        }
        //End Load Items given by StrQuery


        protected string GetItemBoxingByItemId(string itemId)
        {
            var itemBoxing = "";

            WShelfItemDetailsDAL shelfDAl = new WShelfItemDetailsDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

          var res=  shelfDAl.GetShelfItembyItemId(Convert.ToInt32(itemId));
          if (res.Id > 0)
              itemBoxing = res.Boxing;


            return itemBoxing;
        }
    }
}