using System;
using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class InteractiveOverlaysController : Controller
    {
        //
        // GET: /ClickEventToHighlightOverlay/

        public ActionResult ClickEventToHighlightOverlay()
        {
            Map map = new Map("Map1", new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage), 510);
            map.MapBackground.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CurrentExtent = new RectangleShape(-131.22, 55.05, -54.03, 16.91);
            map.MapUnit = GeographyUnit.DecimalDegree;
            map.MapTools.OverlaySwitcher.Enabled = true;

            WorldMapKitWmsWebOverlay worldMapKitOverlay = new WorldMapKitWmsWebOverlay("WorldMapKitOverlay");
            map.CustomOverlays.Add(worldMapKitOverlay);

            Feature feature = new Feature(new RectangleShape(-110.22, 50, -80.03, 30));
            map.HighlightOverlay.HighlightStyle = map.HighlightOverlay.Style;
            map.HighlightOverlay.Features.Add("feature", feature);

            map.Popups.Add(new CloudPopup("Popup") { AutoSize = true });

            return View(map);
        }

        [MapActionFilter]
        public string ClickEvent(Map map, GeoCollection<object> args)
        {
            double x = Convert.ToDouble(args[0]);
            double y = Convert.ToDouble(args[1]);

            PointShape location = new PointShape(x, y);

            return "<span class='popup'>Inside the area</span>";
        }
    }
}
