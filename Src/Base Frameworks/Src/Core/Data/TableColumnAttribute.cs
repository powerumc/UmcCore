using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Data
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public class TableColumnAttribute : Attribute, ITableColumnAttribute
	{		
		public string ColumnName { get; set; }
		public TableColumnType ColumnType { get; set; }

		public TableColumnAttribute() : this(null, TableColumnType.None)
		{
		}

		public TableColumnAttribute(string columnName) : this(columnName, TableColumnType.None)
		{
		}

		public TableColumnAttribute(string columnName, TableColumnType columnType)
		{
			this.ColumnName = columnName;
			this.ColumnType = columnType;
		}
	}

	public enum TableColumnType
	{
		None,
		PK,
		FK
	}
}
