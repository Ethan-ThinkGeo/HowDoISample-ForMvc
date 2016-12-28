using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class MiscellaneousController : Controller
    {
        //
        // GET: /WorldCoordinatesToScreenCoordinates/

        public ActionResult WorldCoordinatesToScreenCoordinates()
        {
            Map map = new Map("Map1",
                    new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
                    510);
            map.MapBackground.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CurrentExtent = new RectangleShape(-131.22, 55.05, -54.03, 16.91);
            map.MapUnit = GeographyUnit.DecimalDegree;

            WorldMapKitWmsWebOverlay worldMapKitWmsWebOverlay = new WorldMapKitWmsWebOverlay();
            map.CustomOverlays.Add(worldMapKitWmsWebOverlay);

            return View(map);
        }
    }
}
