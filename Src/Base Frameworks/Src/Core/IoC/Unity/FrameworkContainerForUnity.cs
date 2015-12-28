using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Umc.Core.IoC.Configuration;
using System.ComponentModel;

namespace Umc.Core.IoC
{
	/// <summary>
	///		Unity Application Block 의 IoC 컨테이너를 지원하는 클래스 입니다.
	/// </summary>
	[DependencyContract(typeof(IFrameworkContainer))]
	public class FrameworkContainerForUnity : FrameworkContainer<IUnityContainer, LifetimeMappingForUnity>
	{
		public FrameworkContainerForUnity()
			: this(KEY_ROOT)
		{
		}

		public FrameworkContainerForUnity(object key)
			: base(key)
		{
		}

		public FrameworkContainerForUnity(object key, IFrameworkContainer parentContainer)
			: base(key, parentContainer)
		{
		}

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 의 구성 작업을 시작합니다. 
		/// </summary>
		/// <returns>	
		/// 	구성을 마친 후에 현재 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public override IFrameworkContainer Configure()
		{
			return this;
		}

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="T">	등록되는 개체의 타입입니다. </typeparam>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public override IFrameworkContainer RegisterType<T>()
		{
			ContainerObject.RegisterType<T>();
			return this;
		}

		public override IFrameworkContainer RegisterType(string key, Type contractType, Type implementType, LifetimeFlag flag)
		{
			ContainerObject.RegisterType(contractType, implementType, key, LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}

		public override IFrameworkContainer RegisterType(Type contractType, Type implementType, LifetimeFlag flag)
		{
			ContainerObject.RegisterType(contractType, implementType, LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}


		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 타입입니다. </typeparam>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public override IFrameworkContainer RegisterType<TContract>(LifetimeFlag flag)
		{
			ContainerObject.RegisterType<TContract>(LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}

		public override IFrameworkContainer RegisterType<TContract>(Type implementType, LifetimeFlag flag)
		{
			ContainerObject.RegisterType(typeof(TContract), implementType, LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}


		public override IFrameworkContainer RegisterInstance<TContract> (TContract @object)
		{
			ContainerObject.RegisterInstance<TContract>(@object);
			return this;
		}

		public override IFrameworkContainer RegisterInstance<TContract>(TContract @object, LifetimeFlag flag)
		{
			ContainerObject.RegisterInstance<TContract>(@object, LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}

		public override IFrameworkContainer RegisterInstance<TContract>(string key, TContract @object, LifetimeFlag flag)
		{
			ContainerObject.RegisterInstance<TContract>(key, @object, LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}

		public override IFrameworkContainer RegisterInstance(string key, Type type, object @object)
		{
			ContainerObject.RegisterInstance(type, key, @object);
			return this;
		}

		public override IFrameworkContainer RegisterInstance(Type type, object @object)
		{
			ContainerObject.RegisterInstance(type, @object);
			return this;
		}

		public override IFrameworkContainer RegisterInstance(Type type, object @object, LifetimeFlag flag)
		{
			ContainerObject.RegisterInstance(type, @object, LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}

		public override IFrameworkContainer RegisterInstance(string key, Type type, object @object, LifetimeFlag flag)
		{
			ContainerObject.RegisterInstance(type, key, @object, LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다.</typeparam>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public override IFrameworkContainer RegisterType<TContract, TImplements>()
		{
			ContainerObject.RegisterType<TContract, TImplements>();
			return this;
		}

		public override bool IsRegisted(Type type)
		{
			return ContainerObject.IsRegistered(type);
		}

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	타입입니다. </typeparam>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public override IFrameworkContainer RegisterType<TContract, TImplements>(LifetimeFlag flag)
		{
			ContainerObject.RegisterType<TContract, TImplements>(LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다. </typeparam>
		/// <param name="key">	객체의 키 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public override IFrameworkContainer RegisterType<TContract, TImplements>(string key)
		{
			ContainerObject.RegisterType<TContract, TImplements>(key);
			return this;
		}


		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다. </typeparam>
		/// <param name="key">	객체의 키 값입니다. </param>
		/// <param name="flag">	The flag. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public override IFrameworkContainer RegisterType<TContract, TImplements>(string key, LifetimeFlag flag)
		{
			ContainerObject.RegisterType<TContract, TImplements>(key, LifetimeMapping.GetLifetimeObject(flag));
			return this;
		}


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 반환합니다. 
		/// </summary>
		/// <param name="type">	반환하는 객체의 타입입니다. </param>
		/// <returns>	
		/// 	반환된 객체입니다. 
		/// </returns>
		public override object Resolve(Type type)
		{
			return ContainerObject.Resolve(type);
		}

		

		public override object Resolve(string key, Type type)
		{
			return ContainerObject.Resolve(type, key);
		}

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public override TContract Resolve<TContract>()
		{
			return ContainerObject.Resolve<TContract>();
		}

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <param name="key">	객체의 키 값입니다. </param>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public override TContract Resolve<TContract>(string key)
		{
			return ContainerObject.Resolve<TContract>(key);
		}

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public override IEnumerable<TContract> ResolveAll<TContract>()
		{
			return ContainerObject.ResolveAll<TContract>();
		}

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 가 참조하는 IoC 컨테이너 객체를 생성합니다.
		/// </summary>
		/// <param name="parentContainer">	부모 <see cref="IFrameworkContainer"/> 객체입니다. </param>
		/// <returns>	
		/// 	참조된 IoC 컨테이너로 <see cref="IFrameworkContainer"/> 를 반환합니다.
		/// </returns>
		protected override IUnityContainer CreateContainer(FrameworkContainer<IUnityContainer> parentContainer)
		{
			return new UnityContainer();
		}
	}
}
