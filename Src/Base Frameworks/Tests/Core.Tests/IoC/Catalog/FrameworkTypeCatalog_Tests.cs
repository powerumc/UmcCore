using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.IoC.Catalog
{
	[TestClass]
	public class DependencyTypeCatalog_Tests : UnitTestBase
	{
		[DependencyContract(typeof(MockDependencyClass))]
		public class MockDependencyClass
		{
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("DependencyTypeCatalog의 DependencyContractAttribute를 올바르게 분석하는지 테스트, Visitor 가 Mock클래스 타입과 같으면 통과")]
		public void DependencyTypeCatalog_Basic_Test()
		{
			var inputTypes = new Type[]
			{
				typeof(MockDependencyClass)
			};

			var catalog = new FrameworkTypeCatalog(inputTypes);
			var types = catalog.GetMatchingTypes();

			foreach (var type in types)
			{
				TestContext.WriteLine(type.AssemblyQualifiedName);

				Assert.IsTrue( type == typeof(MockDependencyClass));
			}
		}
	}
}
