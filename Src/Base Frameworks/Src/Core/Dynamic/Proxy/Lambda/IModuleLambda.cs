using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	///		모듈에 대한 표현식을 나타내는 람다 인터페이스 입니다.
	/// </summary>
	public interface IModuleLambda : IMetadataLambda<IModuleLambda>
	{

		/// <summary>	
		/// 	이 <see cref="IModuleLambda"/> 가 포함이 되는 어셈블리인 <see cref="IAssemblyLambda"/> 객체를 가져옵니다.
		/// </summary>
		IAssemblyLambda AssemblyLambda { get; }


		/// <summary>	
		/// 	이 <see cref="IModuleLambda"/> 에서 사용되는 <see cref="ModuleBuilder"/> 객체를 가져옵니다.
		/// </summary>
		ModuleBuilder ModuleBuilder { get; }


		/// <summary>	
		/// 	모듈을 선언합니다.
		/// </summary>
		/// <returns>
		///		<see cref="IModuleLambda"/> 에서 사용되는 <see cref="ITypeLambda"/> 객체를 반환합니다.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Module")]
		ITypeLambda Module();


		/// <summary>	
		/// 	모듈 이름으로 모듈을 선언합니다.
		/// </summary>
		/// <param name="moduleName">모듈 이름입니다.</param>
		/// <returns>
		///		<see cref="IModuleLambda"/> 에서 사용되는 <see cref="ITypeLambda"/> 객체를 반환합니다.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Module")]
		ITypeLambda Module(string moduleName);
	}
}
