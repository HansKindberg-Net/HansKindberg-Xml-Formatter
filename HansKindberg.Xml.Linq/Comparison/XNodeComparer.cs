using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Collections.Generic.Extensions;

namespace HansKindberg.Xml.Linq.Comparison
{
	public class XNodeComparer : IComparer<IXNode>, IComparer<XNode>
	{
		#region Fields

		private readonly IComparer<IXElement> _xElementComparer;
		private readonly IXNodeComparerSettings _xNodeComparerSettings;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XNodeComparer(IXNodeComparerSettings xNodeComparerSettings, IComparer<IXElement> xElementComparer)
		{
			if(xNodeComparerSettings == null)
				throw new ArgumentNullException("xNodeComparerSettings");

			if(xElementComparer == null)
				throw new ArgumentNullException("xElementComparer");

			this._xElementComparer = xElementComparer;
			this._xNodeComparerSettings = xNodeComparerSettings;
		}

		#endregion

		#region Properties

		protected internal virtual IComparer<IXElement> XElementComparer
		{
			get { return this._xElementComparer; }
		}

		protected internal virtual IXNodeComparerSettings XNodeComparerSettings
		{
			get { return this._xNodeComparerSettings; }
		}

		#endregion

		#region Methods

		public virtual int Compare(IXNode x, IXNode y)
		{
			if(ReferenceEquals(x, y))
				return 0;

			if(x == null)
				return -1;

			if(y == null)
				return 1;

			IXElement firstXElement = x as IXElement;
			IXElement secondXElement = y as IXElement;

			if(firstXElement != null)
			{
				if(secondXElement != null)
					return this.XElementComparer.Compare(firstXElement, secondXElement);

				if(y.AssociatedTo == null)
					return -1;

				if(firstXElement.Equals(y.AssociatedTo))
					return 1;

				return this.XElementComparer.Compare(firstXElement, y.AssociatedTo);
			}

			if(secondXElement != null)
			{
				if(x.AssociatedTo == null)
					return 1;

				if(secondXElement.Equals(x.AssociatedTo))
					return -1;

				return this.XElementComparer.Compare(x.AssociatedTo, secondXElement);
			}

			if(x.AssociatedTo == null)
			{
				if(y.AssociatedTo == null)
					return x.Index.CompareTo(y.Index);

				return 1;
			}

			if(y.AssociatedTo == null)
				return -1;

			if(x.AssociatedTo.Equals(y.AssociatedTo))
			{
				var associatedNodes = x.AssociatedTo.AssociatedNodes.ToArray();

				return associatedNodes.IndexOf(x).CompareTo(associatedNodes.IndexOf(y));
			}

			return this.XElementComparer.Compare(x.AssociatedTo, y.AssociatedTo);
		}

		public virtual int Compare(XNode x, XNode y)
		{
			return this.Compare((XNodeWrapper) x, (XNodeWrapper) y);
		}

		#endregion
	}
}