using System.Collections.ObjectModel;
using System.Data;
using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class SpatialFunctionsController : Controller
    {
        //
        // GET: /GetColumnData/

        public ActionResult GetColumnData()
        {
            Map map = new Map("Map1",
         new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
         new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage));
            map.MapBackground.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#B3C6D4"));
            map.CurrentExtent = new RectangleShape(-125, 72, 50, -46);
            map.MapUnit = GeographyUnit.DecimalDegree;

            WorldMapKitWmsWebOverlay worldMapKitOverlay = new WorldMapKitWmsWebOverlay();
            map.CustomOverlays.Add(worldMapKitOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Server.MapPath("~/App_Data/cntry02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay("StaticOverlay");
            staticOverlay.IsBaseOverlay = false;
            staticOverlay.Layers.Add(worldLayer);
            map.CustomOverlays.Add(staticOverlay);
            ViewData["map"] = map;

            DataTable dataTable = PopulateTable(worldLayer);
            Collection<FeatureColumn> countries = new Collection<FeatureColumn>();
            foreach (DataRow row in dataTable.Rows)
            {
                FeatureColumn country = new FeatureColumn();
                country.ColumnName = row["ColumnName"].ToString();
                country.MaxLength = row["MaxLength"].ToString();
                country.TypeName = row["TypeName"].ToString();

                countries.Add(country);
            }
            return View(countries);
        }

        private DataTable PopulateTable(FeatureLayer featureLayer)
        {
            featureLayer.Open();
            Collection<FeatureSourceColumn> allColumns = featureLayer.QueryTools.GetColumns();
            featureLayer.Close();

            return GetDataTableFromFeatureSourceColumns(allColumns);
        }

        private static DataTable GetDataTableFromFeatureSourceColumns(Collection<FeatureSourceColumn> featureSourceColumns)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ColumnName");
            dataTable.Columns.Add("MaxLength");
            dataTable.Columns.Add("TypeName");

            foreach (FeatureSourceColumn column in featureSourceColumns)
            {
                AddRow(column, dataTable);
            }

            return dataTable;
        }

        private static void AddRow(FeatureSourceColumn featureSourceColumn, DataTable dataTable)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow[0] = featureSourceColumn.ColumnName;
            dataRow[1] = featureSourceColumn.MaxLength;
            dataRow[2] = featureSourceColumn.TypeName;

            dataTable.Rows.Add(dataRow);
        }

    }
}
