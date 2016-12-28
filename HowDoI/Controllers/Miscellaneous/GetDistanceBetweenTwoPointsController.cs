using System;
using System.Web.Mvc;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Mvc;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;

namespace CSharp_HowDoISamples
{
    public partial class MiscellaneousController : Controller
    {
        //
        // GET: /GetDistanceBetweenTwoPoints/

        public ActionResult GetDistanceBetweenTwoPoints()
        {
            Map map = new Map("Map1",
                 new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
                 new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage));
            map.MapBackground = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CurrentExtent = new RectangleShape(-131.22, 55.05, -54.03, 16.91);
            map.MapUnit = GeographyUnit.DecimalDegree;

            WorldMapKitWmsWebOverlay worldMapKitWmsWebOverlay = new WorldMapKitWmsWebOverlay();
            map.CustomOverlays.Add(worldMapKitWmsWebOverlay);

            InMemoryFeatureLayer pointShapeLayer = new InMemoryFeatureLayer();
            pointShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.StandardColors.Red, 8);
            pointShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer lineShapeLayer = new InMemoryFeatureLayer();
            lineShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.StandardColors.Red, 3));
            lineShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay dynamicOverlay = new LayerOverlay("DynamicOverlay");
            dynamicOverlay.IsBaseOverlay = false;
            dynamicOverlay.TileType = TileType.SingleTile;
            dynamicOverlay.Layers.Add("pointShapeLayer", pointShapeLayer);
            dynamicOverlay.Layers.Add("lineShapeLayer", lineShapeLayer);
            map.CustomOverlays.Add(dynamicOverlay);

            map.Popups.Add(new CloudPopup("information") { AutoSize = true, IsVisible = false });

            return View(map);
        }

        [MapActionFilter]
        public string GetDistance(Map map, GeoCollection<object> args)
        {
            PointShape pointShape = new PointShape(string.Format("POINT ({0} {1})", args[0], args[1]));
            LayerOverlay dynamicOverlay = (LayerOverlay)map.CustomOverlays["DynamicOverlay"];
            InMemoryFeatureLayer pointShapeLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["pointShapeLayer"];
            Feature newPoint = new Feature(pointShape);
            InMemoryFeatureLayer lineShapeLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["lineShapeLayer"];

            if (Session["StartPoint"] == null)
            {
                pointShapeLayer.InternalFeatures.Clear();
                lineShapeLayer.InternalFeatures.Clear();
            }

            pointShapeLayer.InternalFeatures.Add(newPoint.Id, newPoint);

            string popupContentHtml = string.Empty;
            if (Session["StartPoint"] != null)
            {
                PointShape startPoint = (PointShape)Session["StartPoint"];
                MultilineShape line = startPoint.GetShortestLineTo(pointShape, GeographyUnit.DecimalDegree);

                Feature lineFeature = new Feature(line);
                string distanceValue = String.Format("<span class='popup'>{0} Mile</span>", line.GetLength(GeographyUnit.DecimalDegree, DistanceUnit.Mile).ToString("N2"));
                lineShapeLayer.InternalFeatures.Add(lineFeature.Id, lineFeature);

                popupContentHtml = distanceValue;

                Session["StartPoint"] = null;
            }
            else
            {
                Session["StartPoint"] = pointShape;
            }

            return popupContentHtml;
        }
    }
}
