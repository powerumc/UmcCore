using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		람다 구문이 상속해야 하는 추상 클래스 입니다.
	/// </summary>
	/// <typeparam name="T">람다식에 사용되는 대상 개체의 타입입니다.</typeparam>
	public abstract class LambdaObject<T>
	{

		/// <summary>	
		/// 	람다 구문에 대한 대상 객체를 가져옵니다.
		/// </summary>
		protected T Object { get; private set; }

		protected LambdaObject(T Object)
		{
			this.Object = Object;
		}
	}
}
