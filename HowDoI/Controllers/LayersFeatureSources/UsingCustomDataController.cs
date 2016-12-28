using System;
using System.Web.Mvc;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Mvc;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;

namespace CSharp_HowDoISamples
{
    public partial class LayersFeatureSourcesController : Controller
    {
        //
        // GET: /UsingCustomData/

        public ActionResult UsingCustomData()
        {
            Map map = new Map("Map1",
                new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
              new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage));
            map.MapBackground = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CurrentExtent = new RectangleShape(-125, 72, 50, -46);
            map.MapUnit = GeographyUnit.DecimalDegree;

            WorldMapKitWmsWebOverlay worldMapKitOverlay = new WorldMapKitWmsWebOverlay();
            map.CustomOverlays.Add(worldMapKitOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Server.MapPath("~/App_Data/cntry02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("Test", "Arial", 10, DrawingFontStyles.Regular, GeoColor.StandardColors.Red, 0, -12);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            worldLayer.FeatureSource.CustomColumnFetch += new EventHandler<CustomColumnFetchEventArgs>(FeatureSource_CustomColumnFetch);
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.RequiredColumnNames.Add("Test");

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.IsBaseOverlay = false;
            staticOverlay.Layers.Add(worldLayer);
            map.CustomOverlays.Add(staticOverlay);

            return View(map);
        }

        void FeatureSource_CustomColumnFetch(object sender, CustomColumnFetchEventArgs e)
        {
            if (e.Id == "135" || e.Id == "47")
            {
                string columnName = e.ColumnName;
                e.ColumnValue = "CountryId:" + e.Id;
            }
        } 
    }
}
