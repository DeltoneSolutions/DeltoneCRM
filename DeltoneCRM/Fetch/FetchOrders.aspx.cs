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
    public partial class FetchOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["CompanyID"]) && !String.IsNullOrEmpty(Request.QueryString["ContactID"]))
            {
                String strCompanyID = Request.QueryString["CompanyID"].ToString();
                String strContactID = Request.QueryString["ContactID"].ToString();
                Response.Write(ReturnOrders(Int32.Parse(strCompanyID), Int32.Parse(strContactID)));
            }
        }

        protected String ReturnOrders(int CompanyID,int ContactID)
        {
            String strOutput = "{\"data\":[";
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                        cmd.CommandText = "SELECT * FROM dbo.Orders where CompanyID=" + CompanyID + "AND ContactID=" + ContactID;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                //EditOrder(OrderID, CompanyID, ContactID)
                                String strEditOrder = "<img src='Images/Edit.png'  onclick='EditOrder(" + sdr["OrderID"] + ","+ CompanyID + ","+ ContactID+")'/>";
                                strOutput = strOutput + "[\"" + sdr["OrderID"] + "\"," + "\"" + sdr["OrderedDateTime"] + "\"," +   "\""+ sdr["CreatedBy"] +"\","+ "\"" + strEditOrder + "\"],";
                                    
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