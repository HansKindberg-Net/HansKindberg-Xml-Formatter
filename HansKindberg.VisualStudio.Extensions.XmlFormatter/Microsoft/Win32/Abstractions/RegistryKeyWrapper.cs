using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable CheckNamespace

namespace Microsoft.Win32.Abstractions // ReSharper restore CheckNamespace
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper for a disposable object. This wrapper disposes the wrapped object.")]
	public class RegistryKeyWrapper : IRegistryKey
	{
		#region Fields

		private readonly RegistryKey _registryKey;

		#endregion

		#region Constructors

		public RegistryKeyWrapper(RegistryKey registryKey)
		{
			if(registryKey == null)
				throw new ArgumentNullException("registryKey");

			this._registryKey = registryKey;
		}

		#endregion

		#region Methods

		public virtual IRegistryKey CreateSubkey(string subkey)
		{
			return (RegistryKeyWrapper) this._registryKey.CreateSubKey(subkey);
		}

		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper for a disposable object. This wrapper disposes the wrapped object.")]
		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper for a disposable object. This wrapper disposes the wrapped object.")]
		public virtual void Dispose()
		{
			this._registryKey.Dispose();
		}

		public static RegistryKeyWrapper FromRegistryKey(RegistryKey registryKey)
		{
			return registryKey;
		}

		public virtual object GetValue(string name)
		{
			return this._registryKey.GetValue(name);
		}

		public virtual IRegistryKey OpenSubkey(string name, bool writable)
		{
			return (RegistryKeyWrapper) this._registryKey.OpenSubKey(name, writable);
		}

		public virtual void SetValue(string name, object value)
		{
			this._registryKey.SetValue(name, value);
		}

		#endregion

		#region Implicit operators

		public static implicit operator RegistryKeyWrapper(RegistryKey registryKey)
		{
			return registryKey == null ? null : new RegistryKeyWrapper(registryKey);
		}

		#endregion
	}
}