﻿using System;
using System.Drawing;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Mvc;
using ZedGraph;
using System.Web.Mvc;

namespace CSharp_HowDoISamples_for_Debug
{
    public partial class StylesController : Controller
    {
        private Map currentMap;
        //
        // GET: /DrawUsingZedGraphStyle/

        [MapActionFilter]
        public ActionResult DrawUsingZedGraphStyle(Map map, GeoCollection<object> args)
        {
            if (null == map)
            {
                map = new Map("Map1", new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage), 510);
                map.CurrentExtent = new RectangleShape(-123.41875, 41.96396484375, -107.158984375, 30.36240234375);
                map.MapUnit = GeographyUnit.DecimalDegree;

                WorldMapKitWmsWebOverlay worldMapKitOverlay = new WorldMapKitWmsWebOverlay();
                map.CustomOverlays.Add(worldMapKitOverlay);

                //Create our Zedgraph Sytle and wire up the event.
                ZedGraphStyle zedGraphStyle = new ZedGraphStyle();
                zedGraphStyle.ZedGraphDrawing += new EventHandler<ZedGraphDrawingEventArgs>(zedGraphStyle_ZedGraphDrawing);

                zedGraphStyle.RequiredColumnNames.Add("WHITE");
                zedGraphStyle.RequiredColumnNames.Add("ASIAN");
                zedGraphStyle.RequiredColumnNames.Add("AREANAME");

                ShapeFileFeatureLayer citiesLayer = new ShapeFileFeatureLayer(Server.MapPath("~/App_Data/cities_a.shp"));
                citiesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(zedGraphStyle);
                citiesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(WorldStreetsTextStyles.GeneralPurpose("AREANAME",8));
                citiesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                LayerOverlay staticOverlay = new LayerOverlay();
                staticOverlay.Layers.Add("Cities", citiesLayer);
                staticOverlay.IsBaseOverlay = false;
                map.CustomOverlays.Add(staticOverlay);
                currentMap = map;
            }

            return View(map);
        }


        private void zedGraphStyle_ZedGraphDrawing(object sender, ZedGraphDrawingEventArgs e)
        {
            ChangeLabelPosition(((ShapeFileFeatureLayer)((LayerOverlay)currentMap.CustomOverlays[1]).Layers["Cities"]), 100);

            ZedGraphControl zedGraph = new ZedGraphControl();
            zedGraph.Size = new Size(100, 100);

            zedGraph.GraphPane.Fill.Type = FillType.None;
            zedGraph.GraphPane.Chart.Fill.Type = FillType.None;

            zedGraph.GraphPane.Border.IsVisible = false;
            zedGraph.GraphPane.Chart.Border.IsVisible = false;
            zedGraph.GraphPane.XAxis.IsVisible = false;
            zedGraph.GraphPane.YAxis.IsVisible = false;
            zedGraph.GraphPane.Legend.IsVisible = false;
            zedGraph.GraphPane.Title.IsVisible = false;

            PieItem pieItem1 = zedGraph.GraphPane.AddPieSlice(Convert.ToDouble(e.Feature.ColumnValues["WHITE"]), GetColorFromGeoColor(GeoColor.StandardColors.LightBlue), 0, "White");
            pieItem1.LabelDetail.IsVisible = false;

            PieItem pieItem2 = zedGraph.GraphPane.AddPieSlice(Convert.ToDouble(e.Feature.ColumnValues["ASIAN"]), GetColorFromGeoColor(GeoColor.StandardColors.LightGreen), 0, "Asian");
            pieItem2.LabelDetail.IsVisible = false;
            pieItem2.Displacement = 0.2;

            zedGraph.AxisChange();

            e.GeoImage = new GeoImage(zedGraph.GraphPane.GetImage());
        }

        private void ChangeLabelPosition(ShapeFileFeatureLayer shapeFileLayer, int graphHeight)
        {
            ((TextStyle)shapeFileLayer.ZoomLevelSet.ZoomLevel01.CustomStyles[1]).XOffsetInPixel = -20;
            ((TextStyle)shapeFileLayer.ZoomLevelSet.ZoomLevel01.CustomStyles[1]).YOffsetInPixel = Convert.ToSingle(graphHeight * 0.4);
        }

        private Color GetColorFromGeoColor(GeoColor geoColor)
        {
            return Color.FromArgb(geoColor.AlphaComponent, geoColor.RedComponent, geoColor.GreenComponent, geoColor.BlueComponent);
        }
    }
}
