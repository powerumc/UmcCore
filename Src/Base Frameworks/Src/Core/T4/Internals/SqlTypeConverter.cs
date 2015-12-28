using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Umc.Core.T4.Internals
{
	public static class SqlTypeConverter
	{
		public static Type Conveter(string databaseColumnType, bool isNullable = false)
		{
			switch (databaseColumnType)
			{
				case "varchar":
				case "nvarchar":
				case "text":
				case "ntext":
					return typeof(string);

				case "image":
					return typeof(System.Drawing.Image);

				case "char":
				case "nchar":
					return typeof(string);

				case "datetime":
					return isNullable ? typeof(DateTime?) : typeof(DateTime);

				case "bit":
					return typeof(bool);

				case "int":
				case "money":
				case "smallint":
				case "real":
					return isNullable ? typeof(int?) : typeof(int);

				case "bigint":
					return typeof(Int64);
			}

			throw new ArgumentException("databaseColumnType=" + databaseColumnType);
		}

		public static SqlDbType ConveterFrom(string data_type)
		{
			switch (data_type)
			{
				case "varchar": return SqlDbType.VarChar;
				case "nvarchar": return SqlDbType.NVarChar;
				case "text": return SqlDbType.Text;
				case "ntext": return SqlDbType.NText;
				case "image": return SqlDbType.Image;
				case "char": return SqlDbType.Char;
				case "nchar": return SqlDbType.NChar;
				case "datetime": return SqlDbType.DateTime;
				case "bit": return SqlDbType.Bit;
				case "int": return SqlDbType.Int;
				case "money": return SqlDbType.Money;
				case "smallint": return SqlDbType.SmallInt;
				case "real": return SqlDbType.Real;
				case "tinyint": return SqlDbType.TinyInt;
				case "bigint": return SqlDbType.BigInt;
			}

			throw new ArgumentException("data_type=" + data_type);
		}

		public static ParameterDirection ConvertParameterDirection(string inout)
		{
			switch (inout)
			{
				case "IN": return ParameterDirection.Input;
				case "OUT": return ParameterDirection.Output;
				case "INOUT": return ParameterDirection.InputOutput;
			}

			throw new ArgumentException("inout=" + inout);
		}
	}
}
