using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Handlers;
using System.Reflection;
using System.Web.Mvc;

namespace Umc.Core.Web.Mvc
{
	public static class FrameworkResources
	{
		private static MethodInfo GetWebResourceUrl = typeof(AssemblyResourceLoader).GetMethod("GetWebResourceUrl", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(Type), typeof(string) }, null);

		public static string GetFrameworkResourceContent(string @fullNamespace)
		{
			var url = GetWebResourceUrl.Invoke(null, new object[] { typeof(MvcController), @fullNamespace });
			if( url == null ) throw new ArgumentException(@fullNamespace + " is not exists namespace resource");

			return url.ToString();
		}
	}


}