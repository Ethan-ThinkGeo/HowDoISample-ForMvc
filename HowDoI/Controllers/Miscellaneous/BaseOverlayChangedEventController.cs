using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;
using System;
using System.Configuration;

namespace CSharp_HowDoISamples
{
    public partial class MiscellaneousController : Controller
    {
        //
        // GET: /BaseOverlayChangedEvent/


        public ActionResult BaseOverlayChangedEvent()
        {
            Map map = new Map("Map1",
                     new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage),
                   new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage));
            map.MapBackground.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#E5E3DF"));
            map.CurrentExtent = new RectangleShape(-13939426.6371, 6701997.4056, -7812401.86, 2626987.386962);
            map.MapUnit = GeographyUnit.Meter;

            map.MapTools.OverlaySwitcher.Enabled = true;

            GoogleOverlay google = new GoogleOverlay("Google Map");
            google.GoogleMapType = GoogleMapType.Normal;
            google.JavaScriptLibraryUri = new Uri(ConfigurationManager.AppSettings["GoogleUriV3"]);
            map.CustomOverlays.Add(google);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Server.MapPath("~/App_Data/cntry02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 243, 239, 228), GeoColor.FromArgb(255, 218, 193, 163), 1);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Proj4Projection proj4 = new Proj4Projection();
            proj4.InternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(4326);
            proj4.ExternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();
            worldLayer.FeatureSource.Projection = proj4;

            LayerOverlay worldOverlay = new LayerOverlay("WorldOverlay");
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            worldOverlay.Name = "ThinkGeoMap";
            map.CustomOverlays.Add(worldOverlay);

            return View(map);
        }
    }
}
