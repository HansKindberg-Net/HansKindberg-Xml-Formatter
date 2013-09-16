namespace HansKindberg.Xml.Formatting
{
	public interface IXmlFormatterFactory
	{
		#region Methods

		IXmlFormatter Create(IXmlFormat xmlFormat);

		#endregion
	}
}