using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq.Comparison
{
	public class XElementComparer : XComparer<IXElement>, IComparer<IXElement>, IComparer<XElement>
	{
		#region Fields

		private readonly IComparer<IXAttribute> _xAttributeComparer;
		private readonly IXElementComparerSettings _xElementComparerSettings;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		public XElementComparer(IXElementComparerSettings xElementComparerSettings, IComparer<IXAttribute> xAttributeComparer)
		{
			if(xElementComparerSettings == null)
				throw new ArgumentNullException("xElementComparerSettings");

			if(xAttributeComparer == null)
				throw new ArgumentNullException("xAttributeComparer");

			this._xAttributeComparer = xAttributeComparer;
			this._xElementComparerSettings = xElementComparerSettings;
		}

		#endregion

		#region Properties

		protected internal virtual IComparer<IXAttribute> XAttributeComparer
		{
			get { return this._xAttributeComparer; }
		}

		protected internal virtual IXElementComparerSettings XElementComparerSettings
		{
			get { return this._xElementComparerSettings; }
		}

		#endregion

		#region Methods

		public virtual int Compare(IXElement x, IXElement y)
		{
			if(ReferenceEquals(x, y))
				return 0;

			int? compare = this.CompareByPinIndex(x, y, this.XElementComparerSettings.ElementNamesToPinFirst);

			if(!compare.HasValue || compare.Value == 0)
			{
				compare = this.CompareByName(x, y, this.XElementComparerSettings.ElementNameComparison, this.XElementComparerSettings.ElementsAlphabeticalSortDirection);

				if(compare.HasValue && compare.Value == 0)
				{
					int? compareByAttribute = this.CompareByAttributes(x, y);

					if(compareByAttribute.HasValue)
						compare = compareByAttribute.Value;

					if(compare.Value == 0)
					{
						int? compareByChildElement = this.CompareByChildElement(x, y);

						if(compareByChildElement.HasValue)
							compare = compareByChildElement.Value;
					}
				}
			}

			if(!compare.HasValue || compare.Value == 0)
				compare = this.CompareByIndex(x, y);

			return compare.Value;
		}

		public virtual int Compare(XElement x, XElement y)
		{
			return this.Compare((XElementWrapper) x, (XElementWrapper) y);
		}

		protected internal virtual int? CompareByAttributes(IXElement firstXElement, IXElement secondXElement)
		{
			int? compare = null;

			if(this.SortAlphabetically(firstXElement, secondXElement) && this.XElementComparerSettings.InvolveAttributesWhenSortingElementsAlphabetically)
			{
				IEnumerable<IXAttribute> firstAttributes = firstXElement != null ? firstXElement.Attributes : new IXAttribute[0];
				IEnumerable<IXAttribute> secondAttributes = secondXElement != null ? secondXElement.Attributes : new IXAttribute[0];

				if(firstAttributes.Any() || secondAttributes.Any())
				{
					int leastNumberOfAttributes = firstAttributes.Count() < secondAttributes.Count() ? firstAttributes.Count() : secondAttributes.Count();

					for(int i = 0; i < leastNumberOfAttributes; i++)
					{
						compare = this.XAttributeComparer.Compare(firstAttributes.ElementAt(i), secondAttributes.ElementAt(i));

						if(compare.Value != 0)
							break;
					}

					if(!compare.HasValue || compare.Value == 0)
						compare = firstAttributes.Count().CompareTo(secondAttributes.Count());

					// ReSharper disable PossibleInvalidOperationException
					compare = this.ResolveAlphabeticalCompare(compare.Value, this.XElementComparerSettings.ElementsAlphabeticalSortDirection);
					// ReSharper restore PossibleInvalidOperationException
				}
			}

			return compare;
		}

		protected internal virtual int? CompareByChildElement(IXElement firstXElement, IXElement secondXElement)
		{
			int? compare = null;

			if(this.SortAlphabetically(firstXElement, secondXElement) && this.XElementComparerSettings.ElementPathsToInvolveChildElementWhenSortingElementsAlphabetically != null)
			{
				if(firstXElement == null)
					throw new ArgumentNullException("firstXElement");

				if(this.XElementComparerSettings.ElementPathsToInvolveChildElementWhenSortingElementsAlphabetically.Contains(firstXElement.Path))
				{
					if(secondXElement == null)
						throw new ArgumentNullException("secondXElement");

					compare = this.Compare(firstXElement.Nodes.OfType<IXElement>().FirstOrDefault(), secondXElement.Nodes.OfType<IXElement>().FirstOrDefault());
				}
			}

			return compare;
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
		protected internal virtual bool SortAlphabetically(IXElement xElement)
		{
			if(!this.XElementComparerSettings.SortElementsAlphabetically)
				return false;

			if(xElement == null)
				return true;

			if(this.XElementComparerSettings.ElementLevelsToExcludeFromSortingAlphabetically != null && this.XElementComparerSettings.ElementLevelsToExcludeFromSortingAlphabetically.Contains(xElement.Level))
				return false;

			if(xElement.Parent == null)
				return true;

			if(this.XElementComparerSettings.ElementNamesToExcludeChildrenFromSortingAlphabetically != null && this.XElementComparerSettings.ElementNamesToExcludeChildrenFromSortingAlphabetically.Contains(xElement.Parent.Name))
				return false;

			if(this.XElementComparerSettings.ElementPathsToExcludeChildrenFromSortingAlphabetically != null && this.XElementComparerSettings.ElementPathsToExcludeChildrenFromSortingAlphabetically.Contains(xElement.Parent.Path))
				return false;

			return true;
		}

		protected internal override bool SortAlphabetically(IXElement firstItem, IXElement secondItem)
		{
			if(ReferenceEquals(firstItem, secondItem))
				return false;

			return this.SortAlphabetically(firstItem) && this.SortAlphabetically(secondItem);
		}

		#endregion
	}
}