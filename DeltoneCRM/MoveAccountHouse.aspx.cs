using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class MoveAccountHouse : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

            if (!IsPostBack)
                SetGridData("");

        }

        private void SetGridData(string stringSearch)
        {

            GetGridData(stringSearch);

        }

        private void GetGridData(string stringSearch)
        {
            var comList = FillCompany(stringSearch);
            comList = comList.OrderBy(c => c.CompanyName).ToList();
            GridView1.DataSource = comList;
            GridView1.DataBind();
        }



        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var hourAcco = "7";
            if (Session["sorList"] != null)
            {
                var comList = Session["sorList"] as List<CompanyView>;
                foreach (var item in comList)
                {
                    var comdal = new CompanyDAL(CONNSTRING);
                    var resComId = comdal.CheckCompanyOwnerByID(item.CompanyID);
                    if (string.IsNullOrEmpty(resComId))
                    {
                        comdal.CreateOwnerCompanyId(item.CompanyID, item.OwnershipAdminID.ToString());
                        
                    }
                    PerformMove(item.CompanyID, hourAcco);
                }
            }

        }

        protected void PerformMove(string CID, string acout)
        {
            SqlConnection LAconn = new SqlConnection();
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LAconn.ConnectionString = conn;
            var comDal = new CompanyDAL(conn);
            var OwnerId = acout;

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
                Console.Write(ex.Message.ToString());
            }

        }



        private void LoadSavedCompany(List<CompanyView> item)
        {

            messagelable.Text = "Successfully Updated";

            GridView1.DataSource = item;
            GridView1.DataBind();

        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("dashboard1.aspx");



        }

        private List<CompanyView> FillCompany(string strSearchTerm)
        {
            var comList = new List<CompanyView>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT Distinct CP.CompanyName, CP.CompanyID,Cp.OwnershipAdminID, max(ods.CreatedDateTime ) as lastorderdate
FROM [dbo].Companies CP 
left join [dbo].Orders ods
on ods.CompanyID=CP.CompanyID
where Cp.OwnershipAdminID<>7
group by CP.CompanyName, CP.CompanyID,Cp.OwnershipAdminID";

                    cmd.Connection = conn;
                    conn.Open();


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var sbj = new CompanyView();
                                sbj.CompanyID = sdr["CompanyID"].ToString();
                                sbj.CompanyName = sdr["CompanyName"].ToString();
                                sbj.OwnershipAdminID = Convert.ToInt32(sdr["OwnershipAdminID"].ToString());
                                if (sdr["lastorderdate"] != DBNull.Value)
                                {
                                    var lastORderDate = Convert.ToDateTime(sdr["lastorderdate"].ToString());
                                    var calastORderDate = lastORderDate.AddYears(1);
                                    var currentDate = DateTime.Now;
                                    if (calastORderDate < currentDate)
                                    {
                                        sbj.LastOrderDate = lastORderDate.ToShortDateString();
                                        comList.Add(sbj);
                                    }
                                }
                                else
                                {
                                    comList.Add(sbj);
                                }



                            }
                        }
                    }


                }

            }
            Session["sorList"] = comList;
            return
                comList;
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            if (Session["sorList"] != null)
            {
                var sessionLit = Session["sorList"] as List<CompanyView>;
                GridView1.DataSource = sessionLit;
                GridView1.DataBind();
            }
            else
            {

                SetGridData("");
            }
        }
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }


        public class CompanyView
        {
            public string CompanyName { get; set; }
            public string CompanyID { get; set; }
            public string LastOrderDate { get; set; }
            public int OwnershipAdminID { get; set; }
            public bool Active { get; set; }
            public bool Hold { get; set; }
        }
    }
}