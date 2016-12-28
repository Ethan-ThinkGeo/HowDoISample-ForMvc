using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class MiscellaneousController : Controller
    {
        //
        // GET: /FindWorldCoordinatesWhereClicked/
        public ActionResult FindWorldCoordinatesWhereClicked()
        {
            Map map = new Map("Map1",
                new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
                new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage));
            map.MapBackground.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CurrentExtent = new RectangleShape(-131.22, 55.05, -54.03, 16.91);
            map.MapUnit = GeographyUnit.DecimalDegree;

            WorldMapKitWmsWebOverlay worldMapKitWmsWebOverlay = new WorldMapKitWmsWebOverlay();
            map.CustomOverlays.Add(worldMapKitWmsWebOverlay);

            return View(map);
        }

        [MapActionFilter]
        public string FindCoordinate(Map map, GeoCollection<object> args)
        {
            return string.Format(@"The world coordinates where you clicked are <br/><span style='color:red;font-size:13;font-weight:bolder;'> ({0}, {1}) </span>.", args[0], args[1]);
        }
    }
}
