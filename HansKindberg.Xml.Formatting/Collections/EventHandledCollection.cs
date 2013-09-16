using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

// ReSharper disable CheckNamespace

namespace HansKindberg.Collections // ReSharper restore CheckNamespace
{
	public class EventHandledCollection<T> : ICollection<T>
	{
		#region Fields

		private readonly Collection<T> _collection;

		#endregion

		#region Constructors

		public EventHandledCollection()
		{
			this._collection = new Collection<T>();
		}

		public EventHandledCollection(IEnumerable<T> items)
		{
			if(items == null)
				throw new ArgumentNullException("items");

			this._collection = new Collection<T>(items.ToList());
		}

		#endregion

		#region Events

		public event EventHandler<CollectionEventArguments<T>> Added;
		public event EventHandler<CancelCollectionEventArguments<T>> Adding;
		public event EventHandler<CollectionEventArguments<T>> Cleared;
		public event EventHandler<CancelCollectionEventArguments<T>> Clearing;
		public event EventHandler<CollectionEventArguments<T>> Removed;
		public event EventHandler<CancelCollectionEventArguments<T>> Removing;

		#endregion

		#region Properties

		public virtual int Count
		{
			get { return this._collection.Count; }
		}

		public virtual bool IsReadOnly
		{
			get { return ((ICollection<T>) this._collection).IsReadOnly; }
		}

		#endregion

		#region Methods

		public virtual void Add(T item)
		{
			if(this.Adding != null)
			{
				CancelCollectionEventArguments<T> cancelCollectionEventArguments = new CancelCollectionEventArguments<T>(this) {Item = item};

				this.Adding(this, cancelCollectionEventArguments);

				if(cancelCollectionEventArguments.Cancel)
					return;
			}

			this._collection.Add(item);

			if(this.Added != null)
				this.Added(this, new CollectionEventArguments<T>(this) {Item = item});
		}

		public virtual void Clear()
		{
			if(this.Clearing != null)
			{
				CancelCollectionEventArguments<T> cancelCollectionEventArguments = new CancelCollectionEventArguments<T>(this);

				this.Clearing(this, cancelCollectionEventArguments);

				if(cancelCollectionEventArguments.Cancel)
					return;
			}

			this._collection.Clear();

			if(this.Cleared != null)
				this.Cleared(this, new CollectionEventArguments<T>(this));
		}

		public virtual bool Contains(T item)
		{
			return this._collection.Contains(item);
		}

		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			this._collection.CopyTo(array, arrayIndex);
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual bool Remove(T item)
		{
			if(this.Removing != null)
			{
				CancelCollectionEventArguments<T> cancelCollectionEventArguments = new CancelCollectionEventArguments<T>(this) {Item = item};

				this.Removing(this, cancelCollectionEventArguments);

				if(cancelCollectionEventArguments.Cancel)
					return false;
			}

			bool remove = this._collection.Remove(item);

			if(this.Removed != null)
				this.Removed(this, new CollectionEventArguments<T>(this) {Item = item, Remove = remove});

			return remove;
		}

		#endregion
	}
}