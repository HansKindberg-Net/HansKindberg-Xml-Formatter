using System.Xml;

namespace HansKindberg.Xml.Linq
{
	public interface IXObject
	{
		#region Properties

		XmlNodeType NodeType { get; }
		IXElement Parent { get; }

		#endregion

		#region Methods

		string ToString();

		#endregion
	}
}