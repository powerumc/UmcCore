using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Builder
{

	/// <summary>	
	/// 	동적인 리플랙션 객체에서 사용하는 Builder 클래스를 파생시키는 클래스 입니다.
	/// </summary>
	public abstract class BuilderExtensionBase
	{

		/// <summary>	
		/// 	동적 타입을 생성하는 <see cref="TypeBuilder"/> 를 가져옵니다.
		/// </summary>
		protected TypeBuilder TypeBuilder { get; set; }


		/// <summary>	
		/// 	동적 모듈을 생성하는 <see cref="ModuleBuilder"/> 를 가져옵니다.
		/// </summary>
		protected ModuleBuilder ModuleBuilder { get; set; }

		protected BuilderExtensionBase(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
		{
			this.ModuleBuilder = moduleBuilder;
			this.TypeBuilder   = typeBuilder;
		}
	}
}
