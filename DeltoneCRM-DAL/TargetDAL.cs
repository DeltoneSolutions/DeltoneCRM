using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;




namespace DeltoneCRM_DAL
{
    public class TargetDAL
    {

        private String CONNSTRING;

        public TargetDAL(String strconnString)
        {
            CONNSTRING = strconnString;
        }

        //This Method Update the Target given by details
        public String updateTarget(int targetid, int LoginID, int TargetMonth, int TargetYear, float TargetCommision, int TargetWorkingDays)
        {

            string output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "update dbo.Targets SET LoginID=@LoginID,TargetMonth=@TargetMonth,TargetYear=@TargetYear,TargetCommission=@TargetCommission,WorkingDays=@workingdays,AmendedDateTime=CURRENT_TIMESTAMP where TargetID=@TargetID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@LoginID", LoginID);
            cmd.Parameters.AddWithValue("@TargetMonth", TargetMonth);
            cmd.Parameters.AddWithValue("@TargetYear", TargetYear);
            cmd.Parameters.AddWithValue("@TargetCommission", TargetCommision);
            cmd.Parameters.AddWithValue("@workingdays", TargetWorkingDays);
            cmd.Parameters.AddWithValue("@TargetID", targetid);

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
    


    }
}
