using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeltoneCRM_DAL;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace DeltoneCRM.Manage.ViewEditScreens
{
    public partial class ViewEditItem : System.Web.UI.Page
    {
        String ItemID = String.Empty;
        String strConnString;
        protected void Page_Load(object sender, EventArgs e)
        {
            strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            if (!String.IsNullOrEmpty(Request.QueryString["ItemID"]))
            {
                ItemID = Request.QueryString["ItemID"].ToString();
                Populate(ItemID);
                populateDropDown(hdnSupplier.Value);
            }
        }

        protected void Populate(String strItemID)
        {

            ItemDAL itemdal = new ItemDAL(strConnString);
            String strItem = itemdal.getItem(Int32.Parse(strItemID));
            String[] arrItem = strItem.Split(':');

            NewItemCode.Value = arrItem[0].ToString();
            NewDescription.Value = arrItem[1].ToString();
            NewCOG.Value = arrItem[2].ToString();
            NewResellPrice.Value = arrItem[4].ToString();
            hdnSupplier.Value = arrItem[5].ToString();
            hdnEditItemID.Value = arrItem[6].ToString();
            txtSupplier.Value = arrItem[7].ToString();
            QuantityText.Value = arrItem[11].ToString();
            DSBPrice.Value = arrItem[12].ToString();
            if (arrItem[8].ToString().Trim().Equals("N"))
            {
                activechkbox.Checked = true;
            }
            else
            {
                activechkbox.Checked = false;
                string strScript = "<script language='javascript'>$(document).ready(function (){$('#activechkbox').toggleCheckedState(true);});</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }

            if (arrItem[9].ToString().Trim().Equals("Y"))
            {
                chk_bestprice.Checked = true;
            }
            else
            {
                chk_bestprice.Checked = false;
            }

            if (arrItem[10].ToString().Trim().Equals("Y"))
            {
                chk_faulty.Checked = true;
            }
            else
            {
                chk_faulty.Checked = false;
            }

        }



        protected void populateDropDown(String SupplierID)
        {
            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT SupplierName, SupplierID FROM dbo.Suppliers", conn);
                adapter.Fill(subjects);
                DropDownList1.DataSource = subjects;
                DropDownList1.DataValueField = "SupplierID";
                DropDownList1.DataTextField = "SupplierName";
                DropDownList1.DataBind();
            }

             //Select the value in the dropdown list
             DropDownList1.Items.FindByValue(SupplierID.ToString()).Selected = true;
            //DropDownList1.Items.FindByText(firstname).Selected = true;


           

        }


    }
}