using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Builder
{

	/// <summary>	
	/// 	<see cref="FieldBuilder"/> 의 확장 클래스 입니다.
	/// </summary>
	/// <remarks>	
	/// 	Umc, 2011-01-13. 
	/// </remarks>
	public  class FieldBuilderExtension : BuilderExtensionBase
	{
		public FieldBuilderExtension(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
			: base(moduleBuilder, typeBuilder)
		{
		}


		/// <summary>	
		/// 	동적 필드를 생성합니다.
		/// </summary>
		/// <param name="fieldName">필드 이름입니다.</param>
		/// <param name="type">	필드의 타입입니다.</param>
		/// <param name="fieldAttributes">필드의 특성을 나타내는 속성입니다.</param>
		/// <returns>	
		/// 	. 
		/// </returns>
		public FieldBuilder CreateField(string fieldName, Type type, FieldAttributes fieldAttributes)
		{
			var fieldBuilder = this.TypeBuilder.DefineField(fieldName, type, fieldAttributes);

			return fieldBuilder;
		}
	}
}