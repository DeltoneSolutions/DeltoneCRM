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
using DeltoneCRM_DAL;

namespace DeltoneCRM.Reports.Queries
{
    public partial class getNoSalesData : System.Web.UI.Page
    {   

        //Object List of Report objects
        static List<Company_Report> Obj_List = new List<Company_Report>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //string RepID = "1";
            //string DateID = "1";

            String strOutput = String.Empty;
            String RepID = Request.QueryString["REPID"].ToString();
            String DateID = Request.QueryString["DATEID"].ToString();
            List<String> AllCompanies = BuildingListOfCompanies(RepID, DateID);

            foreach (var comp in AllCompanies)
            {
                strOutput = strOutput + StringBuilder(comp) + "~"; ;
            }

            int Length = strOutput.Length;
            strOutput = strOutput.Substring(0, (Length - 1));
            Response.Write(strOutput);

            //Construct the Object List 
            //getData(RepID, DateID);
            //Populate the Object List
            //FetchData(Obj_List);
        }

        /// <summary>
        /// Builds a list of companies that belongs to either the account owner or entire list based on the date range selected 
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public List<String> BuildingListOfCompanies(String RepID, String DateRange)
        {
            List<String> CompanyIDList = new List<String>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepID == "ALL")
                    {
                        //cmd.CommandText = "SELECT DISTINCT CP.CompanyID FROM dbo.Orders OD INNER JOIN dbo.Companies CP ON OD.CompanyID = CP.CompanyID WHERE OrderedDateTime <= DATEADD(mm, -" + DateRange.ToString() +", GETDATE())";
                        cmd.CommandText = "SELECT CompanyID FROM dbo.Companies WHERE CompanyID IN (SELECT CompanyID FROM (SELECT CompanyID, OrderedDateTime, Rank() over (partition by CompanyID order by OrderID DESC) RankOrder FROM dbo.Orders OD1) TBL WHERE RankOrder = 1 AND OrderedDateTime <= DATEADD(mm,-" + DateRange + ",getDate()))";
                    }
                    else
                    {
                        //cmd.CommandText = "SELECT DISTINCT CP.CompanyID FROM dbo.Orders OD INNER JOIN dbo.Companies CP ON OD.CompanyID = CP.CompanyID WHERE OrderedDateTime <= DATEADD(mm, -" + DateRange.ToString() + ", GETDATE()) AND CP.OwnershipAdminID = " + RepID;
                        cmd.CommandText = "SELECT CompanyID FROM dbo.Companies WHERE CompanyID IN (SELECT CompanyID FROM (SELECT CompanyID, OrderedDateTime, Rank() over (partition by CompanyID order by OrderID DESC) RankOrder FROM dbo.Orders OD1) TBL WHERE RankOrder = 1 AND OrderedDateTime <= DATEADD(mm,-" + DateRange + ",getDate()) AND OwnershipAdminID = " + RepID + ")";
                    }
                    cmd.Connection = conn;
                    conn.Open();

                    

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                CompanyIDList.Add(sdr["CompanyID"].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
            }

