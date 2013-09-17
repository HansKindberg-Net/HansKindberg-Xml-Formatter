using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public class XTextWrapper : XNodeWrapper<XText>, IXText
	{
		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XTextWrapper(XText xText) : base(xText) {}

		#endregion

		#region Properties

		public virtual string Value
		{
			get { return this.XText.Value; }
			set { this.XText.Value = value; }
		}

		protected internal virtual XText XText
		{
			get { return this.XNode; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XTextWrapper FromXText(XText xText)
		{
			return xText;
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XTextWrapper(XText xText)
		{
			return xText != null ? new XTextWrapper(xText) : null;
		}

		#endregion
	}
}