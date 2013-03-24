using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy
{

	/// <summary>	
	/// 	표준 메타데이터 토큰의 열거형 입니다.
	/// </summary>
	public enum CriteriaMetadataToken
	{
		Assembly,
		Module,
		Type,
		Delegate,
		Event,
		Field,
		Property,
		Method,
		Local
	}
}
