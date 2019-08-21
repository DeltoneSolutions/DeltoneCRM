using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


namespace DeltoneCRM_DAL
{
    public class PurchaseDAL
    {
        String CONNSTRING;

        public PurchaseDAL(String connstring)
        {
            CONNSTRING = connstring;
        }

        //Insert Purchase Order Items for the Order
        public String InsertPurchaseItem(int OrderID,String SupplierName,String ItemList,String CreatedBy)
        {

            String insert_strOutput=String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "Insert into dbo.PurchaseOrderItem(OrderID,SupplierName,ItemList,CreateDateTime,CreatedBy) values(@OrderID,@SuppName,@itemlist,CURRENT_TIMESTAMP,@createdby)";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@SuppName", SupplierName);
            cmd.Parameters.AddWithValue("@itemlist", ItemList);
            cmd.Parameters.AddWithValue("@createdby", CreatedBy);

            try
            {
                conn.Open();
                insert_strOutput = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                insert_strOutput = ex.Message.ToString();
            }


            return insert_strOutput;
        }

        //This method the Update the Purchase Items for the given Order
        public void UpdatePurhcaseItems(int OrderID, Dictionary<String, String> di,String strLoggedUser)
        {
            String strRemove = RemovePurchaseList(OrderID);
            //For each Item in the Dictionary Add an Entry to the List
            foreach (var pair in di)
            {
                String output = InsertPurchaseItem(OrderID, pair.Key.ToString(), pair.Value.ToString(), strLoggedUser);
            }
        }


        //public void UpdatePurchaseOrderGUID

