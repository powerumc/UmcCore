using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{

	/// <summary>	
	/// 	개체가 특정 조건에 만족하는지 여부를 구현하는 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
	public interface IMatchable<TInput>
	{

		/// <summary>	
		/// 	객체가 조건에 만족하는지 여부를 반환합니다.
		/// </summary>
		/// <param name="input">조건으로 사용하는 매개 변수 입니다.</param>
		/// <returns>조건에 만족하면 True, 그렇지 않으면 False 를 반환합니다.</returns>
		bool IsMatches(TInput input);
	}


	/// <summary>	
	/// 	개체가 특정 조건에 만족하는지 여부를 구현하는 인터페이스 입니다.
	/// </summary>
	public interface IMatchable
	{
		/// <summary>	
		/// 	객체가 조건에 만족하는지 여부를 반환합니다.
		/// </summary>
		/// <returns>조건에 만족하면 True, 그렇지 않으면 False 를 반환합니다.</returns>
		bool IsMatches();
	}
}
