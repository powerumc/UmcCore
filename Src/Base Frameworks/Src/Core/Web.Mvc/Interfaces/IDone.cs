using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Umc.Core.Web.Mvc
{
	public interface IDone
	{
		MvcHtmlString Done();
	}

	public interface IDone<TReturn>
	{

	}
}