        /// <summary>
        /// Fetch the Purchase Items Object Given by OrderID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<PurchaseItemAndSupplier> GetPurchaseItems(int OrderID)
        {

            Dictionary<String, String> di_items = new Dictionary<string,string>();

            var listItemsBySupplier = new List<PurchaseItemAndSupplier>();

            String get_strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT * FROM dbo.PurchaseOrderItem WHERE OrderID = " + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var purchaseItem = new PurchaseItemAndSupplier();
                        purchaseItem.OrderItems = new List<OrderItem>();
                        if (sdr["SupplierName"] != DBNull.Value)
                            purchaseItem.SupplierName = sdr["SupplierName"].ToString();
                        if (sdr["ItemList"] != DBNull.Value)
                        {
                           // di_items.Add(sdr["SupplierName"].ToString(), sdr["ItemList"].ToString());

                            var itemsOrdered = sdr["ItemList"].ToString();
                            var splitItems = itemsOrdered.Split('|');
                           
                            for (int i = 0; i < splitItems.Length; i++)
                            {
                                if (!String.IsNullOrEmpty(splitItems[i]))
                                {
                                    var orderItem = new OrderItem();
                                    String[] items = splitItems[i].Split(':');
                                    //SupplierCode
                                    orderItem.SupplierCode = items[0];
                                    orderItem.Quantity = items[1];
                                    orderItem.COGAmount = items[2];
                                    purchaseItem.OrderItems.Add(orderItem);

                                }
                            }
                        }
                        listItemsBySupplier.Add(purchaseItem);
                        
                    }
                }
            }
            conn.Close();

            return listItemsBySupplier;

        }

        public string ItemDescription(string supplierCode)
        {
         
                SqlConnection conn = new SqlConnection();
                string description = supplierCode;
               
                conn.ConnectionString = CONNSTRING;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = " SELECT Description from Items where SupplierItemCode=@SupplierItemCode ";
                cmd.Parameters.AddWithValue("@SupplierItemCode", supplierCode);
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            if (sdr["Description"] != DBNull.Value)
                                description = sdr["Description"].ToString();
                        }
                       
                    }
                }
                conn.Close();
                return description;

        }


        public void UpdatePurchaseItemBYXeroInvoiceId(int OrderId, string supplierName, string xeroInvoiceNumber,
            string referencenumber,string delcost,string purchaseId)
        {
            String Remove_strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = @"update  PurchaseOrderItem SET XeroInvoiceNumber=@xeroInvoiceNumber, 
                   AlteredDateTime=CURRENT_TIMESTAMP , ReferenceNumber=@referencenumber , 
               DeliveryCost=@delcost , XeroPurchaseId=@purchaseId where OrderID=@orderid and SupplierName=@supplierName";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@orderid", OrderId);
            cmd.Parameters.AddWithValue("@supplierName", supplierName);
            cmd.Parameters.AddWithValue("@xeroInvoiceNumber", xeroInvoiceNumber);
            cmd.Parameters.AddWithValue("@referencenumber", referencenumber);
            cmd.Parameters.AddWithValue("@delcost", delcost);
            cmd.Parameters.AddWithValue("@purchaseId", purchaseId);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Remove_strOutPut = ex.Message.ToString();
            }

        }


        public Dictionary<String, String> getPurchaseItemObject(int OrderID)
        {

            Dictionary<String, String> di_items = new Dictionary<string, string>();
            String get_strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT * FROM dbo.PurchaseOrderItem WHERE OrderID = " + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        di_items.Add(sdr["SupplierName"].ToString(), sdr["ItemList"].ToString());

                    }
                }
            }
            conn.Close();

            return di_items;

        }

        public List<String> getPurchasedSupplierByOrderId(int OrderID)
        {

            var di_items = new List<string>();
            String get_strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT SupplierName FROM dbo.PurchaseOrderItem WHERE OrderID = " + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        di_items.Add(sdr["SupplierName"].ToString());

                    }
                }
            }
            conn.Close();

            return di_items;

        }

        public IList<int> getPurchaseIdByOrderId(int OrderID)
        {

            IList<int> itmes = new List<int>();
            String get_strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT * FROM dbo.PurchaseOrderItem WHERE OrderID = " + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                     //   itmes.Add(sdr["SupplierName"].ToString(), sdr["ItemList"].ToString());

                    }
                }
            }
            conn.Close();

            return itmes;

        }

        public List<PurchaseItemIdAndSupplierName> GetSupplierNameAndXeroNumber(int orderID)
        {
            var list = new List<PurchaseItemIdAndSupplierName>();

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = CONNSTRING;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = " SELECT SupplierName,XeroInvoiceNumber,XeroPurchaseId from PurchaseOrderItem where OrderID=@orderID ";
            cmd.Parameters.AddWithValue("@orderID", orderID);
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        var obj = new PurchaseItemIdAndSupplierName();
                        if (sdr["SupplierName"] != DBNull.Value)
                            obj.SupplierName = sdr["SupplierName"].ToString();
                        if (sdr["XeroInvoiceNumber"] != DBNull.Value)
                            obj.XeroInvoiceNumber = sdr["XeroInvoiceNumber"].ToString();
                        if (sdr["XeroPurchaseId"] != DBNull.Value)
                            obj.XeroPurchaseId = sdr["XeroPurchaseId"].ToString();

                        list.Add(obj);
                    }

                }
            }
            conn.Close();
            return list;

        }
        public string GetpurchaseId(int orderID, string supplierName)
        {
            var purchaseId = "";
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = CONNSTRING;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = " SELECT XeroPurchaseId from PurchaseOrderItem where OrderID=@orderID and SupplierName=@supplierName";
            cmd.Parameters.AddWithValue("@orderID", orderID);
            cmd.Parameters.AddWithValue("@supplierName", supplierName);
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {

                        purchaseId = sdr["XeroPurchaseId"].ToString();
                       
                    }

                }
            }
            conn.Close();
            return purchaseId;

        }

        public string GetAdjByOrderId(int orderID)
        {
            var adjValue = "";
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = CONNSTRING;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = " SELECT SupplierAdj from Orders where OrderID=@orderID";
            cmd.Parameters.AddWithValue("@orderID", orderID);
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {

                        adjValue = sdr["SupplierAdj"].ToString();

                    }

                }
            }
            conn.Close();
            return adjValue;

        }

      
        //Remove an Item from Purchase Item
        public String RemovePurchaseList(int OrderID)
        {
            String Remove_strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "Delete from dbo.PurchaseOrderItem where OrderID=@orderid";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@orderid", OrderID);
            
            try
            {
                conn.Open();
                Remove_strOutPut = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Remove_strOutPut = ex.Message.ToString();
            }

            return Remove_strOutPut;
        }


        public class OrderItem
        {

            public string Description { get; set; }
            public string COGAmount { get; set; }
            public string SupplierCode { get; set; }
            public string Quantity { get; set; }
            public string SupplierName { get; set; }

        }

        public class PurchaseItemAndSupplier
        {
            public string SupplierName { get; set; }
            public IList<OrderItem> OrderItems { get; set; }
        }

        public class PurchaseItemIdAndSupplierName
        {
            public string SupplierName { get; set; }
            public string XeroInvoiceNumber { get; set; }
            public string XeroPurchaseId { get; set; }
        }
    }
}
