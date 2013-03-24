using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	public interface IReleaseType
	{
		/// <summary>
		///		<para>동적 타입의 생성을 완료하여 런타임에 적재합니다.</para>
		/// </summary>
		/// <returns>
		///		<para>메타데이터로 정의된 동적 개체를 반환합니다.</para>
		///		<para>일반적으로 릴리즈된 타입은 더 이상 수정할 수 없습니다.</para>
		/// </returns>
		Type ReleaseType();
	}
}
