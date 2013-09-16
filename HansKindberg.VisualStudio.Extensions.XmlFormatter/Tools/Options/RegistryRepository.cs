using System;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;
using Microsoft.Win32.Abstractions;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options
{
	public class RegistryRepository : ISettingsRepository
	{
		#region Fields

		private const string _name = "XmlFormatSection";
		private readonly IXmlFormatterPackage _xmlFormatterPackage;

		#endregion

		#region Constructors

		public RegistryRepository(IXmlFormatterPackage xmlFormatterPackage)
		{
			if(xmlFormatterPackage == null)
				throw new ArgumentNullException("xmlFormatterPackage");

			this._xmlFormatterPackage = xmlFormatterPackage;
		}

		#endregion

		#region Methods

		public virtual void Load(ISettings settings)
		{
			if(settings == null)
				throw new ArgumentNullException("settings");

			using(IRegistryKey rootRegistryKey = this._xmlFormatterPackage.GetUserRegistryRoot())
			{
				string settingsRegistryPath = settings.GetSettingsRegistryPath();

				IRegistryKey subRegistryKey = rootRegistryKey.OpenSubkey(settingsRegistryPath, false);

				if(subRegistryKey == null)
					return;

				XmlFormatSection xmlFormatSection = new XmlFormatSection();

				using(subRegistryKey)
				{
					xmlFormatSection.FromXml(subRegistryKey.GetValue(_name).ToString());
				}

				settings.FromXmlFormatSection(xmlFormatSection);
			}
		}

		public virtual void Save(ISettings settings)
		{
			if(settings == null)
				throw new ArgumentNullException("settings");

			using(IRegistryKey rootRegistryKey = this._xmlFormatterPackage.GetUserRegistryRoot())
			{
				string settingsRegistryPath = settings.GetSettingsRegistryPath();

				IRegistryKey subRegistryKey = rootRegistryKey.OpenSubkey(settingsRegistryPath, true) ?? rootRegistryKey.CreateSubkey(settingsRegistryPath);

				using(subRegistryKey)
				{
					subRegistryKey.SetValue(_name, settings.ToXmlFormatSection().ToXml());
				}
			}
		}

		#endregion
	}
}