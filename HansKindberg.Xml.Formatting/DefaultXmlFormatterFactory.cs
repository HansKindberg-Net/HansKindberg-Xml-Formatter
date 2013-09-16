namespace HansKindberg.Xml.Formatting
{
	public class DefaultXmlFormatterFactory : IXmlFormatterFactory
	{
		#region Methods

		public virtual IXmlFormatter Create(IXmlFormat xmlFormat)
		{
			return new DefaultXmlFormatter(xmlFormat);
		}

		#endregion
	}
}