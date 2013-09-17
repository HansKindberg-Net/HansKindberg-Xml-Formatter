using System;
using System.Collections.Generic;
using System.Linq;

namespace HansKindberg.Xml.Linq.Comparison.Extensions
{
	public static class AlphabeticallyComparableExtension
	{
		#region Methods

		public static int? GetPinIndex(this IAlphabeticallyComparable alphabeticallyComparable, IEnumerable<string> namesToPinFirst)
		{
			if(alphabeticallyComparable == null)
				throw new ArgumentNullException("alphabeticallyComparable");

			if(namesToPinFirst != null)
			{
				IEnumerable<string> namesToPinFirstCopy = namesToPinFirst.ToArray();

				for(int i = 0; i < namesToPinFirstCopy.Count(); i++)
				{
					if(string.Equals(namesToPinFirstCopy.ElementAt(i), alphabeticallyComparable.Name, StringComparison.OrdinalIgnoreCase))
						return i;
				}
			}

			return null;
		}

		#endregion
	}
}