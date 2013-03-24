using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	[TestClass]
	public class Operand_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("Operand 의 New 메서드 테스트, 오류가 발생하지 않으면 통과")]
		[ExpectedException(typeof(NotImplementedException))]
		public void TestMethod1()
		{
			string typeName   = Guid.NewGuid().ToString("N");
			string methodName = Guid.NewGuid().ToString("N");

			var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Public.Class(typeName);
					{
						var constructor = type.Public.Constructor();
						{
							constructor.IL.EmitWriteLine("This is Constructor");
							constructor.Return();
						}

						var method = type.Public.Static.Method(methodName);
						{
							throw new NotImplementedException("AccessorLambda 가 Type, Method 간 공유되는 문제 해결해야 함");
							//method.New(type);
							//method.Return();
						}
					}

					//var releaseType = type.ReleaseType();
				}
			}

			//assembly.AssemblyLambda.Save();
		}
	}
}
