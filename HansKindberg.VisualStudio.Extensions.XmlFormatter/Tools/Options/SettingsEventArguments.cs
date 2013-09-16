using System;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class SettingsEventArguments : EventArgs
	{
		#region Fields

		private readonly ISettings _settings;

		#endregion

		#region Constructors

		public SettingsEventArguments(ISettings settings)
		{
			if(settings == null)
				throw new ArgumentNullException("settings");

			this._settings = settings;
		}

		#endregion

		#region Properties

		public virtual ISettings Settings
		{
			get { return this._settings; }
		}

		#endregion
	}
}