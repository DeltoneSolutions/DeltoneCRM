using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;


namespace DeltoneCRM_DAL
{

    public class OrderDAL
    {
        private String CONNSTRING;

        public OrderDAL(String connstring)
        {
            CONNSTRING = connstring;
        }


        /// <summary>
        /// Get the Order Created Datetime
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public String OrderCreatedDate(int OrderID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select CreatedDateTime from Orders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["CreatedDateTime"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;

        }

        public String DummyOrderCreatedDate(int OrderID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select CreatedDateTime from DummyOrders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["CreatedDateTime"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;

        }

        public class OrderCommissionDisplay
        {
            public string OrderId { get; set; }
            public string UserId { get; set; }
            public string RepName { get; set; }
            public string Amount { get; set; }
            public string CommId { get; set; }
        }

        public void UpdateOrderCommission(string userid, string orderid, string comm)
        {
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "UPDATE [dbo].[Commision] SET Value=@value WHERE OrderID=@OrderID and UserLoginID=@userloginid";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@userloginid", userid);
            cmd.Parameters.AddWithValue("@OrderID", orderid);
            cmd.Parameters.AddWithValue("@value", comm);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }
            conn.Close();


        }

        public IList<OrderCommissionDisplay> GetOrderCommissionLIst(string orderid)
        {
            var list = new List<OrderCommissionDisplay>();
            if (!string.IsNullOrEmpty(orderid))
            {
                String output = String.Empty;
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = CONNSTRING;
                String strQuery = @" select UserLoginID,Value,[dbo].Logins.FirstName,CommInD from [dbo].[Commision]
   join [dbo].Logins on [dbo].[Commision].UserLoginID=[dbo].Logins.LoginID
   where [dbo].[Commision].OrderID =" + orderid;

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strQuery;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var obj = new OrderCommissionDisplay();
                            obj.Amount = sdr["Value"].ToString();
                            obj.UserId = sdr["UserLoginID"].ToString();
                            obj.RepName = sdr["FirstName"].ToString();
                            obj.CommId = sdr["CommInD"].ToString();
                            obj.OrderId = orderid;

                            list.Add(obj);
                        }
                    }
                }
                conn.Close();
            }


