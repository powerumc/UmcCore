using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Mapping;
using System.Reflection;
using System.Diagnostics;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		CLR 개체의 속성에 <see cref="ParamPropertyAttribute"/> 로 선언된 속성에 대해 매핑을 수행하는 프로바이더 입니다.
	/// </summary>
	public class ParamPropertyMappingProvider : MappingProviderForProperty
	{
		protected IDictionary<string, object> MappedNameDictionary = new Dictionary<string, object>();
		protected IDictionary<string, ParamPropertyAttribute> ParamPropertyAttributeDictionary = new Dictionary<string, ParamPropertyAttribute>();

		public ParamPropertyMappingProvider(object @object)
			: base(@object)
		{
		}


		/// <summary>	
		/// 	매핑에 사용할 수 있는 키를 반환합니다. 
		/// </summary>
		public override IEnumerable<object> MappingKeys
		{
			get
			{
				foreach (var attr in this.ParamPropertyAttributeDictionary)
				{
					yield return attr.Value.Name;
				}
			}
		}



		/// <summary>	
		/// 	초기화 작업을 수행합니다. 
		/// </summary>
		/// <exception cref="MappingException">	ParamAction.Send 또는 ParamAction.Received가 중복으로 선언이 되거나 IsRequired 속성인데 매핑할 수 없는 경우 발생하는 예외입니다. </exception>
		public override void Initialize()
		{
			base.Initialize();

			this.PropertyMappingDictionary
				.ToList()
				.ForEach(item =>
				{
					var property = this.PropertyMappingDictionary[item.Key.ToString()];

					var attributes = property.GetCustomAttributes(typeof(ParamPropertyAttribute), true).Cast<ParamPropertyAttribute>();

					if (attributes != null &&
						attributes.Any())
					{
						var sendAttributes = attributes.Where(o => (o.Action & ParamAction.Send) == ParamAction.Send);
						if (sendAttributes.Count() > 1)
							throw new MappingException(ExceptionRS.O_특성은_1_보다_많을수_없습니다, "ParamAction.Send", "1");

						var receiveAttributes = attributes.Where(o => (o.Action & ParamAction.Receive) == ParamAction.Receive);
						if (receiveAttributes.Count() > 1)
							throw new MappingException(ExceptionRS.O_특성은_1_보다_많을수_없습니다, "ParamAction.Received", "1");

						var attribute = attributes.First();
						
						this.MappedNameDictionary.Add(property.Name, item.Key);

						if (attribute.Name != null &&
							attribute.Name != property.Name)
							this.MappedNameDictionary.Add(attribute.Name, item.Key);
						
						attribute.Name = attribute.Name ?? property.Name;

						this.ParamPropertyAttributeDictionary.Add(attribute.Name, attribute);

						if( attribute.DefaultValue != null ) 
							property.SetValue(this.@object, attribute.DefaultValue, null);
					}
				});
		}

		/// <summary>	
		/// 	개체가 특정 객체를 반환할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>특정 객체를 반환할 수 있으면 True, 그렇지 않으면 False 입니다.</returns>
		public override bool CanGetter(object input)
		{
			if (this.ParamPropertyAttributeDictionary.ContainsKey(input.ToString()) == false)
			{
				return false;
			}

			var isContainsKey = this.MappedNameDictionary.ContainsKey(input.ToString());

			return isContainsKey && 
					(this.ParamPropertyAttributeDictionary[input.ToString()].Action & ParamAction.Send) == ParamAction.Send;
		}

		/// <summary>	
		/// 	개체가 특정 객체를 반환합니다.
		/// </summary>
		/// <param name="input">	객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>반환되는 <see cref="Object"/> 입니다.</returns>
		public override object Getter(object input)
		{
			var key = GetMappedName(input.ToString());

			if (key == null)
				throw new MappingException(ExceptionRS.키_O는_매핑_대상_키가_없습니다, input.ToString());

			if (this.PropertyMappingDictionary.ContainsKey(key.ToString()) == false)
				throw new MappingException(ExceptionRS.키_O는_매핑_대상_키가_없습니다, key.ToString());

			var value = base.Getter(key);

			return ConvertToPropertyType(key, value);
		}

		/// <summary>	
		/// 	개체가 특정 객체를 설정할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다.</param>
		/// <returns>개체가 특정 객체를 설정할 수 있으면 True, 그렇지 않으면 False</returns>
		public override bool CanSetter(object input)
		{
			if (this.ParamPropertyAttributeDictionary.ContainsKey(input.ToString()) == false)
			{
				return false;
			}

			return (this.ParamPropertyAttributeDictionary[input.ToString()].Action & ParamAction.Receive) == ParamAction.Receive;
		}

		/// <summary>	
		/// 	개체에 값을 설정합니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다.</param>
		/// <param name="arg">객체에 설정하는 값입니다.</param>
		public override void Setter(object input, object arg)
		{
			var key = GetMappedName(input.ToString());

			if (key == null)
				throw new MappingException(ExceptionRS.키_O는_매핑_대상_키가_없습니다, input.ToString());

			if (this.PropertyMappingDictionary.ContainsKey(key.ToString()) == false)
				throw new MappingException(ExceptionRS.키_O는_매핑_대상_키가_없습니다, key.ToString());

			arg = ConvertToPropertyType(key, arg);

			base.Setter(key, arg);
		}

		private object ConvertToPropertyType(object input, object arg)
		{
			if (arg == null)
				return arg;

			var propertyType = this.PropertyMappingDictionary[input.ToString()].PropertyType;
			if (propertyType != arg.GetType())
			{
				var typeConverter = System.ComponentModel.TypeDescriptor.GetConverter(propertyType);
				arg = typeConverter.ConvertFrom(arg);
			}
			return arg;
		}


		/// <summary>	
		/// 	리플랙션 수준의 속성(Property) 이름, 또는 <see cref="ParamPropertyAttribute"/> 의 Name 속성의 이름을 가져옵니다.
		/// </summary>
		/// <param name="input">	객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>리플랙션 수준의 속성(Property) 이름, 또는 <see cref="ParamPropertyAttribute"/> 의 Name 속성의 이름을 가져옵니다. </returns>
		private object GetMappedName(string input)
		{
			if (this.MappedNameDictionary.ContainsKey(input) == false &&
				this.PropertyMappingDictionary.ContainsKey(input))
				return null;

			if (this.MappedNameDictionary.ContainsKey(input) == false)
				return null;

			return this.MappedNameDictionary[input];
		}


		/// <summary>	
		/// 	리플랙션 수준의 속성(Property) 이름, 또는 <see cref="ParamPropertyAttribute"/> 의 Name 속성의 이름과 매핑된 값을 가져옵니다.
		/// </summary>
		/// <param name="input">	객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>매핑된 리플렉션 수준의 속성(Property) 정보 입니다.</returns>
		public PropertyInfo GetMappedValue(string input)
		{
			var mappedName = GetMappedName(input);
			return this.PropertyMappingDictionary[mappedName.ToString()];
		}

		public override object CreateNewInstance()
		{
			throw new NotSupportedException();
		}


		/// <summary>
		///		매핑이 수행되기 전에 초기화할 수 있는 작업입니다.
		/// </summary>
		/// <param name="sourceProvider"><see cref="IMappingProvider"/> 를 구현하는 원본 매핑 프로바이더 입니다.</param>
		/// <param name="targetProvider"><see cref="IMappingProvider"/> 를 구현하는 대상 매핑 프로바이더 입니다.</param>
		public override void StartOfAssign(IMappingProvider sourceProvider, IMappingProvider targetProvider)
		{
			base.StartOfAssign(sourceProvider, targetProvider);

			var mappingkeyOfSourceProvider = sourceProvider.MappingKeys;

			if (Object.ReferenceEquals(this, targetProvider))
			{
				var requiredParamProperies = this.ParamPropertyAttributeDictionary.Where(o => 
					{
						return this.ParamPropertyAttributeDictionary[o.Key.ToString()].IsRequired;
					})
					.ToDictionary(o => o.Key);

				foreach (var attr in requiredParamProperies)
				{
					if (mappingkeyOfSourceProvider.Contains(attr.Key) == false)
						throw new MappingException(ExceptionRS.키_O는_필수_매핑키_입니다_이_매핑키가_대상_데이터소스에_존재하지않아_매핑할_수_없습니다, attr.Key);
				} 
			}
		}
	}
}
