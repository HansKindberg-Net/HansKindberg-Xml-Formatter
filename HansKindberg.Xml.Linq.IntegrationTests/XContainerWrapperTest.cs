using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Linq.IntegrationTests
{
	[TestClass]
	public class XContainerWrapperTest
	{
		#region Fields

		private const string _prerequisiteTestDescendantsXml = "<root><element_1><element_1_1 /><element_1_2 /></element_1><element_2><element_2_1 /><element_2_2 /></element_2></root>";

		#endregion

		#region Methods

		[TestMethod]
		public void PrerequisiteTest_DescendantsOfAXDocumentIncludesTheRootElement()
		{
			Assert.AreEqual(7, XDocument.Parse(_prerequisiteTestDescendantsXml).Descendants().Count());
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "secondsecond"), TestMethod]
		public void PrerequisiteTest_DescendantsOfAXElementDoesNotIncludeSelf()
		{
			// ReSharper disable PossibleNullReferenceException
			Assert.AreEqual(6, XDocument.Parse(_prerequisiteTestDescendantsXml).Root.Descendants().Count());
			// ReSharper restore PossibleNullReferenceException
		}

		#endregion
	}
}