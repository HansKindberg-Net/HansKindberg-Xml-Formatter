using System.Configuration;
using System.IO;
using System.Xml;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration
{
	public class XmlFormatSection : ConfigurationSection
	{
		#region Fields

		private const string _defaultXmlFormatPropertyName = "defaultXmlFormat";
		private const string _enabledPropertyName = "enabled";
		private const string _xmlFormatsPropertyName = "xmlFormats";
		private const string _xmlRootName = "xmlFormatSection";

		#endregion

		#region Properties

		[ConfigurationProperty(_defaultXmlFormatPropertyName, IsRequired = false)]
		public virtual string DefaultXmlFormat
		{
			get { return (string) this[_defaultXmlFormatPropertyName]; }
			set { this[_defaultXmlFormatPropertyName] = value; }
		}

		[ConfigurationProperty(_enabledPropertyName, DefaultValue = true)]
		public virtual bool Enabled
		{
			get { return (bool) this[_enabledPropertyName]; }
			set { this[_enabledPropertyName] = value; }
		}

		[ConfigurationProperty(_xmlFormatsPropertyName)]
		public virtual XmlFormatElementCollection XmlFormats
		{
			get { return (XmlFormatElementCollection) base[_xmlFormatsPropertyName]; }
		}

		#endregion

		#region Methods

		public virtual void FromXml(string xml)
		{
			StringReader stringReader = null;

			try
			{
				stringReader = new StringReader(xml);

				using(XmlTextReader xmlReader = new XmlTextReader(stringReader))
				{
					stringReader = null;
					this.DeserializeSection(xmlReader);
				}
			}
			finally
			{
				if(stringReader != null)
					stringReader.Dispose();
			}
		}

		public virtual string ToXml()
		{
			return this.SerializeSection(this, _xmlRootName, ConfigurationSaveMode.Full);
		}

		#endregion
	}
}