using System;
using System.Collections.Generic;

namespace Umc.Core.IoC
{
	/// <summary>
	///		<para>IoC 컨테이너의 종속성을 제거하는 <see cref="IFrameworkContainer"/> 입니다. 기본적으로 이 프레임워크에서는 
	///			  Microsoft.Practice.Unity Application Block 을 지원합니다.</para>
	///		<para>만약 MEF(Managed Extensibility Framework) 또는 Castle 과 같은 컨테이너를 지원하기 위해서는 
	///			  <see cref="FrameworkContainer{TContainer}"/> 클래스를 상속하여 구현합니다.</para>
	/// </summary>
	public interface IFrameworkContainer
	{
		/// <summary>
		///		<see cref="IFrameworkContainer"/> 의 고유 키 값을 가져오거나 설정합니다.
		/// </summary>
		object Key { get; set; }
		
		/// <summary>
		///		<para>현재의 <see cref="IFrameworkContainer"/> 의 부모 <see cref="IFrameworkContainer"/>를 가져옵니다.</para>
		///		<para>만약 최상위 부모 <see cref="IFrameworkContainer"/>의 Parent 는 NULL 입니다.</para>
		/// </summary>
		IFrameworkContainer Parent { get; }

		/// <summary>
		///		현재의 <see cref="IFrameworkContainer"/> 의 자식 <see cref="IFrameworkContainer"/> 를 가져옵니다.
		/// </summary>
		IEnumerable<IFrameworkContainer> Childs { get; }

		/// <summary>
		///		<see cref="IFrameworkContainer"/> 의 구성 작업을 시작합니다.
		/// </summary>
		/// <returns>구성을 마친 후에 현재 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		IFrameworkContainer Configure();

		/// <summary>
		///	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다.
		/// </summary>
		/// <typeparam name="T">등록되는 개체의 타입입니다.</typeparam>
		/// <returns>개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		IFrameworkContainer RegisterType<T>() where T : class;

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다.
		/// </summary>
		/// <typeparam name="T">등록되는 개체의 타입입니다.</typeparam>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		IFrameworkContainer RegisterType<T>(LifetimeFlag flag) where T : class;

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다.
		/// </summary>
		/// <typeparam name="TContract">등록되는 개체의 타입입니다.</typeparam>
		/// <param name="object">등록되는 객체입니다.</param>
		/// <returns>개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		IFrameworkContainer RegisterInstance<TContract> (TContract @object) where TContract : class;
		IFrameworkContainer RegisterInstance<TContract>(TContract @object, LifetimeFlag flag) where TContract : class;
		IFrameworkContainer RegisterInstance<TContract>(string key, TContract @object, LifetimeFlag flag) where TContract : class;

		IFrameworkContainer RegisterInstance(string key, Type type, object @object);
		IFrameworkContainer RegisterInstance(Type type, object @object);
		IFrameworkContainer RegisterInstance(Type type, object @object, LifetimeFlag flag);
		IFrameworkContainer RegisterInstance(string key, Type type, object @object, LifetimeFlag flag);

		IFrameworkContainer RegisterType(Type contractType, Type implementType, LifetimeFlag flag);
		IFrameworkContainer RegisterType(string key, Type contractType, Type implementType, LifetimeFlag flag);

		IFrameworkContainer RegisterType<TContract>(Type implementType, LifetimeFlag flag) where TContract : class;

		/// <summary>
		///		타입을 <see cref="IFrameworkContainer"/> 에 등록합니다.
		/// </summary>
		/// <typeparam name="TContract">등록되는 개체의 계약 타입입니다.</typeparam>
		/// <typeparam name="TImplements">등록되는 개체의 구현 타입입니다.</typeparam>
		/// <returns>개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		IFrameworkContainer RegisterType<TContract, TImplements>() where TContract : class
                                                                   where TImplements : TContract;

