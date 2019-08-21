using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Data;


namespace DeltoneCRM_DAL
{
    public class QuoteDAL
    {

        private String CONNSTRING;

        public QuoteDAL(String connString)
        {
            CONNSTRING = connString;
        }


        public int InsertQuoteReassign(String strCompanyID, String strContactID, float COGTotal, float COGSubTotal,
           float ProfitTotal, float ProfitSubtotal, float SuppDelCost, float CusDelCost,
           float ProItemCost, String strCreatedBy, String strSuppDelItems, String strProItems,
           String strCusDelCostItems, String srOwnershipAdminID, String QuoteNotesStr, String PaymentTermsStr,
           String TypeOfCall, String EmailHeader, String EmailFooter, String Flag, String QuoteStatus,
           String QuoteByID, int quoteCategory, DateTime? callBackdate, string reasonWhathappened,string notes)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String sqlOrderedItemStmt = "insert into dbo.Quote(QuoteDateTime,QuoteBy,CompanyID,ContactID,COGTotal, " +
             "  COGSubTotal, SupplierDelCost, CustomerDelCost, ProItemCost, SuppDelItems, ProItems, CusDelCostItems, " +
           " AccountOwnerName, PaymentTerms, TypeOfcall, Status, Total, SubTotal, EmailHeader, EmailFooter, Flag, " +

            "QuoteByID,QuoteCategory,CallBackDate,SellStatusReason,Notes) " +
           " values (CURRENT_TIMESTAMP,@QuoteBy,@CompanyID,@ContactID, @COGTotal, @COGSubTotal, @SupplierDelCost, " +
          "  @CustomerDelCost, @ProItemCost, @SuppDelItems, @ProItems, @CusDelCostItems, @OwnershipAdminID, @PaymentTerms, " +
          "  @TypeOfcall, @Status, @ProfitTotal, @ProfitSubTotal, @EmailHeader, @EmailFooter, @Flag, @QuoteByID,@QuoteCategory,@CallBackDate,@sellStatusReason,@notes);";
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            cmd.Parameters.AddWithValue("@QuoteBy", strCreatedBy);
            cmd.Parameters.AddWithValue("@CompanyID", strCompanyID);
            cmd.Parameters.AddWithValue("@ContactID", strContactID);
            cmd.Parameters.AddWithValue("@COGTotal", COGTotal);
            cmd.Parameters.AddWithValue("@COGSubTotal", COGSubTotal);
            cmd.Parameters.AddWithValue("@ProfitTotal", ProfitTotal);
            cmd.Parameters.AddWithValue("@ProfitSubtotal", ProfitSubtotal);
            cmd.Parameters.AddWithValue("@SupplierDelCost", SuppDelCost);
            cmd.Parameters.AddWithValue("@CustomerDelCost", CusDelCost);
            cmd.Parameters.AddWithValue("@ProItemCost", ProItemCost);
            cmd.Parameters.AddWithValue("@SuppDelItems", strSuppDelItems);
            cmd.Parameters.AddWithValue("@ProItems", strProItems);
            cmd.Parameters.AddWithValue("@CusDelCostItems", strCusDelCostItems);
            cmd.Parameters.AddWithValue("@OwnershipAdminID", srOwnershipAdminID);
            cmd.Parameters.AddWithValue("@PaymentTerms", PaymentTermsStr);
            cmd.Parameters.AddWithValue("@TypeOfcall", TypeOfCall);
            cmd.Parameters.AddWithValue("@EmailHeader", EmailHeader);
            cmd.Parameters.AddWithValue("@EmailFooter", EmailFooter);
            cmd.Parameters.AddWithValue("@Flag", Flag);
            cmd.Parameters.AddWithValue("@Status", QuoteStatus);
            cmd.Parameters.AddWithValue("@QuoteByID", QuoteByID);
            cmd.Parameters.AddWithValue("@notes", notes);
            cmd.Parameters.AddWithValue("@QuoteCategory", quoteCategory);
            cmd.Parameters.AddWithValue("@sellStatusReason", reasonWhathappened);
            if (callBackdate == null)
                cmd.Parameters.AddWithValue("@CallBackDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@CallBackDate", callBackdate);

            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                /*If an Exception Occurres  Write the Exception details to the Database including FileName,Method,Detail Error and and LoggedUser if want ,Create a Table and Write to Table if fails Email*/

            }

