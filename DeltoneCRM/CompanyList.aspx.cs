using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class CompanyList : System.Web.UI.Page
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

        protected void SearchButton_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                SetGridData(searchTextBox.Text);
            }
            else
                SetGridData("");


        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {

            var comList = new List<CompanyView>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)(row.Cells[0].FindControl("chkSelect"));
                if (chkSelect.Checked)
                {
                    var sbj = new CompanyView();
                    var comID = (Label)(row.Cells[0].FindControl("LabelId"));
                    var comValue = comID.Text;
                    var comName = (Label)(row.Cells[0].FindControl("LabelName"));
                    var comNameValue = comName.Text;
                    var comStatus = (CheckBox)(row.Cells[0].FindControl("StatusCheckBox"));
                    var comStatusValue = comStatus.Checked;
                    var comHoldStatus = (CheckBox)(row.Cells[0].FindControl("StatusHoldCheckBox"));
                    var comHoldStatusValue = comHoldStatus.Checked;

                    sbj.CompanyID = comValue;
                    sbj.CompanyName = comNameValue;
                    sbj.Active = comStatusValue;
                    sbj.Hold = comHoldStatusValue;

                    comList.Add(sbj);
                }
            }
            if (comList.Count() > 0)
            {
                UpdateCompanyStatus(comList);
                LoadSavedCompany(comList);
            }
            else
            {
                messagelable.Text = "Please select a company";
            }
        }


        private void CreateAuditRecord(int primaryKey,string coluhmnpreVious,string columcurrnet)
        {
            var columnName = "Status";
            var talbeName = "Companies";
            var ActionType = "Changed";
           


            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;


            var newvalues = "Company  Id " + primaryKey + " : " + columcurrnet;



            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
           
            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(coluhmnpreVious, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, primaryKey);
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
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        cmd.CommandText = "SELECT CompanyID,CompanyName,Active,Hold,LeadLocked FROM dbo.Companies WHERE CompanyName LIKE '%" + strSearchTerm + "%'";
                    }
                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                    {
                        cmd.CommandText = "SELECT CompanyID,CompanyName,Active,Hold,LeadLocked FROM dbo.Companies WHERE CompanyName LIKE '%" + strSearchTerm + "%' AND OwnershipAdminID IN (" + Session["LoggedUserID"] + ")";
                    }

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
                                sbj.Active = false;
                                sbj.Hold = false;
                                if (sdr["Active"] != DBNull.Value)
                                {
                                    if (sdr["Active"].ToString() == "Y")
                                        sbj.Active = true;
                                }
                                if (sdr["Hold"] != DBNull.Value)
                                {
                                    if (sdr["Hold"].ToString() == "Y")
                                        sbj.Hold = true;
                                }
                                if (sdr["LeadLocked"] != DBNull.Value)
                                {
                                    if (sdr["LeadLocked"].ToString() == "Y")
                                        sbj.LeadLocked = true;
                                }

                                comList.Add(sbj);
                            }
                        }
                    }


                }

            }

            return
                comList;
        }

        public void UpdateCompanyStatus(List<CompanyView> itemList)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var comStatusMesage = "";
            var comLocked = "";

            try
            {

                foreach (var item in itemList)
                {
                    String SQLStmt = "UPDATE dbo.Companies SET Active =@active , Hold =@hold  WHERE CompanyID =@comId ";
                    SqlCommand cmd = new SqlCommand(SQLStmt, conn);
                    conn.Open();
                    var activeStatus = "N";
                    comStatusMesage = " Inactive ";
                    if (item.Active)
                    {
                        activeStatus = "Y";
                        comStatusMesage = " Active ";
                    }
                    cmd.Parameters.AddWithValue("@active", activeStatus);
                    var holdStatus = "N";
                    comLocked = " Not Locked";
                    if (item.Hold)
                    {
                        holdStatus = "Y";
                        comLocked = " Locked";
                    }
                    cmd.Parameters.AddWithValue("@hold", holdStatus);
                    var comID = Convert.ToInt32(item.CompanyID);
                    cmd.Parameters.AddWithValue("@comId", comID);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    var comstatusMess = comStatusMesage + " - " + comLocked;
                        CreateAuditRecord(Convert.ToInt32(item.CompanyID)
                            , "", comstatusMess);
                    
                }

                
                return;
            }
            catch (Exception ex)
            {
                return;
            }
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
                if (!string.IsNullOrEmpty(searchTextBox.Text))
                {
                    SetGridData(searchTextBox.Text);
                }
                else
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

        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var stringSearch = "";
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                stringSearch = searchTextBox.Text;
            }
            var comList = FillCompany(stringSearch);
            if (Session["sorList"] == null)
                Session["sorList"] = comList;
            var sessionLit = Session["sorList"] as List<CompanyView>;

            string SortDirection = "DESC";
            if (ViewState["SortExpression"] != null)
            {
                if (ViewState["SortExpression"].ToString() == e.SortExpression)
                {
                    ViewState["SortExpression"] = null;
                    SortDirection = "ASC";
                }
                else
                {
                    ViewState["SortExpression"] = e.SortExpression;
                }
            }
            else
            {
                ViewState["SortExpression"] = e.SortExpression;
            }

            var sortDirection = e.SortDirection;
            var sortExpression = e.SortExpression;
            if (Session["sortDire"] == null)
                Session["sortDire"] = "Asc," + sortExpression;

            //var splitDir = "";
            //if (Session["sortDire"] != null)
            //    splitDir = Session["sortDire"].ToString().Split(',')[1];
            if (SortDirection == "ASC")
            {
                if (sortExpression == "CompanyID")
                    sessionLit = sessionLit.OrderBy(x => x.CompanyID).ToList();
                else if (sortExpression == "CompanyName")
                    sessionLit = sessionLit.OrderBy(x => x.CompanyName).ToList();
                else if (sortExpression == "Active")
                    sessionLit = sessionLit.OrderBy(x => x.Active).ToList();
                else if (sortExpression == "Hold")
                    sessionLit = sessionLit.OrderBy(x => x.Hold).ToList();
                else
                    sessionLit = sessionLit.OrderBy(x => x.LeadLocked).ToList();
            }
            else
            {


                if (sortExpression == "CompanyID")
                    sessionLit = sessionLit.OrderByDescending(x => x.CompanyID).ToList();
                else if (sortExpression == "CompanyName")
                    sessionLit = sessionLit.OrderByDescending(x => x.CompanyName).ToList();
                else if (sortExpression == "Active")
                    sessionLit = sessionLit.OrderByDescending(x => x.Active).ToList();
                else if (sortExpression == "Hold")
                    sessionLit = sessionLit.OrderByDescending(x => x.Hold).ToList();
                else
                    sessionLit = sessionLit.OrderByDescending(x => x.LeadLocked).ToList();
            }



            GridView1.DataSource = sessionLit;
            GridView1.DataBind();

        }

        protected void LeadButton_Click(object sender, EventArgs e)
        {
            var comList = new List<CompanyView>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)(row.Cells[0].FindControl("chkSelect"));
                if (chkSelect.Checked)
                {
                    var sbj = new CompanyView();
                    var comID = (Label)(row.Cells[0].FindControl("LabelId"));
                    var comValue = comID.Text;
                    var comName = (Label)(row.Cells[0].FindControl("LabelName"));
                    var comNameValue = comName.Text;
                    var comleadStatus = (CheckBox)(row.Cells[0].FindControl("StatusLeadCheckBox"));
                    var comleadStatusValue = comleadStatus.Checked;
                 

                    sbj.CompanyID = comValue;
                    sbj.CompanyName = comNameValue;
                    sbj.LeadLocked = comleadStatusValue;

                    comList.Add(sbj);
                }
            }
            if (comList.Count() > 0)
            {
                UpdateLeadCompanyLock(comList);
                LoadSavedCompany(comList);
            }
            else
            {
                messagelable.Text = "Please select a company";
            }
        }

        private void UpdateLeadCompanyLock(List<CompanyView> itemList)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var comStatusMesage = "";
            try
            {
                foreach (var item in itemList)
                {
                    var queryString = "UPDATE dbo.Companies SET LeadLocked =@leadLoked  WHERE CompanyID =@comId ";
                    SqlCommand cmd = new SqlCommand(queryString, conn);
                    conn.Open();
                    var lockedStatus = "N";
                    comStatusMesage = " Inactive ";
                    if (item.LeadLocked)
                    {
                        lockedStatus = "Y";
                    }
                    cmd.Parameters.AddWithValue("@comId", item.CompanyID);
                    cmd.Parameters.AddWithValue("@leadLoked", lockedStatus);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    CreateAuditRecord(Convert.ToInt32(item.CompanyID)
                         , "", lockedStatus);

                }
            }

            catch (Exception ex)
            {
                return;
            }

        }

        public class CompanyView
        {
            public string CompanyName { get; set; }
            public string CompanyID { get; set; }
            public bool Active { get; set; }
            public bool Hold { get; set; }
            public bool LeadLocked { get; set; }
        }


    }
}