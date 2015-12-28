using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Data
{
	/// <summary>
	///		이 특성은 데이터베이스의 저장 프로시저의 엔티티를 구현하는 특성의 클래스 입니다.
	/// </summary>
	/// <seealso cref="IStoredProcedureDataModelAttribute"/>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple=false, Inherited=false)]
	public class StoredProcedureDataModelAttribute : DatabaseAttribute, IStoredProcedureDataModelAttribute
	{

		/// <summary>	
		/// 	저장 프로시저 이름을 가져옵니다. 
		/// </summary>
		public string StoredProcedureName { get; private set; }

		public StoredProcedureDataModelAttribute(string databaseName, string storedProcedureName)
			: base(databaseName)
		{
			this.Name = databaseName;
		}
	}
}
