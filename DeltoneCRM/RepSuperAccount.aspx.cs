using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class RepSuperAccount : System.Web.UI.Page
    {
        private string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

            if (Session["USERPROFILE"].ToString() == "ADMIN")
            {
                repDropsel.Visible = true;
                btnsave.Visible = true;
            }

            if (!IsPostBack){
                SetGridData("");
            PopulateDropDownList();

            }
        }

        protected void PopulateDropDownList()
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins WHERE Active = 'Y' and LoginID NOT IN ( 25, 19,18,23,26,8,21,22)", conn);
                adapter.Fill(subjects);
                RepNameDropDownList.AppendDataBoundItems = true;
                RepNameDropDownList.DataSource = subjects;
                RepNameDropDownList.DataTextField = "FullName";
                RepNameDropDownList.DataValueField = "LoginID";
                RepNameDropDownList.DataBind();
            }
        }
                

        protected void callGridUpdate(object sender, EventArgs e)
        {
            SetGridData("");
        }

        private void SetGridData(string stringSearch, bool canSize = false)
        {
            GetGridData(stringSearch);
        }

        private void GetGridData(string stringSearch, bool canSize = false)
        {
            GridView1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            var comList = FillCompany(stringSearch);
            comList = comList.OrderBy(c => c.CompanyName).ToList();
            GridView1.DataSource = comList;
            GridView1.DataBind();



            //  PopulatePager(comList.Count(), 1);
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

        private int GetSupperAccountLimit()
        {
            var cou = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)(row.Cells[0].FindControl("StatusCheckBox"));
                if (chkSelect.Checked)
                {
                    cou = cou + 1;
                }
            }

            return cou;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //if (GetSuperAccountCount() > 20)
            //{
            //    messagelable.Text = "You can select only 20 Super Accounts";
            //    return;
            //}

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

                    sbj.CompanyID = comValue;
                    sbj.CompanyName = comNameValue;
                    sbj.IsSupperAccount = comStatusValue;

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

        private void CreateAuditRecord(int primaryKey, string coluhmnpreVious, string columcurrnet)
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

        private int GetSuperAccountCount()
        {
            var count = 0;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = CONNSTRING;
                var canExe = false;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        var dropVal = RepNameDropDownList.SelectedValue;
                        if (dropVal != "0")
                        {
                            canExe = true;
                            cmd.CommandText = @"SELECT COUNT(CompanyID) as coCount from Companies WHERE IsSupperAcount =1 AND OwnershipAdminID=@ownerID";
                            cmd.Parameters.AddWithValue("@ownerID", dropVal);

                        }

                    }
                    else
                    {
                        var userID = Session["LoggedUserID"];
                        canExe = true;
                        cmd.CommandText = @"SELECT COUNT ( COMPANYID) as coCount FROM Companies WHERE IsSupperAcount =1 AND OwnershipAdminID=@ownerID";
                        cmd.Parameters.AddWithValue("@ownerID", userID);
                    }

                    if (canExe)
                    {
                        cmd.Connection = con;
                        cmd.Connection.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {
                                    count = Convert.ToInt32(sdr["coCount"].ToString());
                                }
                            }
                        }
                    }
                }
                con.Close();
            }

            return count;
        }

        private List<CompanyView> FillCompany(string strSearchTerm)
        {
            var comList = new List<CompanyView>();
            var userID = Session["LoggedUserID"];
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        var dropVal = RepNameDropDownList.SelectedValue;
                        if (dropVal == "0")
                            cmd.CommandText = @"SELECT CP.CompanyID,CompanyName,IsSupperAcount  ,
                      lc.FirstName + ' ' + lc.LastName AS createdUser FROM dbo.Companies CP
                                  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID
                             WHERE CompanyName LIKE '%" + strSearchTerm + "%'  ";
                        else
                            cmd.CommandText = @"SELECT CP.CompanyID,CompanyName,IsSupperAcount ,
lc.FirstName + ' ' + lc.LastName AS createdUser FROM dbo.Companies CP
                                  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID
                            WHERE CompanyName LIKE '%" + strSearchTerm + "%'  AND OwnershipAdminID = " + dropVal + " ";
                    }
                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                    {
                        cmd.CommandText = @"SELECT CP.CompanyID,CompanyName,IsSupperAcount , lc.FirstName + ' ' + lc.LastName AS createdUser
                               FROM dbo.Companies CP
                                  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID WHERE CompanyName LIKE '%" + strSearchTerm + "%' AND OwnershipAdminID =" + userID + " AND IsSupperAcount=1 ";
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
                                sbj.CreateUser = sdr["createdUser"].ToString();
                                sbj.IsSupperAccount = false;
                                if (sdr["IsSupperAcount"] != DBNull.Value)
                                {
                                    sbj.IsSupperAccount = Convert.ToBoolean(sdr["IsSupperAcount"]);
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
                    String SQLStmt = "UPDATE dbo.Companies SET IsSupperAcount =@isSupperAcount   WHERE CompanyID =@comId ";
                    SqlCommand cmd = new SqlCommand(SQLStmt, conn);
                    conn.Open();

                    if (item.IsSupperAccount)
                    {
                        cmd.Parameters.AddWithValue("@isSupperAcount", item.IsSupperAccount);
                        var comID = Convert.ToInt32(item.CompanyID);
                        cmd.Parameters.AddWithValue("@comId", comID);
                        cmd.ExecuteNonQuery();
                       
                    }

                    var comstatusMess = comStatusMesage + " - " + comLocked;
                    CreateAuditRecord(Convert.ToInt32(item.CompanyID)
                        , "", comstatusMess);
                    conn.Close();
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
                else if (sortExpression == "IsSupperAccount")
                    sessionLit = sessionLit.OrderBy(x => x.IsSupperAccount).ToList();
            }
            else
            {
                if (sortExpression == "CompanyID")
                    sessionLit = sessionLit.OrderByDescending(x => x.CompanyID).ToList();
                else if (sortExpression == "CompanyName")
                    sessionLit = sessionLit.OrderByDescending(x => x.CompanyName).ToList();
                else if (sortExpression == "IsSupperAccount")
                    sessionLit = sessionLit.OrderByDescending(x => x.IsSupperAccount).ToList();
            }

            GridView1.DataSource = sessionLit;
            GridView1.DataBind();
        }

        protected void PageSize_Changed(object sender, EventArgs e)
        {

            this.SetGridData("", true);
        }


        public class CompanyView
        {
            public string CompanyName { get; set; }
            public string CompanyID { get; set; }
            public string CreateUser { get; set; }
            public bool IsSupperAccount { get; set; }
        }
    }
}