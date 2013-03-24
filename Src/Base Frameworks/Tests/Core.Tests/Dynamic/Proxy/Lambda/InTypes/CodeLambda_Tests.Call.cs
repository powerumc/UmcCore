using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	[TestClass]
	public class CodeLambda_Call_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("동적, 정적(Static) 메서드의 호출 테스트, 오류가 발생하지 않고 메서드 실행이 완료하면 통과")]
		public void Call_StaticMethod_Test()
		{
			string typeName    = Guid.NewGuid().ToString("N");
			string methodName1 = Guid.NewGuid().ToString("N");
			string methodName2 = Guid.NewGuid().ToString("N");

			var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Class(typeName);
					{
						var method1 = type.Public.Static.Method(methodName1);
						{
							method1.IL.EmitWriteLine("This is Dynamic Method (Method1)");
							method1.Return();
						};

						var method2 = type.Public.Static.Method(methodName2);
						{
							method2.Emit.EmitWriteLine("Thisis Dynamic Method (Method2)");

							method2.Call((Operand)method1);
							method2.Return();
						};
					};


					var releaseType = type.ReleaseType();

					releaseType.GetMethod(methodName1).Invoke(null, null);
					releaseType.GetMethod(methodName2).Invoke(null, null);
				};
			};
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적, 인스턴스(Instance) 객체의 메서드 호출 테스트, 오류가 발생하지 않고 메서드 실행이 완료되면 통과")]
		[ExpectedException(typeof(NotImplementedException), "테스트 미완료")]
		public void Call_InstanceMethod_Test()
		{
			throw new NotImplementedException("객체를 생성하는 기능이 미완료됨");
		}
	}
}
