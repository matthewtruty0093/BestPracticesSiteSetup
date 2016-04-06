using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace BestPracticesSiteSetup
{
	/// <summary>
	/// Localizable strings for the BestPracticesSiteSetup module
	/// </summary>
	/// <remarks>
	/// You can use Sitefinity Thunder to edit this file.
	/// To do this, open the file's context menu and select Edit with Thunder.
	/// 
	/// If you wish to install this as a part of a custom module,
	/// add this to the module's Initialize method:
	/// App.WorkWith()
	///     .Module(ModuleName)
	///     .Initialize()
	///         .Localization<BestPracticesSiteSetupResources>();
	/// </remarks>
	/// <see cref="http://www.sitefinity.com/documentation/documentationarticles/developers-guide/how-to/how-to-import-events-from-facebook/creating-the-resources-class"/>
	[ObjectInfo("BestPracticesSiteSetupResources", ResourceClassId = "BestPracticesSiteSetupResources", Title = "BestPracticesSiteSetupResourcesTitle", TitlePlural = "BestPracticesSiteSetupResourcesTitlePlural", Description = "BestPracticesSiteSetupResourcesDescription")]
	public class BestPracticesSiteSetupResources : Resource
	{
		#region Construction
		/// <summary>
		/// Initializes new instance of <see cref="BestPracticesSiteSetupResources"/> class with the default <see cref="ResourceDataProvider"/>.
		/// </summary>
		public BestPracticesSiteSetupResources()
		{
		}

		/// <summary>
		/// Initializes new instance of <see cref="BestPracticesSiteSetupResources"/> class with the provided <see cref="ResourceDataProvider"/>.
		/// </summary>
		/// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
		public BestPracticesSiteSetupResources(ResourceDataProvider dataProvider)
			: base(dataProvider)
		{
		}
		#endregion

		#region Class Description
		/// <summary>
		/// BestPracticesSiteSetup Resources
		/// </summary>
		[ResourceEntry("BestPracticesSiteSetupResourcesTitle",
			Value = "BestPracticesSiteSetup module labels",
			Description = "The title of this class.",
			LastModified = "2015/12/06")]
		public string BestPracticesSiteSetupResourcesTitle
		{
			get
			{
				return this["BestPracticesSiteSetupResourcesTitle"];
			}
		}

		/// <summary>
		/// BestPracticesSiteSetup Resources Title plural
		/// </summary>
		[ResourceEntry("BestPracticesSiteSetupResourcesTitlePlural",
			Value = "BestPracticesSiteSetup module labels",
			Description = "The title plural of this class.",
			LastModified = "2015/12/06")]
		public string BestPracticesSiteSetupResourcesTitlePlural
		{
			get
			{
				return this["BestPracticesSiteSetupResourcesTitlePlural"];
			}
		}

		/// <summary>
		/// Contains localizable resources for BestPracticesSiteSetup module.
		/// </summary>
		[ResourceEntry("BestPracticesSiteSetupResourcesDescription",
			Value = "Contains localizable resources for BestPracticesSiteSetup module.",
			Description = "The description of this class.",
			LastModified = "2015/12/06")]
		public string BestPracticesSiteSetupResourcesDescription
		{
			get
			{
				return this["BestPracticesSiteSetupResourcesDescription"];
			}
		}
		#endregion
	}
}