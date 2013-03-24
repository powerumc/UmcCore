using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Mapping;
using System.Reflection.Emit;

namespace Umc.Core.Reflection.Emit
{
	/// <summary>
	///		 계산 스택 맨 위에 있는 값을 변환하여 int32로 확장하기 위한 매핑을 제공하는 클래스 입니다.
	/// </summary>
	public class OpCodeConvMapping : KeyValueMapping<TypeCode, OpCode>
	{
		protected override void InitializeMapping()
		{
			this.Map(TypeCode.SByte).Return(OpCodes.Conv_I1)
				.Map(TypeCode.Byte).Return(OpCodes.Conv_U1)

				.Map(TypeCode.Int16).Return(OpCodes.Conv_I2)
				.Map(TypeCode.UInt16).Return(OpCodes.Conv_U2)

				.Map(TypeCode.Int32).Return(OpCodes.Conv_I4)
				.Map(TypeCode.UInt32).Return(OpCodes.Conv_U4)

				.Map(TypeCode.Int64).Return(OpCodes.Conv_I8)
				.Map(TypeCode.UInt64).Return(OpCodes.Conv_U8)

				.Map(TypeCode.Single).Return(OpCodes.Conv_R4)

				.Map(TypeCode.Double).Return(OpCodes.Conv_R8);
		}
	}
}