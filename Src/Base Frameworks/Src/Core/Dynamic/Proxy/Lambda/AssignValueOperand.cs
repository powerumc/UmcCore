using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using Umc.Core.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	<para>대입 연산을 수행하는 <see cref="Operand"/> 클래스 입니다.</para>
	///		<para>이 대입 연산을 우측 값이 Value 값이 오는 경우입니다.</para>
	/// </summary>
	public class AssignValueOperand : Operand
	{
		private ITypeLambda typeLambda;

		private Operand left;
		private object right;

		private static OpCodeI4Mapping mapping = new OpCodeI4Mapping();

		public AssignValueOperand(ITypeLambda typeLambda, ILGenerator ilGenerator, Operand left, object right)
			: base(typeLambda, ilGenerator)
		{
			this.typeLambda = typeLambda;

			this.left  = left;
			this.right = right;
		}


		/// <summary>	
		/// 	<see cref="AssignOperand"/> 를 구현하는 Emit Byte 코드를 씁니다. 
		/// </summary>
		/// <param name="codeLambda">	구현부 코드에 쓸 <see cref="ICodeLambda"/> 인터페이스를 구현하는 객체입니다. </param>
		/// <exception cref="NotSupportedException">
		///		대입 연산을 좌측 선언이 메서드인 경우 발생하는 예외입니다.
		/// </exception>
		public override void WriteEmit(ICodeLambda codeLambda)
		{
			if (left is LocalOperand)
			{
				this.IL.Emit(OpCodes.Nop);

				var leftLocal = (LocalOperand)left;
				var rightTypeCode = Type.GetTypeCode(right.GetType());

				if (rightTypeCode == TypeCode.String)
				{
					this.IL.Emit(OpCodes.Ldstr, (string)right);
				}
				else 
				{
					var opcode = mapping.GetMappingValue((int)right);

					if (opcode.OperandType == OperandType.InlineNone) this.IL.Emit(opcode);
					else this.IL.Emit(opcode, (int)right);
				}

				this.IL.Emit(OpCodes.Stloc, leftLocal.Value);
				//this.IL.Emit(OpCodes.Nop);
			}
			else if (left is FieldOperand)
			{
				this.IL.Emit(OpCodes.Nop);

				var leftLocal = (FieldOperand)left;
				var rightTypeCode = Type.GetTypeCode(right.GetType());

				if (rightTypeCode == TypeCode.String)
				{
					this.IL.Emit(OpCodes.Ldarg_0);
					this.IL.Emit(OpCodes.Ldstr, (string)right);
					this.IL.Emit(OpCodes.Stfld, leftLocal.Value);
				}
				else
				{
					throw new NotSupportedException("FieldOperand/Int");
					//var opcode = mapping.GetMappingValue((int)right);

					//if (opcode.OperandType == OperandType.InlineNone) this.IL.Emit(opcode);
					//else this.IL.Emit(opcode, (int)right);
				}
			}
			else if (left is MethodOperand)
			{
				throw new NotSupportedException("MethodOperand");
			}
		}
	}
}
