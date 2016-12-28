using System.Web.Mvc;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Mvc;
using ThinkGeo.MapSuite.Shapes;

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

            map.MapBackground = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CustomOverlays.Add(new WorldMapKitWmsWebOverlay());

            return View(map);
        }
    }
}