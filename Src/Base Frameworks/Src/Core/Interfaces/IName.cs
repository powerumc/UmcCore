using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		개체의 고유 이름을 설정하기 위한 인터페이스 입니다.
	/// </summary>
	public interface IName
	{
		/// <summary>
		///		개체의 이름 또는 Qualified 이름을 가져옵니다.
		/// </summary>
		string Name { get; }
	}
}
