using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		람다식의 If 구문을 조건으로 참인 경우 실행을 하는 Then 클래스 입니다.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LambdaThenObject<T> : LambdaInvokeObject<T>
	{
		internal LambdaIfObject<T> LambdaIf { get; set; }
		private Action<T> action;

		internal List<LambdaElseIfObject<T>> elseIfAction = new List<LambdaElseIfObject<T>>();

		public LambdaThenObject(T obj, LambdaIfObject<T> lambdaIf, Action<T> action)
			: base(obj)
		{
			this.LambdaIf = lambdaIf;
			this.action = action;
		}


		/// <summary>	
		/// 	Then 람다 구문을 실행합니다.
		/// </summary>
		public override void Invoke()
		{
			if (LambdaIf.Value == true)
			{
				action(this.Object);
				return;
			}

			foreach (var elseIf in elseIfAction)
			{
				if (elseIf.LambdaIf.Value == true)
				{
					elseIf.Invoke();
					return;
				}
			}
		}


		/// <summary>	
		/// 	If 구문이 만족하지 않는 경우 Else 람다 구문입니다.
		/// </summary>
		/// <param name="action">조건이 만족하지 않는 경우에 실행할 대리자 입니다.</param>
		/// <returns>조건이 만족하지 않는 경우의 람다 구문인 <see cref="LambdaElseObject{T}"/> 객체를 반환합니다.</returns>
		public LambdaElseObject<T> Else(Action<T> action)
		{
			return new LambdaElseObject<T>(this.Object, this.LambdaIf, this, action);
		}


		/// <summary>	
		/// 	If 구문이 만족하지 않는 경우 Else If 람다 구문입니다.
		/// </summary>
		/// <param name="func">Else If 에 포함되는 조건의 대리자입니다.</param>
		/// <param name="action">Else If 조건이 만족할 경우 실행되는 대리자 입니다.</param>
		/// <returns>Else If 람다 구문에서 반환하는 <see cref="LambdaThenObject{T}"/> 객체를 반환합니다.</returns>
		public LambdaThenObject<T> ElseIf(Func<T, bool> func, Action<T> action)
		{
			var f = new LambdaIfObject<T>(this.Object, func);

			this.elseIfAction.Add(new LambdaElseIfObject<T>(this.Object, f, action));

			return this;
		}
	}
}
