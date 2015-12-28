using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umc.Core.Web.Mvc;

namespace System.Web.Mvc
{
	public static class MvcExtensions
	{
		public static IGrid Grid(this HtmlHelper html)
		{
			throw new NotImplementedException();
		}

		public static IPaging Paging(this HtmlHelper html)
		{
			return new Paging();
		}

		public static IPaging Paging(this HtmlHelper html, string id)
		{
			return new Paging(id);
		}

		public static IPaging Paging(this HtmlHelper html, string id, string name)
		{
			return new Paging(id, name);
		}
		
		public static PagingSearchOption GetPagingSearchOption(this HtmlHelper html)
		{
			return PagingSearchOption.PagingGetSearchOption();
		}
	}
}