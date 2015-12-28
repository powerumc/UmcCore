using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		람다식의 If 식과 ElseIf 가 모두 실패하는 경우 실행하는 Else 클래스 입니다.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LambdaElseObject<T> : LambdaInvokeObject<T>
	{
		private LambdaIfObject<T> lambdaIf;
		private LambdaThenObject<T> lambdaThen;
		private Action<T> action;

		public LambdaElseObject(T obj, LambdaIfObject<T> lambdaIf, LambdaThenObject<T> thenLambda, Action<T> elseAction)
			: base(obj)
		{
			this.lambdaIf = lambdaIf;
			this.lambdaThen = thenLambda;
			this.action = elseAction;
		}


		/// <summary>	
		/// 	Else 람다 구문을 실행합니다.
		/// </summary>
		public override void Invoke()
		{
			if (lambdaIf.Value == true)
			{
				lambdaThen.Invoke();
				return;
			}

			foreach (var elseIf in lambdaThen.elseIfAction)
			{
				if (elseIf.LambdaIf.Value == true)
				{
					elseIf.Invoke();
					return;
				}
			}

			action(this.Object);
		}
	}
}