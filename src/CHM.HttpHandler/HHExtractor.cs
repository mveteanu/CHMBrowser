using System;
using System.Collections;
using System.Text;

using HH = HtmlHelp;
using HHDeco = HtmlHelp.ChmDecoding;

namespace CHM.HttpHandler
{
	public class HHExtractor
	{
		private string m_FilePath;
		private string m_DumpsPath;
		private bool m_UseTocImages;
		private HH.HtmlHelpSystem m_HelpSystem;
		
		public HHExtractor(string FilePath)
		{
			this.m_FilePath = FilePath;
			this.m_DumpsPath = Settings.CheckBoolSetting("UseDumps", true) ? System.IO.Path.GetTempPath() : null;
			m_UseTocImages = Settings.CheckBoolSetting("UseTreeImages", true);
		}

		public void OpenFile()
		{
			HH.HtmlHelpSystem helpSys = new HH.HtmlHelpSystem(this.m_FilePath);
			helpSys.OpenFile( this.m_FilePath, this.GetDumpingInfo() );

			this.m_HelpSystem = helpSys;
		}

		public void CloseAllFiles()
		{
			this.m_HelpSystem.CloseAllFiles();
		}

		public string Title
		{
			get
			{
				return this.m_HelpSystem.FileList[0].FileInfo.HelpWindowTitle;
			}
		}

		public string DefaultTopic
		{
			get 
			{ 
				return this.m_HelpSystem.FileList[0].DefaultTopic;
			}
		}

		public void WriteToStream(System.IO.Stream stream, string local)
		{
			HH.Storage.ITStorageWrapper iw = new HH.Storage.ITStorageWrapper(this.m_FilePath, false);

			HH.Storage.FileObject fileObj = iw.OpenUCOMStream(null, local);
			if ( (fileObj == null) || (fileObj.FileStream == null) )
				throw new Exception("FileStream for local file [" + local + "] is not longer available");

			byte[] bs = new byte[fileObj.Length];
			fileObj.Seek((long)0, System.IO.SeekOrigin.Begin);
			
			int i;
			while ((i = fileObj.Read(bs, 0, 1024)) > 0)
				stream.Write(bs, 0, i);

			fileObj.Close();
		}


		public bool HasTableOfContents
		{
			get
			{
				return this.m_HelpSystem.HasTableOfContents;
			}
		}


		public string GetTocAsHtml(string AppPath)
		{
			if (!this.m_HelpSystem.HasTableOfContents)
				return String.Empty;

			StringBuilder sb = new StringBuilder();

			sb.Append("<html>\n");
			sb.Append("  <head>\n");
			sb.Append("    <title>Contents</title>\n");
			sb.Append("    <link rel='stylesheet' type='text/css' href='"+ AppPath +"/CHMBrowser/tree/tree.css' />\n");
			sb.Append("    <script language=javascript>\n");
			sb.Append("    	var imgtreenodeplus = \""+ AppPath +"/CHMBrowser/tree/treenodeplus.gif\";\n");
			sb.Append("    	var imgtreenodeminus = \""+ AppPath +"/CHMBrowser/tree/treenodeminus.gif\";\n");
			sb.Append("    	var imgtreenodedot = \""+ AppPath +"/CHMBrowser/tree/treenodedot.gif\";\n");
			sb.Append("    </script>\n");
			sb.Append("    <script src='"+ AppPath +"/CHMBrowser/tree/tree.js' language='javascript' type='text/javascript'></script>\n");
			sb.Append("  </head>\n");
			sb.Append("  <body id='docBody' style='background-color: #6699CC; color: White; margin: 0px 0px 0px 0px;' onload=\"resizeTree()\" onresize=\"resizeTree()\" onselectstart=\"return false;\">\n");
			sb.Append("    <div style='font-family: verdana; font-size: 8pt; cursor: pointer; margin: 6 4 8 2; text-align: right' onmouseover=\"this.style.textDecoration='underline'\" onmouseout=\"this.style.textDecoration='none'\" onclick=\"syncTree(window.parent.frames[1].document.URL)\">sync toc</div>\n");
			sb.Append("    <div id='tree' style='top: 35px; left: 0px;' class='treeDiv'>\n");
			sb.Append("      <div id='treeRoot' onselectstart=\"return false\" ondragstart=\"return false\">\n");

			string htmlNodes = this.GetTocAsHtml( m_HelpSystem.TableOfContents.TOC, AppPath );
			sb.Append(htmlNodes);

			sb.Append("      </div>\n");
			sb.Append("    </div>\n");
			sb.Append("    </div>\n");
			sb.Append("  </body>\n");
			sb.Append("</html>\n");

			return sb.ToString();
		}


