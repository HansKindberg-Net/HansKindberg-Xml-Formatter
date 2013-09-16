using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Win32.Abstractions;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter
{
	public interface IXmlFormatterPackage : IServiceProvider
	{
		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Making it a property should conflict with existing property.")]
		IRegistryKey GetUserRegistryRoot();

		#endregion
	}
}