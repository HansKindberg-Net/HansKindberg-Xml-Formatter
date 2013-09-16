using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Tools.Options.Components
{
	public class XmlFormatSettingNameSelector : ObjectSelectorEditor
	{
		#region Methods

		protected override void FillTreeWithData(Selector selector, ITypeDescriptorContext context, IServiceProvider provider)
		{
			if(selector == null)
				throw new ArgumentNullException("selector");

			if(context == null)
				throw new ArgumentNullException("context");

			base.FillTreeWithData(selector, context, provider);

			Settings settings = context.Instance as Settings;

			selector.AddNode(string.Empty, string.Empty, null);

			if(settings == null)
				return;

			foreach(var xmlFormatSetting in settings.XmlFormatSettingCollection)
			{
				selector.AddNode(xmlFormatSetting.Name, xmlFormatSetting.Name, null);
			}
		}

		#endregion
	}
}