using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

namespace Umc.Core.Web.Mvc
{
	public interface IGridData
	{
		IGridData Url(string url);
		IGridData Uri(Uri uri);
		IGridData ContentType(string contentType);
		IGridData Encoding(Encoding encoding);
		IGridData Data(object data);
		IGridData Data(string url);
		IGridData Data(Uri uri);
#if NET40
		IGridData Data(Func<bool, int, int, int, string, ActionResult> funcData);
#endif

		IGrid Done();
	}
}