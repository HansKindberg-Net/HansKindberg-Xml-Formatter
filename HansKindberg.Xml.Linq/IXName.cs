using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Xml.Linq
{
	public interface IXName
	{
		#region Properties

		string LocalName { get; }

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Namespace")]
		IXNamespace Namespace { get; }

		string NamespaceName { get; }

		#endregion

		#region Methods

		string ToString();

		#endregion
	}
}