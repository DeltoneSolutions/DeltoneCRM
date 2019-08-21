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
    public partial class getAlarmListForCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strOutput = "";
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"] == "ADMIN")
                    {
                        cmd.CommandText = "SELECT AlarmID, StartDate, Description, CreatedBy from dbo.Alarms WHERE CompanyID = " + Request.QueryString["CID"].ToString();
                    }
                    else
                    {
                        cmd.CommandText = "SELECT AlarmID, StartDate, Description, CreatedBy from dbo.Alarms WHERE CompanyID = " + Request.QueryString["CID"].ToString() + " AND UserID = " + Session["LoggedUserID"].ToString();
                    }
                    
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strOutput = strOutput + sdr["AlarmID"].ToString() + "|";
                                strOutput = strOutput + sdr["StartDate"].ToString() + "|";
                                strOutput = strOutput + sdr["Description"].ToString() + "|";
                                strOutput = strOutput + sdr["CreatedBy"].ToString() + "~";
                            }

                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                            conn.Close();
                            Response.Write(strOutput);
                        }
                        else
                        {
                            Response.Write("NONE");
                        }
                        

                    }

                    
                
                }
                conn.Close();

            }
           
        }
    }
}