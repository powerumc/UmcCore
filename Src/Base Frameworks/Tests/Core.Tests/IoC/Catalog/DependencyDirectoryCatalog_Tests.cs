using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.IoC.Catalog
{
	[TestClass]
	public class DependencyDirectoryCatalog_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("폴더의 모든 DLL에서 올바르게 DependencyContractAttribute타입을 가져오는지 검사, 테스트 객체 내의 여러 Mock 객체가 1개 이상이므로, 개수가 1개 이상이면 통과")]
		public void DependencyDirectoryCatalog_Basic_Test()
		{
			DependencyDirectoryCatalog catalog = new DependencyDirectoryCatalog(TestContext.TestDeploymentDir);
			var types = catalog.GetMatchingTypes();

			types.ToList().ForEach( o => Debug.WriteLine(o.AssemblyQualifiedName));
		}
	}
}
