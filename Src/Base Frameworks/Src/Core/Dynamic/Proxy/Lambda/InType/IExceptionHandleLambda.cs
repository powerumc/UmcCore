using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	구현 코드에서 예외를 처리하는 인터페이스 입니다.
	/// </summary>
	public interface IExceptionHandleLambda
	{

		/// <summary>	
		/// 	예외 처리를 수행합니다.
		/// </summary>
		/// <returns>	
		/// 	예외 처리를 수행한 후, 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		ICodeLambda Try();


		/// <summary>	
		/// 	예외가 발생한 경우 처리를 합니다.
		/// </summary>
		/// <returns>	
		/// 	예외 발생의 처리를 수행한 후, 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Catch")]
		ICodeLambda Catch();

		/// <summary>	
		/// 	예외가 발생한 경우 처리를 합니다. 
		/// </summary>
		/// <param name="catchType">선언되는 타입의 <see cref="Exception"/> 의 상속 수준에서 예외 처리를 합니다.</param>
		/// <returns>	
		/// 	예외 발생의 처리를 수행한 후, 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Catch")]
		ICodeLambda Catch(Type catchType);


		/// <summary>	
		/// 	모든 예외 처리 블럭이 종료가 되는 경우 처리를 합니다.
		/// </summary>
		/// <returns>	
		/// 	예외 처리의 모든 블럭을 수행한 후, 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Finally")]
		ICodeLambda Finally();
	}
}
