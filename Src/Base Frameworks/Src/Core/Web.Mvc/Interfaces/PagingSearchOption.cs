using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Umc.Core.Web.Mvc
{
	public class PagingSearchOption
	{
		public int CurrentPageNo { get; set; }
		public int RowCountOnPage { get; set; }
		public int TotalRowCount { get; set; }
		public IEnumerable<KeyValuePair<string,string>> SearchOption { get;set;}

		public static PagingSearchOption PagingGetSearchOption()
		{
			var request = HttpContext.Current.Request;

			var ret = new PagingSearchOption()
			{
				CurrentPageNo = int.Parse((request[Paging.KEY_CURRENT_PAGE_NO] ?? "1")),
				RowCountOnPage = int.Parse((request[Paging.KEY_ROWCOUNT_ON_PAGE] ?? Paging.CONST_ROWCOUNT_ON_PAGE.ToString())),
				TotalRowCount = int.Parse(request[Paging.KEY_TOTAL_ROW_COUNT] ?? "1")
			};

			var searchOption = (request[Paging.KEY_SEARCH_OPTION] ?? "");

			List<KeyValuePair<string, string>> optionList = new List<KeyValuePair<string,string>>();
			foreach(var option in searchOption.Split(';'))
			{
				if ( option.Contains('=') == false ) continue;
				var optionItem = option.Split('=');

				optionList.Add(new KeyValuePair<string,string>(optionItem[0], optionItem[1]));
			}

			ret.SearchOption = optionList.AsEnumerable();
			
			return ret;
		}
	}
}