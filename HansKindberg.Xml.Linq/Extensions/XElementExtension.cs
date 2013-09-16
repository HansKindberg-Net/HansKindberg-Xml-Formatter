using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq.Extensions
{
	public static class XElementExtension
	{
		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static int? GetPinIndex(this XElement xElement, IEnumerable<string> elementNamesToPinFirst)
		{
			if(xElement == null)
				throw new ArgumentNullException("xElement");

			if(elementNamesToPinFirst != null)
			{
				IEnumerable<string> elementNamesToPinFirstCopy = elementNamesToPinFirst.ToArray();

				for(int i = 0; i < elementNamesToPinFirstCopy.Count(); i++)
				{
					if(string.Equals(elementNamesToPinFirstCopy.ElementAt(i), xElement.Name.LocalName, StringComparison.OrdinalIgnoreCase))
						return i;
				}
			}

			return null;
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static string Path(this XElement xElement)
		{
			if(xElement == null)
				throw new ArgumentNullException("xElement");

			return (xElement.Parent != null ? xElement.Parent.Path() : string.Empty) + "/" + xElement.Name.LocalName;
		}

		#endregion
	}
}