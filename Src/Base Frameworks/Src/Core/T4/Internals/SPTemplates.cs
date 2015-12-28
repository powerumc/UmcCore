using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Umc.Core;
using Umc.Core.Data;

namespace Umc.Core.T4.Internals
{
	public class SPTemplates
	{
		private readonly string _connectionString;
		private readonly string SQL_OF_SP_SCHEMA_INFORMATION = @"
SELECT *
  FROM NX_Kart.INFORMATION_SCHEMA.PARAMETERS
 WHERE SPECIFIC_CATALOG = @DbName
   AND SPECIFIC_NAME = @SpName
   AND PARAMETER_NAME NOT IN ('@frk_n4ErrorCode', '@frk_strErrorText', '@frk_isRequiresNewTransaction')
";

		public string DbName { get; set; }
		public string SPName { get; set; }

		public SPTemplates(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IList<SPSchemaT> GetSpSpecific()
		{
			var dt = new DataTable();
			var da = new SqlDataAdapter(SQL_OF_SP_SCHEMA_INFORMATION, _connectionString);
			da.SelectCommand.Parameters.AddWithValue("@DbName", this.DbName);
			da.SelectCommand.Parameters.AddWithValue("@SpName", this.SPName);
			da.Fill(dt);

			var schemas = dt.ToDataModel<SPSchemaT>();
			return schemas;
		}

		public void Transform()
		{
			var schemas = GetSpSpecific();
		}
	}
}
