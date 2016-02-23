using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		<see cref="ParamPropertyMappingProvider"/> 에서 파라메터로 정의된 속성의 행동 방법을 선언할 때 사용하는 플래그 입니다.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue"), Flags]
	public enum ParamAction
	{

		/// <summary>
		///		매개 변수가 전달되는 용도로 사용합니다.
		/// </summary>
		Send = 1,

		/// <summary>
		///		전달된 매개 변수를 받는 용도로 사용합니다.
		/// </summary>
		Receive = Send << 1
	}
}
