using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE.Extensions;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options;
using HansKindberg.Xml.Formatting;
using Microsoft.VisualStudio.Shell;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Commands
{
	public class FormatXmlCommand
	{
		#region Fields

		private IEnumerable<MenuCommand> _commands;
		private readonly OleMenuCommand _defaultCommand;
		private readonly OleMenuCommand _formatXmlSubmenu;
		private readonly IMenuCommandService _menuCommandService;
		private readonly ISettings _settings;
		private readonly DTE _visualStudio;
		private readonly IXmlFormatterLocator _xmlFormatterLocator;

		#endregion

		#region Constructors

		public FormatXmlCommand(DTE visualStudio, IMenuCommandService menuCommandService, ISettings settings, IXmlFormatterLocator xmlFormatterLocator)
		{
			if(visualStudio == null)
				throw new ArgumentNullException("visualStudio");

			if(menuCommandService == null)
				throw new ArgumentNullException("menuCommandService");

			if(settings == null)
				throw new ArgumentNullException("settings");

			if(xmlFormatterLocator == null)
				throw new ArgumentNullException("xmlFormatterLocator");

			this._menuCommandService = menuCommandService;
			this._settings = settings;
			this._visualStudio = visualStudio;
			this._xmlFormatterLocator = xmlFormatterLocator;

			this._settings.SavedSettingsToStorage += this.OnSavedSettingsToStorage;

			CommandID formatXmlSubmenuCommandId = new CommandID(Identifiers.CommandsGuid, Identifiers.FormatXmlSubmenuId);
			this._formatXmlSubmenu = new OleMenuCommand(this.OnInvokeFormatXmlSubmenuCommand, this.OnChangeFormatXmlSubmenuCommand, this.OnBeforeQueryStatusFormatXmlSubmenuCommand, formatXmlSubmenuCommandId);
			SetMenuCommandStatusInternal(this._formatXmlSubmenu, false);
			this._menuCommandService.AddCommand(this._formatXmlSubmenu);

			CommandID defaultCommandId = new CommandID(Identifiers.CommandsGuid, Identifiers.FormatXmlDefaultCommandId);
			this._defaultCommand = new OleMenuCommand(this.OnInvokeDefaultCommand, this.OnChangeDefaultCommand, this.OnBeforeQueryStatusDefaultCommand, defaultCommandId);
			SetMenuCommandStatusInternal(this._defaultCommand, false);
			this._menuCommandService.AddCommand(this._defaultCommand);

			this.AddCommandsInternal(this._settings);
		}

		#endregion

		#region Properties

		protected internal virtual TextDocument ActiveDocument
		{
			get
			{
				if(this._visualStudio.ActiveDocument == null)
					return null;

				return (TextDocument) this._visualStudio.ActiveDocument.Object();
			}
		}

		protected internal virtual bool ActiveDocumentIsXml
		{
			get { return this.ActiveDocument != null && this.ActiveDocument.IsXml(); }
		}

		#endregion

		#region Methods

		protected internal virtual void AddCommands(ISettings settings)
		{
			this.AddCommandsInternal(settings);
		}

		protected internal void AddCommandsInternal(ISettings settings)
		{
			if(settings == null)
				throw new ArgumentNullException("settings");

			if(!settings.Enabled)
				return;

			List<MenuCommand> commandList = new List<MenuCommand>();

			for(int i = 0; i < settings.XmlFormatSettingCollection.Count; i++)
			{
				var commandId = new CommandID(Identifiers.CommandsGuid, Identifiers.FormatXmlItemsCommandId + i);
				var menuCommand = new OleMenuCommand(this.OnInvokeFormatXmlCommand, this.OnChangeFormatXmlCommand, this.OnBeforeQueryStatusFormatXmlCommand, commandId, settings.XmlFormatSettingCollection[i].Name);
				this._menuCommandService.AddCommand(menuCommand);
				commandList.Add(menuCommand);
			}

			this._commands = commandList.ToArray();
		}

		protected internal virtual void ClearCommands()
		{
			foreach(var command in this._commands ?? new MenuCommand[0])
			{
				this._menuCommandService.RemoveCommand(command);
			}

			this._commands = null;
		}

		protected internal virtual void SetMenuCommandStatus(OleMenuCommand menuCommand, bool enabled)
		{
			SetMenuCommandStatusInternal(menuCommand, enabled);
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		protected internal static void SetMenuCommandStatusInternal(OleMenuCommand menuCommand, bool enabled)
		{
			if(menuCommand == null)
				throw new ArgumentNullException("menuCommand");

			menuCommand.Enabled = enabled;
			menuCommand.Visible = enabled;
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected internal virtual void FormatXml(IXmlFormatter xmlFormatter)
		{
			try
			{
				if(xmlFormatter == null)
					throw new ArgumentNullException("xmlFormatter");

				if(!this.ActiveDocumentIsXml)
					throw new InvalidOperationException("The current document is not XML.");

				TextDocument textDocument = this.ActiveDocument;
				EditPoint editPoint = textDocument.StartPoint.CreateEditPoint();

				editPoint.ReplaceText(textDocument.EndPoint, xmlFormatter.Format(editPoint.GetText(textDocument.EndPoint)), 0);
			}
			catch(Exception exception)
			{
				// ReSharper disable LocalizableElement
				MessageBox.Show(exception.ToString(), "Error formatting XML", MessageBoxButtons.OK);
				// ReSharper restore LocalizableElement
			}
		}

		protected internal virtual void OnBeforeQueryStatusDefaultCommand(object sender, EventArgs e)
		{
			OleMenuCommand menuCommand = sender as OleMenuCommand;

			if(menuCommand == null)
				return;

			this.SetMenuCommandStatus(menuCommand, this._settings.Enabled && this._xmlFormatterLocator.Default != null);
		}

		protected internal virtual void OnBeforeQueryStatusFormatXmlCommand(object sender, EventArgs e)
		{
			OleMenuCommand menuCommand = sender as OleMenuCommand;

			if(null == menuCommand)
				return;

			this.SetMenuCommandStatus(menuCommand, this._settings.Enabled && this._settings.XmlFormatSettingCollection.Any());
		}

		protected internal virtual void OnBeforeQueryStatusFormatXmlSubmenuCommand(object sender, EventArgs e)
		{
			OleMenuCommand menuCommand = sender as OleMenuCommand;

			if(menuCommand == null)
				return;

			this.SetMenuCommandStatus(menuCommand, this._settings.Enabled && this.ActiveDocumentIsXml);
		}

		protected internal virtual void OnChangeDefaultCommand(object sender, EventArgs e) {}
		protected internal virtual void OnChangeFormatXmlCommand(object sender, EventArgs e) {}
		protected internal virtual void OnChangeFormatXmlSubmenuCommand(object sender, EventArgs e) {}

		protected internal virtual void OnInvokeDefaultCommand(object sender, EventArgs e)
		{
			OleMenuCommand menuCommand = sender as OleMenuCommand;

			if(menuCommand == null)
				return;

			this.FormatXml(this._xmlFormatterLocator.Default);
		}

		protected internal virtual void OnInvokeFormatXmlCommand(object sender, EventArgs e)
		{
			OleMenuCommand menuCommand = sender as OleMenuCommand;

			if(menuCommand == null)
				return;

			this.FormatXml(this._xmlFormatterLocator.GetByName(menuCommand.Text));
		}

		protected internal virtual void OnInvokeFormatXmlSubmenuCommand(object sender, EventArgs e) {}

		protected internal virtual void OnSavedSettingsToStorage(object sender, SettingsEventArguments e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			this.ClearCommands();

			this.AddCommands(e.Settings);
		}

		#endregion
	}
}