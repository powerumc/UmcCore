using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	코드의 블럭(Block) 을 구현하는 인터페이스 입니다.
	/// </summary>
	public interface IBlockLambda
	{

		/// <summary>	
		/// 	코드의 블럭을 시작합니다.
		/// </summary>
		/// <returns>	
		/// 	코드의 블럭을 시작한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 를 반환합니다.
		/// </returns>
		ICodeLambda BeginBlock();


		/// <summary>	
		/// 	코드의 블럭을 종료합니다.
		/// </summary>
		/// <returns>	
		/// 	코드의 블럭을 종료한 후 현재 속하고 있는 <see cref="ICodeLambda"/> 를 반환합니다.
		/// </returns>
		ICodeLambda EndBlock();
	}
}
