using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umc.Core.Web.Mvc
{
	public interface IHorizontalAlign<TReturn>
	{
		TReturn Align(HorizontalAlign align);
	}
}