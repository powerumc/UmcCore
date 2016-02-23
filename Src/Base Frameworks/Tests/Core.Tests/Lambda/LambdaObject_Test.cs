using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core
{
	[TestClass]
	public class LambdaObject_Test : UnitTestBase
	{
		public class MockPerson
		{
			public string Name { get; set; }
			public int Age { get; set; }
			public string Description { get; set; }
			public int Level { get; set; }
		}

		private MockPerson person = new MockPerson()
		{
			Name = TestOwner.Junil_Um,
			Age = 1
		};


		[TestCategory("BVT Function"), TestMethod]
		[Description("Lambda 확장 개체가 올바르게 동작하는지 테스트, 오류가 발생하지 않으면 통과")]
		public void LambdaContextObject_ExtensionMethod_Test()
		{
			person.If(o => o.Name == TestOwner.Junil_Um)
				.AndIf(o => o.Age == 1)
				.OrIf(o => o.Name == "")
				.Then(o => o.Description = "Assert is True")	
				.Else(o => o.Description = "Assert is Fail")
				.Invoke();

			Assert.IsTrue(person.Description == "Assert is True");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Lambda If 객체의 If, AndIf, OrIf 테스트, 각 조합 결과를 만족하면 통과")]
		public void LambdaContextObject_If_Test()
		{
			var lambda = new LambdaContextObject<MockPerson>(person);

            // 기본 조건 테스트
            Assert.IsTrue(lambda.If(o => o.Name == TestOwner.Junil_Um).Value);
			Assert.IsFalse(lambda.If(o => o.Name == TestOwner.Junil_Um + "A").Value);


			// And 조건 테스트
			Assert.IsTrue(lambda.If(o => o.Name == TestOwner.Junil_Um)
								.AndIf(o => o.Age == 1).Value);
			Assert.IsFalse(lambda.If(o => o.Name == TestOwner.Junil_Um)
								.AndIf(o => o.Age == 2).Value);

			// Or 조건 테스트
			Assert.IsTrue(lambda.If(o => o.Name == TestOwner.Junil_Um)
								.OrIf(o => o.Age == 2).Value);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Lambda 의 LambdaInvokeObject 테스트, LambdaThen 결과가 올바르면 통과")]
		public void LambdaContextObject_Then_Test()
		{
			var lambda = new LambdaContextObject<MockPerson>(person);

            bool thenResult = false;
			lambda.If(o => o.Name == TestOwner.Junil_Um)
					.AndIf(o => o.Age == 1)
					.Then(o => thenResult = true)
					.Invoke();

			Assert.IsTrue(thenResult);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Lambda 의 LambdaElse 객체가 올바르게 동작하는지 테스트, Else 조건이 만족하면 통과")]
		public void LambdaContextObject_Else_Test()
		{
			var lambda = new LambdaContextObject<MockPerson>(person);

            bool thenResult = false;
			bool elseResult = false;
			lambda.If(o => o.Name == TestOwner.Junil_Um)
					.AndIf(o => o.Age == 2)
					.Then(o => thenResult = true)
					.Else(o => elseResult = true)
					.Invoke();

			Assert.IsFalse(thenResult);
			Assert.IsTrue(elseResult);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Lambda 를 이용하여 복합 조합식이 올바르게 동작하는지 테스트, 모든 연산 결과가 올바르면 통과")]
		public void LambdaContextObject_ElseIf_Test()
		{
			var lambda = new LambdaContextObject<MockPerson>(person);

            bool thenResult = false;
			bool elseResult = false;
			bool elseResult1 = false;
			bool elseResult2 = false;
			bool elseResult3 = false;

			lambda.If(o => o.Name == TestOwner.Junil_Um)
					.AndIf(o => o.Age == 2)
					.Then(o => thenResult = true)
					.ElseIf(o => o.Age == 3, o => { Console.WriteLine("ELSEIF1"); elseResult1 = true; })
					.ElseIf(o => o.Age == 1, o => { Console.WriteLine("ELSEIF2"); elseResult2 = true; })
					.ElseIf(o => o.Age == 4, o => { Console.WriteLine("ELSEIF3"); elseResult3 = true; })
					.Else(o => { Console.WriteLine("ELSE"); elseResult = true; })
					.Invoke();

			Assert.IsFalse(thenResult);
			Assert.IsFalse(elseResult1);
			Assert.IsTrue(elseResult2);
			Assert.IsFalse(elseResult3);
			Assert.IsFalse(elseResult);
		}
	}
}
