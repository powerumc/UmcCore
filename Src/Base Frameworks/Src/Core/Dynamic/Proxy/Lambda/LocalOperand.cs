using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Umc.Core.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	좌측 값이 Local Field 인 경우 수행하는 대입 연산 클래스 입니다.
	/// </summary>
	internal class LocalOperand :	Operand,
									IValuable<LocalBuilder>
	{
		private ITypeLambda typeLambda;
		private ICriteriaMetadataInfo criteriaMetadataInfo;
		private LocalBuilder localBuilder;
		private static OpCodeI4Mapping mapping = new OpCodeI4Mapping();

		public LocalOperand(ITypeLambda typeLambda, ILGenerator ilGenerator, ICriteriaMetadataInfo criteriaMetadataInfo)
			: base(typeLambda, ilGenerator)
		{
			this.typeLambda           = typeLambda;
			this.criteriaMetadataInfo = criteriaMetadataInfo;
		}


		/// <summary>	
		/// 	Emit Byte 코드를 씁니다. 
		/// </summary>
		/// <param name="codeLambda">	구현부 코드에 쓸 <see cref="ICodeLambda"/> 인터페이스를 구현하는 객체입니다. </param>
		public override void WriteEmit(ICodeLambda codeLambda)
		{
			this.localBuilder = codeLambda.IL.DeclareLocal(this.criteriaMetadataInfo.Type);
			localBuilder.SetLocalSymInfo(this.criteriaMetadataInfo.Name);
		}


		/// <summary>	
		/// 	Emit Byte코드를 읽습니다. 
		/// </summary>
		/// <param name="codeLambda">	<see cref="ICodeLambda"/> 를 구현하는 구현부 코드입니다. </param>
		public override void ReadEmit(ICodeLambda codeLambda)
		{
			this.IL.Emit(OpCodes.Ldloc, this.Value);
		}


		/// <summary>	
		/// 	<see cref="LocalOperand"/> 가 소유하고 있는 <see cref="LocalBuilder"/> 객체를 가져옵니다.
		/// </summary>
		public LocalBuilder Value
		{
			get { return this.localBuilder; }
		}
	}
}
