using System;
using System.Xml.Linq;
using HansKindberg.Xml.Linq.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Linq.Tests.Extensions
{
	[TestClass]
	public class XElementExtensionTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetPinIndex_IfTheXElementParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				((XElement) null).GetPinIndex(new string[0]);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "xElement")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Path_IfTheXElementParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				((XElement) null).Path();
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "xElement")
					throw;
			}
		}

		#endregion
	}
}