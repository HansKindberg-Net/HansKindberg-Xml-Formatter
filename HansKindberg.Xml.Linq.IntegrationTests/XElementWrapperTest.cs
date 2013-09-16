using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Comparison;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Xml.Linq.IntegrationTests
{
	[TestClass]
	public class XElementWrapperTest
	{
		#region Methods

		private static IXAttributeComparerSettings CreateXAttributeComparerSettings()
		{
			Mock<IXAttributeComparerSettings> xAttributeComparerSettingsMock = new Mock<IXAttributeComparerSettings>();
			xAttributeComparerSettingsMock.SetupAllProperties();
			IList<string> attributeNamesToPinFirst = new List<string>();
			xAttributeComparerSettingsMock.Setup(xAttributeComparerSettings => xAttributeComparerSettings.AttributeNamesToPinFirst).Returns(attributeNamesToPinFirst);
			return xAttributeComparerSettingsMock.Object;
		}

		[TestMethod]
		public void SortAttributes_Test()
		{
			XElement xElement = XDocument.Parse("<element a=\"a\" b=\"b\" c=\"c\" />").Root;
			XElementWrapper xElementWrapper = new XElementWrapper(xElement);
			IXAttributeComparerSettings xAttributeComparerSettings = CreateXAttributeComparerSettings();
			xAttributeComparerSettings.AttributeNameComparison = StringComparison.OrdinalIgnoreCase;
			//xAttributeComparerSettings.AttributeNamesToPinFirst
			xAttributeComparerSettings.AttributeValueComparison = StringComparison.Ordinal;
			xAttributeComparerSettings.AttributesAlphabeticalSortDirection = ListSortDirection.Descending;
			xAttributeComparerSettings.SortAttributesAlphabetically = true;

			xElementWrapper.SortAttributes(new XAttributeComparer(xAttributeComparerSettings));

			// ReSharper disable PossibleNullReferenceException
			Assert.AreEqual("<element c=\"c\" b=\"b\" a=\"a\" />", xElement.ToString());
			// ReSharper restore PossibleNullReferenceException
		}

		#endregion
	}
}