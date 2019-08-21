using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM.Fetch
{
    public partial class FetchAllTargets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnAllItems());
        }

        protected string ReturnAllItems()
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT TG.TargetID, TG.LoginID, LG.FirstName, TG.TargetMonth,TG.TargetYear,LG.LastName, TG.TargetCommission, TG.WorkingDays FROM dbo.Targets TG, dbo.Logins LG WHERE TG.LoginID = LG.LoginID";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            var month = sdr["TargetMonth"].ToString();
                            var mnoTar = 0;
                            if (int.TryParse(month, out mnoTar))
                            {
                                mnoTar = Convert.ToInt32(month);
                            }

                            if (mnoTar < 10)
                            {
                                if (month.Replace("0", "") == "1")
                                    month = "January";
                                else
                                    if (sdr["TargetMonth"].ToString().Replace("0", "") == "2")
                                        month = "February";
                                    else if (sdr["TargetMonth"].ToString().Replace("0", "") == "3")
                                        month = "March";
                                    else if (sdr["TargetMonth"].ToString().Replace("0", "") == "4")
                                        month = "April";
                                    else if (sdr["TargetMonth"].ToString().Replace("0", "") == "5")
                                        month = "May";
                                    else if (sdr["TargetMonth"].ToString().Replace("0", "") == "6")
                                        month = "June";
                                    else if (sdr["TargetMonth"].ToString().Replace("0", "") == "7")
                                        month = "July";
                                    else if (sdr["TargetMonth"].ToString().Replace("0", "") == "8")
                                        month = "August";
                                    else if (sdr["TargetMonth"].ToString().Replace("0", "") == "9")
                                        month = "September";

                            }
                            else if (sdr["TargetMonth"].ToString() == "10")
                                month = "October";
                            else if (sdr["TargetMonth"].ToString() == "11")
                                month = "November";
                            else if (sdr["TargetMonth"].ToString() == "12")
                                month = "December";



                            Decimal strCommission = (Decimal)(sdr["TargetCommission"]);
                            String strEdit = "<img src='../Images/Edit.png'  onclick='Edit(" + sdr["TargetID"] + ")'/>";
                            strOutput = strOutput + "[\"" + sdr["TargetID"] + "\"," + "\""
                                + sdr["FirstName"] + " " + sdr["LastName"] + "\","
                                + "\"" + sdr["TargetYear"].ToString() + "\","
                                 + "\"" + month + "\","
                                  + "\"" + string.Format("{0:C}", strCommission) + "\","
                                + "\"" + sdr["WorkingDays"] + "\"," + "\"" + strEdit + "\"],";

                        }
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));

                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }

            }
            return strOutput;
        }
    }
}