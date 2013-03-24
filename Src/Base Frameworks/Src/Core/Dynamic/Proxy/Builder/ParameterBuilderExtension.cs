using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Builder
{

	/// <summary>	
	/// 	<see cref="ParameterBuilder"/> 를 확장하는 클래스 입니다.
	/// </summary>
	public class ParameterBuilderExtension : BuilderExtensionBase
	{
		public ParameterBuilderExtension(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
			: base(moduleBuilder, typeBuilder)
		{
		}

		public void CreateGenericTypeParameter()
		{
			throw new NotImplementedException();
		}
	}
}
