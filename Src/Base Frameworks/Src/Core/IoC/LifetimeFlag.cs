using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC
{
	/// <summary>
	///		<see cref="IFrameworkContainer"/> 컨테이너의 객체의 라이프타임에 대한 열거형입니다.
	/// </summary>
	public enum LifetimeFlag
	{
		/// <summary>
		///		IoC 컨테이너를 구현한 기반 프레임워크의 정책을 따릅니다.
		/// </summary>
		Default,
		/// <summary>
		///		<see cref="IFrameworkContainer"/> 에서 꺼내지면 바로 객체를 생성합니다.
		/// </summary>
		PerCall,
		/// <summary>
		///		<see cref="IFrameworkContainer"/> 쓰레드 내에서 객체를 생성합니다.
		/// </summary>
		PerThread,
		/// <summary>
		///		<see cref="IFrameworkContainer"/> 에서 정적 싱글톤 객체를 반환합니다.
		/// </summary>
		Singleton,
		External,
		/// <summary>
		///		<see cref="IFrameworkContainer"/> 에서 Child간의 구조적인 상생관계의 객체를 생성합니다.
		/// </summary>
		Hierarchy
	}
}
