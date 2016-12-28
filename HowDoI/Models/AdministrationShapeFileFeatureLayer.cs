using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinkGeo.MapSuite.Core;

namespace CSharp_HowDoISamples
{
    [Serializable]
    public class AdministrationShapeFileFeatureLayer : ShapeFileFeatureLayer
    {
        private SecurityLevel securityLevel;

        public AdministrationShapeFileFeatureLayer(string pathFileName)
            : base(pathFileName)
        {
            securityLevel = SecurityLevel.AdministrativeLevel;
        }

        public AdministrationShapeFileFeatureLayer(string pathFileName, SecurityLevel securityLevel)
            : base(pathFileName)
        {
            this.securityLevel = securityLevel;
        }

        public SecurityLevel SecurityLevel
        {
            get
            {
                return securityLevel;
            }
            set
            {
                securityLevel = value;
            }
        }
    }
}