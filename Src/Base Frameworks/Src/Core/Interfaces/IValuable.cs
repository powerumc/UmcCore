using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		값을 가질 수 있는 개체를 정의하는 인터페이스 입니다.
	/// </summary>
	public interface IValuable
	{

		/// <summary>	
		/// 	개체의 값을 가져옵니다.
		/// </summary>
		object Value { get; }
	}

	/// <summary>
	///		값을 가질 수 있는 개체를 정의하는 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TReturn">리턴 타입입니다.</typeparam>
	public interface IValuable<TReturn>
	{
		/// <summary>
		///		개체의 값을 가져옵니다.
		/// </summary>
		TReturn Value { get; }
	}
}
