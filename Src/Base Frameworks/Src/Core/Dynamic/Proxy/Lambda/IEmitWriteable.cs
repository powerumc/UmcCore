using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	Emit Byte 코르르 쓸 수 있는 인터페이스 입니다.
	/// </summary>
	public interface IEmitWriteable
	{

		/// <summary>	
		/// 	Emit Byte 코드를 씁니다.
		/// </summary>
		/// <param name="codeLambda">구현부 코드에 쓸 <see cref="ICodeLambda"/> 인터페이스를 구현하는 객체입니다.</param>
		void WriteEmit(ICodeLambda codeLambda);
	}
}
