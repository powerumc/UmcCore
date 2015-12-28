using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Umc.Core;

namespace System
{
	/// <summary>
	///		<see cref="Object"/> 객체의 확장 클래스 입니다.
	/// </summary>
    public static class ObjectExtension
    {
        /// <summary>	
        /// 	객체가 NULL 인지 아닌지 여부를 검사합니다.
        /// </summary>
        /// <param name="object">	NULL 여부를 검사할 객체입니다. </param>
        /// <returns>	
        /// 	객체가 NULL 이면 True, NULL 이 아니면 False 를 반환합니다.
        /// </returns>
        public static bool IsNull(this object @object)
        {
            return @object == null;
        }

		public static bool IsNullOrEmpty(this string str)
		{
			return String.IsNullOrEmpty(str);
		}

		public static bool IsNotNullOrEmpty(this string str)
		{
			return !String.IsNullOrEmpty(str);
		}

		/// <summary>
		///		<para>If 람다식을 사용하는 확장 클래스 입니다.</para>
		///		<para>구조체(Struct) 에 사용하려면 <see cref="Ifs"/> 확장 메서드를 이용하십시오.</para>
		/// </summary>
		/// <typeparam name="T">객체의 타입입니다.</typeparam>
		/// <param name="object">If 람다식을 사용할 객체 입니다.</param>
		/// <param name="func">If 람다식의 조건문입니다.</param>
		/// <returns>
		///		객체의 람다를 표현하는 <see cref="LambdaIfObject{T}"/> 를 반환합니다.
		/// </returns>
		public static LambdaIfObject<T> If<T>(this T @object, Func<T, bool> func) 
			where T : class
		{
			return new LambdaIfObject<T>(@object, func);
		}

		public static T DeepCopy<T>(this T @object)
		{
			return (T)@object.ToBinaryBytes().ToObject();
		}
    }
}
