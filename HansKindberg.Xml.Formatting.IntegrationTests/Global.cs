using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace HansKindberg.Xml.Formatting.IntegrationTests
{
	[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Global")]
	public static class Global
	{
		#region Fields

		private static readonly string _projectPath;
		private static readonly string _visualStudioSolutionPath;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static Global()
		{
			_visualStudioSolutionPath = Path.GetFullPath("..\\..\\..");
			// ReSharper disable AssignNullToNotNullAttribute
			_projectPath = Path.Combine(_visualStudioSolutionPath, typeof(Global).Namespace);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		#endregion

		#region Properties

		public static string ProjectPath
		{
			get { return _projectPath; }
		}

		public static string VisualStudioSolutionPath
		{
			get { return _visualStudioSolutionPath; }
		}

		#endregion
	}
}