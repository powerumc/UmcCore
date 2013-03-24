using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	람다(Lambda) 구현 객체에서 엑세스 제한을 구현하는 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TReturn">리턴되는 객체의 타입입니다.</typeparam>
	public interface IAccessorConfirmLambda<TReturn>
	{
		TReturn IsPublic { get; }
		TReturn IsInternal { get; }
		TReturn IsProtected { get; }
		TReturn IsPrivate { get; }
		TReturn IsStatic { get; }
		TReturn IsReadOnly { get; }
		TReturn IsAbstract { get; }
		TReturn IsSealed { get; }
		TReturn IsOverride { get; }
		TReturn IsVirtual { get; }
	}


	/// <summary>	
	/// 	람다(Lambda) 구현 객체에서 엑세스 제한을 구현하는 인터페이스 입니다.
	/// </summary>
	public interface IAccessorConfirmLambda : IAccessorConfirmLambda<bool>
	{
	}
}