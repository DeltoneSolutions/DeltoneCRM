using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;


namespace DeltoneCRM_DAL
{
    public class ItemDAL
    {
        String CONNSTRING;

        public ItemDAL(String strConnString)
        {
            CONNSTRING = strConnString;
        }

        public String getItem(int itemID)
        {
            String strItem = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select I.*,s.SupplierName from dbo.Items I,Suppliers s  where  I.SupplierID=s.SupplierID  and  ItemID=" + itemID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strItem = strItem + sdr["SupplierItemCode"].ToString() + ":" + sdr["Description"].ToString() + ":" + sdr["COG"].ToString() + ":" + sdr["OEMCode"].ToString() + ":" + sdr["ManagerUnitPrice"].ToString() + ":" + sdr["SupplierID"].ToString() + ":" + sdr["ItemID"].ToString() + ":" + sdr["SupplierName"].ToString()
                            + ":" + sdr["Active"].ToString() + ":" + sdr["PriceLock"].ToString() + ":"
                            + sdr["ReportedFaulty"] + ":" + sdr["Quantity"].ToString() + ":" + sdr["DSB"].ToString();

                    }
                }
            }
            conn.Close();
            return strItem;

        }

        public String UpdateItem(int ItemID, String Description, float COG, float ManagedPrice, int SupplierID,
            String SupplierItemCode, String ActInact, String BestPrice, String Faulty, int qty, double? dsb)
        {
            int RowsEffected = -1;
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = @"update Items set SupplierID=@SupplierID,SupplierItemCode=@SupplierItemCode,
                                   Description=@Description,COG=@cog,ManagerUnitPrice=@ManagedPrice,Active=@ActInact, 
                                 AlterationDate=CURRENT_TIMESTAMP, PriceLock=@PriceLock, 
                                  ReportedFaulty=@ReportedFaulty ,Quantity=@Quantity ,DSB=@dsb where ItemID=@ItemID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.Parameters.AddWithValue("@SupplierItemCode", SupplierItemCode);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@cog", COG);
            cmd.Parameters.AddWithValue("@ManagedPrice", ManagedPrice);
            cmd.Parameters.AddWithValue("@ItemID", ItemID);
            cmd.Parameters.AddWithValue("@ActInact", ActInact);
            cmd.Parameters.AddWithValue("@PriceLock", BestPrice);
            cmd.Parameters.AddWithValue("@ReportedFaulty", Faulty);
            cmd.Parameters.AddWithValue("@Quantity", qty);

            if (dsb == null)
                cmd.Parameters.AddWithValue("@dsb", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@dsb", dsb);

            try
            {
                conn.Open();
                output = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                output = ex.Message.ToString();
            }
            return output;

        }

        public String getSupplier(int SID)
        {

            String strSupplier = String.Empty;


            return strSupplier;
        }


        /*Find Supplier Name by SupplierID*/
        public String FetchSupplierName(int ItemCode)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select I.*,s.SupplierName from dbo.Items I,Suppliers s  where  I.SupplierID=s.SupplierID  and  ItemID=" + ItemCode;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = output;
                    }
                }
            }
            conn.Close();

            return output;
        }
        /*End Fetch SupplierName by SupplierID*/


        // Return's if Item has duplicate Supplier Code
        public String verifyItemSupplierCodeExists(String ItemCode)
        {
            String ItemExists = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT IT.SupplierItemCode, IT.Description, SP.SupplierName FROM dbo.Items IT INNER JOIN dbo.Suppliers SP ON SP.SupplierID = IT.SupplierID WHERE SupplierItemCode = '" + ItemCode + "'";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                ItemExists = "YES|" + sdr["SupplierItemCode"].ToString() + " - " + sdr["Description"].ToString() + " FROM " + sdr["SupplierName"].ToString();
                            }
                        }
                        else
                        {
                            ItemExists = "NO|NOTHING";
                        }
                    }
                }
                conn.Close();
            }

            return ItemExists;
        }

        public List<SupplierItem> GetItemsByItemId(string ItmeID)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;
            var list = new List<SupplierItem>();

            conn.ConnectionString = CONNSTRING;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT IT.ItemID, IT.COG, IT.Description, 
       IT.ManagerUnitPrice,IT.RepUnitPrice,IT.SupplierItemCode FROM dbo.Items IT Where  IT.ItemID=@itmeID";
                cmd.Parameters.AddWithValue("@itmeID", ItmeID);
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var obj = new SupplierItem();
                        obj.ItemId = sdr["ItemID"].ToString();
                        obj.PriceUpdate = sdr["COG"].ToString();
                        if (sdr["ManagerUnitPrice"] != DBNull.Value)
                            obj.ResellPrice = sdr["ManagerUnitPrice"].ToString();
                        else
                            obj.ResellPrice = sdr["RepUnitPrice"].ToString();
                        obj.Description = sdr["Description"].ToString();
                        obj.SupplierItemCode = sdr["SupplierItemCode"].ToString();
                        list.Add(obj);
                    }
                }

                conn.Close();
            }

            return list;
        }

        public List<SupplierItem> GetItemsBySupplierItemCode(string SupplierItemCode)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;
            var list = new List<SupplierItem>();

            conn.ConnectionString = CONNSTRING;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = " SELECT IT.ItemID, IT.COG, IT.Description, IT.ManagerUnitPrice,IT.RepUnitPrice,IT.SupplierItemCode,IT.SupplierID FROM dbo.Items IT Where  IT.SupplierItemCode= " + "'" + SupplierItemCode + "'";
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var obj = new SupplierItem();
                        obj.ItemId = sdr["ItemID"].ToString();
                        obj.SupplierName = sdr["SupplierID"].ToString();
                        obj.PriceUpdate = sdr["COG"].ToString();
                        if (sdr["ManagerUnitPrice"] != DBNull.Value)
                            obj.ResellPrice = sdr["ManagerUnitPrice"].ToString();
                        else
                            obj.ResellPrice = sdr["RepUnitPrice"].ToString();
                        obj.Description = sdr["Description"].ToString();
                        obj.SupplierItemCode = sdr["SupplierItemCode"].ToString();
                        list.Add(obj);
                    }
                }

                conn.Close();
            }

            return list;
        }


        public void UpdateItemPrinterCompatibility(List<SupplierItem> list, int supplierId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                SqlConnection sl = new SqlConnection();
                sl.ConnectionString = CONNSTRING;
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sl;
                sqlComm.CommandText = "UPDATE Items SET PrinterCompatibility=@printerCompatibility  Where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId ";
                sqlComm.Parameters.Add("@suppId", supplierId);
                sqlComm.Parameters.Add("@printerCompatibility", item.PrinterCompatibility);
                sqlComm.Parameters.Add("@SupplierItemCode", item.SupplierItemCode);
                try
                {
                    sl.Open();
                    sqlComm.ExecuteNonQuery();
                    sl.Close();
                }
                catch (Exception ex)
                {

                }

            }


        }


        public void UpdateItemActiveStatus(string stat, int supplierId,string suppcode)
        {
            StringBuilder sb = new StringBuilder();
           
                SqlConnection sl = new SqlConnection();
                sl.ConnectionString = CONNSTRING;
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sl;
                sqlComm.CommandText = "UPDATE Items SET Active=@stats  Where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId ";
                sqlComm.Parameters.Add("@suppId", supplierId);
                sqlComm.Parameters.Add("@stats", stat);
                sqlComm.Parameters.Add("@SupplierItemCode", suppcode);
                try
                {
                    sl.Open();
                    sqlComm.ExecuteNonQuery();
                    sl.Close();
                }
                catch (Exception ex)
                {

                }


        }


        public StringBuilder UpdateOrInsertItemBySupplierCodeForAdmin(List<SupplierItem> list, int supplierId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                SqlConnection conn = new SqlConnection();
                String companyNote = String.Empty;

                conn.ConnectionString = CONNSTRING;


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = " SELECT ItemID from Items where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId ";
                cmd.Parameters.AddWithValue("@SupplierItemCode", item.SupplierItemCode);
                cmd.Parameters.AddWithValue("@suppId", supplierId);
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    if (sdr.HasRows)
                    {
                        var itemId = 0;
                        while (sdr.Read())
                        {
                            if (sdr["ItemID"] != DBNull.Value)
                                itemId = Convert.ToInt32(sdr["ItemID"].ToString());
                        }
                        conn.Close();
                        var productPrice = Convert.ToDouble(item.PriceUpdate);
                        var resellprice = item.ResellPrice;
                        var dsb = item.DSB;
                        String strSQLUpdateStmt = "update Items set COG=@cog, ManagerUnitPrice=@ResellPrice, PrinterCompatibility=@printcom,DSB=@dsb, RepUnitPrice=@ResellPrice,AlterationDate=CURRENT_TIMESTAMP where ItemID=@ItemID";
                        SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
                        cmd2.Parameters.AddWithValue("@cog", Convert.ToDouble(item.PriceUpdate));
                        cmd2.Parameters.AddWithValue("@ResellPrice", resellprice);
                        cmd2.Parameters.AddWithValue("@ItemID", itemId);
                        cmd2.Parameters.AddWithValue("@printcom", item.PrinterCompatibility);
                        cmd2.Parameters.AddWithValue("@dsb", dsb);
                        try
                        {
                            sb.Append(String.Format("Updated  Code = {0}: Price = {1}  Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                            sb.Append(Environment.NewLine);
                            conn.Open();
                            cmd2.ExecuteNonQuery().ToString();
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            if (conn != null) { conn.Close(); } sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);

                        }
                    }


                    else
                    {
                        conn.Close();
                        var BestPrice = 'Y';
                        var Faulty = 'N';
                        var productPrice = Convert.ToDouble(item.PriceUpdate);
                        var resellprice = item.ResellPrice;
                        var dsb = item.DSB;
                        String strSQLInsertStmt = "insert into dbo.Items(SupplierID,SupplierItemCode, Description," +
                       " COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, ReportedFaulty,AlterationDate,PrinterCompatibility,DSB) " +
                       " values (@SupplierID,@ItemCode,@Description, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP, @PriceLock, @ReportedFaulty,CURRENT_TIMESTAMP,@printcom,@dsb);";
                        SqlCommand cmd3 = new SqlCommand(strSQLInsertStmt, conn);
                        cmd3.Parameters.AddWithValue("@SupplierID", supplierId);
                        cmd3.Parameters.AddWithValue("@ItemCode", item.SupplierItemCode);
                        cmd3.Parameters.AddWithValue("@Description", item.Description);
                        cmd3.Parameters.AddWithValue("@COG", Convert.ToDouble(item.PriceUpdate));
                        cmd3.Parameters.AddWithValue("@ResellPrice", resellprice);
                        cmd3.Parameters.AddWithValue("@PriceLock", BestPrice);
                        cmd3.Parameters.AddWithValue("@ReportedFaulty", Faulty);
                        cmd3.Parameters.AddWithValue("@printcom", item.PrinterCompatibility);
                        cmd3.Parameters.AddWithValue("@dsb", dsb);

                        try
                        {
                            sb.Append(String.Format("Inserted newly Code = {0}: Price = {1} Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                            sb.Append(Environment.NewLine);
                            conn.Open();
                            cmd3.ExecuteNonQuery();
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            if (conn != null) { conn.Close(); }
                            sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);
                        }

                    }

                }

            }


            return sb;
        }


        public StringBuilder UpdateOrInsertItemBySupplierCode(List<SupplierItem> list, int supplierId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                SqlConnection conn = new SqlConnection();
                String companyNote = String.Empty;

                conn.ConnectionString = CONNSTRING;


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = " SELECT ItemID from Items where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId ";
                cmd.Parameters.AddWithValue("@SupplierItemCode", item.SupplierItemCode);
                cmd.Parameters.AddWithValue("@suppId", supplierId);
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    if (sdr.HasRows)
                    {
                        var itemId = 0;
                        while (sdr.Read())
                        {
                            if (sdr["ItemID"] != DBNull.Value)
                                itemId = Convert.ToInt32(sdr["ItemID"].ToString());
                        }
                        conn.Close();
                        var productPrice = Convert.ToDouble(item.PriceUpdate);
                        var resellprice = productPrice * 1.5;
                        var dsb = productPrice * 1.32;
                        String strSQLUpdateStmt = "update Items set COG=@cog, ManagerUnitPrice=@ResellPrice, PrinterCompatibility=@printcom,DSB=@dsb, RepUnitPrice=@ResellPrice,AlterationDate=CURRENT_TIMESTAMP where ItemID=@ItemID";
                        SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
                        cmd2.Parameters.AddWithValue("@cog", Convert.ToDouble(item.PriceUpdate));
                        cmd2.Parameters.AddWithValue("@ResellPrice", resellprice);
                        cmd2.Parameters.AddWithValue("@ItemID", itemId);
                        cmd2.Parameters.AddWithValue("@printcom", item.PrinterCompatibility);
                        cmd2.Parameters.AddWithValue("@dsb", dsb);
                        try
                        {
                            sb.Append(String.Format("Updated  Code = {0}: Price = {1}  Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                            sb.Append(Environment.NewLine);
                            conn.Open();
                            cmd2.ExecuteNonQuery().ToString();
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            if (conn != null) { conn.Close(); } sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);

                        }
                    }


                    else
                    {
                        conn.Close();
                        var BestPrice = 'Y';
                        var Faulty = 'N';
                        var productPrice = Convert.ToDouble(item.PriceUpdate);
                        var resellprice = productPrice * 1.5;
                        var dsb = productPrice * 1.32;
                        String strSQLInsertStmt = "insert into dbo.Items(SupplierID,SupplierItemCode, Description," +
                       " COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, ReportedFaulty,AlterationDate,PrinterCompatibility,DSB) " +
                       " values (@SupplierID,@ItemCode,@Description, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP, @PriceLock, @ReportedFaulty,CURRENT_TIMESTAMP,@printcom,@dsb);";
                        SqlCommand cmd3 = new SqlCommand(strSQLInsertStmt, conn);
                        cmd3.Parameters.AddWithValue("@SupplierID", supplierId);
                        cmd3.Parameters.AddWithValue("@ItemCode", item.SupplierItemCode);
                        cmd3.Parameters.AddWithValue("@Description", item.Description);
                        cmd3.Parameters.AddWithValue("@COG", Convert.ToDouble(item.PriceUpdate));
                        cmd3.Parameters.AddWithValue("@ResellPrice", resellprice);
                        cmd3.Parameters.AddWithValue("@PriceLock", BestPrice);
                        cmd3.Parameters.AddWithValue("@ReportedFaulty", Faulty);
                        cmd3.Parameters.AddWithValue("@printcom", item.PrinterCompatibility);
                        cmd3.Parameters.AddWithValue("@dsb", dsb);

                        try
                        {
                            sb.Append(String.Format("Inserted newly Code = {0}: Price = {1} Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                            sb.Append(Environment.NewLine);
                            conn.Open();
                            cmd3.ExecuteNonQuery();
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            if (conn != null) { conn.Close(); }
                            sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);
                        }

                    }

                }

            }


            return sb;
        }



        public StringBuilder UpdateOrInsertItemBySupplierCodeForAusJet(List<SupplierItem> list, int supplierId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                SqlConnection conn = new SqlConnection();
                String companyNote = String.Empty;

                conn.ConnectionString = CONNSTRING;


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = " SELECT ItemID from Items where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId";
                cmd.Parameters.AddWithValue("@SupplierItemCode", item.SupplierItemCode);
                cmd.Parameters.AddWithValue("@suppId", supplierId);
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    if (sdr.HasRows)
                    {
                        var itemId = 0;
                        while (sdr.Read())
                        {
                            if (sdr["ItemID"] != DBNull.Value)
                                itemId = Convert.ToInt32(sdr["ItemID"].ToString());
                        }
                        conn.Close();
                        var productPrice = Convert.ToDouble(item.PriceUpdate);
                        var resellprice = productPrice * 4;
                        String strSQLUpdateStmt = @"update Items set COG=@cog, ManagerUnitPrice=@ResellPrice,RepUnitPrice=@ResellPrice,Description=@Description,OEMCode=@oemCode,
                          AlterationDate=CURRENT_TIMESTAMP where ItemID=@ItemID";
                        SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
                        cmd2.Parameters.AddWithValue("@cog", Convert.ToDouble(item.PriceUpdate));
                        cmd2.Parameters.AddWithValue("@ResellPrice", resellprice);
                        cmd2.Parameters.AddWithValue("@ItemID", itemId);
                        cmd2.Parameters.AddWithValue("@Description", item.Description);
                        cmd2.Parameters.AddWithValue("@oemCode", item.SupplierOEMCODE);
                        try
                        {
                            sb.Append(String.Format("Updated  Code = {0}: Price = {1}  ", item.SupplierItemCode, item.PriceUpdate));
                            sb.Append(Environment.NewLine);
                            conn.Open();
                            cmd2.ExecuteNonQuery().ToString();
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            if (conn != null) { conn.Close(); }

                        }
                    }


                    else
                    {
                        conn.Close();
                        var BestPrice = 'Y';
                        var Faulty = 'N';
                        var productPrice = Convert.ToDouble(item.PriceUpdate);
                        var resellprice = productPrice * 4;

                        String strSQLInsertStmt = "insert into dbo.Items(SupplierID,SupplierItemCode, Description," +
                       " COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, ReportedFaulty,OEMCode,AlterationDate) " +
                       " values (@SupplierID,@ItemCode,@Description, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP, @PriceLock, @ReportedFaulty,@OEMCode,CURRENT_TIMESTAMP);";
                        SqlCommand cmd3 = new SqlCommand(strSQLInsertStmt, conn);
                        cmd3.Parameters.AddWithValue("@SupplierID", supplierId);
                        cmd3.Parameters.AddWithValue("@ItemCode", item.SupplierItemCode);
                        cmd3.Parameters.AddWithValue("@Description", item.Description);
                        cmd3.Parameters.AddWithValue("@COG", Convert.ToDouble(item.PriceUpdate));
                        cmd3.Parameters.AddWithValue("@ResellPrice", resellprice);
                        cmd3.Parameters.AddWithValue("@PriceLock", BestPrice);
                        cmd3.Parameters.AddWithValue("@ReportedFaulty", Faulty);
                        cmd3.Parameters.AddWithValue("@OEMCode", item.SupplierOEMCODE);
                        try
                        {
                            sb.Append(String.Format("Inserted newly Code = {0}: Price = {1} Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                            sb.Append(Environment.NewLine);
                            conn.Open();
                            cmd3.ExecuteNonQuery();
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            if (conn != null) { conn.Close(); }
                            Console.Write(ex.Message.ToString());
                        }

                    }

                }

            }


            return sb;
        }

        public StringBuilder UpdateOrInsertItemBySupplierCodeForTOD(List<SupplierItem> list, int supplierId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                SqlConnection conn = new SqlConnection();
                String companyNote = String.Empty;

                conn.ConnectionString = CONNSTRING;


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = " SELECT ItemID from Items where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId";
                cmd.Parameters.AddWithValue("@SupplierItemCode", item.SupplierItemCode);
                cmd.Parameters.AddWithValue("@suppId", supplierId);
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    if (sdr.HasRows)
                    {
                        var itemId = 0;
                        while (sdr.Read())
                        {
                            if (sdr["ItemID"] != DBNull.Value)
                                itemId = Convert.ToInt32(sdr["ItemID"].ToString());
                        }
                        conn.Close();
                        double pro;
                        if (Double.TryParse(item.PriceUpdate, out pro))
                        {
                            var productPrice = pro;
                            var resellprice = productPrice * 4;
                            var dsb = productPrice * 2;
                            String strSQLUpdateStmt = @"update Items set COG=@cog,ManagerUnitPrice=@resellerPrice ,Description=@Description, DSB=@dsb,
                            OEMCode=@OEMCode,RepUnitPrice=@resellerPrice,PrinterCompatibility=@printcom ,AlterationDate=CURRENT_TIMESTAMP where ItemID=@ItemID";
                            SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
                            cmd2.Parameters.AddWithValue("@cog", Convert.ToDouble(item.PriceUpdate));

                            cmd2.Parameters.AddWithValue("@ItemID", itemId);
                            cmd2.Parameters.AddWithValue("@Description", item.Description);
                            cmd2.Parameters.AddWithValue("@OEMCode", item.SupplierOEMCODE);
                            cmd2.Parameters.AddWithValue("@resellerPrice", resellprice);
                            cmd2.Parameters.AddWithValue("@printcom", item.PrinterCompatibility);
                            cmd2.Parameters.AddWithValue("@dsb", dsb);

                            try
                            {
                                sb.Append(String.Format("Updated  Code = {0}: Price = {1}  ", item.SupplierItemCode, item.PriceUpdate));
                                sb.Append(Environment.NewLine);
                                conn.Open();
                                cmd2.ExecuteNonQuery().ToString();
                                conn.Close();

                            }
                            catch (Exception ex)
                            {
                                if (conn != null) { conn.Close(); }
                                sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);

                            }
                        }
                    }


                    else
                    {
                        conn.Close();
                        var BestPrice = 'Y';
                        var Faulty = 'N';
                        double pro;
                        if (Double.TryParse(item.PriceUpdate, out pro))
                        {
                            var productPrice = pro;
                            var resellprice = productPrice * 4;
                            var dsb = productPrice * 2;
                            String strSQLInsertStmt = "insert into dbo.Items(SupplierID,SupplierItemCode, Description, OEMCode ," +
                           " COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, PrinterCompatibility,ReportedFaulty,AlterationDate,DSB) " +
                           " values (@SupplierID,@ItemCode,@Description,@OEMCode, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP, @PriceLock, @printCom,@ReportedFaulty,CURRENT_TIMESTAMP,@dsb);";
                            SqlCommand cmd3 = new SqlCommand(strSQLInsertStmt, conn);
                            cmd3.Parameters.AddWithValue("@SupplierID", supplierId);
                            cmd3.Parameters.AddWithValue("@ItemCode", item.SupplierItemCode);
                            cmd3.Parameters.AddWithValue("@Description", item.Description);
                            cmd3.Parameters.AddWithValue("@OEMCode", item.SupplierOEMCODE);
                            cmd3.Parameters.AddWithValue("@COG", Convert.ToDouble(item.PriceUpdate));
                            cmd3.Parameters.AddWithValue("@ResellPrice", resellprice);
                            cmd3.Parameters.AddWithValue("@PriceLock", BestPrice);
                            cmd3.Parameters.AddWithValue("@ReportedFaulty", Faulty);
                            cmd3.Parameters.AddWithValue("@printCom", item.PrinterCompatibility);
                            cmd3.Parameters.AddWithValue("@dsb", dsb);
                            try
                            {
                                sb.Append(String.Format("Inserted newly Code = {0}: Price = {1} Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                                sb.Append(Environment.NewLine);
                                conn.Open();
                                cmd3.ExecuteNonQuery();
                                conn.Close();

                            }
                            catch (Exception ex)
                            {
                                if (conn != null) { conn.Close(); }
                                sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);
                            }
                        }

                    }

                }

            }


            return sb;
        }


        public StringBuilder UpdateOrInsertItemBySupplierCodeForRTS(List<SupplierItem> list, int supplierId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                SqlConnection conn = new SqlConnection();
                String companyNote = String.Empty;

                conn.ConnectionString = CONNSTRING;


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = " SELECT ItemID from Items where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId";
                cmd.Parameters.AddWithValue("@SupplierItemCode", item.SupplierItemCode);
                cmd.Parameters.AddWithValue("@suppId", supplierId);
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    if (sdr.HasRows)
                    {
                        var itemId = 0;
                        while (sdr.Read())
                        {
                            if (sdr["ItemID"] != DBNull.Value)
                                itemId = Convert.ToInt32(sdr["ItemID"].ToString());
                        }
                        conn.Close();
                        double pro;
                        if (Double.TryParse(item.PriceUpdate, out pro))
                        {
                            var productPrice = pro;
                            var resellprice = productPrice * 4;
                            String strSQLUpdateStmt = "update Items set COG=@cog,ManagerUnitPrice=@resellerPrice ,Description=@Description, OEMCode=@OEMCode,RepUnitPrice=@resellerPrice, AlterationDate=CURRENT_TIMESTAMP where ItemID=@ItemID";
                            SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
                            cmd2.Parameters.AddWithValue("@cog", Convert.ToDouble(item.PriceUpdate));

                            cmd2.Parameters.AddWithValue("@ItemID", itemId);
                            cmd2.Parameters.AddWithValue("@Description", item.Description);
                            cmd2.Parameters.AddWithValue("@OEMCode", item.SupplierOEMCODE);
                            cmd2.Parameters.AddWithValue("@resellerPrice", resellprice);

                            try
                            {
                                sb.Append(String.Format("Updated  Code = {0}: Price = {1}  ", item.SupplierItemCode, item.PriceUpdate));
                                sb.Append(Environment.NewLine);
                                conn.Open();
                                cmd2.ExecuteNonQuery().ToString();
                                conn.Close();

                            }
                            catch (Exception ex)
                            {
                                if (conn != null) { conn.Close(); }
                                sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);

                            }
                        }
                    }


                    else
                    {
                        conn.Close();
                        var BestPrice = 'Y';
                        var Faulty = 'N';
                        double pro;
                        if (Double.TryParse(item.PriceUpdate, out pro))
                        {
                            var productPrice = pro;
                            var resellprice = productPrice * 4;

                            String strSQLInsertStmt = "insert into dbo.Items(SupplierID,SupplierItemCode, Description, OEMCode ," +
                           " COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, ReportedFaulty,AlterationDate) " +
                           " values (@SupplierID,@ItemCode,@Description,@OEMCode, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP, @PriceLock, @ReportedFaulty,CURRENT_TIMESTAMP);";
                            SqlCommand cmd3 = new SqlCommand(strSQLInsertStmt, conn);
                            cmd3.Parameters.AddWithValue("@SupplierID", supplierId);
                            cmd3.Parameters.AddWithValue("@ItemCode", item.SupplierItemCode);
                            cmd3.Parameters.AddWithValue("@Description", item.Description);
                            cmd3.Parameters.AddWithValue("@OEMCode", item.SupplierOEMCODE);
                            cmd3.Parameters.AddWithValue("@COG", Convert.ToDouble(item.PriceUpdate));
                            cmd3.Parameters.AddWithValue("@ResellPrice", resellprice);
                            cmd3.Parameters.AddWithValue("@PriceLock", BestPrice);
                            cmd3.Parameters.AddWithValue("@ReportedFaulty", Faulty);

                            try
                            {
                                sb.Append(String.Format("Inserted newly Code = {0}: Price = {1} Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                                sb.Append(Environment.NewLine);
                                conn.Open();
                                cmd3.ExecuteNonQuery();
                                conn.Close();

                            }
                            catch (Exception ex)
                            {
                                if (conn != null) { conn.Close(); }
                                sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);
                            }
                        }

                    }

                }

            }


            return sb;
        }


        public StringBuilder UpdateOrInsertItemBySupplierCodeInHouse(List<SupplierItem> list, int supplierId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                SqlConnection conn = new SqlConnection();
                String companyNote = String.Empty;

                conn.ConnectionString = CONNSTRING;


                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = " SELECT ItemID from Items where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId ";
                cmd.Parameters.AddWithValue("@SupplierItemCode", item.SupplierItemCode);
                cmd.Parameters.AddWithValue("@suppId", supplierId);
                double resellprice = 0;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    if (sdr.HasRows)
                    {
                        var itemId = 0;
                        while (sdr.Read())
                        {
                            if (sdr["ItemID"] != DBNull.Value)
                                itemId = Convert.ToInt32(sdr["ItemID"].ToString());
                        }
                        conn.Close();
                        var productPrice = Convert.ToDouble(item.PriceUpdate);
                        if (productPrice > 0)
                            resellprice = productPrice * 4;
                        String strSQLUpdateStmt = @"update Items set COG=@cog, ManagerUnitPrice=@ResellPrice,
                        RepUnitPrice=@ResellPrice,AlterationDate=CURRENT_TIMESTAMP , Quantity=@Quantity where ItemID=@ItemID";
                        SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
                        cmd2.Parameters.AddWithValue("@cog", Convert.ToDouble(item.PriceUpdate));
                        cmd2.Parameters.AddWithValue("@ResellPrice", resellprice);
                        cmd2.Parameters.AddWithValue("@ItemID", itemId);
                        cmd2.Parameters.AddWithValue("@Quantity", item.Quantity);
                        try
                        {
                            sb.Append(String.Format("Updated  Code = {0}: Price = {1}  Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                            sb.Append(Environment.NewLine);
                            conn.Open();
                            cmd2.ExecuteNonQuery().ToString();
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            if (conn != null) { conn.Close(); } sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);

                        }
                    }


                    else
                    {
                        conn.Close();
                        var BestPrice = 'Y';
                        var Faulty = 'N';
                        var productPrice = Convert.ToDouble(item.PriceUpdate);
                        if (productPrice > 0)
                            resellprice = productPrice * 4;
                        String strSQLInsertStmt = @"insert into dbo.Items(SupplierID,SupplierItemCode, Description," +
                       " COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, ReportedFaulty,Quantity,AlterationDate) " +
                       " values (@SupplierID,@ItemCode,@Description, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP, @PriceLock, @ReportedFaulty,@Quantity,CURRENT_TIMESTAMP);";
                        SqlCommand cmd3 = new SqlCommand(strSQLInsertStmt, conn);
                        cmd3.Parameters.AddWithValue("@SupplierID", supplierId);
                        cmd3.Parameters.AddWithValue("@ItemCode", item.SupplierItemCode);
                        cmd3.Parameters.AddWithValue("@Description", item.Description);
                        cmd3.Parameters.AddWithValue("@COG", Convert.ToDouble(item.PriceUpdate));
                        cmd3.Parameters.AddWithValue("@ResellPrice", resellprice);
                        cmd3.Parameters.AddWithValue("@PriceLock", BestPrice);
                        cmd3.Parameters.AddWithValue("@ReportedFaulty", Faulty);
                        cmd3.Parameters.AddWithValue("@Quantity", item.Quantity);

                        try
                        {
                            sb.Append(String.Format("Inserted newly Code = {0}: Price = {1} Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                            sb.Append(Environment.NewLine);
                            conn.Open();
                            cmd3.ExecuteNonQuery();
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            if (conn != null) { conn.Close(); }
                            sb.Append(ex.Message.ToString()); sb.Append(Environment.NewLine);
                        }

                    }

                }

            }


            return sb;
        }

        public class SupplierItem
        {
            public string ItemId { get; set; }
            public string SupplierItemCode { get; set; }
            public string SupplierName { get; set; }
            public string SupplierOEMCODE { get; set; }
            public string Description { get; set; }
            public string PrinterCompatibility { get; set; }
            public string PriceUpdate { get; set; }
            public string ResellPrice { get; set; }
            public string EnterStart { get; set; }
            public string EnterEnd { get; set; }
            public string Quantity { get; set; }
            public string DSB { get; set; }
        }

        public string GetSupplierNameByproductCode(string productCode)
        {
            var suppName = "";
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            conn.ConnectionString = CONNSTRING;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT sp.SupplierName  from Items supIt INNER JOIN [dbo].[Suppliers] sp 
               ON  supIt.SupplierID=sp.SupplierID  where  supIt.SupplierItemCode=@productCode";
            cmd.Parameters.AddWithValue("@productCode", productCode);
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        suppName = sdr["SupplierName"].ToString();

                    }

                }
            }

            return suppName;
        }

        public IList<SupplierItem> GetItemBYSupplier(int supplierId)
        {
            var listItems = new List<SupplierItem>();

            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            conn.ConnectionString = CONNSTRING;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = " SELECT ItemID , SupplierItemCode from Items where  SupplierID=@suppId";
            cmd.Parameters.AddWithValue("@suppId", supplierId);
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        var obj = new SupplierItem();
                        obj.ItemId = sdr["ItemID"].ToString();
                        obj.SupplierItemCode = sdr["SupplierItemCode"].ToString().Trim();
                        listItems.Add(obj);
                    }

                }
            }

            return listItems;
        }

    }
}
