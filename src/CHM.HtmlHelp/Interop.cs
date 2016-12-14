using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace HtmlHelp
{
	/// <summary>
	/// The class <c>Interop</c> imports API-Methods from various dlls and defines some 
	/// structures used by this methods.
	/// </summary>
	[System.Security.SuppressUnmanagedCodeSecurityAttribute()]
	public class Interop
	{
		/// <summary>
		/// Delegate for default window procedure
		/// </summary>
		public delegate int WNDPROC(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Static constructor
		/// </summary>
		static Interop()
		{
			WINTRUST_ACTION_GENERIC_VERIFY_V2 = new Guid("00AAC56B-CD44-11d0-8CC2-00C04FC295EE");
		}

		#region static constants
		/// <summary>
		/// WINTRUST_ACTION_GENERIC_VERIFY_V2 constant
		/// </summary>
		public static Guid WINTRUST_ACTION_GENERIC_VERIFY_V2;
		/// <summary>
		/// Guid for IID_IUnknown
		/// </summary>
		public static Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
		/// <summary>
		/// NULL pointer
		/// </summary>
		public static IntPtr NullIntPtr = ((IntPtr)((int)(0)));
		#endregion

		#region Imported constants
		/// <summary>
		/// Constant imports
		/// </summary>
		public const uint
			S_OK = 0,
			STG_E_FILENOTFOUND = 2147680258,
			STG_E_INVALIDNAME = 2147680508;

		/// <summary>
		/// Constant imports
		/// </summary>
		public const int
			STGTY_STORAGE = 1,
			STGTY_STREAM = 2,
			STGTY_LOCKBYTES = 3,
			STGTY_PROPERTY = 4,

			COMPACT_DATA = 0,
			COMPACT_DATA_AND_PATH = 1,
			
			WM_USER = 1024,
			WM_CHAR = 258,
			WM_CLOSE = 16,
			WM_COMMAND = 273,
			WM_CREATE = 1,
			WM_DESTROY = 2,
			WM_ERASEBKGND = 20,
			WM_KILLFOCUS = 8,
			WM_LBUTTONDBLCLK = 515,
			WM_LBUTTONDOWN = 513,
			WM_LBUTTONUP = 514,
			WM_MBUTTONDBLCLK = 521,
			WM_MBUTTONDOWN = 519,
			WM_MBUTTONUP = 520,
			WM_MOUSEMOVE = 512,
			WM_NCLBUTTONDOWN = 161,
			WM_NCLBUTTONUP = 162,
			WM_NCPAINT = 133,
			WM_NOTIFY = 78,
			WM_PAINT = 15,
			WM_SETFOCUS = 7,
			WM_TIMER = 275,
			
			HWND_TOPMOST = -1,
			SW_SHOWNORMAL = 1,
			
			WS_EX_LAYERED = 524288,
			WS_EX_TOOLWINDOW = 128,
			WS_EX_TOPMOST = 8,
			WS_HSCROLL = 1048576,
			WS_OVERLAPPEDWINDOW = 13565952,
			WS_POPUP = int.MinValue,
			WS_VISIBLE = 268435456,
			WS_VSCROLL = 2097152,
			WS_CHILD = 0x40000000,
			WS_BORDER = 0x00800000,
			WS_EX_WINDOWEDGE = 0x00000100,
			CS_DROPSHADOW = 131072;

		#endregion

		#region structure/class imports

		/// <summary>
		/// ITStorage control data struct
		/// </summary>
		[ComVisible(false), StructLayout(LayoutKind.Sequential)]
		public struct ITS_Control_Data
		{
			/// <summary>
			/// Controldata flag
			/// </summary>
			public int cdwControlData;
			/// <summary>
			/// Controldata flag
			/// </summary>
			public int adwControlData;
		}

		/// <summary>
		/// MSG struct implementation
		/// </summary>
		[ComVisibleAttribute(false)]
			public struct MSG
		{
			/// <summary>
			/// See MSG in Plattform SDK documentation
			/// </summary>
			public IntPtr hwnd;
			/// <summary>
			/// See MSG in Plattform SDK documentation
			/// </summary>
			public int message;
			/// <summary>
			/// See MSG in Plattform SDK documentation
			/// </summary>
			public IntPtr wParam;
			/// <summary>
			/// See MSG in Plattform SDK documentation
			/// </summary>
			public IntPtr lParam;
			/// <summary>
			/// See MSG in Plattform SDK documentation
			/// </summary>
			public int time;
			/// <summary>
			/// See MSG in Plattform SDK documentation
			/// </summary>
			public int pt_x;
			/// <summary>
			/// See MSG in Plattform SDK documentation
			/// </summary>
			public int pt_y;

		}

		/// <summary>
		/// WNDCLASS struct implementation
		/// </summary>
		[ComVisibleAttribute(false)]
			[StructLayoutAttribute(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class WNDCLASS
		{
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public int style = 0;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public Interop.WNDPROC lpfnWndProc = null;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public int cbClsExtra = 0;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public int cbWndExtra = 0;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public IntPtr hInstance = IntPtr.Zero;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public IntPtr hIcon = IntPtr.Zero;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public IntPtr hCursor = IntPtr.Zero;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public IntPtr hbrBackground = IntPtr.Zero;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public string lpszMenuName = null;
			/// <summary>
			/// See WNDCLASS in Plattform SDK documentation
			/// </summary>
			public string lpszClassName = null;

		}

		/// <summary>
		/// PAINTSTRUCT struct implementation
		/// </summary>
		[ComVisibleAttribute(false)]
			public struct PAINTSTRUCT
		{
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public IntPtr hdc;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public bool fErase;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int rcPaint_left;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int rcPaint_top;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int rcPaint_right;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int rcPaint_bottom;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public bool fRestore;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public bool fIncUpdate;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int reserved1;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int reserved2;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int reserved3;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int reserved4;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int reserved5;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int reserved6;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int reserved7;
			/// <summary>
			/// See PAINTSTRUCT in Plattform SDK documentation
			/// </summary>
			public int reserved8;

		}

		#endregion

		#region ole32.dll imports

		/// <summary>
		/// Imports the ole32.dll function StgOpenStorage
		/// </summary>
		/// <param name="wcsName">storage name</param>
		/// <param name="pstgPriority">Points to previous opening of the storage object</param>
		/// <param name="grfMode">Access mode for the new storage object</param>
		/// <param name="snbExclude">Points to a block of stream names in the storage object</param>
		/// <param name="reserved">Reserved; must be zero</param>
		/// <param name="storage">out parameter returning the storage</param>
		/// <returns>Returns S_OK if succeeded</returns>
		[DllImportAttribute("Ole32.dll")]
		public static extern int StgOpenStorage([MarshalAs(UnmanagedType.LPWStr)] string wcsName, IStorage pstgPriority, int grfMode, IntPtr snbExclude, int reserved, out IStorage storage);

		#endregion

		#region user32.dll imports
		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		public static extern IntPtr CreateWindowEx(int dwExStyle, string lpszClassName, string lpszWindowName, int style, int x, int y, int width, int height, IntPtr hWndParent, IntPtr hMenu, IntPtr hInst, [MarshalAs(UnmanagedType.AsAny)] object pvParam);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto)]
		public static extern int DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern bool DestroyWindow(IntPtr hWnd);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto)]
		public static extern int DispatchMessage(ref MSG msg);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern IntPtr GetLastActivePopup(IntPtr hWnd);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		public static extern bool GetMessage(ref MSG msg, IntPtr hwnd, int minFilter, int maxFilter);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern int GetMessagePos();

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern bool KillTimer(IntPtr hwnd, int idEvent);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool PeekMessage(out MSG msg, IntPtr hwnd, int msgMin, int msgMax, int remove);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto)]
		public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern void PostQuitMessage(int nExitCode);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		public static extern IntPtr RegisterClass(WNDCLASS wc);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		public static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto)]
		public static extern int RegisterWindowMessage(string msg);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern int SetTimer(IntPtr hWnd, int nIDEvent, int uElapse, IntPtr lpTimerFunc);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, 
			int x, int y, int cx, int cy, int flags);
		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern bool TranslateMessage(ref MSG msg);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool UpdateWindow(IntPtr hWnd);

		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		#endregion

		#region kernel32.dll imports
		/// <summary>
		/// Search for function name in Plattform SDK documentation
		/// </summary>
		[DllImportAttribute("kernel32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string moduleName);
		#endregion

		#region storage interface imports
		/// <summary>
		/// Imports the OLE interface IEnumSTATSG
		/// </summary>
		[GuidAttribute("0000000D-0000-0000-C000-000000000046")]
			[SuppressUnmanagedCodeSecurityAttribute()]
			[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
			[ComImportAttribute()]
			public interface IEnumSTATSTG
		{
			/// <summary>
			/// Retrieves the next celt items in the enumeration sequence. 
			/// If there are fewer than the requested number of elements left in the sequence, 
			/// it retrieves the remaining elements. 
			/// The number of elements actually retrieved is returned through pceltFetched 
			/// (unless the caller passed in NULL for that parameter).
			/// </summary>
			/// <param name="celt">Number of objects to retreive</param>
			/// <param name="rgVar">Array of STATSG elements</param>
			/// <param name="pceltFetched">Number of elements actually supplied</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int Next(int celt, out STATSTG rgVar, out int pceltFetched);

			/// <summary>
			/// Skips over the next specified number of elements in the enumeration sequence.
			/// </summary>
			/// <param name="celt">Number of elements to skip</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int Skip(int celt);

			/// <summary>
			/// Resets the enumeration sequence to the beginning.
			/// </summary>
			/// <returns>Returns S_OK if succeeded</returns>
			int Reset();

			/// <summary>
			/// Creates another enumerator that contains the same enumeration state 
			/// as the current one. Using this function, a client can record a 
			/// particular point in the enumeration sequence, and then return 
			/// to that point at a later time. The new enumerator supports the same 
			/// interface as the original one.
			/// </summary>
			/// <param name="newEnum">Output parameter of the new enumerator</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int Clone(out IEnumSTATSTG newEnum);
		}

		/// <summary>
		/// The ILockBytes interface is implemented on a byte array object that 
		/// is backed by some physical storage, such as a disk file, global memory, 
		/// or a database. It is used by a COM compound file storage object to give 
		/// its root storage access to the physical device, while isolating the root 
		/// storage from the details of accessing the physical storage.
		/// </summary>
		[GuidAttribute("0000000a-0000-0000-C000-000000000046")]
			[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
			[ComImportAttribute()]
			public interface ILockBytes
		{
			/// <summary>
			/// Reads a specified number of bytes starting at a specified offset 
			/// from the beginning of the byte array object.
			/// </summary>
			/// <param name="ulOffset">Specifies the starting point for reading data</param>
			/// <param name="pv">Points to the buffer into which the data is read</param>
			/// <param name="cb">Specifies the number of bytes to read</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int ReadAt(long ulOffset, out IntPtr pv, int cb);

			/// <summary>
			/// Writes the specified number of bytes starting at a specified offset 
			/// from the beginning of the byte array.
			/// </summary>
			/// <param name="ulOffset">Specifies the starting point for writing data</param>
			/// <param name="pv">Points to the buffer containing the data to be written</param>
			/// <param name="cb">Specifies the number of bytes to write</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int WriteAt(long ulOffset, IntPtr pv, int cb);

			/// <summary>
			/// Ensures that any internal buffers maintained by the ILockBytes 
			/// implementation are written out to the underlying physical storage.
			/// </summary>
			void Flush();

			/// <summary>
			/// Changes the size of the byte array.
			/// </summary>
			/// <param name="cb">Specifies the new size of the byte array in bytes</param>
			void SetSize(long cb);

			/// <summary>
			/// Restricts access to a specified range of bytes in the byte array.
			/// </summary>
			/// <param name="libOffset">Specifies the byte offset for the beginning of the range</param>
			/// <param name="cb">Specifies the length of the range in bytes</param>
			/// <param name="dwLockType">Specifies the type of restriction on accessing the specified range</param>
			void LockRegion(long libOffset, long cb, int dwLockType);

			/// <summary>
			/// Removes the access restriction on a previously locked range of bytes.
			/// </summary>
			/// <param name="libOffset">Specifies the byte offset for the beginning of the range</param>
			/// <param name="cb">Specifies the length of the range in bytes</param>
			/// <param name="dwLockType">Specifies the access restriction previously placed on the range</param>
			void UnlockRegion(long libOffset, long cb, int dwLockType);

			/// <summary>
			/// Retrieves a STATSTG structure containing information for this byte array object.
			/// </summary>
			/// <param name="pstatstg">Location for STATSTG structure</param>
			/// <param name="grfStatFlag">Values taken from the STATFLAG enumeration</param>
			void Stat(ref STATSTG pstatstg, int grfStatFlag);
		}

		/// <summary>
		/// The IStorage interface supports the creation and management of structured storage objects. 
		/// Structured storage allows hierarchical storage of information within a single file, and 
		/// is often referred to as "a file system within a file". Elements of a structured storage 
		/// object are storages and streams. Storages are analogous to directories, and streams are 
		/// analogous to files. Within a structured storage there will be a primary storage object that 
		/// may contain substorages, possibly nested, and streams. Storages provide the structure of the 
		/// object, and streams contain the data, which is manipulated through the IStream interface.
		/// </summary>
		[SuppressUnmanagedCodeSecurityAttribute()]
			[GuidAttribute("0000000B-0000-0000-C000-000000000046")]
			[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
			[ComImportAttribute()]
			public interface IStorage
		{

			/// <summary>
			/// Creates and opens a stream object with the specified name contained in this storage object. 
			/// All elements within a storage object — both streams and other storage objects — are kept in 
			/// the same name space.
			/// </summary>
			/// <param name="pwcsName">Name of the new stream</param>
			/// <param name="grfMode">Access mode for the new stream</param>
			/// <param name="reserved1">Reserved; must be zero</param>
			/// <param name="reserved2">Reserved; must be zero</param>
			/// <returns>Returns S_OK if succeeded</returns>
			[return: MarshalAs(UnmanagedType.Interface)]
			UCOMIStream CreateStream([MarshalAs(UnmanagedType.BStr)] string pwcsName, [MarshalAs(UnmanagedType.U4)] int grfMode, [MarshalAs(UnmanagedType.U4)] int reserved1, [MarshalAs(UnmanagedType.U4)] int reserved2);

			/// <summary>
			/// Opens an existing stream object within this storage object in the specified access mode.
			/// </summary>
			/// <param name="pwcsName">Name of the stream</param>
			/// <param name="reserved1">Reserved; must be NULL</param>
			/// <param name="grfMode">Access mode for the new stream</param>
			/// <param name="reserved2">Reserved; must be zero</param>
			/// <returns>Returns S_OK if succeeded</returns>
			[return: MarshalAs(UnmanagedType.Interface)]
			UCOMIStream OpenStream([MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr reserved1, [MarshalAs(UnmanagedType.U4)] int grfMode, [MarshalAs(UnmanagedType.U4)] int reserved2);

			/// <summary>
			/// Creates and opens a new storage object nested within this storage object.
			/// </summary>
			/// <param name="pwcsName">Name of the new storage object</param>
			/// <param name="grfMode">Access mode for the new storage object</param>
			/// <param name="reserved1">Reserved; must be zero</param>
			/// <param name="reserved2">Reserved; must be zero</param>
			/// <returns>Returns S_OK if succeeded</returns>
			[return: MarshalAs(UnmanagedType.Interface)]
			IStorage CreateStorage([MarshalAs(UnmanagedType.BStr)] string pwcsName, [MarshalAs(UnmanagedType.U4)] int grfMode, [MarshalAs(UnmanagedType.U4)] int reserved1, [MarshalAs(UnmanagedType.U4)] int reserved2);

			/// <summary>
			/// Opens an existing storage object with the specified name in the specified access mode.
			/// </summary>
			/// <param name="pwcsName">Name of the storage</param>
			/// <param name="pstgPriority">Points to previous opening of the storage object</param>
			/// <param name="grfMode">Access mode for the new storage object</param>
			/// <param name="snbExclude">Points to a block of stream names in the storage object</param>
			/// <param name="reserved">Reserved; must be zero</param>
			/// <returns>Returns S_OK if succeeded</returns>
			[return: MarshalAs(UnmanagedType.Interface)]
			IStorage OpenStorage([MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr pstgPriority, [MarshalAs(UnmanagedType.U4)] int grfMode, IntPtr snbExclude, [MarshalAs(UnmanagedType.U4)] int reserved);

			/// <summary>
			/// Copies the entire contents of an open storage object to another storage object.
			/// </summary>
			/// <param name="ciidExclude">Number of elements in rgiidExclude</param>
			/// <param name="rgiidExclude">Array of interface identifiers (IIDs)</param>
			/// <param name="snbExclude">Points to a block of stream names in the storage object</param>
			/// <param name="pstgDest">Points to destination storage object</param>
			void CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] Guid[] rgiidExclude, IntPtr snbExclude, [MarshalAs(UnmanagedType.Interface)] IStorage pstgDest);

			/// <summary>
			/// Copies or moves a substorage or stream from this storage object to another storage object.
			/// </summary>
			/// <param name="pwcsName">Name of the element to be moved</param>
			/// <param name="pstgDest">Points to destination storage object</param>
			/// <param name="pwcsNewName">Points to new name of element in destination</param>
			/// <param name="grfFlags">Specifies a copy or a move</param>
			void MoveElementTo([MarshalAs(UnmanagedType.BStr)] string pwcsName, [MarshalAs(UnmanagedType.Interface)] IStorage pstgDest, [MarshalAs(UnmanagedType.BStr)] string pwcsNewName, [MarshalAs(UnmanagedType.U4)] int grfFlags);

			/// <summary>
			/// Ensures that any changes made to a storage object open in transacted mode are reflected in 
			/// the parent storage; for a root storage, reflects the changes in the actual device, for 
			/// example, a file on disk. For a root storage object opened in direct mode, this method has no 
			/// effect except to flush all memory buffers to the disk. For non-root storage objects in direct mode, 
			/// this method has no effect.
			/// </summary>
			/// <param name="grfCommitFlags">Specifies how changes are to be committed</param>
			void Commit(int grfCommitFlags);

			/// <summary>
			/// Discards all changes that have been made to the storage object since the last commit.
			/// </summary>
			void Revert();

			/// <summary>
			/// Retrieves a pointer to an enumerator object that can be used to enumerate the storage and stream 
			/// objects contained within this storage object.
			/// </summary>
			/// <param name="reserved1">Reserved; must be zero</param>
			/// <param name="reserved2">Reserved; must be NULL</param>
			/// <param name="reserved3">Reserved; must be zero</param>
			/// <param name="ppenum">Output variable that receives the IEnumSTATSTG interface</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int EnumElements([MarshalAs(UnmanagedType.U4)] int reserved1, IntPtr reserved2, [MarshalAs(UnmanagedType.U4)] int reserved3, [MarshalAs(UnmanagedType.Interface)] out IEnumSTATSTG ppenum);

			/// <summary>
			/// Removes the specified storage or stream from this storage object.
			/// </summary>
			/// <param name="pwcsName">Name of the element to be removed</param>
			void DestroyElement([MarshalAs(UnmanagedType.BStr)] string pwcsName);

			/// <summary>
			/// Renames the specified substorage or stream in this storage object.
			/// </summary>
			/// <param name="pwcsOldName">Old name of the element</param>
			/// <param name="pwcsNewName">New name of the element</param>
			void RenameElement([MarshalAs(UnmanagedType.BStr)] string pwcsOldName, [MarshalAs(UnmanagedType.BStr)] string pwcsNewName);

			/// <summary>
			/// Sets the modification, access, and creation times of the specified storage element, if supported 
			/// by the underlying file system.
			/// </summary>
			/// <param name="pwcsName">Name of the element to be changed</param>
			/// <param name="pctime">New creation time for element, or NULL</param>
			/// <param name="patime">New access time for element, or NULL</param>
			/// <param name="pmtime">New modification time for element, or NULL</param>
			void SetElementTimes([MarshalAs(UnmanagedType.BStr)] string pwcsName, FILETIME pctime, FILETIME patime, FILETIME pmtime);

			/// <summary>
			/// Assigns the specified CLSID to this storage object.
			/// </summary>
			/// <param name="clsid">Class identifier to be assigned to the storage object</param>
			void SetClass(ref Guid clsid);

			/// <summary>
			/// Stores up to 32 bits of state information in this storage object.
			/// </summary>
			/// <param name="grfStateBits">Specifies new values of bits</param>
			/// <param name="grfMask">Specifies mask that indicates which bits are significant</param>
			void SetStateBits(int grfStateBits, int grfMask);

			/// <summary>
			/// Retrieves the STATSTG structure for this open storage object.
			/// </summary>
			/// <param name="pStatStg">Ouput STATSTG structure</param>
			/// <param name="grfStatFlag">Values taken from the STATFLAG enumeration</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int Stat(out STATSTG pStatStg, int grfStatFlag);
		}

		/// <summary>
		/// Imports the interface ITStorage
		/// </summary>
		[SuppressUnmanagedCodeSecurityAttribute()]
			[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
			[GuidAttribute("88CC31DE-27AB-11D0-9DF9-00A0C922E6EC")]
			[ComImportAttribute()]
			public interface ITStorage
		{

			/// <summary>
			/// Creates a new doc file
			/// </summary>
			/// <param name="pwcsName">Name of the new stream</param>
			/// <param name="grfMode">Access mode for the new stream</param>
			/// <param name="reserved">Reserved; must be zero</param>
			/// <returns>An IStorage reference to the new file</returns>
			[return: MarshalAs(UnmanagedType.Interface)]
			IStorage StgCreateDocfile([MarshalAs(UnmanagedType.BStr)] string pwcsName, int grfMode, int reserved);

			/// <summary>
			/// Creates a new doc file on licked bytes
			/// </summary>
			/// <param name="plkbyt">ILockBytes interface</param>
			/// <param name="grfMode">Access mode for the new stream</param>
			/// <param name="reserved">Reserved; must be zero</param>
			/// <returns>An IStorage reference to the new file</returns>
			[return: MarshalAs(UnmanagedType.Interface)]
			IStorage StgCreateDocfileOnILockBytes(ILockBytes plkbyt, int grfMode, int reserved);

			/// <summary>
			/// Checks if a filename exists in the storage
			/// </summary>
			/// <param name="pwcsName">name of the file to check</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int StgIsStorageFile([MarshalAs(UnmanagedType.BStr)] string pwcsName);

			/// <summary>
			/// Checks if a ILockBytes is part of the storage
			/// </summary>
			/// <param name="plkbyt">ILockBytes instance</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int StgIsStorageILockBytes(ILockBytes plkbyt);

			/// <summary>
			/// Opens a storage
			/// </summary>
			/// <param name="pwcsName">Name of the storage</param>
			/// <param name="pstgPriority">Points to previous opening of the storage object</param>
			/// <param name="grfMode">Access mode for the new storage object</param>
			/// <param name="snbExclude">Points to a block of stream names in the storage object</param>
			/// <param name="reserved">Reserved; must be zero</param>
			/// <returns>An IStorage reference to the new file</returns>
			[return: MarshalAs(UnmanagedType.Interface)]
			IStorage StgOpenStorage([MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr pstgPriority, [MarshalAs(UnmanagedType.U4)] int grfMode, IntPtr snbExclude, [MarshalAs(UnmanagedType.U4)] int reserved);

			/// <summary>
			/// Opens a storage
			/// </summary>
			/// <param name="plkbyt">ILockBytes instance</param>
			/// <param name="pStgPriority">Points to previous opening of the storage object</param>
			/// <param name="grfMode">Access mode for the new storage object</param>
			/// <param name="snbExclude">Points to a block of stream names in the storage object</param>
			/// <param name="reserved">Reserved; must be zero</param>
			/// <returns>An IStorage reference to the new file</returns>
			[return: MarshalAs(UnmanagedType.Interface)]
			IStorage StgOpenStorageOnILockBytes(ILockBytes plkbyt, IStorage pStgPriority, int grfMode, IntPtr snbExclude, int reserved);

			/// <summary>
			/// Set the file times of a storage file
			/// </summary>
			/// <param name="lpszName">Name of the element to be changed</param>
			/// <param name="pctime">New creation time for element, or NULL</param>
			/// <param name="patime">New access time for element, or NULL</param>
			/// <param name="pmtime">New modification time for element, or NULL</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int StgSetTimes([MarshalAs(UnmanagedType.BStr)] string lpszName, FILETIME pctime, FILETIME patime, FILETIME pmtime);

			/// <summary>
			/// Sets the storage control data
			/// </summary>
			/// <param name="pControlData">control data</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int SetControlData(ITS_Control_Data pControlData);

			/// <summary>
			/// Sets the default control data
			/// </summary>
			/// <param name="ppControlData">control data</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int DefaultControlData(ITS_Control_Data ppControlData);

			/// <summary>
			/// Compact a file
			/// </summary>
			/// <param name="pwcsName">filename</param>
			/// <param name="iLev">level</param>
			/// <returns>Returns S_OK if succeeded</returns>
			int Compact([MarshalAs(UnmanagedType.BStr)] string pwcsName, int iLev);
		}

		/// <summary>
		/// Imports the class <c>UCOMITStorage</c>
		/// </summary>
		[GuidAttribute("5D02926A-212E-11D0-9DF9-00A0C922E6EC")]
			[ComImportAttribute()]
			public class UCOMITStorage
		{
		}
		#endregion
	}
}
