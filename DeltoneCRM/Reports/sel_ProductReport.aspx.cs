using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

namespace DeltoneCRM.Reports
{
    public partial class sel_ProductReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }


       /// <summary>
       /// Report Generation Click Event handler
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       protected void GR_Click(object sender,EventArgs e)
       {

            String strStartDate = String.Empty;
            String strEndDate = String.Empty;


            if (!String.IsNullOrEmpty(startdate.Value)   && !String.IsNullOrEmpty(enddate.Value))
            {
                String[] arr = startdate.Value.Split('/');
                strStartDate = arr[1].ToString() + "/" + arr[0].ToString() + "/" + arr[2].ToString();
            
                String[] arr_End = enddate.Value.Split('/');
                strEndDate = arr_End[1].ToString() + "/" + arr_End[0].ToString() + "/" + arr_End[2].ToString();

                DateTime dt_StartDate = Convert.ToDateTime(strStartDate);
                DateTime dt_EndDate = Convert.ToDateTime(strEndDate);
                String sitemCode = prodcode.Value.ToString();

                 if(dt_EndDate.Date > dt_StartDate.Date)
                 {

                    PopulateGridView(dt_StartDate, dt_EndDate, sitemCode);
                 }   
            }

        }


        /// <summary>
        /// Populate Grid View given by StartDate and EndDate
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        protected void PopulateGridView(DateTime StartDate,DateTime EndDate,String SItemCode)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Deltone_ProductSearch";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Add Parameter Start_date for the StoredProcedure
                    SqlParameter StartDatein = cmd.Parameters.Add("@strat_date", SqlDbType.DateTime);
                    StartDatein.Direction = ParameterDirection.Input;

                    //Add Parameter End_date for the StoredProcedure
                    SqlParameter EndDateIn = cmd.Parameters.Add("@end_date", SqlDbType.DateTime);
                    EndDateIn.Direction = ParameterDirection.Input;

                    //Add parameter SupplierItemCode for the StoredProcedure
                    SqlParameter SitemCodeIn = cmd.Parameters.Add("@SupplierItemCode", SqlDbType.NVarChar,50);
                    SitemCodeIn.Direction = ParameterDirection.Input;

                    StartDatein.Value = StartDate;
                    EndDateIn.Value = EndDate;
                    SitemCodeIn.Value = SItemCode;

                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvProductList.DataSource = dt;
                    gvProductList.DataBind();

                }
                con.Close();
            }
  
        }


        [WebMethod]
        public static string[] GetSupplierItemCodes(string namePrefix)
        {

            List<string> lstNames = new List<string>();

            DataTable dtNames = new DataTable();

            string sqlQuery = "select distinct SupplierItemCode from dbo.Items where SupplierItemCode like '%" + namePrefix + "%'";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            try
            {

                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                da.Fill(dtNames);

                foreach (DataRow row in dtNames.Rows)

                {

                    string name = Convert.ToString(row["SupplierItemCode"]);

                    lstNames.Add(name);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return lstNames.ToArray<string>();

        }



        [WebMethod]
        public static string[] getProductCodes(string keyword)
        {
            List<string> country = new List<string>();
            string query = string.Format("select distinct SupplierItemCode from dbo.Items where SupplierItemCode '%{0}%'", keyword);

            using (SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        country.Add(reader.GetString(0));
                    }
                }
            }
            return country.ToArray();
        }


    }
}