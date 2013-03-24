using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using System.Reflection;

namespace Umc.Core.IoC.Catalog
{
	[TestClass]
	public class DependencyAssemblyCatalog_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("현재 테스트 어셈블리의 DependencyContractAttribute 타입을 올바르게 가져오는지 테스트, Mock객체를 올바르게 1개 이상 가져오면 성공")]
		public void DependencyAssemblyCatalog_Basic_Test()
		{
			DependencyAssemblyCatalog assembly = new DependencyAssemblyCatalog(Assembly.GetExecutingAssembly());
			var types = assembly.GetMatchingTypes();

			foreach (var type in types)
			{
				TestContext.WriteLine(type.AssemblyQualifiedName);
			}

			Assert.IsTrue(types.Count() > 0, "현재 테스트 어셈블리에 Mock개체가 1개 이상이지만 Mock DependencyContrac타입의 개수가 0개여서 실패");
		}
	}
}
