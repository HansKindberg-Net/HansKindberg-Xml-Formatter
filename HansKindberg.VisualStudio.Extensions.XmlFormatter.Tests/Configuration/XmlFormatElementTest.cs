using System.Configuration;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tests.Configuration
{
	[TestClass]
	public class XmlFormatElementTest
	{
		#region Methods

		[TestMethod]
		public void Name_CanIncludeWhiteSpacesTogetherWithOtherCharacters()
		{
			Assert.AreEqual(" T e s t ", new XmlFormatElement {Name = " T e s t "}.Name);
		}

		[TestMethod]
		public void Name_IfGettingItWithoutSettingIt_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, new XmlFormatElement().Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_IfSettingItToASpace_ShouldThrowAConfigurationErrorsException()
		{
			Assert.AreEqual(" ", new XmlFormatElement {Name = " "}.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Name_IfSettingItToAnEmptyString_ShouldThrowAConfigurationErrorsException()
		{
			// ReSharper disable ObjectCreationAsStatement
			new XmlFormatElement {Name = string.Empty};
			// ReSharper restore ObjectCreationAsStatement
		}

		#endregion
	}
}