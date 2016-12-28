using System;
using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class InteractiveOverlaysController : Controller
    {
        public ActionResult AddAMarker()
        {
            return View();
        }

        [MapActionFilter]
        public void AddMarker(Map map, GeoCollection<object> args)
        {
            if (map != null)
            {
                string markerId = args["markerId"].ToString();
                double x = Convert.ToDouble(args["x"]);
                double y = Convert.ToDouble(args["y"]);
                InMemoryMarkerOverlay markerOverlay = map.CustomOverlays["MarkerOverlay"] as InMemoryMarkerOverlay;
                if (!markerOverlay.FeatureSource.InternalFeatures.Contains(markerId))
                {
                    // Add a new feature as a marker
                    markerOverlay.FeatureSource.InternalFeatures.Add(markerId, new Feature(x, y));
                }
            }
        }
    }
}