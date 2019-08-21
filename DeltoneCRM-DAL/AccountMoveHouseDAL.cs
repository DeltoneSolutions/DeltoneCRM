using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
   public class AccountMoveHouseDAL
    {
        private String CONNSTRING;

        public AccountMoveHouseDAL(String connString)
        {
            CONNSTRING = connString;
        }

        public bool MoveRepAccounttoHouseAccount()
        {
            var status = false;
            var monthNoSale = -12;
            var listCompanies = GetLastCompanyNoOrder(monthNoSale);
           status = MoveAccountToHouseAccount(listCompanies);

            return status;
        }

        protected bool MoveAccountToHouseAccount(List<CompanyByOrderCreatedDate> listCompanies)
        {
            var status = true;
            var hourAcco = "7";
            ILog _logger = LogManager.GetLogger(typeof(AccountMoveHouseDAL));
            foreach(var item in listCompanies){
                var comdal = new CompanyDAL(CONNSTRING);
                var resComId = comdal.CheckCompanyOwnerByID(item.CompanyId );
                try
                {
                    if (string.IsNullOrEmpty(resComId))
                    {
                        comdal.CreateOwnerCompanyId(item.CompanyId, item.OwnerId.ToString());

                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Error occurred at MoveAccountToHouseAccount method : CreateOwnerCompanyId" + ex);
                    status = false;
                  
                }
                try
                {
                    PerformMove(item.CompanyId, hourAcco);
                    WriteErrorLog( item.CompanyId +",");
                }

                catch (Exception exc)
                {
                    _logger.Error("Error occurred at MoveAccountToHouseAccount method : PerformMove" + exc);
                    status = false;
                   
                }

            }
            return status;
        }

        protected void PerformMove(string CID, string acout)
        {
            SqlConnection LAconn = new SqlConnection();
            var conn = CONNSTRING;
            LAconn.ConnectionString = conn;
            var comDal = new CompanyDAL(conn);
            var OwnerId = acout;
            ILog _logger = LogManager.GetLogger(typeof(AccountMoveHouseDAL));
            String strSQLInsertStmt = "UPDATE dbo.Companies SET OwnershipAdminID = @OwnershipAdminID, AlteredDateTime=CURRENT_TIMESTAMP, OwnershipPeriod = NULL WHERE CompanyID =@CompanyID";
            SqlCommand LAcmd = new SqlCommand(strSQLInsertStmt, LAconn);
            LAcmd.Parameters.AddWithValue("@OwnershipAdminID", OwnerId);
            LAcmd.Parameters.AddWithValue("@CompanyID", CID);
            try
            {
                LAconn.Open();
                LAcmd.ExecuteNonQuery();
                LAconn.Close();
            }
            catch (Exception ex)
            {
                _logger.Error("Error occurred at PerformMove method" + ex);
              //  Console.Write(ex.Message.ToString());
            }

        }
        public  void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
        protected List<CompanyByOrderCreatedDate> GetLastCompanyNoOrder(int month)
        {
            

            var query = @"SELECT Distinct CP.CompanyName, CP.CompanyID,Cp.OwnershipAdminID, max(ods.CreatedDateTime ) as CreatedDateTime
FROM [dbo].Companies CP 
left join [dbo].Orders ods
on ods.CompanyID=CP.CompanyID
where CP.OwnershipAdminID not in (7,1,2,3) and ( CP.IsSupperAcount is null or CP.IsSupperAcount=0)
group by CP.CompanyName, CP.CompanyID,Cp.OwnershipAdminID";


            var listObj = new List<CompanyByOrderCreatedDate>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = query;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var obj = new CompanyByOrderCreatedDate();
                       
                        obj.CompanyId = reader["CompanyID"].ToString();
                        obj.OwnerId = reader["OwnershipAdminID"].ToString();
                        obj.CompanyName = reader["CompanyName"].ToString();


                        if (reader["CreatedDateTime"] != DBNull.Value)
                        {
                            var lastORderDate = Convert.ToDateTime(reader["CreatedDateTime"].ToString());
                            var calastORderDate = lastORderDate.AddYears(1);
                            var currentDate = DateTime.Now;
                            if (calastORderDate < currentDate)
                            {
                                obj.LastOrderCreatedDate = lastORderDate.ToShortDateString();
                                listObj.Add(obj);
                            }
                        }
                        //else
                        //{
                        //    listObj.Add(obj);
                        //}
                    }
                }
            }
            conn.Close();

            return listObj;


        }
    }

   public class CompanyByOrderCreatedDate
   {
       public string OrderId { get; set; }
       public string CompanyId { get; set; }
       public string OwnerId { get; set; }
       public string CompanyName { get; set; }
       public string LastOrderCreatedDate { get; set; }

   }
}
