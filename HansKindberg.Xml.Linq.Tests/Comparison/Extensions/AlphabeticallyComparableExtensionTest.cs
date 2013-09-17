using System;
using HansKindberg.Xml.Linq.Comparison;
using HansKindberg.Xml.Linq.Comparison.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Xml.Linq.Tests.Comparison.Extensions
{
	[TestClass]
	public class AlphabeticallyComparableExtensionTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetPinIndex_IfTheXElementParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				((IAlphabeticallyComparable) null).GetPinIndex(new string[0]);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "alphabeticallyComparable")
					throw;
			}
		}

		#endregion
	}
}