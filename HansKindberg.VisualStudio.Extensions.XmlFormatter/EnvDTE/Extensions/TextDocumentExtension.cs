using System;

// ReSharper disable CheckNamespace

namespace EnvDTE.Extensions // ReSharper restore CheckNamespace
{
	public static class TextDocumentExtension
	{
		#region Methods

		public static bool IsXml(this TextDocument textDocument)
		{
			if(textDocument == null)
				throw new ArgumentNullException("textDocument");

			return textDocument.Language.Equals("xml", StringComparison.OrdinalIgnoreCase);
		}

		#endregion
	}
}