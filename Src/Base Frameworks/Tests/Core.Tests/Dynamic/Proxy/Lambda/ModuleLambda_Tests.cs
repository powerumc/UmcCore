using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	[TestClass]
	public class ModuleLambda_Tests
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 어셈블리와 모듈이 올바르게 생성이 되는지 테스트, 오류가 발생하지 않으면 통과")]
		public void ModuleLambda_Constructor_Test()
		{
			var assembly = new AssemblyLambda();
			var module = assembly.Assembly();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 대리자 타입이 올바르게 생성되고 실행되는지 테스트, 오류가 발생하지 않으면 통과")]
		public void ModuleLambda_Delegate_Test()
		{
			var typeName     = Guid.NewGuid().ToString("N");
            var delegateName = Guid.NewGuid().ToString("N");
            var methodName	= Guid.NewGuid().ToString("N");

            var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var @delegate = module.Delegate(typeof(void), delegateName, typeof(string));

					var type = module.Class(typeName);
					{
						var method = type.Public.Static.Method(methodName);
						{
							method.Return();
						};
					};
				};

				module.ReleaseType();
			};
		}
	}
}
