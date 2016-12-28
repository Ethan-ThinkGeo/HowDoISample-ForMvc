using System;
using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class InteractiveMapController : Controller
    {
        //
        // GET: /AddAClickEvent/

        public ActionResult AddAClickEvent()
        {
            Map map = new Map("Map1",
                    new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
                    510);
            map.MapBackground.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CurrentExtent = new RectangleShape(-125, 72, 50, -46);
            map.MapUnit = GeographyUnit.DecimalDegree;

            WorldMapKitWmsWebOverlay worldMapKitOverlay = new WorldMapKitWmsWebOverlay("WorldMapKitOverlay");
            map.CustomOverlays.Add(worldMapKitOverlay);

            InMemoryMarkerOverlay markerOverlay = new InMemoryMarkerOverlay("MarkerOverlay");
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultMarkerStyle.WebImage.ImageWidth = 21;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultMarkerStyle.WebImage.ImageHeight = 25;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultMarkerStyle.WebImage.ImageOffsetX = -10.5f;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultMarkerStyle.WebImage.ImageOffsetY = -25f;
            markerOverlay.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            map.CustomOverlays.Add(markerOverlay);

            return View(map);
        }

        [MapActionFilter]
        public void ClickEvent(Map map, GeoCollection<object> args)
        {
            PointShape position = new PointShape(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]));

            InMemoryMarkerOverlay markerOverlay = (InMemoryMarkerOverlay)map.CustomOverlays["MarkerOverlay"];
            markerOverlay.FeatureSource.InternalFeatures.Add("marker" + Guid.NewGuid().ToString(), new Feature(position));
        }
    }
}
