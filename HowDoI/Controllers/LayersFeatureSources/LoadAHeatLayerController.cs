using System;
using System.Web.Mvc;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Mvc;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Layers;

namespace CSharp_HowDoISamples
{
    public partial class LayersFeatureSourcesController : Controller
    {
        //
        // GET: /LoadAHeatLayer/

        public ActionResult LoadAHeatLayer()
        {
            Map map = new Map("Map1",
                new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
              new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage));
            map.MapBackground = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CurrentExtent = new RectangleShape(-91.88, 43.17, -69.88, 29.86);
            map.MapUnit = GeographyUnit.DecimalDegree;

            WorldMapKitWmsWebOverlay worldMapKitOverlay = new WorldMapKitWmsWebOverlay();
            map.CustomOverlays.Add(worldMapKitOverlay);
            //Shapefile Containing historical earthquake data
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Server.MapPath("~/App_Data/quksigx020.shp"));
            featureSource.CustomColumnFetch += new EventHandler<CustomColumnFetchEventArgs>(featureSource_CustomColumnFetch);

            HeatLayer heatLayer = new HeatLayer(featureSource);
            heatLayer.HeatStyle = new HeatStyle(180,"EarthQuakeMagnitude", 0, 12);

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.IsBaseOverlay = false;
            staticOverlay.Layers.Add(heatLayer);
            map.CustomOverlays.Add(staticOverlay);

            return View(map);
        }

        private void featureSource_CustomColumnFetch(object sender, CustomColumnFetchEventArgs e)
        {
            Feature earthQuake = ((ShapeFileFeatureSource)sender).GetFeatureById(e.Id, ReturningColumnsType.AllColumns);

            e.ColumnValue = earthQuake.ColumnValues["OTHER_MAG1"];
        }
    }
}
