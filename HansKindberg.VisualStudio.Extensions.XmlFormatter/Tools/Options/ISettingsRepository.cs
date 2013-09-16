namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options
{
	public interface ISettingsRepository
	{
		#region Methods

		void Load(ISettings settings);
		void Save(ISettings settings);

		#endregion
	}
}