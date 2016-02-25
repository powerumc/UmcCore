using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Registration;

namespace Umc.Core.IoC.Castle
{
	/// <summary>
	///		Castle Windsor의 IoC 컨테이너를 지원하는 클래스 입니다.
	/// </summary>
	public class FrameworkContainerForCastle : FrameworkContainer<IWindsorContainer, LifetimeMappingForCastle>
	{
		public FrameworkContainerForCastle()
			: this(KEY_ROOT)
		{
		}

		public FrameworkContainerForCastle(object key)
			: base(key)
		{
		}

		public FrameworkContainerForCastle(object key, IFrameworkContainer parentContainer)
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
			ContainerObject.Register(Component.For<T>().ImplementedBy<T>());
			return this;
		}

		public override IFrameworkContainer RegisterType(string key, Type contractType, Type implementType, LifetimeFlag flag)
		{
			var lifetime = LifetimeMapping.GetLifetimeObject(flag);
			ContainerObject.Register(Component.For(contractType).ImplementedBy(implementType).LifeStyle.Custom(lifetime).Named(key));
			return this;
		}

		public override IFrameworkContainer RegisterType(Type contractType, Type implementType, LifetimeFlag flag)
		{
			var lifetime = LifetimeMapping.GetLifetimeObject(flag);
			ContainerObject.Register(Component.For(contractType).ImplementedBy(implementType).LifeStyle.Custom(lifetime));
			return this;
		}

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="T">	등록되는 개체의 타입입니다. </typeparam>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public override IFrameworkContainer RegisterType<T>(LifetimeFlag flag)
		{
			var lifetime = LifetimeMapping.GetLifetimeObject(flag);
			ContainerObject.Register(Component.For<T>().ImplementedBy<T>().LifeStyle.Custom(lifetime));
			return this;
		}

		public override IFrameworkContainer RegisterType<TContract>(Type implementType, LifetimeFlag flag)
		{
			var lifetime = LifetimeMapping.GetLifetimeObject(flag);
			ContainerObject.Register(Component.For<TContract>().ImplementedBy(implementType).LifeStyle.Custom(lifetime));
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
			ContainerObject.Register(Component.For<TContract>().ImplementedBy<TImplements>());
			return this;
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
			var lifetime = LifetimeMapping.GetLifetimeObject(flag);
			ContainerObject.Register(Component.For<TContract>().ImplementedBy<TImplements>().LifeStyle.Custom(lifetime));
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
			ContainerObject.Register(Component.For<TContract>().ImplementedBy<TImplements>());
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
			var lifetime = LifetimeMapping.GetLifetimeObject(flag);
			ContainerObject.Register(Component.For<TContract>().ImplementedBy<TImplements>().LifeStyle.Custom(lifetime).Named(key));
			return this;
		}

		public override IFrameworkContainer RegisterInstance<TContract>(TContract @object)
		{
			ContainerObject.Register(Component.For<TContract>().ImplementedBy<TContract>());
			return this;
		}

		public override IFrameworkContainer RegisterInstance<TContract>(TContract @object, LifetimeFlag flag)
		{
			var lifetime = LifetimeMapping.GetLifetimeObject(flag);
			ContainerObject.Register(Component.For<TContract>().Instance(@object).LifeStyle.Custom(lifetime));
			return this;
		}

		public override IFrameworkContainer RegisterInstance<TContract>(string key, TContract @object, LifetimeFlag flag)
		{
			var lifetime = LifetimeMapping.GetLifetimeObject(flag);
			ContainerObject.Register(Component.For<TContract>().Instance(@object).LifeStyle.Custom(lifetime).Named(key));
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
		protected override IWindsorContainer CreateContainer(FrameworkContainer<IWindsorContainer> parentContainer)
		{
			return new WindsorContainer();
		}

		public override IFrameworkContainer RegisterInstance(string key, Type type, object @object)
		{
			throw new NotImplementedException();
		}

		public override IFrameworkContainer RegisterInstance(Type type, object @object)
		{
			throw new NotImplementedException();
		}

		public override IFrameworkContainer RegisterInstance(Type type, object @object, LifetimeFlag flag)
		{
			throw new NotImplementedException();
		}

		public override IFrameworkContainer RegisterInstance(string key, Type type, object @object, LifetimeFlag flag)
		{
			throw new NotImplementedException();
		}

		public override object Resolve(string key, Type type)
		{
			throw new NotImplementedException();
		}

        public override bool IsRegisted(Type type)
        {
            try
            {
                return ContainerObject.Resolve(type) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
