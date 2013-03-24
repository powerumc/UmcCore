using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		매핑된 객체의 리턴 값의 형태를 정의하고 관리하는 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
	/// <typeparam name="TReturn">결과 값의 타입입니다.</typeparam>
	public interface IMappingReturn<TInput, TReturn> :	IValuable<TReturn>, 
														IMapping<TInput, TReturn>
	{
		/// <summary>
		///		매핑된 관계의 키의 결과 값을 리턴합니다.
		/// </summary>
		/// <param name="return">리턴되는 객체 입니다.</param>
		/// <returns>리턴되는 매핑이 속하는 <see cref="IMapping{TInput, TReturn}"/> 객체를 반환합니다.</returns>
		IMapping<TInput, TReturn> Return(TReturn @return);
	}
}