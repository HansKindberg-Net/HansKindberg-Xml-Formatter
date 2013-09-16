using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Extensions;

namespace HansKindberg.Xml.Linq
{
	public class XNodeWrapper : XNodeWrapper<XNode>
	{
		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XNodeWrapper(XNode xNode) : base(xNode) {}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XNodeWrapper FromXNode(XNode xNode)
		{
			return xNode;
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XNodeWrapper(XNode xNode)
		{
			return xNode != null ? new XNodeWrapper(xNode) : null;
		}

		#endregion
	}

	public abstract class XNodeWrapper<T> : XObjectWrapper<T>, IXNode where T : XNode
	{
		#region Fields

		private ValueContainer<IXElement> _associatedTo;
		private int? _index;
		private int? _level;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		protected XNodeWrapper(T xNode) : base(xNode) {}

		#endregion

		#region Properties

		public virtual IXElement AssociatedTo
		{
			get
			{
				if(this._associatedTo == null)
					this._associatedTo = new ValueContainer<IXElement>((XElementWrapper) this.XNode.NodesAfterSelf().OfType<XElement>().FirstOrDefault());

				return this._associatedTo.Value;
			}
		}

		public virtual int Index
		{
			get
			{
				if(!this._index.HasValue)
					this._index = this.XNode.Index();

				return this._index.Value;
			}
		}

		public virtual int Level
		{
			get
			{
				if(!this._level.HasValue)
					this._level = this.XNode.Level();

				return this._level.Value;
			}
		}

		protected internal virtual T XNode
		{
			get { return this.XObject; }
		}

		#endregion

		#region Methods

		protected internal override void ClearState()
		{
			base.ClearState();

			this._associatedTo = null;
			this._index = null;
			this._level = null;
		}

		public virtual void WriteTo(XmlWriter writer)
		{
			this.XNode.WriteTo(writer);
		}

		#endregion
	}
}