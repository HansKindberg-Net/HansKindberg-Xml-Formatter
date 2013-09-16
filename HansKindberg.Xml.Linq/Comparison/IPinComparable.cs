using System.Collections.Generic;

namespace HansKindberg.Xml.Linq.Comparison
{
	public interface IPinComparable
	{
		#region Methods

		int? GetPinIndex(IEnumerable<string> namesToPinFirst);

		#endregion
	}
}