using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Data
{
	/// <summary>
	///		이 인터페이스를 구현하는 클래스는 데이터베이스의 테이블의 엔티티임을 명시합니다.
	/// </summary>
	public interface ITableDataModelAttribute
	{
		/// <summary>
		/// 테이블 이름을 가져옵니다.
		/// </summary>
		string TableName { get; }
	}
}

