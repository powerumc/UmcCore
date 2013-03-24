using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Umc.Core
{
	/// <summary>
	///		객체와 객체간의 매핑 관계를 생성하는 인터페이스 입니다.	
	/// </summary>
	/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
	/// <typeparam name="TReturn">결과 값의 타입입니다.</typeparam>
	public interface IMapping<TInput, TReturn>
	{
		/// <summary>
		///		입력 값을 통하여 관계의 매핑을 정의합니다.
		/// </summary>
		/// <param name="input">매핑에 사용되는 조건입니다.</param>
		/// <returns>
		///		조건에 만족할 경우 반환되는 객체를 정의할 수 있는 <see cref="IMappingReturn{TInput, TReturn}"/> 객체를 반환합니다.
		/// </returns>
		IMappingReturn<TInput, TReturn> Map(TInput input);

		/// <summary>
		///		입력 값을 통하여 이미 정의된 결과간의 관계의 매핑을 정의합니다.
		/// </summary>
		/// <param name="input">매핑에 사용되는 조건입니다.</param>
		/// <param name="return">이미 정의된 결과에 대한 <see cref="IMappingReturn{TInput, TReturn}"/> 객체입니다.</param>
		/// <returns>
		///		조건에 만족할 경우 반환되는 객체를 정의할 수 있는 <see cref="IMappingReturn{TInput, TReturn}"/> 객체를 반환합니다.
		/// </returns>
		IMappingReturn<TInput, TReturn> Map(TInput input, IMappingReturn<TInput, TReturn> @return);

		/// <summary>
		///		매핑되지 않은 객체의 기본 매핑을 정의합니다.
		/// </summary>
		/// <returns>
		///		정의되지 않은 매핑에 대해 반환되는 객체를 정의할 수 있는 <see cref="IMappingReturn{TInput, TReturn}"/> 객체를 반환합니다.
		/// </returns>
		IMappingReturn<TInput, TReturn> MapDefault();

		/// <summary>
		///		매핑된 객체를 입력 값을 키값으로 매핑된 결과 값을 가져옵니다.
		/// </summary>
		/// <param name="input">매핑에 사용된 조건입니다.</param>
		/// <returns>
		///		매핑된 결과 값을 반환합니다.
		/// </returns>
		TReturn GetMappingValue(TInput input);

		/// <summary>
		///		<see cref="IMapping{TInput, TReturn}"/> 의 매핑된 개체에 해당 매핑 키가 존재하는지 여부를 가져옵니다.
		/// </summary>
		/// <param name="input">매핑에 사용된 조건입니다.</param>
		/// <returns>
		///		매핑된 조건이 만족하는 경우 True, 그렇지 않으면 False 를 반환합니다.
		/// </returns>
		bool ContainsKey(TInput input);
	}
}