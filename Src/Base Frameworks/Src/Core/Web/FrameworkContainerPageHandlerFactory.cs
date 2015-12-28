using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

namespace Umc.Core.Web
{
	public class FrameworkContainerPageHandlerFactory : IHttpHandlerFactory
	{
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			var handler = BuildManager.CreateInstanceFromVirtualPath(url, typeof (Page)) as IHttpHandler;
			if (handler == null) throw new NullReferenceException("handler");



			return handler;
		}

		public void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
