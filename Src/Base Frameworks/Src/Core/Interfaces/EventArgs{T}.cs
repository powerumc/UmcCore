using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
    /// <summary>
    ///     <see cref="EventArgs"/> 클래스의 재너릭 클래스 입니다.
    /// </summary>
    /// <typeparam name="T">매개변수의 타입입니다.</typeparam>
	public sealed class EventArgs<T> : EventArgs where T : class
	{
		private readonly T args;


		public EventArgs(T args)
		{
			this.args = args;
		}

        /// <summary>
        ///     이벤트 매개변수 타입의 값입니다.
        /// </summary>
		public T Value { get { return this.args; } }
	}
}
