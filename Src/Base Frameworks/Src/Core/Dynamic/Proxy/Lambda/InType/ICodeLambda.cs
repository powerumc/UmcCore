using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	구현부에 속하는 코드를 구현하는 인터페이스 입니다.
	/// </summary>
	public interface ICodeLambda : 
		IAssignLambda,
		IBlockLambda,
		ICallLambda,
		IIfLambda,
		ILocalLambda,
		IKeywordLambda,
		INativeLambda<ILGenerator>,
		IExceptionHandleLambda
	{

		/// <summary>	
		/// 	이 <see cref="ICodeLambda"/> 가 속하고 있는 <see cref="ITypeLambda"/> 객체를 반환합니다.
		/// </summary>
		ITypeLambda TypeLambda { get; }


		/// <summary>	
		/// 	이 <see cref="ICodeLambda"/> 에서 사용하는 <see cref="ILGenerator"/> 객체를 반환합니다.
		/// </summary>
		ILGenerator IL { get; }
	}
}