            return CompanyIDList;
        }

        public String StringBuilder(String CompanyID)
        {
            String strOutput = String.Empty;

            CompanyDAL cpdal = new CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
            ContactDAL ctdal = new ContactDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
            OrderDAL ordal = new OrderDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            String CPName = cpdal.getCompanyNameByID(CompanyID);
            String CTName = ctdal.getContactByCompanyID(Int32.Parse(CompanyID));
            String CTNumber = ctdal.getContactPhoneBasedOnCompanyID(CompanyID);
            String LastOrder = ordal.getLastOrderDateBasedOnCompanyID(CompanyID);
            String LastSalesperson = ordal.getLastSalespersonBasedOnCompanyID(CompanyID);

            strOutput = CPName + "|" + CTName + "|" + CTNumber + "|" + LastSalesperson + "|" + LastOrder.Substring(0,10);

            return strOutput;
        }

        /// <summary>
        /// Check wether company already exsists 
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public bool CompanyExsists(String companyName)
        {
            bool flag = false;

            //Iterate through Object list and  find if company name  Exsists
            foreach(Company_Report obj in Obj_List)
            {
                if(obj.CompanyName.ToLower().Equals(companyName.ToLower()))
                {  
                    return true;
                }
            }

            return flag;
        }


        /// <summary>
        /// Construct Output based on Object List
        /// </summary>
        /// <param name="Obj_List"></param>
        public void FetchData(List<Company_Report> Obj_List)
        {
            string strOutput = "";


            if (Obj_List.Count > 0)
            {
                foreach(Company_Report obj in Obj_List)
                {

                    String strCompanyName = obj.CompanyName.ToString();
                    String strContact = obj.ContactName.ToString();
                    String Telephone = obj.TelePhoneNumber.ToString();

                    DateTime dt = Convert.ToDateTime(obj.OrderedDateTime.ToString());
                    

                    String OrderedDateTime = dt.ToString("dd/MM/yyyy");
                    String OrderBy = obj.OrderedBy.ToString();
                    strOutput = strOutput + strCompanyName + "|" + strContact + "|" + Telephone + "|" + OrderedDateTime + "|" + OrderBy + "~";

                }
                
            }
            int Length = strOutput.Length;
            strOutput = strOutput.Substring(0, (Length - 1));
            Response.Write(strOutput);
        }
            

        /// <summary>
        /// ResultSet of Orders giveny by RepID,DateRangeID
        /// </summary>
        /// <param name="RepID"></param>
        /// <param name="DateRangeID"></param>
        public void getData(string RepID, string DateRangeID)
        {
            //string strOutput = "";
            Company_Report obj_report;
            
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepID == "ALL")
                    {
                        cmd.CommandText = "SELECT DISTINCT Companies.CompanyName, Contacts.ContactName, Contacts.FirstName + ' ' + Contacts.LastName As ContactName2, Contacts.DEFAULT_CountryCode + ' ' + Contacts.DEFAULT_AreaCode + ' ' + Contacts.DEFAULT_Number As TelephoneNumber , Orders.OrderedDateTime, Orders.OrderedBy, Contacts.PrimaryContact FROM dbo.Contacts INNER JOIN dbo.Companies ON Contacts.CompanyID = Companies.CompanyID INNER JOIN dbo.Orders ON Orders.CompanyID = Companies.CompanyID AND Orders.ContactID = Contacts.ContactID WHERE Orders.OrderedDateTime <= DATEADD(mm, -" + DateRangeID.ToString() + ", GETDATE()) AND Contacts.PrimaryContact = 'Y'";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT DISTINCT Companies.CompanyName, Contacts.ContactName, Contacts.FirstName + ' ' + Contacts.LastName As ContactName2, Contacts.DEFAULT_CountryCode + ' ' + Contacts.DEFAULT_AreaCode + ' ' + Contacts.DEFAULT_Number As TelephoneNumber , Orders.OrderedDateTime, Orders.OrderedBy, Contacts.PrimaryContact FROM dbo.Contacts INNER JOIN dbo.Companies ON Contacts.CompanyID = Companies.CompanyID INNER JOIN dbo.Orders ON Orders.CompanyID = Companies.CompanyID AND Orders.ContactID = Contacts.ContactID WHERE Orders.OrderedDateTime <= DATEADD(mm, -" + DateRangeID.ToString() + ", GETDATE()) AND Contacts.PrimaryContact = 'Y' AND Companies.OwnershipAdminID = " + RepID;
                    }
                    
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        obj_report = new Company_Report();
                        while (sdr.Read())
                        {
                            if (Obj_List.Count == 0)
                            {
                                //Populate the Object and add it to the List
                                obj_report.CompanyName = sdr["CompanyName"].ToString();
                                obj_report.ContactName = sdr["ContactName2"].ToString();
                                obj_report.TelePhoneNumber = sdr["TelephoneNumber"].ToString();
                                obj_report.OrderedDateTime = sdr["OrderedDateTime"].ToString();
                                obj_report.OrderedBy = sdr["OrderedBy"].ToString();
                                obj_report.PrimaryContact = sdr["PrimaryContact"].ToString();
                                Obj_List.Add(obj_report);

                            }
                            else
                            {
                                if(CompanyExsists(sdr["CompanyName"].ToString()))
                                {
                                    
                                    var obj_record = (from o in Obj_List
                                                     where o.CompanyName.ToLower() == sdr["CompanyName"].ToString().ToLower()
                                                     select o).First();

                                    int obj_Index = Obj_List.FindIndex(a=>a.CompanyName.ToString().ToLower().Equals(sdr["CompanyName"].ToString().ToLower()));

                                    //Fetch the object OrderedDateTime of the Object
                                    DateTime obj_DateTimeOrdered = Convert.ToDateTime(obj_record.OrderedDateTime);

                                    DateTime sql_DateTimeOrdered = Convert.ToDateTime(sdr["OrderedDateTime"].ToString());

                                    //Compare two Dates 

                                    int result = DateTime.Compare(sql_DateTimeOrdered, obj_DateTimeOrdered);

                                    if(result>0)
                                    {
                                        //Remove it the Previous Object from the List
                                        Obj_List.RemoveAt(obj_Index);
                                        //Update the Object and Add it to the List
                                        obj_report = new Company_Report();
                                        obj_report.CompanyName = sdr["CompanyName"].ToString();
                                        obj_report.ContactName = sdr["ContactName2"].ToString();
                                        obj_report.TelePhoneNumber = sdr["TelephoneNumber"].ToString();
                                        obj_report.OrderedDateTime = sdr["OrderedDateTime"].ToString();
                                        obj_report.OrderedBy = sdr["OrderedBy"].ToString();
                                        obj_report.PrimaryContact = sdr["PrimaryContact"].ToString();
                                        Obj_List.Add(obj_report);
                                    }  
                                }
                                else
                                {
                                    //If Company not Exsists then add the Company to the ObjectList
                                    obj_report = new Company_Report();
                                    obj_report.CompanyName = sdr["CompanyName"].ToString();
                                    obj_report.ContactName = sdr["ContactName2"].ToString();
                                    obj_report.TelePhoneNumber = sdr["TelephoneNumber"].ToString();
                                    obj_report.OrderedDateTime = sdr["OrderedDateTime"].ToString();
                                    obj_report.OrderedBy = sdr["OrderedBy"].ToString();
                                    obj_report.PrimaryContact = sdr["PrimaryContact"].ToString();
                                    Obj_List.Add(obj_report);
                                }

                            }


                            //strOutput = strOutput + sdr["CompanyName"].ToString() + "|" + sdr["ContactName2"].ToString() + "|" + sdr["TelephoneNumber"].ToString() + "|" + sdr["OrderedDateTime"].ToString() + "|" + sdr["OrderedBy"].ToString() + "~";


                        }
                    }

                    //int Length = strOutput.Length;
                    //strOutput = strOutput.Substring(0, (Length - 1));
                }
                
            }

            //Response.Write(strOutput);
        }


        public void  Contains(String CompanyName,String OrderedDate)
        {
           
        }



        /// <summary>
        /// Populate the Report_Object List
        /// </summary>
        public void PopulateReportObject()
        {
            string strOutput = "";
            Company_Report obj_report;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT DISTINCT Companies.CompanyName, Contacts.ContactName, Contacts.FirstName + ' ' + Contacts.LastName As ContactName2, Contacts.DEFAULT_CountryCode + ' ' + Contacts.DEFAULT_AreaCode + ' ' + Contacts.DEFAULT_Number As TelephoneNumber , Orders.OrderedDateTime, Orders.OrderedBy, Contacts.PrimaryContact FROM dbo.Contacts INNER JOIN dbo.Companies ON Contacts.CompanyID = Companies.CompanyID INNER JOIN dbo.Orders ON Orders.CompanyID = Companies.CompanyID AND Orders.ContactID = Contacts.ContactID WHERE Orders.OrderedDateTime <= DATEADD(mm, -3, GETDATE()) AND Contacts.PrimaryContact = 'Y'";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        obj_report = new Company_Report();
                        while (sdr.Read())
                        {
                            //Populate the Object 
                            obj_report.CompanyName = sdr["CompanyName"].ToString();
                            obj_report.ContactName = sdr["ContactName2"].ToString();
                            obj_report.TelePhoneNumber = sdr["TelephoneNumber"].ToString();
                            obj_report.OrderedDateTime = sdr["OrderedDateTime"].ToString();
                            obj_report.OrderedBy = sdr["OrderedBy"].ToString();

                            //Add it to the Collection
                            Obj_List.Add(obj_report);
                        }
                    }

                }

            }
        }

}
}