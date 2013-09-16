using System.Text;

namespace HansKindberg.Xml.Linq
{
	public interface IXDeclaration
	{
		#region Properties

		Encoding Encoding { get; }
		XStandalone Standalone { get; }
		string Version { get; }

		#endregion

		#region Methods

		string ToString();

		#endregion
	}
}