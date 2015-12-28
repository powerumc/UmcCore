using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Umc.Core.Mapping;

namespace Umc.Core.Data
{
	public class TypeMap : KeyValueMapping<Type, SqlDbType>
	{
		#region Overrides of KeyValueMapping<Type,SqlDbType>

		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected override void InitializeMapping()
		{
			this.Map(typeof (long)).Return(SqlDbType.BigInt)
			    .Map(typeof (byte[])).Return(SqlDbType.Binary)
			    .Map(typeof (decimal)).Return(SqlDbType.Decimal)
			    .Map(typeof (double)).Return(SqlDbType.Decimal)
			    .Map(typeof (int)).Return(SqlDbType.Int)
			    .Map(typeof (float)).Return(SqlDbType.Float)
			    .Map(typeof (bool)).Return(SqlDbType.Bit)
			    .Map(typeof (string)).Return(SqlDbType.NVarChar)
			    .Map(typeof (DateTime)).Return(SqlDbType.DateTime)
			    .Map(typeof (DateTime?)).Return(SqlDbType.DateTime)
			    .Map(typeof (Guid)).Return(SqlDbType.UniqueIdentifier)
			    .MapDefault();
		}

		#endregion
	}
}
