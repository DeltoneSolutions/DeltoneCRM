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
    public class CreditNotesDAL
    {
        //Fetch the Order Items Relate to Credit Notes
        protected static String CONNSTRING;

        //Pass the Connection String in the Constructer
        public CreditNotesDAL(String strConnectionString)
        {
            CONNSTRING = strConnectionString;
        }


        /// <summary>
        /// Get CreditNote Created ID
        /// </summary>
        /// <param name="CreditNoteID"></param>
        /// <returns></returns>
        public String CreditNoteCreatedDate(int CreditNoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select DateCreated from  dbo.CreditNotes  where  CreditNote_ID=" + CreditNoteID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["DateCreated"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;

        }

        //Method to return the Xero invoice Number from the Order
        public String getXeroOrderNumber(String OID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT XeroInvoiceNumber FROM dbo.Orders WHERE OrderID =" + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["XeroInvoiceNumber"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }


        //Method to return the Xero Invoice Number from the CreditNoteID
        public String getXeroInvoiceNumberFromCreditID(int CreditNoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT XeroInvoiceNumber FROM dbo.Orders WHERE OrderID IN (SELECT OrderID FROM dbo.CreditNotes WHERE CreditNote_ID = " + CreditNoteID + ")";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["XeroInvoiceNumber"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }

        //Method to return the OrderID from the CreditNoteID
        public String getOrderIDFromCreditID(int CreditNoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT OrderID FROM dbo.CreditNotes WHERE CreditNote_ID=" + CreditNoteID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["OrderID"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }

        public String getCompanyIDFromCreditID(int CreditNoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT CompID FROM dbo.CreditNotes WHERE CreditNote_ID=" + CreditNoteID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["CompID"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }



        #region CreditNote_Commission_Calculation

        /// <summary>
        /// Split Commission for Credit Note
        /// </summary>
        /// <param name="CreditNOTEID"></param>
        /// <param name="CompanyID"></param>
        /// <param name="LoggedID"></param>
        /// <param name="commissionvalue"></param>
        public void SplitCommission(int CreditNOTEID, int CompanyID, int LoggedID, float commissionvalue, string creditnotestatus)
        {
            String strOutPut = String.Empty;
            String CreditNote_CreatedDateTime = CreditNoteCreatedDate(CreditNOTEID);

            int CompnyOwnerShip = Int32.Parse(CompanyOwnerShip(CompanyID));
            //if (LoggedID == CompnyOwnerShip)
            //{
            WriteCommision_CreditNote(CreditNOTEID, LoggedID, commissionvalue, "NO", LoggedID.ToString(), Convert.ToDateTime(CreditNote_CreatedDateTime), creditnotestatus);
            //}
            //else
            //{
            //  float newCommisionValue = (float)Math.Round((commissionvalue * 0.5), 2);
            // WriteCommision_CreditNote(CreditNOTEID, LoggedID, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreditNote_CreatedDateTime), creditnotestatus);
            // WriteCommision_CreditNote(CreditNOTEID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString(),Convert.ToDateTime(CreditNote_CreatedDateTime), creditnotestatus);
            // }

        }

        /// <summary>
        /// Saves the Commision Entry in the Table
        /// </summary>
        /// <param name="CreditNOTEID"></param>
        /// <param name="UserLoginID"></param>
        /// <param name="CommisionValue"></param>
        /// <param name="SplitFlag"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public String WriteCommision_CreditNote(int CreditNOTEID, int UserLoginID, float CommisionValue, String SplitFlag, String CreatedBy, DateTime createdDate, String CreditNoteStatus)
        {
            LoginDAL logdal = new LoginDAL(CONNSTRING);

            String LoginName = logdal.getLoginNameFromID(CreatedBy);

            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "insert into dbo.Commision(OrderID,UserLoginID,Value,Flag,CreatedBy,CreateDateTime,Type, CreatedByName, Status) values(@CreditNoteID,@LoginID,@CommisionValue,@Flag,@CreatedBy,@Credit_CreatedDate,@Type,@CreatedByName,@Status);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", CreditNOTEID);
            cmd.Parameters.AddWithValue("@LoginID", UserLoginID);
            cmd.Parameters.AddWithValue("@CommisionValue", CommisionValue);
            cmd.Parameters.AddWithValue("@Flag", SplitFlag);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@Type", "CREDITNOTE");
            cmd.Parameters.AddWithValue("@Credit_CreatedDate", createdDate);
            cmd.Parameters.AddWithValue("@CreatedByName", LoginName);
            cmd.Parameters.AddWithValue("@Status", "APPROVED");

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
        /// Fetch the Company OwnerShip for the CreditNote Comssion Calculations
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        protected String CompanyOwnerShip(int CompanyID)
        {
            String strOwnershipAdmin = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select l.FirstName,l.LoginID,c.OwnershipAdminID from Companies c ,dbo.Logins l where  c.OwnershipAdminID=l.LoginID and c.CompanyID=" + CompanyID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strOwnershipAdmin = sdr["OwnershipAdminID"].ToString();
                            }
                        }
                    }

                }

            }

            return strOwnershipAdmin;
        }


        /// <summary>
        ///Update Commissions for the Credit Note
        /// </summary>
        /// <param name="CreditNoteID"></param>
        /// <param name="CompanyID"></param>
        /// <param name="LoggedID"></param>
        /// <param name="commissionvalue"></param>
        /// 
        /*
        public void UpdateCommissions_CreditNote(int CreditNOTEID, int CompanyID, int LoggedID, float commissionvalue, string creditnotestatus)
        {
            //Remove the Old Entry First
            String output = RemoveCommisionEntry_Credit(CreditNOTEID);

            String CreditNote_CreatedDateTime = CreditNoteCreatedDate(CreditNOTEID);

            int CompnyOwnerShip = Int32.Parse(CompanyOwnerShip(CompanyID));

            WriteCommision_CreditNote(CreditNOTEID, CompanyID, LoggedID, commissionvalue, creditnotestatus);

        }*/

        /// <summary>
        /// Remove Commission entry giveb by CreditNoteID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public String RemoveCommisionEntry_Credit(int CreditNoteID)
        {
            String strRowsEffected = String.Empty;
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "DELETE FROM dbo.Commision WHERE OrderID=@OrderID AND Type='CREDITNOTE'";
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

        #endregion


        #region PurchaseItems,Notes

        /// <summary>
        ///get Notes Object given by CreditNoteID
        /// </summary>
        /// <param name="CreditNoteID"></param>
        /// <returns></returns>
        public Dictionary<String, String> getNotesObject(int CreditNoteID)
        {
            Dictionary<String, String> di_notes = new Dictionary<string, string>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT  * FROM dbo.CN_SupplierNotes  WHERE CreditNoteID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        di_notes.Add(sdr["SupplierName"].ToString(), sdr["Notes"].ToString());
                    }
                }
            }
            conn.Close();

            return di_notes;
        }



        /// <summary>
        /// Fetch the Purchase Item Object  Object Given by CreditNoteID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public Dictionary<String, String> getPurchaseItemObject(int CreditNoteID)
        {

            Dictionary<String, String> di_items = new Dictionary<string, string>();
            String get_strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT * FROM dbo.PurchaseOrderItem WHERE Type='Credit' and  OrderID = " + CreditNoteID;
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






        #endregion PurchaseItems,Notes



        #region Credit Note Purchase Items


        //This Function Fetch SupplierNotes given by OrderID
        protected String FetchSupplierNotes(int CreditNoteID)
        {
            String strNotes = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select * from dbo.SupplierNotes where OrderID=" + CreditNoteID + "and Type='Credit'";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strNotes = strNotes + sdr["Suppliername"].ToString() + ":" + sdr["Notes"].ToString();
                        strNotes = strNotes + "|";
                    }
                }
            }
            conn.Close();


            return strNotes;
        }



        /// <summary>
        /// get Suppliert Del cost Items
        /// </summary>
        /// <param name="CreditNoteID"></param>
        /// <returns></returns>
        public String getSuppDelItems(int CreditNoteID)
        {
            String strOutPut = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select o.SuppDeltems, o.CusDelCostItems from Orders o,CreditNotes cn where o.OrderID=cn.OrderID and cn.CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strOutPut = strOutPut + sdr["SuppDeltems"].ToString();

                    }
                }
            }
            conn.Close();


            return strOutPut;

        }



        /// <summary>
        /// get Customer Delivery Cost Items
        /// </summary>
        /// <param name="CreditNoteID"></param>
        /// <returns></returns>
        public String getCusDelItems(int CreditNoteID)
        {
            String strOutPut = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select o.SuppDeltems, o.CusDelCostItems from Orders o,CreditNotes cn where o.OrderID=cn.OrderID and cn.CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strOutPut = strOutPut + sdr["CusDelCostItems"].ToString();

                    }
                }
            }
            conn.Close();


            return strOutPut;

        }




        /// <summary>
        /// Update purchase Order Items(Crdit Notes)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="di"></param>
        /// <param name="strLoggedUser"></param>


        public void UpdatePurhcaseItems(int OrderID, Dictionary<String, String> di, String strLoggedUser)
        {
            String strRemove = RemovePurchaseList(OrderID);
            //For each Item in the Dictionary Add an Entry to the List
            foreach (var pair in di)
            {
                String output = InsertPurchaseItem(OrderID, pair.Key.ToString(), pair.Value.ToString(), strLoggedUser, "Credit");
            }
        }



        /// <summary>
        /// Insert Purchase Order Items for the Credit Notes 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="SupplierName"></param>
        /// <param name="ItemList"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public String InsertPurchaseItem(int OrderID, String SupplierName, String ItemList, String CreatedBy, String Type)
        {

            String insert_strOutput = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            ///Modification done Add Type( 'Credit'/'SalesOrder')
            String strSQLInsertStmt = "Insert into dbo.PurchaseOrderItem(OrderID,SupplierName,ItemList,CreateDateTime,CreatedBy,Type) values(@OrderID,@SuppName,@itemlist,CURRENT_TIMESTAMP,@createdby,@type)";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@SuppName", SupplierName);
            cmd.Parameters.AddWithValue("@itemlist", ItemList);
            cmd.Parameters.AddWithValue("@createdby", CreatedBy);
            cmd.Parameters.AddWithValue("@type", Type);

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


        /// <summary>
        /// Remove Purchase Order Items(Credit Notes)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public String RemovePurchaseList(int CreditNoteID)
        {
            String Remove_strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = "Delete from dbo.PurchaseOrderItem where OrderID=@orderid and Type='Credit'"; //Modification done here Delete if Type is Credit
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@orderid", CreditNoteID);

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



        /// <summary>
        /// Update Supplier Notes(Credit Notes)
        /// </summary>
        /// <param name="OrderID">Credit NotID</param>
        /// <param name="xeroInvoiceNumber">xeroInvoiceNumber</param>
        /// <param name="di">Notes Dictionary Object</param>
        public void UpdateSupplierNotes(int OrderID, String xeroInvoiceNumber, Dictionary<String, String> di)
        {
            //Remove the Previous Entry
            if (RemoveSupplierNotes(OrderID) > 0)
            {
                CreateSupplerNotes(OrderID, xeroInvoiceNumber, di);
            }
        }

        /// <summary>
        /// Remove Supplier Notes 
        /// </summary>
        /// <param name="OrderID">Credit Note ID</param>
        /// <returns></returns>
        protected int RemoveSupplierNotes(int intCreditNoteID)
        {
            int intRowsEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStatement = "Delete from dbo.CN_SupplierNotes  where CreditNoteID=@CreditNoteID "; //Modified here Remove if only Type='Credit'
            SqlCommand cmd = new SqlCommand(strSQLInsertStatement, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", intCreditNoteID);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                //Response.Write(ex.Message.ToString());
            }

            return intRowsEffected;
        }



        /// <summary>
        /// Creates Supplier Notes according to the Dictionary Object
        /// </summary>
        /// <param name="intOrederID">Credit NoteID</param>
        /// <param name="xeroInvoiceNumber">xeroInvoice Number</param>
        /// <param name="di">Notes Dictionary Object</param>
        public void CreateSupplerNotes(int intOrederID, String xeroInvoiceNumber, Dictionary<String, String> di)
        {
            foreach (var item in di)
            {

                AddSupplierNotes(intOrederID, item.Key, item.Value, xeroInvoiceNumber);
            }

        }

        /// <summary>
        /// Add Supplier Notes 
        /// </summary>
        /// <param name="intOrderID">Credit Note ID</param>
        /// <param name="strSupplierName">Supplier Name</param>
        /// <param name="strNotes">Note String </param>
        /// <param name="xeroInvoiceNumber">Xero Invoice Number</param>
        /// <returns></returns>
        protected int AddSupplierNotes(int CreditNoteID, String strSupplierName, String strNotes, String xeroInvoiceNumber)
        {
            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            //Modified here Add Type ('Credit','SalesOrder')
            String strSQLInsertStatement = "INSERT INTO CN_SupplierNotes  values(@CreditNoteID,@SupplierName,@Notes,@xeroInvNumber)";
            SqlCommand cmd = new SqlCommand(strSQLInsertStatement, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", CreditNoteID);
            cmd.Parameters.AddWithValue("@SupplierName", strSupplierName);
            cmd.Parameters.AddWithValue("@Notes", strNotes);
            cmd.Parameters.AddWithValue("@xeroInvNumber", xeroInvoiceNumber);

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






        #endregion Credit Note Purchase Items


        /*This Method Update the CRM Credit Note Table with the Xero Entry*/
        public String UpdateWithXeroEntry(int CreditNoteId, String strXeroGuid, String xerocreditNoteID)
        {
            String strOutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "Update dbo.CreditNotes set XeroGuid=@guid,XeroCreditNoteID=@xerocreditnoteid where CreditNote_ID=@creditnoteid";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@creditnoteid", CreditNoteId);
            cmd.Parameters.AddWithValue("@guid", strXeroGuid);
            cmd.Parameters.AddWithValue("@xerocreditnoteid", xerocreditNoteID);

            try
            {
                conn.Open();
                strOutPut = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                strOutPut = "-1";
            }

            return strOutPut;
        }



        //This  Method Fetch the contact Details given by OrderID
        public String FetchContactDetails(int ContactID)
        {

            String ContactDetails = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "select  cp.CompanyName,cn.* from dbo.Contacts cn,dbo.Companies cp where cn.CompanyID=cp.CompanyID and cn.ContactID=" + ContactID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        String conFullName = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                        String conAddress = sdr["STREET_AddressLine1"].ToString() + Environment.NewLine + sdr["STREET_City"] + Environment.NewLine + sdr["STREET_Region"] + "" + sdr["STREET_PostalCode"] + Environment.NewLine + sdr["STREET_Country"].ToString();
                        String shippingAddress = String.Empty;
                        String conEmail = sdr["Email"].ToString();
                        String CompanyName = sdr["CompanyName"].ToString();
                        ContactDetails = ContactDetails + conFullName + ":" + conAddress + ":" + conEmail + ":" + CompanyName;

                    }
                }

            }
            conn.Close();
            return ContactDetails;

        }


        //This Method Fetch the OrderItem by OrderId
        public String getOrderItemsbyOrderId(String strOrderId, String strConnectionString)
        {

            String strOrderitems = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = strConnectionString;

            String strSqlStmt = "select * from dbo.Ordered_Items where OrderID=" + strOrderId;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOrderitems = strOrderitems + sdr["ItemCode"].ToString() + "," + sdr["Description"] + "," + sdr["UnitAmount"] + "," + sdr["COGamount"] + "," + sdr["SupplierCode"] + "," + sdr["Quantity"] + "," + sdr["SupplierName"];
                        strOrderitems = strOrderitems + "|";

                    }
                }

            }
            conn.Close();

            return strOrderitems;

        }


        //This Method Cancel the Credit Note 
        public String CancelCreditNote(int CreditNoteID)
        {
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "Update CreditNotes set Status='CANCELLED' where CreditNote_ID=@CreditNoteID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", CreditNoteID);
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


        //Fetch the current credit note status
        public String getCurrentCreditNoteStatus(int creditnoteid)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT  Status  FROM CreditNotes WHERE CreditNote_ID=" + creditnoteid;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
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


        /*Return Credit Note Items by CreditNoteID*/
        public String getCreditNoteItems(int creditNoteID)
        {
            String strOrderitems = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT * FROM dbo.CreditNote_Item WHERE CreditNoteID=" + creditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOrderitems = strOrderitems + sdr["CreditNoteItem_ID"].ToString() + "," + sdr["Description"] + "," + sdr["UnitAmount"] + "," + sdr["COG"] + "," + sdr["SupplierCode"] + "," + sdr["Quantity"] + "," + sdr["SupplierName"];
                        strOrderitems = strOrderitems + "|";

                    }
                }

            }
            conn.Close();

            return strOrderitems;

        }



        //Get Credit Note by Credit Note ID
        public String getCreditNote(int CreditNoteID)
        {

            String strOrder = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select * from CreditNotes where CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {


                        String strDateCreated = sdr["DateCreated"].ToString();
                        String reson = sdr["CreditNoteReason"].ToString();
                        String Notes = sdr["Notes"].ToString();
                        String Status = sdr["Status"].ToString();
                        String CreatedBy = sdr["CreatedBy"].ToString();




                        strOrder = strDateCreated + "|" + reson + "|" + Notes + "|" + Status + "|" + CreatedBy;
                    }
                }
            }
            conn.Close();

            return strOrder;

        }

        //Get Credit Note by Credit Note ID
        public List<CreditNoteItem> getCreditNoteList(int CreditNoteID)
        {
            var listItems = new List<CreditNoteItem>();
            String strOrder = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select * from CreditNotes where CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        var obj = new CreditNoteItem();
                        obj.strDateCreated = sdr["DateCreated"].ToString();
                        obj.reson = sdr["CreditNoteReason"].ToString();
                        obj.Notes = sdr["Notes"].ToString();
                        obj.Status = sdr["Status"].ToString();
                        obj.CreatedBy = sdr["CreatedBy"].ToString();

                        listItems.Add(obj);
                    }
                }
            }
            conn.Close();

            return listItems;

        }

        public CreditNoteObj getCreditNoteObj(int CreditNoteID)
        {
            var obj = new CreditNoteObj();
            String strOrder = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select * from CreditNotes where CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        obj.NoteId = CreditNoteID.ToString();
                        while (sdr.Read())
                        {
                            obj.IsAvail = true;
                            if (sdr["CreditNoteReason"] != DBNull.Value)
                                obj.Reason = sdr["CreditNoteReason"].ToString();
                            if (sdr["Notes"] != DBNull.Value)
                                obj.Notes = sdr["Notes"].ToString();
                            obj.Status = sdr["Status"].ToString();
                            obj.CompID = sdr["CompID"].ToString();
                            obj.OrderId = sdr["OrderID"].ToString();
                            obj.ContactId = sdr["ContactID"].ToString();
                            obj.XeroCreditNoteID = sdr["XeroCreditNoteID"].ToString();
                        }
                    }
                }
            }
            conn.Close();

            return obj;

        }

        public List<CreditNoteItemEle> getCreditNoteItemsByCreditId(int creditNoteID)
        {
            String strOrderitems = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var list = new List<CreditNoteItemEle>();
            String strSqlStmt = "SELECT * FROM dbo.CreditNote_Item WHERE CreditNoteID=" + creditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var eleItm = new CreditNoteItemEle();
                        var notID = sdr["CreditNoteItem_ID"].ToString();
                        var desc = sdr["Description"].ToString();
                        var uAm = sdr["UnitAmount"].ToString();
                        var cog = sdr["COG"].ToString();
                        var scCode = sdr["SupplierCode"].ToString();
                        var qty = sdr["Quantity"].ToString();
                        var sname = sdr["SupplierName"].ToString();
                        var rqty = "0";
                        if (sdr["RQty"] != DBNull.Value)
                            rqty = (sdr["RQty"].ToString());

                        eleItm.RQty = rqty.ToString();
                        eleItm.CreditNoteItem_ID = notID;
                        eleItm.Description = desc;
                        eleItm.UnitAmount = uAm;
                        eleItm.COG = cog;
                        eleItm.SupplierCode = scCode;
                        eleItm.Quantity = qty;
                        eleItm.SupplierName = sname;
                        list.Add(eleItm);
                    }
                }

            }
            conn.Close();

            return list;

        }

        public class CreditNoteObj
        {
            public string NoteId { get; set; }
            public string Reason { get; set; }
            public string Notes { get; set; }
            public string Status { get; set; }
            public string CompID { get; set; }
            public string OrderId { get; set; }
            public string ContactId { get; set; }
            public bool IsAvail { get; set; }
            public string XeroCreditNoteID { get; set; }
        }

        public class CreditNoteItem
        {
            public string strDateCreated { get; set; }
            public string reson { get; set; }
            public string Notes { get; set; }
            public string Status { get; set; }
            public string CreatedBy { get; set; }

        }

        public class CreditNoteItemEle
        {
            public string CreditNoteItem_ID { get; set; }
            public string Description { get; set; }
            public string UnitAmount { get; set; }
            public string COG { get; set; }
            public string Quantity { get; set; }
            public string SupplierCode { get; set; }
            public string SupplierName { get; set; }
            public string RQty { get; set; }
        }

        /// <summary>
        /// Fetch Credit NoteID
        /// </summary>
        /// <param name="creditnoteid"></param>
        /// <returns></returns>
        public String FetchCreditNoteID(int creditnoteid)
        {
            String XeroCreditNoteId = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT XeroCreditNoteID FROM CreditNotes WHERE CreditNote_ID=" + creditnoteid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        XeroCreditNoteId = sdr["XeroCreditNoteID"].ToString();
                        //Get the Entity with all Details using EF

                    }
                }

            }
            conn.Close();
            return XeroCreditNoteId;

        }

        //Method returns if the credit is on split Commission
        public String getCommishSplit(int creditnoteid)
        {
            String strOutput = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT CommishSplit FROM CreditNotes WHERE CreditNote_ID=" + creditnoteid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOutput = sdr["CommishSplit"].ToString();

                    }
                }

            }
            conn.Close();
            return strOutput;

        }

        //Method returns the Account Owner ID
        public String getAccountOwnerID(int creditnoteid)
        {
            String strOutput = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT AccountOwnerID FROM CreditNotes WHERE CreditNote_ID=" + creditnoteid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOutput = sdr["AccountOwnerID"].ToString();

                    }
                }

            }
            conn.Close();
            return strOutput;

        }


        //Method returns the Account Owner 
        public String getAccountOwner(int creditnoteid)
        {
            String strOutput = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT AccountOwner FROM CreditNotes WHERE CreditNote_ID=" + creditnoteid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOutput = sdr["AccountOwner"].ToString();

                    }
                }

            }
            conn.Close();
            return strOutput;

        }

        //Method returns the Salesperson ID
        public String getSalespersonID(int creditnoteid)
        {
            String strOutput = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT SalespersonID FROM CreditNotes WHERE CreditNote_ID=" + creditnoteid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOutput = sdr["SalespersonID"].ToString();

                    }
                }

            }
            conn.Close();
            return strOutput;

        }

        //Method returns the Salesperson
        public String getSalesperson(int creditnoteid)
        {
            String strOutput = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT Salesperson FROM CreditNotes WHERE CreditNote_ID=" + creditnoteid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOutput = sdr["Salesperson"].ToString();

                    }
                }

            }
            conn.Close();
            return strOutput;

        }

        // Method return's the number of Ongoing RMAs
        public String getNumberPendingRMA()
        {
            String strOutput = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT COUNT(*) AS TotCount FROM RMATracking WHERE Status NOT IN ('COMPLETED')";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOutput = sdr["TotCount"].ToString();

                    }
                }

            }
            conn.Close();
            return strOutput;
        }

        //Method return's the number of Pending Credits
        public String getNumberCreditPending()
        {
            String strOutput = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT COUNT(*) AS TotCount FROM CreditNotes WHERE Status = 'PENDING'";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOutput = sdr["TotCount"].ToString();

                    }
                }

            }
            conn.Close();
            return strOutput;
        }

        /// <summary>
        /// Fetch CreditNoteGuid given by CreditNoteID
        /// </summary>
        /// <param name="creditnoteid"></param>
        /// <returns></returns>
        public String getCreditNote_XeroGuid(int creditnoteid)
        {

            String xeroGuid = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT XeroGuid FROM CreditNotes WHERE CreditNote_ID=" + creditnoteid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        xeroGuid = sdr["XeroGuid"].ToString();
                        //Get the Entity with all Details using EF

                    }
                }

            }
            conn.Close();
            return xeroGuid;

        }


        //Fetch the Order Details relate to credit Notes
        public String getOrderDetails(String strOrderId)
        {
            String strOrderDetails = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT * FROM dbo.Orders WHERE OrderID=" + strOrderId;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOrderDetails = strOrderDetails + sdr["SuppDeltems"].ToString() + ":" + sdr["ProItems"].ToString() + ":" + sdr["CusDelCostItems"].ToString() + ":" + sdr["XeroInvoiceNumber"].ToString();

                    }
                }

            }
            conn.Close();

            return strOrderDetails;

        }

        //This Function Creates the Credit Notes
        public int CreateCreditNote(int CompID, int ContactID, float Total, String StrDelCost, String strCreatedBy, int OrderID, String strStatus, String strReason, String strNotes, String suppdelitems, String cusdelitems, float SubTotal, float SplitVolume, String AccountOwnerID, String AccountOwner, String SalespersonID, String Salesperson, String CommishSplit)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "insert Into CreditNotes(CompID,ContactID,Total,DelCost,DateCreated,CreatedBy,OrderID,Status,CreditNoteReason,Notes,SuppDelItems,CusDelCostItems, SubTotal, SplitVolumeAmount, AccountOwnerID, AccountOwner, SalespersonID, Salesperson, CommishSplit, ProcessTrackingStatus) values(@CompID,@ContactID,@Total,@DelCost,CURRENT_TIMESTAMP,@CreatedBy,@OrderID,@Status,@Reason,@Notes,@suppdelitems,@customerdelitems,@SubTotal, @SplitVolumeAmount, @AccountOwnerID, @AccountOwner, @SalespersonID, @Salesperson, @CommishSplit, @ProcessTrackingStatus)";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@CompID", CompID);
            cmd.Parameters.AddWithValue("@ContactID", ContactID);
            cmd.Parameters.AddWithValue("@Total", Total);
            cmd.Parameters.AddWithValue("@DelCost", StrDelCost);
            cmd.Parameters.AddWithValue("@CreatedBy", strCreatedBy);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@Status", strStatus);
            cmd.Parameters.AddWithValue("@Reason", strReason);
            cmd.Parameters.AddWithValue("@Notes", strNotes);
            //Modified here Add Parameters ,
            cmd.Parameters.AddWithValue("@suppdelitems", suppdelitems);
            cmd.Parameters.AddWithValue("@customerdelitems", cusdelitems);
            cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
            cmd.Parameters.AddWithValue("@SplitVolumeAmount", SplitVolume);
            cmd.Parameters.AddWithValue("@AccountOwnerID", AccountOwnerID);
            cmd.Parameters.AddWithValue("@AccountOwner", AccountOwner);
            cmd.Parameters.AddWithValue("@SalespersonID", SalespersonID);
            cmd.Parameters.AddWithValue("@Salesperson", Salesperson);
            cmd.Parameters.AddWithValue("@CommishSplit", CommishSplit);
            cmd.Parameters.AddWithValue("@ProcessTrackingStatus", "ONGOING");
            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                intRowsEffected = -1;
            }

            return intRowsEffected;
        }


        //This method fetch the Newly Created CreditNoteID given by Creator
        public int FetchLastCreatedCreaditNoteID(String strCratedBy)
        {
            int CreditNoteID = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "select Top 1 * from CreditNotes where CreatedBy='" + strCratedBy + "'Order by  DateCreated Desc";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        CreditNoteID = Int32.Parse(sdr["CreditNote_ID"].ToString());

                    }
                }

            }
            conn.Close();

            return CreditNoteID;

        }

        //Function writes a supplier into RMA database
        public void WriteSupplierIntoRMA(String CID, String SupplierName)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String SqlStmt = "INSERT INTO dbo.RMATracking (CreditNoteID, Raised, RaisedDateTime, SentToSupplier, ApprovedRMA, CreditInXero, RMAToCustomer, AdjustedNoteFromSupplier, Status, SupplierName) VALUES (@CreditNoteID, @Raised, CURRENT_TIMESTAMP, @SentToSupplier, @ApprovedRMA, @CreditInXero, @RMAToCustomer, @AdjustedNoteFromSupplier, @Status, @SupplierName)";
            SqlCommand cmd = new SqlCommand(SqlStmt, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", CID);
            cmd.Parameters.AddWithValue("@Raised", 1);
            cmd.Parameters.AddWithValue("@SentToSupplier", 0);
            cmd.Parameters.AddWithValue("@ApprovedRMA", 0);
            cmd.Parameters.AddWithValue("@CreditInXero", 0);
            cmd.Parameters.AddWithValue("@RMAToCustomer", 0);
            cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplier", 0);
            cmd.Parameters.AddWithValue("@Status", "IN PROGRESS");
            cmd.Parameters.AddWithValue("@SupplierName", SupplierName);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
            }
        }

        public string RMAStatusByCreditIdAndSuppllier(String CID, String SupplierName)
        {

            var rmaStatus = "";

            String xeroGuid = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT Status FROM RMATracking WHERE SupplierName=@SupplierName and CreditNoteID=@CreditNoteID";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Parameters.AddWithValue("@SupplierName", SupplierName);
                cmd.Parameters.AddWithValue("@CreditNoteID", CID);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        rmaStatus = sdr["Status"].ToString();
                        //Get the Entity with all Details using EF

                    }
                }

            }
            conn.Close();
            return rmaStatus;

        }

        public string RMAStatusByRMAID(String CID)
        {

            var rmaStatus = "";

            String xeroGuid = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "SELECT Status FROM RMATracking WHERE  RMAID=@RMAID";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Parameters.AddWithValue("@RMAID", CID);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        rmaStatus = sdr["Status"].ToString();
                        //Get the Entity with all Details using EF

                    }
                }

            }
            conn.Close();
            return rmaStatus;

        }

        public List<string> GetSuppliersForCreditNoteById(String CreditNoteID)
        {
            var listEle = new List<string>();

            String AllSuppliers = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "SELECT DISTINCT SupplierName FROM dbo.CreditNote_Item WHERE CreditNoteID = " + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        listEle.Add(sdr["SupplierName"].ToString());
                    }
                }

            }

            //string joined = string.Join(",", listEle);

            return listEle;

        }

        //Function returns the individual suppliers for the order
        public void BuildRMAList(String OrderID, String CreditNoteID)
        {
            String AllSuppliers = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "SELECT DISTINCT SupplierName FROM dbo.CreditNote_Item WHERE CreditNoteID = " + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        AllSuppliers = sdr["SupplierName"].ToString() + "|";
                    }
                }

                int Length = AllSuppliers.Length;
                AllSuppliers = AllSuppliers.Substring(0, (Length - 1));
            }

            String[] SeparateSupps = AllSuppliers.Split('|');

            foreach (String s in SeparateSupps)
            {
                String PlainSupplier = s.Trim(new Char[] { ',' });
                String FormattedSupplier = PlainSupplier.Trim();
                if (FormattedSupplier != "delivery" || FormattedSupplier != "")
                {
                    WriteSupplierIntoRMA(CreditNoteID, FormattedSupplier);
                }

            }
        }


        //This Function inserts items to Credit Note Table
        public int CreateCreditNoteItems(int CreditNoteID, String strDescription, int Quanity, float UntiAmout, float COG, String SuppCode, String strCreatedBy, String SupplierName)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = " INSERT into dbo.CreditNote_Item(CreditNoteID,Description,Quantity,UnitAmount,COG,SupplierCode,DateCreated,CreatedBy,SupplierName)  values(@creditNoteID,@Description,@Quanity,@UnitAmount,@COG,@SupplierCode,CURRENT_TIMESTAMP,@strCreatedBy,@suppname);";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@creditNoteID", CreditNoteID);
            cmd.Parameters.AddWithValue("@Description", strDescription);
            cmd.Parameters.AddWithValue("@Quanity", Quanity);
            cmd.Parameters.AddWithValue("@UnitAmount", UntiAmout);
            cmd.Parameters.AddWithValue("@COG", COG);
            cmd.Parameters.AddWithValue("@SupplierCode", SuppCode);
            cmd.Parameters.AddWithValue("@strCreatedBy", strCreatedBy);
            cmd.Parameters.AddWithValue("@suppname", SupplierName); //Modified here Add Supplier Name

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                intRowsEffected = -1;
            }

            return intRowsEffected;

        }

        //This Funciton Update the Credit Note with XeroGuid and XeroCreditNoteID
        public int UpdateCreditNotes(int CreditNoteID, String xeroGuid, String xeroCreditNoteID)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "UPDATE  CreditNotes SET XeroGuid=@guid,XeroCreditNoteID=@xeroCreditNoteID WHERE CreditNote_ID=@CreditNoteID;";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@xeroCreditNoteID", xeroCreditNoteID);
            cmd.Parameters.AddWithValue("@guid", xeroGuid);
            cmd.Parameters.AddWithValue("@CreditNoteID", CreditNoteID);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                intRowsEffected = -1;
            }


            return intRowsEffected;
        }


        //This function Fetch all the approved credit Notes 
        public String getAppprovedCreditNotes(int CompanyID)
        {
            String strOutput = "{\"aaData\":[";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "SElect cn.* ,ct.*,co.* from CreditNotes cn,dbo.Contacts ct, dbo.Companies co where  cn.CompID=co.CompanyID and cn.ContactID=ct.ContactID and cn.CompID=" + CompanyID + "and cn.Status='APPROVED'";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            String strContactName = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                            String CreditNoteID = sdr["CreditNote_ID"].ToString();
                            String CreatedDate = sdr["DateCreated"].ToString();
                            String Total = sdr["Total"].ToString();
                            String CreatedBy = sdr["CreatedBy"].ToString();
                            String OrderID = sdr["OrderID"].ToString();
                            String strStatus = sdr["Status"].ToString();
                            String strView = "<img src='Images/Edit.png'  onclick='ViewCreditNote(" + CreditNoteID + ")'/>";


                            strOutput = strOutput + "[\"" + CreditNoteID + "\"," + "\"" + OrderID + "\"," + "\"" + CreatedDate + "\"," + "\"" + strContactName + "\"," + "\"" + "$" + Total + "\"," + "\"" + CreatedBy + "\"," + "\"" + strStatus + "\"," + "\"" + strView + "\"],";

                        }
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));
                    }
                }
                strOutput = strOutput + "]}";

            }
            conn.Close();

            return strOutput;

        }



        //This Function fetch All the Creadit Notes given by CompanyID
        public String getCreditNotes(int CompanyID)
        {

            String strOutput = "{\"aaData\":[";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "SElect cn.* ,ct.*,co.* from CreditNotes cn,dbo.Contacts ct, dbo.Companies co where  cn.CompID=co.CompanyID and cn.ContactID=ct.ContactID and cn.CompID=" + CompanyID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            String strContactName = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                            String CreditNoteID = sdr["CreditNote_ID"].ToString();
                            String XeroCreditNoteID = sdr["XeroCreditNoteID"].ToString();
                            String CreatedDate = sdr["DateCreated"].ToString();
                            String Total = sdr["Total"].ToString();
                            String CreatedBy = sdr["CreatedBy"].ToString();
                            String OrderID = sdr["OrderID"].ToString();
                            String strStatus = sdr["Status"].ToString();

                            //Modified here 

                            String strContactID = sdr["ContactID"].ToString();
                            String strCompanyID = sdr["CompanyID"].ToString();


                            String strView = "<img src='Images/Edit.png'  onclick='ViewCreditNote(" + CreditNoteID + "," + strContactID + ")'/>";


                            strOutput = strOutput + "[\"" + CreditNoteID + "\"," + "\"" + XeroCreditNoteID + "\"," + "\"" + OrderID + "\"," + "\"" + CreatedDate + "\"," + "\"" + strContactName + "\"," + "\"" + "$" + Total + "\"," + "\"" + CreatedBy + "\"," + "\"" + strStatus + "\"," + "\"" + strView + "\"],";

                        }
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));
                    }
                }
                strOutput = strOutput + "]}";

            }
            conn.Close();

            return strOutput;
        }


        //This Method get the SavedCreditNote given by CreditNoteID
        public String getSavedCreditNote(int CreditNoteID)
        {
            String strCreditNote = String.Empty;


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "select CN.*,CT.*,CM.CompanyName,O.XeroGUID,O.XeroInvoiceNumber from dbo.CreditNotes CN,dbo.Orders O,dbo.Contacts CT, dbo.Companies CM where CN.OrderID = O.OrderID and  CN.ContactID =CT.ContactID  and CN.CompID=CM.CompanyID  and  CN.CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        String OrderID = sdr["OrderID"].ToString();
                        String strXeroInvoiceNumber = sdr["XeroInvoiceNumber"].ToString();
                        String strXeroGuid = sdr["XeroGUID"].ToString();
                        String strCreditNoteid = sdr["CreditNote_ID"].ToString();
                        String strContact = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                        String strCompnay = sdr["CompanyName"].ToString();
                        String strDateCreated = sdr["DateCreated"].ToString().Substring(0, 10);
                        String strStatus = sdr["Status"].ToString();

                        String addressBuilder = sdr["STREET_AddressLine1"] + " " + sdr["STREET_City"] + " " + sdr["STREET_Region"] + " " + sdr["STREET_PostalCode"] + " " + sdr["STREET_Country"];

                        //String strAddress
                        strCreditNote = strCreditNote + OrderID + ":" + strXeroInvoiceNumber + ":" + strXeroGuid + ":" + strCreditNoteid + ":" + strContact + ":" + strCompnay + ":" + strDateCreated + ":" + addressBuilder + ":" + strStatus;

                    }
                }

            }
            conn.Close();
            return strCreditNote;
        }



        //This Method get CreditNote Items given by CreditNoteID
        public String getSavedCreditNoteItems(int CreditNoteID)
        {

            String sterCreditNoteItems = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlStmt = "select * from dbo.CreditNote_Item where CreditNoteID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        sterCreditNoteItems = sterCreditNoteItems + sdr["CreditNoteItem_ID"].ToString() + ":" + sdr["Description"].ToString() + ":" + sdr["Quantity"].ToString() + ":" + sdr["UnitAmount"].ToString() + ":" + sdr["SupplierCode"].ToString() + ":" + sdr["COG"].ToString();
                        sterCreditNoteItems = sterCreditNoteItems + "|";
                    }
                }

            }
            conn.Close();

            return sterCreditNoteItems;
        }


        //This Method Update the Credit Note Status 
        public int UpdateCreditNoteStatus(int CreditNoteID)
        {
            int RowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "UPDATE  CreditNotes SET  Status='APPROVED' WHERE CreditNote_ID=@CreditNoteID;";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", CreditNoteID);

            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = -1;
            }


            return RowsEffected;
        }





    }
}
