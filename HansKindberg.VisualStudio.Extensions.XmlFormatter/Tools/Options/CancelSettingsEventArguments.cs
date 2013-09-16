using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class CancelSettingsEventArguments : SettingsEventArguments
	{
		#region Constructors

		public CancelSettingsEventArguments(ISettings settings) : base(settings) {}

		#endregion

		#region Properties

		public virtual bool Cancel { get; set; }

		#endregion
	}
}