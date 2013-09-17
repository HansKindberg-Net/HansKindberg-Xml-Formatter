using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Xml.Linq.Comparison
{
	public abstract class XComparer<T> where T : class, IAlphabeticallyComparable, IIndexComparable, IPinComparable
	{
		#region Methods

		protected internal virtual int CompareAlphabeticalValue(string firstAlphabeticalValue, string secondAlphabeticalValue, StringComparison alphabeticalComparison, ListSortDirection alphabeticalSortDirection)
		{
			return this.ResolveAlphabeticalCompare(string.Compare(firstAlphabeticalValue, secondAlphabeticalValue, alphabeticalComparison), alphabeticalSortDirection);
		}

		protected internal virtual int CompareByIndex(T firstItem, T secondItem)
		{
			if(ReferenceEquals(firstItem, secondItem))
				return 0;

			if(firstItem == null)
				return -1;

			if(secondItem == null)
				return 1;

			return firstItem.Index.CompareTo(secondItem.Index);
		}

		protected internal virtual int? CompareByName(T firstItem, T secondItem, StringComparison nameComparison, ListSortDirection alphabeticalSortDirection)
		{
			int? compare = null;

			if(this.SortAlphabetically(firstItem, secondItem))
				compare = this.CompareAlphabeticalValue(firstItem != null ? firstItem.Name : null, secondItem != null ? secondItem.Name : null, nameComparison, alphabeticalSortDirection);

			return compare;
		}

		protected internal virtual int? CompareByPinIndex(T firstItem, T secondItem, IEnumerable<string> namesToPinFirst)
		{
			if(ReferenceEquals(firstItem, secondItem))
				return 0;

			// ReSharper disable PossibleMultipleEnumeration
			int? firstPinIndex = firstItem != null ? firstItem.GetPinIndex(namesToPinFirst) : null;
			int? secondPinIndex = secondItem != null ? secondItem.GetPinIndex(namesToPinFirst) : null;
			// ReSharper restore PossibleMultipleEnumeration

			if(firstPinIndex.HasValue)
			{
				if(secondPinIndex.HasValue)
					return firstPinIndex.Value.CompareTo(secondPinIndex.Value);

				return -1;
			}

			if(secondPinIndex.HasValue)
				return 1;

			return null;
		}

		protected internal virtual int? CompareByValue(T firstItem, T secondItem, StringComparison valueComparison, ListSortDirection alphabeticalSortDirection)
		{
			int? compare = null;

			if(this.SortAlphabetically(firstItem, secondItem))
				compare = this.CompareAlphabeticalValue(firstItem != null ? firstItem.Value : null, secondItem != null ? secondItem.Value : null, valueComparison, alphabeticalSortDirection);

			return compare;
		}

		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "compare*-1")]
		protected internal virtual int ResolveAlphabeticalCompare(int compare, ListSortDirection alphabeticalSortDirection)
		{
			if(compare != 0 && alphabeticalSortDirection == ListSortDirection.Descending)
				return compare*-1;

			return compare;
		}

		protected internal abstract bool SortAlphabetically(T firstItem, T secondItem);

		#endregion
	}
}