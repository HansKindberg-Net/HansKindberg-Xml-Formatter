using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public class XCommentWrapper : XNodeWrapper<XComment>, IXComment
	{
		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XCommentWrapper(XComment xComment) : base(xComment) {}

		#endregion

		#region Properties

		public virtual string Value
		{
			get { return this.XComment.Value; }
			set { this.XComment.Value = value; }
		}

		protected internal virtual XComment XComment
		{
			get { return this.XNode; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XCommentWrapper FromXComment(XComment xComment)
		{
			return xComment;
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XCommentWrapper(XComment xComment)
		{
			return xComment != null ? new XCommentWrapper(xComment) : null;
		}

		#endregion
	}
}