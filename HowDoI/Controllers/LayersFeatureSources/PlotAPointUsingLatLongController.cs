using System;
using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples
{
    public partial class LayersFeatureSourcesController : Controller
    {
        public ActionResult PlotAPointUsingLatLong()
        {
            return View();
        }

        [MapActionFilter]
        public void PlotPoint(Map map, GeoCollection<object> args)
        {
            if (map != null)
            {
                double x = Convert.ToDouble(args["x"]);
                double y = Convert.ToDouble(args["y"]);
                LayerOverlay dynamicOverlay = (LayerOverlay)map.CustomOverlays["DynamicOverlay"];
                InMemoryFeatureLayer shapeLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["ShapeLayer"];

                Feature pointFeature = new Feature(new PointShape(x, y));
                shapeLayer.InternalFeatures.Add(pointFeature.Id, pointFeature);
            }
        }
    }
}