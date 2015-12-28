using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		일반 개체 인스턴스에서 <see cref="PropertyInfo"/> 의 리플랙션 정보로 매핑을 수행할 수 있는 프로바이더 클래스 입니다.
	/// </summary>
	public class MappingProviderForProperty : MappingProvider<string, object>
	{
		protected object @object;
		protected IDictionary<string, PropertyInfo> PropertyMappingDictionary = null;
		private Type conversionType;

		public MappingProviderForProperty()
		{
			this.PropertyMappingDictionary = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
		}

		public MappingProviderForProperty(IEqualityComparer<string> comparer)
		{
			this.PropertyMappingDictionary = new Dictionary<string, PropertyInfo>(comparer);

			this.@object = this.CreateNewInstance();
			this.Initialize();
		}

		public MappingProviderForProperty(object @object)
		{
			this.@object = @object;
			this.PropertyMappingDictionary = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);

			this.Initialize();
		}

		public MappingProviderForProperty(object @object, IEqualityComparer<string> comparer)
		{
			this.@object = @object;
			this.PropertyMappingDictionary = new Dictionary<string, PropertyInfo>(comparer);
			
			this.Initialize();
		}
		/// <summary>
		///		매핑 프로바이더의 매핑 데이터소스를 변경합니다.
		/// </summary>
		/// <param name="object">데이터소스로 변경할 객체입니다.</param>
		public override void SetObject(object @object)
		{
			bool isInitialized = this.@object != null && ( this.@object.GetType() == @object.GetType() );
			
			this.@object = @object;

			if ( isInitialized == false )
				this.Initialize();
		}

		/// <summary>
		///		매핑 프로바이더의 매핑 데이터소스를 반환합니다.
		/// </summary>
		/// <returns>매핑 프로바이더와 연결된 객체를 반환합니다.</returns>
		public override object GetObject()
		{
			return this.@object;
		}

		/// <summary>
		///		매핑에 사용할 수 있는 키를 반환합니다.
		/// </summary>
		public override IEnumerable<object> MappingKeys
		{
			get { return this.PropertyMappingDictionary.Keys.Cast<object>(); }
		}

		/// <summary>	
		/// 	초기화 작업을 수행합니다.
		/// </summary>
		public override void Initialize()
		{
			this.@object.GetType()
						.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
						.ToList()
						.ForEach(o => this.PropertyMappingDictionary.Add(o.Name, o));
		}

		/// <summary>
		///		개체가 특정 객체를 반환합니다.
		/// </summary>
		/// <param name="input">객체를 반환할 때 필요한 매개 변수입니다.</param>
		/// <returns><see cref="Object"/> 입니다.</returns>
		public override object Getter(object input)
		{
			if (this.@object == null) throw new NullReferenceException("@object");

			if (this.IsMatches(input.ToString()))
				return this.PropertyMappingDictionary[input.ToString()].GetValue(@object, null);

			throw new KeyNotFoundException(input.ToString());
		}

		/// <summary>	
		/// 	개체에 값을 설정합니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다.</param>
		/// <param name="arg">객체에 설정하는 값입니다.</param>
		public override void Setter(object input, object arg)
		{
			if (this.@object == null) throw new NullReferenceException("@object");

			if (this.IsMatches(input.ToString()) == false)
			{
				return;
				throw new KeyNotFoundException(input.ToString());
			}

			if ( arg is DBNull )
			{
				Debug.WriteLine(string.Format("MappingProviderForProperty : input {0}'s value is DBNull, Cannot assign.", input.ToString()));
				return;
			}

			var propertyType = this.PropertyMappingDictionary[input.ToString()].PropertyType;
			conversionType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

			if (arg != null) 
				arg = Convert.ChangeType(arg, conversionType);
			
			this.PropertyMappingDictionary[input.ToString()].SetValue(@object, arg, null);
		}

		/// <summary>	
		/// 	객체가 조건에 만족하는지 여부를 반환합니다.
		/// </summary>
		/// <param name="input">조건으로 사용하는 매개 변수 입니다.</param>
		/// <returns>조건에 만족하면 True, 그렇지 않으면 False 를 반환합니다.</returns>
		public override bool IsMatches(string input)
		{
			if (this.@object == null) throw new NullReferenceException("@object");

			return this.PropertyMappingDictionary.ContainsKey(input);
		}

		/// <summary>	
		/// 	새로운 객체를 생성합니다.
		/// </summary>
		/// <returns>생성된 새로운 개체를 반환합니다.</returns>
		public override object CreateNewInstance()
		{
			if (this.@object == null) throw new NullReferenceException("@object");

			return Activator.CreateInstance(this.@object.GetType());
		}

		public override bool CanCreateNewInstance
		{
			get { return true; }
		}

		public override bool MoveNext()
		{
			return false;
		}
	}
}