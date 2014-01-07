using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Linq.UnitTests.Extensions
{
	[TestClass]
	public class XAttributeExtensionTest
	{
		#region Methods

		[TestMethod]
		public void Index_IfTheParentOfTheXAttributeParameterIsNull_ShouldReturnZero()
		{
			XAttribute xAttribute = new XAttribute(XName.Get("Test"), string.Empty);
			Assert.IsNull(xAttribute.Parent);
			Assert.AreEqual(0, xAttribute.Index());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Index_IfTheXAttributeParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				((XAttribute) null).Index();
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "xAttribute")
					throw;
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Xml.Linq.XDocument.Parse(System.String)")]
		public void Index_ShouldReturnThePositionInTheParentsAttributeCollection()
		{
			const string attributeNamePrefix = "attribute";
			int numberOfAttributes = DateTime.Now.Second + 1;

			string attributesXml = string.Empty;
			for(int i = 0; i < numberOfAttributes; i++)
			{
				attributesXml += string.Format(CultureInfo.InvariantCulture, " {0}{1}=\"Value{1}\"", attributeNamePrefix, i.ToString(CultureInfo.InvariantCulture));
			}

			string xml = string.Format(CultureInfo.InvariantCulture, "<element{0} />", attributesXml);

			// ReSharper disable PossibleNullReferenceException
			IEnumerable<XAttribute> attributes = XDocument.Parse(xml).Root.Attributes().ToArray();
			// ReSharper restore PossibleNullReferenceException

			for(int i = 0; i < numberOfAttributes; i++)
			{
				XAttribute xAttribute = attributes.First(attribute => attribute.Name.LocalName == attributeNamePrefix + i.ToString(CultureInfo.InvariantCulture));
				Assert.AreEqual(i, xAttribute.Index());

				Assert.IsNotNull(xAttribute);
			}
		}

		[TestMethod]
		public void PrerequisiteTest_IfAnXAttributeHasNoParent_NextAttributeAndPreviousAttributeShouldReturnNull()
		{
			XAttribute xAttribute = new XAttribute(XName.Get("Test"), string.Empty);
			Assert.IsNull(xAttribute.Parent);
			Assert.IsNull(xAttribute.NextAttribute);
			Assert.IsNull(xAttribute.PreviousAttribute);
		}

		#endregion
	}
}