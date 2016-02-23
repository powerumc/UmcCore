using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Mapping
{
	[TestClass]
	public class LazyMapping_Tests : UnitTestBase
	{
		public class Person
		{
			public string Name { get; set; }
			public int Age { get; set; }
		}

		public class PersonMapping : LazyMapping<int, Person>
		{
			protected override void InitializeMapping()
			{
				this.Map(o => o == 1).Return(o => new Person() { Name = "A" })
					.Map(o => o == 2).Return(o => new Person() { Name = "B" });
			}
		}

		public class SampleLazyMapping : LazyMapping<Int32, String>
		{
			protected override void InitializeMapping()
			{
				this.Map(o => o == 1).Return(o => "Level 1")
					.Map(o => o == 2).Return(o => "Level 2")
					.MapDefault().Return(o => "Nothing");
			}
		}



		[TestCategory("BVT Function"), TestMethod]
		[Description("LazyMapping 으로 매핑 테스트, 매핑 값이 올바르면 통과")]
		[AssertionDescription(0, "Mapping 클래스에 키값 {0} 이/가 존재해야 하는데, 존재하지 않습니다")]
		public void LazyMapping_Test()
		{
			var mapping = new PersonMapping();

            AssertExtension.IsTrue(mapping.GetMappingValue(1).Name == "A", 0, 0);
			AssertExtension.IsTrue(mapping.GetMappingValue(2).Name == "B", 0, 1);
		}

		[TestCategory("BVT Function"), TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		[Description("LazyMapping 으로 존재하지 않는 키값 테스트, 오류가 발생해야 통과")]
		public void Expected_LazyMapping_Test()
		{
			var mapping = new PersonMapping();

            Assert.IsTrue(mapping.GetMappingValue(3).Name == String.Empty, "Mapping 클래스에 키값 3이 존재하지 않는데 오류가 발생하지 않았습니다");
		}

		public class PersonMappingAndDefaultMapping : LazyMapping<int, Person>
		{
			protected override void InitializeMapping()
			{
				this.Map( o => o == 1).Return( o => new Person() { Name = "A" })
					.Map( o => o == 2).Return( o => new Person() { Name = "B" })
					.MapDefault().Return( o => new Person() { Name = "DEFAULT" });
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("LazyMapping 으로 Default 값을 설정한 테스트, 키 값이 없을 때 기본 값을 리턴하면 통과")]
		public void LazyMapping_And_DefaultMapped_Test()
		{
			var mapping = new PersonMappingAndDefaultMapping();

            Assert.IsTrue(mapping.GetMappingValue(3).Name == "DEFAULT");
		}




		public class PersonMultiMapping : LazyMapping<int, Person>
		{
			protected override void InitializeMapping()
			{
				this.Map( o => o == 1)
					.Map( o => o == 2)
					.Map( o => o == 3).Return( o => new Person() { Name = "A" })
					.Map( o => o == 4).Return( o => new Person() { Name = "B" })
					.MapDefault().Return( o => new Person() { Name = "DEFAULT" });
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("LazyMapping 으로 다른 키 값으로 하나의 객체를 매핑, 모든 값이 유효하면 통과")]
		[AssertionDescription(0, "매핑된 키 값이 {0} 의 객체가 {1} 가 아닙니다.")]
		public void LazyMapping_MultiMapping_Test()
		{
			var mapping = new PersonMultiMapping();

            AssertExtension.IsTrue(mapping.GetMappingValue(1).Name == "A", 0, 1, "A");
			AssertExtension.IsTrue(mapping.GetMappingValue(2).Name == "A", 0, 2, "A");
			AssertExtension.IsTrue(mapping.GetMappingValue(3).Name == "A", 0, 3, "A");
			AssertExtension.IsTrue(mapping.GetMappingValue(4).Name == "B", 0, 4, "B");
			AssertExtension.IsTrue(mapping.GetMappingValue(5).Name == "DEFAULT", 0, 5, "DEFAULT");
		}



		public class ConstructorInitPersonMapping : LazyMapping<int, Person>
		{
			private int number;
			public ConstructorInitPersonMapping(int number)
			{
				this.number = number;
			}

			protected override void InitializeMapping()
			{
				this.Map(o => o == 1).Return(o => new Person() { Age = 1 })
					.Map(o => o == 2).Return(o => new Person() { Age = 2 });
			}
		}


		[TestCategory("BVT Function"), TestMethod]
		[Description("생성자 초기화한 매핑 테스트, 모든 값이 유효하면 통과")]
		public void LazyMapping_After_Initialize_Constructor_Mapping_Test()
		{
			var mapping = new ConstructorInitPersonMapping(1);

            TestContext.WriteLine("Mapping 1's Age is {0}", mapping.GetMappingValue(1).Age);
			TestContext.WriteLine("Mapping 2's Age is {0}", mapping.GetMappingValue(2).Age);

			Assert.IsTrue(mapping.GetMappingValue(1).Age == 1);
			Assert.IsTrue(mapping.GetMappingValue(2).Age == 2);
		}
	}
}