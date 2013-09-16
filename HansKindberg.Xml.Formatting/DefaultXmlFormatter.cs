using System.Collections.Generic;
using HansKindberg.Xml.Linq;
using HansKindberg.Xml.Linq.Comparison;

namespace HansKindberg.Xml.Formatting
{
	public class DefaultXmlFormatter : XmlFormatter
	{
		#region Constructors

		public DefaultXmlFormatter(IXmlFormat xmlFormat) : this(xmlFormat, new XAttributeComparer(xmlFormat)) {}
		protected internal DefaultXmlFormatter(IXmlFormat xmlFormat, IComparer<IXAttribute> xmlAttributeComparer) : base(xmlFormat, new DefaultXmlParser(), new XNodeComparer(xmlFormat, new XElementComparer(xmlFormat, xmlAttributeComparer)), xmlAttributeComparer) {}

		#endregion
	}
}