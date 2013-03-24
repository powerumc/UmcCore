using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	<para>CLR 개체의 이름을 Full Qualified Name 형태로 표현합니다.</para>
	///		<para>일반적으로 Qualified Name은 FullType, 어셈블리 이름, 어셈블리 토큰 정보로 표현됩니다.</para>
	/// </summary>
	internal static class AssemblyLambdaQualifiedNamePolicy
	{

		/// <summary>	
		/// 	<see cref="IAssemblyLambda"/> 의 토큰 정보를 이용하여 어셈블리 파일이름을 생성하여 반환합니다.
		/// </summary>
		/// <param name="assemblyLambda"><see cref="IAssemblyLambda"/> 를 구현하는 객체입니다.</param>
		/// <returns>	
		/// 	어셈블리 파일 이름을 반환합니다.
		/// </returns>
		internal static string CreateNewFileName(IAssemblyLambda assemblyLambda)
		{
			return String.Concat(assemblyLambda.Name, assemblyLambda.Token.Token.ToString("N"), ".DLL");
		}
	}
}
