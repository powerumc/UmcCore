using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	Enum 타입의 람다(Lambda)를 구현하는 인터페이스 입니다.
	/// </summary>
	public interface IEnumLambda : IReleaseType
	{

		/// <summary>	
		/// 	리터럴을 선언합니다.
		/// </summary>
		/// <param name="name">Enum의 리터럴 이름입니다.</param>
		/// <param name="value">Enum의 리터럴 값입니다.</param>
		void DefineLiteral(string name, object value);
	}

}
