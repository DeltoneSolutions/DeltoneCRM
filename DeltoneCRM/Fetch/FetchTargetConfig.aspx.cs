using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchTargetConfig : System.Web.UI.Page
    {
        Dictionary<String, DelReport_Contact> di_getStat = new Dictionary<String, DelReport_Contact>();
        DelReport_Contact obj_Contact;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(HandleConfigTarget());
        }
        public void getRepName()
        {
            DelReport_Contact obj_contact;

            List<string> RepsName = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT LoginID, FirstName, LastName,Department,Commission , DonotShowOnStats FROM dbo.Logins 
                          WHERE LoginID  IN (1,2,10,15,17,35,41,42,43,44,45) AND Active = 'Y'  ";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                var comme = 0;
                                if (sdr["Commission"] != DBNull.Value)
                                {
                                    comme = Convert.ToInt32(sdr["Commission"].ToString());
                                    if (comme > 0)
                                    {

                                        {
                                            RepsName.Add(sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString());

                                            obj_contact = new DelReport_Contact();
                                            obj_contact.DepartmentId = "3";// null avoid
                                            obj_contact.FirstName = sdr["FirstName"].ToString();
                                            obj_contact.LastName = sdr["LastName"].ToString();
                                            obj_contact.LoginId = Convert.ToInt32(sdr["LoginID"].ToString());
                                            if (sdr["Department"] != DBNull.Value)
                                                obj_contact.DepartmentId = sdr["Department"].ToString();
                                            di_getStat.Add(sdr["LoginID"].ToString(), obj_contact);
                                        }
                                    }
                                }


                            }
                        }

                    }
                    conn.Close();

                }

            }

        }
        public string HandleConfigTarget()
        {
            string re = "";

            var connectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var targetConfig = new RepDayOffDAL(connectionString);

            var listConfig = targetConfig.GetAllTargetItems();
            var currentDay=DateTime.Now.Date;
            listConfig = (from cons in listConfig where cons.TargetDay.Date == currentDay 
                              select cons).ToList();

            if (listConfig.Count() > 0)
            {
                getRepName();
                re = listConfig[0].TargetTitle + "," + Math.Truncate( listConfig[0].TargetAmount);
            }

            float dailyVolume = 0.0f;

            foreach (var pair in di_getStat)
            {
                    obj_Contact = (DelReport_Contact)pair.Value;
                    String repName = obj_Contact.FirstName+ " " + obj_Contact.LastName;
                    dailyVolume = dailyVolume + getDailyVolume(repName);
            }
            if (!string.IsNullOrEmpty(re))
            {
                re = re + "," + dailyVolume + "," + listConfig[0].Id + "," + listConfig[0].IsReached;
            }

            return re;
        }

        public float getDailyVolume(String repname)
        {

            float DVWithoutSplit = 0;
            float DVWithSplitAO = 0;
            float DVWithSplitSW = 0;

            float DCWithoutSplit = 0;
            float DCWithSplitAO = 0;
            float DCWithSplitSW = 0;

            float DailyVolume = 0;
            float DailySales = 0;
            float DailyCredits = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname + "' AND DAY(CreatedDateTime) = DAY(getDate()) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DVWithoutSplit = 0;
                                }
                                else
                                {
                                    DVWithoutSplit = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            DVWithoutSplit = 0;
                        }

                    }
                    conn.Close();

                }


                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname + "' AND DAY(CreatedDateTime) = DAY(getDate()) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DVWithSplitAO = 0;
                                }
                                else
                                {
                                    DVWithSplitAO = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            DVWithSplitAO = 0;
                        }

                    }
                    conn.Close();

                }

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE OrderEnteredBy = '" + repname + "' AND DAY(CreatedDateTime) = DAY(getDate()) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DVWithSplitSW = 0;
                                }
                                else
                                {
                                    DVWithSplitSW = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            DVWithSplitSW = 0;
                        }

                    }
                    conn.Close();

                }

                DailySales = DVWithoutSplit + DVWithSplitAO + DVWithSplitSW;

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname + "' AND DAY(DateCreated) = DAY(getDate()) AND MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DCWithoutSplit = 0;
                                }
                                else
                                {
                                    DCWithoutSplit = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            DCWithoutSplit = 0;
                        }
                    }
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname + "' AND DAY(DateCreated) = DAY(getDate()) AND MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DCWithSplitAO = 0;
                                }
                                else
                                {
                                    DCWithSplitAO = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            DCWithSplitAO = 0;
                        }
                    }
                    conn.Close();
                }


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE Salesperson = '" + repname + "' AND DAY(DateCreated) = DAY(getDate()) AND MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DCWithSplitSW = 0;
                                }
                                else
                                {
                                    DCWithSplitSW = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            DCWithSplitSW = 0;
                        }
                    }
                    conn.Close();
                }

            }

            DailyCredits = DCWithoutSplit + DCWithSplitAO + DCWithSplitSW;

            //DailyCredits = DailyCredits - DailyCredits;

            DailyVolume = DailySales - DailyCredits;

            return DailyVolume;
        }
    }
}