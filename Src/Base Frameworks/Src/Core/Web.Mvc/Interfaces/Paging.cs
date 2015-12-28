using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.UI;

namespace Umc.Core.Web.Mvc
{
	public class Paging : IPaging, IVerticalAlign<IPaging>, IHorizontalAlign<IPaging>
	{
		public static readonly string KEY_CURRENT_PAGE_NO    = "CurrentPageNo";
		public static readonly string KEY_TOTAL_ROW_COUNT    = "TotalRowCount";
		public static readonly string KEY_ROWCOUNT_ON_PAGE   = "RowCountOnPage";
		public static readonly string KEY_SEARCH_OPTION      = "SearchOption";
		public static readonly int CONST_ROWCOUNT_ON_PAGE    = 10;
		public static readonly int CONST_PAGE_NUMBER_COUNT   = 10;

		private int currentPageNo        = 1;
		private int rowCountOnPage       = CONST_ROWCOUNT_ON_PAGE;
		private int totalRowCount        = 1;
		private int pageNumberCount      = CONST_PAGE_NUMBER_COUNT;

		private string id   = null;
		private string name = null;

		private Func<int, string> onClick = null;

		HtmlGenericControl div = new HtmlGenericControl("div");

		delegate string AttributeHandler();
		AttributeHandler styleHandler = new AttributeHandler(() => "");
		AttributeHandler cssHandler   = new AttributeHandler(() => "");

		public Paging() { }

		public Paging(string id)
		{
			this.id = id;
		}

		public Paging(string id, string name)
		{
			this.id   = id;
			this.name= name;
		}

		public IPaging Setup(int rowCountOnPage, int totalRowCount)
		{
			this.rowCountOnPage   = rowCountOnPage;
			this.totalRowCount    = totalRowCount;

			return this;
		}

		#region IVerticalAlign<IPaging> 멤버

		public IPaging Align(VertialAlign align)
		{
			styleHandler += new AttributeHandler(() => String.Concat("text-align:", align.ToString().ToLower(), ';'));
			return this;
		}

		#endregion

		#region IHorizontalAlign<IPaging> 멤버

		public IPaging Align(HorizontalAlign align)
		{
			styleHandler += new AttributeHandler(() => String.Concat("vertical-align:", align.ToString().ToLower(), ';'));
			return this;
		}

		#endregion

		#region IDone 멤버

		public MvcHtmlString Done()
		{
			if ( string.IsNullOrEmpty(this.id) == false ) div.Attributes["id"] = this.id;
			if ( string.IsNullOrEmpty(this.name) == false ) div.Attributes["name"] = this.name;

			if ( styleHandler.GetInvocationList().Count() == 1 )

			div.Attributes["style"] += styleHandler();
			div.Attributes["class"] += cssHandler();

			div.Controls.Add(new HtmlInputHidden() { ID = KEY_CURRENT_PAGE_NO, Name = KEY_CURRENT_PAGE_NO, Value = this.currentPageNo.ToString() });
			div.Controls.Add(new HtmlInputHidden() { ID = KEY_TOTAL_ROW_COUNT, Name = KEY_TOTAL_ROW_COUNT, Value = this.totalRowCount.ToString() });
			div.Controls.Add(new HtmlInputHidden() { ID = KEY_ROWCOUNT_ON_PAGE, Name = KEY_ROWCOUNT_ON_PAGE, Value = this.rowCountOnPage.ToString() });


			var ul = new HtmlGenericControl("ul");
			div.Controls.Add(ul);

			int totalPageNumberCount = totalRowCount / pageNumberCount;
			for ( int i = currentPageNo; i <= (this.currentPageNo + this.pageNumberCount); i++ )
			{
				if ( i > totalPageNumberCount ) break;

				var a = new HtmlAnchor();
				a.HRef = this.onClick != null ? onClick(i) : "#";
				//HtmlInputSubmit a = new HtmlInputSubmit();

				var li       = createLI();
				li.InnerText = i.ToString();
				a.Controls.Add(li);
				ul.Controls.Add(a);
			}

			if ( currentPageNo < totalPageNumberCount )
			{
				var li = createLI();
				li.InnerText = ">>";
				ul.Controls.Add(li);
			}

			var sWriter = new StringWriter();
			var writer = new HtmlTextWriter(sWriter);
			div.RenderControl(writer);

			return MvcHtmlString.Create(sWriter.ToString());
		}

		private static HtmlGenericControl createLI()
		{
			var li = new HtmlGenericControl("li");
			li.Attributes["style"] = "display:inline; list-style-type:none; padding-right:10px;";
			return li;
		}

		#endregion

		#region IPaging 멤버


		public IPaging IsHidden()
		{
			styleHandler += new AttributeHandler(() => String.Concat("visibility:hidden;"));
			return this;
		}

		public IPaging IsHidden(bool isHidden)
		{
			styleHandler += new AttributeHandler(() => String.Concat("visibility:", isHidden ? "hidden;" : "visible;"));
			return this;
		}

		public IPaging Css(string css)
		{
			cssHandler += new AttributeHandler(() => css + " ");
			return this;
		}

		public IPaging PagingNumberCount(int count)
		{
			this.pageNumberCount = count;
			return this;
		}

		public IHtmlSpan<IPaging> PreviousLinkOption
		{
			get { throw new NotImplementedException(); }
		}

		public IHtmlSpan<IPaging> NextLinkOption
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region IHtmlNaming 멤버

		public void Id(string id)
		{
			this.id = id;
		}

		public void Id(string id, string name)
		{
			this.id = id;
			this.name= name;
		}

		public void Name(string name)
		{
			this.name = name;
		}

		#endregion

		#region IPaging 멤버


		public IPaging OnClick(Func<int, string> onClick)
		{
			this.onClick = onClick;
			return this;
		}

		#endregion
	}
}