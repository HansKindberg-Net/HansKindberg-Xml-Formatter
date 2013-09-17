using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using HansKindberg.Xml.Formatting.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Formatting.IntegrationTests
{
	[TestClass]
	public class XmlFormatterTest
	{
		#region Methods

		private static XmlFormatElement CreateDefaultXmlFormat()
		{
			return new XmlFormatElement
				{
					AttributeNameComparison = StringComparison.OrdinalIgnoreCase,
					AttributeNamesToCorrectCommaSeparatedValuesForInternal = "type",
					AttributeNamesToPinFirstInternal = "key, name",
					AttributesAlphabeticalSortDirection = ListSortDirection.Ascending,
					AttributeValueComparison = StringComparison.OrdinalIgnoreCase,
					CloseEmptyElements = true,
					CommentFormat = XmlCommentFormat.SingleLineOrAsXml,
					ElementLevelsToExcludeFromSortingAlphabeticallyInternal = string.Empty,
					ElementNameComparison = StringComparison.OrdinalIgnoreCase,
					ElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal = "handlers, httpHandlers, httpModules, modules",
					ElementNamesToPinFirstInternal = "clear, configSections",
					ElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal = string.Empty,
					ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal = "/configuration/runtime/assemblyBinding/dependentAssembly",
					ElementsAlphabeticalSortDirection = ListSortDirection.Ascending,
					Indent = true,
					IndentString = "\t",
					InvolveAttributesWhenSortingElementsAlphabetically = true,
					MinimumNumberOfAttributesForNewLineOnAttributes = 4,
					Name = "Test",
					NewLineOnAttributes = true,
					NewLineString = Environment.NewLine,
					OmitComments = false,
					OmitXmlDeclaration = false,
					SortAttributesAlphabetically = true,
					SortElementsAlphabetically = true
				};
		}

		private static DefaultXmlFormatter CreateXmlFormatter(IXmlFormat xmlFormat)
		{
			return new DefaultXmlFormatter(xmlFormat);
		}

		[TestMethod]
		public void Format_CommentsAsSingleLineOrAsXml_Test()
		{
			string xml = GetXmlFromFile("Format-CommentsAsSingleLineOrAsXml-Test.xml");
			string expectedFormattedXml = GetXmlFromFile("Format-CommentsAsSingleLineOrAsXml-Test.Expected.xml");

			Assert.AreEqual(expectedFormattedXml, CreateXmlFormatter(CreateDefaultXmlFormat()).Format(xml));
		}

		[TestMethod]
		public void Format_WebConfigWithComments_Test()
		{
			Assert.Inconclusive("Fix this test");

			var xmlFormat = new XmlFormatElement
				{
					AttributeNameComparison = StringComparison.OrdinalIgnoreCase,
					AttributeNamesToCorrectCommaSeparatedValuesForInternal = "type",
					AttributeNamesToPinFirstInternal = "key, name",
					AttributesAlphabeticalSortDirection = ListSortDirection.Ascending,
					AttributeValueComparison = StringComparison.OrdinalIgnoreCase,
					CloseEmptyElements = true,
					CommentFormat = XmlCommentFormat.SingleLineOrAsXml,
					ElementLevelsToExcludeFromSortingAlphabeticallyInternal = "1000",
					ElementNameComparison = StringComparison.OrdinalIgnoreCase,
					ElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal = "handlers, httpHandlers, httpModules, modules",
					ElementNamesToPinFirstInternal = "clear, configSections",
					ElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal = "/bla/bla",
					ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal = "/configuration/runtime/assemblyBinding/dependentAssembly",
					ElementsAlphabeticalSortDirection = ListSortDirection.Ascending,
					Indent = true,
					IndentString = "\t",
					InvolveAttributesWhenSortingElementsAlphabetically = true,
					MinimumNumberOfAttributesForNewLineOnAttributes = 3,
					Name = "Test",
					NewLineOnAttributes = true,
					NewLineString = Environment.NewLine,
					OmitComments = false,
					OmitXmlDeclaration = false,
					SortAttributesAlphabetically = true,
					SortElementsAlphabetically = true
				};

			string webConfigContent = GetXmlFromFile("Web.config");

			string formattedWebConfigContent = CreateXmlFormatter(xmlFormat).Format(webConfigContent);

			string expectedFormattedWebConfigContent = GetXmlFromFile("Web.FormattedWithComments.config");

			Assert.AreEqual(expectedFormattedWebConfigContent, formattedWebConfigContent);

			//Assert.AreEqual(GetTotalNumberOfAttributes(unformattedWebConfigXml), GetTotalNumberOfAttributes(formattedWebConfigXml));
			//Assert.AreEqual(GetTotalNumberOfComments(unformattedWebConfigXml), GetTotalNumberOfComments(formattedWebConfigXml));
			//Assert.AreEqual(GetTotalNumberOfElements(unformattedWebConfigXml), GetTotalNumberOfElements(formattedWebConfigXml));
		}

		[TestMethod]
		public void Format_WebConfigWithoutComments_Test()
		{
			Assert.Inconclusive("Fix this test");

			var xmlFormat = new XmlFormatElement
				{
					AttributeNameComparison = StringComparison.OrdinalIgnoreCase,
					AttributeNamesToCorrectCommaSeparatedValuesForInternal = "type",
					AttributeNamesToPinFirstInternal = "key, name",
					AttributesAlphabeticalSortDirection = ListSortDirection.Ascending,
					AttributeValueComparison = StringComparison.OrdinalIgnoreCase,
					CloseEmptyElements = true,
					CommentFormat = XmlCommentFormat.SingleLineOrAsXml,
					ElementLevelsToExcludeFromSortingAlphabeticallyInternal = "1000",
					ElementNameComparison = StringComparison.OrdinalIgnoreCase,
					ElementNamesToExcludeChildrenFromSortingAlphabeticallyInternal = "handlers, httpHandlers, httpModules, modules",
					ElementNamesToPinFirstInternal = "clear, configSections",
					ElementPathsToExcludeChildrenFromSortingAlphabeticallyInternal = "/bla/bla",
					ElementPathsToInvolveChildElementWhenSortingElementsAlphabeticallyInternal = "/configuration/runtime/assemblyBinding/dependentAssembly",
					ElementsAlphabeticalSortDirection = ListSortDirection.Ascending,
					Indent = true,
					IndentString = "\t",
					InvolveAttributesWhenSortingElementsAlphabetically = true,
					MinimumNumberOfAttributesForNewLineOnAttributes = 3,
					Name = "Test",
					NewLineOnAttributes = true,
					NewLineString = Environment.NewLine,
					OmitComments = false,
					OmitXmlDeclaration = false,
					SortAttributesAlphabetically = true,
					SortElementsAlphabetically = true
				};

			string webConfigContent = GetXmlFromFile("Web.config");

			string formattedWebConfigContent = CreateXmlFormatter(xmlFormat).Format(webConfigContent);

			string expectedFormattedWebConfigContent = GetXmlFromFile("Web.FormattedWithoutComments.config");

			Assert.AreEqual(expectedFormattedWebConfigContent, formattedWebConfigContent);
		}

		private static int GetTotalNumberOfAttributes(string xml)
		{
			return XDocument.Parse(xml).Descendants().SelectMany(xmlElement => xmlElement.Attributes()).Count();
		}

		private static int GetTotalNumberOfComments(string xml)
		{
			return XDocument.Parse(xml).DescendantNodes().OfType<XComment>().Count();
		}

		private static int GetTotalNumberOfElements(string xml)
		{
			return XDocument.Parse(xml).Descendants().Count();
		}

		private static string GetXmlFromFile(string fileName)
		{
			return File.ReadAllText(Path.Combine(Global.ProjectPath, typeof(XmlFormatterTest).Name + "-files", fileName));
		}

		#endregion
	}
}