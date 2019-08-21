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
    public class CompanyDAL
    {

        private String CONNSTRING;

        public CompanyDAL(String connString)
        {
            CONNSTRING = connString;
        }


        /*This Method Validate the Company given by ID and Company name*/
        public String validateCompany(String companyname, int companyID)
        {
            String stroutput = "0";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlContactStmt = "select COUNT(*) as row_count from dbo.Companies where CompanyName=@companyname and CompanyID <> " + companyID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@companyname", companyname);
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        stroutput = sdr["row_count"].ToString();
                    }
                }
            }
            conn.Close();

            return stroutput;
        }

        private Company GetCompanyBeforeUpdate(int companyId)
        {

            var company = new Company();
            company.CompanyId = companyId.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            var strSqlContactStmt = "SELECT * FROM dbo.Companies WHERE CompanyID = " + companyId;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        company.CompanyName = sdr["CompanyName"].ToString();
                        company.CompanyWebsite = sdr["CompanyWebsite"].ToString();
                        company.OwnershipAdminID = sdr["OwnershipAdminID"].ToString();
                        company.OwnershipPeriod = sdr["OwnershipPeriod"].ToString();
                    }
                }
            }
            conn.Close();
            return company;
        }

        public String CheckCompanyOwnerByID(String CID)
        {
            String CompanyOwnerId = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT OwnerId FROM dbo.CompanyOwner WHERE CompanyID = " + CID;
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
                            CompanyOwnerId = sdr["OwnerId"].ToString();
                        }
                    }
                }
            }
            conn.Close();
            return CompanyOwnerId;
        }

        public void InsertLead(int comId, int userId,DateTime createDate,DateTime expiryDate)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSQLInsertStmt = "insert into dbo.LeadCompany(UserId,CompanyId,CreatedDate,ExpiryDate) " +
             " values (@UserId,@CompanyId,@createDate,@expiryDate);";

            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@CompanyId", comId);
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

        public void InsertLeadNotExist(int comId, int userId, DateTime createDate, DateTime expiryDate)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSQLInsertStmt = "insert into dbo.LeadCompanyNotExists(UserId,CompanyId,CreatedDate,ExpiryDate) " +
             " values (@UserId,@CompanyId,@createDate,@expiryDate);";

            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@CompanyId", comId);
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
         public class CompanyView
        {
            public string CompanyName { get; set; }
            public int CompanyID { get; set; }
            public string LastOrderDate { get; set; }
            public int OwnershipAdminID { get; set; }
            public bool Active { get; set; }
            public bool Hold { get; set; }
        }

        public void DeleteLeadRecentCompanies(List<CompanyView> listObj,int repId)
        {
            foreach (var item in listObj)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = CONNSTRING;
                var strSqlContactStmt = @"DELETE FROM LeadCompany WHERE CompanyId = @companyId AND UserId=@userId ";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.Add("@companyId", SqlDbType.Int).Value = item.CompanyID;
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = repId;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }


        public void CreateOwnerCompanyId(string comId, string OwnerId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSQLInsertStmt = "insert into dbo.CompanyOwner(CompanyId,OwnerId) " +
             " values (@CompanyId,@OwnerId);";

            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@CompanyId", comId);
            cmd.Parameters.AddWithValue("@OwnerId", OwnerId);

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

        public class CompanyLead
        {

            public int CompanyId { get; set; }
            public int UserId { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime ExpiryDate { get; set; }
        }

        public CompanyLead GetCompanyLeadByCompanyId(int comID)
        {

            var listCom =new CompanyLead();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = @"SELECT UserId,CompanyId,CreatedDate,ExpiryDate FROM dbo.LeadCompany where CompanyId=" + comID;
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
                            var comLea = new CompanyLead();

                            comLea.UserId = Convert.ToInt32(sdr["UserId"].ToString());
                            comLea.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString());
                            comLea.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString());
                            comLea.ExpiryDate = Convert.ToDateTime(sdr["ExpiryDate"].ToString());

                           // if (comLea.ExpiryDate.Date )
                            return comLea;
                        }
                    }
                }
            }
            conn.Close();

            return null;
        }

        public List<CompanyLead> GetLeadCompanies()
        {

            var listCom = new List<CompanyLead>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = @"SELECT UserId,CompanyId,CreatedDate,ExpiryDate FROM dbo.LeadCompany ";
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
                            var comLea = new CompanyLead();

                            comLea.UserId = Convert.ToInt32(sdr["UserId"].ToString());
                            comLea.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString());
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

        public List<CompanyLead> GetLeadCompaniesNotExist()
        {

            var listCom = new List<CompanyLead>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = @"SELECT UserId,CompanyId,CreatedDate,ExpiryDate FROM dbo.LeadCompanyNotExists ";
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
                            var comLea = new CompanyLead();

                            comLea.UserId = Convert.ToInt32(sdr["UserId"].ToString());
                            comLea.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString());
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

        //This Function Edit Company given by Details
        public int EditCompnay(int CompanyID, String strCompanyName, String webSite, String AccountOwner, String OwnershipPeriod, int loggedUserId = 0)
        {
            int RowsEffected = -1;

            var companyBeforeUpdata = GetCompanyBeforeUpdate(CompanyID);
            var newCompanyData = new Company()
            {
                CompanyId = CompanyID.ToString(),
                CompanyName = strCompanyName,
                CompanyWebsite = webSite,
                OwnershipAdminID = AccountOwner,

            };
            DateTime ownershipCheck = DateTime.MinValue;

            var comExists = CheckCompanyOwnerByID(CompanyID.ToString());
            if (string.IsNullOrEmpty(comExists))
                CreateOwnerCompanyId(CompanyID.ToString(),
                    companyBeforeUpdata.OwnershipAdminID);


            if (DateTime.TryParse(companyBeforeUpdata.OwnershipPeriod, out ownershipCheck))
            {
                var convertDateTimeOwnership = Convert.ToDateTime(companyBeforeUpdata.OwnershipPeriod);
                companyBeforeUpdata.OwnershipPeriod = convertDateTimeOwnership.ToString("yyyy-MM-dd");
            }

            InsertAuditForComanyTableUpdateAction(companyBeforeUpdata, newCompanyData, loggedUserId, CompanyID); //Audit table for tracking all the update

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "Update dbo.Companies SET CompanyName=@ParaCompanyName,CompanyWebsite=@paraCompanyWebSite,OwnershipAdminID=@paraAccountOwner,AlteredDateTime=CURRENT_TIMESTAMP,OwnershipPeriod=@OwnershipPeriod WHERE CompanyID=@paraCompanyID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@ParaCompanyName", strCompanyName);
            cmd.Parameters.AddWithValue("@paraCompanyWebSite", webSite);
            cmd.Parameters.AddWithValue("@paraAccountOwner", AccountOwner);
            cmd.Parameters.AddWithValue("@paraCompanyID", CompanyID);
            cmd.Parameters.AddWithValue("@OwnershipPeriod", OwnershipPeriod);

            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            return CompanyID;
        }


        private void InsertAuditForComanyTableUpdateAction(Company beforeUpdate, Company newData, int loggedinUserId, int companyID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var recordId = Convert.ToInt32(beforeUpdate.CompanyId);
            var str = "";
            if (beforeUpdate.CompanyWebsite != newData.CompanyWebsite)
            {
                var intRowsEffected = 0;
                var columnName = "Website";
                var talbeName = "Companies";
                var ActionType = "Updated";


                intRowsEffected = CreateActionONAuditLog(beforeUpdate.CompanyWebsite, newData.CompanyWebsite, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, recordId, companyID);
                //str = columnName + ActionType + "from " + beforeUpdate.CompanyWebsite + " to " + newData.CompanyWebsite;


            }
            if (beforeUpdate.OwnershipPeriod != newData.OwnershipPeriod)
            {
                var intRowsEffected = 0;
                var columnName = "OWNER PERIOD";
                var talbeName = "Companies";
                var ActionType = "Updated";


                intRowsEffected = CreateActionONAuditLog(beforeUpdate.OwnershipPeriod, newData.OwnershipPeriod, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, recordId, companyID);
                //str = str + " " + columnName + ActionType + "from " + beforeUpdate.CompanyWebsite + " to " + newData.CompanyWebsite;


            }
            if (beforeUpdate.OwnershipAdminID != newData.OwnershipAdminID)
            {
                var intRowsEffected = 0;
                var columnName = "ACCOUNT OWNER";
                var talbeName = "Companies";
                var ActionType = "Updated";


                var beforeOwnerName = new LoginDAL(CONNSTRING).getLoginNameFromID(beforeUpdate.OwnershipAdminID);
                var newOwnerName = new LoginDAL(CONNSTRING).getLoginNameFromID(newData.OwnershipAdminID);
                intRowsEffected = CreateActionONAuditLog(beforeOwnerName, newOwnerName, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, recordId, companyID);
                //str = str + " " + columnName + ActionType + "from " + beforeUpdate.CompanyWebsite + " to " + newData.CompanyWebsite;

            }
            if (beforeUpdate.CompanyName != newData.CompanyName)
            {
                var intRowsEffected = 0;
                var columnName = "Company Name";
                var talbeName = "Companies";
                var ActionType = "Updated";



                intRowsEffected = CreateActionONAuditLog(beforeUpdate.CompanyName, newData.CompanyName, loggedinUserId, conn,
                    intRowsEffected, columnName, talbeName, ActionType, recordId, companyID);

                // str = str + " " + columnName + ActionType + "from " + beforeUpdate.CompanyWebsite + " to " + newData.CompanyWebsite;

            }
        }




        public int CreateActionONAuditLog(string oldData, string newData, int loggedinUserId,
            SqlConnection conn, int intRowsEffected,
            string columnName, string talbeName, string ActionType, int recordId, int companyid)
        {
            String strSQLInsertStmt = "insert into dbo.AuditLog(TableName,Column_Name, Pre_Value, New_Value," +
           " Action_Type, CreatedUserId, RecordId,CompanyId,CreatedDateTime) " +
             " values (@TableName,@Column_Name,@Pre_Value, @New_Value, @Action_Type, @CreatedUserId,@RecordId,@CompanyId,CURRENT_TIMESTAMP);";

            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@TableName", talbeName);
            cmd.Parameters.AddWithValue("@Column_Name", columnName);
            cmd.Parameters.AddWithValue("@Pre_Value", oldData);
            cmd.Parameters.AddWithValue("@New_Value", newData);
            cmd.Parameters.AddWithValue("@Action_Type", ActionType);
            cmd.Parameters.AddWithValue("@CreatedUserId", loggedinUserId);
            cmd.Parameters.AddWithValue("@RecordId", recordId);
            cmd.Parameters.AddWithValue("@CompanyId", companyid);

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

        //This Function Add company given by Details
        public int AddCompany()
        {
            int rowsEffecrted = -1;


            return rowsEffecrted;

        }

        //This method will return the account owner of the company
        public string getCompanyAccountOwner(int cid)
        {
            int RowsEffected = -1;
            String AccountOwnerName = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLQuery = "SELECT FirstName + ' ' + LastName AS FullName FROM dbo.Logins WHERE LoginID IN (SELECT OwnershipAdminID FROM dbo.Companies WHERE CompanyID =" + cid + ")";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSQLQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        AccountOwnerName = sdr["FullName"].ToString();
                    }

                }
                conn.Close();
            }

            return AccountOwnerName;
        }

        public string getCompanyAccountOwnerNotExists(int cid)
        {
            int RowsEffected = -1;
            String AccountOwnerName = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLQuery = "SELECT FirstName + ' ' + LastName AS FullName FROM dbo.Logins WHERE LoginID IN (SELECT OwnershipAdminID FROM dbo.CompaniesNotExists WHERE CompanyID =" + cid + ")";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSQLQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        AccountOwnerName = sdr["FullName"].ToString();
                    }

                }
                conn.Close();
            }

            return AccountOwnerName;
        }

        //Method return the Login ID of the account owner
        public string getCompanyOwnershipAdminID(String CID)
        {
            String AccountOwnerID = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLQuery = "SELECT LoginID FROM dbo.Logins WHERE LoginID IN (SELECT OwnershipAdminID FROM dbo.Companies WHERE CompanyID =" + CID + ")";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSQLQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        AccountOwnerID = sdr["LoginID"].ToString();
                    }

                }
                conn.Close();
            }

            return AccountOwnerID;
        }

        public string getCompanyOwnershipAdminIDNotExists(String CID)
        {
            String AccountOwnerID = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLQuery = "SELECT LoginID FROM dbo.Logins WHERE LoginID IN (SELECT OwnershipAdminID FROM dbo.CompaniesNotExists WHERE CompanyID =" + CID + ")";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSQLQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        AccountOwnerID = sdr["LoginID"].ToString();
                    }

                }
                conn.Close();
            }

            return AccountOwnerID;
        }


        //This method Fetch the Companyname given contactid
        public String GetCompanybyContactID(int contactId)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT cm.CompanyName FROM dbo.Contacts cn,dbo.Companies cm  WHERE cn.CompanyID=cm.CompanyID AND cn.ContactID=" + contactId;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = sdr["CompanyName"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;

        }

        // Return's the Company Name based on the Company ID
        public String getCompanyNameByID(String CID)
        {
            String CompanyName = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT CompanyName FROM dbo.Companies WHERE CompanyID = " + CID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CompanyName = sdr["CompanyName"].ToString();
                    }
                }
            }
            conn.Close();
            return CompanyName;
        }

        public String getCompanyNameByIDNoORder(String CID)
        {
            String CompanyName = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT CompanyName FROM dbo.CompaniesNotExists WHERE CompanyID = " + CID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CompanyName = sdr["CompanyName"].ToString();
                    }
                }
            }
            conn.Close();
            return CompanyName;
        }

        public String getQuoteCompanyNameByID(String CID)
        {
            String CompanyName = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT CompanyName FROM dbo.Quote_Companies WHERE CompanyID = " + CID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CompanyName = sdr["CompanyName"].ToString();
                    }
                }
            }
            conn.Close();
            return CompanyName;
        }


        //Save's the Company default payment terms
        public String saveCompanyPaymentTerms(String CID, String DPT, int loggedinUserId = 0)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var oldPaymentTerms = getCompanyPaymentTerms(CID);
            UpdatePaymentAudit(Convert.ToInt32(CID), oldPaymentTerms, DPT, loggedinUserId, conn);
            try
            {

                String SQLStmt = "UPDATE dbo.Companies SET DefaultPaymentTerms = '" + DPT + "' WHERE CompanyID = " + CID;
                SqlCommand cmd = new SqlCommand(SQLStmt, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return "ok";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }

        private void UpdatePaymentAudit(int companyID, string olddata, string newdata, int loggedinUserId, SqlConnection conn)
        {
            var intRowsEffected = 0;
            var columnName = "Payment Terms";
            var talbeName = "Companies";
            var ActionType = "Updated";

            intRowsEffected = CreateActionONAuditLog(olddata, newdata, loggedinUserId, conn,
                intRowsEffected, columnName, talbeName, ActionType, companyID, companyID);
        }

        // Return's the Company Default Payment Terms
        public String getCompanyPaymentTerms(String CID)
        {
            SqlConnection conn = new SqlConnection();
            String PaymentTerm = String.Empty;

            conn.ConnectionString = CONNSTRING;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT DefaultPaymentTerms FROM dbo.Companies WHERE CompanyID = " + CID;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        PaymentTerm = sdr["DefaultPaymentTerms"].ToString();
                    }
                }

                conn.Close();
            }

            return PaymentTerm;
        }

        public String getCompanyPaymentTermsNotExists(String CID)
        {
            SqlConnection conn = new SqlConnection();
            String PaymentTerm = String.Empty;

            conn.ConnectionString = CONNSTRING;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT DefaultPaymentTerms FROM dbo.CompaniesNotExists WHERE CompanyID = " + CID;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        PaymentTerm = sdr["DefaultPaymentTerms"].ToString();
                    }
                }

                conn.Close();
            }

            return PaymentTerm;
        }

        public List<LeadCompanyRep> GetLeadCompanyNOExpires(int rep)
        {
            var listleadCom = new List<LeadCompanyRep>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {


                    cmd.CommandText = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                                  CP.CompanyID ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId And lo.UserId =" + rep + "  and convert(varchar(10),lo.ExpiryDate, 120) >= CAST(getdate() as date) ";


                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var comId = sdr["CompanyID"].ToString();
                                var CoName = sdr["CompanyName"].ToString();
                                var contactName = sdr["FullName"].ToString();
                                var TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                                var MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                                var Email = sdr["Email"].ToString();

                                var obj = new LeadCompanyRep()
                                {
                                    CompanyId = comId,
                                    CoName=CoName,
                                    contactName = contactName,
                                    Email=Email,
                                    TeleBuilder=TeleBuilder

                                };

                                listleadCom.Add(obj);
                             //   var ViewEdit = "<img src='../Images/Edit.png'  onclick='OpenCompany(" + sdr["CompanyID"].ToString() + ");'>";
                            }
                        }
                    }
                    conn.Close();
                }
            }
            return listleadCom;
        }

        public class LeadCompanyRep
        {
            public string CompanyId { get; set; }
            public string CoName { get; set; }
            public string contactName { get; set; }
            public string TeleBuilder { get; set; }
            public string Email { get; set; }
        }


        public String getCompanyNotes(String CID)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            conn.ConnectionString = CONNSTRING;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Notes FROM dbo.Companies WHERE CompanyID = " + CID;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        companyNote = sdr["Notes"].ToString();
                    }
                }

                conn.Close();
            }

            return companyNote;
        }

        public String getQuoteCompanyNotes(String CID)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            conn.ConnectionString = CONNSTRING;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Notes FROM dbo.QUOTE WHERE QuoteID = " + CID;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        companyNote = sdr["Notes"].ToString();
                    }
                }

                conn.Close();
            }

            return companyNote;
        }

        public String getQuoteAllocatedCompanyNotes(String CID)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            conn.ConnectionString = CONNSTRING;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Notes FROM  dbo.QuoteAllocate WHERE QuoteID = " + CID;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (companyNote==String.Empty)
                        companyNote = sdr["Notes"].ToString();
                        else
                            companyNote = companyNote + sdr["Notes"].ToString();
                    }
                }

                conn.Close();
            }

            return companyNote;
        }

        public class Company
        {
            public string CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string CompanyWebsite { get; set; }
            public string OwnershipAdminID { get; set; }
            public string OwnershipPeriod { get; set; }
        }

        public class AuditLogCompany
        {
            public string TableName { get; set; }
            public string Column_Name { get; set; }
            public string Pre_Value { get; set; }
            public string New_Value { get; set; }
            public string Action_Type { get; set; }
            public string CreatedDateTime { get; set; }
            public string CreatedUserId { get; set; }
            public string RecordId { get; set; }
        }
    }
}
