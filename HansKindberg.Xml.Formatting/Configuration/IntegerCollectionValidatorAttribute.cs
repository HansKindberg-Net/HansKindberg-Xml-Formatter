using System;
using System.Configuration;

namespace HansKindberg.Xml.Formatting.Configuration
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IntegerCollectionValidatorAttribute : ConfigurationValidatorAttribute
	{
		#region Fields

		private int _maximumLength = int.MaxValue;
		private int _minimumLength;

		#endregion

		#region Properties

		public int MaximumLength
		{
			get { return this._maximumLength; }
			set
			{
				if(this._minimumLength > value)
					throw new ArgumentOutOfRangeException("value", "The maximum length can not be less than the minimum length.");

				this._maximumLength = value;
			}
		}

		public int MinimumLength
		{
			get { return this._minimumLength; }
			set
			{
				if(this._maximumLength < value)
					throw new ArgumentOutOfRangeException("value", "The minimum length can not be greater than the maximum length.");

				this._minimumLength = value;
			}
		}

		public override ConfigurationValidatorBase ValidatorInstance
		{
			get { return new IntegerCollectionValidator(this._minimumLength, this._maximumLength); }
		}

		#endregion
	}
}