		private string GetTocAsHtml(ArrayList searchIn, string AppPath)
		{
			if ((searchIn == null) || (searchIn.Count == 0))
				return "";

			StringBuilder sb = new StringBuilder();

			foreach(HH.TOCItem node in searchIn)
			{
				ArrayList children = node.Children;
				bool bHasChildren = ( (children != null) && (children.Count > 0));

				sb.Append("<div class='treeNode'>\n");
				if (!bHasChildren)
					sb.Append("<img src='"+ AppPath +"/CHMBrowser/tree/treenodedot.gif' class='treeNoLinkImage' />\n");
				else
					sb.Append("<img src='"+ AppPath +"/CHMBrowser/tree/treenodeplus.gif' class='treeLinkImage' onclick='expandCollapse(this.parentNode)' />\n");

				string nodeLocal = node.Local;
				if (nodeLocal == null)
					nodeLocal = String.Empty;

				sb.Append("<a href='" + nodeLocal + "' target='main' class='treeUnselected' onclick='");
				sb.Append( nodeLocal.Length == 0 ? "clickAnchor2(this); return false;" : "clickAnchor(this)" );
				sb.Append("'>");

				if (m_UseTocImages)
					sb.Append("<img border=0 src='"+ AppPath +"/CHMBrowser/images/node" + node.ImageIndex.ToString() + ".gif' /> ");
				
				sb.Append(node.Name);
				sb.Append("</a>\n");

				if (bHasChildren)
				{
					sb.Append("<div class='treeSubnodesHidden'>\n");
					sb.Append( this.GetTocAsHtml(children, AppPath) );
					sb.Append("</div>\n");
				}

				 sb.Append("</div>\n");
			}

			return sb.ToString();
		}


		private HHDeco.DumpingInfo GetDumpingInfo()
		{
			if ( this.m_DumpsPath == null )
				return null;
			
			HHDeco.DumpingFlags dumpFlags = HHDeco.DumpingFlags.DumpTextTOC |
											HHDeco.DumpingFlags.DumpBinaryTOC |
											HHDeco.DumpingFlags.DumpTextIndex |
											HHDeco.DumpingFlags.DumpBinaryIndex |
											HHDeco.DumpingFlags.DumpStrings |
											HHDeco.DumpingFlags.DumpUrlStr;
			
			return new HHDeco.DumpingInfo(dumpFlags, m_DumpsPath); //, HHDeco.DumpCompression.None);
		}


		#region Old GetTocAsHtml
//		public string GetTocAsHtml(string AppPath)
//		{
//			if (!this.m_HelpSystem.HasTableOfContents)
//				return String.Empty;
//			
//			StringBuilder sb = new StringBuilder();
//			sb.Append("<head>\n");
//			sb.Append("	<SCRIPT LANGUAGE='JavaScript' SRC='" + AppPath + "/tree/mktree.js'></SCRIPT>\n");
//			sb.Append("	<LINK REL='stylesheet' HREF='" + AppPath + "/tree/mktree.css'>\n");
//			sb.Append("	<LINK REL='stylesheet' HREF='" + AppPath + "/tree/tree.css'>\n");
//			sb.Append("</head>\n");
//
//			sb.Append("<body leftmargin=0>\n");
//			sb.Append("<div class='treeDiv'>\n");
//			sb.Append( this.GetTocAsHtml(null, AppPath) );
//			sb.Append("</div>\n");
//			sb.Append("</body>\n");
//
//			return sb.ToString();
//		}
//
//
//
//		private string GetTocAsHtml(HH.TOCItem parentNode, string AppPath)
//		{
//			StringBuilder sb = new StringBuilder();
//			
//			ArrayList nodes = this.GetToc(parentNode);
//			if ((nodes == null) || (nodes.Count == 0))
//				return "";
//
//			if (parentNode == null)
//				sb.Append("<ul class='mktree'>\n");
//			else
//				sb.Append("<ul>\n");
//
//			foreach(HH.TOCItem node in nodes)
//			{
//				sb.Append("<li><img src='");
//				sb.Append(AppPath);
//				sb.Append("/img/node");
//				sb.Append(node.ImageIndex.ToString());
//				sb.Append(".gif'> <a target='right' href='");
//				sb.Append(node.Local);
//				sb.Append("'>");
//				sb.Append(node.Name);
//				sb.Append("</a></li>\n");
//
//				if ( (node.Children != null) && (node.Children.Count > 0) )
//					sb.Append( this.GetTocAsHtml(node, AppPath) );
//			}
//			sb.Append("</ul>\n");
//
//			return sb.ToString();
//		}
//
//		private ArrayList GetToc(HH.TOCItem parentNode)
//		{
//			if (parentNode == null)
//				return m_HelpSystem.TableOfContents.TOC;
//			
//			return parentNode.Children;
//		}

		#endregion 

		#region Old FindTocItem methods
		//		public void WriteToStream(System.IO.Stream stream, string local)
		//		{
		//			HH.TOCItem item = this.FindTOCItem( local, null );
		//			if (item == null)
		//				throw new Exception("Cannot find local file [" + local + "]");
		//
		//			HH.Storage.FileObject fileObj = item.ContentFile;
		//			if (fileObj.FileStream == null)
		//				throw new Exception("FileStream for local file [" + local + "] is not longer available");
		//
		//			byte[] bs = new byte[fileObj.Length];
		//			fileObj.Seek((long)0, System.IO.SeekOrigin.Begin);
		//			
		//			int i;
		//			while ((i = fileObj.Read(bs, 0, 1024)) > 0)
		//				stream.Write(bs, 0, i);
		//		}
		//
		//		private HH.TOCItem FindTOCItem(string local, HH.TOCItem parentNode)
		//		{
		//			ArrayList nodes = this.GetToc(parentNode);
		//			
		//			foreach(HH.TOCItem curItem in nodes)
		//			{
		//				if(curItem.Local == local)
		//					return curItem;
		//
		//				if(curItem.Children.Count > 0)
		//				{
		//					HH.TOCItem nf = FindTOCItem(local, curItem);
		//					if(nf != null)
		//						return nf;
		//				}
		//			}
		//
		//			return null;
		//		}
		#endregion

	}
}
