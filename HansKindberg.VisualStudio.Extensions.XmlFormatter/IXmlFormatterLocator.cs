using System.Diagnostics.CodeAnalysis;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;
using HansKindberg.Xml.Formatting;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter
{
	public interface IXmlFormatterLocator
	{
		#region Properties

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Default")]
		IXmlFormatter Default { get; }

		#endregion

		#region Methods

		IXmlFormatter GetByName(string name);
		void Refresh(XmlFormatSection xmlFormatSection);

		#endregion
	}
}