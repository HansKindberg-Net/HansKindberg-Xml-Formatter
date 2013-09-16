using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HansKindberg.Xml.Linq.Comparison
{
	public interface IXAttributeComparerSettings
	{
		#region Properties

		StringComparison AttributeNameComparison { get; set; }
		ICollection<string> AttributeNamesToPinFirst { get; }
		StringComparison AttributeValueComparison { get; set; }
		ListSortDirection AttributesAlphabeticalSortDirection { get; set; }
		bool SortAttributesAlphabetically { get; set; }

		#endregion
	}
}