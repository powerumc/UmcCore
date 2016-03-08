using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
	/// <summary>
	/// 	구조체나 객체가 특정값 범위에 있는지 검사하는 클래스 입니다.
	/// </summary>
	/// <typeparam name="T"> Generic type parameter. </typeparam>
	public class Between<T> where T : IComparable<T>, IEquatable<T>
	{
		/// <summary>
		/// 	시작 값입니다.
		/// </summary>
		public T Begin { get; private set; }

		/// <summary>
		/// 	현재 값입니다.
		/// </summary>
		public T Now { get; private set; }

		/// <summary>
		/// 	마지막 값입니다.
		/// </summary>
		public T End { get; private set; }

		/// <summary>
		/// 	구조체나 객체가 특정값 범위에 있는지 검사하는 클래스의 생성자 입니다.
		/// </summary>
		/// <param name="begin"> 시작 값입니다. </param>
		/// <param name="now">   현재 값입니다. </param>
		/// <param name="end">   마지막 값입니다. </param>
		public Between(T begin, T now, T end)
		{
			this.Begin = begin;
			this.Now = now;
			this.End = end;
		}

		/// <summary>
		///		값이 사이에 있는지 여부를 검사합니다.
		/// </summary>
		public bool IsBetween
		{
			get { return this.Now.CompareTo(this.Begin) != -1 && this.Now.CompareTo(this.End) != 1; }
		}

		/// <summary>
		/// 	값이 마지막 값의 범위를 벗어났는지 여부를 검사합니다.
		/// </summary>
		public bool IsOver
		{
			get { return this.Now.CompareTo(this.End) == 1; }
		}

		/// <summary>
		/// 	값이 사이에 있거나 마지막 값의 범위를 벗어났는지 여부를 검사합니다.
		/// </summary>
		public bool IsBetweenOrOver
		{
			get { return IsBetween || IsOver; }
		}
}
}
