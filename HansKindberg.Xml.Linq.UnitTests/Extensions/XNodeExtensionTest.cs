using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Linq.UnitTests.Extensions
{
	[TestClass]
	public class XNodeExtensionTest
	{
		#region Methods

		private static IList<XNode> CreateNodes()
		{
			//const string xml = "<!-- Comment -->" +
			//				   "<?xml-stylesheet href=\"style.css\" title=\"Compact\" type=\"text/css\"?>" +
			//				   "<!DOCTYPE Pubs [<!ELEMENT Pubs (Book+)><!ELEMENT Book (Title, Author)><!ELEMENT Title (#PCDATA)><!ELEMENT Author (#PCDATA)>]>" +
			//				   "<root>{0}</root>" +
			//				   "<!-- Another comment -->";

			string nodes = string.Empty;

			int numberOfGroups = DateTime.Now.Second + 1;

			for(int i = 0; i < numberOfGroups; i++)
			{
				nodes += "<!-- Comment -->";
				nodes += "<?xml-stylesheet href=\"style.css\" title=\"Compact\" type=\"text/css\"?>";
				nodes += "<Pubs>";
				nodes += "<Book><Title>Artifacts of Roman Civilization</Title><Author>Moreno, Jordan</Author></Book>";
				nodes += "<Book><Title>IT Tools and Implements</Title><Author>Mechanic, Mike</Author></Book>";
				nodes += "</Pubs>";
				nodes += "<!-- Another comment -->";
			}

			return new List<XNode>(XDocument.Parse("<root>" + nodes + "</root>").Nodes());
		}

		[TestMethod]
		public void Index_IfTheParentOfTheXNodeParameterIsNull_ShouldReturnZero()
		{
			XText xmlNode = new XText("Test");
			Assert.IsNull(xmlNode.Parent);
			Assert.AreEqual(0, xmlNode.Index());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Index_IfTheXNodeParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				((XNode) null).Index();
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "xNode")
					throw;
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Xml.Linq.XDocument.Parse(System.String)")]
		public void Index_ShouldReturnThePositionInTheParentsNodesCollection()
		{
			IList<XNode> nodes = CreateNodes();

			for(int i = 0; i < nodes.Count; i++)
			{
				XNode node = nodes[i];
				Assert.AreEqual(i, node.Index());
			}
		}

		[TestMethod]
		public void Level_IfTheParentOfTheXNodeParameterIsNull_ShouldReturnZero()
		{
			XNode xNode = XDocument.Parse("<root />").Root;

			// ReSharper disable PossibleNullReferenceException
			Assert.IsNull(xNode.Parent);
			// ReSharper restore PossibleNullReferenceException

			Assert.AreEqual(0, xNode.Level());
		}

		[TestMethod]
		public void Level_ShouldReturnTheNumberOfAncestorsOfTheNode()
		{
			XNode xNode = XDocument.Parse("<root><first><second><third><fourth><fifth /></fourth></third></second></first></root>").Descendants().Last();
			XElement xElement = (XElement) xNode;
			Assert.AreEqual("fifth", xElement.Name.LocalName);
			Assert.AreEqual(5, xNode.Level());
		}

		#endregion
	}
}