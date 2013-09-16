using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Commands;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32.Abstractions;
using StructureMap;
using Container = System.ComponentModel.Container;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter
{
	[Guid(Identifiers.PackageGuidValue)]
	// This attribute is used to register the information needed to show this package in the Help/About dialog of Visual Studio.
#pragma warning disable 436
	[InstalledProductRegistration("#110", "#112", SolutionInfo.AssemblyVersion, IconResourceID = 400)]
#pragma warning restore 436
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[ProvideAutoLoad(UIContextGuids80.NoSolution)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideOptionPage(typeof(Settings), _packageName, _settingsName, 110, 114, true)]
	[ProvideProfile(typeof(Settings), _packageName, _settingsName, 110, 116, false, DescriptionResourceID = 118)]
	public class XmlFormatterPackage : Package, IXmlFormatterPackage
	{
		#region Fields

		private const string _packageName = "Hans Kindberg - XML Formatter";
		private const string _settingsName = "Settings";
		private IContainer _container;
		private FormatXmlCommand _formatXmlCommand;
		private static readonly Type _packageContainerType = Type.GetType(typeof(Package).AssemblyQualifiedName.Replace("Microsoft.VisualStudio.Shell.Package, ", "Microsoft.VisualStudio.Shell.Package+PackageContainer, "));
		private Container _pagesAndProfiles;
		private static readonly FieldInfo _pagesAndProfilesFieldInfo = typeof(Package).GetField("_pagesAndProfiles", BindingFlags.Instance | BindingFlags.NonPublic);

		#endregion

		#region Properties

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected internal virtual Container PagesAndProfiles
		{
			get
			{
				if(this._pagesAndProfiles == null)
				{
					this._pagesAndProfiles = (Container) _pagesAndProfilesFieldInfo.GetValue(this);

					if(this._pagesAndProfiles == null)
					{
						try
						{
							ConstructorInfo constructorInfo = _packageContainerType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] {typeof(IServiceProvider)}, null);
							_pagesAndProfilesFieldInfo.SetValue(this, constructorInfo.Invoke(new object[] {this}));
						}
						catch(Exception exception)
						{
							Debug.WriteLine(exception);
							throw;
						}
					}

					this._pagesAndProfiles = (Container) _pagesAndProfilesFieldInfo.GetValue(this);
				}

				return this._pagesAndProfiles;
			}
		}

		#endregion

		#region Methods

		public virtual IRegistryKey GetUserRegistryRoot()
		{
			return (RegistryKeyWrapper) this.UserRegistryRoot;
		}

		protected override void Initialize()
		{
			try
			{
				Bootstrapper.Bootstrap(this);

				this._container = ObjectFactory.Container;

				base.Initialize();

				ISettings settings = this._container.GetInstance<ISettings>();

				this.PagesAndProfiles.Add(settings);

				settings.SavedSettingsToStorage += this.OnSavedSettingsToStorage;

				this._formatXmlCommand = this._container.GetInstance<FormatXmlCommand>();
			}
			catch(Exception exception)
			{
				Debug.WriteLine(exception);
				// ReSharper disable LocalizableElement
				MessageBox.Show("Error on package initialize: " + exception, "Error", MessageBoxButtons.OK);
				// ReSharper restore LocalizableElement
				throw;
			}
		}

		protected internal virtual void OnSavedSettingsToStorage(object sender, SettingsEventArguments e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			this._container.GetInstance<IXmlFormatterLocator>().Refresh(e.Settings.ToXmlFormatSection());
		}

		#endregion
	}
}