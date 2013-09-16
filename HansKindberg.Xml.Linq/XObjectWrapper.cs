using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public class XObjectWrapper : XObjectWrapper<XObject>
	{
		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XObjectWrapper(XObject xObject) : base(xObject) {}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XObjectWrapper FromXObject(XObject xObject)
		{
			return xObject;
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XObjectWrapper(XObject xObject)
		{
			return xObject != null ? new XObjectWrapper(xObject) : null;
		}

		#endregion
	}

	public abstract class XObjectWrapper<T> : InternalXObjectWrapper, IXObject where T : XObject
	{
		#region Fields

		private ValueContainer<IXElement> _parent;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		protected XObjectWrapper(T xObject) : base(xObject)
		{
			xObject.Changed += this.OnXObjectChanged;
		}

		#endregion

		#region Properties

		public virtual XmlNodeType NodeType
		{
			get { return this.XObject.NodeType; }
		}

		public virtual IXElement Parent
		{
			get
			{
				if(this._parent == null)
					this._parent = new ValueContainer<IXElement>((XElementWrapper) this.XObject.Parent);

				return this._parent.Value;
			}
		}

		protected internal virtual T XObject
		{
			get { return (T) this.InternalXObject; }
		}

		#endregion

		#region Methods

		protected internal virtual void ClearState()
		{
			this._parent = null;
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
				return true;

			XObjectWrapper<T> xObjectWrapper = obj as XObjectWrapper<T>;

			return xObjectWrapper != null && this.XObject.Equals(xObjectWrapper.XObject);
		}

		public override int GetHashCode()
		{
			return this.XObject.GetHashCode();
		}

		public override string ToString()
		{
			return this.XObject.ToString();
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		protected internal virtual void OnXObjectChanged(object sender, XObjectChangeEventArgs xObjectChangeEventArgs)
		{
			this.ClearState();
		}

		#endregion
	}
}