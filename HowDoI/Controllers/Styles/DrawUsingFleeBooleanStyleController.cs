using System.Web.Mvc;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples_for_Debug
{
    public partial class StylesController : Controller
    {
        //
        // GET: /DrawUsingFleeBooleanStyle/
        [MapActionFilter]
        public ActionResult DrawUsingFleeBooleanStyle()
        {
            return View();
        }

    }
}
