using HansKindberg.Xml.Formatting.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Formatting.Tests.Configuration
{
	[TestClass]
	public class XmlFormatSectionTest
	{
		#region Methods

		[TestMethod]
		public void DefaultSetting_ShouldReturnAnEmptyStringByDefault()
		{
			Assert.AreEqual(string.Empty, new XmlFormatSection().DefaultXmlFormat);
		}

		[TestMethod]
		public void Settings_ShouldReturnAnEmptyCollectionByDefault()
		{
			Assert.AreEqual(0, new XmlFormatSection().XmlFormats.Count);
		}

		#endregion
	}
}