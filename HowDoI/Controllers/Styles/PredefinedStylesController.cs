using System.Web.Mvc;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.MvcEdition;

namespace CSharp_HowDoISamples_for_Debug
{
    public partial class StylesController : Controller
    {
        //
        // GET: /PredefinedStyles/

        public ActionResult PredefinedStyles()
        {

            return View();
        }

        [MapActionFilter]
        public void PreDefinedStyles_SelectedIndexChanged(Map map, GeoCollection<object> args)
        {
            if (null != map)
            {
                string optionString = args[0] as string;
                FeatureLayer worldLayer = (FeatureLayer)((LayerOverlay)map.CustomOverlays[1]).Layers["WorldLayer"];
                switch (optionString)
                {
                    case "AreaStyles.Country1":
                        worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
                        break;
                    case "AreaStyles.Swamp1":
                        worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Swamp1;
                        break;
                    case "AreaStyles.Grass1":
                        worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Grass1;
                        break;
                    case "AreaStyles.Sand1":
                        worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Sand1;
                        break;
                    case "AreaStyles.Crop1":
                        worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Crop1;
                        break;
                    default:
                        break;
                }

                ((LayerOverlay)map.CustomOverlays[1]).ClientCache.CacheId = optionString;
                // ((LayerOverlay)Map1.CustomOverlays[1]).ServerCache.CacheDirectory = MapPath("~/ImageCache/" + Request.Path + "/" + ddlPreDefinedStyles.SelectedValue);
                ((LayerOverlay)map.CustomOverlays[1]).Redraw();
            }
        }
    }
}
