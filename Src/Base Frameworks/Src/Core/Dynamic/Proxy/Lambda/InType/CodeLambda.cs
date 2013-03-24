using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{


	public class CodeLambda : ICodeLambda
	{
		public ITypeLambda TypeLambda { get; private set; }
		public ILGenerator IL { get; internal set; }

		public CodeLambda(ITypeLambda typeLambda, ILGenerator ilGenreator)
		{
			this.TypeLambda = typeLambda;
			this.IL = ilGenreator;
		}

		public Operand Assign(Operand left, Operand right)
		{
			var operand = new AssignOperand(this.TypeLambda, this.IL, left, right);
			operand.WriteEmit(this);

			return operand;
		}

		public Operand AssignValue(Operand left, object right)
		{
			var operand = new AssignValueOperand(this.TypeLambda, this.IL, left, right);
			operand.WriteEmit(this);

			return operand;
		}

		public Operand AssignValueToProperty(Operand left)
		{
			throw new NotImplementedException();
		}

		public ICodeLambda BeginBlock()
		{
			throw new NotImplementedException();
		}

		public ICodeLambda EndBlock()
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Call()
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Call(Operand operand)
		{
			operand.WriteEmit(this);

			return this;
		}

		public ICodeLambda Call(MethodInfo methodInfo, params Operand[] methodArguments)
		{
			var operand = new MethodOperand(this.TypeLambda, this.IL, methodInfo, methodArguments);
			operand.WriteEmit(this);

			return operand;
		}

		public Operand Local(Type type, string name)
		{
			ICriteriaMetadataInfo criteriaMetadataInfo = new CriteriaMetadataInfo(type, name, CriteriaMetadataToken.Local);
			var operand = new LocalOperand(this.TypeLambda, this.IL, criteriaMetadataInfo);
			operand.WriteEmit(this);

			return operand;
		}

		//public ICodeLambda New()
		//{
		//    return this.New(Operand.Empty);
		//}

		public ICodeLambda New(ITypeLambda typeLambda)
		{
			if (typeLambda.TypeAccessor.IsStatic)
			{
				throw new DynamicProxyException(ExceptionRS.정적_타입의_ITypeLambda는_개체를_생성할_수_없습니다);
			}
			else
			{
				// 파라메터를 스택에 로드
				//foreach (var parameter in constructorParameterOperand)
				//{
				//    parameter.ReadEmit(this);
				//}

				this.IL.Emit(OpCodes.Newobj, this.TypeLambda.TypeBuilder.DeclaringType);
			}

			return this;
		}

		public ICodeLambda Return()
		{
			this.IL.Emit(OpCodes.Ret);

			return this;
		}

		public ICodeLambda Return(Operand operand)
		{
			operand.ReadEmit(this);
			this.IL.Emit(OpCodes.Ret);

			return this;
		}

		public ICodeLambda Break()
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Continue()
		{
			throw new NotImplementedException();
		}

		public ILGenerator Emit { get { return this.IL; } }

		public ICodeLambda EmitFromSource()
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Try()
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Catch()
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Catch(Type catchType)
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Finally()
		{
			throw new NotImplementedException();
		}

		public virtual void WriteEmit(ICodeLambda codeLambda)
		{
			throw new NotImplementedException();
		}
	}
}
