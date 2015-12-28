using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		람다식의 If 구문이 실패한 경우 ElseIf 를 구현한 클래스 입니다.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LambdaElseIfObject<T> : LambdaInvokeObject<T>
	{
		internal LambdaIfObject<T> LambdaIf { get; set; }
		private Action<T> action;

		public LambdaElseIfObject(T obj, LambdaIfObject<T> lambdaThen, Action<T> action)
			: base(obj)
		{
			this.LambdaIf = lambdaThen;
			this.action = action;
		}


		/// <summary>	
		/// 	Else If 람다 구문을 실행합니다.
		/// </summary>
		public override void Invoke()
		{
			if (LambdaIf.Value == true)
			{
				action(this.Object);
			}
		}
	}
}
