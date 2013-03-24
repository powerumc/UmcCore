using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC
{
	/// <summary>
	///		IoC 컨테이너 기반 프레임워크마다 Lifecycle 또는 Lifetime 이 틀리기 때문에 /// <see cref="IFrameworkContainer"/> 에서 이것을 매핑하는 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TLifetime">기반 프레임워크에서 사용하는 IoC 컨테이너의 Lifetime 타입입니다.</typeparam>
	public interface ILifetimeMapping<TLifetime>
	{
		/// <summary>
		///		기반 프레임워크의 Lifetime 또는 LifeStyle 을 <see cref="IFrameworkContainer"/> 의 LifetimeFlag 로 매핑합니다.
		/// </summary>
		/// <param name="func">매핑하는 조건입니다.</param>
		/// <returns>매핑에 만족하는 <see cref="ILifetimeMappingReturn{TLifetime}"/> 객체를 반환합니다.</returns>
		ILifetimeMappingReturn<TLifetime> Map(Func<LifetimeFlag, bool> func);

		/// <summary>
		///		<see cref="IFrameworkContainer"/>에 매핑된 <see cref="LifetimeFlag"/>를 제거합니다.
		/// </summary>
		/// <param name="flag">IoC 컨테이너의 Lifetime 플래그 입니다.</param>
		/// <returns>제거할 때 사용된 <see cref="ILifetimeMapping{TLifetime}"/> 객체를 반환합니다.</returns>
		ILifetimeMapping<TLifetime> RemoveMap(LifetimeFlag flag);

		/// <summary>
		///		<see cref="IFrameworkContainer"/>에 매핑되지 않은 모든 Lifetime 을 가져온 후 일괄적으로 Lifetime을 매핑할 수 있습니다.
		/// </summary>
		/// <returns>매핑에 만족하는 <see cref="ILifetimeMappingReturn{TLifetime}"/> 객체를 반환합니다.</returns>
		ILifetimeMappingReturn<TLifetime> MapAnothers();

		/// <summary>
		///		<see cref="IFrameworkContainer"/>에 매핑된 <see cref="LifetimeFlag"/>값으로 기반 프레임워크의 Lifetime 객체를 가져옵니다.
		/// </summary>
		/// <param name="flag">IoC 컨테이너의 Lifetime 플래그 입니다.</param>
		/// <returns></returns>
		TLifetime GetLifetimeObject(LifetimeFlag flag);

		/// <summary>
		///		<see cref="IFrameworkContainer"/>에 매핑된 <see cref="LifetimeFlag"/>값으로 기반 프레임워크의 Lifetime 객체를 가져옵니다.
		/// </summary>
		/// <param name="flag">IoC 컨테이너의 Lifetime 플래그 입니다.</param>
		/// <param name="currentLifetimeObject">반환에 필요로 하는 <see cref="ILifetimeMapping{TLifetime}"/> 객체입니다.</param>
		/// <returns></returns>
		TLifetime GetLifetimeObject(LifetimeFlag flag, TLifetime currentLifetimeObject);
	}
}
