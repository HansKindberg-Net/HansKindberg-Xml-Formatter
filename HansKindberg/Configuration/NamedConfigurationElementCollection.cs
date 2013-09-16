using System;
using System.Configuration;

namespace HansKindberg.Configuration
{
	//[Obsolete("This assembly will later exist at https://github.com/HansKindberg-Net/HansKindberg, change the references to that NuGet-package later.")]
	[Obsolete("Remove this, it does not work as you expected. Inherit from HansKindberg.Configuration.ConfigurationElementCollection instead and implement Name i each class.", true)]
	public abstract class NamedConfigurationElementCollection<T> : ConfigurationElementCollection<T> where T : NamedConfigurationElement, new()
	{
		#region Methods

		protected override object GetElementKey(ConfigurationElement element)
		{
			if(element == null)
				throw new ArgumentNullException("element");

			return ((T) element).Name;
		}

		#endregion
	}
}