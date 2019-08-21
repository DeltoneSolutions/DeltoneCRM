using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Reports
{
    public partial class NumberOfAccountsGraphDisplay : System.Web.UI.Page
    {
        
             protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PopulateDropDownList();
            GetSalesCommissionByRepId("", "", "", "2");
        }


        protected void PopulateDropDownList()
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins WHERE Active = 'Y' and LoginID NOT IN ( 25,23,26,8,21,22,7)", conn);
                adapter.Fill(subjects);
                RepNameDropDownList.Items.Add(new ListItem("All", "0"));
                RepNameDropDownList.AppendDataBoundItems = true;
                RepNameDropDownList.DataSource = subjects;
                RepNameDropDownList.DataTextField = "FullName";
                RepNameDropDownList.DataValueField = "LoginID";
                RepNameDropDownList.DataBind();

            }

            //Get the Current Account OwnerShip


        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("~/dashboard1.aspx");


        }

        protected void btnbackupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("Reports.aspx");


        }

        public string getDailyCommission(string repID, string startDate, string endDate, RepGraph repData, bool canCall = false)
        {
            string RepCommission = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT Count(CompanyID) AS CompTotal,convert(varchar(10),Companies.CreatedDateTime, 120) as dateCreated FROM dbo.Companies WHERE OwnershipAdminID = " + repID
                  + " AND CreatedDateTime between  "
                          + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                 + " AND EXISTS (SELECT 1 FROM dbo.Orders WHERE Orders.CompanyID = Companies.CompanyID) group by convert(varchar(10),Companies.CreatedDateTime, 120) order by dateCreated";

                    

                    if (canCall)
                    {
//                        cmd.CommandText = @"SELECT Sum(VALUE) AS TOTALCOM ,  convert(varchar(10),Commision.CreateDateTime, 120) as dateCreated FROM dbo.Commision 
//                                          WHERE UserLoginID = " + repID + " AND  STATUS NOT IN ('CANCELLED')  AND convert(varchar(10),Commision.CreateDateTime, 120) =  "
//                         + "'" + startDate + "'" + " group by convert(varchar(10),Commision.CreateDateTime, 120) order by dateCreated ";


                        cmd.CommandText = "SELECT Count(CompanyID) AS CompTotal,convert(varchar(10),Companies.CreatedDateTime, 120) as dateCreated FROM dbo.Companies WHERE OwnershipAdminID = " + repID
                      + " AND convert(varchar(10),Companies.CreatedDateTime, 120) =  "
                              + "'" + startDate + "'"
                     + " AND EXISTS (SELECT 1 FROM dbo.Orders WHERE Orders.CompanyID = Companies.CompanyID) group by convert(varchar(10),Companies.CreatedDateTime, 120) order by dateCreated";

                    

                    }

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["CompTotal"] == DBNull.Value)
                                {
                                    RepCommission = "0";
                                }
                                else
                                {
                                    RepCommission = sdr["CompTotal"].ToString();
                                }

                                var repGraphData = new RepDisplayGraph();
                                repGraphData.Commission = RepCommission;
                                repGraphData.DateCreated = sdr["dateCreated"].ToString();
                                repData.GraphValue.Add(repGraphData);

                            }
                        }

                    }
                    conn.Close();

                }

            }
            return RepCommission;

        }


        private void GetSalesCommissionByRepId(string userId, string startDate, string endDate, string type)
        {
            var startDateSet = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
            var endDateSet = DateTime.Now.ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(startDate))
            {
                var conStartDate = Convert.ToDateTime(startDate);
                startDate = conStartDate.ToString("yyyy-MM-dd");
            }

            var dicGraphData = new Dictionary<string, string>();
            var repNameDrop = RepNameDropDownList.SelectedValue;
            if (type == "1")
            {
                startDateSet = DateTime.Now.ToString("yyyy-MM-dd");
            }

            else if (type == "2")
            {
                startDateSet = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + "01";
            }

            else
                if (type == "3")
                {
                    startDateSet = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"); ;
                }
                else if (type == "4")
                {

                    var startdatea = startdate.Value;
                    var enddatea = enddate.Value;
                    startDateSet = Convert.ToDateTime( startdatea).ToString("yyyy-MM-dd");
                    endDateSet = Convert.ToDateTime(enddatea).ToString("yyyy-MM-dd");
                }

            var repLsit = GetReps();
            if (repNameDrop != "0")
                repLsit = (from re in repLsit where re.RepId == repNameDrop select re).ToList();
            var listRepGraphData = new List<RepGraph>();
            foreach (var item in repLsit)
            {
                var repObjGraph = new RepGraph();
                repObjGraph.RepName = item.RepName;
                repObjGraph.GraphValue = new List<RepDisplayGraph>();
                if (type == "1")
                    getDailyCommission(item.RepId, startDateSet, endDateSet, repObjGraph,true);
                else
                    getDailyCommission(item.RepId, startDateSet, endDateSet, repObjGraph);
                listRepGraphData.Add(repObjGraph);

            }



            var listElements = new Dictionary<string, int>();

            var repNames = "";

            foreach (var item in listRepGraphData)
            {
                var count = item.GraphValue.Count();
                listElements.Add(item.RepName, count);
                repNames = repNames + item.RepName + ",";
            }
            repNameSales.Value = repNames;
            var listDataProvider = new Dictionary<string, Dictionary<string, string>>();

            var resultList = listElements.OrderByDescending(x => x.Value).ToList();

            foreach (var item in resultList)
            {
                var repame = item.Key;

                var listGraph = (from rep in listRepGraphData where rep.RepName == repame select rep).ToList();
                foreach (var tie in listGraph)
                {
                    Dictionary<string, string> valueRep;
                    var graphData = tie.GraphValue;
                    foreach (var valeuByDate in graphData)
                    {
                        if (listDataProvider.TryGetValue(valeuByDate.DateCreated, out valueRep))
                        {
                            valueRep.Add(repame, valeuByDate.Commission);

                        }
                        else
                        {
                            valueRep = new Dictionary<string, string>();
                            valueRep.Add(repame, valeuByDate.Commission);
                            listDataProvider.Add(valeuByDate.DateCreated, valueRep);
                        }



                    }
                }

            }


            // var getMaxRepData=(from res in listRepGraphData where res.GraphValue.cou)

            CreateSalesCommissionDataprovider(listDataProvider);

        }

        protected void callDailySales(object sender, EventArgs e)
        {
            GetSalesCommissionByRepId("", "", "", "1");
        }

        protected void callmonthlySales(object sender, EventArgs e)
        {
            GetSalesCommissionByRepId("", "", "", "2");
        }

        protected void callyearlySales(object sender, EventArgs e)
        {
            GetSalesCommissionByRepId("", "", "", "3");

        }

        protected void callFilerSales(object sender, EventArgs e)
        {
            var startdatea = startdate.Value;
            var enddatea = enddate.Value;
            GetSalesCommissionByRepId("", "", "", "4");

        }


        private void CreateSalesCommissionDataprovider(Dictionary<string, Dictionary<string, string>> salesCommission)
        {

            var listDataProvider = new List<string>();
            var counttotal = 0;
            var totalEle = salesCommission.Count();
            // alternate syntax
            var str = "[ ";
            foreach (var item in salesCommission)
            {
                counttotal = counttotal + 1;
                // dynamic salesCommissionObj = new ExpandoObject();
                // salesCommissionObj.date = item.Key;
                //  ChartDataPro dateObj = new ChartDataPro();
                //    c.Properties["date"] = item.Key;

                //  var x = new ExpandoObject() as IDictionary<string, Object>;
                //  x.Add("NewProp", string.Empty);
                //  listDataProvider.Add(dateObj);
                string strdate = "\"date\"";
                str = str + "{" + strdate + ": \"" + item.Key + "\",";
                var count = 0;
                var length = item.Value.Count();
                foreach (var commItem in item.Value)
                {
                    var commision = commItem.Value;
                    count = count + 1;
                    var repName = commItem.Key;

                    if (count == length)
                    {
                        str = str + "\"" + commItem.Key + "\": " + commItem.Value;
                    }
                    else
                        str = str + "\"" + commItem.Key + "\": " + commItem.Value + ",";
                }
                if (counttotal == totalEle)
                    str = str + "}";
                else
                    str = str + "},";

                //  listDataProvider.Add(salesCommissionObj);
            }


            str = str + " ]";
            graphDataSales.Value = str;
        }


        public class ChartDataPro
        {
            public Hashtable Properties = new Hashtable();
            public string Commision { get; set; }
        }

        public class RepDetail
        {
            public string RepName { get; set; }
            public string RepId { get; set; }
        }


        private IList<RepDetail> GetReps()
        {


            List<RepDetail> RepsName = new List<RepDetail>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT LoginID, FirstName, LastName,Department,Commission FROM dbo.Logins WHERE LoginID NOT 
                                           IN (1,4, 9,18, 8,7, 19,20,21,22,14) AND Active = 'Y'  ";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var comme = 0;
                                if (sdr["Commission"] != DBNull.Value)
                                {
                                    comme = Convert.ToInt32(sdr["Commission"].ToString());
                                    if (comme > 0)
                                    {
                                        // RepsName.Add(sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString());
                                        var obj = new RepDetail();
                                        obj.RepId = sdr["LoginID"].ToString();
                                        obj.RepName = sdr["FirstName"].ToString();
                                        //  obj_contact.LastName = sdr["LastName"].ToString();
                                        RepsName.Add(obj);
                                        //if (sdr["Department"] != DBNull.Value)
                                        //    obj_contact.DepartmentId = sdr["Department"].ToString();
                                        //di_getStat.Add(sdr["LoginID"].ToString(), obj_contact);
                                    }
                                }


                            }
                        }

                    }
                    conn.Close();

                }

            }

            return RepsName;
        }

        public class RepGraph
        {
            public string RepName { get; set; }
            public IList<RepDisplayGraph> GraphValue { get; set; }
        }

        public class RepDisplayGraph
        {
            public string DateCreated { get; set; }
            public string Commission { get; set; }
        }
    }
        
    
}