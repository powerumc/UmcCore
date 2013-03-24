using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	명령을 호출하는 인터페이스 입니다.
	/// </summary>
	public interface ICallLambda
	{

		/// <summary>	
		/// 	매개 변수가 없는 <see cref="Operand"/> 를 호출합니다.
		/// </summary>
		/// <returns>	
		/// 	호출을 수행한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Call")]
		ICodeLambda Call();

		/// <summary>	
		/// 	매개 변수가 없는 <see cref="Operand"/> 를 호출합니다.
		/// </summary>
		/// <param name="operand">매개 변수로 사용할 <see cref="Operand"/> 객체 입니다.</param>
		/// <returns>	
		/// 	호출을 수행한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Call")]
		ICodeLambda Call(Operand operand);

		/// <summary>	
		/// 	매개 변수가 없는 <see cref="Operand"/> 를 호출합니다.
		/// </summary>
		/// <param name="methodInfo">실행할 메서드의 정보를 가지고 있는 메서드의 리플랙션 객체입니다.</param>
		/// <param name="methodArguments">매개 변수로 사용할 <see cref="Operand"/> 객체 입니다.</param>
		/// <returns>	
		/// 	호출을 수행한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Call")]
		ICodeLambda Call(MethodInfo methodInfo, params Operand[] methodArguments);
	}
}
