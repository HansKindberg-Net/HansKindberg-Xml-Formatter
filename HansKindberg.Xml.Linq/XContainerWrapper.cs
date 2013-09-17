using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public class XContainerWrapper : XContainerWrapper<XContainer>
	{
		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XContainerWrapper(XContainer xContainer) : base(xContainer) {}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XContainerWrapper FromXContainer(XContainer xContainer)
		{
			return xContainer;
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XContainerWrapper(XContainer xContainer)
		{
			return xContainer != null ? new XContainerWrapper(xContainer) : null;
		}

		#endregion
	}

	public abstract class XContainerWrapper<T> : XNodeWrapper<T>, IXContainer where T : XContainer
	{
		#region Fields

		private IEnumerable<IXNode> _descendantNodes;
		private IEnumerable<IXElement> _descendants;
		private IEnumerable<IXNode> _nodes;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		protected XContainerWrapper(T xContainer) : base(xContainer) {}

		#endregion

		#region Properties

		public override IXElement AssociatedTo
		{
			get { return null; }
		}

		public virtual IEnumerable<IXNode> DescendantNodes
		{
			get { return this._descendantNodes ?? (this._descendantNodes = this.ConvertNodes(this.XContainer.DescendantNodes())); }
		}

		public virtual IEnumerable<IXElement> Descendants
		{
			get { return this._descendants ?? (this._descendants = this.XContainer.Descendants().Select(descendant => (IXElement) (XElementWrapper) descendant)); }
		}

		public virtual IEnumerable<IXNode> Nodes
		{
			get { return this._nodes ?? (this._nodes = this.ConvertNodes(this.XContainer.Nodes())); }
		}

		protected internal virtual T XContainer
		{
			get { return this.XNode; }
		}

		#endregion

		#region Methods

		protected internal override void ClearState()
		{
			base.ClearState();

			this._descendants = null;
			this._descendantNodes = null;
			this._nodes = null;
		}

		protected internal virtual IEnumerable<IXNode> ConvertNodes(IEnumerable<XNode> nodes)
		{
			if(nodes == null)
				throw new ArgumentNullException("nodes");

			List<IXNode> nodeList = new List<IXNode>();

			foreach(var node in nodes)
			{
				XComment comment = node as XComment;
				if(comment != null)
				{
					nodeList.Add((XCommentWrapper) comment);
					continue;
				}

				XElement element = node as XElement;
				if(element != null)
				{
					nodeList.Add((XElementWrapper) element);
					continue;
				}

				XText text = node as XText;
				if(text != null)
				{
					nodeList.Add((XTextWrapper) text);
					continue;
				}

				nodeList.Add((XNodeWrapper) node);
			}

			return nodeList.ToArray();
		}

		public virtual void Sort(IComparer<IXNode> xNodeComparer, IComparer<IXAttribute> xAttributeComparer)
		{
			foreach(var descendant in this.Descendants)
			{
				descendant.SortAttributes(xAttributeComparer);
			}

			this.SortNodes(xNodeComparer);
		}

		public virtual void SortNodes(IComparer<IXNode> xNodeComparer)
		{
			if(xNodeComparer == null)
				throw new ArgumentNullException("xNodeComparer");

			List<IXNode> nodeList = this.Nodes.ToList();

			foreach(var container in nodeList.OfType<IXContainer>().ToArray())
			{
				container.SortNodes(xNodeComparer);
			}

			nodeList.Sort(xNodeComparer);

			List<XNode> sortedNodes = new List<XNode>();

			foreach(var node in nodeList)
			{
				InternalXObjectWrapper internalXObjectWrapper = node as InternalXObjectWrapper;

				if(internalXObjectWrapper != null)
				{
					XNode xNode = internalXObjectWrapper.InternalXObject as XNode;

					if(xNode != null)
					{
						sortedNodes.Add(xNode);
						continue;
					}
				}

				// ReSharper disable PossibleNullReferenceException
				sortedNodes.Add(XDocument.Parse(string.Format(CultureInfo.InvariantCulture, "<root>{0}</root>", node.ToString())).Root.Nodes().First());
				// ReSharper restore PossibleNullReferenceException
			}

			this.XContainer.ReplaceNodes(sortedNodes);
		}

		#endregion
	}
}