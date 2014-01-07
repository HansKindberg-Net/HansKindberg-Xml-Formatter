using System.Configuration;
using HansKindberg.Xml.Formatting.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Formatting.UnitTests.Configuration
{
	[TestClass]
	public class XmlFormatElementCollectionTest
	{
		#region Methods

		[TestMethod]
		public void Add_IfAnItemWithTheSameKeyAndSameValuesIsAdded_ShouldNotThrowAConfigurationErrorsException()
		{
			// ReSharper disable ObjectCreationAsStatement
			new XmlFormatElementCollection {new XmlFormatElement {Name = "Test", Indent = true}, new XmlFormatElement {Name = "Test", Indent = true}};
			// ReSharper restore ObjectCreationAsStatement
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Add_IfAnItemWithTheSameKeyButDifferentValuesIsAdded_ShouldThrowAConfigurationErrorsException()
		{
			// ReSharper disable ObjectCreationAsStatement
			new XmlFormatElementCollection {new XmlFormatElement {Name = "Test", Indent = true}, new XmlFormatElement {Name = "Test", Indent = false}};
			// ReSharper restore ObjectCreationAsStatement
		}

		#endregion
	}
}