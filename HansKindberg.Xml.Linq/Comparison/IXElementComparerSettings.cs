using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HansKindberg.Xml.Linq.Comparison
{
	public interface IXElementComparerSettings
	{
		#region Properties

		ICollection<int> ElementLevelsToExcludeFromSortingAlphabetically { get; }
		StringComparison ElementNameComparison { get; set; }
		ICollection<string> ElementNamesToExcludeChildrenFromSortingAlphabetically { get; }
		ICollection<string> ElementNamesToPinFirst { get; }
		ICollection<string> ElementPathsToExcludeChildrenFromSortingAlphabetically { get; }
		ICollection<string> ElementPathsToInvolveChildElementWhenSortingElementsAlphabetically { get; }
		ListSortDirection ElementsAlphabeticalSortDirection { get; set; }
		bool InvolveAttributesWhenSortingElementsAlphabetically { get; set; }
		bool SortElementsAlphabetically { get; set; }

		#endregion
	}
}