using EPiServer.Forms.Helpers.Internal;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using EPiServer.Web.Hosting;
using System.Collections.Specialized;
using System.Web;
using System.Web.Hosting;


namespace EPiServer.Forms.Samples
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule), typeof(EPiServer.Shell.ShellInitialization), typeof(EPiServer.Forms.InitializationModule))]
    public partial class InitializationModule : IConfigurableModule, IInitializableHttpModule
    {
        private static object _lock = new object();
        private static readonly ILogger _logger = LogManager.GetLogger(typeof(InitializationModule));

        /// <summary>
        /// Configure the container for this module
        /// </summary>
        /// <param name="context">The EPiServer context</param>
        public void ConfigureContainer(ServiceConfigurationContext serviceConfigurationContext) { }

        /// <summary>
        /// Initialize this module
        /// </summary>
        /// <param name="context">The EPiServer initialization context</param>
        public void Initialize(InitializationEngine context)
        {
            _logger.Information("Initialize EPiServer Forms Samples");
            AddVppForPublicFolderInsideThisAddOn();
        }

        /// <summary>
        /// Initializes HTTP events such as attaching HTTP modules to events
        /// </summary>
        /// <param name="application">The application context</param>
        public void InitializeHttpEvents(HttpApplication application) { }

        /// <summary>
        /// Preloads for this module (not used)
        /// </summary>
        /// <param name="parameters">Any parameters required to preload this module</param>
        public void Preload(string[] parameters) { }

        /// <summary>
        /// Uninitialize this module
        /// </summary>
        /// <param name="context">The EPiServer initialization context</param>
        public void Uninitialize(global::EPiServer.Framework.Initialization.InitializationEngine context)
        {
        }

        /// <summary>
        /// add a new VPP folders to point to this own plugin folders, 
        /// so we don't need to modify the EPiServerFramework.config
        /// <remarks>This is protected AddOn, so public end user cannot access its files under /se/EPiServer.Forms.Samples/ClientResources, ... </remarks>
        /// </summary>
        private void AddVppForPublicFolderInsideThisAddOn()
        {
            // TECHNOTE: allow access to protected module's client resources
            // because anonymous user views Forms's Elements in ViewMode, we need to provide them .js file inside the addon (protected) folder
            var publicVpp = new NameValueCollection();
            publicVpp.Add("virtualPath", "~" + ModuleHelper.GetPublicVirtualPath(Constants.ModuleName) + "/ClientResources/ViewMode");
            publicVpp.Add("physicalPath", ModuleHelper.ToPhysicalVPPClientResource(this.GetType(), @"ClientResources\ViewMode"));
            publicVpp.Add("bypassAccessCheck", "true");
            HostingEnvironment.RegisterVirtualPathProvider(new VirtualPathNonUnifiedProvider("PublicEPiServerFormsClientResourcesVpp", publicVpp));
            _logger.Information("Create VPP for EPiServer.Forms.Samples {0} ==> {1}", publicVpp["virtualPath"], publicVpp["physicalPath"]);
        }
    }
}