using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Data
{
	/// <summary>
	///		이 인터페이스를 구현하는 클래스는 데이터베이스의 저장 프로시저의 엔티티임을 명시합니다.
	/// </summary>
	public interface IStoredProcedureDataModelAttribute : IDatabaseAttribute
	{
		/// <summary>
		///		저장 프로시저 이름을 가져옵니다.
		/// </summary>
		string StoredProcedureName { get; }
	}
}
