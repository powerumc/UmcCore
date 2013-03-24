using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC
{
	/// <summary>
	///		<para>런타임 또는 부트스트래퍼가 필요한 경우 <see cref="IFrameworkContainer"/> 의 구성 작업을 시작합니다.</para>
	///		<para>만약 재구성이 필요한 경우도 이 인터페이스를 구현하십시오. </para>
	/// </summary>
	public interface IFrameworkComposable
	{
		void Compose();
	}


	/// <summary>
	///		<para>런타임 또는 부트스트래퍼가 필요한 경우 <see cref="IFrameworkContainer"/> 의 구성 작업을 시작합니다.</para>
	///		<para>만약 재구성이 필요한 경우도 이 인터페이스를 구현하십시오. </para>
	/// </summary>
	/// <typeparam name="TContainer">IoC 컨테이너의 타입 입니다.</typeparam>
	public interface IFrameworkComposable<TContainer> : IFrameworkComposable
	{
	}
}