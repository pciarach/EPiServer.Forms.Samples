using EPiServer.Forms.Samples.Controllers;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Modules;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text.Json;


namespace EPiServer.Forms.Samples
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule), typeof(EPiServer.Shell.ShellInitialization), typeof(EPiServer.Forms.InitializationModule))]
    public partial class InitializationModule : IConfigurableModule
    {
        /// <summary>
        /// Configure the container for this module
        /// </summary>
        /// <param name="context">The EPiServer context</param>
        public void ConfigureContainer(ServiceConfigurationContext serviceConfigurationContext)
        {
            var serviceProvider = serviceConfigurationContext.Services.BuildServiceProvider();

            serviceConfigurationContext.Services.AddStartupFilter<FormsSamplePublicStaticFileStartupFilter>();
            //serviceConfigurationContext.Services.AddSingleton<IFormSamplesConfiguration, FormSamplesConfiguration>();
            serviceConfigurationContext.Services.AddHttpContextAccessor();
            serviceConfigurationContext.Services.AddEmbeddedLocalization<InitializationModule>();
            serviceConfigurationContext.Services.AddHttpClient();
            serviceConfigurationContext.Services.Configure<ProtectedModuleOptions>(options =>
            {
                if (!options.Items.Any(x => x.Name.Equals("EPiServer.Forms.Samples")))
                {
                    var module = new ModuleDetails
                    {
                        Name = "EPiServer.Forms.Samples",

                    };
                    options.Items.Add(module);
                }
            });

            serviceConfigurationContext.Services.UseSystemTextJsonSerialization(new[]
            {
                typeof(FormInfoController),
                typeof(ExternalValidateController)
            },
            settings =>
            {
                settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        }

        /// <summary>
        /// Initialize this module
        /// </summary>
        /// <param name="context">The EPiServer initialization context</param>
        public void Initialize(InitializationEngine context)
        {
        }

        /// <summary>
        /// Preloads for this module (not used)
        /// </summary>
        /// <param name="parameters">Any parameters required to preload this module</param>
        public void Preload(string[] parameters) { }

        /// <summary>
        /// Uninitialize this module
        /// </summary>
        /// <param name="context">The EPiServer initialization context</param>
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
