using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		객체와 객체간의 매핑 관계를 키/값 형태로 정의하는 클래스 입니다.
	/// </summary>
	/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
	/// <typeparam name="TReturn">결과 값의 타입입니다.</typeparam>
	public class KeyValueMapping<TInput, TReturn> : Mapping<TInput, TReturn>
	{
		protected KeyValueMapping()
			: base()
		{
		}

		protected KeyValueMapping(Type typeofReturnObject)
			: base(typeofReturnObject)
		{
		}


		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected override void InitializeMapping() { }
	}

	/// <summary>
	///		객체와 객체간의 매핑 관계를 키/값 형태로 정의하는 클래스 입니다.
	/// </summary>
	/// <typeparam name="TReturn">결과 값의 타입입니다.</typeparam>
	public class KeyValueMapping<TReturn> : KeyValueMapping<object, TReturn>
	{
		protected KeyValueMapping()
			: base()
		{
		}

		protected KeyValueMapping(Type typeofReturnObject)
			: base(typeofReturnObject)
		{
		}

		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected override void InitializeMapping() { }
	}

	/// <summary>
	///		객체와 객체간의 매핑 관계를 키/값 형태로 정의하는 클래스 입니다.
	/// </summary>
	public class KeyValueMapping : KeyValueMapping<Object>
	{
		protected KeyValueMapping()
			: base()
		{
		}

		protected KeyValueMapping(Type typeofReturnObject)
			: base(typeofReturnObject)
		{
		}

		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected override void InitializeMapping() { }
	}


}
