using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace DeltoneCRM_DAL
{

    public class ContactDAL
    {
        public String CONNSTRING;

        public ContactDAL(String strConnString)
        {
            CONNSTRING = strConnString;
        }


        //This Method retrives  the Xero Guid  from the Company for the Contact
        public String getXeroGuid_ForContact(int ContactID)
        {

            String strContact = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT cm.XeroGUID  FROM dbo.Contacts cn,dbo.Companies cm    WHERE   cn.CompanyID=cm.CompanyID AND  cn.ContactID = " + ContactID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = sdr["XeroGUID"].ToString();
                    }
                }
            }


            conn.Close();
            return strContact;
        }
        //End Method Retrives Xero Guid for the Contact

        //Method to find the first contact person of the company
        public String getContactByCompanyID(int CompanyID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT TOP 1 FirstName + ' ' + LastName AS FullName FROM dbo.Contacts WHERE CompanyID = " + CompanyID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = strContact + sdr["FullName"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;
        }

        //Method to find the contact person based on the last order for that company
        public String getContactByCompanyBasedOnLastOrder(int CompanyID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT TOP 1 ContactID FROM dbo.Orders WHERE CompanyID = " + CompanyID + " ORDER BY CreatedDatetime DESC";
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
                            strContact = strContact + sdr["ContactID"].ToString();
                        }
                        conn.Close();
                    }
                    else
                    {
                        conn.Close();
                        String strSqlNewContact = "SELECT TOP 1 ContactID FROM dbo.Contacts WHERE CompanyID = " + CompanyID;
                        using (SqlCommand cmd2 = new SqlCommand())
                        {
                            cmd2.CommandText = strSqlNewContact;
                            cmd2.Connection = conn;
                            conn.Open();

                            using (SqlDataReader sdr2 = cmd2.ExecuteReader())
                            {
                                if (sdr2.HasRows)
                                {
                                    while (sdr2.Read())
                                    {
                                        strContact = sdr2["ContactID"].ToString();
                                    }
                                }
                                else
                                {
                                    strContact = "0";
                                }
                            }
                            conn.Close();
                        }
                    }

                }
            }

            return strContact;
        }

        //Method to find contact person's full name based on ContactID
        public String getContactFullNameBasedOnContactID(String ContactID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            if (ContactID == "0")
            {
                strContact = "NO CONTACTS";
            }
            else
            {
                String strSqlContactStmt = "SELECT Firstname + ' ' + LastName AS FullName FROM dbo.Contacts WHERE ContactID = " + ContactID;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strContact = strContact + sdr["FullName"].ToString();
                        }
                    }
                }
                conn.Close();
            }


            return strContact;
        }

        public String getContact(int contactID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT * FROM dbo.Contacts WHERE ContactID = " + contactID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = strContact + sdr["FirstName"].ToString() + ":" + sdr["LastName"].ToString() + ":" + sdr["DEFAULT_AreaCode"].ToString() + ":" + sdr["DEFAULT_Number"].ToString() + ":" + sdr["MOBILE_Number"].ToString() + ":" + sdr["Email"].ToString() + ":" + sdr["STREET_AddressLine1"].ToString() + ":" + sdr["STREET_AddressLine2"].ToString() + ":" + sdr["STREET_City"].ToString() + ":" + sdr["STREET_PostalCode"].ToString() + ":" + sdr["STREET_Region"].ToString() + ":" + sdr["POSTAL_AddressLine1"].ToString() + ":" + sdr["POSTAL_AddressLine2"].ToString() + ":" + sdr["POSTAL_City"].ToString() + ":" + sdr["POSTAL_PostalCode"].ToString() + ":" + sdr["POSTAL_Region"].ToString() + ":" + sdr["PrimaryContact"].ToString() + ":" + sdr["Active"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;
        }

        public String getContactQuote(int contactID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT * FROM dbo.Quote_Contacts WHERE ContactID = " + contactID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = strContact + sdr["FirstName"].ToString() + ":" + sdr["LastName"].ToString() + ":" + sdr["DEFAULT_AreaCode"].ToString() + ":" + sdr["DEFAULT_Number"].ToString() + ":" + sdr["MOBILE_Number"].ToString() + ":" + sdr["Email"].ToString() + ":" + sdr["STREET_AddressLine1"].ToString() + ":" + sdr["STREET_AddressLine2"].ToString() + ":" + sdr["STREET_City"].ToString() + ":" + sdr["STREET_PostalCode"].ToString() + ":" + sdr["STREET_Region"].ToString() + ":" + sdr["POSTAL_AddressLine1"].ToString() + ":" + sdr["POSTAL_AddressLine2"].ToString() + ":" + sdr["POSTAL_City"].ToString() + ":" + sdr["POSTAL_PostalCode"].ToString() + ":" + sdr["POSTAL_Region"].ToString() + ":" + sdr["Active"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;
        }

        // Return's Contact Person Address Line 1 
        public String getContactAddressLine1(String CID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT STREET_AddressLine1 FROM dbo.Contacts WHERE ContactID = " + CID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = sdr["STREET_AddressLine1"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;
        }

        // Return's Contact Person Suburb, State and Postcode together
        public String getContactSubStaPost(String CID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT STREET_City + ' ' + STREET_Region + ' ' + STREET_PostalCode AS FullAddress FROM dbo.Contacts WHERE ContactID = " + CID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = sdr["FullAddress"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;
        }


        //This Method Update the Contact with the Xero generated Guid
        public String UpdateWithXeroDetails(int CompID, String XeroGuid)
        {

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSQLUpdateStmt = "Update dbo.Companies set XeroGUID=@guid where CompanyID=@compid";
            //String strSQLUpdateStmt = "UPDATE dbo.Contacts SET XeroContactID=@guid WHERE ContactID=@ContactID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@guid", XeroGuid);
            cmd.Parameters.AddWithValue("@compid", CompID);

            try
            {
                conn.Open();
                output = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                output = ex.Message.ToString();
            }

            return output;
        }

        //This methods returns the contact phone number based on the companyID
        public String getContactPhoneBasedOnCompanyID(String CompanyID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT TOP 1 DEFAULT_AreaCode + ' ' + DEFAULT_Number AS FullNumber FROM dbo.Contacts WHERE CompanyID = " + CompanyID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = sdr["FullNumber"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;
        }

        //End Method Update the Contact with Xero Guid


        public String UpdateContact(int ContactID, String FirstName, String LastName, String DefaultAreaCode, String DefaultNumber,
            String MobileNumber, String EmailAddy, String ShipLine1, String ShipLine2, String ShipCity,
            String ShipState, String ShipPostcode, String BillLine1, String BillLine2, String BillCity,
            String BillState, String BillPostcode, String PrimaryContact, String ActInact, int loggedInUserID = 0)
        {
            int RowsEffected = -1;
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            var oldContactObj = GetContactByContactId(ContactID);

            String strSQLUpdateStmt = "UPDATE dbo.Contacts SET FirstName=@FirstName, LastName=@LastName, PrimaryContact=@PrimaryContact, " +
           " DEFAULT_AreaCode=@DefaultAreaCode, DEFAULT_Number=@DefaultNumber, MOBILE_Number=@MobileNumber, Email=@EmailAddy, " +
            " STREET_AddressLine1=@ShipLine1, STREET_AddressLine2=@ShipLine2, STREET_City=@ShipCity, STREET_PostalCode=@ShipPostcode, " +
            " STREET_Region=@ShipState, POSTAL_AddressLine1=@BillLine1, POSTAL_AddressLine2=@BillLine2, POSTAL_City=@BillCity, POSTAL_PostalCode=@BillPostcode, " +
             " POSTAL_Region=@BillState, Active=@ActInact WHERE ContactID = " + ContactID;

            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
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
            cmd.Parameters.AddWithValue("@ActInact", ActInact);

            var newContact = new Contact()
            {
                FirstName = FirstName,
                LastName = LastName,
                PrimaryContact = PrimaryContact,
                DEFAULT_AreaCode = DefaultAreaCode,
                DEFAULT_Number = DefaultNumber,
                MOBILE_Number = MobileNumber,
                Email = EmailAddy,
                STREET_AddressLine1 = ShipLine1,
                STREET_AddressLine2 = ShipLine2,
                STREET_City = ShipCity,
                STREET_PostalCode = ShipPostcode,
                STREET_Region = ShipState,
                POSTAL_AddressLine1 = BillLine1,
                POSTAL_AddressLine2 = BillLine2,
                POSTAL_City = BillCity,
                POSTAL_PostalCode = BillPostcode,
                POSTAL_Region = BillState,
                Active = ActInact
            };


            InsertAuditTableContact(oldContactObj, newContact, conn, loggedInUserID);

            try
            {
                conn.Open();
                output = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                output = ex.Message.ToString();
            }

            return output;
        }

        private void InsertAuditTableContact(Contact oldData, Contact newData, SqlConnection conn, int loggedinUserId)
        {
            if (oldData.FirstName != newData.FirstName)
            {
                var intRowsEffected = 0;
                var columnName = "FIRST NAME";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.FirstName, newData.FirstName, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.LastName != newData.LastName)
            {
                var intRowsEffected = 0;
                var columnName = "Last Name";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.LastName, newData.LastName, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.DEFAULT_AreaCode != newData.DEFAULT_AreaCode)
            {
                var intRowsEffected = 0;
                var columnName = "DEFAULT AREA CODE";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.DEFAULT_AreaCode, newData.DEFAULT_AreaCode, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.DEFAULT_Number != newData.DEFAULT_Number)
            {
                var intRowsEffected = 0;
                var columnName = "DEFAULT NUMBER";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.DEFAULT_Number, newData.DEFAULT_Number, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.MOBILE_Number != newData.MOBILE_Number)
            {
                var intRowsEffected = 0;
                var columnName = "MOBILE NUMBER";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.MOBILE_Number, newData.MOBILE_Number, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.Email != newData.Email)
            {
                var intRowsEffected = 0;
                var columnName = "Email";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.Email, newData.Email, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.STREET_AddressLine1 != newData.STREET_AddressLine1)
            {
                var intRowsEffected = 0;
                var columnName = "SHIPPING ADDRESS LINE 1";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.STREET_AddressLine1, newData.STREET_AddressLine1, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.STREET_AddressLine2 != newData.STREET_AddressLine2)
            {
                var intRowsEffected = 0;
                var columnName = "SHIPPING ADDRESS LINE 2";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.STREET_AddressLine2, newData.STREET_AddressLine2, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.STREET_Region != newData.STREET_Region)
            {
                var intRowsEffected = 0;
                var columnName = "SHIPPING STATE";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.STREET_Region, newData.STREET_Region, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.STREET_City != newData.STREET_City)
            {
                var intRowsEffected = 0;
                var columnName = "SHIPPING CITY";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.STREET_City, newData.STREET_City, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }

            if (oldData.STREET_PostalCode != newData.STREET_PostalCode)
            {
                var intRowsEffected = 0;
                var columnName = "SHIPPING POSTCODE";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.STREET_PostalCode, newData.STREET_PostalCode, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }

            if (oldData.POSTAL_AddressLine1 != newData.POSTAL_AddressLine1)
            {
                var intRowsEffected = 0;
                var columnName = "BILLING ADDRESS LINE 1";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.POSTAL_AddressLine1, newData.POSTAL_AddressLine1, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }

            if (oldData.POSTAL_AddressLine2 != newData.POSTAL_AddressLine2)
            {
                var intRowsEffected = 0;
                var columnName = "BILLING ADDRESS LINE 1";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.POSTAL_AddressLine2, newData.POSTAL_AddressLine2, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }

            if (oldData.POSTAL_City != newData.POSTAL_City)
            {
                var intRowsEffected = 0;
                var columnName = "BILLING CITY";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.POSTAL_City, newData.POSTAL_City, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.POSTAL_Region != newData.POSTAL_Region)
            {
                var intRowsEffected = 0;
                var columnName = "BILLING STATE";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.POSTAL_Region, newData.POSTAL_Region, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }

            if (oldData.POSTAL_PostalCode != newData.POSTAL_PostalCode)
            {
                var intRowsEffected = 0;
                var columnName = "BILLING POSTCODE";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.POSTAL_PostalCode, newData.POSTAL_PostalCode, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }
            if (oldData.PrimaryContact != newData.PrimaryContact)
            {
                var intRowsEffected = 0;
                var columnName = "Primary Contact";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.PrimaryContact, newData.PrimaryContact, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }

            if (oldData.Active != newData.Active)
            {
                var intRowsEffected = 0;
                var columnName = "Active";
                var talbeName = "Contact";
                var ActionType = "Updated";
                var companyID = Convert.ToInt32(oldData.CompanyId);

                intRowsEffected = CreateCompanyDal().CreateActionONAuditLog(oldData.Active, newData.Active, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, oldData.ContactId, companyID);
            }

        }

        public void UpdateContactByCompanyId(int comId, string firstName, string lastName)
        {

            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = CONNSTRING;
            var queryStr = @"UPDATE Contacts SET FirstName=@firstName ,LastName=@lastname WHERE CompanyID=@comId";


            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlCon;
                sqlCon.Open();
                sqlCommand.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = firstName;
                sqlCommand.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastName;
                sqlCommand.Parameters.Add("@comId", SqlDbType.Int).Value = comId;
                sqlCommand.CommandText = queryStr;
                sqlCommand.ExecuteNonQuery();

            }
            sqlCon.Close();
        }

        private CompanyDAL CreateCompanyDal()
        {
            return new CompanyDAL(CONNSTRING);
        }


        public Contact GetContactByContactNameandEmail(string firstName, string lastName, string email)
        {

            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var contact = new Contact();

            String strSqlContactStmt = "SELECT * FROM dbo.Contacts WHERE FirstName =@firstname AND LastName=@lastname AND  Email=@email ";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.AddWithValue("@firstname", firstName);
                cmd.Parameters.AddWithValue("@lastname", lastName);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        contact.ContactId = Convert.ToInt32(sdr["ContactID"].ToString());
                        contact.CompanyId = sdr["CompanyID"].ToString();
                        contact.FirstName = sdr["FirstName"].ToString();
                        contact.LastName = sdr["LastName"].ToString();
                        contact.DEFAULT_AreaCode = sdr["DEFAULT_AreaCode"].ToString();
                        contact.DEFAULT_Number = sdr["DEFAULT_Number"].ToString();
                        contact.MOBILE_Number = sdr["MOBILE_Number"].ToString();
                        contact.Email = sdr["Email"].ToString();
                        contact.STREET_AddressLine1 = sdr["STREET_AddressLine1"].ToString();
                        contact.STREET_AddressLine2 = sdr["STREET_AddressLine2"].ToString();
                        contact.STREET_City = sdr["STREET_City"].ToString();
                        contact.STREET_PostalCode = sdr["STREET_PostalCode"].ToString();
                        contact.STREET_Region = sdr["STREET_Region"].ToString();
                        contact.POSTAL_AddressLine1 = sdr["POSTAL_AddressLine1"].ToString();
                        contact.POSTAL_AddressLine2 = sdr["POSTAL_AddressLine2"].ToString();
                        contact.POSTAL_City = sdr["POSTAL_City"].ToString();
                        contact.POSTAL_PostalCode = sdr["POSTAL_PostalCode"].ToString();
                        contact.POSTAL_Region = sdr["POSTAL_Region"].ToString();
                        contact.PrimaryContact = sdr["PrimaryContact"].ToString();
                        contact.Active = sdr["Active"].ToString();
                    }
                }
            }
            conn.Close();

            return contact;
        }

        public List<Contact> GetContatByCompanyId(string comId)
        {
           
            var listContact = new List<Contact>();
            var sqlconnection = new SqlConnection();
            sqlconnection.ConnectionString = CONNSTRING;
            var queryString = "SELECT * from dbo.Contacts WHERE CompanyId=" + comId;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = queryString;
                cmd.Connection = sqlconnection;
                sqlconnection.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var contact = new Contact();
                        contact.CompanyId = comId;
                        contact.ContactId = Convert.ToInt32(sdr["ContactId"].ToString());
                        contact.FirstName = sdr["FirstName"].ToString();
                        contact.LastName = sdr["LastName"].ToString();
                        contact.DEFAULT_AreaCode = sdr["DEFAULT_AreaCode"].ToString();
                        contact.DEFAULT_Number = sdr["DEFAULT_Number"].ToString();
                        contact.MOBILE_Number=sdr["Mobile_Number"].ToString();
                        contact.Email = sdr["Email"].ToString();
                        contact.STREET_AddressLine1 = sdr["STREET_AddressLine1"].ToString();
                        contact.STREET_AddressLine2 = sdr["STREET_AddressLine2"].ToString();
                        contact.STREET_City = sdr["STREET_City"].ToString();
                        contact.STREET_PostalCode = sdr["STREET_PostalCode"].ToString();
                        contact.STREET_Region = sdr["STREET_Region"].ToString();
                        contact.POSTAL_AddressLine1 = sdr["POSTAL_AddressLine1"].ToString();
                        contact.POSTAL_AddressLine2 = sdr["POSTAL_AddressLine2"].ToString();
                        contact.POSTAL_City = sdr["POSTAL_City"].ToString();
                        contact.POSTAL_PostalCode = sdr["POSTAL_PostalCode"].ToString();
                        contact.POSTAL_Region = sdr["POSTAL_Region"].ToString();
                        contact.Active = sdr["Active"].ToString();

                        listContact.Add(contact);
                    }
                }
                
            }

            return listContact;
        }

        public Contact GetContactByContactId(int contactId)
        {

            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var contact = new Contact();

            String strSqlContactStmt = "SELECT * FROM dbo.Contacts WHERE ContactID = " + contactId;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        contact.ContactId = contactId;
                        contact.CompanyId = sdr["CompanyID"].ToString();
                        contact.FirstName = sdr["FirstName"].ToString();
                        contact.LastName = sdr["LastName"].ToString();
                        contact.DEFAULT_AreaCode = sdr["DEFAULT_AreaCode"].ToString();
                        contact.DEFAULT_Number = sdr["DEFAULT_Number"].ToString();
                        contact.MOBILE_Number = sdr["MOBILE_Number"].ToString();
                        contact.Email = sdr["Email"].ToString();
                        contact.STREET_AddressLine1 = sdr["STREET_AddressLine1"].ToString();
                        contact.STREET_AddressLine2 = sdr["STREET_AddressLine2"].ToString();
                        contact.STREET_City = sdr["STREET_City"].ToString();
                        contact.STREET_PostalCode = sdr["STREET_PostalCode"].ToString();
                        contact.STREET_Region = sdr["STREET_Region"].ToString();
                        contact.POSTAL_AddressLine1 = sdr["POSTAL_AddressLine1"].ToString();
                        contact.POSTAL_AddressLine2 = sdr["POSTAL_AddressLine2"].ToString();
                        contact.POSTAL_City = sdr["POSTAL_City"].ToString();
                        contact.POSTAL_PostalCode = sdr["POSTAL_PostalCode"].ToString();
                        contact.POSTAL_Region = sdr["POSTAL_Region"].ToString();
                        contact.PrimaryContact = sdr["PrimaryContact"].ToString();
                        contact.Active = sdr["Active"].ToString();
                    }
                }
            }
            conn.Close();

            return contact;
        }

        public class Contact
        {
            public int ContactId { get; set; }
            public string CompanyId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PrimaryContact { get; set; }
            public string DEFAULT_AreaCode { get; set; }
            public string DEFAULT_Number { get; set; }
            public string MOBILE_Number { get; set; }
            public string Email { get; set; }
            public string STREET_AddressLine1 { get; set; }
            public string STREET_AddressLine2 { get; set; }
            public string STREET_City { get; set; }
            public string STREET_Region { get; set; }
            public string STREET_PostalCode { get; set; }
            public string POSTAL_AddressLine1 { get; set; }
            public string POSTAL_AddressLine2 { get; set; }
            public string POSTAL_City { get; set; }
            public string POSTAL_PostalCode { get; set; }
            public string POSTAL_Region { get; set; }
            public string Active { get; set; }

        }
    }
}
