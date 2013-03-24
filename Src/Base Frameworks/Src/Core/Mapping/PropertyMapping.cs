using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Mapping
{

	/// <summary>	
	/// 	리플랙션 수준의 속성(Property) 간의 매핑 관계를 정의하는 클래스 입니다.
	/// </summary>
	/// <typeparam name="TInput">	입력 값의 타입입니다.. </typeparam>
	/// <typeparam name="TReturn">	반환되는 값의 타입입니다.. </typeparam>
	public abstract class PropertyMapping<TInput, TReturn> : KeyValueMapping<TInput, TReturn>
	{

		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected abstract override void InitializeMapping();
	}
}
