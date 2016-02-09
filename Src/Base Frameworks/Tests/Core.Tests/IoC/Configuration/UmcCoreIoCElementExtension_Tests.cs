using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.IoC.Catalog;
using System.Xml.Serialization;
using Umc.Core.Testing.UnitTest;
using System.IO;

namespace Umc.Core.IoC.Configuration
{
	[TestClass]
	public class UmcCoreIoCElementExtension_Tests : UnitTestBase
	{
		[DependencyContract(typeof(MockClass_Must_Success1))]
		internal class MockClass_Must_Success1 { }

		[DependencyContract(typeof(MockClass_Must_Success2))]
		internal class MockClass_Must_Success2 { }

		private void WriteToSerialization(object root)
		{
			XmlSerializer xs = new XmlSerializer(typeof(UmcCoreIoCElement));
			xs.Serialize(Console.Out, root);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("중복된 Contract과 Key 없는 타입으로 테스트, Verify() 결과가 True 이면 통과")]
		public void FrameworkDependencyVisitor_By_Extension()
		{
			Type[] mockTypes = new Type[]
			{
				typeof(MockClass_Must_Success1),
				typeof(MockClass_Must_Success2)
			};

			FrameworkTypeCatalog catalog = new FrameworkTypeCatalog(mockTypes);
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(catalog);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);

			var result = root.Verify();

			Assert.IsTrue(result.Result);
		}


		[DependencyContract(typeof(MockClass_Must_Success_With_Key1))]
		internal class MockClass_Must_Success_With_Key1 { }
		[DependencyContract(typeof(MockClass_Must_Success_With_Key2), "uniqueKey1")]
		internal class MockClass_Must_Success_With_Key2 { }

		[TestCategory("BVT Function"), TestMethod]
		[Description("중복된 Contract지만 Key가 다르므로 성공 테스트, Verify() 결과가 True 이면 통과")]
		public void FrameworkDependencyVisitor_By_Must_Success_With_Key()
		{
			Type[] mockTypes = new Type[]
			{
				typeof(MockClass_Must_Success_With_Key1),
				typeof(MockClass_Must_Success_With_Key2)
			};

			FrameworkTypeCatalog catalog = new FrameworkTypeCatalog(mockTypes);
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(catalog);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);

			var result = root.Verify();

			Assert.IsTrue(result.Result);
		}


		[DependencyContract(typeof(MockClass_Must_Fail1))]
		internal class MockClass_Must_Fail1 { }
		[DependencyContract(typeof(MockClass_Must_Fail1))]
		internal class MockClass_Must_Fail2 { }

		[TestCategory("BVT Function"), TestMethod]
		[Description("중복된 Contract와 Key가 존재하므로 실패 테스트, Verfy() 결과가 False 이면 통과")]
		public void FrameworkDependencyVisitor_By_Extension_and_Must_Fail()
		{
			Type[] mockTypes = new Type[]
			{
				typeof(MockClass_Must_Fail1),
				typeof(MockClass_Must_Fail2)
			};

			FrameworkTypeCatalog catalog = new FrameworkTypeCatalog(mockTypes);
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(catalog);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);

			var result = root.Verify();

			Assert.IsFalse(result.Result);

			foreach (var element in result.InvalidRegisterElement)
			{
				TestContext.WriteLine("Invalid Element - Contract:{0} key:{1}", element.contract, element.key);
			}
		}

		[DependencyContract]
		internal class MockClass_NonTypeOfContractName_But_Success1 { }
		[DependencyContract]
		internal class MockClass_NonTypeOfContractName_But_Success2 { }

		[TestCategory("BVT Function"), TestMethod]
		[Description("Contact계약의 Type/Key 가 없는 DependencyContact 테스트, Verify() 결과가 True 이면 통과")]
		public void FrameworkDependencyVisitor_By_NonTypeOfContractName_But_Must_Success()
		{
			Type[] mockTypes = new Type[]
			{
				typeof(MockClass_NonTypeOfContractName_But_Success1),
				typeof(MockClass_NonTypeOfContractName_But_Success2)
			};

			FrameworkTypeCatalog catalog = new FrameworkTypeCatalog(mockTypes);
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(catalog);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);

			var result = root.Verify();

			Assert.IsTrue(result.Result);
		}
	}
}
