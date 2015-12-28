using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	/// <para>람다식의 If 구문을 트리의 클래스 입니다.</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LambdaIfObject<T> : LambdaObject<T>
	{
		internal Func<T, bool> FuncIf { get; set; }

		public LambdaIfObject(T obj, Func<T, bool> func)
			: base(obj)
		{
			this.FuncIf = func;
		}

		public virtual bool Value
		{
			get { return FuncIf(this.Object); }
		}


		/// <summary>	
		/// 	And If 구문을 처리합니다.
		/// </summary>
		/// <param name="func">And If 구문에 포함되는 조건을 위임하는 대리자입니다.</param>
		/// <returns>현재 객체가 속하는 <see cref="LambdaIfObject{T}"/> 객체를 반환합니다.</returns>
		public LambdaIfObject<T> AndIf(Func<T, bool> func)
		{
			return new LambdaAndIfObject<T>(this.Object, this, func);
		}


		/// <summary>	
		/// 	Or If 구문을 처리합니다.
		/// </summary>
		/// <param name="func">	Or If 구문에 포함되는 조건을 위임하는 대리자입니다. </param>
		/// <returns>현재 객체가 속하는 <see cref="LambdaIfObject{T}"/> 객체를 반환합니다. </returns>
		public LambdaIfObject<T> OrIf(Func<T, bool> func)
		{
			return new LambdaOrIfObject<T>(this.Object, this, func);
		}


		/// <summary>	
		/// 	Then 구문을 처리합니다.
		/// </summary>
		/// <param name="action">조건이 일치할 경우 실행될 대리자입니다.</param>
		/// <returns>조건이 일치하는 경우 처리하는 <see cref="LambdaThenObject{T}"/> 객체를 반환합니다.</returns>
		public LambdaThenObject<T> Then(Action<T> action)
		{
			return new LambdaThenObject<T>(this.Object, this, action);
		}
	}
}
