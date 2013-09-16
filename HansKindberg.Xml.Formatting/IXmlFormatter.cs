using HansKindberg.Xml.Linq;

namespace HansKindberg.Xml.Formatting
{
	public interface IXmlFormatter
	{
		#region Methods

		string Format(string xml);
		void FormatAttribute(IXAttribute xmlAttribute);
		void FormatComment(IXComment xmlComment);
		void FormatElement(IXElement xmlElement);

		#endregion
	}
}