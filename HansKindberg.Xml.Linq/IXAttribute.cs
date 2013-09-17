using System.Diagnostics.CodeAnalysis;
using HansKindberg.Xml.Linq.Comparison;

namespace HansKindberg.Xml.Linq
{
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public interface IXAttribute : IXObject, IAlphabeticallyComparable, IIndexComparable, IPinComparable
	{
		#region Properties

		string Path { get; }
		new string Value { get; set; }
		IXName XName { get; }

		#endregion
	}
}