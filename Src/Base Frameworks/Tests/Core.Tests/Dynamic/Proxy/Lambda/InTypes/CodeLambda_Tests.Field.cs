using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Dynamic.Proxy.Lambda.InTypes
{
	[TestClass]
	public class CodeLambda_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 타입에 필드를 선언하여 사용하는 테스트, 오류가 발생하지 않으면 통과")]
		public void Define_Field_Test()
		{
			var typeName    = Guid.NewGuid().ToString("N");
            var methodName1 = Guid.NewGuid().ToString("N");
            var fieldName1  = Guid.NewGuid().ToString("N");

            var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Class(typeName);
					{
						var field1 = type.Public.Field(typeof(string), fieldName1);
					};

					var releaseType = type.ReleaseType();

					int fieldCount = releaseType.GetFields().Length;

					Assert.IsTrue(fieldCount > 0, "동적 타입에 필드를 1개 생성하였지만 필드의 개수가 {0} 입니다.", fieldCount);
				};
			};
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 필드 타입을 선언하여 필드간의 값을 Assign 하는 테스트, 오류가 발생하지 않고 값이 유효하면통과")]
		public void Define_Field_And_Assign_Test()
		{
			throw new NotImplementedException("동적 필드 타입을 선언하여 값을 Assign");
		}
	}
}
