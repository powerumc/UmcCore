using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umc.Core.Web.Mvc
{
	public interface IVerticalAlign<TReturn>
	{
		TReturn Align(VertialAlign align);
	}
}