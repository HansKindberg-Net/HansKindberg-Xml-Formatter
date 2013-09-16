using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Comparison;
using HansKindberg.Xml.Linq.IntegrationTests.Comparison.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Linq.IntegrationTests
{
	[TestClass]
	public class XDocumentWrapperTest
	{
		#region Methods

		private static string GetFileContent(string fileName)
		{
			return File.ReadAllText(Path.Combine(Global.ProjectPath, typeof(XDocumentWrapperTest).Name + "-files", fileName));
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public void Sort_MixedElementTypes_Test()
		{
			Assert.Inconclusive("Fix this test");

			IXAttributeComparerSettings xAttributeComparerSettings = new XAttributeComparerSettingsMock();
			xAttributeComparerSettings.AttributeNameComparison = StringComparison.OrdinalIgnoreCase;
			xAttributeComparerSettings.AttributeValueComparison = StringComparison.OrdinalIgnoreCase;
			xAttributeComparerSettings.AttributesAlphabeticalSortDirection = ListSortDirection.Ascending;
			xAttributeComparerSettings.SortAttributesAlphabetically = true;

			XAttributeComparer xAttributeComparer = new XAttributeComparer(xAttributeComparerSettings);

			IXElementComparerSettings xElementComparerSettings = new XElementComparerSettingsMock();
			xElementComparerSettings.ElementNameComparison = StringComparison.OrdinalIgnoreCase;
			xElementComparerSettings.ElementsAlphabeticalSortDirection = ListSortDirection.Ascending;
			xElementComparerSettings.SortElementsAlphabetically = true;

			XElementComparer xElementComparer = new XElementComparer(xElementComparerSettings, xAttributeComparer);

			IXNodeComparerSettings xNodeComparerSettings = new XNodeComparerSettingsMock();

			XNodeComparer xNodeComparer = new XNodeComparer(xNodeComparerSettings, xElementComparer);

			XDocument xDocument = XDocument.Parse(GetFileContent("Mixed-element-types.xml"));
			XDocumentWrapper xDocumentWrapper = new XDocumentWrapper(xDocument);
			xDocumentWrapper.Sort(xNodeComparer, xAttributeComparer);

			Assert.AreEqual(GetFileContent("Mixed-element-types.Sorted.xml"), xDocument);
		}

		#endregion
	}
}