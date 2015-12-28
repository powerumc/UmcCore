using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Data
{
	public interface ITableColumnAttribute
	{
		string ColumnName { get; set; }
		TableColumnType ColumnType { get; set; }
	}
}
