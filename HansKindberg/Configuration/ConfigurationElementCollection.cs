using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace HansKindberg.Configuration
{
	[Obsolete("This assembly will later exist at https://github.com/HansKindberg-Net/HansKindberg, change the references to that NuGet-package later.")]
	public abstract class ConfigurationElementCollection<T> : ConfigurationElementCollection, IList<T> where T : ConfigurationElement, new()
	{
		#region Properties

		public new virtual bool IsReadOnly
		{
			get { return this.IsReadOnly(); }
		}

		public virtual T this[int index]
		{
			get { return (T) this.BaseGet(index); }
			set
			{
				this.BaseRemoveAt(index);
				this.BaseAdd(index, value);
			}
		}

		#endregion

		#region Methods

		public virtual void Add(T item)
		{
			this.BaseAdd(item);
		}

		public virtual void Clear()
		{
			this.BaseClear();
		}

		public virtual bool Contains(T item)
		{
			return this.BaseIndexOf(item) >= 0;
		}

		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			// ReSharper disable CoVariantArrayConversion
			base.CopyTo(array, arrayIndex);
			// ReSharper restore CoVariantArrayConversion
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new T();
		}

		public new virtual IEnumerator<T> GetEnumerator()
		{
			// ReSharper disable LoopCanBeConvertedToQuery
			foreach(ConfigurationElement item in (IEnumerable) this)
			{
				yield return (T) item;
			}
			// ReSharper restore LoopCanBeConvertedToQuery
		}

		public virtual int IndexOf(T item)
		{
			return this.BaseIndexOf(item);
		}

		public virtual void Insert(int index, T item)
		{
			this.BaseAdd(index, item);
		}

		public virtual bool Remove(T item)
		{
			if(!this.Contains(item))
				return false;

			this.BaseRemove(this.GetElementKey(item));
			return true;
		}

		public virtual void RemoveAt(int index)
		{
			this.BaseRemoveAt(index);
		}

		#endregion
	}
}