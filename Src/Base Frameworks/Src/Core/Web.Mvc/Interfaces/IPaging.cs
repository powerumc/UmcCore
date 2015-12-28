using System;
using System.Collections.Generic;
using System.Linq; 
using System.Web;

namespace Umc.Core.Web.Mvc
{
	public interface IPaging : IDone, IHtmlNaming
	{
		IPaging Setup(int rowCountOnPage, int totalRowCount);

		IPaging Align(HorizontalAlign align);
		
		IPaging IsHidden();
		IPaging IsHidden(bool isHidden);

		IPaging Css(string css);

		IPaging PagingNumberCount(int count);
		IPaging OnClick(Func<int, string> navigateUrl);

		IHtmlSpan<IPaging> PreviousLinkOption { get; }
		IHtmlSpan<IPaging> NextLinkOption { get; }
	}
}