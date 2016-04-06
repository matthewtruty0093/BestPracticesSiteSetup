using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Fluent.Modules.Toolboxes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace BestPracticesSiteSetup
{
	/// <summary>
	/// Custom Sitefinity module 
	/// </summary>
	public class BestPracticesSiteSetupModule : ModuleBase
	{
		#region Properties
		/// <summary>
		/// Gets the landing page id for the module.
		/// </summary>
		/// <value>The landing page id.</value>
		public override Guid LandingPageId
		{
			get
			{
				return SiteInitializer.DashboardPageNodeId;
			}
		}

		/// <summary>
		/// Gets the CLR types of all data managers provided by this module.
		/// </summary>
		/// <value>An array of <see cref="T:System.Type" /> objects.</value>
		public override Type[] Managers
		{
			get
			{
				return new Type[0];
			}
		}
		#endregion

		#region Module Initialization
		/// <summary>
		/// Initializes the service with specified settings.
		/// This method is called every time the module is initializing (on application startup by default)
		/// </summary>
		/// <param name="settings">The settings.</param>
		public override void Initialize(ModuleSettings settings)
		{
			base.Initialize(settings);

			// Add your initialization logic here
			// here we register the module resources
			// but if you have you should register your module configuration or web service here

			App.WorkWith()
				.Module(settings.Name)
					.Initialize()
					.Localization<BestPracticesSiteSetupResources>();

			// Here is also the place to register to some Sitefinity specific events like Bootstrapper.Initialized or subscribe for an event in with the EventHub class            
			// Please refer to the documentation for additional information http://www.sitefinity.com/documentation/documentationarticles/developers-guide/deep-dive/sitefinity-event-system/ieventservice-and-eventhub
		}

		/// <summary>
		/// Installs this module in Sitefinity system for the first time.
		/// </summary>
		/// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
		public override void Install(SiteInitializer initializer)
		{
			// Here you can install a virtual path to be used for this assembly
			// A virtual path is required to access the embedded resources
			//this.InstallVirtualPaths(initializer);

			// Here you can install you backend pages
			//this.InstallBackendPages(initializer);

			// Here you can also install your page/form/layout widgets
			//this.InstallPageWidgets(initializer);

			this.SystemConfigSetup();
			this.PagesConfigSetup();
			this.SecurityConfigSetup();
		}

		private void SecurityConfigSetup()
		{
			var configManager = ConfigManager.GetManager();
			var securityConfig = configManager.GetSection<SecurityConfig>();
			DataProviderSettings defaultProvider = securityConfig.MembershipProviders["Default"];
            defaultProvider.Parameters["enablePasswordRetrieval"] = "true";
            defaultProvider.Parameters["enablePasswordReset"] = "true";

            configManager.Provider.SuppressSecurityChecks = true;
			configManager.SaveSection(securityConfig);
			configManager.Provider.SuppressSecurityChecks = false;
		}

		private void PagesConfigSetup()
		{
			var configManager = ConfigManager.GetManager();
			var pagesConfig = configManager.GetSection<PagesConfig>();

			pagesConfig.IsToRedirectFromGroupPage = true;
			pagesConfig.OpenHomePageAsSiteRoot = true;
			pagesConfig.EnableBrowseAndEdit = false;

			configManager.Provider.SuppressSecurityChecks = true;
			configManager.SaveSection(pagesConfig);
			configManager.Provider.SuppressSecurityChecks = false;

		}

		private void SystemConfigSetup()
		{
			var configManager = ConfigManager.GetManager();
			var systemConfig = configManager.GetSection<SystemConfig>();
			var cacheSettings = systemConfig.CacheSettings;

			foreach (OutputCacheProfileElement item in cacheSettings.Profiles.Elements)
			{
				item.WaitForPageOutputCacheToFill = true;
                if(item.Name == "Long Cache")
                {
                    item.Duration = 86400;
                }
			}
			
			configManager.Provider.SuppressSecurityChecks = true;
			configManager.SaveSection(systemConfig);
			configManager.Provider.SuppressSecurityChecks = false;
		}

		/// <summary>
		/// Upgrades this module from the specified version.
		/// This method is called instead of the Install method when the module is already installed with a previous version.
		/// </summary>
		/// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
		/// <param name="upgradeFrom">The version this module us upgrading from.</param>
		public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
		{
			// Here you can check which one is your prevous module version and execute some code if you need to
			// See the example bolow
			//
			//if (upgradeFrom < new Version("1.0.1.0"))
			//{
			//    some upgrade code that your new version requires
			//}
		}

		/// <summary>
		/// Uninstalls the specified initializer.
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		public override void Uninstall(SiteInitializer initializer)
		{
			base.Uninstall(initializer);
			// Add your uninstall logic here
		}
		#endregion

		#region Public and overriden methods
		/// <summary>
		/// Gets the module configuration.
		/// </summary>
		protected override ConfigSection GetModuleConfig()
		{
			// If you have a module configuration, you should return it here
			// return Config.Get<ModuleConfigurationType>();
			return null;
		}
		#endregion

		#region Virtual paths
		/// <summary>
		/// Installs module virtual paths.
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		private void InstallVirtualPaths(SiteInitializer initializer)
		{
			// Here you can register your module virtual paths

			//var virtualPaths = initializer.Context.GetConfig<VirtualPathSettingsConfig>().VirtualPaths;
			//var moduleVirtualPath = BestPracticesSiteSetupModule.ModuleVirtualPath + "*";
			//if (!virtualPaths.ContainsKey(moduleVirtualPath))
			//{
			//    virtualPaths.Add(new VirtualPathElement(virtualPaths)
			//    {
			//        VirtualPath = moduleVirtualPath,
			//        ResolverName = "EmbeddedResourceResolver",
			//        ResourceLocation = typeof(BestPracticesSiteSetupModule).Assembly.GetName().Name
			//    });
			//}
		}
		#endregion

		#region Install backend pages
		/// <summary>
		/// Installs the backend pages.
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		private void InstallBackendPages(SiteInitializer initializer)
		{
			// Using our Fluent Api you can add your module backend pages hierarchy in no time
			// Here is an example using resources to localize the title of the page and adding a dummy control
			// to the module page. 

			//Guid groupPageId = new Guid("7008559d-a387-4742-b0e2-3dd4250366bc");
			//Guid pageId = new Guid("cb7d5871-5c65-48b3-b664-9a442ac9fab7");

			//initializer.Installer
			//    .CreateModuleGroupPage(groupPageId, "BestPracticesSiteSetup group page")
			//        .PlaceUnder(SiteInitializer.SitefinityNodeId)
			//        .SetOrdinal(100)
			//        .LocalizeUsing<BestPracticesSiteSetupResources>()
			//        .SetTitleLocalized("BestPracticesSiteSetupGroupPageTitle")
			//        .SetUrlNameLocalized("BestPracticesSiteSetupGroupPageUrlName")
			//        .SetDescriptionLocalized("BestPracticesSiteSetupGroupPageDescription")
			//        .ShowInNavigation()
			//        .AddChildPage(pageId, "BestPracticesSiteSetup page")
			//            .SetOrdinal(1)
			//            .LocalizeUsing<BestPracticesSiteSetupResources>()
			//            .SetTitleLocalized("BestPracticesSiteSetupPageTitle")
			//            .SetHtmlTitleLocalized("BestPracticesSiteSetupPageTitle")
			//            .SetUrlNameLocalized("BestPracticesSiteSetupPageUrlName")
			//            .SetDescriptionLocalized("BestPracticesSiteSetupPageDescription")
			//            .AddControl(new System.Web.UI.WebControls.Literal()
			//            {
			//                Text = "<h1 class=\"sfBreadCrumb\">BestPracticesSiteSetup module Installed</h1>",
			//                Mode = LiteralMode.PassThrough
			//            })
			//            .ShowInNavigation()
			//        .Done()
			//    .Done();
		}
		#endregion

		#region Widgets
		/// <summary>
		/// Installs the form widgets.
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		private void InstallFormWidgets(SiteInitializer initializer)
		{
			// Here you can register your custom form widgets in the toolbox.
			// See the example below.

			//string moduleFormWidgetSectionName = "BestPracticesSiteSetup";
			//string moduleFormWidgetSectionTitle = "BestPracticesSiteSetup";
			//string moduleFormWidgetSectionDescription = "BestPracticesSiteSetup";

			//initializer.Installer
			//    .Toolbox(CommonToolbox.FormWidgets)
			//        .LoadOrAddSection(moduleFormWidgetSectionName)
			//            .SetTitle(moduleFormWidgetSectionTitle)
			//            .SetDescription(moduleFormWidgetSectionDescription)
			//            .LoadOrAddWidget<WidgetType>(WidgetNameForDevelopers)
			//                .SetTitle(WidgetTitle)
			//                .SetDescription(WidgetDescription)
			//                .LocalizeUsing<BestPracticesSiteSetupResources>()
			//                .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (this is optional)
			//            .Done()
			//        .Done()
			//    .Done();
		}

		/// <summary>
		/// Installs the layout widgets.
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		private void InstallLayoutWidgets(SiteInitializer initializer)
		{
			// Here you can register your custom layout widgets in the toolbox.
			// See the example below.

			//string moduleLayoutWidgetSectionName = "BestPracticesSiteSetup";
			//string moduleLayoutWidgetSectionTitle = "BestPracticesSiteSetup";
			//string moduleLayoutWidgetSectionDescription = "BestPracticesSiteSetup";

			//initializer.Installer
			//    .Toolbox(CommonToolbox.Layouts)
			//        .LoadOrAddSection(moduleLayoutWidgetSectionName)
			//            .SetTitle(moduleLayoutWidgetSectionTitle)
			//            .SetDescription(moduleLayoutWidgetSectionDescription)
			//            .LoadOrAddWidget<LayoutControl>(WidgetNameForDevelopers)
			//                .SetTitle(WidgetTitle)
			//                .SetDescription(WidgetDescription)
			//                .LocalizeUsing<BestPracticesSiteSetupResources>()
			//                .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
			//                .SetParameters(new NameValueCollection() 
			//                { 
			//                    { "layoutTemplate", FullPathToTheLayoutWidget },
			//                })
			//            .Done()
			//        .Done()
			//    .Done();
		}

		/// <summary>
		/// Installs the page widgets.
		/// </summary>
		/// <param name="initializer">The initializer.</param>
		private void InstallPageWidgets(SiteInitializer initializer)
		{
			// Here you can register your custom page widgets in the toolbox.
			// See the example below.

			//string modulePageWidgetSectionName = "BestPracticesSiteSetup";
			//string modulePageWidgetSectionTitle = "BestPracticesSiteSetup";
			//string modulePageWidgetSectionDescription = "BestPracticesSiteSetup";

			//initializer.Installer
			//    .Toolbox(CommonToolbox.PageWidgets)
			//        .LoadOrAddSection(modulePageWidgetSectionName)
			//            .SetTitle(modulePageWidgetSectionTitle)
			//            .SetDescription(modulePageWidgetSectionDescription)
			//            .LoadOrAddWidget<WidgetType>(WidgetNameForDevelopers)
			//                .SetTitle(WidgetTitle)
			//                .SetDescription(WidgetDescription)
			//                .LocalizeUsing<BestPracticesSiteSetupResources>()
			//                .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
			//            .Done()
			//        .Done()
			//    .Done();
		}
		#endregion

		#region Upgrade methods
		#endregion

		#region Private members & constants
		public const string ModuleName = "BestPracticesSiteSetup";
		internal const string ModuleTitle = "BestPracticesSiteSetup";
		internal const string ModuleDescription = "This is a Custom Module which has been built with Sitefinity Thunder.";
		internal const string ModuleVirtualPath = "~/BestPracticesSiteSetup/";
		#endregion
	}
}