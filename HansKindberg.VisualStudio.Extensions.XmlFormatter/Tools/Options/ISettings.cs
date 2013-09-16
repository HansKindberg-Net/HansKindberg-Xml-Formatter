using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options
{
	public interface ISettings : IComponent
	{
		#region Events

		event EventHandler<SettingsEventArguments> SavedSettingsToStorage;
		event EventHandler<SettingsEventArguments> SavedSettingsToXml;
		event EventHandler<CancelSettingsEventArguments> SavingSettingsToStorage;
		event EventHandler<CancelSettingsEventArguments> SavingSettingsToXml;

		#endregion

		#region Properties

		object AutomationObject { get; }
		string DefaultXmlFormat { get; set; }
		bool Enabled { get; set; }
		XmlFormatSettingCollection XmlFormatSettingCollection { get; }

		#endregion

		#region Methods

		void FromXmlFormatSection(XmlFormatSection xmlFormatSection);

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Making it a property should conflict with existing property.")]
		string GetSettingsRegistryPath();

		XmlFormatSection ToXmlFormatSection();

		#endregion
	}
}