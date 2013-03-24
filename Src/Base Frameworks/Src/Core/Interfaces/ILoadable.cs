using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		데이터소스로 부터 로드할 수 있는 인터페이스 입니다.
	/// </summary>
	public interface ILoadable
	{
		/// <summary>
		///		객체가 데이터 소스로부터 로드할 수 있는지 여부를 가져옵니다.
		/// </summary>
		bool CanLoad { get; }

		/// <summary>
		///		객체가 데이터 소스로부터 데이터를 로드합니다.
		/// </summary>
		void Load();
	}
}
