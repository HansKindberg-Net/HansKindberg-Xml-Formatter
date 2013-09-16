using System.ComponentModel.Design;
using EnvDTE;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Commands;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options;
using HansKindberg.Xml.Formatting;
using Microsoft.VisualStudio.Shell.Interop;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter
{
	public abstract class Registry : StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry() {}

		#endregion
	}

	public class ProductionRegistry : Registry
	{
		#region Constructors

		public ProductionRegistry()
		{
			this.For<DTE>().Singleton().Use(structureMap => (DTE) structureMap.GetInstance<IXmlFormatterPackage>().GetService(typeof(SDTE)));
			this.For<FormatXmlCommand>().Singleton().Use<FormatXmlCommand>();
			this.For<IMenuCommandService>().Singleton().Use(structureMap => (IMenuCommandService) structureMap.GetInstance<IXmlFormatterPackage>().GetService(typeof(IMenuCommandService)));
			this.For<ISettings>().Singleton().Use<Settings>();
			this.For<ISettingsRepository>().Singleton().Use<RegistryRepository>();
			this.For<IXmlFormatterFactory>().Singleton().Use<DefaultXmlFormatterFactory>();
			this.For<IVsMonitorSelection>().Singleton().Use(structureMap => (IVsMonitorSelection) structureMap.GetInstance<IXmlFormatterPackage>().GetService(typeof(SVsShellMonitorSelection)));
			this.For<IXmlFormatterLocator>().Singleton().Use<XmlFormatterLocator>();
			this.For<XmlFormatSection>().Use(context => context.GetInstance<ISettings>().ToXmlFormatSection());
		}

		#endregion
	}

	public class FakeRegistry : Registry
	{
		#region Constructors

		public FakeRegistry() {}

		#endregion
	}
}