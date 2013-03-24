using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC.Interceptor
{
	public enum FrameworkInterceptorKinds
	{
		/// <summary>
		/// 인터페이스를 구현하는 클래스를 대상으로 프락시를 생성하는 방법입니다.
		/// </summary>
		Interface,

		/// <summary>
		/// .NET Framework 의 리모팅의 내부 프락시를 이용하는 방법입니다. 이 옵션이 적용되면 반드시 ContextBoundObject 를 상속해야 합니다.
		/// </summary>
		TransparentProxy,

		/// <summary>
		/// 클래스의 내부 구현의 Virtual 을 대상으로 프락시를 생성하는 방법입니다.
		/// </summary>
		VirtualMethod,
	}
}
