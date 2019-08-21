using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using DeltoneCRM_DAL;


namespace DeltoneCRM
{
    public partial class alarmlist : System.Web.UI.Page
    {

        CompanyDAL company = new CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            TableRow trow = new TableRow();
            var td = new TableCell();
            var lt = new Literal();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlStmt = "SELECT * FROM dbo.Alarms WHERE UserID = " + Session["LoggedUserID"].ToString() + " AND AlarmTriggered = 'N' AND StartDate <= getDate()";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            HtmlTableRow row = new HtmlTableRow();
                            HtmlTableCell cell = new HtmlTableCell();

                            cell.Attributes.Add("width", "30");
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);

                            String CompanyName = company.getCompanyNameByID(sdr["CompanyID"].ToString());

                            cell = new HtmlTableCell();
                            cell.ColSpan = 3;
                            cell.Attributes.Add("class", "alarm-heading-style");
                            cell.InnerText = "ALARM - " + CompanyName.ToUpper();
                            row.Cells.Add(cell);


                            cell = new HtmlTableCell();
                            cell.Attributes.Add("width", "20");
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);

                            tableContent.Rows.Add(row);

                            row = new HtmlTableRow();
                            cell = new HtmlTableCell();

                            cell.ColSpan = 5;
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);

                            tableContent.Rows.Add(row);

                            row = new HtmlTableRow();
                            cell = new HtmlTableCell();

                            cell.Attributes.Add("width", "30");
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();

                            cell.ColSpan = 3;
                            cell.Attributes.Add("align", "left");
                            cell.Attributes.Add("class", "alarm-description");
                            cell.InnerText = sdr["Description"].ToString();
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();

                            cell.Attributes.Add("width", "30");
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);

                            tableContent.Rows.Add(row);

                            row = new HtmlTableRow();
                            cell = new HtmlTableCell();

                            cell.ColSpan = 5;
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);

                            tableContent.Rows.Add(row);

                            row = new HtmlTableRow();
                            cell = new HtmlTableCell();

                            cell.Attributes.Add("width", "30");
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);
                            
                            cell = new HtmlTableCell();

                            cell.Attributes.Add("width", "200");
                            cell.Attributes.Add("class", "createdby");
                            cell.InnerText = "Created By: " + sdr["CreatedBy"].ToString();
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();

                            cell.Attributes.Add("width", "125");
                            cell.Attributes.Add("class", "alarm_buttons");
                            cell.Attributes.Add("onclick", "OpenCompany(" + sdr["CompanyID"].ToString() + "," + sdr["AlarmID"].ToString() + ")");
                            cell.InnerText = "OPEN COMPANY";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();

                            cell.Attributes.Add("width", "125");
                            cell.Attributes.Add("class", "alarm_buttons");
                            cell.Attributes.Add("onclick", "DismissAlarm(" + sdr["AlarmID"].ToString() + ")");
                            cell.InnerText = "DISMISS ALARM";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();

                            cell.Attributes.Add("width", "30");
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);

                            tableContent.Rows.Add(row);

                            row = new HtmlTableRow();
                            cell = new HtmlTableCell();

                            cell.ColSpan = 5;
                            cell.InnerHtml = "<hr/>";
                            row.Cells.Add(cell);

                            tableContent.Rows.Add(row);

                        }
                    }
                }
            }
            conn.Close();
        }

        
    }
}