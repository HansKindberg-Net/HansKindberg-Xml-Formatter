using System;
using System.Collections.Generic;
using System.ComponentModel;
using HansKindberg.Xml.Linq.Comparison;

namespace HansKindberg.Xml.Linq.IntegrationTests.Comparison.Mocks
{
	public class XAttributeComparerSettingsMock : IXAttributeComparerSettings
	{
		#region Fields

		private readonly ICollection<string> _attributeNamesToPinFirst = new List<string>();

		#endregion

		#region Properties

		public virtual StringComparison AttributeNameComparison { get; set; }

		public virtual ICollection<string> AttributeNamesToPinFirst
		{
			get { return this._attributeNamesToPinFirst; }
		}

		public virtual StringComparison AttributeValueComparison { get; set; }
		public virtual ListSortDirection AttributesAlphabeticalSortDirection { get; set; }
		public virtual bool SortAttributesAlphabetically { get; set; }

		#endregion
	}
}