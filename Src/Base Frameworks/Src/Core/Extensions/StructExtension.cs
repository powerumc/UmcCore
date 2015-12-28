using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		구조체 타입의 확장 클래스 입니다.
	/// </summary>
	public static class StructExtension
	{
		/// <summary>
		///		<para>If 람다식을 사용하는 확장 클래스 입니다.</para>
		///		<para>클래스 타입에 사용하려면 <see cref="ObjectExtension.If"/> 확장 메서드를 이용하십시오.</para>
		/// </summary>
		///<typeparam name="T">객체의 타입입니다.</typeparam>
		/// <param name="object">If 람다식을 사용할 객체 입니다.</param>
		/// <param name="func">If 람다식의 조건문입니다.</param>
		/// <returns>
		///		객체의 람다를 표현하는 <see cref="LambdaIfObject{T}"/> 를 반환합니다.
		/// </returns>
		public static LambdaIfObject<T> Ifs<T>(this T @object, Func<T, bool> func)
			where T : struct
		{
			return new LambdaIfObject<T>(@object, func);
		}
	}
}
