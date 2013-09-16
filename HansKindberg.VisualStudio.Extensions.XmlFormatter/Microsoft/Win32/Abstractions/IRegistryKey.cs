using System;

// ReSharper disable CheckNamespace

namespace Microsoft.Win32.Abstractions // ReSharper restore CheckNamespace
{
	public interface IRegistryKey : IDisposable
	{
		#region Methods

		IRegistryKey CreateSubkey(string subkey);
		object GetValue(string name);
		IRegistryKey OpenSubkey(string name, bool writable);
		void SetValue(string name, object value);

		#endregion
	}
}