using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.IoC;

namespace Umc.Core.IoC
{

	/// <summary>
	///		<para>런타임 또는 부트스트래퍼가 필요한 경우 <see cref="IFrameworkContainer"/> 의 구성 작업을 시작합니다.</para>
	///		<para>만약 재구성이 필요한 경우도 이 인터페이스를 구현하십시오. </para>
	/// </summary>
	public class FrameworkComposerForUnity : IFrameworkComposable
	{

		/// <summary>	
		/// 	구성을 시작합니다.
		/// </summary>
		public void Compose()
		{
		}
	}
}
