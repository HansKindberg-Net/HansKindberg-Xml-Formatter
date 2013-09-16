using HansKindberg.Xml.Linq;

namespace HansKindberg.Xml.Formatting
{
	public interface IXmlParser
	{
		#region Methods

		IXDocument Parse(string xml);

		#endregion
	}
}