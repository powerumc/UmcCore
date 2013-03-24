using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Mapping
{
	public class InvocationMapping<TInput, TReturn> : Mapping<TInput, Action<TInput>>
	{

		protected override void InitializeMapping()
		{
		}

		protected override Action<TInput> ReturnMappedValue(IMappingReturn<TInput, Action<TInput>> @return)
		{
			return base.ReturnMappedValue(@return);
		}


		/// <summary>
		///		매핑된 객체를 입력 값을 키값으로 매핑된 결과 값을 가져옵니다.
		/// </summary>
		/// <param name="input">매핑에 사용된 조건값입니다.</param>
		/// <returns>
		///		<para>매핑된 결과 값을 반환합니다.</para>
		///		<para><see cref="InvocationMapping{TInput, TReturn}"/>에서는 반환되는 값이 없습니다.</para>
		/// </returns>
		public virtual TReturn GetMappingValue(TInput input)
		{
			var action = base.GetMappingValue(input);

			action(input);

			return default(TReturn);
		}
	}
}
