using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using Umc.Core.Dynamic.Proxy.Builder;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	좌측 값이 Field 인 경우 수행하는 대입 연산 클래스 입니다.
	/// </summary>
	internal class FieldOperand :	Operand,
									IValuable<FieldBuilder>
	{
		private ITypeLambda typeLambda;
		private ICriteriaMetadataInfo criteriaMetadataInfo;
		private FieldBuilder fieldBuilder;

		public FieldOperand(ITypeLambda typeLambda, ILGenerator ilGenerator, ICriteriaMetadataInfo criteriaMetadataInfo)
			: base(typeLambda, ilGenerator)
		{
			this.typeLambda           = typeLambda;
			this.criteriaMetadataInfo = criteriaMetadataInfo;
		}


		/// <summary>	
		/// 	/Emit Byte 코드를 씁니다. 
		/// </summary>
		/// <param name="codeLambda">	구현부 코드에 쓸 <see cref="ICodeLambda"/> 인터페이스를 구현하는 객체입니다. </param>
		public override void WriteEmit(ICodeLambda codeLambda)
		{
			this.fieldBuilder = fieldBuilder = new FieldBuilderExtension(null, this.typeLambda.TypeBuilder)
													.CreateField(criteriaMetadataInfo.Name, criteriaMetadataInfo.Type, this.typeLambda.FieldAccessor.FieldAttribute);
		}


		/// <summary>	
		/// 	Emit Byte코드를 읽습니다. 
		/// </summary>
		/// <param name="codeLambda">	<see cref="ICodeLambda"/> 를 구현하는 구현부 코드입니다. </param>
		public override void ReadEmit(ICodeLambda codeLambda)
		{
			// Field 선언시 ILGenerator 상태가 없기 때문에, FieldOperand ReadEmit 호출 시에 IL 넘겨줌
			if (this.IL == null)
				this.IL = codeLambda.IL;

			this.IL.Emit(OpCodes.Ldarg_0);
			this.IL.Emit(OpCodes.Ldfld, this.Value);
		}


		/// <summary>	
		/// 	<see cref="FieldOperand"/> 가 소유하고 있는 <see cref="FieldBuilder"/> 객체를 가져옵니다.
		/// </summary>
		public FieldBuilder Value
		{
			get { return this.fieldBuilder; }
		}
	}
}
