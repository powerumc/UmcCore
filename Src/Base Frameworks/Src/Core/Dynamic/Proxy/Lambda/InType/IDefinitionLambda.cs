using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	대입 연산에서 좌측에 선언되는 형식을 나타내는 인터페이스입니다.
	/// </summary>
	public interface IDefinitionLambda :
		ILocalLambda,
		IFieldLambda,
		IPropertyLambda
	{
	}
}
