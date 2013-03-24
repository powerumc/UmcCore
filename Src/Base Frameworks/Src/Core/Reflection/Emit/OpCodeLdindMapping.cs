using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using Umc.Core.Mapping;

namespace Umc.Core.Reflection.Emit
{
	/// <summary>
	///		형식의 값을 스택에 간접적으로 로드하기 위한 매핑을 제공하는 클래스 입니다.
	/// </summary>
	public class OpCodeLdindMapping : LazyMapping<TypeCode, OpCode>
	{
		private Type elementType;

		public OpCodeLdindMapping(Type elementType)
		{
			this.elementType = elementType;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		protected override void InitializeMapping()
		{
			// 기본 형식인 경우
			this.Map(o => this.elementType.IsPrimitive && o == TypeCode.SByte)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Boolean).Return(o => OpCodes.Ldind_I1)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Byte).Return(o => OpCodes.Ldind_U1)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Int16).Return(o => OpCodes.Ldind_I2)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.UInt16)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Char).Return(o => OpCodes.Ldind_U2)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Int32).Return(o => OpCodes.Ldind_I4)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.UInt32).Return(o => OpCodes.Ldind_U4)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Int64)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.UInt64).Return(o => OpCodes.Ldind_I8)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Single).Return(o => OpCodes.Ldind_R4)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Double).Return(o => OpCodes.Ldind_R8)

			// 값 타입 형식인 경우
				// TODO : Emit(OpCodes.Ldobj, type);
				.Map(o => this.elementType.IsValueType).Return(o => OpCodes.Ldobj)

			// 참고 타입인 경우
				.MapDefault().Return(o => OpCodes.Ldind_Ref);
		}
	}
}
