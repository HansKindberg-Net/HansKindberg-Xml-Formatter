using System.Collections.Generic;
using HansKindberg.Xml.Linq.Comparison;

namespace HansKindberg.Xml.Formatting
{
	public interface IXmlFormat : IXAttributeComparerSettings, IXElementComparerSettings, IXNodeComparerSettings
	{
		#region Properties

		ICollection<string> AttributeNamesToCorrectCommaSeparatedValuesFor { get; }
		bool CloseEmptyElements { get; set; }
		XmlCommentFormat CommentFormat { get; set; }
		bool Indent { get; set; }
		string IndentString { get; set; }
		int MinimumNumberOfAttributesForNewLineOnAttributes { get; set; }
		string Name { get; set; }
		bool NewLineOnAttributes { get; set; }
		string NewLineString { get; set; }
		bool OmitComments { get; set; }
		bool OmitXmlDeclaration { get; set; }

		#endregion
	}
}