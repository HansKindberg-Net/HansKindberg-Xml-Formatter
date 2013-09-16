using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace HansKindberg.Configuration
{
	//[Obsolete("This assembly will later exist at https://github.com/HansKindberg-Net/HansKindberg, change the references to that NuGet-package later.")]
	[Obsolete("Remove this, it does not work as you expected. Inherit from HansKindberg.Configuration.ConfigurationElementCollection instead and implement Name i each class.", true)]
	public abstract class NamedConfigurationElement : ConfigurationElement
	{
		#region Fields

		protected internal const string InvalidNameCharacters = " ~!@#$%^&*()[]{}/;'\"|\\";
		private static readonly object _lockObject = new object();
		private static readonly ConfigurationProperty _nameConfigurationProperty = new ConfigurationProperty(_namePropertyName, typeof(string), null, null, new StringValidator(1, int.MaxValue, InvalidNameCharacters), ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
		private const string _namePropertyName = "name";
		private static readonly IDictionary<Type, bool> _propertiesAreInitialized = new Dictionary<Type, bool>();

		#endregion

		#region Properties

		public virtual string Name
		{
			get { return (string) this[_namePropertyName]; }
			set
			{
				if(value == null)
					throw new ConfigurationErrorsException("The value can not be null.", new ArgumentNullException("value"));

				this[_namePropertyName] = value;
			}
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if(!_propertiesAreInitialized.ContainsKey(this.GetType()) || !_propertiesAreInitialized[this.GetType()])
				{
					lock(_lockObject)
					{
						if(!_propertiesAreInitialized.ContainsKey(this.GetType()) || !_propertiesAreInitialized[this.GetType()])
						{
							if(!base.Properties.Contains(_namePropertyName))
							{
								// We want to have the name property first.
								List<ConfigurationProperty> configurationPropertyList = new List<ConfigurationProperty>(base.Properties.Cast<ConfigurationProperty>());
								base.Properties.Clear();
								base.Properties.Add(_nameConfigurationProperty);
								foreach(ConfigurationProperty configurationProperty in configurationPropertyList)
								{
									base.Properties.Add(configurationProperty);
								}
							}

							_propertiesAreInitialized.Add(this.GetType(), true);
						}
					}
				}

				return base.Properties;
			}
		}

		#endregion
	}
}