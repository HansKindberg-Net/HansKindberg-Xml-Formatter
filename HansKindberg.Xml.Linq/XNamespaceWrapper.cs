using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public class XNamespaceWrapper : IXNamespace
	{
		#region Fields

		private readonly XNamespace _xNamespace;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XNamespaceWrapper(XNamespace xNamespace)
		{
			if(xNamespace == null)
				throw new ArgumentNullException("xNamespace");

			this._xNamespace = xNamespace;
		}

		#endregion

		#region Properties

		public virtual string NamespaceName
		{
			get { return this.XNamespace.NamespaceName; }
		}

		protected internal virtual XNamespace XNamespace
		{
			get { return this._xNamespace; }
		}

		#endregion

		#region Methods

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
				return true;

			XNamespaceWrapper xNamespaceWrapper = obj as XNamespaceWrapper;

			return xNamespaceWrapper != null && this.XNamespace.Equals(xNamespaceWrapper.XNamespace);
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XNamespaceWrapper FromXNamespace(XNamespace xNamespace)
		{
			return xNamespace;
		}

		public override int GetHashCode()
		{
			return this.XNamespace.GetHashCode();
		}

		public virtual IXName GetName(string localName)
		{
			return (XNameWrapper) this.XNamespace.GetName(localName);
		}

		public override string ToString()
		{
			return this.XNamespace.ToString();
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XNamespaceWrapper(XNamespace xNamespace)
		{
			return xNamespace != null ? new XNamespaceWrapper(xNamespace) : null;
		}

		#endregion
	}
}