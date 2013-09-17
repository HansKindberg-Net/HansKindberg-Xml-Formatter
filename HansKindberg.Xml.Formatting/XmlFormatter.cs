using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using HansKindberg.Xml.Linq;

namespace HansKindberg.Xml.Formatting
{
	public class XmlFormatter : IXmlFormatter
	{
		#region Fields

		private readonly IComparer<IXAttribute> _xmlAttributeComparer;
		private readonly IXmlFormat _xmlFormat;
		private readonly IComparer<IXNode> _xmlNodeComparer;
		private readonly IXmlParser _xmlParser;

		#endregion

		#region Constructors

		public XmlFormatter(IXmlFormat xmlFormat, IXmlParser xmlParser, IComparer<IXNode> xmlNodeComparer, IComparer<IXAttribute> xmlAttributeComparer)
		{
			if(xmlFormat == null)
				throw new ArgumentNullException("xmlFormat");

			if(xmlParser == null)
				throw new ArgumentNullException("xmlParser");

			if(xmlNodeComparer == null)
				throw new ArgumentNullException("xmlNodeComparer");

			if(xmlAttributeComparer == null)
				throw new ArgumentNullException("xmlAttributeComparer");

			this._xmlAttributeComparer = xmlAttributeComparer;
			this._xmlFormat = xmlFormat;
			this._xmlNodeComparer = xmlNodeComparer;
			this._xmlParser = xmlParser;
		}

		#endregion

		#region Properties

		protected internal virtual bool FormatAttributes
		{
			get { return this.XmlFormat.AttributeNamesToCorrectCommaSeparatedValuesFor != null && this.XmlFormat.AttributeNamesToCorrectCommaSeparatedValuesFor.Any(); }
		}

		protected internal virtual bool FormatComments
		{
			get { return !this.XmlFormat.OmitComments && this.XmlFormat.CommentFormat != XmlCommentFormat.None; }
		}

		protected internal virtual bool FormatElements
		{
			get { return false; }
		}

		protected internal virtual IComparer<IXAttribute> XmlAttributeComparer
		{
			get { return this._xmlAttributeComparer; }
		}

		protected internal virtual IXmlFormat XmlFormat
		{
			get { return this._xmlFormat; }
		}

		protected internal virtual IComparer<IXNode> XmlNodeComparer
		{
			get { return this._xmlNodeComparer; }
		}

		protected internal virtual IXmlParser XmlParser
		{
			get { return this._xmlParser; }
		}

		#endregion

		#region Methods

		protected internal virtual void CorrectCommaSeparatedValue(IXAttribute attribute)
		{
			if(attribute == null)
				throw new ArgumentNullException("attribute");

			var valueParts = (attribute.Value ?? string.Empty).Split(",".ToCharArray());

			for(int i = 0; i < valueParts.Length; i++)
			{
				valueParts[i] = valueParts[i].Trim();
			}

			attribute.Value = string.Join(", ", valueParts);
		}

		public virtual string Format(string xml)
		{
			if(xml == null)
				throw new ArgumentNullException("xml");

			IXDocument xmlDocument = this.XmlParser.Parse(xml);

			if(this.FormatAttributes)
			{
				foreach(var attribute in xmlDocument.Descendants.SelectMany(descendant => descendant.Attributes))
				{
					this.FormatAttribute(attribute);
				}
			}

			if(this.FormatComments)
			{
				foreach(var comment in xmlDocument.DescendantNodes.OfType<IXComment>())
				{
					this.FormatComment(comment);
				}
			}

			if(this.FormatElements)
			{
				foreach(var element in xmlDocument.Descendants)
				{
					this.FormatElement(element);
				}
			}

			xmlDocument.Sort(this.XmlNodeComparer, this.XmlAttributeComparer);

			StringBuilder output = new StringBuilder();

			this.WriteXmlDocument(xmlDocument, output);

			return output.ToString();
		}

