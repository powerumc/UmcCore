using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		객체의 처리가 완료된 후의 완료 결과를 받을 수 있는 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
	/// <typeparam name="TReturn">반환되는 값의 타입입니다.</typeparam>
	public interface IResolvable<TInput, TReturn>
	{
		/// <summary>
		///		객체의 상태를 반환합니다.
		/// </summary>
		/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
		/// <param name="inputs">입력 값입니다.</param>
		TReturn Resolve<TInput>(params TInput[] inputs);
	}

	/// <summary>
	///		객체의 처리가 완료된 후의 완료 결과를 받을 수 있는 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TReturn">반환되는 값의 타입입니다.</typeparam>
	public interface IResolvable<TReturn> : IResolvable<object, TReturn>
	{
	}

	/// <summary>
	///		객체의 처리가 완료된 후의 완료 결과를 받을 수 있는 인터페이스 입니다.
	/// </summary>
	public interface IResolvable : IResolvable<object, object>
	{
	}
}
