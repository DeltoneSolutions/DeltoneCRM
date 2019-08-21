using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
    public class RaiseCsSalesRepDAL
    {
        private String CONNSTRING;

        public RaiseCsSalesRepDAL(String connString)
        {
            CONNSTRING = connString;
        }
        public void AddRepCS(RaiseSalesRep rep)
        {
            //insert
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO RaiseCSSalesRep(Complaint, OutCome, OrderId, CompanyId, ContactId,CreatedDate,CreatedUserId,Question,Status,CsType) 
                                     VALUES(@complaint, @outCome, @orderId, @companyId, @contactId,CURRENT_TIMESTAMP,@createdUserId,@question,@status,@CsType)";
            var status = "N";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@complaint", SqlDbType.NVarChar).Value = rep.Complaint;
                cmd.Parameters.Add("@outCome", SqlDbType.NVarChar).Value = rep.OutCome;
                cmd.Parameters.Add("@orderId", SqlDbType.Int).Value = rep.OrderId;
                cmd.Parameters.Add("@companyId", SqlDbType.Int).Value = rep.CompanyId;
                cmd.Parameters.Add("@contactId", SqlDbType.Int).Value = rep.ContactId;
                cmd.Parameters.Add("@createdUserId", SqlDbType.Int).Value = rep.CreatedUserId;
                cmd.Parameters.Add("@question", SqlDbType.NVarChar).Value = rep.Question;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
                cmd.Parameters.Add("@CsType", SqlDbType.Int).Value = rep.CsTyte;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();



            }
            conn.Close();

        }


        public string GetCompanyIDByRaisedId(int raisedId)
        {
            string comId = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    var strSqlContactStmt = @"SELECT rs.Id, rs.Complaint, rs.OutCome,rs.OrderId, rs.CompanyId,
                                   rs.ContactId,rs.CreatedDate,rs.Question from RaiseCSSalesRep rs where rs.Id=@rId";


                    cmd.Parameters.AddWithValue("@rId", SqlDbType.Int).Value = raisedId;
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                comId = sdr["CompanyId"].ToString();
                              

                            }

                        }
                    }
                }



            }

            return comId;

        }

        public RaiseSalesCsUI ReadSalesRepCsById(int Id)
        {
            var obj = new RaiseSalesCsUI();
            obj.Id = Id;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    var strSqlContactStmt = @"SELECT rs.Id, rs.Complaint, rs.OutCome,rs.OrderId, rs.CompanyId,
                                   rs.ContactId,rs.CreatedDate,rs.Question,rs.Status,rs.CsType from RaiseCSSalesRep rs where rs.Id=@rId";


                    cmd.Parameters.AddWithValue("@rId", SqlDbType.Int).Value = Id;
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                var complaint = sdr["Complaint"].ToString();
                                var outCome = "";
                                var csType = "1";
                                var question = "";
                                if (sdr["OutCome"] != DBNull.Value)
                                    outCome = sdr["OutCome"].ToString();
                                if (sdr["Question"] != DBNull.Value)
                                    question = sdr["Question"].ToString();
                                if (sdr["CsType"] != DBNull.Value)
                                    csType = sdr["CsType"].ToString();
                                obj.CsTyte = csType.ToString();
                                obj.Complaint = complaint;
                                obj.OutCome = outCome;
                                obj.orderId = sdr["OrderId"].ToString();
                                obj.contactId = sdr["ContactId"].ToString();
                                obj.companyId = sdr["CompanyId"].ToString();
                                obj.Question = question;
                                if (sdr["Status"] != DBNull.Value)
                                {
                                    if (sdr["Status"].ToString() == "Y")
                                        obj.Status = true;

                                }

                            }

                        }
                    }
                }



            }

            return obj;
        }



        public void UpdateSalesRepCsById(RaiseSalesCsUI obj)
        {

            SqlConnection conn = new SqlConnection();
            var status = "N";
            if (obj.Status)
                status = "Y";

            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE RaiseCSSalesRep SET Complaint=@complaint, 
                 OutCome=@outCome , Question=@question, Status=@status , ModifiedDate=CURRENT_TIMESTAMP WHERE Id=@Id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@Complaint", SqlDbType.VarChar).Value = obj.Complaint;
                cmd.Parameters.Add("@outCome", SqlDbType.VarChar).Value = obj.OutCome;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                cmd.Parameters.Add("@question", SqlDbType.NVarChar).Value = obj.Question;
                cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();
            }
            conn.Close();


        }


        public void DeleteRecordCS(int id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM RaiseCSSalesRep WHERE (Id = @Id)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }


    public enum CSType
    {
        Order=1,
        CreditNote=2
    }

    public class RaiseSalesRep
    {

        public int Id { get; set; }
        public string Complaint { get; set; }
        public string OutCome { get; set; }
        public string Question { get; set; }
        public int OrderId { get; set; }
        public int CompanyId { get; set; }
        public int ContactId { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CsTyte { get; set; }
    }

    public class RaiseSalesCsUI
    {
        public int Id { get; set; }
        public string Complaint { get; set; }
        public string OutCome { get; set; }
        public string Question { get; set; }
        public bool Status { get; set; }
        public string orderId { get; set; }
        public string contactId { get; set; }
        public string companyId { get; set; }
        public string CsTyte { get; set; }
    }

    public class CreditNoteRMAFaultyGoods
    {
        public string CreditNoteId { get; set; }
        public string CompanyId { get; set; }
        public string ContactId { get; set; }
        public int RMAId { get; set; }
        public string BatchNumber { get; set; }
        public string ModelNumber { get; set; }
        public string SupplierName { get; set; }
        public string ErrorMessage { get; set; }
        public string FaultyNotes { get; set; }
    }
}
