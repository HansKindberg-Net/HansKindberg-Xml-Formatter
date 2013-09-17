using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq.Extensions
{
	public static class XAttributeExtension
	{
		#region Methods

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

		#endregion
	}
}