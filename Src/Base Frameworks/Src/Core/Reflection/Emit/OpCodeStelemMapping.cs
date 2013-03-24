using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using Umc.Core.Mapping;

namespace Umc.Core.Reflection.Emit
{
	/// <summary>
	///		주어진 인덱스에 있는 배열 요소를 계산 스택에 있는 값으로 바꾸기 위한 매핑을 제공하는 클래스 입니다.
	/// </summary>
	public class OpCodeStelemMapping : LazyMapping<TypeCode, OpCode>
	{
		private Type elementType;

		public OpCodeStelemMapping(Type elementType)
		{
			this.elementType = elementType;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		protected override void InitializeMapping()
		{
			// 기본 형식인 경우
			this.Map(o => this.elementType.IsPrimitive && o == TypeCode.Byte)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.SByte)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Boolean).Return(o => OpCodes.Stelem_I1)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Int16)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.UInt16)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Char).Return(o => OpCodes.Stelem_I2)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Int32)
				.Map(o => this.elementType.IsPrimitive && o == TypeCode.UInt32).Return(o => OpCodes.Stelem_I4)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Single).Return(o => OpCodes.Stelem_R4)

				.Map(o => this.elementType.IsPrimitive && o == TypeCode.Double).Return(o => OpCodes.Stelem_R8)

				// Value Type 인 경우
				.Map(o => this.elementType.IsValueType).Return(o => OpCodes.Stobj)

				// 참조 타입인 경우
				.MapDefault().Return(o => OpCodes.Stelem_Ref);
		}
	}
}