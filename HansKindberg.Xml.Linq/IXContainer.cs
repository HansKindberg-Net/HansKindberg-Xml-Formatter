using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Xml.Linq
{
	public interface IXContainer : IXNode
	{
		#region Properties

		IEnumerable<IXNode> DescendantNodes { get; }
		IEnumerable<IXElement> Descendants { get; }
		IEnumerable<IXNode> Nodes { get; }

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		void Sort(IComparer<IXNode> xNodeComparer, IComparer<IXAttribute> xAttributeComparer);

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		void SortNodes(IComparer<IXNode> xNodeComparer);

		#endregion
	}
}