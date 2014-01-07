using System;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.UnitTests.Tools.Options
{
	[TestClass]
	public class XmlFormatSettingTest
	{
		#region Methods

		[TestMethod]
		public void IndentString_ShouldResolveEscapeStrings()
		{
			const string indentString = "\t\t\t\t";
			const string resolvedIndentString = @"\t\t\t\t";

			XmlFormatSetting xmlFormatSetting = new XmlFormatSetting {IndentString = resolvedIndentString};
			Assert.AreEqual(indentString, xmlFormatSetting.XmlFormatElement.IndentString);

			xmlFormatSetting.XmlFormatElement.IndentString = indentString;
			Assert.AreEqual(resolvedIndentString, xmlFormatSetting.IndentString);
		}

		[TestMethod]
		public void NewLineString_ShouldResolveEscapeStrings()
		{
			string newLineString = Environment.NewLine + Environment.NewLine;
			const string resolvedNewLineString = @"\r\n\r\n";

			XmlFormatSetting xmlFormatSetting = new XmlFormatSetting {NewLineString = resolvedNewLineString};
			Assert.AreEqual(newLineString, xmlFormatSetting.XmlFormatElement.NewLineString);

			xmlFormatSetting.XmlFormatElement.NewLineString = newLineString;
			Assert.AreEqual(resolvedNewLineString, xmlFormatSetting.NewLineString);
		}

		[TestMethod]
		public void NewLineString_Test()
		{
			Assert.AreEqual(@"\r\n", new XmlFormatSetting().NewLineString);
		}

		[TestMethod]
		public void ResolveInput_Test()
		{
			Assert.AreEqual("\t", new XmlFormatSetting().ResolveInput(@"\t"));
			Assert.AreEqual(Environment.NewLine, new XmlFormatSetting().ResolveInput(@"\r\n"));
		}

		[TestMethod]
		public void ResolveOutput_Test()
		{
			Assert.AreEqual(@"\t", new XmlFormatSetting().ResolveOutput("\t"));
			Assert.AreEqual(@"\r\n", new XmlFormatSetting().ResolveOutput(Environment.NewLine));
		}

		[TestMethod]
		public void ToString_ShouldReturnTheName()
		{
			var xmlFormatSetting = new XmlFormatSetting();
			Assert.AreEqual(string.Empty, xmlFormatSetting.Name);
			Assert.AreEqual(xmlFormatSetting.Name, xmlFormatSetting.ToString());

			xmlFormatSetting.Name = "Test";
			Assert.AreEqual("Test", xmlFormatSetting.Name);
			Assert.AreEqual(xmlFormatSetting.Name, xmlFormatSetting.ToString());
		}

		#endregion
	}
}