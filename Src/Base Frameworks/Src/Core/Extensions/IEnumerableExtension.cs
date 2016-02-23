using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		<see cref="IEnumerable{T}"/> 타입의 확장 메서드 입니다.
	/// </summary>
	public static class IEnumerableExtension
	{
		/// <summary>	
		/// 	객체를 <see cref="IEnumerable{T}"/> 객체로 변환합니다.
		/// </summary>
		/// <typeparam name="TReturn">	반환되는 개체의 타입입니다. </typeparam>
		/// <param name="object">	<see cref="IEnumerable{T}"/> 로 반환되는 개체의 요소입니다. </param>
		/// <returns>	
		///		<see cref="IEnumerable{T}"/> 객체로 반환합니다.
		/// </returns>
		public static IEnumerable<TReturn> ToEnumerable<TReturn>(this TReturn @object)
		{
			var list = new List<TReturn>();

            if ( @object == null )
				return list.AsEnumerable();

			list.Add(@object);

			return list.AsEnumerable();
		}
	}
}


