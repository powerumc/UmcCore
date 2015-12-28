using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		람다 구문의 If 구문에서 파생되는 AndIf 클래스 입니다.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LambdaAndIfObject<T> : LambdaIfObject<T>
	{
		private LambdaIfObject<T> leftLambda;

		public LambdaAndIfObject(T obj, LambdaIfObject<T> left, Func<T, bool> func)
			: base(obj, func)
		{
			this.leftLambda = left;
		}

		public override bool Value
		{
			get
			{
				return leftLambda.Value & FuncIf(this.Object);
			}
		}
	}
}
