using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class InteractiveOverlaysController : Controller
    {
        public ActionResult SetBackgroundColor()
        {
            return View();
        }

        [MapActionFilter]
        public void UpdateBackgound(Map map, GeoCollection<object> args)
        {
            GeoColor backcolor = GeoColor.FromHtml(args[0].ToString());
            map.MapBackground.BackgroundBrush = new GeoSolidBrush(backcolor);
        }
    }
}