using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umc.Core.Web.Mvc
{
	public interface IGrid
	{
		IGridColumn Column { get; }
		IPaging Paging { get; }
		IGridData Data { get; }

		IGrid Width(int gridWidth);
		IGrid Height(int gridHegiht);
		IGrid Size(int gridWidth, int gridHegiht);
		IGrid Title(string gridTitle);
		IGrid RowCount(int rowCount);
		IGrid RowHeight(int rowHeight);
	}
}