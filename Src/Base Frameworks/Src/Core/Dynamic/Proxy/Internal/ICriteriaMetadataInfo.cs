using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy
{

	/// <summary>	
	/// 	표준적인 메타데이터 정보를 표현하는 인터페이스 입니다.
	/// </summary>
	public interface ICriteriaMetadataInfo
	{

		/// <summary>	
		/// 	표준 메타데이터 토큰의 열거형을 가져옵니다.
		/// </summary>
		CriteriaMetadataToken Token { get; }
		

		/// <summary>	
		/// 	표준적인 메타데이터 정보의 타입을 가져옵니다.
		/// </summary>
		Type Type { get; }


		/// <summary>	
		/// 	표준적인 메타데이터 정보의 이름을 가져옵니다.
		/// </summary>
		string Name { get; }
	}
}