		public virtual void FormatAttribute(IXAttribute xmlAttribute)
		{
			if(xmlAttribute == null)
				throw new ArgumentNullException("xmlAttribute");

			if(this.XmlFormat.AttributeNamesToCorrectCommaSeparatedValuesFor != null && this.XmlFormat.AttributeNamesToCorrectCommaSeparatedValuesFor.Contains(xmlAttribute.Name))
				this.CorrectCommaSeparatedValue(xmlAttribute);
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual void FormatComment(IXComment xmlComment)
		{
			if(this.XmlFormat.CommentFormat == XmlCommentFormat.None)
				return;

			if(xmlComment == null)
				throw new ArgumentNullException("xmlComment");

			if(this.XmlFormat.CommentFormat == XmlCommentFormat.SingleLine || this.XmlFormat.CommentFormat == XmlCommentFormat.SingleLineOrAsXml)
			{
				string xmlCommentValue = xmlComment.Value.Trim();

				xmlCommentValue = xmlCommentValue.Replace(Environment.NewLine, " ");
				xmlCommentValue = xmlCommentValue.Replace("\n", " ");
				xmlCommentValue = xmlCommentValue.Replace("\r", " ");
				xmlCommentValue = xmlCommentValue.Replace("\t", " ");

				while(xmlCommentValue.IndexOf("  ", StringComparison.Ordinal) >= 0)
				{
					xmlCommentValue = xmlCommentValue.Replace("  ", " ");
				}

				bool unTrim = true;

				if(this.XmlFormat.CommentFormat == XmlCommentFormat.SingleLineOrAsXml)
				{
					try
					{
						string commentAsXml = "<root>" + xmlCommentValue + "</root>";
						IXDocument xmlDocument = this.XmlParser.Parse(commentAsXml);

						IEnumerable<IXNode> nodes = xmlDocument.Root.Nodes.ToArray();

						if(nodes.Any() && (nodes.Count() > 1 || !(nodes.ElementAt(0) is IXText)))
						{
							commentAsXml = this.Format(commentAsXml);
							commentAsXml = commentAsXml.Substring(6, commentAsXml.Length - 13);
							xmlCommentValue = commentAsXml;

							if(nodes.Count() == 1)
							{
								IXElement element = nodes.ElementAt(0) as IXElement;

								if(element != null && element.Nodes.Any())
									unTrim = false;
								else
									xmlCommentValue = xmlCommentValue.Trim();
							}
							else
							{
								unTrim = false;
							}
						}
					}
					catch(Exception exception)
					{
						Debug.WriteLine(exception.Message);
					}
				}

				if(unTrim)
					xmlCommentValue = " " + xmlCommentValue + (!string.IsNullOrEmpty(xmlCommentValue) ? " " : string.Empty);

				xmlComment.Value = xmlCommentValue;
			}
		}

		public virtual void FormatElement(IXElement xmlElement) {}

		protected internal virtual void WriteIndent(IXNode xmlNode, StringBuilder output)
		{
			if(xmlNode == null)
				throw new ArgumentNullException("xmlNode");

			this.WriteIndent(xmlNode.Level, output);
		}

		protected internal virtual void WriteIndent(int indentLevel, StringBuilder output)
		{
			if(!this.XmlFormat.Indent)
				return;

			if(output == null)
				throw new ArgumentNullException("output");

			if(!string.IsNullOrEmpty(output.ToString()))
				output.Append(this.XmlFormat.NewLineString);

			for(int i = indentLevel; i > 0; i--)
			{
				output.Append(this.XmlFormat.IndentString);
			}
		}

		protected internal virtual void WriteXmlComment(IXComment xmlComment, StringBuilder output)
		{
			if(xmlComment == null)
				throw new ArgumentNullException("xmlComment");

			if(output == null)
				throw new ArgumentNullException("output");

			if(this.FormatComments && this.XmlFormat.CommentFormat == XmlCommentFormat.SingleLineOrAsXml)
			{
				var xmlCommmentLines = xmlComment.ToString().Split(new[] {this.XmlFormat.NewLineString}, StringSplitOptions.None);

				foreach(string xmlCommentLine in xmlCommmentLines)
				{
					this.WriteIndent(xmlComment, output);
					output.Append(xmlCommentLine.TrimEnd(" ".ToCharArray()));
				}

				return;
			}

			this.WriteIndent(xmlComment, output);

			output.Append(xmlComment.ToString());
		}

		protected internal virtual void WriteXmlDeclaration(IXDeclaration xmlDeclaration, StringBuilder output)
		{
			if(xmlDeclaration == null || this.XmlFormat.OmitXmlDeclaration)
				return;

			if(output == null)
				throw new ArgumentNullException("output");

			output.Append(xmlDeclaration.ToString());
		}

		protected internal virtual void WriteXmlDocument(IXDocument xmlDocument, StringBuilder output)
		{
			if(xmlDocument == null)
				throw new ArgumentNullException("xmlDocument");

			if(output == null)
				throw new ArgumentNullException("output");

			this.WriteXmlDeclaration(xmlDocument.Declaration, output);

			foreach(var xmlNode in xmlDocument.Nodes)
			{
				this.WriteXmlNode(xmlNode, output);
			}
		}

		protected internal virtual void WriteXmlElement(IXElement xmlElement, StringBuilder output)
		{
			if(xmlElement == null)
				throw new ArgumentNullException("xmlElement");

			if(output == null)
				throw new ArgumentNullException("output");

			this.WriteIndent(xmlElement, output);

			output.Append("<" + xmlElement.Name);

			bool newLineOnAttributes = this.XmlFormat.Indent && this.XmlFormat.NewLineOnAttributes && xmlElement.Attributes.Count() > this.XmlFormat.MinimumNumberOfAttributesForNewLineOnAttributes;

			foreach(IXAttribute xmlAttribute in xmlElement.Attributes)
			{
				if(newLineOnAttributes)
					this.WriteIndent(xmlElement.Level + 1, output);
				else
					output.Append(" ");

				output.Append(xmlAttribute.Name + "=" + "\"" + xmlAttribute.Value + "\"");
			}

			if(newLineOnAttributes)
				this.WriteIndent(xmlElement.Level, output);

			if(this.XmlFormat.CloseEmptyElements && !xmlElement.Nodes.Any() && string.IsNullOrEmpty(xmlElement.Value))
			{
				if(!newLineOnAttributes)
					output.Append(" ");

				output.Append("/>");
				return;
			}

			output.Append(">");

			if(!xmlElement.Nodes.Any())
			{
				//if (!this.XmlFormat.OmitComments)
				//{
				//	XmlDocument xmlDocument = new XmlDocument();
				//	xmlDocument.LoadXml(element.ToString());

				//	// ReSharper disable PossibleNullReferenceException
				//	output.Append(xmlDocument.DocumentElement.InnerXml);
				//	// ReSharper restore PossibleNullReferenceException
				//}
				//else
				//{
				//	output.Append(element.Value);
				//}

				output.Append(xmlElement.Value);

				if(newLineOnAttributes)
					this.WriteIndent(xmlElement.Level, output);
			}
			else
			{
				foreach(IXNode xmlNode in xmlElement.Nodes)
				{
					this.WriteXmlNode(xmlNode, output);
				}

				this.WriteIndent(xmlElement.Level, output);
			}

			output.Append("</" + xmlElement.Name + ">");
		}

		protected internal virtual void WriteXmlNode(IXNode xmlNode, StringBuilder output)
		{
			if(xmlNode == null)
				throw new ArgumentNullException("xmlNode");

			IXComment xmlComment = xmlNode as IXComment;
			if(xmlComment != null)
			{
				this.WriteXmlComment(xmlComment, output);
				return;
			}

			IXElement xmlElement = xmlNode as IXElement;
			if(xmlElement != null)
			{
				this.WriteXmlElement(xmlElement, output);
				return;
			}

			if(output == null)
				throw new ArgumentNullException("output");

			this.WriteIndent(xmlNode, output);

			output.Append(xmlNode.ToString());
		}

		#endregion
	}
}