using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.ComponentModel;

using HtmlHelp.ChmDecoding;
using HtmlHelp.Storage;

namespace HtmlHelp
{
	/// <summary>
	/// The class <c>ChmFileInfo</c> only extracts system information from a CHM file. 
	/// It doesn't build the index and table of contents.
	/// </summary>
	public class ChmFileInfo
	{
		/// <summary>
		/// Internal member storing the full filename
		/// </summary>
		private string _chmFileName = "";
		/// <summary>
		/// Internal member storing the associated chmfile object
		/// </summary>
		private CHMFile _associatedFile = null;

		/// <summary>
		/// Constructor for extrating the file information of the provided file. 
		/// The constructor opens the chm-file and reads its system data.
		/// </summary>
		/// <param name="chmFile">full file name which information should be extracted</param>
		public ChmFileInfo(string chmFile)
		{
			if(!File.Exists(chmFile))
				throw new ArgumentException("Chm file must exist on disk !", "chmFileName");

			if( ! chmFile.ToLower().EndsWith(".chm") )
				throw new ArgumentException("HtmlHelp file must have the extension .chm !", "chmFile");

			_chmFileName = chmFile;
			_associatedFile = new CHMFile(null, chmFile, true); // only load system data of chm
		}

		/// <summary>
		/// Internal constructor used in the class <see cref="HtmlHelp.ChmDecoding.CHMFile">CHMFile</see>.
		/// </summary>
		/// <param name="associatedFile">associated chm file</param>
		internal ChmFileInfo(CHMFile associatedFile)
		{
			_associatedFile = associatedFile;

			if( _associatedFile == null)
				throw new ArgumentException("Associated CHMFile instance must not be null !", "associatedFile");
		}

		#region default info properties
		/// <summary>
		/// Gets the full filename of the chm file
		/// </summary>
		[Description("The full name of the loaded CHM file")]
		public string ChmFileName
		{
			get 
			{ 
				return _associatedFile.ChmFilePath; 
			}
		}

		/// <summary>
		/// Gets a <see cref="System.IO.FileInfo">FileInfo</see> instance for the chm file.
		/// </summary>
		[Browsable(false)]
		public FileInfo FileInfo
		{
			get { return new FileInfo(_associatedFile.ChmFilePath); }
		}
		#endregion

		#region #SYSTEM properties
		/// <summary>
		/// Gets the file version of the chm file. 
		/// 2 for Compatibility=1.0,  3 for Compatibility=1.1
		/// </summary>
		[Description("The file version: 2 for compatibility=1.0, 3 for compatibility=1.1")]
		public int FileVersion
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.FileVersion;
					
