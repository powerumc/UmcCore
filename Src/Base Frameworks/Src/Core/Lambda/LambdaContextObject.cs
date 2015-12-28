using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		람다식의 트리의 상태를 관리하는 클래스 입니다.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LambdaContextObject<T> : LambdaObject<T>
	{
		public LambdaContextObject(T obj)
			: base(obj)
		{
		}


		/// <summary>	
		/// 	람다식의 조건문을 처리합니다.
		/// </summary>
		/// <param name="func">조건문을 위임하는 대리자입니다.</param>
		/// <returns>조건문을 표현하는 <see cref="LambdaIfObject{T}"/> 객체를 반환합니다.</returns>
		public LambdaIfObject<T> If(Func<T, bool> func)
		{
			return new LambdaIfObject<T>(this.Object, func);
		}
	}
}
