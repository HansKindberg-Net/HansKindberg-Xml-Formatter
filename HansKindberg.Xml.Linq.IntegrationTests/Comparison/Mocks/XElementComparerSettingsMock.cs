using System;
using System.Collections.Generic;
using System.ComponentModel;
using HansKindberg.Xml.Linq.Comparison;

namespace HansKindberg.Xml.Linq.IntegrationTests.Comparison.Mocks
{
	public class XElementComparerSettingsMock : IXElementComparerSettings
	{
		#region Fields

		private readonly ICollection<int> _elementLevelsToExcludeFromSortingAlphabetically = new List<int>();
		private readonly ICollection<string> _elementNamesToExcludeChildrenFromSortingAlphabetically = new List<string>();
		private readonly ICollection<string> _elementNamesToPinFirst = new List<string>();
		private readonly ICollection<string> _elementPathsToExcludeChildrenFromSortingAlphabetically = new List<string>();
		private readonly ICollection<string> _elementPathsToInvolveChildElementWhenSortingElementsAlphabetically = new List<string>();

		#endregion

		#region Properties

		public virtual ICollection<int> ElementLevelsToExcludeFromSortingAlphabetically
		{
			get { return this._elementLevelsToExcludeFromSortingAlphabetically; }
		}

		public virtual StringComparison ElementNameComparison { get; set; }

		public virtual ICollection<string> ElementNamesToExcludeChildrenFromSortingAlphabetically
		{
			get { return this._elementNamesToExcludeChildrenFromSortingAlphabetically; }
		}

		public virtual ICollection<string> ElementNamesToPinFirst
		{
			get { return this._elementNamesToPinFirst; }
		}

		public virtual ICollection<string> ElementPathsToExcludeChildrenFromSortingAlphabetically
		{
			get { return this._elementPathsToExcludeChildrenFromSortingAlphabetically; }
		}

		public virtual ICollection<string> ElementPathsToInvolveChildElementWhenSortingElementsAlphabetically
		{
			get { return this._elementPathsToInvolveChildElementWhenSortingElementsAlphabetically; }
		}

		public virtual ListSortDirection ElementsAlphabeticalSortDirection { get; set; }
		public virtual bool InvolveAttributesWhenSortingElementsAlphabetically { get; set; }
		public virtual bool SortElementsAlphabetically { get; set; }

		#endregion
	}
}