				return 0; 
			}
		}

		/// <summary>
		/// Gets the contents file name
		/// </summary>
		[Description("The name of the sitemap contents file.")]
		public string ContentsFile
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.ContentsFile;
					
				return ""; 
			}
		}

		/// <summary>
		/// Gets the index file name
		/// </summary>
		[Description("The name of the sitemap index file.")]
		public string IndexFile
		{
			get 
			{ 
				if(_associatedFile != null)
					return _associatedFile.IndexFile;
					
				return ""; 
			}
		}

		/// <summary>
		/// Gets the default help topic
		/// </summary>
		[Description("The url of the default topic. If emtpy, the first topic of the table of contents will be used.")]
		public string DefaultTopic
		{
			get 
			{ 
				if(_associatedFile != null)
					return _associatedFile.DefaultTopic;
					
				return ""; 
			}
		}

		/// <summary>
		/// Gets the title of the help window
		/// </summary>
		[Description("The title of the viewer window.")]
		public string HelpWindowTitle
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.HelpWindowTitle;
					
				return ""; 
			}
		}

		/// <summary>
		/// Gets the flag if DBCS is in use
		/// </summary>
		[Description("A flag id DBCS is in use")]
		public bool DBCS
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.DBCS;
					
				return false; 
			}
		}

		/// <summary>
		/// Gets the flag if full-text-search is available
		/// </summary>
		[Description("A flag specifying if fulltext-search is supported.")]
		public bool FullTextSearch
		{
			get 
			{
				if(_associatedFile != null)
					return _associatedFile.FullTextSearch;
					
				return false; 
			}
		}

		/// <summary>
		/// Gets the flag if the file has ALinks
		/// </summary>
		[Description("A flag specifying if the CHM contains associative links (ALinks index).")]
		public bool HasALinks
		{
			get 
			{ 
				if(_associatedFile != null)
					return _associatedFile.HasALinks;
					
				return false; 
			}
		}

		/// <summary>
		/// Gets the flag if the file has KLinks
		/// </summary>
		[Description("A flag specifying if the CHM contains keyword links (KLinks index).")]
		public bool HasKLinks
		{
			get 
			{ 
				if(_associatedFile != null)
					return _associatedFile.HasKLinks;
					
				return false; 
			}
		}

		/// <summary>
		/// Gets the default window name
		/// </summary>
		[Description("The name of the default window.")]
		public string DefaultWindow
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.DefaultWindow;
					
				return ""; 
			}
		}

		/// <summary>
		/// Gets the file name of the compile file
		/// </summary>
		[Description("The name of the compile file.")]
		public string CompileFile
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.CompileFile;
					
				return ""; 
			}
		}

		/// <summary>
		/// Gets the flag if the chm has a binary index file
		/// </summary>
		[Description("A flag specifying if this CHM contains a binary index (false = text based sitemap index or none)")]
		public bool BinaryIndex
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.BinaryIndex;
					
				return false; 
			}
		}

		/// <summary>
		/// Gets the flag if the chm has a binary index file
		/// </summary>
		[Description("The version of the compiler with which this file was created.")]
		public string CompilerVersion
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.CompilerVersion;
					
				return ""; 
			}
		}

		/// <summary>
		/// Gets the flag if the chm has a binary toc file
		/// </summary>
		[Description("A flag specifying if the CHM contains a binary table of contents (false = text based sitemap toc or none)")]
		public bool BinaryTOC
		{
			get 
			{ 
				if(_associatedFile != null)
					return _associatedFile.BinaryTOC;
					
				return false; 
			}
		}

		/// <summary>
		/// Gets the font face of the read font property.
		/// Empty string for default font.
		/// </summary>
		[Description("The default font face.")]
		public string FontFace
		{
			get
			{
				if(_associatedFile != null)
					return _associatedFile.FontFace;
					
				return ""; 
			}
		}

		/// <summary>
		/// Gets the font size of the read font property.
		/// 0 for default font size
		/// </summary>
		[Description("The default font size.")]
		public double FontSize
		{
			get
			{
				if(_associatedFile != null) 
					return _associatedFile.FontSize;
					
				return 0.0; 
			}
		}

		/// <summary>
		/// Gets the character set of the read font property
		/// 1 for default
		/// </summary>
		[Description("The default character set.")]
		public int CharacterSet
		{
			get
			{
				if(_associatedFile != null) 
					return _associatedFile.CharacterSet;
					
				return 1; 
			}
		}

		/// <summary>
		/// Gets the codepage depending on the read font property
		/// </summary>
		[Description("The default code page.")]
		public int CodePage
		{
			get
			{
				if(_associatedFile != null) 
					return _associatedFile.CodePage;
					
				return 0; 
			}
		}

		/// <summary>
		/// Gets the assiciated culture info
		/// </summary>
		[Browsable(false)]
		public CultureInfo Culture
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.Culture;
					
				return CultureInfo.CurrentCulture; 
			}
		}
		#endregion

		#region #IDXHDR properties
		/// <summary>
		/// Gets the number of topic nodes including the contents and index files
		/// </summary>
		[Description("The total number of topic nodes in this file (incl. table of contents and index).")]
		public int NumberOfTopicNodes
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.NumberOfTopicNodes;
				
				return 0;
			}
		}

		/// <summary>
		/// Gets the ImageList string specyfied in the #IDXHDR file.
		/// </summary>
		/// <remarks>This property uses the #STRINGS file to extract the string at a given offset.</remarks>
		[Description("The image list name.")]
		public string ImageList
		{
			get 
			{
				if(_associatedFile != null) 
					return _associatedFile.ImageList;

				return "";
			}
		}

		/// <summary>
		/// Gets the background setting 
		/// </summary>
		[Description("The background setting.")]
		public int Background
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.Background;

				return 0;
			}
		}

		/// <summary>
		/// Gets the foreground setting 
		/// </summary>
		[Description("The foreground setting.")]
		public int Foreground
		{
			get 
			{ 
				if(_associatedFile != null) 
					return _associatedFile.Foreground;

				return 0;
			}
		}

		/// <summary>
		/// Gets the FrameName string specyfied in the #IDXHDR file.
		/// </summary>
		/// <remarks>This property uses the #STRINGS file to extract the string at a given offset.</remarks>
		[Description("The frame name.")]
		public string FrameName
		{
			get 
			{
				if(_associatedFile != null) 
					return _associatedFile.FrameName;

				return "";
			}
		}

		/// <summary>
		/// Gets the WindowName string specyfied in the #IDXHDR file.
		/// </summary>
		/// <remarks>This property uses the #STRINGS file to extract the string at a given offset.</remarks>
		[Description("The window name.")]
		public string WindowName
		{
			get 
			{
				if(_associatedFile != null) 
					return _associatedFile.WindowName;

				return "";
			}
		}

		/// <summary>
		/// Gets a string array containing the merged file names
		/// </summary>
		[Description("The merged file list")]
		public string[] MergedFiles
		{
			get
			{
				if(_associatedFile != null) 
					return _associatedFile.MergedFiles;

				return new string[0];
			}
		}

		#endregion
	}
}
