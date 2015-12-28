using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Web.UI;
using System.Web;

namespace Umc.Core.Web
{
	public static class FormBuilderResources
	{
		public static string IncludeScriptFromResources(this Page page, Type type, params string[] resourcesName)
		{
			var sb = new StringBuilder(1024);
			foreach (var name in resourcesName)
			{
				sb.AppendFormat("<script type='text/javascript' src='{0}'></script>", page.ClientScript.GetWebResourceUrl(type, name));
			}

			return sb.ToString();
		}

		public static string IncludeStylesheetFromResources(this Page page, Type type, params string[] resourcesName)
		{
			var sb = new StringBuilder(1024);
			foreach (var name in resourcesName)
			{
				sb.AppendFormat("<link rel='stylesheet' href='{0}' />", page.ClientScript.GetWebResourceUrl(type, name));
			}

			return sb.ToString();
		}

		//public static string GetScriptInResources(this Page page)
		//{
		//	var sb = new StringBuilder(1024);
		//	sb.AppendFormat("<script type='text/javascript' src='{0}'></script>", page.ClientScript.GetWebResourceUrl(typeof(FormBuilderResources), FORMBUILDER.RESOURCES.API_CHECK.DIST.API_CHECK_JS))
		//	.AppendFormat("<script type='text/javascript' src='{0}'></script>", page.ClientScript.GetWebResourceUrl(typeof(FormBuilderResources), FORMBUILDER.RESOURCES.ANGULAR_FORMLY.DIST.BOOTSTRAP_MIN_JS))
		//	.AppendFormat("<script type='text/javascript' src='{0}'></script>", page.ClientScript.GetWebResourceUrl(typeof(FormBuilderResources), FORMBUILDER.RESOURCES.ANGULAR_FORMLY.DIST.ANGULAR_JS))
		//	.AppendFormat("<script type='text/javascript' src='{0}'></script>", page.ClientScript.GetWebResourceUrl(typeof(FormBuilderResources), FORMBUILDER.RESOURCES.ANGULAR_FORMLY.DIST.FORMLY_JS))
		//	.AppendFormat("<script type='text/javascript' src='{0}'></script>", page.ClientScript.GetWebResourceUrl(typeof(FormBuilderResources), FORMBUILDER.RESOURCES.ANGULAR_FORMLY.DIST.ANGULAR_FORMLY_TEMPLATES_BOOTSTRAP_JS));

		//	return sb.ToString();
		//}

		//public static string GetCSSInResources(this Page page)
		//{
		//	var sb = new StringBuilder(1024);
		//	sb.AppendFormat("<link rel='stylesheet' href='{0}'></link>", page.ClientScript.GetWebResourceUrl(typeof(FormBuilderResources), FORMBUILDER.RESOURCES.ANGULAR_FORMLY.DIST.BOOTSTRAP_CSS));
			
		//	return sb.ToString();
		//}
	}
}
