using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.IoC.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel;
using System.Reflection;
using System.Diagnostics;

namespace Umc.Core.IoC.Castle
{
	/// <summary>
	///		Castle Windsor의 IoC 컨테이너의 직렬화된 IoC Container 의 데이터를 <see cref="IFrameworkContainer"/> 로 역직렬화를 하는 클래스입니다.
	/// </summary>
	public class FrameworkCompositionResolverForCastle : FrameworkCompositionResolver<FrameworkContainerForCastle>
	{
		private UmcCoreIoCElement rootElement;
		public FrameworkCompositionResolverForCastle(FrameworkContainerForCastle container, UmcCoreIoCElement rootElement)
			: base(container)
		{
			this.rootElement = rootElement;
		}

		/// <summary>	
		/// 	자식 컨테이너에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">컨테이너 요소입니다.</param>
		protected override void ResolveChildContainer(ContainerElement element)
		{
			var childContainer = container.AddChildContainer(container);
			new FrameworkCompositionResolverForCastle((FrameworkContainerForCastleChild)childContainer, this.rootElement)
				.ResolveContainer(element);
		}

		/// <summary>	
		/// 	생성자에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 생성자에 대한 객체 입니다.</returns>
		protected override object ResolveConstructor(RegisterElement element)
		{
			List<object> paramList = new List<object>();

			foreach (var p in element.constructor)
			{
				var obj = this.ResolveParam(p.name, p.Item);
				if (obj is DynamicParametersDelegate)
					paramList.Add(obj);
				else
					paramList.Add(Parameter.ForKey(p.name).Eq(obj.ToString()));
			}

			return paramList;
		}

		/// <summary>	
		/// 	매개 변수에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="reflectionName">매개 변수의 리플랙션 수준의 이름입니다.</param>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 매개 변수에 대한 객체 입니다.</returns>
		protected override object ResolveParamOfDependencyElement(string reflectionName, DependencyElement element)
		{
			DynamicParametersDelegate @delegate = (kernel, d) => 
				{
					if (element.key == null)
						d[reflectionName] = kernel.Resolve(Type.GetType(element.typeOfContract));
					else
						d[reflectionName] = kernel.Resolve(element.key, Type.GetType(element.typeOfContract));
				};
			return @delegate;
		}

		/// <summary>	
		/// 	값(Value) 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="reflectionName">	매개 변수의 리플랙션 수준의 이름입니다. </param>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 값(Value)에 대한 객체 입니다.</returns>
		protected override object ResolveParamOfValueElement(string reflectionName, ValueElement element)
		{
			var obj = System.ComponentModel.TypeDescriptor.GetConverter(Type.GetType(element.type)).ConvertTo(element.value, Type.GetType(element.type));
			return obj;
		}

		/// <summary>	
		/// 	메서드 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 메서드에 대한 객체 입니다.</returns>
		protected override object ResolveMethod(MethodElement element)
		{
			List<object> paramList = new List<object>();

			foreach (var p in element.param)
			{
				var obj = this.ResolveParam(p.name, p.Item);
				paramList.Add(obj);
			}

			return paramList;
		}

		/// <summary>	
		/// 	속성(Property) 요소에 대한 역직렬화를 수행합니다.
		/// </summary>
		/// <param name="element">	컨테이너 요소입니다. </param>
		/// <returns>IoC 컨테이너가 제공하는 속성(Property)에 대한 객체 입니다.</returns>
		protected override object ResolveProperty(PropertyElement element)
		{
			var obj = this.ResolveParam(element.name, element.Item);
			if (obj is DynamicParametersDelegate)
				return obj;
			
			return Property.ForKey(element.name).Eq(obj);
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
			var component = Component.For(Type.GetType(element.contract))
									.ImplementedBy(Type.GetType(element.dependencyTo))
									.Named(element.key);

			if (constructor != null)
			{
				var constructorParams = ((List<object>)constructor.First());
				constructorParams.Where( o => o is Parameter).Cast<Parameter>().ToList().ForEach( o => component.Parameters(o));
				constructorParams.Where( o => o is DynamicParametersDelegate).Cast<DynamicParametersDelegate>().ToList().ForEach( @delegate =>
					component.DynamicParameters( (kernel, d) => @delegate(kernel, d)));
			}

			if (properties != null)
			{
				properties.Where( o => o is Property).Cast<Property>().ToList().ForEach( o => component.DependsOn(o));
				properties.Where( o => o is DynamicParametersDelegate).Cast<DynamicParametersDelegate>().ToList().ForEach( @delegate =>
					component.DynamicParameters( (kernel, d) => @delegate(kernel, d)));
			}

			if (methods != null)
			{
				throw new NotSupportedException("FrameworkContainerForCastle 은 현재 Method Injection 을 지원하지 않습니다.");
			}

			container.ContainerObject.Register(component);
		}

		protected override void ResolveRegisterProcessor(string key, Type contractType, Type implementType, LifetimeFlag lifetime)
		{
			var component = Component.For(contractType)
									.ImplementedBy(implementType)
									.LifeStyle.Custom(container.LifetimeMapping.GetLifetimeObject(lifetime))
									.Named(key);

			container.ContainerObject.Register(component);
		}


		/// <summary>	
		/// 	구성을 시작합니다.
		/// </summary>
		public override void Compose()
		{
			this.ResolveRoot(rootElement);
		}
	}
}
