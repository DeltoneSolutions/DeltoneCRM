using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Warehouse
{
    public partial class AddNewItemReturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadShelfDetails();
            }
        }

        protected void LoadShelfDetails()
        {
            WareShelfDAL shelfDAl = new WareShelfDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            var listAll = shelfDAl.GetAllShelfs();
            var listRow = new List<LocationManageRow>();

            Dictionary<string, string> list = new Dictionary<string, string>();
            foreach (var item in listAll)
            {
                if (!list.ContainsValue(item.ColumnName))
                    list.Add(item.Id.ToString(), (item.ColumnName));
                var obj = new LocationManageRow();
                obj.Id = item.Id.ToString();
                obj.RowName = item.RowNumber;
                obj.ColName = item.ColumnName;
                listRow.Add(obj);

            }



            Dictionary<string, string> listRowDrop = new Dictionary<string, string>();
            if (listAll.Count() > 0)
            {
                var firstElement = listAll[0];
                var filterRowsById = (from ro in listAll where ro.ColumnName == firstElement.ColumnName select ro).ToList();

                foreach (var item in filterRowsById)
                {
                    listRowDrop.Add(item.Id.ToString(), (item.RowNumber));

                }
            }

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var serJsonDetails = javaScriptSerializer.Serialize(listRow);

            rownamehidden.Value = serJsonDetails;

            warehouselocationdropdownlist.DataSource = list;
            warehouselocationdropdownlist.DataTextField = "Value";
            warehouselocationdropdownlist.DataValueField = "Key";
            warehouselocationdropdownlist.DataBind();

            warehouselocationrowdropdownlist.DataSource = listRowDrop;
            warehouselocationrowdropdownlist.DataTextField = "Value";
            warehouselocationrowdropdownlist.DataValueField = "Key";
            warehouselocationrowdropdownlist.DataBind();




        }

        public class LocationManageRow
        {
            public string Id { get; set; }
            public string RowName { get; set; }
            public string ColName { get; set; }
        }
    }
}