using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	Local Field 를 구현하는 인터페이스 입니다.
	/// </summary>
	public interface ILocalLambda
	{

		/// <summary>	
		/// 	Local Field 로 선언합니다.
		/// </summary>
		/// <param name="type">Local Field 타입입니다.</param>
		/// <param name="name">Local Field 의 이름입니다.</param>
		/// <returns>	
		/// 	선언된 Local Field 의 <see cref="Operand"/> 를 반환합니다.
		/// </returns>
		Operand Local(Type type, string name);
	}
}
