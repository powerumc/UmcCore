using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Reflection;
using System.Reflection;
using System.ComponentModel;

namespace Umc.Core.IoC
{
	/// <summary>
	///		IoC 컨테이너의 종속성을 판단하는 조건 클래스 입니다.
	/// </summary>
	internal static class DependencyIf
	{

		/// <summary>	
		/// 	타입이 <typeparamref name="T"/> 의 같은 타입인지 검사합니다.
		/// </summary>
		/// <typeparam name="T">비교되는 대상 타입입니다. </typeparam>
		/// <param name="type">	비교되는 원본 타입입니다.</param>
		/// <returns>	
		/// 	true if it succeeds, false if it fails. 
		/// </returns>
		public static bool Is<T>(this Type type) where T : class
		{
			return type == typeof(T);
		}


		/// <summary>	
		/// 	객체가 <typeparamref name="TInterface"/> 인터페이스를 구현했는지 검사합니다.
		/// </summary>
		/// <typeparam name="TInterface">	비교할 인터페이스 타입입니다. </typeparam>
		/// <param name="implementationType">	비교되는 원본 타입입니다. </param>
		/// <returns>	
		/// 	true if interface of< t interface>, false if not. 
		/// </returns>
		public static bool IsInterfaceOf<TInterface>(this Type implementationType) where TInterface : class
		{
			return implementationType.GetInterfaces().Any( t => t == typeof(TInterface));
		}


		/// <summary>	
		/// 	타입이 <see cref="DependencyContractAttribute"/> 특성이 존재하는지 여부를 검사합니다.
		/// </summary>
		/// <param name="type">	비교되는 원본 타입입니다. </param>
		/// <returns><see cref="DependencyContractAttribute"/> 특성이 존재하는 True, 그렇지 않으면 False</returns>
		public static bool IsDependencyContract(this Type type)
		{
			var attribute = type.GetCustomAttributeEx<DependencyContractAttribute>();
			return attribute != null;
		}


		/// <summary>	
		/// 	타입이 <see cref="IDependencyAttribute"/> 인터페이스를 구현하였는지 여부를 검사합니다.
		/// </summary>
		/// <param name="type">	비교되는 원본 타입입니다. </param>
		/// <returns><see cref="IDependencyAttribute"/> 인터페이스가 구연되었으면 True, 그렇지 않으면 False</returns>
		public static bool IsDependencyAttribute(this Type type)
		{
			return type.GetCustomAttributes(false).Any( o => o is IDependencyAttribute);
		}


		/// <summary>	
		/// 	타입에서 선언된 <see cref="DependencyContractAttribute"/> 특성을 가져옵니다.
		/// </summary>
		/// <param name="type">	비교되는 원본 타입입니다. </param>
		/// <returns>선언된 특성을 반환합니다.</returns>
		public static IEnumerable<DependencyContractAttribute> GetDependencyContractsOfType(this Type type)
		{
			return type.GetCustomAttributesEx<DependencyContractAttribute>();
		}


		/// <summary>	
		/// 	생성자에 선언된 <see cref="DependencyInjectionAttribute"/> 특성을 반환합니다.
		/// </summary>
		/// <param name="constructor">리플랙션 수준의 생성자 정보입니다.</param>
		/// <returns>생성자에 선언된 <see cref="DependencyInjectionAttribute"/> 를 반환합니다.</returns>
		public static IEnumerable<DependencyInjectionAttribute> GetDependencyInjectionOnConstructor(this ConstructorInfo constructor)
		{
			var attributes = constructor.GetCustomAttributes(typeof(DependencyInjectionAttribute), false).Cast<DependencyInjectionAttribute>();
			return attributes;
		}


