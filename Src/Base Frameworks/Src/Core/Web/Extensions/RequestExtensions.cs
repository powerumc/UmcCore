using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Umc.Core.Web
{
	public static class RequestExtensions
	{
		public static string GetPayloadData(this Page page)
		{
			
			page.Request.InputStream.Position = 0;
			var data = page.Request.BinaryRead(page.Request.ContentLength);
			var json = Encoding.UTF8.GetString(data);

			return json;
		}
	}
}
