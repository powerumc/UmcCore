using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	언어(Language) 사양에서 지원하는 키워드(Keyword)를 구현하는 인터페이스 입니다.
	/// </summary>
	public interface IKeywordLambda
	{

		/// <summary>	
		/// 	New 키워드를 실행합니다.
		/// </summary>
		/// <param name="typeLambda">	The type lambda. </param>
		/// <returns>	
		/// 	키워드를 실행한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "New")]
		ICodeLambda New(ITypeLambda typeLambda);


		/// <summary>	
		/// 	Return 키워드를 실행합니다.
		/// </summary>
		/// <returns>	
		/// 	키워드를 실행한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		ICodeLambda Return();



		/// <summary>	
		/// 	Return 키워드를 실행합니다. 
		/// </summary>
		/// <param name="operand">반환될 값을 가지고 있는 <see cref="Operand"/> 입니다.</param>
		/// <returns>	
		/// 	키워드를 실행한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		/// </returns>
		ICodeLambda Return(Operand operand);


		/// <summary>	
		/// 	임의의 블럭(Block) 의 실행을 종료하는 Break 키워드를 실행합니다.
		/// </summary>
		/// <returns>	
		/// 	키워드를 실행한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		/// </returns>
		ICodeLambda Break();


		/// <summary>	
		/// 	임의의 루프(Loop) 의 다음 실행을 수행하는 Contunue 키워드를 실행합니다.
		/// </summary>
		/// <returns>	
		/// 	키워드를 실행한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Continue")]
		ICodeLambda Continue();
	}
}
