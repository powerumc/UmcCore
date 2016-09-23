using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	[TestClass]
	public class CodeLambda__Local_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 메서드의 로컬 변수를 선언하는 테스트, 오류가 발생하지 않으면 통과")]
		public void Define_Local_Test()
		{
			var typeName    = Guid.NewGuid().ToString("N");
            var methodName1 = Guid.NewGuid().ToString("N");
            var localName1  = Guid.NewGuid().ToString("N");

            var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Class(typeName);
					{
						var method1 = type.Public.Static.Method(methodName1);
						{
							var local1 = method1.Local(typeof(string), localName1);
                            method1.Return();
						}
					}

					var releaseType = type.ReleaseType();
				}
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 로컬 변수를 선언하여 Operand 값을 출력하는 테스트, 오류가 발생하지 않고 값이 유효하면 통과")]
		public void Define_Local_And_Verify_Test()
		{
			throw new NotImplementedException("로컬 변수 값을 Peek 기능 미완료");
		}
	}
}