		/// <summary>	
		/// 	속성(Property)에 선언된 <see cref="DependencyInjectionAttribute"/> 특성을 반환합니다.
		/// </summary>
		/// <param name="property">	리플랙션 수준의 속성(Property) 정보입니다. </param>
		/// <returns>속성(Property)에 선언된 <see cref="DependencyInjectionAttribute"/> 를 반환합니다.</returns>
		public static DependencyInjectionAttribute GetDependencyInjectionOnProperty(this PropertyInfo property)
		{
			var attributes = property.GetCustomAttributes(typeof(DependencyInjectionAttribute), false).Cast<DependencyInjectionAttribute>();
			return attributes.FirstOrDefault();
		}


		/// <summary>	
		/// 	속성(Property)에 선언된 <see cref="DefaultValueAttribute"/> 특성을 반환합니다.
		/// </summary>
		/// <param name="property">	리플랙션 수준의 속성(Property) 정보입니다. </param>
		/// <returns>속성(Property)에 선언된 <see cref="DefaaultValueAttribute"/> 특성을 반환합니다.</returns>
		public static DefaultValueAttribute GetDefaultValueOnProperty(this PropertyInfo property)
		{
			var attributes = property.GetCustomAttributes(typeof(DefaultValueAttribute), false).Cast<DefaultValueAttribute>();
			return attributes.FirstOrDefault();
		}


		/// <summary>	
		/// 	메서드에 선언된 <see cref="DependencyInjectionAttribute"/> 특성을 반환합니다.
		/// </summary>
		/// <param name="method">	리플랙션 수준의 메서드 정보입니다. </param>
		/// <returns>메서드에 선언된 <see cref="DependencyInjectionAttribute"/> 를 반환합니다.</returns>
		public static DependencyInjectionAttribute GetDependencyInjectionOnMethod(this MethodInfo method)
		{
			var attributes = method.GetCustomAttributes(typeof(DependencyInjectionAttribute), false).Cast<DependencyInjectionAttribute>();
			return attributes.FirstOrDefault();
		}


		/// <summary>	
		/// 	매개 변수에 선언된 <see cref="DependencyInjectionAttribute"/> 특성을 반환합니다.
		/// </summary>
		/// <param name="parameter">	리플랙션 수준의 매개 변수 정보입니다. </param>
		/// <returns>매개 변수에에 선언된 <see cref="DependencyInjectionAttribute"/> 를 반환합니다.</returns>
		public static DependencyInjectionAttribute GetDependencyInjectionOnParameter(this ParameterInfo parameter)
		{
			var attributes = parameter.GetCustomAttributes(typeof(DependencyInjectionAttribute), false).Cast<DependencyInjectionAttribute>();
			return attributes.FirstOrDefault();
		}


		/// <summary>	
		/// 	매개 변수에 선언된 <see cref="DefaultValueAttribute"/> 특성을 반환합니다.
		/// </summary>
		/// <param name="param">	리플랙션 수준의 매개 변수 정보입니다. </param>
		/// <returns>매개 변수에에 선언된 <see cref="DefaultValueAttribute"/> 를 반환합니다</returns>
		public static DefaultValueAttribute GetDependencyDefaultValueOnParameter(this ParameterInfo param)
		{
			var attributes = param.GetCustomAttributes(typeof(DefaultValueAttribute), false).Cast<DefaultValueAttribute>();
			return attributes.FirstOrDefault();
		}

		public static bool IsDynamicAttribute(this Type type)
		{
			if ( type.IsInterface )
			{
				var attribute = type.GetCustomAttributes(false).Cast<Attribute>();
				var dynamicAttribute = attribute.FirstOrDefault();

				if ( dynamicAttribute is IDynamicAttribute )
					return true;
			}

			return false;
		}

		public static IDynamicAttribute GetDynamicAttribute(this Type type)
		{
			if ( type.IsInterface )
			{
				var attribute = type.GetCustomAttributes(false).Cast<Attribute>();
				var dynamicAttribute = attribute.FirstOrDefault();

				if ( dynamicAttribute is IDynamicAttribute )
					return (IDynamicAttribute)dynamicAttribute;
			}

			return null;
		}
	}
}
