using System;
using System.Web;
using System.Web.SessionState;


namespace CHM.HttpHandler
{
	public class HttpHandler : IHttpHandler, IRequiresSessionState
	{
		public HttpHandler(){}

		// This property is called to determine whether this instance of http handler 
		// can be reused for fulfilling another requests of the same type. 
		public bool IsReusable
		{
			get { return true; }
		}

		// This method is actually the heart of all http handlers. 
		// This method is called to process http requests.
		public void ProcessRequest(System.Web.HttpContext context)
		{
			try
			{
				ChmProcessor chmProc = new ChmProcessor(context);
				chmProc.Process();
			}
			catch(Exception ex)
			{
				System.Web.HttpResponse r = context.Response;
				r.Write("<h2>CHM App error</h2><br>\n");
				r.Write("<pre>\n");
				r.Write("Error processing path: " + context.Request.Path + "<br>\n");
				r.Write("Error: " + ex.ToString());
				r.Write("</pre>\n");
			}
		}

	}
}
