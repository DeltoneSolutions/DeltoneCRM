using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchFilterAllCompanies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String searchTerm = Request.QueryString["term"].ToString();
            String filterType = Request.QueryString["type"].ToString();
            //String searchTerm = "In";
            Response.Write(ReturnAllCompanies(searchTerm, filterType));

        }

        protected string ReturnAllCompanies(string strSearchTerm, string type)
        {
            String strOutput = "[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var displayString = "";
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {


                        //                        if (type == "2")
                        //                        {
                        //                            cmd.CommandText = @"SELECT * FROM dbo.Companies co FULL OUTER JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE 
                        //                                          co.CompanyName LIKE '%" + strSearchTerm + "%' "
                        //                                           + " OR cs.FirstName LIKE '%" + strSearchTerm + "%' "
                        //                                            + " OR cs.LastName LIKE '%" + strSearchTerm + "%' "
                        //                                             + " OR cs.STREET_AddressLine1 LIKE '%" + strSearchTerm + "%' "
                        //                                              + " OR cs.STREET_City LIKE '%" + strSearchTerm + "%' "
                        //                                               + " OR cs.STREET_PostalCode LIKE '%" + strSearchTerm + "%' "
                        //                                                + " OR cs.STREET_Region LIKE '%" + strSearchTerm + "%' "
                        //                                                 + " OR cs.Email LIKE '%" + strSearchTerm + "%' "
                        //                                           + " AND co.Active ='Y' ";
                        //                        }
                        if (type == "3")
                        {
                            cmd.CommandText = @"SELECT * FROM dbo.Companies co FULL OUTER JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "
                                             + "  cs.STREET_AddressLine1 LIKE '%" + strSearchTerm + "%' "
                                              + " OR cs.STREET_City LIKE '%" + strSearchTerm + "%' "

                                                + " OR cs.STREET_Region LIKE '%" + strSearchTerm + "%' "

                                           + " AND co.Active ='Y' ";
                        }
                        else
                            if (type == "4")
                            {
                                cmd.CommandText = @"SELECT * FROM dbo.Companies co FULL OUTER JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "

                                                   + "  cs.STREET_PostalCode LIKE '%" + strSearchTerm + "%' "

                                               + " AND co.Active ='Y' ";
                            }
                            else
                                if (type == "5")
                                {
                                    cmd.CommandText = @"SELECT * FROM dbo.Companies co FULL OUTER JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "

                                                      + "  cs.STREET_City LIKE '%" + strSearchTerm + "%' "


                                                   + " AND co.Active ='Y' ";
                                }
                                else if (type == "6")
                                {

                                    cmd.CommandText = @"SELECT * FROM dbo.Companies co FULL OUTER JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "


                                                        + " cs.STREET_Region LIKE '%" + strSearchTerm + "%' "

                                                   + " AND co.Active ='Y' ";
                                }

                                else if (type == "7")
                                {

                                    cmd.CommandText = @"SELECT * FROM dbo.Companies co FULL OUTER JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "


                                                        + " cs.DEFAULT_Number LIKE '%" + strSearchTerm + "%' "

                                                   + " AND co.Active ='Y' ";
                                }

                                else if (type == "8")
                                {

                                    cmd.CommandText = @"SELECT * FROM dbo.Companies co FULL OUTER JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "


                                                        + " cs.Email LIKE '%" + strSearchTerm + "%' "

                                                   + " AND co.Active ='Y' ";
                                }

                    }
                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                    {

                        if (type == "3")
                        {
                            cmd.CommandText = @"SELECT * FROM dbo.Companies co INNER  JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "
                                             + "  cs.STREET_AddressLine1 LIKE '%" + strSearchTerm + "%' "


                                           + " AND co.Active ='Y' AND co.OwnershipAdminID IN (" + Session["LoggedUserID"] + ")";


                        }
                        if (type == "4")
                        {
                            cmd.CommandText = @"SELECT * FROM dbo.Companies co INNER  JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "

                                               + "  cs.STREET_PostalCode LIKE '%" + strSearchTerm + "%' "

                                           + " AND co.Active ='Y' AND co.OwnershipAdminID IN (" + Session["LoggedUserID"] + ")";
                        }
                        if (type == "5")
                        {
                            cmd.CommandText = @"SELECT * FROM dbo.Companies co INNER  JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "

                                               + "  cs.STREET_City LIKE '%" + strSearchTerm + "%' "

                                           + " AND co.Active ='Y' AND co.OwnershipAdminID IN (" + Session["LoggedUserID"] + ")";
                        }

                        if (type == "6")
                        {
                            cmd.CommandText = @"SELECT * FROM dbo.Companies co INNER  JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "

                                               + "  cs.STREET_Region LIKE '%" + strSearchTerm + "%' "

                                           + " AND co.Active ='Y' AND co.OwnershipAdminID IN (" + Session["LoggedUserID"] + ")";
                        }
                        if (type == "7")
                        {
                            cmd.CommandText = @"SELECT * FROM dbo.Companies co INNER  JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "

                                               + "  cs.DEFAULT_Number LIKE '%" + strSearchTerm + "%' "

                                           + " AND co.Active ='Y' AND co.OwnershipAdminID IN (" + Session["LoggedUserID"] + ")";
                        }
                        if (type == "8")
                        {
                            cmd.CommandText = @"SELECT * FROM dbo.Companies co INNER  JOIN Contacts cs ON co.CompanyID=cs.CompanyID  WHERE "

                                               + "  cs.Email LIKE '%" + strSearchTerm + "%' "

                                           + " AND co.Active ='Y' AND co.OwnershipAdminID IN (" + Session["LoggedUserID"] + ")";
                        }
                    }

                    cmd.Connection = conn;
                    conn.Open();


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            displayString = sdr["CompanyName"].ToString();
                            if (type == "3")
                            {
                                displayString = displayString + " : " + sdr["STREET_AddressLine1"].ToString();
                            }

                            else if (type == "4")
                            {
                                displayString = displayString + " : " + sdr["STREET_PostalCode"].ToString();
                            }
                            else if (type == "5")
                            {
                                displayString = displayString + " : " + sdr["STREET_City"].ToString();
                            }
                            else if (type == "6")
                            {
                                displayString = displayString + " : " + sdr["STREET_Region"].ToString();
                            }
                            else if (type == "7")
                            {
                                displayString = displayString + " : " + sdr["DEFAULT_Number"].ToString();
                            }
                            else if (type == "8")
                            {
                                displayString = displayString + " : " + sdr["Email"].ToString();
                            }
                            strOutput = strOutput + "{\"id\": \"" + sdr["CompanyID"] + "\", \"value\": \"" + displayString + "\"},";
                        }
                    }

                    int Length = strOutput.Length;
                    strOutput = strOutput.Substring(0, (Length - 1));
                    conn.Close();
                }
                strOutput = strOutput + "]";
            }

            return strOutput;
        }



    }
}