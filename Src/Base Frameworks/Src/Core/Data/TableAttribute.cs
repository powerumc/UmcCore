using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Data
{
	/// <summary>
	///		이 특성은 데이터베이스의 테이블의 엔티티를 구현하는 특성의 클래스 입니다.
	/// </summary>
	/// <seealso cref="ITableDataModelAttribute"/>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited=false, AllowMultiple=false)]
	public class TableAttribute : Attribute, ITableDataModelAttribute
	{
		/// <summary>	
		/// 	테이블 이름을 가져옵니다. 
		/// </summary>
		public string TableName { get; private set; }

		public TableAttribute(string tableName)
		{
			this.TableName = tableName;
		}
	}
}
