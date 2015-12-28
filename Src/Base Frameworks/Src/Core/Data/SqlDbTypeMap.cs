using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Umc.Core.Mapping;

namespace Umc.Core.Data
{
	public class SqlDbTypeMap : KeyValueMapping<SqlDbType, Type>
	{
		#region Overrides of KeyValueMapping<SqlDbType,Type>

		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected override void InitializeMapping()
		{
			this.Map(SqlDbType.BigInt).Return(typeof (Int64))
			    .Map(SqlDbType.Int).Return(typeof (Int32))
			    .Map(SqlDbType.SmallInt).Return(typeof (Int16))
			    .Map(SqlDbType.Bit).Return(typeof (bool))
			    .Map(SqlDbType.Char)
			    .Map(SqlDbType.NChar)
			    .Map(SqlDbType.VarChar)
			    .Map(SqlDbType.NVarChar).Return(typeof (string))
			    .Map(SqlDbType.Date)
			    .Map(SqlDbType.DateTime)
			    .Map(SqlDbType.DateTime2).Return(typeof (DateTime))
			    .Map(SqlDbType.DateTimeOffset).Return(typeof (DateTimeOffset))
			    .Map(SqlDbType.Decimal)
			    .Map(SqlDbType.Money)
			    .Map(SqlDbType.Real).Return(typeof (double))
			    .Map(SqlDbType.Float).Return(typeof (float))
			    .Map(SqlDbType.Image).Return(typeof (byte[]))
			    .Map(SqlDbType.Binary).Return(typeof (byte[]));
		}

		#endregion
	}
}
