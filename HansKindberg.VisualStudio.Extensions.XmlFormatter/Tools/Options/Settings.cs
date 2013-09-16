using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options.Components;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options
{
	[Guid("0467bc95-f05a-4d1d-a8c3-c56489bed87b")]
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "DialogPage already implements IDisposable.")]
	public class Settings : DialogPage, ISettings
	{
		#region Fields

		private const string _defaultCategory = "Settings";
		private static readonly MethodInfo _hookPropertiesMethodInfo = typeof(DialogPage).GetMethod("HookProperties", BindingFlags.Instance | BindingFlags.NonPublic);
		private static readonly FieldInfo _initializingFieldInfo = typeof(DialogPage).GetField("_initializing", BindingFlags.Instance | BindingFlags.NonPublic);
		private readonly ISettingsRepository _settingsRepository;
		private readonly XmlFormatSettingCollection _xmlFormatSettingCollection;

		#endregion

		#region Constructors

		public Settings(ISettingsRepository settingsRepository)
		{
			if(settingsRepository == null)
				throw new ArgumentNullException("settingsRepository");

			this._settingsRepository = settingsRepository;
			this._xmlFormatSettingCollection = new XmlFormatSettingCollection();
		}

		#endregion

		#region Events

		public event EventHandler<SettingsEventArguments> SavedSettingsToStorage;
		public event EventHandler<SettingsEventArguments> SavedSettingsToXml;
		public event EventHandler<CancelSettingsEventArguments> SavingSettingsToStorage;
		public event EventHandler<CancelSettingsEventArguments> SavingSettingsToXml;

		#endregion

		#region Properties

		[Category(_defaultCategory)]
		[Description("Default XML-format - description")]
		[DisplayName("Default XML-format")]
		[Editor(typeof(XmlFormatSettingNameSelector), typeof(UITypeEditor))]
		public virtual string DefaultXmlFormat { get; set; }

		[Category("Mode")]
		[Description("Enabled - description")]
		[DisplayName("Enabled")]
		public virtual bool Enabled { get; set; }

		protected internal virtual bool Initializing
		{
			get { return (bool) _initializingFieldInfo.GetValue(this); }
			set { _initializingFieldInfo.SetValue(this, value); }
		}

		[Category(_defaultCategory)]
		[Description("XML-formats - description")]
		[DisplayName("XML-formats")]
		public virtual XmlFormatSettingCollection XmlFormatSettingCollection
		{
			get { return this._xmlFormatSettingCollection; }
		}

		#endregion

		#region Methods

		public virtual void FromXmlFormatSection(XmlFormatSection xmlFormatSection)
		{
			if(xmlFormatSection == null)
				return;

			this.DefaultXmlFormat = xmlFormatSection.DefaultXmlFormat;
			this.Enabled = xmlFormatSection.Enabled;
			this._xmlFormatSettingCollection.Clear();
			foreach(var xmlFormatElement in xmlFormatSection.XmlFormats)
			{
				this._xmlFormatSettingCollection.Add(xmlFormatElement);
			}
		}

		public virtual string GetSettingsRegistryPath()
		{
			return this.SettingsRegistryPath;
		}

		protected internal virtual void HookProperties(bool hook)
		{
			_hookPropertiesMethodInfo.Invoke(this, new object[] {hook});
		}

		public override void LoadSettingsFromStorage()
		{
			this.Initializing = true;

			try
			{
				this._settingsRepository.Load(this);
			}
			finally
			{
				this.Initializing = false;
			}

			this.HookProperties(true);
		}

		public override void LoadSettingsFromXml(IVsSettingsReader reader)
		{
			if(reader == null)
				throw new ArgumentNullException("reader");

			this.Initializing = true;

			try
			{
				string xml;

				if(reader.ReadSettingString(typeof(XmlFormatSection).Name, out xml) >= 0 && xml != null)
				{
					XmlFormatSection xmlFormatSection = new XmlFormatSection();
					xmlFormatSection.FromXml(xml);
					this.FromXmlFormatSection(xmlFormatSection);
				}
			}
			finally
			{
				this.Initializing = false;
			}

			this.HookProperties(true);
		}

		public override void SaveSettingsToStorage()
		{
			if(this.SavingSettingsToStorage != null)
			{
				CancelSettingsEventArguments cancelSettingsEventArguments = new CancelSettingsEventArguments(this);

				this.SavingSettingsToStorage(this, cancelSettingsEventArguments);

				if(cancelSettingsEventArguments.Cancel)
					return;
			}

			this._settingsRepository.Save(this);

			if(this.SavedSettingsToStorage != null)
				this.SavedSettingsToStorage(this, new SettingsEventArguments(this));
		}

		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Microsoft.VisualStudio.Shell.Interop.IVsSettingsWriter.WriteSettingString(System.String,System.String)")]
		public override void SaveSettingsToXml(IVsSettingsWriter writer)
		{
			if(writer == null)
				throw new ArgumentNullException("writer");

			if(this.SavingSettingsToXml != null)
			{
				CancelSettingsEventArguments cancelSettingsEventArguments = new CancelSettingsEventArguments(this);

				this.SavingSettingsToXml(this, cancelSettingsEventArguments);

				if(cancelSettingsEventArguments.Cancel)
					return;
			}

			writer.WriteSettingString(typeof(XmlFormatSection).Name, this.ToXmlFormatSection().ToXml());

			if(this.SavedSettingsToXml != null)
				this.SavedSettingsToXml(this, new SettingsEventArguments(this));
		}

		public virtual XmlFormatSection ToXmlFormatSection()
		{
			return this;
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected override void OnApply(PageApplyEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			var names = this.XmlFormatSettingCollection.Select(xmlFormatSetting => xmlFormatSetting.Name).ToArray();

			foreach(var name in names)
			{
				try
				{
					// ReSharper disable ObjectCreationAsStatement
					new XmlFormatSetting {Name = name};
					// ReSharper restore ObjectCreationAsStatement
				}
				catch(Exception exception)
				{
					e.ApplyBehavior = ApplyKind.Cancel;
					Debug.WriteLine(exception.Message);
					// ReSharper disable LocalizableElement
					MessageBox.Show(exception.Message, "Input Error", MessageBoxButtons.OK);
					// ReSharper restore LocalizableElement
				}
			}

			if(names.Distinct(StringComparer.OrdinalIgnoreCase).Count() < names.Count())
			{
				e.ApplyBehavior = ApplyKind.Cancel;
				const string exceptionMessage = "The name must be unique.";
				Debug.WriteLine(exceptionMessage);
				// ReSharper disable LocalizableElement
				MessageBox.Show(exceptionMessage, "Input Error", MessageBoxButtons.OK);
				// ReSharper restore LocalizableElement
			}

			if(!string.IsNullOrEmpty(this.DefaultXmlFormat) && !names.Contains(this.DefaultXmlFormat, StringComparer.OrdinalIgnoreCase))
			{
				e.ApplyBehavior = ApplyKind.Cancel;
				string exceptionMessage = string.Format(CultureInfo.InvariantCulture, "The name \"{0}\" does not exist.", this.DefaultXmlFormat);
				Debug.WriteLine(exceptionMessage);
				// ReSharper disable LocalizableElement
				MessageBox.Show(exceptionMessage, "Input Error", MessageBoxButtons.OK);
				// ReSharper restore LocalizableElement
			}

			if(e.ApplyBehavior == ApplyKind.Apply)
				base.OnApply(e);
		}

		#endregion

		#region Implicit operators

		public static implicit operator XmlFormatSection(Settings settings)
		{
			if(settings == null)
				return null;

			var xmlFormatSection = new XmlFormatSection
				{
					DefaultXmlFormat = settings.DefaultXmlFormat,
					Enabled = settings.Enabled
				};

			foreach(var xmlFormatSetting in settings.XmlFormatSettingCollection)
			{
				xmlFormatSection.XmlFormats.Add(xmlFormatSetting);
			}

			return xmlFormatSection;
		}

		#endregion
	}
}