using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC
{
	/// <summary>
	///		<para><see cref="IFrameworkContainer"/> 를 구현하는 추상 클래스입니다. 이 클래스를 사용하여 컨테이너의 기능을 구현하십시오.</para>
	///		<para>이 컨테이너는 기존 <see cref="FrameworkContainer{TContainer}"/>에 Lifetime 을 매핑할 수 있는 기능이 확장되었습니다.</para>
	/// </summary>
	/// <typeparam name="TContainer">다른 IoC 컨테이너의 타입입니다.</typeparam>
	/// <typeparam name="TLifetime"><see cref="ILifetimeMapping{TLifetime}"/>을 구현한 LifetimeMapping 클래스 타입입니다.</typeparam>
	public abstract class FrameworkContainer<TContainer, TLifetime> : FrameworkContainer<TContainer>
		where TLifetime : new()
	{
		public TLifetime LifetimeMapping { get; set; }

		protected FrameworkContainer()
			: base()
		{
			createLifetimeMapping();
		}

		protected FrameworkContainer(TContainer container)
			: base(container)
		{
			createLifetimeMapping();
		}

		protected FrameworkContainer(object key)
			: base()
		{
			this.Key = key;
			createLifetimeMapping();
		}

		public FrameworkContainer(object key, IFrameworkContainer parentContainer)
			: base(key, parentContainer)
		{
			createLifetimeMapping();
		}

		private void createLifetimeMapping()
		{
			this.LifetimeMapping = (TLifetime)Activator.CreateInstance(typeof(TLifetime));
		}


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 의 구성 작업을 시작합니다. 
		/// </summary>
		/// <returns>	
		/// 	구성을 마친 후에 현재 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract override IFrameworkContainer Configure();

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="T">	등록되는 개체의 타입입니다. </typeparam>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract override IFrameworkContainer RegisterType<T>();

		public abstract override IFrameworkContainer RegisterType<TContract>(Type implementType, LifetimeFlag flag);

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다.</typeparam>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract override IFrameworkContainer RegisterType<TContract, TImplements>();

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다. </typeparam>
		/// <param name="key">	객체의 키 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract override IFrameworkContainer RegisterType<TContract, TImplements>(string key);

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="T">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract override IFrameworkContainer RegisterType<T>(LifetimeFlag flag);

		public abstract override IFrameworkContainer RegisterType(Type contractType, Type implementType, LifetimeFlag flag);
		public abstract override IFrameworkContainer RegisterType(string key, Type contractType, Type implementType, LifetimeFlag flag);

		public abstract override IFrameworkContainer RegisterInstance<TContract>(TContract @object);
		public abstract override IFrameworkContainer RegisterInstance<TContract>(TContract @object, LifetimeFlag flag);
		public abstract override IFrameworkContainer RegisterInstance<TContract>(string key, TContract @object, LifetimeFlag flag);

		public abstract override IFrameworkContainer RegisterInstance(string key, Type type, object @object);
		public abstract override IFrameworkContainer RegisterInstance(Type type, object @object);
		public abstract override IFrameworkContainer RegisterInstance(Type type, object @object, LifetimeFlag flag);
		public abstract override IFrameworkContainer RegisterInstance(string key, Type type, object @object, LifetimeFlag flag);

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다. </typeparam>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract override IFrameworkContainer RegisterType<TContract, TImplements>(LifetimeFlag flag);

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다. </typeparam>
		/// <param name="key">	객체의 키 값입니다. </param>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract override IFrameworkContainer RegisterType<TContract, TImplements>(string key, LifetimeFlag flag);

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 반환합니다. 
		/// </summary>
		/// <param name="type">	반환하는 객체의 타입입니다. </param>
		/// <returns>	
		/// 	반환된 객체입니다. 
		/// </returns>
		public abstract override object Resolve(Type type);
		public abstract override object Resolve(string key, Type type);

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public abstract override TContract Resolve<TContract>();

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <param name="key">	객체의 키 값입니다. </param>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public abstract override TContract Resolve<TContract>(string key);

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public abstract override IEnumerable<TContract> ResolveAll<TContract>();

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 가 참조하는 IoC 컨테이너 객체를 생성합니다.
		/// </summary>
		/// <param name="parentContainer">	부모 <see cref="IFrameworkContainer"/> 객체입니다. </param>
		/// <returns>	
		/// 	참조된 IoC 컨테이너로 <see cref="IFrameworkContainer"/> 를 반환합니다.
		/// </returns>
		protected abstract override TContainer CreateContainer(FrameworkContainer<TContainer> parentContainer);
	}
}
