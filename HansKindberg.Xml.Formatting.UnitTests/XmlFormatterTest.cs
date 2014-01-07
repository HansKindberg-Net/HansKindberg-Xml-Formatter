using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Xml.Formatting.UnitTests
{
	[TestClass]
	public class XmlFormatterTest
	{
		#region Methods

		private static DefaultXmlFormatter CreateXmlFormatter(IXmlFormat xmlFormat)
		{
			return new DefaultXmlFormatter(xmlFormat);
		}

		//[TestMethod]
		//public void CompareAttribute_ShouldBeCaseInsensitive()
		//{
		//	Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
		//	xmlFormatMock.Setup(xmlFormat => xmlFormat.SortAttributes).Returns(true);
		//	XmlFormatter xmlFormatter = CreateXmlFormatter(xmlFormatMock.Object);
		//	Assert.AreEqual(1, xmlFormatter.CompareAttribute(new ListItem<XAttribute> {Item = new XAttribute("b", "b"), Index = 1}, new ListItem<XAttribute> {Item = new XAttribute("a", "a"), Index = 2}));
		//	Assert.AreEqual(0, xmlFormatter.CompareAttribute(new ListItem<XAttribute> {Item = new XAttribute("test", "test"), Index = 1}, new ListItem<XAttribute> {Item = new XAttribute("TEST", "TEST"), Index = 2}));
		//}
		[TestMethod]
		public void Format_IfAnElementIsClosedInTheXmlAndCloseEmptyElementsIsFalse_ShouldOpenTheClosedElement()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element a=\"A\" b=\"B\" c=\"C\" /></root>";
			const string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element a=\"A\" b=\"B\" c=\"C\"></element></root>";
			Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
			xmlFormatMock.Setup(xmlFormat => xmlFormat.CloseEmptyElements).Returns(false);
			string formattedXml = CreateXmlFormatter(xmlFormatMock.Object).Format(xml);
			Assert.AreEqual(expectedXml, formattedXml);
		}

		[TestMethod]
		public void Format_IfCloseEmptyElementsIsSetToTrue_ShouldCloseEmptyElements()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element></element><element>Test</element></root>";
			const string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element /><element>Test</element></root>";
			Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
			xmlFormatMock.Setup(xmlFormat => xmlFormat.CloseEmptyElements).Returns(true);
			Assert.AreEqual(expectedXml, CreateXmlFormatter(xmlFormatMock.Object).Format(xml));
		}

		[TestMethod]
		public void Format_IfIndentIsSetToFalse_ShouldNotIndent()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element a=\"A\" b=\"B\" c=\"C\"></element></root>";
			const string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element a=\"A\" b=\"B\" c=\"C\"></element></root>";
			Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
			xmlFormatMock.Setup(xmlFormat => xmlFormat.MinimumNumberOfAttributesForNewLineOnAttributes).Returns(0);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.NewLineOnAttributes).Returns(true);
			string formattedXml = CreateXmlFormatter(xmlFormatMock.Object).Format(xml);
			Assert.AreEqual(expectedXml, formattedXml);
		}

		[TestMethod]
		public void Format_IfNewLineOnAttributesIsSetToTrueAndAttributesAreEqualToMinimumNumberOfAttributesForNewLineOnAttributes_ShouldReturnAStringWithAttributesOnAnewLine()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element a=\"A\" b=\"B\" c=\"C\"></element></root>";
			string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + "<root>" + Environment.NewLine + "\t" + "<element" + Environment.NewLine + "\t" + "\t" + "a=\"A\"" + Environment.NewLine + "\t" + "\t" + "b=\"B\"" + Environment.NewLine + "\t" + "\t" + "c=\"C\"" + Environment.NewLine + "\t" + ">" + Environment.NewLine + "\t" + "</element>" + Environment.NewLine + "</root>";
			Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
			xmlFormatMock.Setup(xmlFormat => xmlFormat.Indent).Returns(true);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.IndentString).Returns("\t");
			xmlFormatMock.Setup(xmlFormat => xmlFormat.MinimumNumberOfAttributesForNewLineOnAttributes).Returns(3);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.NewLineString).Returns(Environment.NewLine);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.NewLineOnAttributes).Returns(true);
			string formattedXml = CreateXmlFormatter(xmlFormatMock.Object).Format(xml);
			Assert.AreEqual(expectedXml, formattedXml);
		}

		[TestMethod]
		public void Format_IfNewLineOnAttributesIsSetToTrueAndAttributesAreLessThanMinimumNumberOfAttributesForNewLineOnAttributes_ShouldNotReturnAStringWithAttributesOnAnewLine()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element a=\"A\" b=\"B\" c=\"C\"></element></root>";
			string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + "<root>" + Environment.NewLine + "\t" + "<element a=\"A\" b=\"B\" c=\"C\"></element>" + Environment.NewLine + "</root>";
			Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
			xmlFormatMock.Setup(xmlFormat => xmlFormat.Indent).Returns(true);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.IndentString).Returns("\t");
			xmlFormatMock.Setup(xmlFormat => xmlFormat.MinimumNumberOfAttributesForNewLineOnAttributes).Returns(4);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.NewLineString).Returns(Environment.NewLine);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.NewLineOnAttributes).Returns(true);
			string formattedXml = CreateXmlFormatter(xmlFormatMock.Object).Format(xml);
			Assert.AreEqual(expectedXml, formattedXml);
		}

		[TestMethod]
		public void Format_IfNewLineOnAttributesIsSetToTrueAndAttributesAreMoreThanMinimumNumberOfAttributesForNewLineOnAttributes_ShouldReturnAStringWithAttributesOnAnewLine()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element a=\"A\" b=\"B\" c=\"C\"></element></root>";
			string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + "<root>" + Environment.NewLine + "\t" + "<element" + Environment.NewLine + "\t" + "\t" + "a=\"A\"" + Environment.NewLine + "\t" + "\t" + "b=\"B\"" + Environment.NewLine + "\t" + "\t" + "c=\"C\"" + Environment.NewLine + "\t" + ">" + Environment.NewLine + "\t" + "</element>" + Environment.NewLine + "</root>";
			Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
			xmlFormatMock.Setup(xmlFormat => xmlFormat.Indent).Returns(true);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.IndentString).Returns("\t");
			xmlFormatMock.Setup(xmlFormat => xmlFormat.MinimumNumberOfAttributesForNewLineOnAttributes).Returns(2);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.NewLineString).Returns(Environment.NewLine);
			xmlFormatMock.Setup(xmlFormat => xmlFormat.NewLineOnAttributes).Returns(true);
			string formattedXml = CreateXmlFormatter(xmlFormatMock.Object).Format(xml);
			Assert.AreEqual(expectedXml, formattedXml);
		}

		[TestMethod]
		public void Format_IfSortElementsIsSetToTrueAndInvolveAttributesWhenSortingElementsIsSetToFalseAndTheElementNamesAreEqual_ShouldSortByIndex()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element name=\"3\"></element><element name=\"1\"></element><element name=\"2\"></element></root>";
			const string expectedXml = xml;
			Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
			xmlFormatMock.Setup(format => format.SortElementsAlphabetically).Returns(true);
			xmlFormatMock.Setup(format => format.InvolveAttributesWhenSortingElementsAlphabetically).Returns(false);
			var xmlFormat = xmlFormatMock.Object;

			for(int i = 0; i < 100; i++)
			{
				string formattedXml = CreateXmlFormatter(xmlFormat).Format(xml);
				Assert.AreEqual(expectedXml, formattedXml);
			}
		}

		[TestMethod]
		public void Format_IfSortElementsIsSetToTrue_ShouldSortElements()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element_2></element_2><element_3></element_3><element_1></element_1></root>";
			const string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element_1></element_1><element_2></element_2><element_3></element_3></root>";
			Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
			xmlFormatMock.Setup(xmlFormat => xmlFormat.SortElementsAlphabetically).Returns(true);
			string formattedXml = CreateXmlFormatter(xmlFormatMock.Object).Format(xml);
			Assert.AreEqual(expectedXml, formattedXml);
		}

		[TestMethod]
		public void Format_IfTheXmlDeclarationIsEmpty_ShouldNotStartWithAnewLine()
		{
			const string xml = "<root></root>";
			const string expectedXml = "<root></root>";
			string formattedXml = CreateXmlFormatter(Mock.Of<IXmlFormat>()).Format(xml);
			Assert.AreEqual(expectedXml, formattedXml);
		}

		[TestMethod]
		[ExpectedException(typeof(XmlException))]
		public void Format_IfTheXmlParameterIsNotValidXml_ShouldThrowAXmlException()
		{
			Assert.AreEqual("Test", CreateXmlFormatter(Mock.Of<IXmlFormat>()).Format("Test"));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Format_IfTheXmlParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				CreateXmlFormatter(Mock.Of<IXmlFormat>()).Format(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "xml")
					throw;
			}
		}

		[TestMethod]
		public void Format_ShouldRemoveMultipleSpacesBetweenAttributes()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element  a=\"A\"   b = \"B\"    c  =  \" C \"  ></element></root>";
			const string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><element a=\"A\" b=\"B\" c=\" C \"></element></root>";
			string formattedXml = CreateXmlFormatter(Mock.Of<IXmlFormat>()).Format(xml);
			Assert.AreEqual(expectedXml, formattedXml);
		}

		[TestMethod]
		public void Format_ShouldReturnTheOriginalXmlDeclarationEncoding()
		{
			const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root></root>";
			const string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root></root>";
			Assert.AreEqual(expectedXml, CreateXmlFormatter(Mock.Of<IXmlFormat>()).Format(xml));
		}

		#endregion

		//[TestMethod]
		//public void GetAttributePinIndex_ShouldBeCaseInsensitive()
		//{
		//	Collection<string> attributeNamesToPinFirst = new Collection<string>();
		//	attributeNamesToPinFirst.Add("test");
		//	Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
		//	xmlFormatMock.Setup(xmlFormat => xmlFormat.AttributeNamesToPinFirst).Returns(attributeNamesToPinFirst);
		//	XmlFormatter xmlFormatter = CreateXmlFormatter(xmlFormatMock.Object);
		//	Assert.AreEqual(0, xmlFormatter.GetAttributePinIndex("TEST"));
		//}
		//[TestMethod]
		//public void SortAttributes_IfTheAttributesParameterIsNull_ShouldReturnAnEmptyArray()
		//{
		//	Assert.AreEqual(0, CreateXmlFormatter(Mock.Of<IXmlFormat>()).SortAttributes(null).Count());
		//}
		//[TestMethod]
		//public void SortAttributes_ShouldPinAttributes()
		//{
		//	Collection<string> attributeNamesToPinFirst = new Collection<string>
		//		{
		//			"sixth",
		//			"third"
		//		};
		//	Mock<IXmlFormat> xmlFormatMock = new Mock<IXmlFormat>();
		//	xmlFormatMock.Setup(xmlFormat => xmlFormat.AttributeNamesToPinFirst).Returns(attributeNamesToPinFirst);
		//	XmlFormatter xmlFormatter = CreateXmlFormatter(xmlFormatMock.Object);
		//	List<XAttribute> attributes = new List<XAttribute>
		//		{
		//			new XAttribute("eighth", "Eighth"),
		//			new XAttribute("seventh", "Seventh"),
		//			new XAttribute("sixth", "Sixth"),
		//			new XAttribute("fifth", "Fifth"),
		//			new XAttribute("fourth", "Fourth"),
		//			new XAttribute("third", "Third"),
		//			new XAttribute("second", "Second"),
		//			new XAttribute("first", "First")
		//		};
		//	IEnumerable<XAttribute> sortedAttributes = xmlFormatter.SortAttributes(attributes);
		//	// ReSharper disable PossibleMultipleEnumeration
		//	Assert.AreEqual("sixth", sortedAttributes.ElementAt(0).Name.ToString());
		//	Assert.AreEqual("third", sortedAttributes.ElementAt(1).Name.ToString());
		//	Assert.AreEqual("eighth", sortedAttributes.ElementAt(2).Name.ToString());
		//	Assert.AreEqual("seventh", sortedAttributes.ElementAt(3).Name.ToString());
		//	Assert.AreEqual("fifth", sortedAttributes.ElementAt(4).Name.ToString());
		//	Assert.AreEqual("fourth", sortedAttributes.ElementAt(5).Name.ToString());
		//	Assert.AreEqual("second", sortedAttributes.ElementAt(6).Name.ToString());
		//	Assert.AreEqual("first", sortedAttributes.ElementAt(7).Name.ToString());
		//	// ReSharper restore PossibleMultipleEnumeration
		//	xmlFormatMock = new Mock<IXmlFormat>();
		//	xmlFormatMock.Setup(xmlFormat => xmlFormat.AttributeNamesToPinFirst).Returns(attributeNamesToPinFirst);
		//	xmlFormatMock.Setup(xmlFormat => xmlFormat.SortAttributes).Returns(true);
		//	xmlFormatter = CreateXmlFormatter(xmlFormatMock.Object);
		//	sortedAttributes = xmlFormatter.SortAttributes(attributes);
		//	// ReSharper disable PossibleMultipleEnumeration
		//	Assert.AreEqual("sixth", sortedAttributes.ElementAt(0).Name.ToString());
		//	Assert.AreEqual("third", sortedAttributes.ElementAt(1).Name.ToString());
		//	Assert.AreEqual("eighth", sortedAttributes.ElementAt(2).Name.ToString());
		//	Assert.AreEqual("fifth", sortedAttributes.ElementAt(3).Name.ToString());
		//	Assert.AreEqual("first", sortedAttributes.ElementAt(4).Name.ToString());
		//	Assert.AreEqual("fourth", sortedAttributes.ElementAt(5).Name.ToString());
		//	Assert.AreEqual("second", sortedAttributes.ElementAt(6).Name.ToString());
		//	Assert.AreEqual("seventh", sortedAttributes.ElementAt(7).Name.ToString());
		//	// ReSharper restore PossibleMultipleEnumeration
		//}
		//[TestMethod]
		//public void Format_Comments_Test()
		//{
		//	const string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><!-- root-leading-comment --><root><!-- element_1-leading-comment-1 --><!-- element_1-leading-comment-2 --><element_1 /><!-- element_1-trailing-comment-1 --><!-- element_1-trailing-comment-2 --><!-- element_2-leading-comment-1 --><!-- element_2-leading-comment-2 --><element_2 /><!-- element_2-trailing-comment-1 --><!-- element_2-trailing-comment-2 --></root><!-- root-trailing-comment -->";
		//	string formattedXml = CreateXmlFormatter().Format(xml);
		//	formattedXml = CreateXmlFormatter { ElementsAlphabeticalSortDirection = ListSortDirection.Descending, SortElements = true }.Format(xml);
		//	Assert.IsNotNull(formattedXml);
		//}
	}
}