using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable CheckNamespace

namespace HansKindberg.Collections // ReSharper restore CheckNamespace
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class CancelCollectionEventArguments<T> : CollectionEventArguments<T>
	{
		#region Constructors

		public CancelCollectionEventArguments(ICollection<T> items) : base(items) {}

		#endregion

		#region Properties

		public virtual bool Cancel { get; set; }

		#endregion
	}
}