		/// <summary>	
		/// 	타입을 <see cref="IFrameworkContainer"/> 에 등록합니다.
		/// </summary>
		/// <typeparam name="TContract">등록되는 개체의 계약 타입입니다.</typeparam>
		/// <typeparam name="TImplements">등록되는 개체의 구현 타입입니다.</typeparam>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		IFrameworkContainer RegisterType<TContract, TImplements>(LifetimeFlag flag) where TContract : class 
                                                                                    where TImplements : TContract;

		/// <summary>
		///		타입을 <see cref="IFrameworkContainer"/> 에 등록합니다.
		/// </summary>
		/// <typeparam name="TContract">등록되는 개체의 계약 타입입니다.</typeparam>
		/// <typeparam name="TImplements">등록되는 개체의 구현 타입입니다.</typeparam>
		/// <param name="key">개체의 키 값입니다.</param>
		/// <returns>개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		IFrameworkContainer RegisterType<TContract, TImplements>(string key) where TContract : class
                                                                             where TImplements : TContract;

		/// <summary>	
		/// 	<para>타입을 <see cref="IFrameworkContainer"/> 에 등록합니다.</para> 
		/// </summary>
		/// <typeparam name="TContract">등록되는 개체의 계약 타입입니다.</typeparam>
		/// <typeparam name="TImplements">등록되는 개체의 구현 타입입니다.</typeparam>
		/// <param name="key">개체의 키 값입니다.</param>
		/// <param name="flag">	객체의 생명주기 값입니다. </param>
		/// <returns>개체 등록을 마친 후 현재의 <see cref="IFrameworkContainer"/> 를 반환합니다.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		IFrameworkContainer RegisterType<TContract, TImplements>(string key, LifetimeFlag flag) where TContract : class
                                                                                                where TImplements : TContract;

		bool IsRegisted(Type type);

		/// <summary>
		///		<see cref="IFrameworkContainer"/> 에 등록된 개체를 반환합니다.
		/// </summary>
		/// <param name="type">반환하는 객체의 타입입니다.</param>
		/// <returns>반환된 객체입니다.</returns>
		object Resolve(Type type);

		object Resolve(string key, Type type);
		
		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다.
		/// </summary>
		/// <typeparam name="TContract">등록한 개체의 계약 타입입니다.</typeparam>
		/// <returns>등록된 객체를 반환합니다.</returns>
		TContract Resolve<TContract>();

		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다.
		/// </summary>
		/// <typeparam name="TContract">등록한 개체의 계약 타입입니다.</typeparam>
		/// <param name="key">개체의 키 값입니다.</param>
		/// <returns>등록된 객체를 반환합니다.</returns>
		TContract Resolve<TContract>(string key);

		/// <summary>
		///		<see cref="IFrameworkContainer"/> 에 등록된 개체를 가져옵니다.
		/// </summary>
		/// <typeparam name="TContract">등록한 개체의 계약 타입입니다.</typeparam>
		/// <returns>등록된 객체를 반환합니다.</returns>
		IEnumerable<TContract> ResolveAll<TContract>();

		/// <summary>
		///		<see cref="IFrameworkContainer"/> 개별 구성이 필요한 경우 구성 이전에 사전 구성 작업을 시작합니다.
		/// </summary>
		/// <param name="composer">구성할 <see cref="IFrameworkComposable"/> 을 구현한 객체입니다.</param>
		void PreCompose(IFrameworkComposable composer);

		/// <summary>
		///		<see cref="IFrameworkContainer"/> 개별 구성이 필요한 경우 구성 작업을 시작합니다.
		/// </summary>
		/// <param name="composer">구성할 <see cref="IFrameworkComposable"/> 을 구현한 객체입니다.</param>
		void Compose(IFrameworkComposable composer);

		/// <summary>
		///		런타임에서 <see cref="IFrameworkContainer"/> 개별 구성이 필요한 경우 재구성 작업을 시작합니다.
		/// </summary>
		///<param name="recomposer">구성할 <see cref="IFrameworkComposable"/> 을 구현한 객체입니다.</param>
		void Recompose(IFrameworkComposable recomposer);
	}
}
