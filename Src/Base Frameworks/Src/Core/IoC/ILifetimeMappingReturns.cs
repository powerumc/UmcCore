using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC
{
	/// <summary>
	///		<see cref="IFrameworkContainer"/> 에서 공통으로 사용하는 IoC 컨테이너의 Lifecycle 또는 Lifetime 을 매핑하여
	///		IoC 기반 프레임워크의 Lifetime 을 생성 위한 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TLifetime">기반 프레임워크에서 사용하는 IoC 컨테이너의 Lifetime 타입입니다.</typeparam>
	public interface ILifetimeMappingReturn<TLifetime>
	{

		/// <summary>	
		/// 	매핑된 조건에 일치하는 반환 객체를 생성합니다.
		/// </summary>
		/// <param name="createLifetime">반환되는 객체를 생성하는 대리자입니다.</param>
		/// <returns>현재 <see cref="ILifetimeMappingReturn{TLifetime}"/> 에 속하는 <see cref="ILifetimeMapping{TLifetime}"/> 객체를 반환합니다.</returns>
		ILifetimeMapping<TLifetime> Return(Func<TLifetime, TLifetime> createLifetime);


		/// <summary>	
		/// 	반환되는 객체의 대리자를 가져옵니다.
		/// </summary>
		Func<TLifetime, TLifetime> ReturnAction { get; }
	}
}