            return list;

        }

        //Method return if commish split was requested
        public String getCommishSplitBoolean(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT CommishSplit FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["CommishSplit"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }
        public String getDummyCommishSplitBoolean(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT CommishSplit FROM dbo.DummyOrders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["CommishSplit"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }
        //Method returns the urgency of the order
        public String getOrderUrgency(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT Urgency FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["Urgency"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        public String getDummyOrderUrgency(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT Urgency FROM dbo.DummyOrders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["Urgency"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        //Method returns if the person who the commission is split with
        public String getCommishSplitWith(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT OrderEnteredBy FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderEnteredBy"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        //Method returns the salesperson Login ID
        public String getCommishSplitWithID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT OrderEnteredByID FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderEnteredByID"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        //Method returns the account owner's Name
        public String getSalespersonName(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT OrderEnteredBy FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderEnteredBy"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        public String getSalespersonNameNotExists(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT OrderEnteredBy FROM dbo.OrdersNotExists WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderEnteredBy"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }


        public String getDummySalespersonName(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT OrderEnteredBy FROM dbo.DummyOrders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderEnteredBy"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        //Method returns the account owner's Login ID
        public String getRealAccountOwnerID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT AccountOwnerID FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["AccountOwnerID"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        //Method returns the account owner's Login Name
        public String getRealAccountOwner(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT AccountOwner FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["AccountOwner"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        //Method returns the person splitting the commission with Login ID
        public String getSalespersonByID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT OrderEnteredByID FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderEnteredByID"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        public String getSalespersonByIDNotExists(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT OrderEnteredByID FROM dbo.OrdersNotExists WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderEnteredByID"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        public String getDummySalespersonByID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT OrderEnteredByID FROM dbo.DummyOrders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderEnteredByID"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        public String getCreditNoteCreatedBy(int CreditNoteID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT CreatedBy FROM dbo.CreditNotes WHERE CreditNote_ID = " + CreditNoteID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["CreatedBy"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        public String getVolumeSplitAmount(int OrderID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select VolumeSplitAmount from Orders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        OutPut = sdr["VolumeSplitAmount"].ToString();

                    }
                }
            }
            conn.Close();
            return OutPut;

        }



        public String getDummyVolumeSplitAmount(int OrderID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select VolumeSplitAmount from DummyOrders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        OutPut = sdr["VolumeSplitAmount"].ToString();

                    }
                }
            }
            conn.Close();
            return OutPut;

        }


        //This method returns the last order date based on the companyID
        public String getLastOrderDateBasedOnCompanyID(String CompanyID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT TOP 1 OrderedDateTime FROM dbo.Orders WHERE CompanyID = " + CompanyID + " ORDER BY OrderedDateTime DESC";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["OrderedDateTime"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        //This method return the last salesperson of the order based on the companyID
        public String getLastSalespersonBasedOnCompanyID(String CompanyID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT TOP 1 CreatedBy FROM dbo.Orders WHERE CompanyID = " + CompanyID + " ORDER BY OrderedDateTime DESC";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["CreatedBy"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }


        public String getOrderCreatedBy(int OrderID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select CreatedBy from Orders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        OutPut = sdr["CreatedBy"].ToString();

                    }
                }
            }
            conn.Close();
            return OutPut;

        }

        public String getdummOrderCreatedBy(int OrderID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select CreatedBy from DummyOrders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        OutPut = sdr["CreatedBy"].ToString();

                    }
                }
            }
            conn.Close();
            return OutPut;

        }

        public String getCustomerDelItems(int OrderID)
        {

            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select CusDelCostItems from Orders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        OutPut = sdr["CusDelCostItems"].ToString();

                    }
                }
            }
            conn.Close();
            return OutPut;

        }

        public String getSupplierDelItems(int OrderID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select SuppDeltems from Orders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        OutPut = sdr["SuppDeltems"].ToString();

                    }
                }
            }
            conn.Close();
            return OutPut;
        }


        /// <summary>
        /// Get Supplier notes for the Order given by OrderID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public Dictionary<String, String> getOrderSupplierNotes(int OrderID)
        {
            Dictionary<String, String> di_SuppNotes = new Dictionary<string, string>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select * from SupplierNotes where Type='SalesOrder' and OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        di_SuppNotes.Add(sdr["Suppliername"].ToString(), sdr["Notes"].ToString());
                    }
                }
            }

            return di_SuppNotes;

        }


        /// <summary>
        /// Add Supplier Notes 
        /// </summary>
        /// <param name="intOrderID">Credit Note ID</param>
        /// <param name="strSupplierName">Supplier Name</param>
        /// <param name="strNotes">Note String </param>
        /// <param name="xeroInvoiceNumber">Xero Invoice Number</param>
        /// <returns></returns>
        public int AddSupplierNotes(int intOrderID, String strSupplierName, String strNotes, String xeroInvoiceNumber, String Type)
        {
            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            //Modified here Add Type ('Credit','SalesOrder')
            String strSQLInsertStatement = "INSERT INTO SupplierNotes values(@OrderID,@SupplierName,@Notes,@xeroInvNumber,@Type)";
            SqlCommand cmd = new SqlCommand(strSQLInsertStatement, conn);
            cmd.Parameters.AddWithValue("@OrderID", intOrderID);
            cmd.Parameters.AddWithValue("@SupplierName", strSupplierName);
            cmd.Parameters.AddWithValue("@Notes", strNotes);
            cmd.Parameters.AddWithValue("@xeroInvNumber", xeroInvoiceNumber);
            cmd.Parameters.AddWithValue("@Type", Type);

            try
            {
                conn.Open();
                rowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                //Response.Write(ex.Message.ToString());
            }

            return rowsEffected;
        }


        /// <summary>
        /// Add Order Supplier notes to Credit Notes
        /// </summary>
        /// <param name="intOrederID"></param>
        /// <param name="xeroInvoiceNumber"></param>
        /// <param name="di"></param>
        public void CreateSupplerNotes(int intOrederID, String xeroInvoiceNumber, Dictionary<String, String> di)
        {
            foreach (var item in di)
            {

                AddSupplierNotes(intOrederID, item.Key, item.Value, xeroInvoiceNumber, "Credit");
            }

        }



        /*Get Order Status for Deletion*/
        public String getCurrentOrderStatus(int OrderID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select Status from dbo.Orders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        OutPut = sdr["Status"].ToString();

                    }
                }
            }
            conn.Close();
            return OutPut;
        }
        /*End mehtod get status*/

        //Return Order Created Date
        public String getOrderCreatedDate(int OrderID)
        {
            String output = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT  CreatedDateTime FROM Orders WHERE OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["CreatedDateTime"].ToString();
                    }
                }
            }
            conn.Close();

            return output;

        }

        //Method to return the last order number for a particular company
        public String getLastOrderDateForCompany(String CompanyID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT TOP 1 CreatedDateTime FROM dbo.Orders WHERE CompanyID = " + CompanyID + " ORDER BY CreatedDateTime DESC";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CreatedDateTime"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }


        //This Method retunrs the Guid given by OrderID
        public String getOrderGuid(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select XeroGUID from Orders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["XeroGUID"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        /*This Method Fetch the Order Status*/
        public String getORDER_STATUS(int OrderID)
        {

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select Status from Orders where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["Status"].ToString();
                    }
                }
            }
            conn.Close();

            return output;
        }

        /// <summary>
        /// Cancel the Order given by OrderID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public String CancelOrder(int OrderID)
        {
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "UPDATE dbo.Orders SET Status='CANCELLED' WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }
            conn.Close();

            return RowsEffected;
        }

        /// <summary>
        /// Saves the Commision Entry in the Table
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="UserLoginID"></param>
        /// <param name="CommisionValue"></param>
        /// <param name="SplitFlag"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public String WriteCommision(int OrderID, int UserLoginID, float CommisionValue, String SplitFlag, String CreatedBy, DateTime OdrCreatedDate, String OrderStatus)
        {
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "insert into dbo.Commision(OrderID,UserLoginID,Value,Flag,CreatedBy,CreateDateTime,Type, Status) values(@OrderID,@LoginID,@CommisionValue,@Flag,@CreatedBy,@CreatedDate,@Type,@Status);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@LoginID", UserLoginID);
            cmd.Parameters.AddWithValue("@CommisionValue", CommisionValue);
            cmd.Parameters.AddWithValue("@Flag", SplitFlag);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@Type", "ORDER");
            cmd.Parameters.AddWithValue("@CreatedDate", OdrCreatedDate);
            cmd.Parameters.AddWithValue("@Status", OrderStatus);

            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }
            conn.Close();

            return RowsEffected;
        }

        public String WriteDummyCommision(int OrderID, int UserLoginID, float CommisionValue, String SplitFlag, String CreatedBy, DateTime OdrCreatedDate, String OrderStatus)
        {
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "insert into dbo.DummyCommision(OrderID,UserLoginID,Value,Flag,CreatedBy,CreateDateTime,Type, Status) values(@OrderID,@LoginID,@CommisionValue,@Flag,@CreatedBy,@CreatedDate,@Type,@Status);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@LoginID", UserLoginID);
            cmd.Parameters.AddWithValue("@CommisionValue", CommisionValue);
            cmd.Parameters.AddWithValue("@Flag", SplitFlag);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@Type", "ORDER");
            cmd.Parameters.AddWithValue("@CreatedDate", OdrCreatedDate);
            cmd.Parameters.AddWithValue("@Status", OrderStatus);

            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }
            conn.Close();

            return RowsEffected;
        }

        /// <summary>
        /// Update the Commission Entry given by OrderID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public String RemoveCommisionEntry(int CreditNoteID)
        {
            String strRowsEffected = String.Empty;
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "DELETE FROM dbo.Commision WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", CreditNoteID);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }

            return strRowsEffected;
        }


        public void DeleteFromDummyOrder(int orderId)
        {
            DeleteDummyOrderItems(orderId);
            DeleteDummyOrderCommission(orderId);
            DeleteDummyOrderSupplierNotes(orderId);
            String strRowsEffected = String.Empty;
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "DELETE FROM dbo.DummyOrders WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", orderId);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }

            //return strRowsEffected;
        }

        public void DeleteDummyOrderCommission(int orderId)
        {
            String strRowsEffected = String.Empty;
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "DELETE FROM dbo.DummyCommision WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", orderId);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }

            // return strRowsEffected;
        }

        public void DeleteDummyOrderSupplierNotes(int orderId)
        {
            String strRowsEffected = String.Empty;
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "DELETE FROM dbo.DummySupplierNotes WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", orderId);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }

            // return strRowsEffected;
        }

        public void DeleteDummyOrderItems(int orderId)
        {
            String strRowsEffected = String.Empty;
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "DELETE FROM dbo.DummyOrdered_Items WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", orderId);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }

            // return strRowsEffected;
        }


        /// <summary>
        /// Fetch the Account Owner given by OrderID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public String FetchAccountOwner(int OrderID)
        {

            String output = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select o.OrderedBy,l.LoginID,l.FirstName,l.LastName from dbo.Orders o,Logins l   where   o.OrderedBy=l.LoginID  and OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = output + sdr["LoginID"].ToString() + ":" + sdr["FirstName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;


        }


        // Return's the Customer's shipping cost
        public String getCustomerShippingCost(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = " SELECT CustomerDelCost from dbo.Orders WHERE OrderID=" + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CustomerDelCost"].ToString();

                    }
                }
            }
            conn.Close();

            if (output.Equals("0"))
            {
                output = "FREE";
            }

            return output;
        }

        // Return's Company ID based on the OrderID
        public String getCompanyIDFromOrder(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = " SELECT CompanyID from dbo.Orders WHERE OrderID=" + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CompanyID"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }


        //This Method returns CompanyName given by CompanyID
        public String getCompanyName(int CompID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = " select CompanyName from Companies where CompanyID=" + CompID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CompanyName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }



        public String getCompanyNameByOrderId(int OrderId)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = " select CompanyName from Companies as com  join [dbo].[Orders] as ors ON com.CompanyID=ors.CompanyID where ors.OrderId=" + OrderId;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CompanyName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }


        public String getCompanyNameByCreditId(int OrderId)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = " select CompanyName from Companies as com  join [dbo].[CreditNotes] as ors ON com.CompanyID=ors.CompID where ors.CreditNote_ID=" + OrderId;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CompanyName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        public String getCompanyNameNoOrder(int CompID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = " select CompanyName from CompaniesNotExists where CompanyID=" + CompID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CompanyName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This Method returns CompanyName given by CompanyID from the Quote DB
        public String getNewCompanyName(int CompID, String WhichDB)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = String.Empty;

            if (WhichDB == "QuoteDB")
            {
                strSqlOrderStmt = " select CompanyName from dbo.Quote_Companies where CompanyID=" + CompID;
            }
            else
            {
                strSqlOrderStmt = " select CompanyName from dbo.Companies where CompanyID=" + CompID;
            }


            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CompanyName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //Method returns the Order Owner's Login ID 
        public String getOrderOwnerID(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT CreatedBy from dbo.Orders OR, dbo.Logins LG where OrderID = " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CreatedBy"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method returns who created the order
        public String getOrderOwner(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT CreatedBy from dbo.Orders where OrderID = " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CreatedBy"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method returns the XERO Credit Note ID
        public String getXeroOrderID(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select XeroCreditNoteID from dbo.CreditNotes where CreditNote_ID= " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["XeroCreditNoteID"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        public String getXeroDTSID(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT XeroInvoiceNumber FROM dbo.Orders WHERE OrderID = " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["XeroInvoiceNumber"].ToString();
                    }

                }
            }

            conn.Close();

            return output;
        }




        //This method returns the SupplierNotes for the PrintOrder function
        public String getSupplierNotes(String XeroInvoiceNumber)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT SupplierName, Notes from dbo.SupplierNotes where XeroInvoiceNumber = '" + XeroInvoiceNumber + "'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = output + sdr["SupplierName"].ToString() + "<br>" + sdr["Notes"].ToString() + "<br><br>";

                    }
                }
            }
            conn.Close();

            return output;
        }










        //This method returns the Order Notes for the PrintOrder function
        public String getOrderNotes(String XeroInvoiceNumber)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Notes from dbo.Orders where XeroInvoiceNumber = '" + XeroInvoiceNumber + "'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["Notes"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        public String getOrderSuppDeltems(int orderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT SuppDeltems from dbo.Orders where orderId = " + orderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["SuppDeltems"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method returns the Payment Terms for the PrintOrder function
        public String getPaymentTerms(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT PaymentTerms from dbo.Orders where OrderID = '" + OrderID + "'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["PaymentTerms"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method returns the Order Date for the PrintOrder function
        public String getOrderDate(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT CONVERT(varchar, OrderedDateTime, 103) as OrderedDateTime2 from dbo.Orders where OrderID = '" + OrderID + "'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["OrderedDateTime2"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method returns the Reference Number for the PrintOrder function
        public String getReferenceNumber(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Reference from dbo.Orders where OrderID = '" + OrderID + "'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["Reference"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method returns the Type Of Call for the PrintOrder function
        public String getTypeOfCall(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT TypeOfCall from dbo.Orders where OrderID = '" + OrderID + "'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["TypeOfCall"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method returns the Stamp for either New Account or Re-Order for the PrintOrder function
        public String getStampType(int CompanyID, int ContactID, int OrderID)
        {
            String output = String.Empty;
            String CreatedDateTime = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLFindOldOrder = "SELECT CreatedDateTime FROM dbo.Orders WHERE OrderID = " + OrderID;
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = strSQLFindOldOrder;
                cmd2.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr2 = cmd2.ExecuteReader())
                {
                    while (sdr2.Read())
                    {
                        CreatedDateTime = sdr2["CreatedDateTime"].ToString();
                    }
                }
            }
            conn.Close();
            String strSqlOrderStmt = "SET DATEFORMAT dmy;SELECT COUNT(*) AS COUNT FROM dbo.Orders WHERE CompanyID = " + CompanyID + " AND CreatedDateTime < '" + CreatedDateTime + "'";
            String TotalCount = String.Empty;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            TotalCount = sdr["COUNT"].ToString();
                            if (TotalCount == "0")
                            {
                                String ImgUrl = "Images/img-new-account.png";
                                output = "<img width=180 src=" + ImgUrl + " />";
                            }
                            else
                            {
                                String ImgUrl = "Images/img-re-order.png";
                                output = "<img width=180 src=" + ImgUrl + " />";
                            }

                        }
                    }
                }
            }
            conn.Close();

            return output;
        }

        // Return's the total amount of the order
        public String getOrderTotal(int OID)
        {
            String output = String.Empty;
            float OrderTotal = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Total FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        OrderTotal = float.Parse(sdr["Total"].ToString());

                    }
                }
            }
            conn.Close();

            output = String.Format("{0:C2}", OrderTotal);

            return output;
        }

        // Return's the Contact Person's ID from the Order
        public String getContactPersonID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT ContactID FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["ContactID"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        public String getComapnyID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT CompanyID FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["CompanyID"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        // Return's the Contact Person email address
        public String getContactPersonEmail(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Email FROM dbo.Contacts WHERE ContactID IN (SELECT ContactID FROM dbo.Orders WHERE OrderID = " + OID + ")";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {


                        output = sdr["Email"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        // Return's the Contact Person for the order
        public String getContactPersonForOrder(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT FirstName + ' ' + LastName AS FullName FROM dbo.Contacts WHERE ContactID IN (SELECT ContactID FROM dbo.Orders WHERE OrderID = " + OID + ")";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["FullName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        // Return's order items for the email confirmation
        public String getOrderItemList(String OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Description, Quantity, UnitAmount FROM dbo.Ordered_Items WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            output = output + sdr["Description"].ToString() + "|" + sdr["Quantity"].ToString() + "|" + String.Format("{0:C2}", sdr["UnitAmount"].ToString()) + "~";

                        }
                    }
                    else
                    {
                        output = "";
                    }
                }

                int Length = output.Length;
                output = output.Substring(0, (Length - 1));
            }
            conn.Close();

            return output;
        }


        public List<OrderItem> getOrderItemListByOrderId(String OID)
        {
            var orderItemList = new List<OrderItem>();
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Description, Quantity, UnitAmount ,SupplierName FROM dbo.Ordered_Items WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            var orderItem = new OrderItem();
                            orderItem.Description = sdr["Description"].ToString();
                            orderItem.Quantity = sdr["Quantity"].ToString();
                            orderItem.UnitAmount = string.Format("{0:C2}", sdr["UnitAmount"].ToString());
                            orderItem.SuppName = sdr["SupplierName"].ToString();
                            orderItemList.Add(orderItem);
                        }
                    }
                    else
                    {
                        output = "";
                    }
                }


            }
            conn.Close();

            return orderItemList;
        }

        public class OrderItem
        {
            public string Description { get; set; }
            public string Quantity { get; set; }
            public string UnitAmount { get; set; }
            public string SuppName { get; set; }
        }


        //This method returns the Account Owner for the PrintOrder function
        public String getAccountOwner(int CompanyID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT CP.CompanyName, LG.FirstName, LG.LastName FROM dbo.Companies CP, dbo.Logins LG WHERE CP.OwnershipAdminID = LG.LoginID AND CP.CompanyID = " + CompanyID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            output = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();

                        }
                    }
                    else
                    {
                        String ImgUrl = "../Images/img-new-account.png";
                        output = "<img width=180 src=" + ImgUrl + " />";
                    }
                }
            }
            conn.Close();

            return output;
        }

        // Method returns the Number of Pending Orders
        public String getNumberPendingOrders()
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT COUNT(*) AS TotCount FROM dbo.Orders WHERE Status = 'PENDING'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            output = sdr["TotCount"].ToString();

                        }
                    }
                    else
                    {
                        output = "0";
                    }
                }
            }
            conn.Close();

            return output;
        }

        public String EOMBOOrderUpdate(int OrderID, string typeorder)
        {
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "UPDATE dbo.Orders SET Status=@stat WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@stat", typeorder);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }
            conn.Close();

            return RowsEffected;
        }

        public String UpdateContactPersonName(int OrderID, string contactpersonName)
        {
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "UPDATE dbo.Orders SET OrderContactName=@name WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@name", contactpersonName);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }
            conn.Close();

            return RowsEffected;
        }

        public string GetOrderContactPersonName(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT OrderContactName FROM dbo.Orders WHERE OrderID = " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["OrderContactName"] != DBNull.Value)
                            output = sdr["OrderContactName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

    }
}
