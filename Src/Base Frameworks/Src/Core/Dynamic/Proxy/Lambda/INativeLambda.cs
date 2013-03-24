using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	<see cref="ICodeLambda"/> 이 Byte Code 수준에서 코드를 작성하기 위한 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TGenerator">MSIL 수준의 코드를 작성하는 Generator 입니다.</typeparam>
	public interface INativeLambda<TGenerator>
	{
		/// <summary>	
		/// 	<typeparam name="TGenerator" /> 를 반환합니다.
		/// </summary>
		TGenerator Emit { get; }


		/// <summary>	
		/// 	IL 코드로 부터 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </summary>
		ICodeLambda EmitFromSource();
	}
}
