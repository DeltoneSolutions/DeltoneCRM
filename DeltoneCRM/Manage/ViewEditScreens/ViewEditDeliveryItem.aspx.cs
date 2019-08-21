using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Data;

namespace DeltoneCRM.Manage.ViewEditScreens
{
    public partial class ViewEditDeliveryItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            returnAllItems();
        }

        protected string returnAllItems()
        {
            String strOutput = "";
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT deliveryid,DeliveryDetails, DeliveryCost, Active FROM dbo.DeliveryDetails WHERE DeliveryID = " + Request.QueryString["cid"];
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            NewItem.Value = sdr["DeliveryDetails"].ToString();
                            NewCost.Value = sdr["DeliveryCost"].ToString();
                            cusdelID.Value = sdr["deliveryid"].ToString();
                            if (sdr["Active"].ToString().Trim().Equals("N"))
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

                    conn.Close();

                }

            }
            return strOutput;
        }
    }
}