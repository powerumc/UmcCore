using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.IoC
{
	/// <summary>
	///		<see cref="IFrameworkContainer"/> 를 구현하는 추상 클래스입니다. 이 클래스를 사용하여 컨테이너의 기능을 구현하십시오.
	/// </summary>
	/// <typeparam name="TContainer"><see cref="IFrameworkContainer"/> 가 참조하는 실제 IoC 컨테이너의 타입입니다.</typeparam>
	public abstract class FrameworkContainer<TContainer> : IFrameworkContainer
	{
		protected readonly static object KEY_ROOT = "ROOT";
		
		internal TContainer ContainerObject { get; set; }
		internal Dictionary<object, List<IFrameworkContainer>> ChildContainers;


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected FrameworkContainer()
		{
			this.ContainerObject = this.CreateContainer(this);
		}

		protected FrameworkContainer(TContainer container)
		{
			this.ContainerObject = container;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FrameworkContainer(object key)
			: this()
		{
			this.Key = key;
		}


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FrameworkContainer(object key, IFrameworkContainer parentContainer)
		{
			this.Key = key;
			this.ContainerObject = this.CreateContainer((FrameworkContainer<TContainer>)parentContainer);

			this.AddChildContainer((FrameworkContainer<TContainer>)parentContainer);
		}


		/// <summary>	
		/// 	현재 <see cref="IFrameworkContainer"/> 에 자식 <see cref="IFrameworkContainer"/> 를 추가합니다.
		/// </summary>
		/// <param name="parentContainer">부모 <see cref="IFrameworkContainer"/> 객체입니다.</param>
		/// <returns>자식 <see cref="IFrameworkContainer"/> 를 추가한 후, 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		public IFrameworkContainer AddChildContainer(IFrameworkContainer parentContainer)
		{
			FrameworkContainer<TContainer> container = (FrameworkContainer<TContainer>)parentContainer;

			if (container.ChildContainers == null)
			{
				container.ChildContainers = new Dictionary<object, List<IFrameworkContainer>>();
				container.ChildContainers.Add(this.Key, new List<IFrameworkContainer>() { this });
			}
			else if (container.ChildContainers.ContainsKey(parentContainer.Key) == false)
			{
				container.ChildContainers.Add(this.Key, new List<IFrameworkContainer>() { this });
			}
			else
			{
				container.ChildContainers[this.Key].Add(this);
			}

			this.Parent = container;

			return this;
		}


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 의 고유 키 값을 가져오거나 설정합니다. 
		/// </summary>
		public object Key { get; set; }

		/// <summary>	
		/// 	<para>현재의 <see cref="IFrameworkContainer"/> 의 부모 <see cref="IFrameworkContainer"/>를
		/// 	가져옵니다.</para>	<para>만약 최상위 부모 <see cref="IFrameworkContainer"/>의 Parent 는 NULL
		/// 	입니다.</para> 
		/// </summary>
		public IFrameworkContainer Parent { get; set; }

		/// <summary>	
		/// 	현재의 <see cref="IFrameworkContainer"/> 의 자식 <see cref="IFrameworkContainer"/> 를 가져옵니다. 
		/// </summary>
		public IEnumerable<IFrameworkContainer> Childs
		{
			get
			{
				if( this.ChildContainers == null || this.ChildContainers.Count() == 0 )
					return Enumerable.Empty<IFrameworkContainer>();

				return this.ChildContainers.Values.SelectMany( o => o.AsEnumerable());
			}
		}


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 의 구성 작업을 시작합니다. 
		/// </summary>
		/// <returns>	
		/// 	구성을 마친 후에 현재 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract IFrameworkContainer Configure();

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="T">	등록되는 개체의 타입입니다. </typeparam>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract IFrameworkContainer RegisterType<T>();

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="T">	등록되는 개체의 타입입니다. </typeparam>
		/// <param name="flag">	The flag. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract IFrameworkContainer RegisterType<T>(LifetimeFlag flag);

		
		public abstract IFrameworkContainer RegisterInstance<T> (T @object);
		public abstract IFrameworkContainer RegisterInstance<TContract>(TContract @object, LifetimeFlag flag);
		public abstract IFrameworkContainer RegisterInstance<TContract>(string key, TContract @object, LifetimeFlag flag);

		public abstract IFrameworkContainer RegisterInstance(string key, Type type, object @object);
		public abstract IFrameworkContainer RegisterInstance(Type type, object @object);
		public abstract IFrameworkContainer RegisterInstance(Type type, object @object, LifetimeFlag flag);
		public abstract IFrameworkContainer RegisterInstance(string key, Type type, object @object, LifetimeFlag flag);

		public abstract IFrameworkContainer RegisterType(Type contractType, Type implementType, LifetimeFlag flag);
		public abstract IFrameworkContainer RegisterType(string key, Type contractType, Type implementType, LifetimeFlag flag);
		public abstract IFrameworkContainer RegisterType<TContract>(Type implementType, LifetimeFlag flag);
		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다.</typeparam>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract IFrameworkContainer RegisterType<TContract, TImplements>() where TImplements : TContract;

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다. </typeparam>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract IFrameworkContainer RegisterType<TContract, TImplements>(LifetimeFlag flag) where TImplements : TContract;


		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록되는 개체의 계약 타입입니다. </typeparam>
		/// <typeparam name="TImplements">	등록되는 개체의 구현 타입입니다. </typeparam>
		/// <param name="key">	객체의 키 값입니다. </param>
		/// <returns>	
		/// 	개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다. 
		/// </returns>
		public abstract IFrameworkContainer RegisterType<TContract, TImplements>(string key) where TImplements : TContract;


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
		public abstract IFrameworkContainer RegisterType<TContract, TImplements>(string key, LifetimeFlag flag) where TImplements : TContract;

		public abstract bool IsRegisted(Type type);

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 반환합니다. 
		/// </summary>
		/// <param name="type">	반환하는 객체의 타입입니다. </param>
		/// <returns>	
		/// 	반환된 객체입니다. 
		/// </returns>
		public abstract object Resolve(Type type);
		public abstract object Resolve(string key, Type type);


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public abstract TContract Resolve<TContract>();


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <param name="key">	객체의 키 값입니다. </param>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public abstract TContract Resolve<TContract>(string key);


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다. 
		/// </summary>
		/// <typeparam name="TContract">	등록한 개체의 계약 타입입니다. </typeparam>
		/// <returns>	
		/// 	등록된 객체를 반환합니다. 
		/// </returns>
		public abstract IEnumerable<TContract> ResolveAll<TContract>(); 


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 가 참조하는 IoC 컨테이너 객체를 생성합니다.
		/// </summary>
		/// <param name="parentContainer">	부모 <see cref="IFrameworkContainer"/> 객체입니다. </param>
		/// <returns>	
		/// 	참조된 IoC 컨테이너로 <see cref="IFrameworkContainer"/> 를 반환합니다.
		/// </returns>
		protected abstract TContainer CreateContainer(FrameworkContainer<TContainer> parentContainer);


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 개별 구성이 필요한 경우 구성 이전에 사전 구성 작업을 시작합니다. 
		/// </summary>
		/// <param name="composer">	구성할 <see cref="IFrameworkComposable"/> 을 구현한 객체입니다. </param>
		public virtual void PreCompose(IFrameworkComposable composer)
		{
			if( composer != null )
				composer.Compose();
		}


		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 개별 구성이 필요한 경우 구성 작업을 시작합니다. 
		/// </summary>
		/// <param name="composer">	구성할 <see cref="IFrameworkComposable"/> 을 구현한 객체입니다. </param>
		public virtual void Compose(IFrameworkComposable composer)
		{
			if( composer != null )
				composer.Compose();
		}


		/// <summary>	
		/// 	런타임에서 <see cref="IFrameworkContainer"/> 개별 구성이 필요한 경우 재구성 작업을 시작합니다. 
		/// </summary>
		/// <param name="recomposer">	구성할 <see cref="IFrameworkComposable"/> 을 구현한 객체입니다. </param>
		public virtual void Recompose(IFrameworkComposable recomposer)
		{
			if( recomposer != null )
				recomposer.Compose();
		}
	}
}
