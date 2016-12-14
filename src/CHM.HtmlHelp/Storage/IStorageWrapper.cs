using System;
using System.Runtime.InteropServices;

namespace HtmlHelp.Storage
{
	/// <summary>
	/// The class <c>IStorageWrapper</c> extends <c>IBaseStorageWrapper</c> and adds functionality for 
	/// the interface IStorage.
	/// </summary>
    public class IStorageWrapper : IBaseStorageWrapper
    {
		/// <summary>
		/// Constructor of the class
		/// </summary>
		/// <param name="workPath">workpath of the storage</param>
		/// <param name="enumStorage">true if the storage should be enumerated automatically</param>
        public IStorageWrapper(string workPath, bool enumStorage)
        {
            Interop.StgOpenStorage(workPath, null, 16, IntPtr.Zero, 0, out storage);
            IBaseStorageWrapper.BaseUrl = workPath;
            STATSTG sTATSTG = new STATSTG();
            storage.Stat(out sTATSTG, 1);
			if(enumStorage)
			{
				base.EnumIStorageObject();
			}
        }

		/// <summary>
		/// Enumerates an IStorage object and creates the file object collection
		/// </summary>
		public override void EnumIStorageObject()
		{
			base.EnumIStorageObject();
		}
    }

}
