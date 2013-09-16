using System;
using System.Diagnostics.CodeAnalysis;
using StructureMap;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter
{
	public class Bootstrapper
	{
		#region Fields

		private static Boolean _hasStarted;

		#endregion

		#region Methods

		public static void Bootstrap(IXmlFormatterPackage xmlFormatterPackage)
		{
			new Bootstrapper().BootstrapStructureMap(xmlFormatterPackage);
		}

		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public void BootstrapStructureMap(IXmlFormatterPackage xmlFormatterPackage)
		{
			if(xmlFormatterPackage == null)
				throw new ArgumentNullException("xmlFormatterPackage");

			ObjectFactory.Initialize(initializer =>
			{
				initializer.For<IXmlFormatterPackage>().Singleton().Use(xmlFormatterPackage);
				initializer.AddRegistry<ProductionRegistry>();
			});
		}

		public static void Restart(IXmlFormatterPackage xmlFormatterPackage)
		{
			if(_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap(xmlFormatterPackage);
				_hasStarted = true;
			}
		}

		#endregion
	}
}