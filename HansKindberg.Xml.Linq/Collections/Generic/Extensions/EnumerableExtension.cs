using System;
using System.Collections.Generic;

namespace HansKindberg.Xml.Linq.Collections.Generic.Extensions
{
	public static class EnumerableExtension
	{
		#region Methods

		public static int IndexOf<T>(this IEnumerable<T> enumerable, T value)
		{
			if(enumerable == null)
				throw new ArgumentNullException("enumerable");

			int index = -1;

			foreach(var item in enumerable)
			{
				index++;

				if(Equals(value, item))
					return index;
			}

			return index;
		}

		#endregion
	}
}