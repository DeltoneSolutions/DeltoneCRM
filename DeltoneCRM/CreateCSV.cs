using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Data;
using System.IO;
using DeltoneCRM_DAL;

namespace DeltoneCRM
{
    public class CreateCSV
    {
        public void BuildCSV(String OrderID)
        {
            OrderDAL orderdal = new OrderDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            String AusjetNotes = String.Empty;

            using (SqlConnection conn2 = new SqlConnection())
            {
                conn2.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Notes FROM dbo.SupplierNotes WHERE OrderID = " + OrderID + " AND Suppliername = 'Ausjet'";
                    cmd.Connection = conn2;
                    conn2.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                AusjetNotes = sdr["Notes"].ToString();
                            }
                        }
                    }
                }
                conn2.Close();
            }

            using (SqlConnection conn = new SqlConnection())
            {

                String XeroOrderID = orderdal.getXeroDTSID(Int32.Parse(OrderID));
                String StringBuilder = String.Empty;

                StreamWriter SW;
                SW = File.CreateText("C:\\inetpub\\wwwroot\\DeltoneCRM\\Invoices\\Supplier\\SUPPLIERCSV\\AUSJET - Order " + XeroOrderID + ".csv");
                SW.Write("record id,cust first,cust last,company,address1,address2,suburb,state, postcode,sku,qty,unitprice,notes\r\n");

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.ORDERS WHERE OrderID = " + OrderID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                
                                String OrderedItems = getAusjetItemsForOrder(sdr["OrderID"].ToString());
                                if (OrderedItems != "NONE")
                                {
                                    
                                    string[] ArrayOfOrderedItems = OrderedItems.Split('~');
                                    foreach (string item in ArrayOfOrderedItems)
                                    {
                                        StringBuilder = String.Empty;
                                        StringBuilder = sdr["XeroInvoiceNumber"].ToString() + ",";
                                        String ContactFirstName = getContactFirstName(sdr["ContactID"].ToString());
                                        StringBuilder = StringBuilder + ContactFirstName + ",";
                                        String ContactLastName = getContactLastName(sdr["ContactID"].ToString());
                                        StringBuilder = StringBuilder + ContactLastName + ",";
                                        String CompanyName = getCompanyName(sdr["CompanyID"].ToString());
                                        StringBuilder = StringBuilder + CompanyName + ",";
                                        String FullAddress = getContactAddress(sdr["ContactID"].ToString());
                                        string[] SplitAddress = FullAddress.Split('|');
                                        foreach (string bit in SplitAddress)
                                        {
                                            StringBuilder = StringBuilder + bit + ",";
                                        }
                                        string[] ItemDetails = item.Split('|');
                                        foreach (string details in ItemDetails)
                                        {
                                            StringBuilder = StringBuilder + details + ",";
                                            
                                        }
                                        StringBuilder = StringBuilder + AusjetNotes + ",";
                                        int Length = StringBuilder.Length;
                                        StringBuilder = StringBuilder.Substring(0, (Length - 1));
                                        StringBuilder = StringBuilder + "\r\n";
                                        SW.Write(StringBuilder);
                                    }
                                    
                                }

                                
                            }

                        }
                    }
                    conn.Close();

                }
                SW.Close();

            }
        }


        private String getAusjetItemsForOrder(String OrderID)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                String OrderList = String.Empty;
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SupplierCode, Quantity, COGAmount FROM dbo.Ordered_Items WHERE OrderID = " + OrderID + " AND SupplierName = 'Ausjet'" ;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                OrderList = OrderList + sdr["SupplierCode"].ToString() + "|";
                                OrderList = OrderList + sdr["Quantity"].ToString() + "|";
                                OrderList = OrderList + sdr["COGAmount"].ToString() + "~";

                            }
                            int Length = OrderList.Length;
                            OrderList = OrderList.Substring(0, (Length - 1));
                        }
                        else
                        {
                            OrderList = "NONE";
                        }
                    }
                    conn.Close();
                    
                }
                return OrderList;

            }
        }

        private String getContactFirstName(String ContactID)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                String ContactFirstName = String.Empty;
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT FirstName FROM dbo.Contacts WHERE ContactID = " + ContactID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                ContactFirstName = sdr["FirstName"].ToString();
                            }

                        }
                    }
                    conn.Close();

                }
                return ContactFirstName;
            }

            
        }

        private String getContactLastName(String ContactID)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                String ContactLastName = String.Empty;
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT LastName FROM dbo.Contacts WHERE ContactID = " + ContactID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                ContactLastName = sdr["LastName"].ToString();
                            }

                        }
                    }
                    conn.Close();

                }
                return ContactLastName;

            }
        }

        private String getCompanyName(String CompanyID)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                String CompanyName = String.Empty;
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CompanyName FROM dbo.Companies WHERE CompanyID = " + CompanyID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                CompanyName = sdr["CompanyName"].ToString();
                            }

                        }
                    }
                    conn.Close();

                }
                return CompanyName;

            }
        }

        private String getContactAddress(String CID)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                String ContactAddress = String.Empty;
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT STREET_AddressLine1, STREET_AddressLine2, STREET_City, STREET_Region, STREET_PostalCode FROM dbo.Contacts WHERE ContactID = " + CID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                ContactAddress = sdr["STREET_AddressLine1"].ToString() + "|";
                                ContactAddress = ContactAddress + sdr["STREET_AddressLine2"].ToString() + "|";
                                ContactAddress = ContactAddress + sdr["STREET_City"].ToString() + "|";
                                ContactAddress = ContactAddress + sdr["STREET_Region"].ToString() + "|";
                                ContactAddress = ContactAddress + sdr["STREET_PostalCode"].ToString();
                            }

                        }
                    }
                    conn.Close();

                }
                return ContactAddress;

            }
        }
    }
}