using System;
using System.Configuration;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tests.Configuration
{
	[TestClass]
	public class XmlFormatSectionTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void FromXml_IfTheXmlHasAnInvalidAttribute_ShouldThrowAConfigurationErrorsException()
		{
			XmlFormatSection xmlFormatSection = new XmlFormatSection();
			xmlFormatSection.FromXml("<xmlFormatSection invalidAttribute=\"\" defaultSetting=\"\" enabled=\"false\"><xmlFormats /></xmlFormatSection>");
		}

		[TestMethod]
		public void FromXml_Test()
		{
			XmlFormatSection xmlFormatSection = new XmlFormatSection();
			xmlFormatSection.FromXml("<xmlFormatSection defaultXmlFormat=\"\" enabled=\"false\"><xmlFormats /></xmlFormatSection>");
			Assert.AreEqual(string.Empty, xmlFormatSection.DefaultXmlFormat);
			Assert.IsFalse(xmlFormatSection.Enabled);
			Assert.AreEqual(0, xmlFormatSection.XmlFormats.Count);
		}

		[TestMethod]
		public void ToXml_Test()
		{
			string expectedXml = "<xmlFormatSection defaultXmlFormat=\"\" enabled=\"true\">" + Environment.NewLine + "    <xmlFormats>" + Environment.NewLine + "        <clear />" + Environment.NewLine + "    </xmlFormats>" + Environment.NewLine + "</xmlFormatSection>";

			Assert.AreEqual(expectedXml, new XmlFormatSection().ToXml());
		}

		#endregion

		//internal const string DefaultXml = "<add attributeNameComparison=\"OrdinalIgnoreCase\" attributeNamesToCorrectCommaSeparatedValuesFor=\"\" attributeNamesToPinFirst=\"\" attributeValueComparison=\"OrdinalIgnoreCase\" attributesAlphabeticalSortDirection=\"Ascending\" closeEmptyElements=\"false\" commentFormat=\"None\" elementLevelsToExcludeFromSortingAlphabetically=\"\" elementNameComparison=\"OrdinalIgnoreCase\" elementNamesToExcludeChildrenFromSortingAlphabetically=\"\" elementNamesToPinFirst=\"\" elementPathsToExcludeChildrenFromSortingAlphabetically=\"\" elementPathsToInvolveChildElementWhenSortingElementsAlphabetically=\"\" elementsAlphabeticalSortDirection=\"Ascending\" indent=\"true\" indentString=\"\\t\" involveAttributesWhenSortingElementsAlphabetically=\"false\" minimumNumberOfAttributesForNewLineOnAttributes=\"0\" newLineOnAttributes=\"false\" newLineString=\"\\r\\n\" omitComments=\"false\" omitXmlDeclaration=\"false\" sortAttributesAlphabetically=\"false\" sortElementsAlphabetically=\"false\" />";
		//[TestMethod]
		//[ExpectedException(typeof(ArgumentNullException))]
		//public void FromXml_IfTheXmlParameterIsNull_ShouldThrowAnArgumentNullException()
		//{
		//	XmlFormatSettingCollection.FromXml(null);
		//}
		//[TestMethod]
		//public void ToXml_Test()
		//{
		//	XmlFormatSettingCollection xmlFormatSettingCollection = new XmlFormatSettingCollection();
		//	Assert.AreEqual("<xmlFormats />", xmlFormatSettingCollection.ToXml());
		//	xmlFormatSettingCollection.Add(new XmlFormatSetting { Name = "First" });
		//	Assert.AreEqual("<xmlFormats>" + XmlFormatSettingTest.DefaultXml.Replace(" newLineOnAttributes=", " name=\"First\" newLineOnAttributes=") + "</xmlFormats>", xmlFormatSettingCollection.ToXml());
		//	xmlFormatSettingCollection.Add(new XmlFormatSetting { Name = "Second" });
		//	Assert.AreEqual("<xmlFormats>" + XmlFormatSettingTest.DefaultXml.Replace(" newLineOnAttributes=", " name=\"First\" newLineOnAttributes=") + XmlFormatSettingTest.DefaultXml.Replace(" newLineOnAttributes=", " name=\"Second\" newLineOnAttributes=") + "</xmlFormats>", xmlFormatSettingCollection.ToXml());
		//}
	}
}