using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	Method 가 수행되는 연산을 처리하는 클래스 입니다.
	/// </summary>
	internal class MethodOperand : Operand
	{
		private ITypeLambda typeLambda;
		private MethodInfo methodInfo;
		private Operand[] parameterOperands;

		public MethodOperand(ITypeLambda typeLambda, ILGenerator ilGenerator, MethodInfo methodInfo)
			: base(typeLambda, ilGenerator)
		{
			this.typeLambda = typeLambda;
			this.methodInfo = methodInfo;
		}

		public MethodOperand(ITypeLambda typeLambda, ILGenerator ilGenerator, MethodInfo methodInfo, params Operand[] parameterOperands)
			: this(typeLambda, ilGenerator, methodInfo)
		{
			this.parameterOperands = parameterOperands;
		}


		/// <summary>	
		/// 	Emit Byte 코드를 씁니다. 
		/// </summary>
		/// <param name="codeLambda">	구현부 코드에 쓸 <see cref="ICodeLambda"/> 인터페이스를 구현하는 객체입니다. </param>
		public override void WriteEmit(ICodeLambda codeLambda)
		{
			if (typeLambda.MethodAccessor.IsStatic || this.methodInfo.IsStatic)
			{
				this.WriteEmitOfStaticMethod(codeLambda.IL);
			}
			else
			{
				this.WriteEmitOfInstanceMethod(codeLambda.IL);
			}
		}

		private void WriteEmitOfInstanceMethod(ILGenerator il)
		{
			throw new NotImplementedException();
		}

		private void WriteEmitOfStaticMethod(ILGenerator il)
		{
			if (this.parameterOperands != null)
			{
				var methodInfoParameters = this.methodInfo.GetParameters();

				if (methodInfoParameters.Length != parameterOperands.Length)
					throw new DynamicProxyException(ExceptionRS.O_의_개수가_맞지_않습니다_1_2,
													MessageRS.파라메터, 
													methodInfoParameters.Length.ToString(), 
													parameterOperands.Length.ToString());

				// 호출 메서드의 파라메터 타입이 Object 인 경우 Boxing
				for (int i = 0; i < this.parameterOperands.Length; i++)
				{
					this.parameterOperands[i].ReadEmit(this);

					var operandDeclareType = this.GetOperandDeclareType((IValuable)this.parameterOperands[i]);

					if (methodInfoParameters[i].ParameterType == typeof(Object) &&
						methodInfoParameters[i].ParameterType != operandDeclareType)
					{
						this.IL.Emit(OpCodes.Box, operandDeclareType);
					}
				}
			}

			il.Emit(OpCodes.Call, methodInfo);
		}

		private Type GetOperandDeclareType(IValuable operand)
		{
			if (operand is LocalOperand) return ((LocalOperand)operand).Value.LocalType;
			if (operand is FieldOperand) return ((FieldOperand)operand).Value.FieldType;
			
			// TODO PropertyOperand 추가 해야함
			throw new NotSupportedException(operand.ToString());
		}
	}
}
