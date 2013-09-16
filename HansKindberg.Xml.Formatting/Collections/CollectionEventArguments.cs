using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable CheckNamespace

namespace HansKindberg.Collections // ReSharper restore CheckNamespace
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class CollectionEventArguments<T> : EventArgs
	{
		#region Fields

		private readonly ICollection<T> _items;

		#endregion

		#region Constructors

		public CollectionEventArguments(IEnumerable<T> items)
		{
			this._items = items != null ? new List<T>(items) : new List<T>();
		}

		#endregion

		#region Properties

		public virtual T Item { get; set; }

		public virtual ICollection<T> Items
		{
			get { return this._items; }
		}

		public virtual bool Remove { get; set; }

		#endregion
	}
}