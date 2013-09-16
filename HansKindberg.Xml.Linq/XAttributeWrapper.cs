using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Extensions;

namespace HansKindberg.Xml.Linq
{
	public class XAttributeWrapper : XObjectWrapper<XAttribute>, IXAttribute
	{
		#region Fields

		private int? _index;
		private ValueContainer<IXName> _name;
		private ValueContainer<string> _path;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XAttributeWrapper(XAttribute xAttribute) : base(xAttribute) {}

		#endregion

		#region Properties

		public virtual int Index
		{
			get
			{
				if(!this._index.HasValue)
					this._index = this.XAttribute.Index();

				return this._index.Value;
			}
		}

		public virtual IXName Name
		{
			get
			{
				if(this._name == null)
					this._name = new ValueContainer<IXName>((XNameWrapper) this.XAttribute.Name);

				return this._name.Value;
			}
		}

		public virtual string Path
		{
			get
			{
				if(this._path == null)
					this._path = new ValueContainer<string>(this.XAttribute.Path());

				return this._path.Value;
			}
		}

		public virtual string Value
		{
			get { return this.XAttribute.Value; }
			set { this.XAttribute.Value = value; }
		}

		protected internal virtual XAttribute XAttribute
		{
			get { return this.XObject; }
		}

		#endregion

		#region Methods

		protected internal override void ClearState()
		{
			base.ClearState();

			this._index = null;
			this._name = null;
			this._path = null;
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static XAttributeWrapper FromXAttribute(XAttribute xAttribute)
		{
			return xAttribute;
		}

		public virtual int? GetPinIndex(IEnumerable<string> namesToPinFirst)
		{
			return this.XAttribute.GetPinIndex(namesToPinFirst);
		}

		#endregion

		#region Implicit operators

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static implicit operator XAttributeWrapper(XAttribute xAttribute)
		{
			return xAttribute != null ? new XAttributeWrapper(xAttribute) : null;
		}

		#endregion
	}
}