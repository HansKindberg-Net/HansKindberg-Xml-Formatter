using System;
using System.Xml.Linq;

namespace HansKindberg.Xml.Linq
{
	public abstract class InternalXObjectWrapper
	{
		#region Fields

		private readonly XObject _internalXObject;

		#endregion

		#region Constructors

		protected InternalXObjectWrapper(XObject internalXObject)
		{
			if(internalXObject == null)
				throw new ArgumentNullException("internalXObject");

			this._internalXObject = internalXObject;
		}

		#endregion

		#region Properties

		protected internal XObject InternalXObject
		{
			get { return this._internalXObject; }
		}

		#endregion
	}
}