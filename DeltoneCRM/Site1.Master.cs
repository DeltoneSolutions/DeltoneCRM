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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string userName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUser"] != null)
            {



                if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                {
                    userName = Session["LoggedUser"].ToString();
                    if (Session["USERPROFILE"].ToString().Equals("ADMIN"))
                    {
                        lbWelComeMsg.Text = "Welcome " + Session["LoggedUser"] + ".";
                        searchtd.Visible = true;
                    }

                    if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
                    {
                        lbWelComeMsg.Text = "Welcome " + Session["LoggedUser"] + ". ";
                        ManageButton.Visible = false;
                        ManageLink.Visible = false;
                        ReportsLink.Visible = false;
                        ReportsButton.Visible = false;
                        noorderForRep.Visible = true;
                        reportsRep.Visible = true;
                        quoteallocateRep.Visible = true;
                    }

                   
                }
            }
            if (Session["LoggedUser"] == null)
            {
                Response.Redirect("http://delcrm");
            }

          var toatlNumber= getTotalCustomerCs();
          numberNotCompleted.InnerText = toatlNumber;

        }


        //Get and Set Property for Master Page WelCome Message
        public string WelComeMessage
        {
            get
            {
                return lbWelComeMsg.Text;
            }
            set
            {
                lbWelComeMsg.Text = value;
            }
        }
        //End Get and Set Property for Master Page WelCome Message


        protected void lnkLogout_Click(object sender, EventArgs e)
        {

        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session["USERPROFILE"] = null;
            Session["LoggedUser"] = null;
            Response.Redirect("http://delcrm");
        }


        public string getTotalCustomerCs()
        {
            string totalNotCompletedCs = String.Empty;

            var userID = Convert.ToInt32(Session["LoggedUserID"].ToString());

            var state = "Where rs.status <>'Y'";
            var strSqlContactStmt = "";

            if (Session["USERPROFILE"].ToString() == "ADMIN")
            {
                strSqlContactStmt = @"SELECT COUNT(rs.Id) as  Totalcs  from RaiseCSSalesRep rs " + state;


            }
            else
            {
                state = " and rs.status <>'Y'";

                strSqlContactStmt = @"SELECT COUNT(rs.Id) as  Totalcs from 
                       RaiseCSSalesRep rs inner join Companies  c on rs.CompanyId=c.CompanyID 
                 where (rs.CreatedUserId=@userId or  c.OwnershipAdminID =@userId ) " + state;

            }



            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = userID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                totalNotCompletedCs = sdr["Totalcs"].ToString();
                            }
                        }
                        else
                        {
                            totalNotCompletedCs = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return totalNotCompletedCs;
        }

    }
}