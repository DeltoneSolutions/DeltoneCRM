using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;





namespace DeltoneCRM.Fetch
{
    public partial class FetchCompanyListbyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strLogin = Request.Form["LoginDetails"].ToString();
        }

        public int getOwnershipAdminID()
        {
            int AdminID = 0;


            return AdminID;

        }

        public String GetCompanyList(int AdminID)
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select cm.OwnershipAdminID, cm.CompanyName, cn.FirstName + ' ' + cn.LastName as CompanyContactName, cm.CompanyID from Companies cm,Contacts cn where cm.CompanyID =cn.CompanyID and cm.OwnershipAdminID=" + AdminID;

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            String strAdminID = sdr["OwnershipAdminID"].ToString();
                            String strCompnayName = sdr["CompanyName"].ToString();
                            String strCompnayContactName = sdr["CompanyContactName"].ToString();
                            String strCompanyID = sdr["CompanyID"].ToString();
                            String strViewContact = "<img src='Images/Edit.png'  onclick='LoadView(" + sdr["ContactID"] + ")'/>";


                            //Modification Done here
                            strOutput = strOutput + "[\"" + strAdminID + "\"," + "\"" + strCompnayName + "\"," + "\"" + strCompnayContactName + "\"," + "\"" + strViewContact + "\"," + "\"" + strViewContact + "\"],";


                        }
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));

                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }

                return strOutput;

            }
        }




    }
}