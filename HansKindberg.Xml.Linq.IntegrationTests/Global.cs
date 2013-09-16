using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace HansKindberg.Xml.Linq.IntegrationTests
{
	[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Global")]
	public static class Global
	{
		#region Fields

		private static readonly string _projectPath = Path.GetFullPath("..\\..");

		#endregion

		#region Properties

		public static string ProjectPath
		{
			get { return _projectPath; }
		}

		#endregion
	}
}