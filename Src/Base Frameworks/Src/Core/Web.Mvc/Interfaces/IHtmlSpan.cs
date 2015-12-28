using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umc.Core.Web.Mvc
{
	public interface IHtmlSpan<TReturn>
	{
		IHtmlSpan<TReturn> Text(string text);
		IHtmlSpan<TReturn> Image(string url);
		IHtmlSpan<TReturn> Image(Uri uri);

		TReturn Done();
	}
}