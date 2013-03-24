using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		객체의 상태의 필드, 속성 등이 필수 기능인지 여부를 나타내는 인터페이스 입니다.
	/// </summary>
	public interface IRequireable
	{
		/// <summary>
		///		객체의 소속 맴버, 속성 등이 필수 여부인지를 가져옵니다.
		/// </summary>
		bool IsRequired { get; }
	}
}
