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
    public partial class VerifyIfEmailExists : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             String responseString = String.Empty;

             using (SqlConnection conn = new SqlConnection())
             {
                 conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
                 using (SqlCommand cmd = new SqlCommand())
                 {
                     cmd.CommandText = "SELECT ContactID FROM dbo.Contacts WHERE Email = '" + Request.QueryString["Email"].ToString() + "'";
                     cmd.Connection = conn;
                     conn.Open();

                     using (SqlDataReader sdr = cmd.ExecuteReader())
                     {
                         if (sdr.HasRows)
                         {
                             responseString = responseString + "RED|";
                         }
                         else
                         {
                             responseString = responseString + "GREEN|";
                         }
                     }
                 }
                 conn.Close();

                 using (SqlCommand cmd2 = new SqlCommand())
                 {
                     cmd2.CommandText = "SELECT ContactID FROM dbo.Quote_Contacts WHERE Email = '" + Request.QueryString["Email"].ToString() + "'";
                     cmd2.Connection = conn;
                     conn.Open();

                     using (SqlDataReader sdr = cmd2.ExecuteReader())
                     {
                         if (sdr.HasRows)
                         {
                             responseString = responseString + "RED";
                         }
                         else
                         {
                             responseString = responseString + "GREEN";
                         }
                     }
                 }
                 conn.Close();

                 Response.Write(responseString);
             }
        }
    }
}