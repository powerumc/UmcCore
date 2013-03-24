using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy
{

	/// <summary>	
	/// 	동적인 프락시에서 사용하는 토큰 정보 클래스 입니다.
	/// </summary>
	public class DynamicProxyToken
	{

		/// <summary>	
		/// 	토큰의 고유 <see cref="Guid"/> 입니다.
		/// </summary>
		public Guid Token { get; private set; }

		/// <summary>	
		/// 	동적 어셈블리의 토큰입니다.
		/// </summary>
		public DynamicAssemblyToken AssemblyToken { get; set; }

		public DynamicProxyToken()
		{
			this.AssemblyToken = DynamicAssemblyToken.DLL;
			this.CreateUniqueToken();
		}

		private void CreateUniqueToken()
		{
			this.Token = Guid.NewGuid();
		}

		public static bool operator ==(DynamicProxyToken left, DynamicProxyToken right)
		{
			return Object.ReferenceEquals(left, right) && left.Token.Equals(right.Token);
		}

		public static bool operator !=(DynamicProxyToken left, DynamicProxyToken right)
		{
			return !(Object.ReferenceEquals(left, right) && left.Token.Equals(right.Token));
		}

		public override int GetHashCode()
		{
			return this.Token.GetHashCode() ^ this.AssemblyToken.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}


		/// <summary>	
		/// 	동적인 어셈블리의 토큰 형식입니다.
		/// </summary>
		public enum DynamicAssemblyToken
		{

			/// <summary>
			///		일반적으로 실행 가능한 형태의 확장자인 .EXE 입니다.
			/// </summary>
			EXE,

			/// <summary> 
			///		라이브러리 형태의 확장자를 가지는 .DLL 입니다.
			///  </summary>
			DLL
		}
	}
}
