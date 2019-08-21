using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace DeltoneCRM
{
    public partial class CompanyContactInfo : System.Web.UI.Page
    {
        public string canCallDefault = "true";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["theCID"] = Request.QueryString["cid"].ToString();
            var holdStats = GetStatusOfCompany(Session["theCID"].ToString());
            if (Session["USERPROFILE"].ToString() == "ADMIN")
            {
                if (holdStats == "Y")
                    Td1unarchive.Visible = true;
                else
                    Td1archive.Visible = true;
            }
            var supStatus = GetSuperStatusOfCompany(Session["theCID"].ToString());

            if (supStatus == true)
                removesuperTD.Visible = true;
            else
                setsuperTD.Visible = true;


            if (!string.IsNullOrWhiteSpace(holdStats))
            {
                if (holdStats == "Y")
                {
                    canCallDefault = "false";
                    populateValuesHold(Session["theCID"].ToString());
                }
                else
                    populateValues(Session["theCID"].ToString());
            }
            else
                populateValues(Session["theCID"].ToString());
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

        public void UpdateCompanyStatus(int comId,bool statusSuper )
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            var comStatusMesage = "";
            var comLocked = "";

            try
            {
                
                    String SQLStmt = "UPDATE dbo.Companies SET IsSupperAcount =@isSupperAcount   WHERE CompanyID =@comId ";
                    SqlCommand cmd = new SqlCommand(SQLStmt, conn);
                    conn.Open();

                     cmd.Parameters.AddWithValue("@isSupperAcount", statusSuper);

                     cmd.Parameters.AddWithValue("@comId", comId);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    

                    var comstatusMess = comStatusMesage + " - " + comLocked;
                    CreateAuditRecord(comId
                        , "", comstatusMess);
               

                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private int GetSuperAccountCount()
        {
            var count = 0;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
               
                using (SqlCommand cmd = new SqlCommand())
                {
                   
                        var userID = Session["LoggedUserID"];
                      
                        cmd.CommandText = @"SELECT COUNT ( COMPANYID) as coCount FROM Companies WHERE IsSupperAcount =1 AND OwnershipAdminID=@ownerID";
                        cmd.Parameters.AddWithValue("@ownerID", userID);
                  
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
                con.Close();
            }

            return count;
        }

        private string GetStatusOfCompany(string comId)
        {
            var activeHold = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT Hold FROM dbo.Companies WHERE CompanyID =" + comId;


                    cmd.Connection = conn;
                    conn.Open();


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["Hold"] != DBNull.Value)
                                activeHold = sdr["Hold"].ToString();
                        }
                    }


                    conn.Close();
                }
            }

            return activeHold;
        }

        private bool GetSuperStatusOfCompany(string comId)
        {
            var activeHold = false;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT IsSupperAcount FROM dbo.Companies WHERE CompanyID =" + comId;


                    cmd.Connection = conn;
                    conn.Open();


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["IsSupperAcount"] != DBNull.Value)
                                activeHold = Convert.ToBoolean( sdr["IsSupperAcount"]);
                        }
                    }


                    conn.Close();
                }
            }

            return activeHold;
        }

        private void populateValues(String cid)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strCompanyInfo = "SELECT CP.*, LG.FirstName, LG.LastName FROM dbo.Companies CP, dbo.Logins LG WHERE CompanyID = " + cid + " AND CP.OwnershipAdminID = LG.LoginID";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strCompanyInfo;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CompanyName.InnerHtml = sdr["CompanyName"].ToString();
                       var status= GetSuperStatusOfCompany(sdr["CompanyID"].ToString());
                       if (status)
                       {
                           CompanyName.InnerHtml = CompanyName.InnerHtml + " -- SUPER ACCOUNT ";
                       }
                        Website.InnerHtml = "Website: " + sdr["CompanyWebsite"].ToString() + "  |  Account Owner: " + sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                        CompID.Value = sdr["CompanyID"].ToString();
                        if (Session["USERPROFILE"].ToString() == "ADMIN")
                        {
                            EditCompDIV.InnerHtml = "<a href='#' onclick='window.parent.Edit(" + cid + ")'><img ID='Image2' src='Images/btn_02.png' width='24' height='24'/></a>";
                        }
                        else
                        {
                            EditCompDIV.InnerHtml = "<img ID='Image2' src='Images/btn_02.png' width='24' height='24'/>";
                        }
                        var repName = LeadAllocatedTo(sdr["CompanyID"].ToString());
                        if (!string.IsNullOrEmpty(repName))
                        {
                            Website.InnerHtml = Website.InnerHtml + " | Lead allocated to " + repName;
                        }

                    }
                }
                conn.Close();
            }


        }

        private string LeadAllocatedTo(string comId)
        {
            string repName = "";

            var query = @"SELECT Distinct CP.CompanyName, lc.FirstName + ' ' + lc.LastName AS createdUser
                               FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId   
                         inner join Logins lc on lo.UserId=lc.LoginID AND CP.OwnershipAdminID  <>  lo.UserId
                        AND CP.CompanyID = " + comId + " AND convert(varchar(10),lo.ExpiryDate, 120) >= CAST(getdate() as date) ";

            SqlConnection conSql = new SqlConnection();
            conSql.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                cmd.Connection = conSql;
                conSql.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            repName = sdr["createdUser"].ToString();
                            return repName;
                        }

                    }
                }
                conSql.Close();

            }


            return repName;
        }

        private void populateValuesHold(String cid)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strCompanyInfo = "SELECT CP.*, LG.FirstName, LG.LastName FROM dbo.Companies CP, dbo.Logins LG WHERE CompanyID = " + cid + " AND CP.OwnershipAdminID = LG.LoginID";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strCompanyInfo;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CompanyName.InnerHtml = sdr["CompanyName"].ToString() + " - Account is being Locked";
                        Website.InnerHtml = "Website: " + sdr["CompanyWebsite"].ToString() + "  |  Account Owner: " + sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                        CompID.Value = sdr["CompanyID"].ToString();
                        // EditCompDIV.Attributes.Add("style","background-color: gray;");
                        tablecom.Attributes.Add("style", "background-color: gray;");
                        if (Session["USERPROFILE"].ToString() == "ADMIN")
                        {
                            EditCompDIV.InnerHtml = "<a href='#' onclick='window.parent.Edit(" + cid + ")'><img ID='Image2' src='Images/btn_02.png' width='24' height='24'/></a>";
                        }
                        else
                        {
                            EditCompDIV.InnerHtml = "<img ID='Image2' src='Images/btn_02.png' width='24' height='24'/>";
                        }

                    }
                }
                conn.Close();
            }


        }



        public String populateContacts()
        {
            String htmlStr = "";
            String contactName = "";
            String contactEmail = "";
            String editLink = "";
            String CreateOrderLink = "";
            string CreateDummyOrderLink = "";

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            if (canCallDefault == "true")
            {
                String strContactInfo = "SELECT * FROM dbo.Contacts WHERE CompanyID = " + Session["theCID"].ToString() + " AND Active = 'Y'";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strContactInfo;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            contactName = sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString();
                            contactEmail = sdr["Email"].ToString() + " | " + sdr["DEFAULT_AreaCode"].ToString() + " " + sdr["DEFAULT_Number"].ToString() + " | " + sdr["STREET_AddressLine1"].ToString() + " " + sdr["STREET_AddressLine2"].ToString() + " " + sdr["STREET_City"].ToString() + " " + sdr["STREET_Region"].ToString() + " " + sdr["STREET_PostalCode"].ToString();
                            editLink = "<input name='EditContact' style='cursor:pointer' type='button' id='EditContact' class='contacts-btn-edit' value='EDIT' onclick='window.parent.EditContact(" + sdr["ContactID"].ToString() + ")'/>";
                            CreateOrderLink = "<input name='CreateOrder' style='cursor:pointer' type='button' id='CreateOrder' class='contacts-btn-create-ordr' value='CREATE ORDER' onclick='window.parent.CreateOrder(" + sdr["ContactID"].ToString() + ", " + Session["theCID"].ToString() + ")'/>";
                            CreateDummyOrderLink = "<input name='CreateOrderDummy' style='cursor:pointer' type='button' id='CreateOrderDummy' class='contacts-btn-edit' value='CDO' onclick='window.parent.CreateOrderDummy(" + sdr["ContactID"].ToString() + ", " + Session["theCID"].ToString() + ")'/>";
                            htmlStr += "<tr><td><table align='center' cellpadding='0' cellspacing='0' class='width-980px-style'><tr><td class='contacts-001-style'>" + contactName + "</td><td class='contacts-002-style'>&nbsp;</td><td class='contacts-002-style'>&nbsp;</td><td class='contacts-003-style'>&nbsp;</td></tr><tr><td class='contacts-004-style'>" + contactEmail
                                + "</td><td class='contacts-005-style'>" + editLink +
                                "</td><td class='contacts-006-style'>"
                                + CreateOrderLink + "</td>  <td class='contacts-005-style' >"
                                + CreateDummyOrderLink + "</td>     </tr></table></td></tr>";
                        }
                    }
                    conn.Close();

                }

                return htmlStr;
            }
            else
            {

                OverallContactTbl.Attributes.Add("style", " background-color: #cccccc !important;");
                OverallContactTbl.Attributes.Add("style", " background-color: #cccccc !important;");
                contactvisibletd.Style.Add("display", "none;"); contactvisibletdNo.Style.Add("display", "none;");
                showprinterTr.Style.Add("display", "none;");
                showcompanynameTr.Style.Add("display", "none;");
                String strContactInfo = "SELECT * FROM dbo.Contacts WHERE CompanyID = " + Session["theCID"].ToString() + " AND Active = 'Y'";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strContactInfo;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            contactName = sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString();
                            contactEmail = sdr["Email"].ToString() + " | " + sdr["DEFAULT_AreaCode"].ToString() + " " + sdr["DEFAULT_Number"].ToString() + " | " + sdr["STREET_AddressLine1"].ToString() + " " + sdr["STREET_AddressLine2"].ToString() + " " + sdr["STREET_City"].ToString() + " " + sdr["STREET_Region"].ToString() + " " + sdr["STREET_PostalCode"].ToString();
                            editLink = "<input name='EditContact' style='cursor:pointer' type='button' id='EditContact' class='contacts-btn-edit' value='EDIT' onclick='window.parent.EditContact(" + sdr["ContactID"].ToString() + ")'/>";
                            CreateOrderLink = "<input name='CreateOrder' style='cursor:pointer' type='button' id='CreateOrder' class='contacts-btn-create-ordr' value='CREATE ORDER' onclick='window.parent.CreateOrder(" + sdr["ContactID"].ToString() + ", " + Session["theCID"].ToString() + ")'/>";
                            CreateDummyOrderLink = "<input name='CreateOrderDummy' style='cursor:pointer' type='button' id='CreateOrderDummy' class='contacts-btn-edit' value='CDO' onclick='window.parent.CreateOrderDummy(" + sdr["ContactID"].ToString() + ", " + Session["theCID"].ToString() + ")'/>";
                            htmlStr += "<tr><td><table align='center' cellpadding='0' cellspacing='0' class='width-980px-style'><tr><td class='contacts-001-style'>" + contactName + "</td><td class='contacts-002-style'>&nbsp;</td><td class='contacts-002-style'>&nbsp;</td><td class='contacts-003-style'>&nbsp;</td></tr><tr><td class='contacts-004-style'>" + contactEmail
                                + "</td>    </tr></table></td></tr>";
                        }
                    }
                    conn.Close();

                }

                return htmlStr;

            }
        }


        public String populateContactsHold()
        {
            String htmlStr = "";
            String contactName = "";
            String contactEmail = "";
            String editLink = "";
            String CreateOrderLink = "";
            string CreateDummyOrderLink = "";

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strContactInfo = "SELECT * FROM dbo.Contacts WHERE CompanyID = " + Session["theCID"].ToString() + " AND Active = 'Y'";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strContactInfo;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        contactName = sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString();
                        contactEmail = sdr["Email"].ToString() + " | " + sdr["DEFAULT_AreaCode"].ToString() + " " + sdr["DEFAULT_Number"].ToString() + " | " + sdr["STREET_AddressLine1"].ToString() + " " + sdr["STREET_AddressLine2"].ToString() + " " + sdr["STREET_City"].ToString() + " " + sdr["STREET_Region"].ToString() + " " + sdr["STREET_PostalCode"].ToString();
                        editLink = "<input name='EditContact' style='cursor:pointer' type='button' id='EditContact' class='contacts-btn-edit' value='EDIT' onclick='window.parent.EditContact(" + sdr["ContactID"].ToString() + ")'/>";
                        CreateOrderLink = "<input name='CreateOrder' style='cursor:pointer' type='button' id='CreateOrder' class='contacts-btn-create-ordr' value='CREATE ORDER' onclick='window.parent.CreateOrder(" + sdr["ContactID"].ToString() + ", " + Session["theCID"].ToString() + ")'/>";
                        CreateDummyOrderLink = "<input name='CreateOrderDummy' style='cursor:pointer' type='button' id='CreateOrderDummy' class='contacts-btn-edit' value='CDO' onclick='window.parent.CreateOrderDummy(" + sdr["ContactID"].ToString() + ", " + Session["theCID"].ToString() + ")'/>";
                        htmlStr += "<tr><td><table align='center' cellpadding='0' cellspacing='0' class='width-980px-style'><tr><td class='contacts-001-style'>" + contactName + "</td><td class='contacts-002-style'>&nbsp;</td><td class='contacts-002-style'>&nbsp;</td><td class='contacts-003-style'>&nbsp;</td></tr><tr><td class='contacts-004-style'>" + contactEmail
                            + "</td>    </tr></table></td></tr>";
                    }
                }
                conn.Close();
            }

            return htmlStr;
        }
    }
}