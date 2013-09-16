using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options
{
	public class XmlFormatSettingCollection : IList<XmlFormatSetting>, IList
	{
		#region Fields

		private readonly List<XmlFormatSetting> _list;

		#endregion

		#region Constructors

		public XmlFormatSettingCollection()
		{
			this._list = new List<XmlFormatSetting>();
		}

		public XmlFormatSettingCollection(IEnumerable<XmlFormatSetting> collection) : this()
		{
			this.AddRangeInternal(collection);
		}

		#endregion

		#region Properties

		public virtual int Count
		{
			get { return this._list.Count; }
		}

		public virtual bool IsFixedSize
		{
			get { return ((IList) this._list).IsFixedSize; }
		}

		public virtual bool IsReadOnly
		{
			get { return ((IList) this._list).IsReadOnly; }
		}

		public virtual bool IsSynchronized
		{
			get { return ((IList) this._list).IsSynchronized; }
		}

		public virtual XmlFormatSetting this[int index]
		{
			get { return this._list[index]; }
			set { this._list[index] = value; }
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { this[index] = (XmlFormatSetting) value; }
		}

		public virtual object SyncRoot
		{
			get { return ((IList) this._list).SyncRoot; }
		}

		#endregion

		#region Methods

		public virtual void Add(XmlFormatSetting item)
		{
			this.AddInternal(item);
		}

		public virtual int Add(object value)
		{
			this.Add((XmlFormatSetting) value);
			return this.Count - 1;
		}

		protected internal void AddInternal(XmlFormatSetting item)
		{
			this.ValidateAdd(item);
			this._list.Add(item);
		}

		public virtual void AddRange(IEnumerable<XmlFormatSetting> collection)
		{
			this.AddRangeInternal(collection);
		}

		protected internal void AddRangeInternal(IEnumerable<XmlFormatSetting> collection)
		{
			// ReSharper disable PossibleMultipleEnumeration
			this.ValidateAddRange(collection);
			this._list.AddRange(collection);
			// ReSharper restore PossibleMultipleEnumeration
		}

		public virtual void Clear()
		{
			this._list.Clear();
		}

		public virtual bool Contains(object value)
		{
			return this.Contains((XmlFormatSetting) value);
		}

		public virtual bool Contains(XmlFormatSetting item)
		{
			return this._list.Contains(item);
		}

		protected internal bool Contains(string name)
		{
			return this._list.Any(xmlFormatSetting => string.Equals(xmlFormatSetting.Name, name, StringComparison.OrdinalIgnoreCase));
		}

		public virtual void CopyTo(Array array, int index)
		{
			((IList) this._list).CopyTo(array, index);
		}

		public virtual void CopyTo(XmlFormatSetting[] array, int arrayIndex)
		{
			this._list.CopyTo(array, arrayIndex);
		}

		public virtual IEnumerator<XmlFormatSetting> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual int IndexOf(object value)
		{
			return this.IndexOf((XmlFormatSetting) value);
		}

		public virtual int IndexOf(XmlFormatSetting item)
		{
			return this._list.IndexOf(item);
		}

		public virtual void Insert(int index, object value)
		{
			this.Insert(index, (XmlFormatSetting) value);
		}

		public virtual void Insert(int index, XmlFormatSetting item)
		{
			this._list.Insert(index, item);
		}

		public virtual void Remove(object value)
		{
			this.Remove((XmlFormatSetting) value);
		}

		public virtual bool Remove(XmlFormatSetting item)
		{
			return this._list.Remove(item);
		}

		public virtual void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		protected internal void ThrowArgumentExceptionIfNameAlreadyExists(string name)
		{
			if(this.Contains(name))
				throw new ArgumentException("An item with the same key has already been added.");
		}

		protected internal void ValidateAdd(XmlFormatSetting item)
		{
			if(item == null)
				throw new ArgumentNullException("item");

			this.ThrowArgumentExceptionIfNameAlreadyExists(item.Name);
		}

		protected internal void ValidateAddRange(IEnumerable<XmlFormatSetting> collection)
		{
			if(collection == null)
				throw new ArgumentNullException("collection");

			foreach(var xmlFormatSetting in collection)
			{
				this.ValidateAdd(xmlFormatSetting);
			}
		}

		#endregion
	}
}