using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public class XNameWrapper : IXName
	{
		#region Fields

		private readonly XName _xName;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XNameWrapper(XName xName)
		{
			if(xName == null)
				throw new ArgumentNullException("xName");

			this._xName = xName;
		}

		#endregion

		#region Properties

		public virtual string LocalName
		{
			get { return this.XName.LocalName; }
		}

		public virtual IXNamespace Namespace
		{
			get { return (XNamespaceWrapper) this.XName.Namespace; }
		}

		public virtual string NamespaceName
		{
			get { return this.XName.NamespaceName; }
		}

		protected internal virtual XName XName
		{
			get { return this._xName; }
		}

		#endregion

		#region Methods

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
				return true;

			XNameWrapper xNameWrapper = obj as XNameWrapper;

			return xNameWrapper != null && this.XName.Equals(xNameWrapper.XName);
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XNameWrapper FromXName(XName xName)
		{
			return xName;
		}

		public override int GetHashCode()
		{
			return this.XName.GetHashCode();
		}

		public override string ToString()
		{
			return this.XName.ToString();
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XNameWrapper(XName xName)
		{
			return xName != null ? new XNameWrapper(xName) : null;
		}

		#endregion
	}
}