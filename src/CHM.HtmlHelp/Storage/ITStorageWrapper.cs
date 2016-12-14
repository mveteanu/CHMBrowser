using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HtmlHelp.Storage
{
	/// <summary>
	/// The class <c>ITStorageWrapper</c> extends <c>IBaseStorageWrapper</c> and adds functionality for 
	/// the interface ITStorage.
	/// </summary>
    public class ITStorageWrapper : IBaseStorageWrapper
    {
		/// <summary>
		/// Internal UCOM storage
		/// </summary>
        private Interop.UCOMITStorage comITStorage;
		/// <summary>
		/// Internal storage
		/// </summary>
        private Interop.ITStorage comITStorageInterfaced;


		/// <summary>
		/// Constructor of the class
		/// </summary>
		/// <param name="workPath">workpath of the storage</param>
		/// <param name="enumStorage">true if the storage should be enumerated automatically</param>
        public ITStorageWrapper(string workPath, bool enumStorage)
        {
            comITStorage = new Interop.UCOMITStorage();
            comITStorageInterfaced = (Interop.ITStorage)comITStorage;
            storage = comITStorageInterfaced.StgOpenStorage(workPath, IntPtr.Zero, 32, IntPtr.Zero, 0);
            IBaseStorageWrapper.BaseUrl = workPath;
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

		/// <summary>
		/// Opens a sub storage. Call this if an enumerated file object is of type storage
		/// </summary>
		/// <param name="parentStorage">parent storage of the sub storage file</param>
		/// <param name="storageName">name of the storage</param>
		/// <returns>Returns a reference to the sub storage</returns>
		public Interop.IStorage OpenSubStorage(Interop.IStorage parentStorage, string storageName)
		{
			if(parentStorage == null)
				parentStorage = storage;

			Interop.IStorage retObject = null;

			STATSTG sTATSTG;
			sTATSTG.pwcsName = storageName;
			sTATSTG.type = 1;

			try
			{
				Interop.IStorage iStorage = parentStorage.OpenStorage(sTATSTG.pwcsName, IntPtr.Zero, 16, IntPtr.Zero, 0);

				retObject = iStorage;
			}
			catch(Exception ex)
			{
				retObject = null;

				Debug.WriteLine("ITStorageWrapper.OpenSubStorage() - Failed for storage '" + storageName + "'");
				Debug.Indent();
				Debug.WriteLine("Exception: " + ex.Message);
				Debug.Unindent();
			}

			return retObject;
		}

		/// <summary>
		/// Opens an UCOMIStream and returns the associated file object
		/// </summary>
		/// <param name="parentStorage">storage used to open the stream</param>
		/// <param name="fileName">filename of the stream</param>
		/// <returns>A <see cref="FileObject">FileObject</see> instance if the file was found, otherwise null.</returns>
		public FileObject OpenUCOMStream(Interop.IStorage parentStorage, string fileName)
		{
			if(parentStorage == null)
				parentStorage = storage;

			FileObject retObject = null;

			STATSTG sTATSTG;
			sTATSTG.pwcsName = fileName;
			sTATSTG.type = 2;

			try
			{
				retObject = new FileObject();
				
				UCOMIStream uCOMIStream = parentStorage.OpenStream(sTATSTG.pwcsName, IntPtr.Zero, 16, 0);

				if(uCOMIStream != null)
				{
					retObject.FileType = sTATSTG.type;
					retObject.FilePath = "";
					retObject.FileName = sTATSTG.pwcsName.ToString();
					retObject.FileStream = uCOMIStream;
				} 
				else 
				{
					retObject = null;
				}
			}
			catch(Exception ex)
			{
				retObject = null;

				Debug.WriteLine("ITStorageWrapper.OpenUCOMStream() - Failed for file '" + fileName + "'");
				Debug.Indent();
				Debug.WriteLine("Exception: " + ex.Message);
				Debug.Unindent();
			}

			return retObject;
		}
    }

}
