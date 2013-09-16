using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq.Comparison
{
	public class XAttributeComparer : XComparer<IXAttribute>, IComparer<IXAttribute>, IComparer<XAttribute>
	{
		#region Fields

		private readonly IXAttributeComparerSettings _xAttributeComparerSettings;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XAttributeComparer(IXAttributeComparerSettings xAttributeComparerSettings)
		{
			if(xAttributeComparerSettings == null)
				throw new ArgumentNullException("xAttributeComparerSettings");

			this._xAttributeComparerSettings = xAttributeComparerSettings;
		}

		#endregion

		#region Properties

		protected internal virtual IXAttributeComparerSettings XAttributeComparerSettings
		{
			get { return this._xAttributeComparerSettings; }
		}

		#endregion

		#region Methods

		public virtual int Compare(IXAttribute x, IXAttribute y)
		{
			if(ReferenceEquals(x, y))
				return 0;

			int? compare = this.CompareByPinIndex(x, y, this.XAttributeComparerSettings.AttributeNamesToPinFirst);

			if(!compare.HasValue || compare.Value == 0)
			{
				compare = this.CompareByName(x, y, this.XAttributeComparerSettings.AttributeNameComparison, this.XAttributeComparerSettings.AttributesAlphabeticalSortDirection);

				if(compare.HasValue && compare.Value == 0)
					compare = this.CompareByValue(x, y, this.XAttributeComparerSettings.AttributeValueComparison, this.XAttributeComparerSettings.AttributesAlphabeticalSortDirection);
			}

			if(!compare.HasValue || compare.Value == 0)
				compare = this.CompareByIndex(x, y);

			return compare.Value;
		}

		public virtual int Compare(XAttribute x, XAttribute y)
		{
			return this.Compare((XAttributeWrapper) x, (XAttributeWrapper) y);
		}

		protected internal override bool SortAlphabetically(IXAttribute firstItem, IXAttribute secondItem)
		{
			if(ReferenceEquals(firstItem, secondItem))
				return false;

			return this.XAttributeComparerSettings.SortAttributesAlphabetically;
		}

		#endregion
	}
}