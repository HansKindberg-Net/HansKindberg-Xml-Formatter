using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using HansKindberg.Collections;
using HansKindberg.Xml.Formatting.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Formatting.Tests.Configuration
{
	[TestClass]
	public class XmlFormatElementTest
	{
		#region Methods

		[TestMethod]
		public void AttributeNameComparison_ShouldReturnOrdinalIgnoreCaseByDefault()
		{
			Assert.AreEqual(StringComparison.OrdinalIgnoreCase, new XmlFormatElement().AttributeNameComparison);
		}

		[TestMethod]
		public void AttributeNamesToPinFirst_AddShouldUpdateAttributeNamesToPinFirstInternal()
		{
			XmlFormatElement xmlFormatElement = new XmlFormatElement();
			Assert.AreEqual(string.Empty, xmlFormatElement.AttributeNamesToPinFirstInternal);
			xmlFormatElement.AttributeNamesToPinFirst.Add("First");
			xmlFormatElement.AttributeNamesToPinFirst.Add("Second");
			Assert.AreEqual("First, Second", xmlFormatElement.AttributeNamesToPinFirstInternal);
		}

		[TestMethod]
		public void AttributeNamesToPinFirst_ShouldReturnAnEmptyCollectionByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().AttributeNamesToPinFirst.Any());
		}

		[TestMethod]
		public void AttributeValueComparison_ShouldReturnOrdinalIgnoreCaseByDefault()
		{
			Assert.AreEqual(StringComparison.OrdinalIgnoreCase, new XmlFormatElement().AttributeValueComparison);
		}

		[TestMethod]
		public void AttributesAlphabeticalSortDirection_ShouldReturnAscendingByDefault()
		{
			Assert.AreEqual(ListSortDirection.Ascending, new XmlFormatElement().AttributesAlphabeticalSortDirection);
		}

		[TestMethod]
		public void CloseEmptyElements_ShouldReturnFalseByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().CloseEmptyElements);
		}

		[TestMethod]
		public void CommentFormat_ShouldReturnNoneByDefault()
		{
			Assert.AreEqual(XmlCommentFormat.None, new XmlFormatElement().CommentFormat);
		}

		[TestMethod]
		public void ElementLevelsToExcludeFromSortingAlphabetically_AddShouldUpdateElementLevelsToExcludeFromSortingAlphabeticallyInternal()
		{
			XmlFormatElement xmlFormatElement = new XmlFormatElement();
			Assert.AreEqual(string.Empty, xmlFormatElement.ElementLevelsToExcludeFromSortingAlphabeticallyInternal);
			xmlFormatElement.ElementLevelsToExcludeFromSortingAlphabetically.Add(1);
			xmlFormatElement.ElementLevelsToExcludeFromSortingAlphabetically.Add(2);
			Assert.AreEqual("1, 2", xmlFormatElement.ElementLevelsToExcludeFromSortingAlphabeticallyInternal);
		}

		[TestMethod]
		public void ElementLevelsToExcludeFromSortingAlphabetically_ShouldReturnAnEmptyCollectionByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().ElementLevelsToExcludeFromSortingAlphabetically.Any());
		}

		[TestMethod]
		public void ElementNameComparison_ShouldReturnOrdinalIgnoreCaseByDefault()
		{
			Assert.AreEqual(StringComparison.OrdinalIgnoreCase, new XmlFormatElement().ElementNameComparison);
		}

		[TestMethod]
		public void ElementNamesToPinFirst_AddShouldUpdateElementNamesToPinFirstInternal()
		{
			XmlFormatElement xmlFormatElement = new XmlFormatElement();
			Assert.AreEqual(string.Empty, xmlFormatElement.ElementNamesToPinFirstInternal);
			xmlFormatElement.ElementNamesToPinFirst.Add("First");
			xmlFormatElement.ElementNamesToPinFirst.Add("Second");
			Assert.AreEqual("First, Second", xmlFormatElement.ElementNamesToPinFirstInternal);
		}

		[TestMethod]
		public void ElementNamesToPinFirst_ShouldReturnAnEmptyCollectionByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().ElementNamesToPinFirst.Any());
		}

		[TestMethod]
		public void ElementsAlphabeticalSortDirection_ShouldReturnAscendingByDefault()
		{
			Assert.AreEqual(ListSortDirection.Ascending, new XmlFormatElement().ElementsAlphabeticalSortDirection);
		}

		[TestMethod]
		public void IndentString_ShouldReturnATabStringByDefault()
		{
			Assert.AreEqual("\t", new XmlFormatElement().IndentString);
		}

		[TestMethod]
		public void Indent_ShouldReturnTrueByDefault()
		{
			Assert.IsTrue(new XmlFormatElement().Indent);
		}

		[TestMethod]
		public void Join_ShouldNotTrimItems()
		{
			const string commaSeparatedValue = " one ,  two  ,   three   ";
			const string expectedValue = " one ,   two  ,    three   ";
			CollectionEventArguments<string> stringArguments = new CollectionEventArguments<string>(commaSeparatedValue.Split(",".ToCharArray(), StringSplitOptions.None));
			Assert.AreEqual(expectedValue, new XmlFormatElement().Join(stringArguments));
		}

		[TestMethod]
		public void Join_ShouldSeparateEachItemWithACommaAndAWhiteSpace()
		{
			Assert.AreEqual("one, two, three", new XmlFormatElement().Join(new CollectionEventArguments<string>(new[] {"one", "two", "three"})));
			Assert.AreEqual("1, 2, 3", new XmlFormatElement().Join(new CollectionEventArguments<int>(new[] {1, 2, 3})));
		}

		[TestMethod]
		public void MinimumNumberOfAttributesForNewLineOnAttributes_ShouldReturnZeroByDefault()
		{
			Assert.AreEqual(0, new XmlFormatElement().MinimumNumberOfAttributesForNewLineOnAttributes);
		}

		[TestMethod]
		public void Name_IfGettingItWithoutSettingIt_ShouldReturnNull()
		{
			Assert.IsNull(new XmlFormatElement().Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_IfSettingItToASpace_ShouldThrowAConfigurationErrorsException()
		{
			// ReSharper disable ObjectCreationAsStatement
			new XmlFormatElement {Name = " "};
			// ReSharper restore ObjectCreationAsStatement
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_IfSettingItToAnEmptyString_ShouldThrowAConfigurationErrorsException()
		{
			// ReSharper disable ObjectCreationAsStatement
			new XmlFormatElement {Name = string.Empty};
			// ReSharper restore ObjectCreationAsStatement
		}

		[TestMethod]
		public void NewLineOnAttributes_ShouldReturnFalseByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().NewLineOnAttributes);
		}

		[TestMethod]
		public void NewLineString_ShouldReturnEnvironmentNewLineByDefault()
		{
			Assert.AreEqual(Environment.NewLine, new XmlFormatElement().NewLineString);
		}

		[TestMethod]
		public void OmitComments_ShouldReturnFalseByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().OmitComments);
		}

		[TestMethod]
		public void OmitXmlDeclaration_ShouldReturnFalseByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().OmitXmlDeclaration);
		}

		[TestMethod]
		public void SortAttributesAlphabetically_ShouldReturnFalseByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().SortAttributesAlphabetically);
		}

		[TestMethod]
		public void SortElementsAlphabetically_ShouldReturnFalseByDefault()
		{
			Assert.IsFalse(new XmlFormatElement().SortElementsAlphabetically);
		}

		[TestMethod]
		public void Split_ShouldNotRemoveEmptyEntries()
		{
			const string commaSeparatedString = ",     , ,,              ,          ,  ,    ,   ,     ,  ";
			IEnumerable<string> stringCollection = new XmlFormatElement().Split(commaSeparatedString).ToArray();
			Assert.AreEqual(11, stringCollection.Count());
			for(int i = 0; i < stringCollection.Count(); i++)
			{
				Assert.AreEqual(string.Empty, stringCollection.ElementAt(i));
			}
		}

		[TestMethod]
		public void Split_ShouldTrimEachItem()
		{
			const string commaSeparatedString = ",  one   , two,three,four              ,        five  , six , seven   , eight  , nine ten   ,  ";
			IEnumerable<string> stringCollection = new XmlFormatElement().Split(commaSeparatedString).ToArray();
			Assert.AreEqual(11, stringCollection.Count());

			Assert.AreEqual(string.Empty, stringCollection.ElementAt(0));
			Assert.AreEqual("one", stringCollection.ElementAt(1));
			Assert.AreEqual("two", stringCollection.ElementAt(2));
			Assert.AreEqual("three", stringCollection.ElementAt(3));
			Assert.AreEqual("four", stringCollection.ElementAt(4));
			Assert.AreEqual("five", stringCollection.ElementAt(5));
			Assert.AreEqual("six", stringCollection.ElementAt(6));
			Assert.AreEqual("seven", stringCollection.ElementAt(7));
			Assert.AreEqual("eight", stringCollection.ElementAt(8));
			Assert.AreEqual("nine ten", stringCollection.ElementAt(9));
			Assert.AreEqual(string.Empty, stringCollection.ElementAt(10));
		}

		#endregion
	}
}