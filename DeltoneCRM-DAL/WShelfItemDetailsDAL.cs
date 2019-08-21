using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
    public class WShelfItemDetailsDAL
    {
        private String CONNSTRING;

        public WShelfItemDetailsDAL(String connString)
        {
            CONNSTRING = connString;
        }

        public void CreateWShelfItemDetails(WShelfItemDetails obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO WShelfItemDetails(OEMCode, LocationId, CreatedUserId,Length,Width,Height,Weight,Boxing,Damaged,CreatedDate) 
                                     VALUES(@oEMCode, @locationId,@createdUserId,@length,@width, @height,@weight,@boxing,@damaged,CURRENT_TIMESTAMP)";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@oEMCode", SqlDbType.NVarChar).Value = obj.OEMCode;
                cmd.Parameters.Add("@locationId", SqlDbType.Int).Value = obj.LocationId;
                cmd.Parameters.Add("@createdUserId", SqlDbType.Int).Value = obj.CreatedUserId;
                cmd.Parameters.Add("@length", SqlDbType.NVarChar).Value = obj.Length;
                cmd.Parameters.Add("@width", SqlDbType.NVarChar).Value = obj.Width;
                cmd.Parameters.Add("@weight", SqlDbType.NVarChar).Value = obj.Weight;
                cmd.Parameters.Add("@boxing", SqlDbType.NVarChar).Value = obj.Boxing;
                cmd.Parameters.Add("@damaged", SqlDbType.Bit).Value = obj.Damaged;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();

        }

        public void CreateWShelfItemDetailsAndWItemQuantity(DisplayWShelfItemDetails obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO WShelfItemDetails(OEMCode, Name,LocationId, CreatedUserId,Length,
                                               Width,Height,Weight,Boxing,Damaged,CreatedDate,Notes,ItemId,Type,Brand) 
                                     VALUES(@oEMCode, @name,@locationId,@createdUserId,@length,@width,
                            @height,@weight,@boxing,@damaged,CURRENT_TIMESTAMP,@notes,@itemId,@type,@brand)";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@oEMCode", SqlDbType.NVarChar).Value = obj.OEMCode;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = obj.Name;
                cmd.Parameters.Add("@locationId", SqlDbType.Int).Value = obj.LocationId;
                cmd.Parameters.Add("@createdUserId", SqlDbType.Int).Value = obj.CreatedUserId;
                cmd.Parameters.Add("@length", SqlDbType.NVarChar).Value = obj.Length;
                cmd.Parameters.Add("@width", SqlDbType.NVarChar).Value = obj.Width;
                cmd.Parameters.Add("@height", SqlDbType.NVarChar).Value = obj.Height;
                cmd.Parameters.Add("@weight", SqlDbType.NVarChar).Value = obj.Weight;
                cmd.Parameters.Add("@boxing", SqlDbType.NVarChar).Value = obj.Boxing;
                cmd.Parameters.Add("@damaged", SqlDbType.Bit).Value = obj.Damaged;
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar).Value = obj.Notes;
                cmd.Parameters.Add("@itemId", SqlDbType.Int).Value = obj.ItemId;
                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = obj.Type;
                cmd.Parameters.Add("@brand", SqlDbType.NVarChar).Value = obj.Brand;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();


        }

        public DisplayWShelfProductItem GetItemBySupplierIdAndItemCode(string itemCode, int supplieId)
        {
            String strItem = String.Empty;
            DisplayWShelfProductItem itemDisplay = new DisplayWShelfProductItem();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = @"select I.*,s.SupplierName from dbo.Items I,
                      Suppliers s  where  I.SupplierID=s.SupplierID  and  SupplierItemCode=@itemCode and  I.SupplierID=" + supplieId;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Parameters.Add("@itemCode", SqlDbType.NVarChar).Value = itemCode;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        itemDisplay.SupplierItemCode = sdr["SupplierItemCode"].ToString();
                        itemDisplay.Description = sdr["Description"].ToString();
                        itemDisplay.SupplierID = Convert.ToInt32(supplieId);
                        itemDisplay.COG = sdr["COG"].ToString();
                        itemDisplay.OEMCode = sdr["OEMCode"].ToString();
                        itemDisplay.ManagerUnitPrice = sdr["ManagerUnitPrice"].ToString();
                        itemDisplay.RepUnitPrice = sdr["RepUnitPrice"].ToString();
                        itemDisplay.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                        //itemDisplay.COG = sdr["SupplierName"].ToString();
                        itemDisplay.Active = sdr["Active"].ToString();
                        itemDisplay.PriceLock = sdr["PriceLock"].ToString();
                        itemDisplay.ReportedFaulty = sdr["ReportedFaulty"].ToString();
                        itemDisplay.Quantity = sdr["Quantity"].ToString();
                        itemDisplay.DSB = sdr["DSB"].ToString();

                    }
                }
            }
            conn.Close();
            return itemDisplay;

        }


        public int GetLastInsertedWShelfItemDetailsId(int userId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            int key = 0;
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                //get primary key of inserted row
                var strcmd = @"SELECT max(Id) FROM WShelfItemDetails where  createdUserId=@userId";
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.CommandText = strcmd;
                key = (int)cmd.ExecuteScalar();
            }
            conn.Close();
            return key;

        }

        public void UpdateWShelfItemDetails(WShelfItemDetails obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE WShelfItemDetails SET OEMCode=@oEMCode, LocationId=@locationId, 
                                    Length=@length,Width=@width,Height=@height,Weight=@weight , 
                                    Boxing=@boxing,Damaged=@damaged ModifiedDate=CURRENT_TIMESTAMP WHERE Id=@Id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                cmd.Connection = conn;
                cmd.Parameters.Add("@oEMCode", SqlDbType.NVarChar).Value = obj.OEMCode;
                cmd.Parameters.Add("@locationId", SqlDbType.Int).Value = obj.LocationId;
                cmd.Parameters.Add("@length", SqlDbType.NVarChar).Value = obj.Length;
                cmd.Parameters.Add("@width", SqlDbType.NVarChar).Value = obj.Width;
                cmd.Parameters.Add("@weight", SqlDbType.NVarChar).Value = obj.Weight;
                cmd.Parameters.Add("@boxing", SqlDbType.NVarChar).Value = obj.Boxing;
                cmd.Parameters.Add("@damaged", SqlDbType.Bit).Value = obj.Damaged;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void UpdateWShelfItemDetailsAndWItemQuantity(DisplayWShelfItemDetails obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE WShelfItemDetails SET OEMCode=@oEMCode, Name=@name ,LocationId=@locationId, 
                                    Length=@length,Width=@width,Height=@height,Weight=@weight , 
                                    Boxing=@boxing,Damaged=@damaged,Notes=@notes,Type=@type,Brand=@brand, ModifiedDate=CURRENT_TIMESTAMP WHERE Id=@Id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                cmd.Connection = conn;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = obj.Name;
                cmd.Parameters.Add("@oEMCode", SqlDbType.NVarChar).Value = obj.OEMCode;
                cmd.Parameters.Add("@locationId", SqlDbType.Int).Value = obj.LocationId;
                cmd.Parameters.Add("@length", SqlDbType.NVarChar).Value = obj.Length;
                cmd.Parameters.Add("@width", SqlDbType.NVarChar).Value = obj.Width;
                cmd.Parameters.Add("@weight", SqlDbType.NVarChar).Value = obj.Weight;
                cmd.Parameters.Add("@height", SqlDbType.NVarChar).Value = obj.Height;
                cmd.Parameters.Add("@boxing", SqlDbType.NVarChar).Value = obj.Boxing;
                cmd.Parameters.Add("@damaged", SqlDbType.Bit).Value = obj.Damaged;
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar).Value = obj.Notes;

                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = obj.Type;
                cmd.Parameters.Add("@brand", SqlDbType.NVarChar).Value = obj.Brand;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            conn.Close();


        }

        public void CreateWItemQuantity(WItemQuantity obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO WItemQuantity(ShelfItemId, Qty, ProductCode,SupplierName,Active) 
                                     VALUES(@shelfItemId, @qty, @productCode,@supplierName,@active)";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@shelfItemId", SqlDbType.Int).Value = obj.ShelfItemId;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = obj.Qty;
                cmd.Parameters.Add("@productCode", SqlDbType.NVarChar).Value = obj.ProductCode;
                cmd.Parameters.Add("@supplierName", SqlDbType.NVarChar).Value = obj.SupplierName;
                cmd.Parameters.Add("@active", SqlDbType.Bit).Value = true;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void UpdateWItemQuantity(WItemQuantity obj, int shelfId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE  WItemQuantity SET Qty= @qty 
                                     WHERE ProductCode=@procode AND ShelfItemId=@shelfItemId";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@shelfItemId", SqlDbType.Int).Value = obj.ShelfItemId;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = obj.Qty;
                cmd.Parameters.Add("@procode", SqlDbType.NVarChar).Value = obj.ProductCode;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();

        }

        public void UpdateWItemActiveStatus(WItemQuantity obj, int shelfId, bool status)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE  WItemQuantity SET Active=@active WHERE ProductCode=@procode AND ShelfItemId=@shelfItemId";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@shelfItemId", SqlDbType.Int).Value = obj.ShelfItemId;
                cmd.Parameters.Add("@active", SqlDbType.Bit).Value = status;
                cmd.Parameters.Add("@procode", SqlDbType.NVarChar).Value = obj.ProductCode;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();

        }



        public WShelfItemDetails GetBYId(int id)
        {
            var obj = new WShelfItemDetails();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, OEMCode, LocationId,Length,Width,Height,Weight,Boxing,Damaged, CreatedUserId ,CreatedDate
                                      FROM dbo.WShelfItemDetails WHERE Id=@Id";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    obj.Id = Convert.ToInt32(reader["Id"]);
                    obj.OEMCode = Convert.ToString(reader["OEMCode"]);
                    obj.LocationId = Convert.ToInt32(reader["LocationId"]);
                    obj.CreatedUserId = Convert.ToInt32(reader["CreatedUserId"]);
                    obj.Length = Convert.ToString(reader["Length"]);
                    obj.Width = Convert.ToString(reader["Width"]);
                    obj.Height = Convert.ToString(reader["Height"]);
                    obj.Weight = Convert.ToString(reader["Weight"]);
                    obj.Boxing = Convert.ToString(reader["Boxing"]);
                    obj.Damaged = Convert.ToBoolean(reader["Damaged"]);
                    obj.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                }
            }
            conn.Close();

            return obj;
        }


        public List<WItemQuantity> GetBYProductCodeAndShelfId(int shelfId)
        {
            var listItems = new List<WItemQuantity>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, ShelfItemId, Qty, ProductCode,SupplierName FROM dbo.WItemQuantity 
                                     WHERE ShelfItemId = @id  AND Active=@active ";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = shelfId;
                cmd.Parameters.Add("@active", SqlDbType.Bit).Value = true;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var obj = new WItemQuantity();
                    obj.Id = Convert.ToInt32(reader["Id"]);
                    obj.ProductCode = Convert.ToString(reader["ProductCode"]);
                    obj.SupplierName = Convert.ToString(reader["SupplierName"]);
                    obj.Qty = Convert.ToInt32(reader["Qty"]);
                    obj.ShelfItemId = Convert.ToInt32(reader["ShelfItemId"].ToString());
                    listItems.Add(obj);

                }
            }
            conn.Close();
            return listItems;
        }


        public WItemQuantity GetBYProductCodeAndShelfId(int shelfId, string procode)
        {
            var obj = new WItemQuantity();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, ShelfItemId, Qty, ProductCode,SupplierName FROM dbo.WItemQuantity 
                                     WHERE ShelfItemId = @id AND ProductCode=@procode  AND Active=@active ";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = shelfId;
                cmd.Parameters.Add("@procode", SqlDbType.NVarChar).Value = procode;
                cmd.Parameters.Add("@active", SqlDbType.Bit).Value = true;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    obj.Id = Convert.ToInt32(reader["Id"]);
                    obj.ProductCode = Convert.ToString(reader["ProductCode"]);
                    obj.SupplierName = Convert.ToString(reader["SupplierName"]);
                    obj.Qty = Convert.ToInt32(reader["Qty"]);
                    obj.ShelfItemId = Convert.ToInt32(reader["ShelfItemId"].ToString());

                }
            }
            conn.Close();
            return obj;
        }

        public IList<DisplayWShelfItemDetails> GetAllShelfItems()
        {
            var list = new List<DisplayWShelfItemDetails>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT ssfit.Id, ssfit.OEMCode,ssfit.Name ,ssfit.Brand,ssfit.Type,ssfit.LocationId,ssfit.Length,ssfit.Width,ssfit.Height,ssfit.ItemId,
                               itm.SupplierItemCode,itm.Description,itm.COG,itm.DSB,itm.RepUnitPrice,itm.COG,itm.Quantity,itm.Active,ssfit.Notes,
                                      ssfit.Weight,ssfit.Boxing,ssfit.Damaged, ssfit.CreatedUserId ,ssfit.CreatedDate,wsfl.ColumnName,wsfl.RowNumber
                                      FROM   dbo.WShelfItemDetails ssfit JOIN  [dbo].[WShelf] wsfl ON ssfit.LocationId=wsfl.Id
                                          join Items itm ON itm.ItemId=ssfit.ItemId";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var obj = new DisplayWShelfItemDetails();
                    obj.Id = Convert.ToInt32(reader["Id"]);
                    obj.ItemId = Convert.ToInt32(reader["ItemId"]);
                    obj.OEMCode = Convert.ToString(reader["OEMCode"]);
                    obj.Name = Convert.ToString(reader["Name"]);
                    obj.LocationId = Convert.ToInt32(reader["LocationId"]);
                    obj.CreatedUserId = Convert.ToInt32(reader["CreatedUserId"]);
                    obj.Length = Convert.ToString(reader["Length"]);
                    obj.Width = Convert.ToString(reader["Width"]);
                    obj.Height = Convert.ToString(reader["Height"]);
                    obj.Weight = Convert.ToString(reader["Weight"]);
                    obj.Boxing = Convert.ToString(reader["Boxing"]);
                    obj.Damaged = Convert.ToBoolean(reader["Damaged"]);
                    obj.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    obj.Notes = reader["Notes"].ToString();
                    if (reader["DSB"] != DBNull.Value)
                        obj.DSB = reader["DSB"].ToString();
                    obj.LocationColumnName = Convert.ToString(reader["ColumnName"]);
                    obj.LocationRowNumber = Convert.ToString(reader["RowNumber"]);
                    obj.SupplierItemCode = reader["SupplierItemCode"].ToString();
                    obj.Description = reader["Description"].ToString();
                    obj.COG = reader["COG"].ToString();
                    obj.RepUnitPrice = reader["RepUnitPrice"].ToString();
                    obj.ManagerUnitPrice = reader["RepUnitPrice"].ToString();
                    obj.Quantity = reader["Quantity"].ToString();
                    obj.Brand = reader["Brand"].ToString();
                    obj.Type = reader["Type"].ToString();
                    list.Add(obj);
                }
            }
            conn.Close();

            return list;

        }

        public IList<WItemQuantity> GetAllItemQuantiyByShelfId(int id)
        {
            var list = new List<WItemQuantity>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, ShelfItemId, Qty, ProductCode,SupplierName FROM dbo.WItemQuantity 
                                  WHERE ShelfItemId = @id AND Active=@active ";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@active", SqlDbType.Bit).Value = true;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var obj = new WItemQuantity()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductCode = Convert.ToString(reader["ProductCode"]),
                        SupplierName = Convert.ToString(reader["SupplierName"]),
                        Qty = Convert.ToInt32(reader["Qty"]),
                        ShelfItemId = Convert.ToInt32(reader["ShelfItemId"].ToString())
                    };

                    list.Add(obj);
                }
            }
            conn.Close();

            return list;
        }


        public void DeleteWItemQuantity(int id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM WItemQuantity WHERE (ShelfItemId = @id)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public DisplayWShelfItemDetails GetDisplayWShelfItemDetailsBYId(int id)
        {
            var obj = new DisplayWShelfItemDetails();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT ssfit.Id, ssfit.OEMCode,ssfit.Name ,ssfit.Type,ssfit.Brand,ssfit.LocationId,ssfit.Length,ssfit.Width,ssfit.Height,ssfit.ItemId,
                               itm.SupplierItemCode,itm.Description,itm.COG,itm.DSB,itm.RepUnitPrice,itm.COG,itm.Quantity,itm.Active,ssfit.Notes,
                                      ssfit.Weight,ssfit.Boxing,ssfit.Damaged, ssfit.CreatedUserId ,ssfit.CreatedDate,wsfl.ColumnName,wsfl.RowNumber
                                      FROM   dbo.WShelfItemDetails ssfit JOIN  [dbo].[WShelf] wsfl ON ssfit.LocationId=wsfl.Id
                                          join Items itm ON itm.ItemId=ssfit.ItemId WHERE ssfit.Id=@Id";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    obj.Id = Convert.ToInt32(reader["Id"]);
                    obj.ItemId = Convert.ToInt32(reader["ItemId"]);
                    obj.OEMCode = Convert.ToString(reader["OEMCode"]);
                    obj.Name = Convert.ToString(reader["Name"]);
                    obj.LocationId = Convert.ToInt32(reader["LocationId"]);
                    obj.CreatedUserId = Convert.ToInt32(reader["CreatedUserId"]);
                    obj.Length = Convert.ToString(reader["Length"]);
                    obj.Width = Convert.ToString(reader["Width"]);
                    obj.Height = Convert.ToString(reader["Height"]);
                    obj.Weight = Convert.ToString(reader["Weight"]);
                    obj.Boxing = Convert.ToString(reader["Boxing"]);
                    obj.Damaged = Convert.ToBoolean(reader["Damaged"]);
                    if (reader["Active"] != DBNull.Value)
                        if (reader["Active"].ToString().Trim() == "Y")
                            obj.ActiveSts = true;
                  
                    obj.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    obj.Notes = reader["Notes"].ToString();
                    if (reader["DSB"] != DBNull.Value)
                        obj.DSB = reader["DSB"].ToString();
                    obj.LocationColumnName = Convert.ToString(reader["ColumnName"]);
                    obj.LocationRowNumber = Convert.ToString(reader["RowNumber"]);
                    obj.SupplierItemCode = reader["SupplierItemCode"].ToString();
                    obj.Description = reader["Description"].ToString();
                    obj.COG = reader["COG"].ToString();
                    obj.RepUnitPrice = reader["RepUnitPrice"].ToString();
                    obj.ManagerUnitPrice = reader["RepUnitPrice"].ToString();
                    obj.Quantity = reader["Quantity"].ToString();
                    obj.Type = reader["Type"].ToString();
                    obj.Brand = reader["Brand"].ToString();

                }
            }
            conn.Close();

            return obj;
        }

        public int GetLastInsertedItemId(string supplierCode, int supplierId, string user)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            int key = 0;
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                //get primary key of inserted row
                var strcmd = @"SELECT max(ItemID) FROM Items where  CreatedBy=@userId and SupplierID=@suppId and SupplierItemCode=@suppCode and Active='Y'";

                cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = user;
                cmd.Parameters.Add("@suppCode", SqlDbType.NVarChar).Value = supplierCode;
                cmd.Parameters.Add("@suppId", SqlDbType.Int).Value = supplierId;
                cmd.CommandText = strcmd;
                key = (int)cmd.ExecuteScalar();
            }
            conn.Close();
            return key;

        }

        public int CheckItemIdExistOrNotInShelf(int ItemId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            int key = 0;
            using (SqlCommand cmd = new SqlCommand())
            {

                var strcmd = @"SELECT Id FROM WShelfItemDetails where  ItemId=@itId";

                cmd.Parameters.Add("@itId", SqlDbType.Int).Value = ItemId;
                cmd.CommandText = strcmd;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    key = Convert.ToInt32(reader["Id"]);
                }

            }
            conn.Close();
            return key;

        }

        public DisplayWShelfItemDetails GetShelfItembyItemId(int ItemId)
        {
            var obj = new DisplayWShelfItemDetails();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT ssfit.Id, ssfit.OEMCode,ssfit.Name ,ssfit.Type,ssfit.Brand,ssfit.LocationId,ssfit.Length,ssfit.Width,ssfit.Height,ssfit.ItemId,
                               itm.SupplierItemCode,itm.Description,itm.COG,itm.DSB,itm.RepUnitPrice,itm.COG,itm.Quantity,itm.Active,ssfit.Notes,
                                      ssfit.Weight,ssfit.Boxing,ssfit.Damaged, ssfit.CreatedUserId ,ssfit.CreatedDate,wsfl.ColumnName,wsfl.RowNumber
                                      FROM   dbo.WShelfItemDetails ssfit JOIN  [dbo].[WShelf] wsfl ON ssfit.LocationId=wsfl.Id
                                          join Items itm ON itm.ItemId=ssfit.ItemId WHERE ssfit.ItemId=@Id";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = ItemId;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    obj.Id = Convert.ToInt32(reader["Id"]);
                    obj.ItemId = Convert.ToInt32(reader["ItemId"]);
                    obj.OEMCode = Convert.ToString(reader["OEMCode"]);
                    obj.Name = Convert.ToString(reader["Name"]);
                    obj.LocationId = Convert.ToInt32(reader["LocationId"]);
                    obj.CreatedUserId = Convert.ToInt32(reader["CreatedUserId"]);
                    obj.Length = Convert.ToString(reader["Length"]);
                    obj.Width = Convert.ToString(reader["Width"]);
                    obj.Height = Convert.ToString(reader["Height"]);
                    obj.Weight = Convert.ToString(reader["Weight"]);
                    obj.Boxing = Convert.ToString(reader["Boxing"]);
                    obj.Damaged = Convert.ToBoolean(reader["Damaged"]);
                    obj.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    obj.Notes = reader["Notes"].ToString();
                    if (reader["DSB"] != DBNull.Value)
                        obj.DSB = reader["DSB"].ToString();
                    obj.LocationColumnName = Convert.ToString(reader["ColumnName"]);
                    obj.LocationRowNumber = Convert.ToString(reader["RowNumber"]);
                    obj.SupplierItemCode = reader["SupplierItemCode"].ToString();
                    obj.Description = reader["Description"].ToString();
                    obj.COG = reader["COG"].ToString();
                    obj.RepUnitPrice = reader["RepUnitPrice"].ToString();
                    obj.ManagerUnitPrice = reader["RepUnitPrice"].ToString();
                    obj.Quantity = reader["Quantity"].ToString();
                    obj.Type = reader["Type"].ToString();
                    obj.Brand = reader["Brand"].ToString();

                }
            }
            conn.Close();

            return obj;

        }

        public int AddNewItem(String SupplierID, String ItemCode, String Description,
           float cog, float resellprice, String BestPrice, String Faulty, int qty, double? dsb, string oemCode)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLInsertStmt = @"insert into dbo.Items(SupplierID,SupplierItemCode, Description,
                                   COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, ReportedFaulty,Quantity,DSB,OriComp) 
                                  values (@SupplierID,@ItemCode,@Description, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP,
                                  @PriceLock, @ReportedFaulty,@Quantity,@dsb,@oemCode);";
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@COG", cog);
            cmd.Parameters.AddWithValue("@ResellPrice", resellprice);
            cmd.Parameters.AddWithValue("@PriceLock", BestPrice);
            cmd.Parameters.AddWithValue("@ReportedFaulty", Faulty);
            cmd.Parameters.AddWithValue("@Quantity", qty);
            cmd.Parameters.AddWithValue("@oemCode", oemCode);
            if (dsb == null)
                cmd.Parameters.AddWithValue("@dsb", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@dsb", dsb);
            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            return intRowsEffected;
        }

        public void UpdateItemQuantity(int ItemID, int qty)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = @"update Items set Quantity=@Quantity  where ItemID=@ItemID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@ItemID", ItemID);
            cmd.Parameters.AddWithValue("@Quantity", qty);

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
        }

        public String UpdateItemWareHouse(int ItemID, String Description, float COG, float ManagedPrice, int SupplierID,
         String SupplierItemCode, String ActInact, String BestPrice, String Faulty, int qty, double? dsb, string oemCode)
        {
            int RowsEffected = -1;
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = @"update Items set SupplierID=@SupplierID,SupplierItemCode=@SupplierItemCode,
                                   Description=@Description,COG=@cog,ManagerUnitPrice=@ManagedPrice,RepUnitPrice=@ManagedPrice,Active=@ActInact, 
                                 AlterationDate=CURRENT_TIMESTAMP, PriceLock=@PriceLock, 
                                  ReportedFaulty=@ReportedFaulty ,Quantity=@Quantity ,DSB=@dsb,OriComp=@oemCode where ItemID=@ItemID";
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
            cmd.Parameters.AddWithValue("@oemCode", oemCode);

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


        public String UpdateItem(int ItemID, String Description, float COG, float ManagedPrice, int SupplierID,
            String SupplierItemCode, String ActInact, String BestPrice, String Faulty, int qty, double? dsb, string oemCode)
        {
            int RowsEffected = -1;
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = @"update Items set SupplierID=@SupplierID,SupplierItemCode=@SupplierItemCode,
                                   Description=@Description,COG=@cog,ManagerUnitPrice=@ManagedPrice,RepUnitPrice=@ManagedPrice,Active=@ActInact, 
                                 AlterationDate=CURRENT_TIMESTAMP, PriceLock=@PriceLock, 
                                  ReportedFaulty=@ReportedFaulty ,Quantity=@Quantity ,DSB=@dsb,OriComp=@oemCode where ItemID=@ItemID";
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
            cmd.Parameters.AddWithValue("@oemCode", oemCode);

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



    }

    public class DisplayWShelfItemDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OEMCode { get; set; }
        public int LocationId { get; set; }
        public string LocationColumnName { get; set; }
        public string LocationRowNumber { get; set; }
        public int SupplierId { get; set; }
        public int CreatedUserId { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Boxing { get; set; }
        public bool Damaged { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Notes { get; set; }
        public int ItemId { get; set; }
        public string COG { get; set; }
        public string ManagerUnitPrice { get; set; }
        public string RepUnitPrice { get; set; }
        public string OriComp { get; set; }
        public string CreatedBy { get; set; }
        public string Active { get; set; }
        public bool PriceLock { get; set; }
        public bool ReportedFaulty { get; set; }
        public string Quantity { get; set; }
        public string PrinterCompatibility { get; set; }
        public string Description { get; set; }
        public string DSB { get; set; }
        public string SupplierItemCode { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public bool ActiveSts { get; set; }
        public IList<WItemQuantity> DisplayWItemQuantity { get; set; }

    }


    public class WShelfItemDetails
    {
        public int Id { get; set; }
        public string OEMCode { get; set; }
        public int LocationId { get; set; }
        public int CreatedUserId { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Boxing { get; set; }
        public bool Damaged { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }

    public class WItemQuantity
    {
        public int Id { get; set; }
        public int ShelfItemId { get; set; }
        public int Qty { get; set; }
        public string ProductCode { get; set; }
        public string SupplierName { get; set; }
        public int Isremoved { get; set; }
    }

    public class DisplayWShelfProductItem
    {
        public int ItemID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierItemCode { get; set; }
        public string OEMCode { get; set; }
        public string COG { get; set; }
        public string ManagerUnitPrice { get; set; }
        public string RepUnitPrice { get; set; }
        public string OriComp { get; set; }
        public string CreatedBy { get; set; }
        public string Active { get; set; }
        public string PriceLock { get; set; }
        public string ReportedFaulty { get; set; }
        public string Quantity { get; set; }
        public string PrinterCompatibility { get; set; }
        public string DSB { get; set; }
        public string Description { get; set; }
    }
}
