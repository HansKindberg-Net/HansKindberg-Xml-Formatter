using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;
using HansKindberg.Xml.Formatting;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options
{
	public class XmlFormatSetting
	{
		#region Fields

		private const string _attributesCategory = "Attributes";
		private const string _commentsCategory = "Comments";
		private const string _elementsCategory = "Elements";

		private static readonly Dictionary<string, string> _escapeStringConversions = new Dictionary<string, string>
			{
				{"\n", @"\n"},
				{"\r", @"\r"},
				{"\t", @"\t"}
			};

		private const string _indentationCategory = "Indentation";
		private const string _xmlCategory = "XML";
		private XmlFormatElement _xmlFormatElement;

		#endregion

		#region Properties

		[Category(_attributesCategory)]
		[Description("Attribute name-comparison - description")]
		[DisplayName("Attribute name-comparison")]
		public virtual StringComparison AttributeNameComparison
		{
			get { return this.XmlFormatElement.AttributeNameComparison; }
			set { this.XmlFormatElement.AttributeNameComparison = value; }
		}

		[Category(_attributesCategory)]
		[Description("Attributes to correct comma-separated values for - description")]
		[DisplayName("Attributes to correct comma-separated values for")]
		public virtual string AttributeNamesToCorrectCommaSeparatedValuesFor
		{
			get { return this.XmlFormatElement.AttributeNamesToCorrectCommaSeparatedValuesForInternal; }
			set { this.XmlFormatElement.AttributeNamesToCorrectCommaSeparatedValuesForInternal = value; }
		}

		[Category(_attributesCategory)]
		[Description("Attributes to pin - description")]
		[DisplayName("Attributes to pin")]
		public virtual string AttributeNamesToPinFirst
		{
			get { return this.XmlFormatElement.AttributeNamesToPinFirstInternal; }
			set { this.XmlFormatElement.AttributeNamesToPinFirstInternal = value; }
		}

		[Category(_attributesCategory)]
		[Description("Attribute value-comparison - description")]
		[DisplayName("Attribute value-comparison")]
		public virtual StringComparison AttributeValueComparison
		{
			get { return this.XmlFormatElement.AttributeValueComparison; }
			set { this.XmlFormatElement.AttributeValueComparison = value; }
		}

		[Category(_attributesCategory)]
		[Description("Attribute alphabetical sort-direction - description")]
		[DisplayName("Attribute alphabetical sort-direction")]
		public virtual ListSortDirection AttributesAlphabeticalSortDirection
		{
			get { return this.XmlFormatElement.AttributesAlphabeticalSortDirection; }
			set { this.XmlFormatElement.AttributesAlphabeticalSortDirection = value; }
		}

		[Category(_elementsCategory)]
		[Description("Close empty elements - description")]
		[DisplayName("Close empty elements")]
		public virtual bool CloseEmptyElements
		{
			get { return this.XmlFormatElement.CloseEmptyElements; }
			set { this.XmlFormatElement.CloseEmptyElements = value; }
		}

		[Category(_commentsCategory)]
		[Description("Comment format - description")]
		[DisplayName("Comment format")]
		public virtual XmlCommentFormat CommentFormat
		{
			get { return this.XmlFormatElement.CommentFormat; }
			set { this.XmlFormatElement.CommentFormat = value; }
		}

		[Category(_elementsCategory)]
		[Description("Element levels to exclude from sorting - description")]
		[DisplayName("Element levels to exclude from sorting")]
		public virtual string ElementLevelsToExcludeFromSortingAlphabetically
		{
			get { return this.XmlFormatElement.ElementLevelsToExcludeFromSortingAlphabeticallyInternal; }
			set { this.XmlFormatElement.ElementLevelsToExcludeFromSortingAlphabeticallyInternal = value; }
		}

		[Category(_elementsCategory)]
		[Description("Element name-comparison - description")]
		[DisplayName("Element name-comparison")]
		public virtual StringComparison ElementNameComparison
		{
			get { return this.XmlFormatElement.ElementNameComparison; }
			set { this.XmlFormatElement.ElementNameComparison = value; }
		}

		[Category(_elementsCategory)]
		[Description("Elements to exclude children from sorting - description")]
		[DisplayName("Elements to exclude children from sorting")]
		public virtual string ElementNamesToExcludeChildrenFromSortingAlphabetically
		{
			get { return this.XmlFormatElement.ElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal; }
			set { this.XmlFormatElement.ElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal = value; }
		}

		[Category(_elementsCategory)]
		[Description("Elements to pin - description")]
		[DisplayName("Elements to pin")]
		public virtual string ElementNamesToPinFirst
		{
			get { return this.XmlFormatElement.ElementNamesToPinFirstInternal; }
			set { this.XmlFormatElement.ElementNamesToPinFirstInternal = value; }
		}

		[Category(_elementsCategory)]
		[Description("Element paths to exclude children from sorting - description")]
		[DisplayName("Element paths to exclude children from sorting")]
		public virtual string ElementPathsToExcludeChildrenFromSortingAlphabetically
		{
			get { return this.XmlFormatElement.ElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal; }
			set { this.XmlFormatElement.ElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal = value; }
		}

		[Category(_elementsCategory)]
		[Description("Element paths to involve children when sorting - description")]
		[DisplayName("Element paths to involve children when sorting")]
		public virtual string ElementPathsToInvolveChildElementWhenSortingElementsAlphabetically
		{
			get { return this.XmlFormatElement.ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal; }
			set { this.XmlFormatElement.ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal = value; }
		}

		[Category(_elementsCategory)]
		[Description("Elements alphabetical sort-direction - description")]
		[DisplayName("Elements alphabetical sort-direction")]
		public virtual ListSortDirection ElementsAlphabeticalSortDirection
		{
			get { return this.XmlFormatElement.ElementsAlphabeticalSortDirection; }
			set { this.XmlFormatElement.ElementsAlphabeticalSortDirection = value; }
		}

		protected internal virtual Dictionary<string, string> EscapeStringConversions
		{
			get { return _escapeStringConversions; }
		}

		[Category(_indentationCategory)]
		[DisplayName("Indent")]
		[Description("Indent - description")]
		public virtual bool Indent
		{
			get { return this.XmlFormatElement.Indent; }
			set { this.XmlFormatElement.Indent = value; }
		}

		[Category(_indentationCategory)]
		[DisplayName("Indentation-string")]
		[Description("Indentation-string - description")]
		public virtual string IndentString
		{
			get { return this.ResolveOutput(this.XmlFormatElement.IndentString); }
			set { this.XmlFormatElement.IndentString = this.ResolveInput(value); }
		}

		[Category(_elementsCategory)]
		[DisplayName("Involve attributes when sorting elements")]
		[Description("Involve attributes when sorting elements - description")]
		public virtual bool InvolveAttributesWhenSortingElementsAlphabetically
		{
			get { return this.XmlFormatElement.InvolveAttributesWhenSortingElementsAlphabetically; }
			set { this.XmlFormatElement.InvolveAttributesWhenSortingElementsAlphabetically = value; }
		}

		[Category(_indentationCategory)]
		[DisplayName("Minimum number of attributes for new line on attributes")]
		[Description("Minimum number of attributes for new line on attributes - description")]
		public virtual int MinimumNumberOfAttributesForNewLineOnAttributes
		{
			get { return this.XmlFormatElement.MinimumNumberOfAttributesForNewLineOnAttributes; }
			set { this.XmlFormatElement.MinimumNumberOfAttributesForNewLineOnAttributes = value; }
		}

		[Category(" ")]
		[Description("Name - description")]
		[DisplayName("Name")]
		public virtual string Name
		{
			get { return this.XmlFormatElement.Name; }
			set { this.XmlFormatElement.Name = value; }
		}

		[Category(_indentationCategory)]
		[Description("New line on attributes - description")]
		[DisplayName("New line on attributes")]
		public virtual bool NewLineOnAttributes
		{
			get { return this.XmlFormatElement.NewLineOnAttributes; }
			set { this.XmlFormatElement.NewLineOnAttributes = value; }
		}

		[Category(_indentationCategory)]
		[Description("New line string - description")]
		[DisplayName("New line string")]
		public virtual string NewLineString
		{
			get { return this.ResolveOutput(this.XmlFormatElement.NewLineString); }
			set { this.XmlFormatElement.NewLineString = this.ResolveInput(value); }
		}

		[Category(_commentsCategory)]
		[Description("Omit comments - description")]
		[DisplayName("Omit comments")]
		public virtual bool OmitComments
		{
			get { return this.XmlFormatElement.OmitComments; }
			set { this.XmlFormatElement.OmitComments = value; }
		}

		[Category(_xmlCategory)]
		[Description("Omit xml-declaration - description")]
		[DisplayName("Omit xml-declaration")]
		public virtual bool OmitXmlDeclaration
		{
			get { return this.XmlFormatElement.OmitXmlDeclaration; }
			set { this.XmlFormatElement.OmitXmlDeclaration = value; }
		}

		[Category(_attributesCategory)]
		[Description("Sort attributes alphabetically - description")]
		[DisplayName("Sort attributes alphabetically")]
		public virtual bool SortAttributesAlphabetically
		{
			get { return this.XmlFormatElement.SortAttributesAlphabetically; }
			set { this.XmlFormatElement.SortAttributesAlphabetically = value; }
		}

		[Category(_elementsCategory)]
		[Description("Sort elements alphabetically - description")]
		[DisplayName("Sort elements alphabetically")]
		public virtual bool SortElementsAlphabetically
		{
			get { return this.XmlFormatElement.SortElementsAlphabetically; }
			set { this.XmlFormatElement.SortElementsAlphabetically = value; }
		}

		protected internal virtual XmlFormatElement XmlFormatElement
		{
			get { return this._xmlFormatElement ?? (this._xmlFormatElement = new XmlFormatElement()); }
		}

		#endregion

		#region Methods

		protected internal virtual string ResolveInput(string value)
		{
			return value == null ? null : this.EscapeStringConversions.Aggregate(value, (current, escapeStringConversion) => current.Replace(escapeStringConversion.Value, escapeStringConversion.Key));
		}

		protected internal virtual string ResolveOutput(string value)
		{
			return value == null ? null : this.EscapeStringConversions.Aggregate(value, (current, escapeStringConversion) => current.Replace(escapeStringConversion.Key, escapeStringConversion.Value));
		}

		public override string ToString()
		{
			return this.Name;
		}

		#endregion

		#region Implicit operators

		public static implicit operator XmlFormatElement(XmlFormatSetting xmlFormatSetting)
		{
			return xmlFormatSetting == null ? null : xmlFormatSetting.XmlFormatElement;
		}

		public static implicit operator XmlFormatSetting(XmlFormatElement xmlFormatElement)
		{
			return xmlFormatElement == null ? null : new XmlFormatSetting {_xmlFormatElement = xmlFormatElement};
		}

		#endregion
	}
}