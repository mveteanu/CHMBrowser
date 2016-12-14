using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace HtmlHelp.Storage
{
	/// <summary>
	/// The class <c>FileObject</c> implements the properties/methods for handling storage file streams
	/// </summary>
	public class FileObject : Stream
	{
		/// <summary>
		/// Internal member storing the name of the file
		/// </summary>
		private string fileName;
		/// <summary>
		/// Internal member storing the path of the file
		/// </summary>
		private string filePath;
		/// <summary>
		/// Internal member storing the url of the file
		/// </summary>
		private string fileUrl;
		/// <summary>
		/// Internal member storing the type of the file
		/// </summary>
		private int fileType;
		/// <summary>
		/// Internal member storing the storage of the file
		/// </summary>
		private Interop.IStorage fileStorage;
		/// <summary>
		/// Internal member storing the storage stream
		/// </summary>
		private UCOMIStream fileStream;

		/// <summary>
		/// Gets the length of the file
		/// </summary>
		public override long Length
		{
			get
			{
				STATSTG sTATSTG;

				if (fileStream == null)
				{
					throw new ObjectDisposedException("fileStream", "storage stream no longer available");
				}
				fileStream.Stat(out sTATSTG, 1);
				return sTATSTG.cbSize;
			}
		}

		/// <summary>
		/// Gets a flag if reading is supported
		/// </summary>
		public override bool CanRead
		{
			get
			{
				if (fileStream != null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Gets a flag if writing is supported
		/// </summary>
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Gets a flag if seeking is supported
		/// </summary>
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Gets the current position in the stream
		/// </summary>
		public override long Position
		{
			get
			{
				return Seek((long)0, SeekOrigin.Current);
			}

			set
			{
				Seek(value, SeekOrigin.Begin);
			}
		}

		/// <summary>
		/// Gets the file name
		/// </summary>
		public string FileName
		{
			get
			{
				return fileName;
			}

			set
			{
				fileName = value;
			}
		}

		/// <summary>
		/// Gets the file path
		/// </summary>
		public string FilePath
		{
			get
			{
				return filePath;
			}

			set
			{
				filePath = value;
			}
		}

		/// <summary>
		/// Gets the file url
		/// </summary>
		public string FileUrl
		{
			get
			{
				return String.Concat(IBaseStorageWrapper.BaseUrl, FilePath.Replace("\\", "/"), "/", FileName);
			}

			set
			{
				fileUrl = value;
			}
		}

		/// <summary>
		/// Gets the file type
		/// </summary>
		public int FileType
		{
			get
			{
				return fileType;
			}

			set
			{
				fileType = value;
			}
		}

		/// <summary>
		/// Gets the storage of the file
		/// </summary>
		public Interop.IStorage FileStorage
		{
			get
			{
				return fileStorage;
			}

			set
			{
				fileStorage = value;
			}
		}

		/// <summary>
		/// Gets the storage stream for this file
		/// </summary>
		public UCOMIStream FileStream
		{
			get
			{
				return fileStream;
			}

			set
			{
				fileStream = value;
			}
		}

		/// <summary>
		/// Reads bytes from the stream
		/// </summary>
		/// <param name="buffer">buffer which will receive the bytes</param>
		/// <param name="offset">offset</param>
		/// <param name="count">number of bytes to be read</param>
		/// <returns>Returns the actual number of bytes read from the stream.</returns>
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (fileStream == null)
			{
				throw new ObjectDisposedException("fileStream", "storage stream no longer available");
			}
			int i = 0;
			object local = i;
			GCHandle gCHandle = new GCHandle();
			try
			{
				gCHandle = GCHandle.Alloc(local, GCHandleType.Pinned);
				IntPtr j = gCHandle.AddrOfPinnedObject();
				if (offset != 0)
				{
					byte[] bs = new byte[count - 1];
					fileStream.Read(bs, count, j);
					i = (int)local;
					Array.Copy(bs, 0, buffer, offset, i);
				}
				else
				{
					fileStream.Read(buffer, count, j);
					i = (int)local;
				}
			}
			finally
			{
				if (gCHandle.IsAllocated)
				{
					gCHandle.Free();
				}
			}
			return i;
		}

		/// <summary>
		/// Writes bytes to the stream
		/// </summary>
		/// <param name="buffer">byte array which contains the bytes to write</param>
		/// <param name="offset">offset where to write the bytes</param>
		/// <param name="count">number of bytes to write</param>
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (fileStream == null)
			{
				throw new ObjectDisposedException("theStream");
			}
			if (offset != 0)
			{
				int i = (int)buffer.Length - offset;
				byte[] bs = new byte[i];
				Array.Copy(buffer, offset, bs, 0, i);
				fileStream.Write(bs, i, IntPtr.Zero);
				return;
			}
			fileStream.Write(buffer, count, IntPtr.Zero);
		}

		/// <summary>
		/// Seeks to a position within the stream
		/// </summary>
		/// <param name="offset">offset to seek</param>
		/// <param name="origin">seek origin</param>
		/// <returns>Returns the new position</returns>
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (fileStream == null)
			{
				throw new ObjectDisposedException("fileStream", "storage stream no longer available");
			}
			long l = 0;
			object local = l;
			GCHandle gCHandle = new GCHandle();
			try
			{
				gCHandle = GCHandle.Alloc(local, GCHandleType.Pinned);
				IntPtr i = gCHandle.AddrOfPinnedObject();
				fileStream.Seek(offset, (int)origin, i);
				l = (long)local;
			}
			finally
			{
				if (gCHandle.IsAllocated)
				{
					gCHandle.Free();
				}
			}
			return l;
		}

		/// <summary>
		/// Flushes the stream
		/// </summary>
		public override void Flush()
		{
			if (fileStream == null)
			{
				throw new ObjectDisposedException("fileStream", "storage stream no longer available");
			}
			fileStream.Commit(0);
		}

		/// <summary>
		/// Closes the storage stream
		/// </summary>
		public override void Close()
		{
			if (fileStream != null)
			{
				fileStream.Commit(0);
				Marshal.ReleaseComObject(fileStream);
				fileStream = null;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Sets the length of the stream
		/// </summary>
		/// <param name="Value">new length of the stream</param>
		public override void SetLength(long Value)
		{
			if (fileStream == null)
			{
				throw new ObjectDisposedException("fileStream", "storage stream no longer available");
			}
			fileStream.SetSize(Value);
		}

		/// <summary>
		/// Saves the stream to a file
		/// </summary>
		/// <param name="FileName">filename</param>
		public void Save(string FileName)
		{
			int i;

			if (fileStream == null)
			{
				throw new ObjectDisposedException("fileStream", "storage stream no longer available");
			}
			byte[] bs = new byte[Length];
			Seek((long)0, SeekOrigin.Begin);
			Stream stream = File.OpenWrite(FileName);
			while ((i = Read(bs, 0, 1024)) > 0)
			{
				stream.Write(bs, 0, i);
			}
			stream.Close();
		}

		/// <summary>
		/// Reads from the stream (text-based)
		/// </summary>
		/// <returns>The string contents of the stream</returns>
		public string ReadFromFile()
		{
			int i;

			if (fileStream == null)
			{
				throw new ObjectDisposedException("fileStream", "storage stream no longer available");
			}
			Stream stream = new MemoryStream();
			byte[] bs = new byte[Length];
			Seek((long)0, SeekOrigin.Begin);
			while ((i = Read(bs, 0, 1024)) > 0)
			{
				stream.Write(bs, 0, i);
			}
			stream.Seek((long)0, SeekOrigin.Begin);
			return new StreamReader(stream, System.Text.Encoding.Default).ReadToEnd().ToString();
		}

		/// <summary>
		/// Reads from the stream (text based)
		/// </summary>
		/// <param name="encoder">encoder to use during read operation</param>
		/// <returns>The string contents of the stream</returns>
		public string ReadFromFile(Encoding encoder)
		{
			int i;

			if (fileStream == null)
			{
				throw new ObjectDisposedException("fileStream", "storage stream no longer available");
			}
			Stream stream = new MemoryStream();
			byte[] bs = new byte[Length];
			Seek((long)0, SeekOrigin.Begin);
			while ((i = Read(bs, 0, 1024)) > 0)
			{
				stream.Write(bs, 0, i);
			}
			stream.Seek((long)0, SeekOrigin.Begin);
			return new StreamReader(stream, encoder).ReadToEnd();
		}
	}
}
