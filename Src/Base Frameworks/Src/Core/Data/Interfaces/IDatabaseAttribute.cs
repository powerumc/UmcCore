using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Data
{
	/// <summary>
	///		이 인터페이스를 구현하는 클래스는 특정 데이터베이스에 종속된 클래스의 엔티티임을 명시하는 인터페이스 입니다.
	/// </summary>
	public interface IDatabaseAttribute
	{
		/// <summary>
		///		데이터베이스 이름을 가져옵니다.
		/// </summary>
		string Name { get; }
	}
}
