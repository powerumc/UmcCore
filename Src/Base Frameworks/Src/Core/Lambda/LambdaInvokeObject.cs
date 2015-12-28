using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		람다 트리에서 특정 명령을 실행하는 클래스입니다.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class LambdaInvokeObject<T> : LambdaObject<T>
	{
		protected LambdaInvokeObject(T obj)
			: base(obj)
		{
		}


		/// <summary>	
		/// 	람다 트리의 식을 처리합니다.
		/// </summary>
		public abstract void Invoke();
	}
}
