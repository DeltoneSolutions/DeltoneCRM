using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using DeltoneCRM_DAL;


/* This Function contains all Methods Relate Data Access functions*/
namespace DeltoneCRM
{
    public class DeltoneCRMDAL
    {

        public DeltoneCRMDAL()
        {
            //Create a Deltone Connection Object 
        }

        public String CompanyNameCount(String compname)
        {

            String strItem = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strSqlOrderStmt = "SELECT  COUNT(*)as Comp_Count FROM dbo.Companies WHERE CompanyName ='" + compname + "'";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strItem = sdr["Comp_Count"].ToString();

                    }
                }
            }
            conn.Close();
            return strItem;

        }

        public String GetCompanyIdByCompanyName(string compname)
        {

            String strItem = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strSqlOrderStmt = "SELECT CompanyID  FROM dbo.Companies WHERE CompanyName ='" + compname + "'";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strItem = sdr["CompanyID"].ToString();

                    }
                }
            }
            conn.Close();
            return strItem;
        }



        public String AddNewCompanyNotExists(String Company, String Website, int AccountOwner, int loggedinUserId = 0)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;

            SqlConnection conn = new SqlConnection();
            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            conn.ConnectionString = connectionstring;
            var columnName = "Company Table All columns";
            var talbeName = "Companies";
            var ActionType = "Created";
            int primaryKey = 0;



            String strSQLInsertStmt = "insert into dbo.CompaniesNotExists(CompanyName,CompanyWebsite,OwnershipAdminID, " +
           " Active, CreatedBy,CreatedDateTime, DefaultPaymentTerms) values (@CompanyName,@CompanyWebsite,@OwnershipAdminID, 'Y','SYSTEM',CURRENT_TIMESTAMP, '21');";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@CompanyName", Company);
            cmd.Parameters.AddWithValue("@CompanyWebsite", Website);
            cmd.Parameters.AddWithValue("@OwnershipAdminID", AccountOwner);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                msg = intRowsEffected.ToString();


                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
                msg = ex.Message.ToString();
            }

            String LastestCompanyID = String.Empty;
            SqlConnection conn2 = new SqlConnection();
            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlLastCompStmt = "SELECT CompanyID FROM dbo.CompaniesNotExists WHERE CompanyName = '" + Company + "'";
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = strSqlLastCompStmt;
                cmd2.Connection = conn2;
                conn2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LastestCompanyID = sdr["CompanyID"].ToString();
                    }
                }
            }

            conn2.Close();

            return LastestCompanyID;
        }

        public String AddNewCompany(String Company, String Website, int AccountOwner, int loggedinUserId = 0)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;

            SqlConnection conn = new SqlConnection();
            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            conn.ConnectionString = connectionstring;
            var columnName = "Company Table All columns";
            var talbeName = "Companies";
            var ActionType = "Created";
            int primaryKey = 0;



            String strSQLInsertStmt = "insert into dbo.Companies(CompanyName,CompanyWebsite,OwnershipAdminID, " +
           " Active, CreatedBy,CreatedDateTime, DefaultPaymentTerms) values (@CompanyName,@CompanyWebsite,@OwnershipAdminID, 'Y','SYSTEM',CURRENT_TIMESTAMP, '21');";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@CompanyName", Company);
            cmd.Parameters.AddWithValue("@CompanyWebsite", Website);
            cmd.Parameters.AddWithValue("@OwnershipAdminID", AccountOwner);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                msg = intRowsEffected.ToString();


                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
                msg = ex.Message.ToString();
            }

            String LastestCompanyID = String.Empty;
            SqlConnection conn2 = new SqlConnection();
            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlLastCompStmt = "SELECT CompanyID FROM dbo.Companies WHERE CompanyName = '" + Company + "'";
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = strSqlLastCompStmt;
                cmd2.Connection = conn2;
                conn2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LastestCompanyID = sdr["CompanyID"].ToString();
                    }
                }
            }

            var accoutOwnerName = new LoginDAL(connectionstring).getLoginNameFromID(AccountOwner.ToString());
            var newvalues = "Company Name " + Company + " Website " + Website + " and AccountOwner " + accoutOwnerName;
            primaryKey = Convert.ToInt32(LastestCompanyID);
            new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", newvalues, loggedinUserId, conn, 0,
           columnName, talbeName, ActionType, primaryKey, primaryKey);

            return LastestCompanyID;
        }

        //this Method Add new Contact in DeltoneCRM System and returns that Contact with companyID
        public String AddNewContact(int CompID, String FirstName, String LastName, String DefaultAreaCode,
            String DefaultNumber, String MobileNumber, String EmailAddy, String ShipLine1, String ShipLine2,
            String ShipCity, String ShipState, String ShipPostcode, String BillLine1, String BillLine2, String BillCity,
            String BillState, String BillPostcode, String PrimaryContact, int loggedinuserId = 0)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;
            SqlConnection conn = new SqlConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            conn.ConnectionString = connectionString;

            var columnName = "Contact Table All columns";
            var talbeName = "Contact";
            var ActionType = "Created";
            int primaryKey = 0;

            String strSQLInsertStmt = "INSERT into dbo.Contacts(CompanyID,FirstName,LastName, PrimaryContact, DEFAULT_AreaCode,DEFAULT_Number, " +
           " MOBILE_Number, Email, STREET_AddressLine1, STREET_AddressLine2, STREET_City, STREET_PostalCode, STREET_Region, POSTAL_AddressLine1, " +
           " POSTAL_AddressLine2, POSTAL_City, POSTAL_PostalCode, POSTAL_Region, Active) values (@CompID,@FirstName,@LastName,@PrimaryContact," +
             "   @DefaultAreaCode, @DefaultNumber, @MobileNumber, @EmailAddy, @ShipLine1, @ShipLine2, @ShipCity, @ShipPostcode, @ShipState, " +
              "  @BillLine1, @BillLine2, @BillCity, @BillPostcode, @BillState, 'Y');";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@CompID", CompID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@PrimaryContact", PrimaryContact);
            cmd.Parameters.AddWithValue("@DefaultAreaCode", DefaultAreaCode);
            cmd.Parameters.AddWithValue("@DefaultNumber", DefaultNumber);
            cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
            cmd.Parameters.AddWithValue("@EmailAddy", EmailAddy);
            cmd.Parameters.AddWithValue("@ShipLine1", ShipLine1);
            cmd.Parameters.AddWithValue("@ShipLine2", ShipLine2);
            cmd.Parameters.AddWithValue("@ShipCity", ShipCity);
            cmd.Parameters.AddWithValue("@ShipPostcode", ShipPostcode);
            cmd.Parameters.AddWithValue("@ShipState", ShipState);
            cmd.Parameters.AddWithValue("@BillLine1", BillLine1);
            cmd.Parameters.AddWithValue("@BillLine2", BillLine2);
            cmd.Parameters.AddWithValue("@BillCity", BillCity);
            cmd.Parameters.AddWithValue("@BillPostcode", BillPostcode);
            cmd.Parameters.AddWithValue("@BillState", BillState);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                msg = intRowsEffected.ToString();

                conn.Close();




            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
                msg = ex.Message.ToString();
            }

            String LastestCompanyID = String.Empty;
            String LatestContactID = String.Empty;
            String LatestCompanyName = String.Empty;
            String LatestCompanyWebSite = String.Empty;

            SqlConnection conn2 = new SqlConnection();
            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlLastCompStmt = "SELECT cn.ContactID,cm.CompanyID,cm.CompanyName,cm.CompanyWebsite FROM dbo.Contacts cn,dbo.Companies cm WHERE   cm.CompanyID=cn.CompanyID AND cn.CompanyID = '" + CompID + "' AND cn.FirstName = '" + FirstName + "' AND cn.LastName = '" + LastName + "'";
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = strSqlLastCompStmt;
                cmd2.Connection = conn2;
                conn2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LastestCompanyID = sdr["CompanyID"].ToString();
                        LatestContactID = sdr["ContactID"].ToString();
                        LatestCompanyName = sdr["CompanyName"].ToString();
                        LatestCompanyWebSite = sdr["CompanyWebsite"].ToString();
                    }
                }
            }

            var companyName = new CompanyDAL(connectionString).getCompanyNameByID(CompID.ToString());
            var newvalues = "Company Name " + companyName + " Contacts, First Name " + FirstName + " and Last Name " + LastName;
            primaryKey = Convert.ToInt32(LatestContactID);
            new DeltoneCRM_DAL.CompanyDAL(connectionString).CreateActionONAuditLog("", newvalues, loggedinuserId, conn, 0,
           columnName, talbeName, ActionType, primaryKey, CompID);

            String finalStr = String.Empty;
            finalStr = LastestCompanyID + ":" + LatestContactID + ":" + LatestCompanyName + ":" + LatestCompanyWebSite;

            return finalStr;
        }


        public String AddNewContactNotExists(int CompID, String FirstName, String LastName, String DefaultAreaCode,
      String DefaultNumber, String MobileNumber, String EmailAddy, String ShipLine1, String ShipLine2,
      String ShipCity, String ShipState, String ShipPostcode, String BillLine1, String BillLine2, String BillCity,
      String BillState, String BillPostcode, String PrimaryContact, int loggedinuserId = 0)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;
            SqlConnection conn = new SqlConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            conn.ConnectionString = connectionString;

            var columnName = "Contact Table All columns";
            var talbeName = "Contact";
            var ActionType = "Created"; 
            int primaryKey = 0;

            String strSQLInsertStmt = "INSERT into dbo.ContactsNotExists(CompanyID,FirstName,LastName, PrimaryContact, DEFAULT_AreaCode,DEFAULT_Number, " +
           " MOBILE_Number, Email, STREET_AddressLine1, STREET_AddressLine2, STREET_City, STREET_PostalCode, STREET_Region, POSTAL_AddressLine1, " +
           " POSTAL_AddressLine2, POSTAL_City, POSTAL_PostalCode, POSTAL_Region, Active) values (@CompID,@FirstName,@LastName,@PrimaryContact," +
             "   @DefaultAreaCode, @DefaultNumber, @MobileNumber, @EmailAddy, @ShipLine1, @ShipLine2, @ShipCity, @ShipPostcode, @ShipState, " +
              "  @BillLine1, @BillLine2, @BillCity, @BillPostcode, @BillState, 'Y');";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@CompID", CompID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@PrimaryContact", PrimaryContact);
            cmd.Parameters.AddWithValue("@DefaultAreaCode", DefaultAreaCode);
            cmd.Parameters.AddWithValue("@DefaultNumber", DefaultNumber);
            cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
            cmd.Parameters.AddWithValue("@EmailAddy", EmailAddy);
            cmd.Parameters.AddWithValue("@ShipLine1", ShipLine1);
            cmd.Parameters.AddWithValue("@ShipLine2", ShipLine2);
            cmd.Parameters.AddWithValue("@ShipCity", ShipCity);
            cmd.Parameters.AddWithValue("@ShipPostcode", ShipPostcode);
            cmd.Parameters.AddWithValue("@ShipState", ShipState);
            cmd.Parameters.AddWithValue("@BillLine1", BillLine1);
            cmd.Parameters.AddWithValue("@BillLine2", BillLine2);
            cmd.Parameters.AddWithValue("@BillCity", BillCity);
            cmd.Parameters.AddWithValue("@BillPostcode", BillPostcode);
            cmd.Parameters.AddWithValue("@BillState", BillState);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                msg = intRowsEffected.ToString();

                conn.Close();




            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
                msg = ex.Message.ToString();
            }

            String LastestCompanyID = String.Empty;
            String LatestContactID = String.Empty;
            String LatestCompanyName = String.Empty;
            String LatestCompanyWebSite = String.Empty;

            SqlConnection conn2 = new SqlConnection();
            conn2.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlLastCompStmt = "SELECT cn.ContactID,cm.CompanyID,cm.CompanyName,cm.CompanyWebsite FROM dbo.ContactsNotExists cn,dbo.CompaniesNotExists cm WHERE   cm.CompanyID=cn.CompanyID AND cn.CompanyID = '" + CompID + "' AND cn.FirstName = '" + FirstName + "' AND cn.LastName = '" + LastName + "'";
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = strSqlLastCompStmt;
                cmd2.Connection = conn2;
                conn2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LastestCompanyID = sdr["CompanyID"].ToString();
                        LatestContactID = sdr["ContactID"].ToString();
                        LatestCompanyName = sdr["CompanyName"].ToString();
                        LatestCompanyWebSite = sdr["CompanyWebsite"].ToString();
                    }
                }
            }


            conn2.Close();
            String finalStr = String.Empty;
            finalStr = LastestCompanyID + ":" + LatestContactID + ":" + LatestCompanyName + ":" + LatestCompanyWebSite;

            return finalStr;
        }

        public String AddNewTarget(String StaffMember, float Commission, int WorkingDays, String Month, String Year)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "insert into dbo.Targets(LoginID,TargetMonth,TargetYear, TargetCommission, WorkingDays, CreatedBy,CreatedDateTime) values (@LoginID,@TargetMonth,@TargetYear,@TargetCommission, @WorkingDays,'SYSTEM',CURRENT_TIMESTAMP);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@LoginID", StaffMember);
            cmd.Parameters.AddWithValue("@TargetMonth", Month);
            cmd.Parameters.AddWithValue("@TargetYear", Year);
            cmd.Parameters.AddWithValue("@TargetCommission", Commission);
            cmd.Parameters.AddWithValue("@WorkingDays", WorkingDays);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                msg = intRowsEffected.ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
                msg = ex.Message.ToString();
            }

            return msg;
        }

        public String AddNewLogin(String FirstName, String LastName, String Username, String Password, int AcccessLevel, String EmailAddy, int Department)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "insert into dbo.Logins(Username,Password,FirstName, LastName, AccessLevel, Active, CreatedBy,CreatedDateTime, EmailAddress,Department) values (@Username,@Password,@FirstName,@LastName, @AccessLevel,'Y','SYSTEM',CURRENT_TIMESTAMP, @EmailAddress,@Department);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@AccessLevel", AcccessLevel);
            cmd.Parameters.AddWithValue("@EmailAddress", EmailAddy);
            cmd.Parameters.AddWithValue("@Department", Department);
            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                msg = intRowsEffected.ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
                msg = ex.Message.ToString();
            }

            return msg;
        }


        //This Method Returns the LatestItem given by Details
        public String getLatestItem(String itemCode, String OEMCode, String description)
        {

            String strItem = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strSqlOrderStmt = "select * from dbo.Items where Description='" + description + "' AND OEMCode='" + OEMCode + "' AND SupplierItemCode='" + itemCode + "'";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strItem = sdr["ItemID"].ToString() + ":" + sdr["Description"].ToString() + ":" + sdr["COG"].ToString() + ":" + sdr["OEMCode"].ToString();

                    }
                }
            }
            conn.Close();
            return strItem;
        }


        //This method Add NewItem given by details 
        public int AddNewItem(String SupplierID, String ItemCode, String Description,
            float cog, float resellprice, String BestPrice, String Faulty, int qty, double? dsb)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = @"insert into dbo.Items(SupplierID,SupplierItemCode, Description,
                                   COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, ReportedFaulty,Quantity,DSB) 
                                  values (@SupplierID,@ItemCode,@Description, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP,
                                  @PriceLock, @ReportedFaulty,@Quantity,@dsb);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@COG", cog);
            cmd.Parameters.AddWithValue("@ResellPrice", resellprice);
            cmd.Parameters.AddWithValue("@PriceLock", BestPrice);
            cmd.Parameters.AddWithValue("@ReportedFaulty", Faulty);
            cmd.Parameters.AddWithValue("@Quantity", qty);
            if (dsb == null)
                cmd.Parameters.AddWithValue("@dsb", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@dsb", dsb);
            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            return intRowsEffected;
        }


        /* This Method Fetch the Guid associates with the item*/
        public String getXeroGuid(int ItemID)
        {
            String strItem = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strSqlOrderStmt = "select XeroGuid from Items where ItemID=" + ItemID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strItem = sdr["XeroGuid"].ToString();

                    }
                }
            }
            conn.Close();
            return strItem;
        }



        /*This Method Write Xero Details to Table*/
        public String UpdateWithXeroGuid(int ItemID, String Guid)
        {

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "Update dbo.Items  set XeroGuid=@xeroguid Where ItemID=@itemid";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@xeroguid", Guid);
            cmd.Parameters.AddWithValue("@itemid", ItemID);
            try
            {
                conn.Open();
                output = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            return output;
        }
        /*End Method Write Xero Details to Table*/


        public int AddNewDeliveryItem(String strNewItem, float newcost)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "insert into dbo.DeliveryDetails(deliverydetails,deliverycost,Active,CreatedBy,CreatedDateTime) values (@ItemName,@ItemCost,'Y','SYSTEM',CURRENT_TIMESTAMP);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@ItemName", strNewItem);
            cmd.Parameters.AddWithValue("@ItemCost", newcost);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            return intRowsEffected;
        }

        // Method to add new Promotional Item
        public int AddNewPromoItem(String strNewItem, float newcost, int defaultqty, float shippingcost)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "insert into dbo.PromoDetails(promoitem,promocost,promoqty,shippingcost,Active,CreatedBy,CreatedDateTime,AlteredBy,AlterationDateTime) values (@ItemName,@ItemCost,@DefaultQty,@ShippingCost,'Y','SYSTEM',CURRENT_TIMESTAMP,null,null);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@ItemName", strNewItem);
            cmd.Parameters.AddWithValue("@ItemCost", newcost);
            cmd.Parameters.AddWithValue("@DefaultQty", defaultqty);
            cmd.Parameters.AddWithValue("@ShippingCost", shippingcost);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            return intRowsEffected;
        }

        public void AddSupplierDetails()
        {

            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var insertStatement = @"INSERT INTO dbo.Suppliers() VALUES ()";
        }

        //This Method Add a New Supplier 
        public int AddNewSupplier(String strSupplierName, float StandardDeliveryCost)
        {

            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "insert into dbo.Suppliers(SupplierName,StandardDeliveryCost,Active,CreatedBy,CreatedDateTime,AlteredBy,AlterationDateTime) values (@SupplierName,@DeliveryCost,'Y','SYSTEM',CURRENT_TIMESTAMP,null,null);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@SupplierName", strSupplierName);
            cmd.Parameters.AddWithValue("@DeliveryCost", StandardDeliveryCost);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            return intRowsEffected;

        }

        public void AddNewSupplier(SupplierObj obj)
        {

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            string strquery = @"INSERT INTO dbo.Suppliers(SupplierName,StandardDeliveryCost,ContactName,PhoneNumber,
                                Address,City,State,PostCode,SalesEmail,ReturnEmail,AccountsPhoneNumber,AccountsEmail,
                             Notes,UserName,Password,Active,CreatedBy,CreatedDateTime) VALUES 
                                (@suppliername,@standarddeliverycost,@contactname,@phonenumber,@address,@city,@state,@postcode,@salesemail,@returnemail,
                                   @accountsphonenumber,@accountsemail,@notes,@username,@password, @active,@createdby,CURRENT_TIMESTAMP)";

            SqlCommand cmd = new SqlCommand(strquery, sqlConnection);
            cmd.Parameters.AddWithValue("@suppliername", obj.SupplierName);
            cmd.Parameters.AddWithValue("@standarddeliverycost", obj.StandardDeliveryCost);
            cmd.Parameters.AddWithValue("@contactname", obj.ContactName);
            cmd.Parameters.AddWithValue("@phonenumber", obj.PhoneNumber);
            cmd.Parameters.AddWithValue("@address", obj.Address);
            cmd.Parameters.AddWithValue("@city", obj.City);
            cmd.Parameters.AddWithValue("@state", obj.State);
            cmd.Parameters.AddWithValue("@postcode", obj.PostCode);
            cmd.Parameters.AddWithValue("@salesemail", obj.SalesEmail);
            cmd.Parameters.AddWithValue("@returnemail", obj.ReturnEmail);
            cmd.Parameters.AddWithValue("@accountsphonenumber", obj.AccountsPhoneNumber);
            cmd.Parameters.AddWithValue("@accountsemail", obj.AccountsEmail);
            cmd.Parameters.AddWithValue("@notes", obj.Notes);
            cmd.Parameters.AddWithValue("@username", obj.UserName);
            cmd.Parameters.AddWithValue("@password", obj.Password);
            cmd.Parameters.AddWithValue("@active", "Y");
            cmd.Parameters.AddWithValue("@createdby", "SYSTEM");

            try
            {
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                if (sqlConnection != null) { sqlConnection.Close(); };
                return;
            }

            var supplierId = SupplierIdByName(obj.SupplierName);


            if (!string.IsNullOrEmpty(obj.SalesEmail))
            {
                var type = "1";// sales email
                UpdateSupplierEmailForAutomatedEmailConfig(supplierId, obj.SalesEmail, type, obj.SupplierName);
            }

            if (!string.IsNullOrEmpty(obj.ReturnEmail))
            {
                var type = "2";// return email
                UpdateSupplierEmailForAutomatedEmailConfig(supplierId, obj.ReturnEmail, type, obj.SupplierName);
            }
        }

        public int SupplierIdByName(string supplierName)
        {
            SqlConnection suppemailConnection = new SqlConnection();
            suppemailConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlCommand suppEmailCommand = new SqlCommand();
            suppEmailCommand.Connection = suppemailConnection;
            suppEmailCommand.CommandText = " SELECT SupplierID from Suppliers WHERE SupplierName=@suppliername ";
            suppEmailCommand.Parameters.AddWithValue("@suppliername", supplierName);

            suppemailConnection.Open();
            var Id = 0;
            using (SqlDataReader sdr = suppEmailCommand.ExecuteReader())
            {

                if (sdr.HasRows)
                {

                    while (sdr.Read())
                    {
                        if (sdr["SupplierID"] != DBNull.Value)
                            Id = Convert.ToInt32(sdr["SupplierID"].ToString());
                    }
                    suppemailConnection.Close();
                }
            }

            return Id;
        }

        public void UpdateSupplierEmailForAutomatedEmailConfig(int supplierId, string email, string type, string name)
        {

            SqlConnection suppemailConnection = new SqlConnection();
            suppemailConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlCommand suppEmailCommand = new SqlCommand();
            suppEmailCommand.Connection = suppemailConnection;
            suppEmailCommand.CommandText = " SELECT Id from SupplierEmail where SupplierId=@supplierId and Active=@active and Type=@type";
            suppEmailCommand.Parameters.AddWithValue("@supplierId", supplierId);
            suppEmailCommand.Parameters.AddWithValue("@type", type);
            suppEmailCommand.Parameters.AddWithValue("@active", 1);
            suppemailConnection.Open();
            var Id = 0;
            using (SqlDataReader sdr = suppEmailCommand.ExecuteReader())
            {

                if (sdr.HasRows)
                {

                    while (sdr.Read())
                    {
                        if (sdr["Id"] != DBNull.Value)
                            Id = Convert.ToInt32(sdr["Id"].ToString());
                    }
                    suppemailConnection.Close();

                    SqlConnection sqlConnection = new SqlConnection();
                    sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    string strQuery = @"UPDATE SupplierEmail SET SupplierEmail=@supplieremail , SupplierName= @suppliername , Type=@type , Active=@active  WHERE Id=@id";
                    SqlCommand sqlCommand = new SqlCommand(strQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@supplieremail", email);
                    sqlCommand.Parameters.AddWithValue("@suppliername", name);
                    sqlCommand.Parameters.AddWithValue("@type", type);
                    var active = 1;
                    sqlCommand.Parameters.AddWithValue("@active", active);
                    sqlCommand.Parameters.AddWithValue("@id", Id);
                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();

                    }
                    catch (Exception ex)
                    {
                        if (sqlConnection != null) { sqlConnection.Close(); };
                        return;
                    }

                }
                else
                {
                    SqlConnection sqlConnectionIN = new SqlConnection();
                    sqlConnectionIN.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    string strQuery = @"INSERT INTO SupplierEmail (SupplierId,SupplierEmail ,SupplierName,Type,Active) VALUES (@supplierId,@supplieremail , @suppliername , @type ,@active) ";
                    SqlCommand sqlCommandIn = new SqlCommand(strQuery, sqlConnectionIN);
                    sqlCommandIn.Parameters.AddWithValue("@supplierId", supplierId);
                    sqlCommandIn.Parameters.AddWithValue("@supplieremail", email);
                    sqlCommandIn.Parameters.AddWithValue("@suppliername", name);
                    sqlCommandIn.Parameters.AddWithValue("@type", type);
                    var active = 1;
                    sqlCommandIn.Parameters.AddWithValue("@active", active);
                    try
                    {
                        sqlConnectionIN.Open();
                        sqlCommandIn.ExecuteNonQuery();
                        sqlConnectionIN.Close();

                    }
                    catch (Exception ex)
                    {
                        if (sqlConnectionIN != null) { sqlConnectionIN.Close(); };
                        return;
                    }
                }
            }






        }

        public void UpdateSupplierName(SupplierObj obj, string loggedusername)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            string strQuery = @"UPDATE dbo.Suppliers SET SupplierName=@suppliername,StandardDeliveryCost=@standarddeliverycost ,ContactName=@contactname,
                              PhoneNumber =@phonenumber ,Address=@address ,City=@city ,State=@state ,PostCode=@postcode , SalesEmail=@salesemail ,
                            ReturnEmail=@returnemail , AccountsPhoneNumber=@accountsphonenumber , AccountsEmail=@accountsemail ,Notes=@notes, UserName=@username,
                                Password=@password ,AlteredBy=@alteredby ,AlterationDateTime=@alterationdatetime WHERE SupplierId=@supplierid";
            SqlCommand sqlCommand = new SqlCommand(strQuery, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@suppliername", obj.SupplierName);
            sqlCommand.Parameters.AddWithValue("@standarddeliverycost", obj.StandardDeliveryCost);
            sqlCommand.Parameters.AddWithValue("@contactname", obj.ContactName);
            sqlCommand.Parameters.AddWithValue("@phonenumber", obj.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@address", obj.Address);
            sqlCommand.Parameters.AddWithValue("@city", obj.City);
            sqlCommand.Parameters.AddWithValue("@state", obj.State);
            sqlCommand.Parameters.AddWithValue("@postcode", obj.PostCode);
            sqlCommand.Parameters.AddWithValue("@salesemail", obj.SalesEmail);
            sqlCommand.Parameters.AddWithValue("@returnemail", obj.ReturnEmail);
            sqlCommand.Parameters.AddWithValue("@accountsphonenumber", obj.AccountsPhoneNumber);
            sqlCommand.Parameters.AddWithValue("@accountsemail", obj.AccountsEmail);
            sqlCommand.Parameters.AddWithValue("@notes", obj.Notes);
            sqlCommand.Parameters.AddWithValue("@username", obj.UserName);
            sqlCommand.Parameters.AddWithValue("@password", obj.Password);
            sqlCommand.Parameters.AddWithValue("@alteredby", loggedusername);
            sqlCommand.Parameters.AddWithValue("@alterationdatetime", DateTime.Now);
            sqlCommand.Parameters.AddWithValue("@supplierid", obj.SupplierID);
            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                if (sqlConnection != null) { sqlConnection.Close(); };
                return;
            }
            var supplierId = obj.SupplierID;
            if (!string.IsNullOrEmpty(obj.SalesEmail))
            {
                var type = "1";// sales email
                UpdateSupplierEmailForAutomatedEmailConfig(supplierId, obj.SalesEmail, type, obj.SupplierName);
            }

            if (!string.IsNullOrEmpty(obj.ReturnEmail))
            {
                var type = "2";// return email
                UpdateSupplierEmailForAutomatedEmailConfig(supplierId, obj.ReturnEmail, type, obj.SupplierName);
            }

        }

        public SupplierObj SupplierIdById(int supplierId)
        {
            SqlConnection suppemailConnection = new SqlConnection();
            suppemailConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlCommand suppEmailCommand = new SqlCommand();
            suppEmailCommand.Connection = suppemailConnection;
            suppEmailCommand.CommandText = " SELECT * from Suppliers WHERE SupplierID=@Id ";
            suppEmailCommand.Parameters.AddWithValue("@Id", supplierId);

            suppemailConnection.Open();
            var obj = new SupplierObj();

            using (SqlDataReader sdr = suppEmailCommand.ExecuteReader())
            {

                if (sdr.HasRows)
                {

                    while (sdr.Read())
                    {
                        obj.SupplierID = supplierId;
                        obj.SupplierName = (sdr["SupplierName"].ToString());
                        obj.AccountsEmail = (sdr["AccountsEmail"].ToString());
                        obj.AccountsPhoneNumber = sdr["AccountsPhoneNumber"].ToString();
                        obj.Address = sdr["Address"].ToString();
                        obj.City = sdr["City"].ToString();
                        obj.ContactName = sdr["ContactName"].ToString();
                        obj.Notes = sdr["Notes"].ToString();
                        obj.Password = sdr["Password"].ToString();
                        obj.PhoneNumber = sdr["PhoneNumber"].ToString();
                        obj.PostCode = sdr["PostCode"].ToString();
                        obj.ReturnEmail = sdr["ReturnEmail"].ToString();
                        obj.SalesEmail = sdr["SalesEmail"].ToString();
                        obj.StandardDeliveryCost = sdr["StandardDeliveryCost"].ToString();
                        obj.State = sdr["State"].ToString();
                        obj.UserName = sdr["UserName"].ToString();


                    }
                    suppemailConnection.Close();
                }
            }

            return obj;
        }




        //This Method Update an Exsisting Supplier
        public int UpdateSupplier(int SupplierID, String strName, float DeliveryCost)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLUpdateStmt = " Update dbo.Suppliers set SupplierName=@SupplierName,StandardDeliveryCost=@DeliveyCost Where SupplierID=@SupplierID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@SupplierName", strName);
            cmd.Parameters.AddWithValue("@DeliveyCost", DeliveryCost);
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);


            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }


            return intRowsEffected;
        }

        public class SupplierObj
        {
            public int SupplierID { get; set; }
            public string SupplierName { get; set; }
            public string StandardDeliveryCost { get; set; }
            public string ContactName { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostCode { get; set; }
            public string SalesEmail { get; set; }
            public string ReturnEmail { get; set; }
            public string AccountsPhoneNumber { get; set; }
            public string AccountsEmail { get; set; }
            public string Notes { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Active { get; set; }

        }

    }
}