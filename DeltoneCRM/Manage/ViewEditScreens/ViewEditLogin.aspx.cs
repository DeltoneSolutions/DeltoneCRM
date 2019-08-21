using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using DeltoneCRM_DAL;


namespace DeltoneCRM.Manage.ViewEditScreens
{
    public partial class ViewEditLogin : System.Web.UI.Page
    {
        String connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        String LoginID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["loginid"]))
            {
                LoginID = Request.QueryString["loginid"].ToString();
                LoginDAL logingdal = new LoginDAL(connString);
                String user = logingdal.getLoginDetails(Int32.Parse(LoginID));
                String[] arr_user = user.Split(':');
                //Populate the Fields

                EditLoginID.Value = arr_user[0].ToString();
                FirstName.Value = arr_user[3].ToString();
                LastName.Value = arr_user[4].ToString();
                UserName.Value = arr_user[1].ToString();
                //password.Value = arr_user[2].ToString();
                //conPassWord.Value = arr_user[2].ToString();
                String strAccesslevel = arr_user[5].ToString();
                dropdownAccessLevel.Items.FindByValue(strAccesslevel).Selected = true;
                accessLevel.Value = (strAccesslevel.Equals("1")) ? "Administrator" : "Operator";
                EmailAddy.Value = arr_user[7].ToString();

                var department = Convert.ToInt32(arr_user[8].ToString());

                if (department == 1)
                {
                    UserDepartment.Value = "Managers";
                    UserDepartmentDropDownList.Items.FindByValue(department.ToString()).Selected = true;
                }
                else

                    if (department == 2)
                    {
                        UserDepartment.Value = "ReOrders";
                        UserDepartmentDropDownList.Items.FindByValue(department.ToString()).Selected = true;
                    }
                    else
                        if (department == 3)
                        {
                            UserDepartment.Value = "Coldies";
                            UserDepartmentDropDownList.Items.FindByValue(department.ToString()).Selected = true;
                        }
                        else
                        {
                            UserDepartmentDropDownList.Items.FindByValue("").Selected = true;
                        }

                var donotShowStatsVa = Convert.ToInt32(arr_user[9].ToString());

                if (donotShowStatsVa == 1)
                {
                    donotshowstats.Checked = true;
                }


                if (arr_user[6].ToString().Trim().Equals("N"))
                {
                    activechkbox.Checked = true;
                }
                else
                {
                    activechkbox.Checked = false;
                    string strScript = "<script language='javascript'>$(document).ready(function (){$('#activechkbox').toggleCheckedState(true);});</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
                }
            }
        }


    }
}