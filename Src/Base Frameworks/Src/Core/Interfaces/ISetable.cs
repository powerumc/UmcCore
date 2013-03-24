using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		<para>개체의 맴버 또는 메타데이터에 Setter 를 구현하는 인터페이스 입니다.</para>
	///		<para>이 인터페이스와 함께 사용할 수 있는 <see cref="IGetable"/> 인터페이스를 참고하십시오.</para>
	/// </summary>
	public interface ISetable
	{

		/// <summary>	
		/// 	개체가 특정 객체를 설정할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다.</param>
		/// <returns>개체가 특정 객체를 설정할 수 있으면 True, 그렇지 않으면 False</returns>
		bool CanSetter(object input);


		/// <summary>	
		/// 	개체에 값을 설정합니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다.</param>
		/// <param name="arg">객체에 설정하는 값입니다.</param>
		void Setter(object input, object arg);
	}



	/// <summary>	
	/// 	<para>개체의 맴버 또는 메타데이터에 Setter 를 구현하는 인터페이스 입니다.</para>
	///		<para>이 인터페이스와 함께 사용할 수 있는 <see cref="IGetable"/> 인터페이스를 참고하십시오.</para>
	/// </summary>
	/// <typeparam name="TInput">입력되는 매개 변수의 타입입니다.</typeparam>
	/// <typeparam name="TArg">설정되는 값의 타입입니다.</typeparam>
	public interface ISetable<TInput, TArg>
	{

		/// <summary>	
		/// 	개체가 특정 객체를 설정할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다. </param>
		/// <returns>개체가 특정 객체를 설정할 수 있으면 True, 그렇지 않으면 False</returns>
		bool CanSetter(TInput input);


		/// <summary>	
		/// 	개체가 특정 객체를 설정합니다.
		/// </summary>
		/// <param name="input">	객체를 설정할 때 필요한 매개 변수 입니다. </param>
		/// <param name="arg">	설정되는 값의 타입입니다. </param>
		void Setter(TInput input, TArg arg);
	}
}
