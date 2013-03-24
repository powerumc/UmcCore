using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		객체와 객체간의 매핑 관계를 대리자를 통해 지연하여 정의하는 클래스 입니다.
	/// </summary>
	/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
	/// <typeparam name="TReturn">결과 값의 타입입니다.</typeparam>
	public abstract class LazyMapping<TInput, TReturn> : Mapping<Func<TInput, bool>, Func<TInput, TReturn>>
	{

		protected LazyMapping()
			: base()
		{
		} 

		protected LazyMapping(Type typeofReturnObject)
			: base(typeofReturnObject)
		{
		}


		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected abstract override void InitializeMapping();

		/// <summary>	
		/// 	매핑된 결과에 대한 <see cref="IValuable{TReturn}"/> 값을 반환합니다.
		/// </summary>
		/// <param name="return">매핑된 결과 객체입니다.</param>
		/// <returns>매핑된 결과에 대한 값을 반환합니다.</returns>
		protected override Func<TInput, TReturn> ReturnMappedValue(IMappingReturn<Func<TInput, bool>, Func<TInput, TReturn>> @return)
		{
			return base.ReturnMappedValue(@return);
		}

		/// <summary>
		///		매핑된 객체를 입력 값을 키값으로 매핑된 결과 값을 가져옵니다.
		/// </summary>
		/// <param name="input">매핑에 사용된 조건값입니다.</param>
		/// <returns>매핑된 결과 값을 반환합니다.</returns>
		public virtual TReturn GetMappingValue(TInput input)
		{
			var result = Mapper.FirstOrDefault(o => o.Key(input)).Value;

			if (result == null)
			{
				if (this.IsMappedDefault == true)
				{
					return this.ReturnMappedValue(base.DefaultReturnObject)(input);
				}
				else
				{
					throw new KeyNotFoundException(String.Format(ExceptionRS.O_값이_1_입니다, input, "NULL"));
				}
			}

			return this.ReturnMappedValue(result)(input);
		}
	}

	/// <summary>
	///		객체와 객체간의 매핑 관계를 대리자 형태로 정의하는 클래스 입니다.
	/// </summary>
	/// <typeparam name="TReturn">결과 값의 타입입니다.</typeparam>
	public abstract class LazyMapping<TReturn> : LazyMapping<object, TReturn>
	{
		protected LazyMapping()
			: base()
		{
		}

		protected LazyMapping(Type typeofReturnObject)
			: base(typeofReturnObject)
		{
		}


		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected abstract override void InitializeMapping();
	}
}