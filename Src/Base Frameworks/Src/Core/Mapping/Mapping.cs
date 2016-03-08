using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		객체와 객체간의 매핑 관계를 생성하는 기본 클래스 입니다.
	/// </summary>
	/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
	/// <typeparam name="TReturn">결과 값의 타입입니다.</typeparam>
	public class Mapping<TInput, TReturn> : IMapping<TInput, TReturn>
	{
		private Type typeofReturnObject;
		private IDictionary<TInput, IMappingReturn<TInput, TReturn>> mapper = new Dictionary<TInput, IMappingReturn<TInput, TReturn>>();

		protected IMappingReturn<TInput, TReturn> DefaultReturnObject { get; private set; }
		protected bool IsMappedDefault { get { return this.DefaultReturnObject != null; } }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected IDictionary<TInput, IMappingReturn<TInput, TReturn>> Mapper { get { return this.mapper; } }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Mapping()
			: this(typeof(MappingReturn<TInput, TReturn>))
		{
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Mapping(Type typeofReturnObject)
		{
			this.typeofReturnObject = typeofReturnObject;

			this.InitializeMapping();
		}

		/// <summary>
		///		<see cref="InitializeMapping"/> 메서드를 자식 클래스에서 재정의하여 사용하십시오.
		/// </summary>
		protected virtual void InitializeMapping() { }


		/// <summary>	
		/// 	매핑된 결과에 대한 <see cref="IValuable{TReturn}"/> 값을 반환합니다.
		/// </summary>
		/// <param name="@return">매핑된 결과 객체입니다.</param>
		/// <returns>매핑된 결과에 대한 값을 반환합니다.</returns>
		protected virtual TReturn ReturnMappedValue(IMappingReturn<TInput, TReturn> @return)
		{
			return @return.Value;
		}

		/// <summary>
		///		입력 값을 통하여 관계의 매핑을 정의합니다.
		/// </summary>
		/// <param name="input">매핑에 사용되는 조건입니다.</param>
		/// <returns>건에 만족할 경우 반환되는 객체를 정의할 수 있는 <see cref="IMappingReturn{TInput, TReturn}"/> 객체를 반환합니다.</returns>
		public virtual IMappingReturn<TInput, TReturn> Map(TInput input)
		{
			var @return = (IMappingReturn<TInput, TReturn>)Activator.CreateInstance(this.typeofReturnObject, this);
			this.mapper.Add(input, @return);

			return @return;
		}

		/// <summary>
		///		입력 값을 통하여 이미 정의된 결과간의 관계의 매핑을 정의합니다.
		/// </summary>
		/// <param name="input">매핑에 사용되는 조건입니다.</param>
		/// <param name="return">이미 정의된 결과에 대한 <see cref="IMappingReturn{TInput, TReturn}"/> 객체입니다.</param>
		/// <returns>조건에 만족할 경우 반환되는 객체를 정의할 수 있는 <see cref="IMappingReturn{TInput, TReturn}"/> 객체를 반환합니다.</returns>
		public IMappingReturn<TInput, TReturn> Map(TInput input, IMappingReturn<TInput, TReturn> @return)
		{
			this.mapper.Add(input, @return);

			return @return;
		}

		/// <summary>
		///		매핑되지 않은 객체의 기본 매핑을 정의합니다.
		/// </summary>
		/// <returns>정의되지 않은 매핑에 대해 반환되는 객체를 정의할 수 있는 <see cref="IMappingReturn{TInput, TReturn}"/> 객체를 반환합니다.</returns>
		public IMappingReturn<TInput, TReturn> MapDefault()
		{
			this.DefaultReturnObject = (IMappingReturn<TInput, TReturn>)Activator.CreateInstance(this.typeofReturnObject, this);

			return this.DefaultReturnObject;
		}

		/// <summary>
		///		매핑된 객체를 입력 값을 키값으로 매핑된 결과 값을 가져옵니다.
		/// </summary>
		/// <param name="input">매핑에 사용된 조건값입니다.</param>
		/// <returns>매핑된 결과 값을 반환합니다.</returns>
		public virtual TReturn GetMappingValue(TInput input)
		{
			if( input == null )
				throw new ArgumentNullException(String.Format(ExceptionRS.O_값이_1_입니다, input, "NULL"));

			var result = mapper.FirstOrDefault(o => o.Key.Equals(input)).Value;

			if (result == null)
			{
				if (this.IsMappedDefault == true)
				{
					return this.ReturnMappedValue(this.DefaultReturnObject);
				}
				else
				{
					throw new KeyNotFoundException(String.Format(ExceptionRS.O_값이_1_입니다, input, "NULL"));
				}
			}

			return this.ReturnMappedValue(result);
		}

		/// <summary>
		///		<see cref="IMapping{TInput, TReturn}"/> 의 매핑된 개체에 해당 매핑 키가 존재하는지 여부를 가져옵니다.
		/// </summary>
		/// <param name="input">매핑에 사용된 조건입니다.</param>
		/// <returns>매핑된 조건이 만족하는 경우 True, 그렇지 않으면 False 를 반환합니다.</returns>
		public bool ContainsKey(TInput input)
		{
			return this.mapper.ContainsKey(input);
		}
	}

	/// <summary>
	///		객체와 객체간의 매핑 관계를 생성하는 기본 클래스 입니다.
	/// </summary>
	/// <typeparam name="TReturn">결과 값의 타입입니다.</typeparam>
	public class Mapping<TReturn> : Mapping<Object, TReturn>
	{
		public Mapping()
			: base()
		{
		}

		public Mapping(Type typeofReturnObject)
			: base(typeofReturnObject)
		{
		}
	}

	/// <summary>
	///		객체와 객체간의 매핑 관계를 생성하는 기본 클래스 입니다.
	/// </summary>
	public class Mapping : Mapping<Object, Object>
	{
		public Mapping()
			: base()
		{
		}

		public Mapping(Type typeofReturnObject)
			: base(typeofReturnObject)
		{
		}
	}
}