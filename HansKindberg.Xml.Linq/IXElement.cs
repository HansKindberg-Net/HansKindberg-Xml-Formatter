using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Xml.Linq.Comparison;

namespace HansKindberg.Xml.Linq
{
	public interface IXElement : IXContainer, IAlphabeticallyComparable, IPinComparable
	{
		#region Properties

		IEnumerable<IXNode> AssociatedNodes { get; }
		IEnumerable<IXAttribute> Attributes { get; }
		string Path { get; }
		new string Value { get; set; }
		IXName XName { get; }

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		void SortAttributes(IComparer<IXAttribute> xAttributeComparer);

		#endregion
	}
}