using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	///		모듈에 대한 표현식을 나타내는 람다 클래스 입니다..
	/// </summary>
	internal class ModuleLambda : IModuleLambda
	{

		/// <summary>	
		/// 	이 <see cref="IModuleLambda"/> 가 포함이 되는 어셈블리인 <see cref="IAssemblyLambda"/> 객체를 가져옵니다. 
		/// </summary>
		public IAssemblyLambda AssemblyLambda { get; private set; }


		/// <summary>	
		/// 	이 <see cref="IModuleLambda"/> 에서 사용되는 <see cref="ModuleBuilder"/> 객체를 가져옵니다. 
		/// </summary>
		public ModuleBuilder ModuleBuilder { get; private set; }

		public ModuleLambda(IAssemblyLambda assemblyLambda)
		{
			this.AssemblyLambda = assemblyLambda;
		}


		/// <summary>	
		/// 	모듈을 선언합니다. 
		/// </summary>
		/// <returns>	
		/// 	<see cref="IModuleLambda"/> 에서 사용되는 <see cref="ITypeLambda"/> 객체를 반환합니다. 
		/// </returns>
		public ITypeLambda Module()
		{
			return this.Module(Guid.NewGuid().ToString("N"));
		}


		/// <summary>	
		/// 	모듈을 선언합니다. 
		/// </summary>
		/// <param name="moduleName">모듈 이름입니다.</param>
		/// <returns>	
		/// 	<see cref="IModuleLambda"/> 에서 사용되는 <see cref="ITypeLambda"/> 객체를 반환합니다. 
		/// </returns>
		public ITypeLambda Module(string moduleName)
		{
			this.ModuleBuilder = this.AssemblyLambda.AssemblyBuilder.DefineDynamicModule(moduleName, this.AssemblyLambda.AssemblyLambdaQualifiedName, true);

			return new TypeLambda(this);
		}


		public IModuleLambda Attribute()
		{
			throw new NotImplementedException();
		}

		public IModuleLambda Attribute(params object[] @object)
		{
			throw new NotImplementedException();
		}
	}
}
