using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using Umc.Core.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	대입 연산을 수행하는 <see cref="Operand"/> 클래스 입니다.
	/// </summary>
	internal class AssignOperand : Operand
	{
		private ITypeLambda typeLambda;

		private Operand left;
		private Operand right;

		private OpCodeI4Mapping opcodeI4Mapping = new OpCodeI4Mapping();

		public AssignOperand(ITypeLambda typeLambda, ILGenerator ilGenerator, Operand left, Operand right)
			: base(typeLambda, ilGenerator)
		{
			this.typeLambda = typeLambda;

			this.left = left;
			this.right = right;
		}


		/// <summary>	
		/// 	<see cref="AssignOperand"/> 를 구현하는 코드의 Emit Byte 코드를 씁니다. 
		/// </summary>
		/// <param name="codeLambda">	구현부 코드에 쓸 <see cref="ICodeLambda"/> 인터페이스를 구현하는 객체입니다. </param>
		public override void WriteEmit(ICodeLambda codeLambda)
		{
			if (right is LocalOperand)
			{
				var leftLocal = (LocalOperand)left;
				var rightLocal = (LocalOperand)right;

				if (rightLocal.Value.LocalType.IsPrimitive == true)
				{
					//OpCode opcode = opcodeI4Mapping.GetMappingValue(local.Value.3)
					IL.Emit(OpCodes.Ldloc, rightLocal.Value);
					IL.Emit(OpCodes.Stloc, leftLocal.Value);
				}
				
			}
			else if (right is FieldOperand)
			{
			}
			else if (right is MethodOperand)
			{
			}
		}
	}
}
