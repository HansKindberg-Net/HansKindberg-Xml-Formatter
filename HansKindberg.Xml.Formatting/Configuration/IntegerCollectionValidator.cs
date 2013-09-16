using System;
using System.Configuration;
using System.Globalization;

namespace HansKindberg.Xml.Formatting.Configuration
{
	public class IntegerCollectionValidator : ConfigurationValidatorBase
	{
		#region Fields

		public const string Separator = ",";
		private readonly int _maximumLength;
		private readonly int _minimumLength;

		#endregion

		#region Constructors

		public IntegerCollectionValidator() : this(0) {}
		public IntegerCollectionValidator(int minimumLength) : this(minimumLength, int.MaxValue) {}

		public IntegerCollectionValidator(int minimumLength, int maximumLength)
		{
			this._minimumLength = minimumLength;
			this._maximumLength = maximumLength;
		}

		#endregion

		#region Methods

		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		public override void Validate(object value)
		{
			new StringValidator(this._minimumLength, this._maximumLength).Validate(value);

			string valueString = value as string;

			if(string.IsNullOrEmpty(valueString))
				return;

			try
			{
				foreach(string item in valueString.Split(Separator.ToCharArray()))
				{
					string integerString = item.Trim();
					int integer;

					if(!int.TryParse(integerString, out integer))
						throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture, "The value \"{0}\" is not parseable to \"{1}\".", integerString, typeof(int).FullName));
				}
			}
			catch(Exception exception)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The value \"{0}\" is invalid.", value), "value", exception);
			}
		}

		#endregion
	}
}