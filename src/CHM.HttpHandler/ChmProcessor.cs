using System;
using System.Text;

namespace CHM.HttpHandler
{
	public class ChmProcessor
	{
		private System.Web.HttpContext m_context;
		private bool m_AllowChmDownload;

		public ChmProcessor(System.Web.HttpContext context)
		{
			m_context = context;
			m_AllowChmDownload = Settings.CheckBoolSetting("AllowChmDownload", true);
		}

		public void Process()
		{
			PathAnalyzer pathAnalyzer = new PathAnalyzer(m_context);
			string reqFile = pathAnalyzer.RequestedFile;
		
			switch(reqFile.ToLower())
			{
				case "":
					if (m_AllowChmDownload)
						this.SendChmFile();
					else
						RedirectToIndex();
					break;
				case "/":
					this.SendFramesIndex();
					break;
				case "/toc":
					this.SendToc();
					break;
				default:
					this.SendInternalFile(reqFile);
					break;
			}
		}


		private void RedirectToIndex()
		{
			string newPath = m_context.Request.CurrentExecutionFilePath + "/";
			m_context.Response.Redirect( newPath );
		}

		private void SendChmFile()
		{
			string filePath = this.GetRequestPhysicalPath();
			m_context.Response.ContentType = "application/octet-stream";
			m_context.Response.WriteFile(filePath);
		}


		private void SendFramesIndex()
		{
			HHExtractor extr = null;

			try
			{
				string filePath = this.GetRequestPhysicalPath();
				extr = new HHExtractor(filePath);
				extr.OpenFile();
				string defaultTopic = extr.DefaultTopic;
				bool hasTableOfContents = extr.HasTableOfContents;

				if (hasTableOfContents)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("<html>\n");
					sb.Append("	<head>\n");
					sb.Append("		<title>");
					sb.Append(extr.Title);
					sb.Append("</title>\n");
					sb.Append("		<script language=JavaScript>\n");
					sb.Append("		if (top.location != self.location)\n");
					sb.Append("			top.location = self.location;\n");
					sb.Append("		</script>\n");
					sb.Append("	</head>\n");
					sb.Append("	<frameset cols='250,*' framespacing=6 bordercolor=#6699CC>\n");
					sb.Append("		<frame name=contents src='toc' frameborder=0 scrolling=no>\n");
					sb.Append("		<frame name=main src='" + defaultTopic + "' frameborder=1>\n");
					sb.Append("	</frameset>\n");
					sb.Append("</html>\n");

					m_context.Response.Write(sb.ToString());
				}
				else
				{
					m_context.Response.Redirect( m_context.Request.CurrentExecutionFilePath + "/" + defaultTopic);
				}

				this.SetCacheability();
			}
			catch(Exception ex)
			{
				throw new Exception("Cannot send frame index.", ex);
			}
			finally
			{
				if (extr != null)
					extr.CloseAllFiles();
			}
		}

		private void SendToc()
		{
			HHExtractor extr = null;

			try
			{
				string filePath = this.GetRequestPhysicalPath();
				extr = new HHExtractor(filePath);
				extr.OpenFile();

				string htmlToc = extr.GetTocAsHtml( this.GetAppPath() );

				m_context.Response.Write(htmlToc);

				this.SetCacheability();
			}
			catch(Exception ex)
			{
				throw new Exception("Cannot send TOC.", ex);
			}
			finally
			{
				if (extr != null)
					extr.CloseAllFiles();
			}
		}


		private string GetAppPath()
		{
			string path = m_context.Request.ApplicationPath;
			if (path == "/")
				path = "";

			return path;
		}

		private void SetCacheability()
		{
			m_context.Response.Cache.SetExpires( System.DateTime.Now.AddHours(24) );
			m_context.Response.Cache.SetCacheability( System.Web.HttpCacheability.Public );
			m_context.Response.Cache.SetValidUntilExpires(true);
		}

		private void SendInternalFile(string local)
		{
			HHExtractor extr = null;

			try
			{
				string filePath = this.GetRequestPhysicalPath();
				extr = new HHExtractor(filePath);

				m_context.Response.ContentType = this.FileExtToMimeType( System.IO.Path.GetExtension(local) );
				extr.WriteToStream( m_context.Response.OutputStream, local);
			}
			catch(Exception ex)
			{
				throw new Exception("Cannot send internal file: " + local, ex);
			}
		}



		private string GetRequestPhysicalPath()
		{
			return m_context.Request.PhysicalPath;
		}


		private string FileExtToMimeType(string FileExt)
		{
			string unknownType = "application/octet-stream";

			string[] fileExt = new string[]{
												".htm", ".html",
												".txt", ".bas", ".c", ".cpp", ".h", ".cs",
												".htc",
												".xml",
												".css",
												".mht",
												".class",
												".js",
												".bmp",
												".gif",
												".jpg", ".jpeg",
												".png",
												".tiff",
												".xbm",
												".ico",
												".wmf",
												".doc",
												".pdf",
												".xls",
												".ppt",
												".vsd",
												".z",
												".tgz",
												".gz",
												".swf",
												".tar",
												".zip"
										   };

			string[] mimeTypes = new string[]{
												"text/html", "text/html",
												"text/plain", "text/plain", "text/plain", "text/plain", "text/plain", "text/plain", 
												"text/x-component",
												"text/xml",
												"text/css",
												"message/rfc822",
												"application/octet-stream",
												"application/x-javascript",
												"image/bmp",
												"image/gif",
												"image/jpeg", "image/jpeg",
												"image/png",
												"image/tiff",
												"image/xbm",
												"image/x-icon",
												"application/x-msmetafile",
												"application/msword",
												"application/pdf",
												"application/vnd.ms-excel",
												"application/vnd.ms-powerpoint",
												"application/vnd.visio",
												"application/x-compress",
												"application/x-compressed",
												"application/x-gzip",
												"application/x-shockwave-flash",
												"application/x-tar",
												"application/x-zip-compressed"
											 };

			if ( (FileExt == null) || (FileExt.Length == 0) )
				return unknownType;

			string fileExtLow = FileExt.Trim().ToLower();

			for(int i = 0; i < fileExt.Length; i++)
				if (fileExtLow == fileExt[i])
					return mimeTypes[i];

			return unknownType;
		}


	}
}
