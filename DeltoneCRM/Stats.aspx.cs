using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM
{
    public partial class Stats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["stDate"] == null)
            {
                var dateNow = DateTime.Now;
                var st = dateNow.ToString("dd-MM-yyyy");
                var days = DateTime.DaysInMonth(dateNow.Year, dateNow.Month);
                var endate = dateNow;
                var en = endate.ToString("dd-MM-yyyy");
                StartDateSTxt.Value = st;
                EndDateSTxt.Value = en;
                Session["stDate"] = st;
                Session["enDate"] = en;
                checkClick.Value = "y";
                Session["nofiler"] = "y";
            }
            var nofiel = Request.QueryString["res"];
            if (!string.IsNullOrEmpty(nofiel))
            {
                var dateNow = DateTime.Now;
                var st = dateNow.ToString("dd-MM-yyyy");
                var days = DateTime.DaysInMonth(dateNow.Year, dateNow.Month);
                var endate = dateNow;
                var en = endate.ToString("dd-MM-yyyy");
                StartDateSTxt.Value = st;
                EndDateSTxt.Value = en;
                Session["stDate"] = st;
                Session["enDate"] = en;
                checkClick.Value = "y";
                Session["nofiler"] = "y";
            }

            var cc = Request.QueryString["sDates"];
            var ccs = Request.QueryString["eDates"];
            if (!string.IsNullOrEmpty(cc))
            {
                var datS = cc;
                var datE = ccs;
                Session["nofiler"] = "n";
                Session["stDate"] = datS;
                Session["enDate"] = datE;
            }
            if (Session["stDate"] != null)
                StartDateSTxt.Value = Session["stDate"].ToString();
            if (Session["enDate"] != null)
                EndDateSTxt.Value = Session["enDate"].ToString();

        }
    }
}