using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	Emit Byte코드를 읽을 수 있는 인터페이스 입니다.
	/// </summary>
	public interface IEmitReadable
	{

		/// <summary>	
		/// 	Emit Byte코드를 읽습니다.
		/// </summary>
		/// <param name="codeLambda"><see cref="ICodeLambda"/> 를 구현하는 구현부 코드입니다.</param>
		void ReadEmit(ICodeLambda codeLambda);
	}
}
