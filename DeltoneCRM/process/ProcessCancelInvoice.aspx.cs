using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using DeltoneCRM_DAL;
//Modified here SK Cancel Xero Invoice
using System.Data.SqlTypes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Collections;
using XeroApi.Model;
using XeroApi.Model.Reporting;
using XeroApi;
using XeroApi.OAuth;
using System.Security.Cryptography.X509Certificates;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth.Signing;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Core;
using Xero.Api.Example.Applications;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Serialization;
using DeltoneCRM_DAL;

namespace DeltoneCRM.process
{
    public partial class ProcessCancelInvoice : System.Web.UI.Page
    {

        String strConnSitring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        OrderDAL orderdal;
        XeroIntergration xero;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Request.Form["OrderID"]))
            {
                int OrderID =  Int32.Parse(Request.Form["OrderID"].ToString());
                OrderDAL dal = new OrderDAL(strConnSitring);
                CommissionDAL dal_commission = new DeltoneCRM_DAL.CommissionDAL(strConnSitring);
                String pre_status = dal.getCurrentOrderStatus(OrderID);
                 
                orderdal = new OrderDAL(strConnSitring);
                String output = orderdal.CancelOrder(OrderID);
                if (Int32.Parse(output)>0)
                {
                    xero = new XeroIntergration();
                    Repository repos = xero.CreateRepository();
                    String strOrderGuid = orderdal.getOrderGuid(OrderID);
                    XeroApi.Model.Invoice cancelInvoice = xero.CancelInvoice(OrderID,repos, pre_status, strOrderGuid);
                    if (cancelInvoice != null)
                    {
                    }
                }

                output = dal_commission.RemoveCommissionEntry(OrderID,"ORDER");

                var orderDtsNumber = orderdal.getXeroDTSID(OrderID);
                UpdateInHouseCancelRecord(OrderID, orderDtsNumber);

                Response.Write(orderdal.CancelOrder(OrderID));
            }

        }


        private void UpdateInHouseCancelRecord(int orderId, string orderNumberDts)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            var orderedItemDetails = GetORderItemDetails(orderId);
            foreach (var item in orderedItemDetails)
            {
                if (item.ItemSuppierName == "INHOUSE" || item.ItemSuppierName == "TW-INHOUSE")
                {
                    var qtyFromDataBase = getQuantityItem(item.ItemSupplierCode);
                    //if (qtyFromDataBase >= qtyEntered)
                    //{
                    var NeedToUpdate = qtyFromDataBase + item.ItemQty;
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
                    String strSQLUpdateStmt = "update Items set Quantity=@Quantity, AlterationDate=CURRENT_TIMESTAMP where SupplierItemCode=@repCode";
                    SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
                    cmd2.Parameters.AddWithValue("@Quantity", NeedToUpdate);

                    cmd2.Parameters.AddWithValue("@repCode", item.ItemSupplierCode);

                    try
                    {

                        conn.Open();
                        cmd2.ExecuteNonQuery().ToString();
                        conn.Close();
                        UpdateProductAudit(qtyFromDataBase, NeedToUpdate, item.ItemQty, item.ItemSupplierCode, orderNumberDts);
                    }
                    catch (Exception ex)
                    {
                        if (conn != null) { conn.Close(); }

                    }
                }

            }
        }

        private List<itemOrdered> GetORderItemDetails(int orderId)
        {
            var listitme = new List<itemOrdered>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Modification done here Add Supplier Name  //s.SupplierName //Write a Stored Proc for this
                    cmd.CommandText = "SELECT I.Quantity,I.SupplierName,I.Quantity,I.SupplierCode from dbo.Ordered_Items I  WHERE I.OrderID=@orderId";
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var obj = new itemOrdered();
                            if (sdr["Quantity"] != DBNull.Value)
                                obj.ItemQty = Convert.ToInt32(sdr["Quantity"].ToString());
                            obj.ItemSuppierName = sdr["SupplierName"].ToString();
                            obj.ItemSupplierCode = sdr["SupplierCode"].ToString();
                            listitme.Add(obj);
                        }


                    }
                    conn.Close();
                }
            }

            return listitme;

        }

        private int getQuantityItem(string rep)
        {
            var strOutput = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Modification done here Add Supplier Name  //s.SupplierName //Write a Stored Proc for this
                    cmd.CommandText = "SELECT I.Quantity from dbo.Items I  WHERE I.SupplierItemCode=@supp";
                    cmd.Parameters.AddWithValue("@supp", rep);
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["Quantity"] != DBNull.Value)
                                strOutput = Convert.ToInt32(sdr["Quantity"].ToString());
                        }


                    }
                    conn.Close();
                }
            }

            return strOutput;

        }

        private void UpdateProductAudit(int databaseqty, int newqty, int enteredQty, string itemCode, string Dtsnumber)
        {
            var itemId = getItemIdByItemCode(itemCode);
            var oldvalues = " Quantity : " + databaseqty + " Product Code :" + itemCode;
            var newValues = " Quantity : " + newqty + " Cancelled Order Quantity : " + enteredQty + " Product Code :" + itemCode + " Order Number :" + Dtsnumber;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());

            var columnName = "Product Items";
            var talbeName = "Product Items";
            var ActionType = "Updated Product Items";
            int primaryKey = itemId;
            var strCompanyID = -1; // For product we do not use companyid refer -1 to identify the type for items
            new DeltoneCRM_DAL.CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString).CreateActionONAuditLog(oldvalues, newValues, loggedInUserId, conn, 0,
       columnName, talbeName, ActionType, primaryKey, strCompanyID);


        }

        private int getItemIdByItemCode(string code)
        {
            var strOutput = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Modification done here Add Supplier Name  //s.SupplierName //Write a Stored Proc for this
                    cmd.CommandText = "SELECT I.ItemID from dbo.Items I  WHERE I.SupplierItemCode=@supp";
                    cmd.Parameters.AddWithValue("@supp", code);
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["ItemID"] != DBNull.Value)
                                strOutput = Convert.ToInt32(sdr["ItemID"].ToString());
                        }


                    }
                    conn.Close();
                }
            }

            return strOutput;

        }
  
    }
}