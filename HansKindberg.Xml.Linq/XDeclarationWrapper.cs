using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public class XDeclarationWrapper : IXDeclaration
	{
		#region Fields

		private ValueContainer<Encoding> _encoding;
		private XStandalone? _standalone;
		private readonly XDeclaration _xDeclaration;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XDeclarationWrapper(XDeclaration xDeclaration)
		{
			if(xDeclaration == null)
				throw new ArgumentNullException("xDeclaration");

			this._xDeclaration = xDeclaration;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		public XDeclarationWrapper(string version, Encoding encoding, XStandalone standalone)
		{
			this._xDeclaration = new XDeclaration(version, encoding != null ? encoding.WebName : null, standalone != XStandalone.Omit ? standalone.ToString().ToLowerInvariant() : null);
		}

		#endregion

		#region Properties

		public virtual Encoding Encoding
		{
			get
			{
				if(this._encoding == null)
					this._encoding = new ValueContainer<Encoding>(!string.IsNullOrEmpty(this.XDeclaration.Encoding) ? Encoding.GetEncoding(this.XDeclaration.Encoding) : null);

				return this._encoding.Value;
			}
		}

		public virtual XStandalone Standalone
		{
			get
			{
				if(this._standalone == null)
				{
					if(string.IsNullOrEmpty(this.XDeclaration.Standalone))
						this._standalone = XStandalone.Omit;
					else if(this.XDeclaration.Standalone.Equals(XStandalone.Yes.ToString(), StringComparison.OrdinalIgnoreCase))
						this._standalone = XStandalone.Yes;
					else if(this.XDeclaration.Standalone.Equals(XStandalone.No.ToString(), StringComparison.OrdinalIgnoreCase))
						this._standalone = XStandalone.No;
					else
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The value \"{0}\" is not a valid standalone value.", this.XDeclaration.Standalone));
				}

				return this._standalone.Value;
			}
		}

		public virtual string Version
		{
			get { return this.XDeclaration.Version; }
		}

		protected internal virtual XDeclaration XDeclaration
		{
			get { return this._xDeclaration; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XDeclarationWrapper FromXDeclaration(XDeclaration xDeclaration)
		{
			return xDeclaration;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		public override string ToString()
		{
			return this.XDeclaration.ToString().ToLowerInvariant();
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XDeclarationWrapper(XDeclaration xDeclaration)
		{
			return xDeclaration != null ? new XDeclarationWrapper(xDeclaration) : null;
		}

		#endregion
	}
}