using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using Umc.Core.Mapping;

namespace Umc.Core.Reflection.Emit
{
	/// <summary>
	///		지정된 배열 인덱스의 형식을 갖는 요소를 계산 스택 위에 로드하기 위한 매핑을 제공하는 클래스 입니다.
	/// </summary>
	public class OpCodeLdelemMapping : LazyMapping<TypeCode, OpCode>
	{
		private Type elementType;

		public OpCodeLdelemMapping(Type elementType)
		{
			this.elementType = elementType;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		protected override void InitializeMapping()
		{
				// 기본 형식인 경우
			this.Map(o => this.elementType.IsPrimitive && o == TypeCode.SByte)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Boolean).Return(o => OpCodes.Ldelem_I1)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Byte).Return(o => OpCodes.Ldelem_U1)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Int16).Return(o => OpCodes.Ldelem_I2)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.UInt16)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Char).Return(o => OpCodes.Ldelem_U2)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Int32).Return(o => OpCodes.Ldelem_I4)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.UInt32).Return(o => OpCodes.Ldelem_U4)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Int64)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.UInt64).Return(o => OpCodes.Ldelem_I8)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Single).Return(o => OpCodes.Ldelem_R4)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Double).Return(o => OpCodes.Ldelem_R8)



				// 값 타입인 경우인 경우
				.Map(o => this.elementType.IsValueType).Return(o => OpCodes.Not)

				// 참조 타입인 경우
				.MapDefault().Return(o => OpCodes.Ldelem_Ref);
		}
	}
}
