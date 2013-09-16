using System;
using System.Collections.Generic;
using HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration;
using HansKindberg.Xml.Formatting;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter
{
	public class XmlFormatterLocator : IXmlFormatterLocator
	{
		#region Fields

		private readonly object _lockObject = new object();
		private XmlFormatSection _xmlFormatSection;
		private readonly IXmlFormatterFactory _xmlFormatterFactory;
		private readonly IDictionary<string, IXmlFormatter> _xmlFormatters;

		#endregion

		#region Constructors

		public XmlFormatterLocator(IXmlFormatterFactory xmlFormatterFactory, XmlFormatSection xmlFormatSection)
		{
			if(xmlFormatterFactory == null)
				throw new ArgumentNullException("xmlFormatterFactory");

			ValidateXmlFormatSection(xmlFormatSection);

			this._xmlFormatSection = xmlFormatSection;
			this._xmlFormatterFactory = xmlFormatterFactory;
			this._xmlFormatters = new Dictionary<string, IXmlFormatter>(StringComparer.OrdinalIgnoreCase);
		}

		#endregion

		#region Properties

		public virtual IXmlFormatter Default
		{
			get
			{
				if(!this.Initialized)
					this.Initialize();

				return this.DefaultXmlFormatter;
			}
		}

		protected internal virtual IXmlFormatter DefaultXmlFormatter { get; set; }
		protected internal virtual bool Initialized { get; set; }

		protected internal virtual XmlFormatSection XmlFormatSection
		{
			get { return this._xmlFormatSection; }
			set { this._xmlFormatSection = value; }
		}

		protected internal virtual IXmlFormatterFactory XmlFormatterFactory
		{
			get { return this._xmlFormatterFactory; }
		}

		protected internal virtual IDictionary<string, IXmlFormatter> XmlFormatters
		{
			get { return this._xmlFormatters; }
		}

		#endregion

		#region Methods

		public virtual IXmlFormatter GetByName(string name)
		{
			if(name == null)
				throw new ArgumentNullException("name");

			if(!this.Initialized)
				this.Initialize();

			return this.XmlFormatters.ContainsKey(name) ? this.XmlFormatters[name] : null;
		}

		protected internal virtual void Initialize()
		{
			lock(this._lockObject)
			{
				this.XmlFormatters.Clear();

				foreach(IXmlFormat xmlFormat in this.XmlFormatSection.XmlFormats)
				{
					IXmlFormatter xmlFormatter = this.XmlFormatterFactory.Create(xmlFormat);
					this.XmlFormatters.Add(xmlFormat.Name, xmlFormatter);
				}

				this.DefaultXmlFormatter = this.XmlFormatSection.DefaultXmlFormat != null && this.XmlFormatters.ContainsKey(this.XmlFormatSection.DefaultXmlFormat) ? this.XmlFormatters[this.XmlFormatSection.DefaultXmlFormat] : null;

				this.Initialized = true;
			}
		}

		public virtual void Refresh(XmlFormatSection xmlFormatSection)
		{
			ValidateXmlFormatSection(xmlFormatSection);
			this.XmlFormatSection = xmlFormatSection;
			this.Initialize();
		}

		private static void ValidateXmlFormatSection(XmlFormatSection xmlFormatSection)
		{
			if(xmlFormatSection == null)
				throw new ArgumentNullException("xmlFormatSection");
		}

		#endregion
	}
}