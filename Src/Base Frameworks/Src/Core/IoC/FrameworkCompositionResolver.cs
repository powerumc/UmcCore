using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.IoC.Configuration;
using Umc.Core.Dynamic;

namespace Umc.Core.IoC
{
	/// <summary>
	///		직렬화된 IoC Container 의 데이터를 <see cref="IFrameworkContainer"/> 로 역직렬화를 하는 클래스입니다.
	/// </summary>
	/// <typeparam name="TFrameworkContainer"><see cref="IFrameworkContainer"/> 를 구현하는 컨테이너 객체입니다.</typeparam>
	public abstract class FrameworkCompositionResolver<TFrameworkContainer> : IFrameworkComposable<TFrameworkContainer>
		where TFrameworkContainer : IFrameworkContainer
	{
		protected TFrameworkContainer container { get; private set; }

		protected FrameworkCompositionResolver(TFrameworkContainer container)
		{
			this.container = container;
			this.container.RegisterInstance<IFrameworkContainer>(container);
		}



		/// <summary>	
		/// 	자식 컨테이너에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">컨테이너 요소입니다.</param>
		protected abstract void ResolveChildContainer(ContainerElement element);
		
		/// <summary>	
		/// 	생성자에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 생성자에 대한 객체 입니다.</returns>
		protected abstract object ResolveConstructor(RegisterElement element);

		/// <summary>	
		/// 	매개 변수에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="reflectionName">매개 변수의 리플랙션 수준의 이름입니다.</param>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 매개 변수에 대한 객체 입니다.</returns>
		protected abstract object ResolveParamOfDependencyElement(string reflectionName, DependencyElement element);

		/// <summary>	
		/// 	값(Value) 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="reflectionName">	매개 변수의 리플랙션 수준의 이름입니다. </param>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 값(Value)에 대한 객체 입니다.</returns>
		protected abstract object ResolveParamOfValueElement(string reflectionName, ValueElement element);

		/// <summary>	
		/// 	메서드 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 메서드에 대한 객체 입니다.</returns>
		protected abstract object ResolveMethod(MethodElement element);

		/// <summary>	
		/// 	속성(Property) 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 속성(Property)에 대한 객체 입니다.</returns>
		protected abstract object ResolveProperty(PropertyElement element);

		/// <summary>	
		/// 	IoC 컨테이너가 제공하는 객체 처리를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <param name="lifetime">	The lifetime. </param>
		/// <param name="constructor">역직렬화된 생성자 요소입니다.</param>
		/// <param name="properties">역직렬화된 속성(Property) 요소입니다.</param>
		/// <param name="methods">역직렬화된 메서드 요소입니다.</param>
		protected abstract void ResolveRegisterProcessor(RegisterElement element, LifetimeFlag lifetime, IEnumerable<object> constructor, IEnumerable<object> properties, IEnumerable<object> methods);

		protected abstract void ResolveRegisterProcessor(string key, Type contractType, Type implementType, LifetimeFlag lifetime);
		

		/// <summary>	
		/// 	역직렬화 작업의 구성을 수행합니다.
		/// </summary>
		public abstract void Compose();


		/// <summary>	
		/// 	<see cref="UmcCoreIoCElement"/> 의 루트 컨테이너를 처리합니다.
		/// </summary>
		/// <param name="rootElement">	The root element. </param>
		protected void ResolveRoot(UmcCoreIoCElement rootElement)
		{
			if (rootElement == null) return;

			this.ResolveContainers(rootElement.containers);
		}


		/// <summary>	
		/// 	자식 컨테이너 요소를 처리합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		protected void ResolveContainers(List<ContainerElement> element)
		{
			if (element == null || element.Count() == 0) return;

			this.ResolveContainer(element.FirstOrDefault());
		}


		/// <summary>	
		/// 	컨테이너의 요소를 처리합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		protected void ResolveContainer(ContainerElement element)
		{
			if (element == null) return;

			if (element.container != null && element.container.Count() > 0)
			{
				this.ResolveChildContainer(element);
			}

			this.ResolveRegisters(element.register);
			this.ResolveDynamic(element.dynamic);
		}

		protected void ResolveDynamic(List<DynamicElement> elements)
		{
//			if ( elements == null || elements.Count() == 0 ) return;
//
//			foreach ( var e in elements )
//			{
//				LifetimeFlag lifetime;
//				if ( e.lifetime != null ) lifetime = (LifetimeFlag)Enum.Parse(typeof(LifetimeFlag), e.lifetime.ToString());
//				else lifetime = LifetimeFlag.Default;
//
//				var contractType  = Type.GetType(e.type);
//				var implementType = DynamicObject.InterfaceImplementationType(Type.GetType(e.type));
//				container.RegisterType(contractType, implementType, lifetime);
//
//				this.ResolveRegisterProcessor(null, contractType, implementType, lifetime);
//			}
		}


		/// <summary>	
		/// 	컨테이너 요소에 대해 IoC 컨테이너에 등록 작업을 처리합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		protected void ResolveRegisters(List<RegisterElement> element)
		{
			if (element == null || element.Count() == 0) return;

			foreach (var e in element)
			{
				IEnumerable<object> constructor = null;
				IEnumerable<object> properties = null;
				IEnumerable<object> methods = null;

				if (e.constructor != null) constructor = this.ResolveConstructor(e).ToEnumerable();
				if (e.property != null) properties = this.ResolveProperties(e.property);
				if (e.method != null) methods = this.ResolveMethods(e.method);

				LifetimeFlag lifetime;

				if (e.lifetime != null) lifetime = (LifetimeFlag)Enum.Parse(typeof(LifetimeFlag), e.lifetime.type.ToString());
				else lifetime = LifetimeFlag.Default;

				this.ResolveRegisterProcessor(e, lifetime, constructor, properties, methods);
			}
		}


		/// <summary>	
		/// 	매개 변수에 대한 객체를 반환합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 매개 변수에 대한 객체입니다.</returns>
		protected IEnumerable<object> ResolveParams(List<ParamElement> element)
		{
			foreach (var param in element)
			{
				yield return this.ResolveParam(param.name, param.Item);
			}
		}


		/// <summary>	
		/// 	매개 변수에 대한 객체를 반환합니다.
		/// </summary>
		/// <param name="reflectionName">	매개 변수의 리플랙션 수준의 이름입니다. </param>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 매개 변수에 대한 객체입니다. </returns>
		protected object ResolveParam(string reflectionName, object element)
		{
			if (element != null)
			{
				if (element is ValueElement) 
					return this.ResolveParamOfValueElement(reflectionName, (ValueElement)element);
				else if (element is DependencyElement) 
					return this.ResolveParamOfDependencyElement(reflectionName, (DependencyElement)element);
			}

			throw new NotSupportedException(element.GetType().ToString());
		}


		/// <summary>	
		/// 	속성(Property)에 대한 객체를 반환합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>	IoC 컨테이너가 제공하는 속성(Property)에 대한 객체입니다. </returns>
		protected IEnumerable<object> ResolveProperties(List<PropertyElement> element)
		{
			foreach (var p in element)
			{
				yield return this.ResolveProperty(p);
			}
		}


		/// <summary>	
		/// 	메서드에 대한 객체를 반환합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 메서드에 대한 객체입니다.</returns>
		protected IEnumerable<object> ResolveMethods(List<MethodElement> element)
		{
			foreach (var m in element)
			{
				yield return this.ResolveMethod(m);
			}
		}
	}
}
