using EPiServer.Forms.Core;
using EPiServer.Forms.Helpers;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;

namespace EPiServer.Forms.Samples
{
    /// <summary>
    /// Tell Forms.Core to load this Samples JS and CSS files
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(IViewModeExternalResources))]
    public class ViewModeExternalResources : IViewModeExternalResources
    {
        public IEnumerable<Tuple<string, string>> Resources
        {
            get
            {
                var publicVirtualPath = ModuleHelper.GetPublicVirtualPath(Constants.ModuleName);
                var arrRes = new List<Tuple<string, string>>();
                arrRes.Add(new Tuple<string, string>("script", publicVirtualPath + "/ClientResources/ViewMode/jquery-ui.modified.js"));
                arrRes.Add(new Tuple<string, string>("script", publicVirtualPath + "/ClientResources/ViewMode/datetimepicker.modified.js"));
                arrRes.Add(new Tuple<string, string>("script", publicVirtualPath + "/ClientResources/ViewMode/EPiServerFormsSamples.js"));

                arrRes.Add(new Tuple<string, string>("css", publicVirtualPath + "/ClientResources/ViewMode/EPiServerFormsSamples.css"));
                arrRes.Add(new Tuple<string, string>("css", publicVirtualPath + "/ClientResources/ViewMode/jquery-ui.min.css"));
                arrRes.Add(new Tuple<string, string>("css", publicVirtualPath + "/ClientResources/ViewMode/jquery-ui.structure.min.css"));
                arrRes.Add(new Tuple<string, string>("css", publicVirtualPath + "/ClientResources/ViewMode/jquery-ui.theme.min.css"));

                return arrRes;
            }
        }

    }

}