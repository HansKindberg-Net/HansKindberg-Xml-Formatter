using System.Xml.Linq;
using HansKindberg.Xml.Linq;

namespace HansKindberg.Xml.Formatting
{
	public class DefaultXmlParser : IXmlParser
	{
		#region Methods

		public virtual IXDocument Parse(string xml)
		{
			return new XDocumentWrapper(XDocument.Parse(xml));
		}

		#endregion
	}
}