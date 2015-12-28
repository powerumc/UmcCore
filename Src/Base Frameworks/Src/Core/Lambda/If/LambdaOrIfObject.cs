using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		람다 구문의 If 구문에서 파생되는 OrIf 클래스 입니다.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LambdaOrIfObject<T> : LambdaIfObject<T>
	{
		private LambdaIfObject<T> leftLambda;

		public LambdaOrIfObject(T obj, LambdaIfObject<T> left, Func<T, bool> func)
			: base(obj, func)
		{
			this.leftLambda = left;
		}


		/// <summary>	
		/// 	람다 조건의 결과를 반환합니다.
		/// </summary>
		public override bool Value
		{
			get { return leftLambda.Value | FuncIf(this.Object); }
		}
	}
}
