using System;

namespace CHM.HttpHandler
{
	public class PathAnalyzer
	{
		private System.Web.HttpContext m_context;

		public PathAnalyzer(System.Web.HttpContext context)
		{
			this.m_context = context;
		}

		public string RequestedFile
		{
			get { return this.GetRequestedFile(); }
		}

		protected virtual string GetRequestedFile()
		{
			string exePath = m_context.Request.CurrentExecutionFilePath;
			string path = m_context.Request.Path;

			if (exePath.Length == path.Length)
				return String.Empty;

			int pos = path.IndexOf(exePath);
			if (pos == -1)
				return String.Empty;

			return path.Substring(exePath.Length);
		}
	}
}
