using System.Configuration;

namespace HansKindberg.VisualStudio.Extensions.XmlFormatter.Configuration
{
	public class XmlFormatElement : HansKindberg.Xml.Formatting.Configuration.XmlFormatElement
	{
		#region Fields

		private const string _invalidNameCharacters = "~!@#$%^&*()[]{}/;'\"|\\";

		#endregion

		#region Properties

		public override string Name
		{
			get { return base.Name ?? string.Empty; }
			set
			{
				if(!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(value.Trim()))
					throw new ConfigurationErrorsException("The name can not only consist of whitespace characters.");

				base.Name = value;
			}
		}

		#endregion

		#region Methods

		protected internal override ConfigurationValidatorBase CreateNameConfigurationPropertyValidator()
		{
			return this.CreateNameConfigurationPropertyValidator(_invalidNameCharacters);
		}

		#endregion
	}
}