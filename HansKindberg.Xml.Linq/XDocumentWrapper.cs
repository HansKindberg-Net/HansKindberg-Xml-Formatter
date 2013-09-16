using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public class XDocumentWrapper : XContainerWrapper<XDocument>, IXDocument
	{
		#region Fields

		private ValueContainer<IXDeclaration> _declaration;
		private ValueContainer<IXElement> _root;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XDocumentWrapper(XDocument xDocument) : base(xDocument) {}

		#endregion

		#region Properties

		public virtual IXDeclaration Declaration
		{
			get
			{
				if(this._declaration == null)
					this._declaration = new ValueContainer<IXDeclaration>((XDeclarationWrapper) this.XDocument.Declaration);

				return this._declaration.Value;
			}
		}

		public virtual IXElement Root
		{
			get
			{
				if(this._root == null)
					this._root = new ValueContainer<IXElement>((XElementWrapper) this.XDocument.Root);

				return this._root.Value;
			}
		}

		protected internal virtual XDocument XDocument
		{
			get { return this.XContainer; }
		}

		#endregion

		#region Methods

		protected internal override void ClearState()
		{
			base.ClearState();

			this._declaration = null;
			this._root = null;
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XDocumentWrapper FromXDocument(XDocument xDocument)
		{
			return xDocument;
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XDocumentWrapper(XDocument xDocument)
		{
			return xDocument != null ? new XDocumentWrapper(xDocument) : null;
		}

		#endregion
	}
}