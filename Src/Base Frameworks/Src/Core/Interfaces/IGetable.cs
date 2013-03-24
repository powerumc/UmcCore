using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		<para>개체의 맴버 또는 메타데이터에 Getter 를 구현하는 인터페이스 입니다.</para>
	///		<para>이 인터페이스와 함께 사용할 수 있는 <see cref="ISetable"/> 인터페이스를 참고하십시오.</para>
	/// </summary>
	public interface IGetable<TInput, TReturn>
	{

		/// <summary>	
		/// 	개체가 특정 객체를 반환할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>특정 객체를 반환할 수 있으면 True, 그렇지 않으면 False 입니다.</returns>
		bool CanGetter(TInput input);


		/// <summary>	
		/// 	개체가 특정 객체를 반환합니다.
		/// </summary>
		/// <param name="input">	객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>반환되는 <see cref="Object"/> 입니다.</returns>
		TReturn Getter(TInput input);
	}

	/// <summary>
	///		<para>개체의 맴버 또는 메타데이터에 Getter 를 구현하는 인터페이스 입니다.</para>
	///		<para>이 인터페이스와 함께 사용할 수 있는 <see cref="ISetable"/> 인터페이스를 참고하십시오.</para>
	/// </summary>
	public interface IGetable
	{

		/// <summary>	
		/// 	개체가 특정 객체를 반환할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>특정 객체를 반환할 수 있으면 True, 그렇지 않으면 False 입니다.</returns>
		bool CanGetter(object input);

		/// <summary>
		///		개체가 특정 객체를 반환합니다.
		/// </summary>
		/// <param name="input">객체를 반환할 때 필요한 매개 변수입니다.</param>
		/// <returns><see cref="Object"/> 입니다.</returns>
		object Getter(object input);
	}
}
