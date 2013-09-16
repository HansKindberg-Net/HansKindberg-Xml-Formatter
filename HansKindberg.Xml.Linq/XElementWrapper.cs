using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Extensions;

namespace HansKindberg.Xml.Linq
{
	public class XElementWrapper : XContainerWrapper<XElement>, IXElement
	{
		#region Fields

		private IEnumerable<IXNode> _associatedNodes;
		private IEnumerable<IXAttribute> _attributes;
		private ValueContainer<IXName> _name;
		private ValueContainer<string> _path;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XElementWrapper(XElement xElement) : base(xElement) {}

		#endregion

		#region Properties

		public virtual IEnumerable<IXNode> AssociatedNodes
		{
			get
			{
				if(this._associatedNodes == null)
				{
					List<XNode> associatedNodeList = new List<XNode>();

					XNode previousNode = this.XElement.PreviousNode;

					while(previousNode != null && !(previousNode is XElement))
					{
						associatedNodeList.Insert(0, previousNode);
						previousNode = previousNode.PreviousNode;
					}

					this._associatedNodes = this.ConvertNodes(associatedNodeList).ToArray();
				}

				return this._associatedNodes;
			}
		}

		public virtual IEnumerable<IXAttribute> Attributes
		{
			get { return this._attributes ?? (this._attributes = this.XElement.Attributes().Select(attribute => (IXAttribute) (XAttributeWrapper) attribute)); }
		}

		public virtual IXName Name
		{
			get
			{
				if(this._name == null)
					this._name = new ValueContainer<IXName>((XNameWrapper) this.XElement.Name);

				return this._name.Value;
			}
		}

		public virtual string Path
		{
			get
			{
				if(this._path == null)
					this._path = new ValueContainer<string>(this.XElement.Path());

				return this._path.Value;
			}
		}

		public virtual string Value
		{
			get { return this.XElement.Value; }
			set { this.XElement.Value = value; }
		}

		protected internal virtual XElement XElement
		{
			get { return this.XNode; }
		}

		#endregion

		#region Methods

		protected internal override void ClearState()
		{
			base.ClearState();

			this._associatedNodes = null;
			this._attributes = null;
			this._name = null;
			this._path = null;
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XElementWrapper FromXElement(XElement xElement)
		{
			return xElement;
		}

		public virtual int? GetPinIndex(IEnumerable<string> namesToPinFirst)
		{
			return this.XElement.GetPinIndex(namesToPinFirst);
		}

		public override void Sort(IComparer<IXNode> xNodeComparer, IComparer<IXAttribute> xAttributeComparer)
		{
			this.SortAttributes(xAttributeComparer);

			base.Sort(xNodeComparer, xAttributeComparer);
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Xml.Linq.XDocument.Parse(System.String)")]
		public virtual void SortAttributes(IComparer<IXAttribute> xAttributeComparer)
		{
			if(xAttributeComparer == null)
				throw new ArgumentNullException("xAttributeComparer");

			List<IXAttribute> attributeList = this.Attributes.ToList();
			attributeList.Sort(xAttributeComparer);
			string attributesString = string.Empty;
			foreach(var attribute in attributeList)
			{
				attributesString += " ";
				attributesString += attribute.ToString();
			}
			string xml = string.Format(CultureInfo.InvariantCulture, "<element{0}></element>", attributesString);

			// ReSharper disable PossibleNullReferenceException
			this.XElement.ReplaceAttributes(XDocument.Parse(xml).Root.Attributes());
			// ReSharper restore PossibleNullReferenceException
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XElementWrapper(XElement xElement)
		{
			return xElement != null ? new XElementWrapper(xElement) : null;
		}

		#endregion
	}
}