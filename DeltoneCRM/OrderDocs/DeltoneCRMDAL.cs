using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


/* This Function contains all Methods Relate Data Access functions*/
namespace DeltoneCRM
{
    public class DeltoneCRMDAL
    {

        public DeltoneCRMDAL()
        {
            //Create a Deltone Connection Object 
        }

        public String AddNewCompany(String Company, String Website, int AccountOwner)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "insert into dbo.Companies(CompanyName,CompanyWebsite,OwnershipAdminID, Active, CreatedBy,CreatedDateTime) values (@CompanyName,@CompanyWebsite,@OwnershipAdminID, 'Y','SYSTEM',CURRENT_TIMESTAMP);";
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

            return LastestCompanyID;
        }

        //this Method Add new Contact in DeltoneCRM System and returns that Contact with companyID
        public String AddNewContact(int CompID, String FirstName, String LastName, String DefaultAreaCode, String DefaultNumber, String MobileNumber, String EmailAddy, String ShipLine1, String ShipLine2, String ShipCity, String ShipState, String ShipPostcode, String BillLine1, String BillLine2, String BillCity, String BillState, String BillPostcode, String PrimaryContact)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "INSERT into dbo.Contacts(CompanyID,FirstName,LastName, PrimaryContact, DEFAULT_AreaCode,DEFAULT_Number, MOBILE_Number, Email, STREET_AddressLine1, STREET_AddressLine2, STREET_City, STREET_PostalCode, STREET_Region, POSTAL_AddressLine1, POSTAL_AddressLine2, POSTAL_City, POSTAL_PostalCode, POSTAL_Region, Active) values (@CompID,@FirstName,@LastName,@PrimaryContact,@DefaultAreaCode, @DefaultNumber, @MobileNumber, @EmailAddy, @ShipLine1, @ShipLine2, @ShipCity, @ShipPostcode, @ShipState, @BillLine1, @BillLine2, @BillCity, @BillPostcode, @BillState, 'Y');";
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

        public String AddNewLogin(String FirstName, String LastName, String Username, String Password, int AcccessLevel, String EmailAddy)
        {
            int intRowsEffected = -1;
            String msg = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "insert into dbo.Logins(Username,Password,FirstName, LastName, AccessLevel, Active, CreatedBy,CreatedDateTime, EmailAddress) values (@Username,@Password,@FirstName,@LastName, @AccessLevel,'Y','SYSTEM',CURRENT_TIMESTAMP, @EmailAddress);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@AccessLevel", AcccessLevel);
            cmd.Parameters.AddWithValue("@EmailAddress", EmailAddy);

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

        public int AddNewItem(String SupplierID, String ItemCode, String OEMCode, String Description, float cog, float resellprice)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStmt = "insert into dbo.Items(SupplierID,SupplierItemCode,OEMCode, Description, COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime) values (@SupplierID,@ItemCode,@OEMCode,@Description, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
            cmd.Parameters.AddWithValue("@OEMCode", OEMCode);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@COG", cog);
            cmd.Parameters.AddWithValue("@ResellPrice", resellprice);


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


        //This Method Add a New Supplier 
        public int  AddNewSupplier(String strSupplierName, float StandardDeliveryCost)
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

    }
}