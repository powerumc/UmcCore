using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Mapping
{

	/// <summary>	
	/// 	<see cref="ReflectionMapping"/> 객체의 매핑에서 사용되는 옵션 클래스 입니다.
	/// </summary>
	public class ReflectionMappingOption
	{
		/// <summary>	
		/// 	매핑할 수 없는 경우 예외를 발생할지 여부를 설정하거나 가져옵니다.
		/// </summary>
		public bool ThrowIfNotMatched { get; set; }

		/// <summary>	
		/// 	메서드의 특정 오버로드에서 사용할 culture, 대/소문자 및 정렬 규칙을 설정하거나 가져옵니다.
		/// </summary>
		public StringComparison StringComparison { get; set; }

		/// <summary>	
		/// 	리플렉션에서 멤버 및 형식 검색이 수행되는 방식과 바인딩을 제어하는 플래그를 설정하거나 가져옵니다.
		/// </summary>
		public BindingFlags BindingFlags { get; set; }

		public ReflectionMappingOption()
		{
			this.ThrowIfNotMatched = false;
			this.StringComparison = System.StringComparison.OrdinalIgnoreCase;
			this.BindingFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
		}
	}
}
