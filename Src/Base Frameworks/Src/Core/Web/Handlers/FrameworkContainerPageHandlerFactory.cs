using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using Umc.Core.IoC;

namespace Umc.Core.Web.Handlers
{
	public class FrameworkContainerPageHandlerFactory : IHttpHandlerFactory
	{
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
            var handler = BuildManager.GetObjectFactory(url, false).CreateInstance();
            if (handler.GetType().ToString().StartsWith("ASP."))
            {
                var container = context.Application["container"] as IFrameworkContainer;
                return container.Resolve(handler.GetType().BaseType) as IHttpHandler;
            }

            return handler as IHttpHandler;
        }

		public void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
