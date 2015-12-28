using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umc.Core.Web.Mvc
{
	public interface IGridColumn
	{
		IGridColumn AddColumn(string columnName);
		IGridColumn AddColumn(int indexOfInsertBefore, string columnName);
		IGridColumn AddColumn(string columnName, int indexOfInsertAfter);

		IGridColumn Width(int columnWidth);
		IGridColumn Width(int columnWidth, HorizontalAlign columnAlign);

		IGridColumn Align(HorizontalAlign columnAlign);

		IGridColumn IsSortable();
		IGridColumn IsSortable(bool isSortable);

		IGridColumn IsHiiden();
		IGridColumn IsHiiden(bool isHidden);

		IGridColumn Count { get; }

		IGrid Done();
		
	}
}