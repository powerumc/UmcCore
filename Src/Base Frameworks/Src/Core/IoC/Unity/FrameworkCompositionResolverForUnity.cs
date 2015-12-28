using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Umc.Core.IoC.Configuration;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Umc.Core.Logger;

namespace Umc.Core.IoC.Unity
{

	/// <summary>
	///		Unity Application Block의 IoC 컨테이너의 직렬화된 IoC Container 의 데이터를 <see cref="IFrameworkContainer"/> 로 역직렬화를 하는 클래스입니다.
	/// </summary>
	public class FrameworkCompositionResolverForUnity : FrameworkCompositionResolver<FrameworkContainerForUnity>
	{
		private static IFrameworkLogger logger = FrameworkLogger.GetLogger(typeof (FrameworkCompositionResolverForUnity));
		private UmcCoreIoCElement rootElement;

		public FrameworkCompositionResolverForUnity(FrameworkContainerForUnity container, UmcCoreIoCElement rootElement)
			: base(container)
		{
			this.rootElement = rootElement;
		}





		/// <summary>	
		/// 	구성을 시작합니다.
		/// </summary>
		public override void Compose()
		{
			this.ResolveRoot(rootElement);
		}


		/// <summary>	
		/// 	자식 컨테이너에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">컨테이너 요소입니다.</param>
		protected override void ResolveChildContainer(ContainerElement element)
		{
			var childContainer = container.AddChildContainer(container);
			new FrameworkCompositionResolverForUnity((FrameworkContainerForUnityChild)childContainer, this.rootElement)
				.ResolveContainer(element);
		}

		/// <summary>	
		/// 	생성자에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 생성자에 대한 객체 입니다.</returns>
		protected override object ResolveConstructor(RegisterElement element)
		{
			return new InjectionConstructor(this.ResolveParams(element.constructor).ToArray());
		}

		/// <summary>	
		/// 	매개 변수에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="reflectionName">매개 변수의 리플랙션 수준의 이름입니다.</param>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 매개 변수에 대한 객체 입니다.</returns>
		protected override object ResolveParamOfDependencyElement(string reflectionName, DependencyElement element)
		{
			Debug.WriteLine(element.typeOfContract);
			return new ResolvedParameter(Type.GetType(element.typeOfContract), element.key);
		}

		/// <summary>	
		/// 	값(Value) 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="reflectionName">	매개 변수의 리플랙션 수준의 이름입니다. </param>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 값(Value)에 대한 객체 입니다.</returns>
		protected override object ResolveParamOfValueElement(string reflectionName, ValueElement element)
		{
			var type = Type.GetType(element.type);
			if (type == null) 
				return null;

			//var obj = TypeDescriptor.GetConverter(type).ConvertTo(element.value, type);
			var obj = TypeDescriptor.GetConverter(type).ConvertFrom(element.value);

			return new InjectionParameter(obj);
		}

		/// <summary>	
		/// 	메서드 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 메서드에 대한 객체 입니다.</returns>
		protected override object ResolveMethod(MethodElement element)
		{
			var param = this.ResolveParams(element.param);
			return new InjectionMethod(element.name, param.ToArray());
		}

		/// <summary>	
		/// 	속성(Property) 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 속성(Property)에 대한 객체 입니다.</returns>
		protected override object ResolveProperty(PropertyElement element)
		{
			var propertyValue = this.ResolveParam(element.name, element.Item);
			if (propertyValue == null)
				return null;

			var injectionProperty = new InjectionProperty(element.name, propertyValue);
			return injectionProperty;
		}

		/// <summary>	
		/// 	IoC 컨테이너가 제공하는 객체 처리를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <param name="lifetime">	The lifetime. </param>
		/// <param name="constructor">역직렬화된 생성자 요소입니다.</param>
		/// <param name="properties">역직렬화된 속성(Property) 요소입니다.</param>
		/// <param name="methods">역직렬화된 메서드 요소입니다.</param>
		protected override void ResolveRegisterProcessor(RegisterElement element, LifetimeFlag lifetime, IEnumerable<object> constructor, IEnumerable<object> properties, IEnumerable<object> methods)
		{
			IEnumerable<InjectionMember> concat = Enumerable.Empty<InjectionMember>();
			if (constructor != null) concat = concat.Concat(constructor.Cast<InjectionMember>());
			if (properties != null) concat = concat.Concat(properties.Cast<InjectionMember>());
			if (methods != null) concat = concat.Concat(methods.Cast<InjectionMember>());

			var contractType   = Type.GetType(element.contract);
			var dependencyType = Type.GetType(element.dependencyTo);

			try
			{
				container.ContainerObject.RegisterType(
						contractType,
						dependencyType,
						element.key,
						container.LifetimeMapping.GetLifetimeObject(lifetime),
						concat.ToArray());
			}
			catch (Exception e)
			{
				logger.Error(e);
			}
		}

		protected override void ResolveRegisterProcessor(string key, Type contractType, Type implementType, LifetimeFlag lifetime)
		{
			container.ContainerObject.RegisterType(contractType, implementType, key, container.LifetimeMapping.GetLifetimeObject(lifetime), new InjectionMember[] { });
		}
	}
}
