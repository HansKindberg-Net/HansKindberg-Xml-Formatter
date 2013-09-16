using System;
using System.Configuration;
using HansKindberg.Configuration;

namespace HansKindberg.Xml.Formatting.Configuration
{
	public class XmlFormatElementCollection : ConfigurationElementCollection<XmlFormatElement>
	{
		#region Methods

		protected override object GetElementKey(ConfigurationElement element)
		{
			if(element == null)
				throw new ArgumentNullException("element");

			return ((XmlFormatElement) element).Name;
		}

		#endregion
	}
}