using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using System.Xml.Serialization;
using Umc.Core.IoC.Catalog;
using System.Reflection;

namespace Umc.Core.IoC.Configuration
{
	[TestClass]
	public class FrameworkDependencyVisitor_Tests : UnitTestBase
	{
		IEnumerable<Type> mockTypes;

		public override void UnitTestStartup()
		{
			this.mockTypes = new Type[]
			{
				typeof(MockDependSimple),
				typeof(MockDependPropertyInjection),
				typeof(MockConstructorInjection),
				typeof(MockMethodInjection)
			}.AsEnumerable();
		}

		private void WriteToSerialization(object root)
		{
			XmlSerializer xs = new XmlSerializer(typeof(UmcCoreIoCElement));
			xs.Serialize(Console.Out, root);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("종속성 관계를 분석하여 XML 형태로 출력하는 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkDependencyVisitor_Visit_Method_Executing_Test()
		{
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(this.mockTypes);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);
			
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("DependencyTypeCatalog를 이용하여 종속성 관계를 분석하여 XML 형태로 출력 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkDependencyVisitor_By_DependencyTypeCatalog()
		{
			DependencyTypeCatalog catalog = new DependencyTypeCatalog(this.mockTypes);
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(catalog);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("DependencyAssemblyCatalog를 이용하여 종속성 관계를 분석하여 XML 형태로 출력 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkDependencyVisitor_By_DependencyAssemblyCatalog()
		{
			DependencyAssemblyCatalog catalog = new DependencyAssemblyCatalog(Assembly.GetExecutingAssembly());
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(catalog);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("DependencyDirectoryCatalog를 이용하여 종속성 관계를 분석하여 XML 형태로 출력 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkDependencyVisitor_By_DependencyDirectoryCatalog()
		{
			DependencyDirectoryCatalog catalog = new DependencyDirectoryCatalog(TestContext.TestDeploymentDir);
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(catalog);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);
		}
	}
}
