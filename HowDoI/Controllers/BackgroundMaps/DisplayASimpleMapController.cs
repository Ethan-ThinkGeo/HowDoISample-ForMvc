using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class BackgroundMapsController : Controller
    {
        public ActionResult DisplayASimpleMap()
        {
            Map map = new Map("Map1",
                new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
                new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage));

            map.MapUnit = GeographyUnit.DecimalDegree;
            map.CurrentExtent = new RectangleShape(-125, 72, 50, -46);

            map.MapBackground = new BackgroundLayer(new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF")));
            map.CustomOverlays.Add(new WorldMapKitWmsWebOverlay());

            return View(map);
        }
    }
}