//  Authors:  Brendan C. Ward, Robert M. Scheller

//using Landis.Core;
using Landis.Library.BiomassCohorts;
using System.Collections.Generic;
using Landis.SpatialModeling;
using Landis.Library.Biomass;

namespace Landis.Extension.Output.BiomassByAge
{
    public static class SiteVars
    {
        private static ISiteVar<ISiteCohorts> cohorts;

        //---------------------------------------------------------------------

        public static void Initialize()
        {
            cohorts = PlugIn.ModelCore.GetSiteVar<ISiteCohorts>("Succession.BiomassCohorts");

            if (cohorts == null)
            {
                string mesg = string.Format("Cohorts are empty.  Please double-check that this extension is compatible with your chosen succession extension.");
                throw new System.ApplicationException(mesg);
            }

        }

        //---------------------------------------------------------------------
        public static ISiteVar<ISiteCohorts> Cohorts
        {
            get
            {
                return cohorts;
            }
        }
    }
}
