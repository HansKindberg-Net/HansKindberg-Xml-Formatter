namespace HansKindberg.Xml.Linq
{
	public interface IXNamespace
	{
		#region Properties

		string NamespaceName { get; }

		#endregion

		#region Methods

		IXName GetName(string localName);
		string ToString();

		#endregion
	}
}