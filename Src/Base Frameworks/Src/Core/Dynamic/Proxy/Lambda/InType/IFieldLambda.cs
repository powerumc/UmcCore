using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	Field 를 표현하는 인터페이스 입니다.
	/// </summary>
	public interface IFieldLambda :
		IAccessorLambda
	{
		ICodeLambda Code();
	}
}
