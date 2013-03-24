using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	/// <para>
	///		어셈블리의 표현식을 나타내는 인터페이스 입니다.
	/// </para>
	/// </summary>
	public interface IAssemblyLambda :	IMetadataLambda<IAssemblyLambda>,
										ITypeLambda,
										ISaveable,
										IName
	{
		/// <summary>
		///		<para>동적 어셈블리 개체를 생성하기 위한 <see cref="AssemblyBuilder"/> 개체를 가져옵니다.</para>
		/// </summary>
		/// <value>
		///		어셈블리를 동적으로 생성하는 <see cref="AssemblyBuilder"/> 클래스를 반환합니다.
		///	</value>
		AssemblyBuilder AssemblyBuilder { get; }

		/// <summary>
		///		<para>동적 어셈블리와 모듈에게 발행된 고유 토큰 값을 가져옵니다.</para>
		/// </summary>
		/// <value>
		///		이 <see cref="IAssemblyLambda"/>	 객체의 토큰 정보를 담는 클래스를 반환합니다.
		///	</value>
		DynamicProxyToken Token { get; }

		/// <summary>	
		/// 	<para>어셈블리와 모듈의 Qualified 이름을 가져옵니다.</para> 
		/// </summary>
		/// <value>	
		/// 	이 <see cref="IAssemblyLambda"/> 객체의 Qualified Name 을 반환합니다.
		/// </value>
		string AssemblyLambdaQualifiedName { get; }

		#region Assembly
		/// <summary>
		///		<para>동적 어셈블리를 생성한 후에 <see cref="IModuleLambda"/> 객체를 반환합니다.</para>
		/// </summary>
		/// <returns><see cref="IModuleLambda"/> 객체를 반환합니다.</returns>
		IModuleLambda Assembly();
		/// <summary>	
		/// 	<para>동적 어셈블리를 해당 Assembly Qualified Name 으로 생성한 후에 <see cref="IModuleLambda"/> 객체를 반환합니다.</para> 
		/// </summary>
		/// <param name="assemblyName">어셈블리의 이름입니다.</param>
		/// <returns>	
		/// 	<see cref="IModuleLambda"/> 객체를 반환합니다.
		/// </returns>
		IModuleLambda Assembly(string assemblyName);
		/// <summary>
		///		<para>동적 어셈블리를 해당 Assembly Qualified Name 으로 생성한 후에 <see cref="IModuleLambda"/> 객체를 반환합니다. </para>
		///		<para><see cref="DynamicProxyToken"/> 의 토큰 값으로 어셈블리의 유일성을 보장할 수 있습니다.</para>
		/// </summary>
		/// <param name="assemblyName">어셈블리 이름입니다.</param>
		/// <param name="token">프락시 객체의 토큰 정보를 전달하는 클래스입니다.</param>
		/// <returns>
		///		<see cref="IModuleLambda"/> 객체를 반환합니다.
		/// </returns>
		IModuleLambda Assembly(string assemblyName, DynamicProxyToken token);
		#endregion

		#region Set Type
		/// <summary>
		///		<para>생성된 동적 타입의 부모 타입을 설정합니다.</para>
		/// </summary>
		/// <param name="name">현재 타입에 상속할 타입의 이름입니다.</param>
		/// <returns>
		///		<see cref="ITypeLambda"/> 를 구현하는 객체를 반환합니다.
		/// </returns>
		ITypeLambda SetParent(string name);
		/// <summary>
		///		<para>생성된 동적 타입에 인터페이스를 설정합니다.</para>
		/// </summary>
		/// <param name="name">인터페이스의 이름입니다.</param>
		/// <returns>
		///		<see cref="ITypeLambda"/> 를 구현하는 객체를 반환합니다.
		///	</returns>
		ITypeLambda AddInterface(string name); 
		#endregion
	}
}