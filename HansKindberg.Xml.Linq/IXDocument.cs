namespace HansKindberg.Xml.Linq
{
	public interface IXDocument : IXContainer
	{
		#region Properties

		IXDeclaration Declaration { get; }
		IXElement Root { get; }

		#endregion
	}
}