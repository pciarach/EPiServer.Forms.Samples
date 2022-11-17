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
                
                arrRes.Add(new Tuple<string, string>("script", publicVirtualPath + "/jquery-ui/jquery-ui.js"));
                arrRes.Add(new Tuple<string, string>("script", publicVirtualPath + "/js/EPiServerFormsSamples.js"));
                
                arrRes.Add(new Tuple<string, string>("css", publicVirtualPath + "/css/EPiServerFormsSamples.css"));
                arrRes.Add(new Tuple<string, string>("css", publicVirtualPath + "/jquery-ui/jquery-ui.css"));
                arrRes.Add(new Tuple<string, string>("css", publicVirtualPath + "/jquery-ui/jquery-ui.structure.css"));
                arrRes.Add(new Tuple<string, string>("css", publicVirtualPath + "/jquery-ui/jquery-ui.theme.css"));

                return arrRes;
            }
        }

    }

}
