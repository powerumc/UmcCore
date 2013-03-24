using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using Umc.Core.Mapping;

namespace Umc.Core.Reflection.Emit
{
	/// <summary>
	///		인덱스 n에 있는 인수를 계산 스택으로 로드하기 위한 매핑을 제공하는 클래스 입니다.
	/// </para>
	/// </summary>
	public class OpCodeLdargMapping : LazyMapping<ushort, OpCode>
	{
		private ushort uvalue;

		public OpCodeLdargMapping(ushort uvalue)
		{
			this.uvalue = uvalue;
		}

		protected override void InitializeMapping()
		{
			// MSIL 이 제공하는 인덱스 범위에 있는 경우
			this.Map(o => o == 0).Return(o => OpCodes.Ldarg_0)
				.Map(o => o == 1).Return(o => OpCodes.Ldarg_1)
				.Map(o => o == 2).Return(o => OpCodes.Ldarg_2)
				.Map(o => o == 3).Return(o => OpCodes.Ldarg_3)

				// Byte 타입 범위에 있는 경우
				// TODO: Emit(OpCodes.Ldarg_S, value)
				.Map(o => o <= Byte.MaxValue).Return(o => OpCodes.Not)

				// MSIL 이 제공하는 인덱스 범위에 있지 않은 경우
				.MapDefault().Return( o => OpCodes.Ldarg);
		}
	}
}
