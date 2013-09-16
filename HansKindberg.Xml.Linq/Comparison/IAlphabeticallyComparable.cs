namespace HansKindberg.Xml.Linq.Comparison
{
	public interface IAlphabeticallyComparable
	{
		#region Properties

		IXName Name { get; }
		string Value { get; }

		#endregion
	}
}