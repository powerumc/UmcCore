using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umc.Core.Web.Mvc;

namespace System.Web.Mvc
{
	public static class UrlExtension
	{
		public static string Resource(this UrlHelper url, string resourceName)
		{
			return FrameworkResources.GetFrameworkResourceContent(resourceName);
		}
	}
}