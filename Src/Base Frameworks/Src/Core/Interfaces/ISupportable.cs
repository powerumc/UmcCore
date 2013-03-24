using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		객체가 기능의 지원 여부를 나타내는 인터페이스 입니다.
	/// </summary>
	public interface ISupportable
	{
		/// <summary>
		/// 객체가 기능의 지원을 할 수 있는지 여부를 가져옵니다.
		/// </summary>
		bool IsSupport { get; }
	}
}
