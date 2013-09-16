using System;

namespace HansKindberg
{
	[Obsolete("This assembly will later exist at https://github.com/HansKindberg-Net/HansKindberg, change the references to that NuGet-package later.")]
	public class ValueContainer<T>
	{
		#region Fields

		private T _value;

		#endregion

		//public ValueContainer() {}

		#region Constructors

		public ValueContainer(T value)
		{
			this._value = value;
		}

		#endregion

		#region Properties

		public virtual T Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		#endregion
	}
}