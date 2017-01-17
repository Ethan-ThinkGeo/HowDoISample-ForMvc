using System.Web.Mvc;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Mvc;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;

namespace CSharp_HowDoISamples
{
    public partial class PrintingController : Controller
    {
        public ActionResult PrintAMap()
        {
            Map map1 = new Map("Map1", new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage), 510);
            map1.CurrentExtent = new RectangleShape(-131.22, 55.05, -54.03, 16.91);
            map1.MapUnit = GeographyUnit.DecimalDegree;

            WorldMapKitWmsWebOverlay worldMapKitOverlay = new WorldMapKitWmsWebOverlay();
            map1.CustomOverlays.Add(worldMapKitOverlay);

            ShapeFileFeatureLayer usStatesLayer = new ShapeFileFeatureLayer(Server.MapPath("~/App_Data/STATES.SHP"));
            usStatesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.Transparent, GeoColor.FromArgb(255, 156, 155, 154), 1);
            usStatesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.StartCap = DrawingLineCap.Round;
            usStatesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.IsBaseOverlay = false;
            staticOverlay.Layers.Add(usStatesLayer);

            map1.CustomOverlays.Add(staticOverlay);

            return View(map1);
        }
    }
}