            return rowEffected;
        }

        public int LastQuoteIdByName(String strCreatedBy)
        {
            int intOrderID = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlStmt = "SELECT TOP 1 * FROM dbo.Quote WHERE QuoteBy='" + strCreatedBy + "'ORDER BY QuoteID Desc";
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
                            intOrderID = Int32.Parse(sdr["QuoteID"].ToString());
                        }
                    }
                    else
                    {
                        intOrderID = 0;
                    }

                }

            }
            conn.Close();

            return intOrderID;
        }


        //This Method Insert Quote given by Details
        public int InsertQuote(String strCompanyID, String strContactID, float COGTotal, float COGSubTotal,
            float ProfitTotal, float ProfitSubtotal, float SuppDelCost, float CusDelCost,
            float ProItemCost, String strCreatedBy, String strSuppDelItems, String strProItems,
            String strCusDelCostItems, String srOwnershipAdminID, String QuoteNotesStr, String PaymentTermsStr,
            String TypeOfCall, String EmailHeader, String EmailFooter, String Flag, String QuoteStatus,
            String QuoteByID, int quoteCategory, DateTime? callBackdate, string reasonWhathappened)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String sqlOrderedItemStmt = "insert into dbo.Quote(QuoteDateTime,QuoteBy,CompanyID,ContactID,COGTotal, " +
             "  COGSubTotal, SupplierDelCost, CustomerDelCost, ProItemCost, SuppDelItems, ProItems, CusDelCostItems, " +
           " AccountOwnerName, PaymentTerms, TypeOfcall, Status, Total, SubTotal, EmailHeader, EmailFooter, Flag, QuoteByID,QuoteCategory,CallBackDate,SellStatusReason) " +
           " values (CURRENT_TIMESTAMP,@QuoteBy,@CompanyID,@ContactID, @COGTotal, @COGSubTotal, @SupplierDelCost, " +
          "  @CustomerDelCost, @ProItemCost, @SuppDelItems, @ProItems, @CusDelCostItems, @OwnershipAdminID, @PaymentTerms, " +
          "  @TypeOfcall, @Status, @ProfitTotal, @ProfitSubTotal, @EmailHeader, @EmailFooter, @Flag, @QuoteByID,@QuoteCategory,@CallBackDate,@sellStatusReason);";
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            cmd.Parameters.AddWithValue("@QuoteBy", strCreatedBy);
            cmd.Parameters.AddWithValue("@CompanyID", strCompanyID);
            cmd.Parameters.AddWithValue("@ContactID", strContactID);
            cmd.Parameters.AddWithValue("@COGTotal", COGTotal);
            cmd.Parameters.AddWithValue("@COGSubTotal", COGSubTotal);
            cmd.Parameters.AddWithValue("@ProfitTotal", ProfitTotal);
            cmd.Parameters.AddWithValue("@ProfitSubtotal", ProfitSubtotal);
            cmd.Parameters.AddWithValue("@SupplierDelCost", SuppDelCost);
            cmd.Parameters.AddWithValue("@CustomerDelCost", CusDelCost);
            cmd.Parameters.AddWithValue("@ProItemCost", ProItemCost);
            cmd.Parameters.AddWithValue("@SuppDelItems", strSuppDelItems);
            cmd.Parameters.AddWithValue("@ProItems", strProItems);
            cmd.Parameters.AddWithValue("@CusDelCostItems", strCusDelCostItems);
            cmd.Parameters.AddWithValue("@OwnershipAdminID", srOwnershipAdminID);
            cmd.Parameters.AddWithValue("@PaymentTerms", PaymentTermsStr);
            cmd.Parameters.AddWithValue("@TypeOfcall", TypeOfCall);
            cmd.Parameters.AddWithValue("@EmailHeader", EmailHeader);
            cmd.Parameters.AddWithValue("@EmailFooter", EmailFooter);
            cmd.Parameters.AddWithValue("@Flag", Flag);
            cmd.Parameters.AddWithValue("@Status", QuoteStatus);
            cmd.Parameters.AddWithValue("@QuoteByID", QuoteByID);
            cmd.Parameters.AddWithValue("@QuoteCategory", quoteCategory);
            cmd.Parameters.AddWithValue("@sellStatusReason", reasonWhathappened);
            if (callBackdate==null)
            cmd.Parameters.AddWithValue("@CallBackDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@CallBackDate", callBackdate);

            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                /*If an Exception Occurres  Write the Exception details to the Database including FileName,Method,Detail Error and and LoggedUser if want ,Create a Table and Write to Table if fails Email*/

            }

            return rowEffected;
        }

        // This method returns which database to query for the company/contact details
        public String getWhichDBToQuery(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Flag FROM dbo.Quote WHERE QuoteID=" + QuoteID;

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
                            OutPut = sdr["Flag"].ToString();
                        }
                    }

                }
            }
            conn.Close();
            return OutPut;
        }

        // Method returns the total cost of the quote
        public String getTotal(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Total FROM dbo.Quote WHERE QuoteID=" + QuoteID;

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
                            OutPut = sdr["Total"].ToString();
                        }
                    }

                }
            }
            conn.Close();
            return OutPut;
        }

        //Method to retrieve Email Header for the quote
        public String getEmailHeaderText(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT EmailHeader FROM dbo.Quote WHERE QuoteID=" + QuoteID;

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
                            OutPut = sdr["EmailHeader"].ToString();
                        }
                    }

                }
            }
            conn.Close();
            return OutPut;
        }

        // Method returns the cost of shipping for the quote
        public String getShippingCost(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT CustomerDelCost FROM dbo.Quote WHERE QuoteID=" + QuoteID;

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
                            OutPut = sdr["CustomerDelCost"].ToString();
                        }
                    }

                }
            }
            conn.Close();
            return OutPut;
        }


        //Method to retrieve Email Footer for the quote
        public String getEmailFooterText(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT EmailFooter FROM dbo.Quote WHERE QuoteID=" + QuoteID;

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
                            OutPut = sdr["EmailFooter"].ToString();
                        }
                    }

                }
            }
            conn.Close();
            return OutPut;
        }

        public int ConvertQuoteToOrder(int QuoteID)
        {
            // Build value holders
            int readQuoteID = 0;
            DateTime readQuotedatetime = new DateTime();
            String readQuotedBy = String.Empty;
            int readCompanyID = 0;
            int readContactID = 0;
            String readNotes = String.Empty;
            float readTotal = 0;
            float readSubTotal = 0;
            float readCOGTotal = 0;
            float readCOGSubTotal = 0;
            float readSuppDelCost = 0;
            float readCustomerDelCost = 0;
            float readProItemCost = 0;
            String readSuppDelItems = String.Empty;
            String readProItems = String.Empty;
            String readCustDelCostItems = String.Empty;
            String readAccountOwner = String.Empty;
            String readPaymentTerms = String.Empty;
            String readReference = String.Empty;
            String readTypeOfCall = String.Empty;
            String readStatus = String.Empty;

            String SavedOrderID = String.Empty;
            // End Value Holders

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT TOP 1 * FROM dbo.Quote WHERE QuoteID = " + QuoteID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        readQuoteID = Int32.Parse(sdr["QuoteID"].ToString());
                        readQuotedBy = sdr["QuoteBy"].ToString();
                        readCompanyID = Int32.Parse(sdr["CompanyID"].ToString());
                        readContactID = Int32.Parse(sdr["ContactID"].ToString());
                        readNotes = sdr["Notes"].ToString();
                        readTotal = float.Parse(sdr["Total"].ToString());
                        readSubTotal = float.Parse(sdr["Subtotal"].ToString());
                        readCOGTotal = float.Parse(sdr["COGTotal"].ToString());
                        readCOGSubTotal = float.Parse(sdr["COGSubTotal"].ToString());
                        readSuppDelCost = float.Parse(sdr["SupplierDelCost"].ToString());
                        readCustomerDelCost = float.Parse(sdr["CustomerDelCost"].ToString());
                        readProItemCost = float.Parse(sdr["ProItemCost"].ToString());
                        readSuppDelItems = sdr["SuppDelItems"].ToString();
                        readProItems = sdr["ProItems"].ToString();
                        readCustDelCostItems = sdr["CusDelCostItems"].ToString();
                        readAccountOwner = sdr["AccountOwnerName"].ToString();
                        readPaymentTerms = sdr["PaymentTerms"].ToString();
                        readReference = sdr["Reference"].ToString();
                        readTypeOfCall = sdr["TypeOfCall"].ToString();
                        readStatus = sdr["Status"].ToString();
                    }
                }
            }
            conn.Close();

            ContactDAL contactdal = new ContactDAL(CONNSTRING);

            String readXeroGUID = String.Empty;
            readXeroGUID = contactdal.getXeroGuid_ForContact(readContactID);

            int rowEffected = -1;
            SqlConnection conn2 = new SqlConnection();
            conn2.ConnectionString = CONNSTRING;
            String SqlStmt = "INSERT INTO dbo.Orders (CompanyID, ContactID, XeroGUID, DueDate, Reference, Status, Total, SubTotal, OrderedDateTime, CreatedDateTime, CreatedBy, COGSubTotal, COGTotal, SupplierDelCost, CustomerDelCost, ProItemCost, SuppDeltems, ProItems, CusDelCostItems, OrderedBy, Notes, PaymentTerms, TypeOfCall, QuoteNumber) VALUES (@CompanyID, @ContactID, @XeroGUID, CURRENT_TIMESTAMP, @Reference, 'QUOTED', @Total, @SubTotal, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, @CreatedBy, @COGSubTotal, @COGTotal, @SupplierDelCost, @CustomerDelCost, @ProItemCost, @SuppDelItems, @ProItems, @CusDelCostItems, @OrderedBy, @Notes, @PaymentTerms, @TypeOfCall, @QuoteID)";
            SqlCommand cmd2 = new SqlCommand(SqlStmt, conn2);

            cmd2.Parameters.AddWithValue("@CompanyID", readCompanyID);
            cmd2.Parameters.AddWithValue("@ContactID", readContactID);
            cmd2.Parameters.AddWithValue("@XeroGUID", readXeroGUID);
            cmd2.Parameters.AddWithValue("@Reference", readReference);
            cmd2.Parameters.AddWithValue("@Total", readTotal);
            cmd2.Parameters.AddWithValue("@SubTotal", readSubTotal);
            cmd2.Parameters.AddWithValue("@CreatedBy", readQuotedBy);
            cmd2.Parameters.AddWithValue("@COGSubTotal", readCOGSubTotal);
            cmd2.Parameters.AddWithValue("@COGTotal", readCOGTotal);
            cmd2.Parameters.AddWithValue("@SupplierDelCost", readSuppDelCost);
            cmd2.Parameters.AddWithValue("@CustomerDelCost", readCustomerDelCost);
            cmd2.Parameters.AddWithValue("@ProItemCost", readProItemCost);
            cmd2.Parameters.AddWithValue("@SuppDelItems", readSuppDelItems);
            cmd2.Parameters.AddWithValue("@ProItems", readProItems);
            cmd2.Parameters.AddWithValue("@CusDelCostItems", readCustDelCostItems);
            cmd2.Parameters.AddWithValue("@OrderedBy", readQuotedBy);
            cmd2.Parameters.AddWithValue("@Notes", readNotes);
            cmd2.Parameters.AddWithValue("@PaymentTerms", readPaymentTerms);
            cmd2.Parameters.AddWithValue("@TypeOfCall", readTypeOfCall);
            cmd2.Parameters.AddWithValue("@QuoteID", QuoteID);

            try
            {
                conn2.Open();
                rowEffected = cmd2.ExecuteNonQuery();
                conn2.Close();

            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
            }

            String sqlOrderID = "SELECT OrderID FROM dbo.Orders WHERE QuoteNumber = " + QuoteID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sqlOrderID;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        SavedOrderID = sdr["OrderID"].ToString();
                    }
                }
                conn.Close();
            }

            String sqlIndividualItems = "SELECT * FROM dbo.Quote_Item WHERE QuoteID = " + QuoteID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sqlIndividualItems;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        WriteQuoteItemsToOrder(Int32.Parse(SavedOrderID), sdr["Description"].ToString(), sdr["ItemCode"].ToString(), sdr["UnitAmount"].ToString(), sdr["Quantity"].ToString(), sdr["CreatedBy"].ToString(), sdr["COGAmount"].ToString(), sdr["SupplierCode"].ToString(), sdr["SupplierName"].ToString());
                    }
                }
            }

            return rowEffected;
        }

        public int WriteQuoteItemsToOrder(int OrderID, String Description, String ItemCode, String UnitAmount, String Quantity, String CreatedBy, String COGAmount, String SupplierCode, String SupplierName)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String sqlItems = "INSERT INTO dbo.Ordered_Items (OrderID, Description, ItemCode, UnitAmount, Quantity, CreatedDateTime, CreatedBy, COGAmount, SupplierCode, SupplierName) VALUES (@OrderID, @Description, @ItemCode, @UnitAmount, @Quantity, CURRENT_TIMESTAMP, @CreatedBy, @COGAmount, @SupplierCode, @SupplierName)";
            SqlCommand cmd = new SqlCommand(sqlItems, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
            cmd.Parameters.AddWithValue("@UnitAmount", UnitAmount);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@COGAmount", COGAmount);
            cmd.Parameters.AddWithValue("@SupplierCode", SupplierCode);
            cmd.Parameters.AddWithValue("@SupplierName", SupplierName);

            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
            }

            return rowEffected;
        }

        public int CreateQuoteItems(int QuoteID, String strItemDesc, String strItemCode, float UnitAmout, int quantity, float COGAmount, String strCratedBy, String strSupplierItemCode, String strSuppName)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String sqlOrderedItemStmt = "insert into dbo.Quote_Item(QuoteID,Description,ItemCode,UnitAmount,Quantity,CreatedDateTime,CreatedBy,COGamount,SupplierCode,SupplierName)values (@QuoteID,@ItemDescription,@ItemCode,@UnitAmout,@qty,CURRENT_TIMESTAMP,@CreatedBy,@COGAmout,@SupplierCode,@SuppName);";
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            cmd.Parameters.AddWithValue("@QuoteID", QuoteID);
            cmd.Parameters.AddWithValue("@ItemDescription", strItemDesc);
            cmd.Parameters.AddWithValue("@ItemCode", strItemCode);
            cmd.Parameters.AddWithValue("@UnitAmout", UnitAmout);
            cmd.Parameters.AddWithValue("@qty", quantity);
            cmd.Parameters.AddWithValue("@CreatedBy", strCratedBy);
            cmd.Parameters.AddWithValue("@COGAmout", COGAmount);
            cmd.Parameters.AddWithValue("@SupplierCode", strSupplierItemCode);
            cmd.Parameters.AddWithValue("@SuppName", strSuppName);

            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                /*If an Exception Occurres  Write the Exception details to the Database including FileName,Method,Detail Error and and LoggedUser if want ,Create a Table and Write to Table if fails Email*/

            }

            return rowEffected;
        }

        public class QuoteItemAssign
        {
            public string Description { get; set; }
            public string QuoteID { get; set; }
            public string ItemCode { get; set; }
            public string UnitAmount { get; set; }
            public string Quantity { get; set; }
            public string CreatedBy { get; set; }
            public string COGAmount { get; set; }
            public string SupplierCode { get; set; }
            public string CreatedDateTime { get; set; }
            public string SupplierName { get; set; }
        }

        public class QuoteCompanyAssign
        {
            public string QuoteDateTime { get; set; }
            public string QuoteBy { get; set; }
            public string QuoteId { get; set; }
            public string CompanyID { get; set; }
            public string ContactID { get; set; }
            public string COGTotal { get; set; }
            public string COGSubTotal { get; set; }
            public string SupplierDelCost { get; set; }
            public string CustomerDelCost { get; set; }
            public string ProItemCost { get; set; }
            public string SuppDelItems { get; set; }
            public string ProItems { get; set; }
            public string CusDelCostItems { get; set; }
            public string AccountOwnerName { get; set; }
            public string PaymentTerms { get; set; }
            public string TypeOfcall { get; set; }
            public string Status { get; set; }
            public string Total { get; set; }
            public string SubTotal { get; set; }
            public string EmailHeader { get; set; }
            public string EmailFooter { get; set; }
            public string Flag { get; set; }
            public string QuoteByID { get; set; }
            public string QuoteCategory { get; set; }
            public string CallBackDate { get; set; }
            public string SellStatusReason { get; set; }
            public string Notes { get; set; }

            public List<QuoteItemAssign> ItemsQuote { get; set; }
        }

        //This Method Insert a Quote Items 
        public String insertQuoteItems()
        {
            String Output = String.Empty;
            //Insert Into Quote_Item(QuoteID,Description,ItemCode,UnitAmount,Quantity,CreatedDateTime,CreatedBy,COGamount,SupplierCode) values(12,'LC133 black','LC133',4.50,2,CURRENT_TIMESTAMP,'TestUser',4.00,'Cgk1233');
            return Output;
        }

        //This Method Get the Quote by Quote ID
        public String getQuote(int QuoteId)
        {
            String output = String.Empty;


            return output;

        }

        public String getContactFirstName(String ContactID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select FirstName from dbo.Contacts where ContactID=" + ContactID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["FirstName"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }

        public String getQuoteStatus(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlOrderStmt = String.Empty;

            strSqlOrderStmt = "SELECT Status FROM dbo.Quote WHERE QuoteID =" + QuoteID;

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

        public String getQuoteCategory(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlOrderStmt = String.Empty;

            strSqlOrderStmt = "SELECT QuoteCategory FROM dbo.Quote WHERE QuoteID =" + QuoteID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["QuoteCategory"] != DBNull.Value)
                            OutPut = sdr["QuoteCategory"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }

        public void UpdateQuoteCategory(int quoteId,int categoryID)
        {
            var sqlConn = new SqlConnection();
            sqlConn.ConnectionString = CONNSTRING;
            var query = "UPDATE dbo.Quote SET  QuoteCategory=@quoteCategory,AlteredDateTime=CURRENT_TIMESTAMP WHERE QuoteID=@quoteID";
            var sold = categoryID; //status sold 3 

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = sqlConn;
                sqlConn.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@quoteCategory", SqlDbType.Int).Value = sold;
                cmd.Parameters.AddWithValue("@quoteID", SqlDbType.Int).Value = quoteId;
                cmd.ExecuteNonQuery();

            }
            sqlConn.Close();

        }
        public String getQuoteCategoryCallBackDate(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlOrderStmt = String.Empty;

            strSqlOrderStmt = "SELECT CallBackDate FROM dbo.Quote WHERE QuoteID =" + QuoteID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["CallBackDate"] != DBNull.Value)
                            OutPut = Convert.ToDateTime(sdr["CallBackDate"]).ToString("yyyy-MM-dd"); 
                    }
                }
            }
            conn.Close();
            return OutPut;
        }

        public int cancelQuote(String QuoteID)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String sqlOrderedItemStmt = "UPDATE dbo.Quote SET Status = 'CANCELLED' WHERE QuoteID = " + QuoteID;
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                /*If an Exception Occurres  Write the Exception details to the Database including FileName,Method,Detail Error and and LoggedUser if want ,Create a Table and Write to Table if fails Email*/

            }

            return rowEffected;
        }

        //This method return the person who created the quote
        public String getQuoteCreatedBy(int QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlOrderStmt = String.Empty;

            strSqlOrderStmt = "SELECT QuoteBy FROM dbo.Quote WHERE QuoteID =" + QuoteID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["QuoteBy"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }

        public String checkIfEmailHasBeenPreviouslySent(String QuoteID)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlOrderStmt = String.Empty;

            strSqlOrderStmt = "SELECT EmailHasBeenSent FROM dbo.Quote WHERE QuoteID =" + QuoteID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OutPut = sdr["EmailHasBeenSent"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }

        public String getNumberPendingQuotes()
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlOrderStmt = String.Empty;

            strSqlOrderStmt = "SELECT COUNT(*) As TotCount FROM dbo.Quote WHERE Status = 'PENDING'";

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
                            OutPut = sdr["TotCount"].ToString();
                        }
                    }
                    else
                    {
                        OutPut = "0";
                    }

                }
            }
            conn.Close();
            return OutPut;
        }

        public int FlagAsEmailSent(String QuoteID)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String sqlOrderedItemStmt = "UPDATE dbo.Quote SET EmailHasBeenSent = 1 WHERE QuoteID = " + QuoteID;
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                /*If an Exception Occurres  Write the Exception details to the Database including FileName,Method,Detail Error and and LoggedUser if want ,Create a Table and Write to Table if fails Email*/

            }

            return rowEffected;
        }

        public int approveQuote(String QuoteID)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String sqlOrderedItemStmt = "UPDATE dbo.Quote SET Status = 'ACTIVE' WHERE QuoteID = " + QuoteID;
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                /*If an Exception Occurres  Write the Exception details to the Database including FileName,Method,Detail Error and and LoggedUser if want ,Create a Table and Write to Table if fails Email*/

            }

            return rowEffected;
        }

        public String getNewContactFirstName(String ContactID, String WhichDB)
        {
            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlOrderStmt = String.Empty;

            if (WhichDB == "QuoteDB" || WhichDB == "")
            {
                strSqlOrderStmt = "select FirstName from dbo.Quote_Contacts where ContactID=" + ContactID;
            }
            else
            {
                strSqlOrderStmt = "select FirstName from dbo.Contacts where ContactID=" + ContactID;
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
                        OutPut = sdr["FirstName"].ToString();
                    }
                }
            }
            conn.Close();
            return OutPut;
        }

        //Method returns the account owner's Login ID
        public String getRealAccountOwnerID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT QuoteByID FROM dbo.Quote WHERE QuoteID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["QuoteByID"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        public String getCompayID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT CompanyID FROM dbo.Quote WHERE QuoteID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
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

        //Method returns the account owner's Login Name
        public String getRealAccountOwner(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strQuery = "SELECT QuoteBy FROM dbo.Quote WHERE QuoteID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["QuoteBy"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }

        //Return Order Created Date
        public String getOrderCreatedDate(int OrderID)
        {
            String output = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT QuoteDateTime FROM Quote WHERE QuoteID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["QuoteDateTime"].ToString();
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
            String strSqlOrderStmt = "SELECT QuoteBy from dbo.Quote where QuoteID = " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["QuoteBy"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method return's the Auto Quote Approve Status
        public String getQuoteAutoApproveStatus(int LID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT SettingValue from dbo.Login_Settings where LoginID = " + LID + " AND SettingName = 'QuoteApprove'";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                   
                  
                    while (sdr.Read())
                    {

                        output = sdr["SettingValue"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        //This method return's if the Quote Email has been sent previously
        public String getQuoteEmailAlready(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT EmailHasBeenSent from dbo.Quote where QuoteID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["EmailHasBeenSent"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        // Return's the Contact Person email address
        public String getContactPersonEmail(int OID)
        {

            //Find which database the contact is
            String WhichDB = String.Empty;
            String strSqlOrderStmt = String.Empty;
            WhichDB = getWhichDBToQuery(OID);

            if (WhichDB == "LiveDB")
            {
                strSqlOrderStmt = "SELECT FirstName + ' ' + LastName AS FullName, Email FROM dbo.Contacts WHERE ContactID IN (SELECT ContactID FROM dbo.Quote WHERE QuoteID = " + OID + ")";
            }
            else
            {
                strSqlOrderStmt = "SELECT FirstName + ' ' + LastName AS FullName, Email FROM dbo.Quote_Contacts WHERE ContactID IN (SELECT ContactID FROM dbo.Quote WHERE QuoteID = " + OID + ")";
            }

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;


            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["Email"].ToString() + "|" + sdr["FullName"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        public void InsertQuoteAllocate(int quoteId, int userId, DateTime createDate, DateTime expiryDate)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSQLInsertStmt = "insert into dbo.QuoteAllocate(QuoteId,UserId,CreatedDate,ExpiryDate) " +
             " values (@QuoteId,@UserId,@createDate,@expiryDate);";

            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@QuoteId", quoteId);
            cmd.Parameters.AddWithValue("@createDate", createDate);
            cmd.Parameters.AddWithValue("@expiryDate", expiryDate);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

        }

        public List<QuoteCompanyAssign> GetAllQuotesByCompany(int comId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT  * FROM dbo.Quote WHERE CompanyID = " + comId;
            var listQuotesByCompany = new List<QuoteCompanyAssign>();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var QuoteCompanyAssign = new QuoteCompanyAssign();
                        QuoteCompanyAssign.QuoteId =(sdr["QuoteID"].ToString());
                        QuoteCompanyAssign.QuoteDateTime = sdr["QuoteDateTime"].ToString();
                        QuoteCompanyAssign.QuoteBy = sdr["QuoteBy"].ToString();
                        QuoteCompanyAssign.CompanyID = (sdr["CompanyID"].ToString());
                        QuoteCompanyAssign.ContactID = (sdr["ContactID"].ToString());
                        QuoteCompanyAssign.Notes = sdr["Notes"].ToString();
                        QuoteCompanyAssign.Total = (sdr["Total"].ToString());
                        QuoteCompanyAssign.SubTotal = (sdr["Subtotal"].ToString());
                        QuoteCompanyAssign.COGTotal = (sdr["COGTotal"].ToString());
                        QuoteCompanyAssign.COGSubTotal = (sdr["COGSubTotal"].ToString());
                        QuoteCompanyAssign.SupplierDelCost = (sdr["SupplierDelCost"].ToString());
                        QuoteCompanyAssign.CustomerDelCost = (sdr["CustomerDelCost"].ToString());
                        QuoteCompanyAssign.ProItemCost = (sdr["ProItemCost"].ToString());
                        QuoteCompanyAssign.SuppDelItems = sdr["SuppDelItems"].ToString();
                        QuoteCompanyAssign.ProItems = sdr["ProItems"].ToString();
                        QuoteCompanyAssign.CusDelCostItems = sdr["CusDelCostItems"].ToString();
                        QuoteCompanyAssign.AccountOwnerName = sdr["AccountOwnerName"].ToString();
                        QuoteCompanyAssign.PaymentTerms = sdr["PaymentTerms"].ToString();
                        QuoteCompanyAssign.EmailHeader = sdr["EmailHeader"].ToString();
                        QuoteCompanyAssign.EmailFooter = sdr["EmailFooter"].ToString();
                        QuoteCompanyAssign.Status = sdr["Status"].ToString();
                        QuoteCompanyAssign.TypeOfcall = sdr["TypeOfcall"].ToString();
                        QuoteCompanyAssign.QuoteByID = sdr["QuoteByID"].ToString();
                        QuoteCompanyAssign.QuoteCategory = sdr["QuoteCategory"].ToString();
                        QuoteCompanyAssign.CallBackDate = sdr["CallBackDate"].ToString();
                        QuoteCompanyAssign.SellStatusReason = sdr["SellStatusReason"].ToString();

                        QuoteCompanyAssign.ItemsQuote = new List<QuoteItemAssign>();
                        if(!string.IsNullOrEmpty(QuoteCompanyAssign.QuoteId)){
                            var QuoteItem = GetQuoteItemsbyQuoteID(Convert.ToInt32(QuoteCompanyAssign.QuoteId));
                            QuoteCompanyAssign.ItemsQuote = QuoteItem;
                        }
                        listQuotesByCompany.Add(QuoteCompanyAssign);
                    }
                }
            }
            conn.Close();

            return listQuotesByCompany;

        }

        protected List<QuoteItemAssign> GetQuoteItemsbyQuoteID(int quoteId)
        {

            String strOrderitems = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var quoteAssignlist = new List<QuoteItemAssign>();
           
            String strSqlStmt = @"select OT.* , IT.PriceLock, IT.ReportedFaulty,IT.Quantity as itemQty,IT.COG as priceCog from dbo.Quote_Item OT 
                               INNER JOIN dbo.Items IT ON IT.ItemID = OT.ItemCode where OT.QuoteID=" + quoteId;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var quoteAssign = new QuoteItemAssign();
                        quoteAssign.QuoteID = quoteId.ToString();
                        quoteAssign.ItemCode = sdr["ItemCode"].ToString();
                        quoteAssign.Description = sdr["Description"].ToString();
                        quoteAssign.UnitAmount = sdr["UnitAmount"].ToString();
                        quoteAssign.COGAmount = sdr["COGamount"].ToString();
                        quoteAssign.Quantity = sdr["Quantity"].ToString();
                        quoteAssign.CreatedDateTime = sdr["CreatedDateTime"].ToString();
                        quoteAssign.CreatedBy = sdr["CreatedBy"].ToString();
                        quoteAssign.SupplierCode = sdr["SupplierCode"].ToString();
                        quoteAssign.SupplierName = sdr["SupplierName"].ToString();
                        quoteAssignlist.Add(quoteAssign);
                       
                    }
                }

            }

            conn.Close();

            return quoteAssignlist;

        }

        public void DeleteQuoteRecentAllocate(List<QuoteAssignOne> listObj, int repId)
        {
            foreach (var item in listObj)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = CONNSTRING;
                var strSqlContactStmt = @"DELETE FROM QuoteAllocate WHERE QuoteId = @QuoteId AND UserId=@userId ";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.Add("@QuoteId", SqlDbType.Int).Value = item.QuoteId;
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = repId;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
        public class QuoteAssignOne
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
            public int CompanyId { get; set; }
        }

        public class QuoteAssignData
        {
            public int QuoteId { get; set; }
            public int UserId { get; set; }
            public DateTime ExpiryDate { get; set; }
            public DateTime CreatedDate { get; set; }
        }


        public List<QuoteAssignData> GetAssignQuotes()
        {

            var listCom = new List<QuoteAssignData>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = @"SELECT UserId,QuoteId,CreatedDate,ExpiryDate FROM QuoteAllocate ";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            var comLea = new QuoteAssignData();

                            comLea.UserId = Convert.ToInt32(sdr["UserId"].ToString());
                            comLea.QuoteId = Convert.ToInt32(sdr["QuoteId"].ToString());
                            comLea.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString());
                            comLea.ExpiryDate = Convert.ToDateTime(sdr["ExpiryDate"].ToString());
                            listCom.Add(comLea);
                        }
                    }
                }
            }
            conn.Close();

            return listCom;
        }

    }
}
