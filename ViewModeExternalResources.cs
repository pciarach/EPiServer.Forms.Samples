using EPiServer.Forms.Helpers.Internal;
using EPiServer.Configuration;
using EPiServer.Forms.Implementation;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;

namespace EPiServer.Forms.Samples
{
    /// <summary>
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(IViewModeExternalResources))]
    public class ViewModeExternalResources : IViewModeExternalResources
    {
        public virtual IEnumerable<Tuple<string, string>> Resources
        {
            get
            {
                var publicVirtualPath = ModuleHelper.GetPublicVirtualPath(Constants.ModuleName);
                var currentPageLanguage = FormsExtensions.GetCurrentPageLanguage();

                var arrRes = new List<Tuple<string, string>>();
                
                arrRes.Add(new Tuple<string, string>("script", publicVirtualPath + "/ClientResources/ViewMode/jquery-ui.modified.js"));
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