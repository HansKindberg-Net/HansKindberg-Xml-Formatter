using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq.Extensions
{
	public static class XNodeExtension
	{
		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static int Index(this XNode xNode)
		{
			if(xNode == null)
				throw new ArgumentNullException("xNode");

			return xNode.NodesBeforeSelf().Count();
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public static int Level(this XNode xNode)
		{
			if(xNode == null)
				throw new ArgumentNullException("xNode");

			return xNode.Ancestors().Count();
		}

		#endregion
	}
}