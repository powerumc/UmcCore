using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umc.Core.Web.Mvc
{
	public interface IHtmlNaming
	{
		void Id(string id);
		void Id(string id, string name);
		void Name(string name);
	}
}