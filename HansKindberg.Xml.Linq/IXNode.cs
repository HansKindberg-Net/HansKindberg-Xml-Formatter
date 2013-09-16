using System.Xml;
using HansKindberg.Xml.Linq.Comparison;

namespace HansKindberg.Xml.Linq
{
	public interface IXNode : IXObject, IIndexComparable
	{
		#region Properties

		IXElement AssociatedTo { get; }
		int Level { get; }

		#endregion

		#region Methods

		void WriteTo(XmlWriter writer);

		#endregion
	}
}