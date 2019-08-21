using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM
{
    public partial class Order : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            String CustID = Request.QueryString["cid"].ToString();
            getCustomerDetails(CustID);

            //Set CustomerID Hidden value
            hdnContactID.Value = CustID;
        }

        protected void btnOrderSubmit_Click(object sender, EventArgs e)
        {

            //Response.Write("Order Items" + OrderItems.Value);


        }


        //This Function Fetch The LastCreated OrderID
        protected int LastOrderID(String strCratedBy)
        {
            int intOrderID = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "select Top 1 * from dbo.Orders where CreatedBy='" +strCratedBy +"'Order by  CreatedDateTime Desc";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        intOrderID =Int32.Parse(sdr["OrderID"].ToString());
                    }
                }

            }
            conn.Close();

            return intOrderID;
        }



        //This Function Create an Order and OrderItems
        protected int  CreateOrder(String strCompanyID,String strContactId,float COGTotal, float COGSubTotal, float ProfitTotal, float ProfitSubtotal, float SuppDelCost, float CusDelCost, float ProItemCost,String strCreatedBy)
        {
            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "insert into dbo.Orders(CompanyID,ContactID,COGTotal,COGSubTotal,Total,SubTotal,SupplierDelCost,CustomerDelCost,ProItemCost,OrderedDateTime,CreatedDateTime,CreatedBy) values (@CompanyID,@ContactID,@COGtotal,@CogSubtotal,@ProfitTotal,@ProfitSubTotal,@SuppDelCost,@CusDelCost,@ProItemCost,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,@CreatedBy);";
            SqlCommand cmd = new SqlCommand("strSqlStmt", conn);
            cmd.Parameters.AddWithValue("@CompanyID", strCompanyID);
            cmd.Parameters.AddWithValue("@ContactID", strContactId);
            cmd.Parameters.AddWithValue("@COGtotal", COGTotal);
            cmd.Parameters.AddWithValue("@CogSubtotal", COGSubTotal);
            cmd.Parameters.AddWithValue("@ProfitTotal", ProfitTotal);
            cmd.Parameters.AddWithValue("@ProfitSubTotal", ProfitSubtotal);
            cmd.Parameters.AddWithValue("@SuppDelCost", SuppDelCost);
            cmd.Parameters.AddWithValue("@CusDelCost", CusDelCost);
            cmd.Parameters.AddWithValue("@ProItemCost", ProItemCost);
            cmd.Parameters.AddWithValue("@CreatedBy", strCreatedBy);
         
            try
            {
                conn.Open();
                rowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            return rowsEffected;
        }


        //This Function Insert Order Items
        protected int CreateOrderItems(int OrderID, String strOderItems)
        {
            int rowsEffected = 0;
            String[] strItems = strOderItems.Split('|');
            String[] lineItem;
            
           
            return rowsEffected;
        }



        protected void getCustomerDetails(String CustID)
        {
            String CustomerName = "";
            String CustomerBillAddressLine1 = "";
            String CustomerBillAddressLine2 = "";
            String CustomerBillCity = "";
            String CustomerBillPostcode = "";
            String CustomerBillState = "";

            String CustomerShipAddressLine1 = "";
            String CustomerShipAddressLine2 = "";
            String CustomerShipCity = "";
            String CustomerShipPostcode = "";
            String CustomerShipState = "";


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Contacts WHERE ContactID = " + CustID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CustomerName = sdr["ContactName"].ToString();
                            CustomerBillAddressLine1 = sdr["STREET_AddressLine1"].ToString();
                            CustomerBillAddressLine2 = sdr["STREET_AddressLine2"].ToString();
                            CustomerBillCity = sdr["STREET_City"].ToString();
                            CustomerBillPostcode = sdr["STREET_PostalCode"].ToString();
                            CustomerBillState = sdr["STREET_Region"].ToString();

                            CustomerShipAddressLine1 = sdr["POSTAL_AddressLine1"].ToString();
                            CustomerShipAddressLine2 = sdr["POSTAL_AddressLine2"].ToString();
                            CustomerShipCity= sdr["POSTAL_City"].ToString();
                            CustomerShipPostcode = sdr["POSTAL_PostalCode"].ToString();
                            CustomerShipState = sdr["POSTAL_Region"].ToString();

                        }
                    }
                }
            }

            ContactInfo.InnerHtml = CustomerName;
            BillingAddress.InnerHtml = CustomerBillAddressLine1 + "<br/>" + CustomerBillAddressLine2 + "<br/>" + CustomerBillCity + "<br/>" + CustomerBillState + " - " + CustomerBillPostcode;
            ShippingAddress.InnerHtml = CustomerShipAddressLine1 + "<br/>" + CustomerShipAddressLine2 + "<br/>" + CustomerShipCity + "<br/>" + CustomerShipState + " - " + CustomerShipPostcode;
        }
    }
}