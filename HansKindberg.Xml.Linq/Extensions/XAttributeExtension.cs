using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq.Extensions
{
	public static class XAttributeExtension
	{
		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static int? GetPinIndex(this XAttribute xAttribute, IEnumerable<string> attributeNamesToPinFirst)
		{
			if(xAttribute == null)
				throw new ArgumentNullException("xAttribute");

			if(attributeNamesToPinFirst != null)
			{
				IEnumerable<string> attributeNamesToPinFirstCopy = attributeNamesToPinFirst.ToArray();

				for(int i = 0; i < attributeNamesToPinFirstCopy.Count(); i++)
				{
					if(string.Equals(attributeNamesToPinFirstCopy.ElementAt(i), xAttribute.Name.LocalName, StringComparison.OrdinalIgnoreCase))
						return i;
				}
			}

			return null;
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static int Index(this XAttribute xAttribute)
		{
			if(xAttribute == null)
				throw new ArgumentNullException("xAttribute");

			int index = 0;

			while(xAttribute.PreviousAttribute != null)
			{
				index++;
				xAttribute = xAttribute.PreviousAttribute;
			}

			return index;
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static string Path(this XAttribute xAttribute)
		{
			if(xAttribute == null)
				throw new ArgumentNullException("xAttribute");

			return (xAttribute.Parent != null ? xAttribute.Parent.Path() : string.Empty) + "@" + xAttribute.Name.LocalName;
		}

		#endregion
	}
}