using System.Configuration;

namespace HansKindberg.Xml.Formatting.Configuration
{
	public class XmlFormatSection : ConfigurationSection
	{
		#region Fields

		private const string _defaultXmlFormatPropertyName = "defaultXmlFormat";
		private const string _xmlFormatsPropertyName = "xmlFormats";

		#endregion

		#region Properties

		[ConfigurationProperty(_defaultXmlFormatPropertyName, IsRequired = false)]
		public virtual string DefaultXmlFormat
		{
			get { return (string) base[_defaultXmlFormatPropertyName]; }
			set { base[_defaultXmlFormatPropertyName] = value; }
		}

		[ConfigurationProperty(_xmlFormatsPropertyName)]
		public virtual XmlFormatElementCollection XmlFormats
		{
			get { return (XmlFormatElementCollection) base[_xmlFormatsPropertyName]; }
		}

		#endregion
	}
}