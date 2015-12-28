using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Mapping
{
	[TestClass]
	public class KeyValueMapping_Tests : UnitTestBase
	{
		public class Person
		{
			public string Name { get;set;}
			public int Age { get;set;}
		}

		public class PersonMapping : KeyValueMapping<Person>
		{
			protected override void InitializeMapping()
			{
				this.Map(1).Return(new Person() { Name = "A" })
					.Map(2).Return(new Person() { Name = "B" });
			}
		}


		[TestCategory("BVT Function"), TestMethod]
		[Description("KeyValueMapping 으로 매핑 테스트, 매핑 값이 올바르면 통과")]
		[AssertionDescription(0, "Mapping 클래스에 키값 {0} 이/가 존재해야 하는데, 존재하지 않습니다")]
		public void KeyValueMapping_Test()
		{
			PersonMapping mapping = new PersonMapping();

			AssertExtension.IsTrue(mapping.GetMappingValue(1).Name == "A", 0, 0);
			AssertExtension.IsTrue(mapping.GetMappingValue(2).Name == "B", 0, 1);
		}

		[TestCategory("BVT Function"), TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		[Description("KeyValueMapping 으로 존재하지 않는 키값 테스트, 오류가 발생해야 통과")]
		public void Expected_KeyValueMapping_Test()
		{
			PersonMapping mapping = new PersonMapping();

			Assert.IsTrue(mapping.GetMappingValue(3).Name == String.Empty, "Mapping 클래스에 키값 3이 존재하지 않는데 오류가 발생하지 않았습니다");
		}


		public class PersonMappingAndDefaultMapping : KeyValueMapping<Person>
		{
			protected override void InitializeMapping()
			{
				this.Map(1).Return(new Person() { Name = "A" })
					.Map(2).Return(new Person() { Name = "B" })
					.MapDefault().Return(new Person() { Name = "DEFAULT" });
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("KeyValueMapping 으로 Default 값을 설정한 테스트, 키 값이 없을 때 기본 값을 리턴하면 통과")]
		public void KeyValueMapping_And_DefaultMapped_Test()
		{
			PersonMappingAndDefaultMapping mapping = new PersonMappingAndDefaultMapping();

			Assert.IsTrue(mapping.GetMappingValue(3).Name == "DEFAULT");
		}





		public class PersonMultiMapping : KeyValueMapping<Person>
		{
			protected override void InitializeMapping()
			{
				this.Map(1)
					.Map(2)
					.Map(3).Return(new Person() { Name = "A" })
					.Map(4).Return(new Person() { Name = "B" })
					.MapDefault().Return(new Person() { Name = "DEFAULT" });
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("KeyValueMapping 으로 다른 키 값으로 하나의 객체를 매핑, 모든 값이 유효하면 통과")]
		[AssertionDescription(0, "매핑된 키 값이 {0} 의 객체가 {1} 가 아닙니다.")]
		public void KeyValueMapping_MultiMapping_Test()
		{
			PersonMultiMapping mapping = new PersonMultiMapping();

			AssertExtension.IsTrue(mapping.GetMappingValue(1).Name == "A", 0, 1, "A");
			AssertExtension.IsTrue(mapping.GetMappingValue(2).Name == "A", 0, 2, "A");
			AssertExtension.IsTrue(mapping.GetMappingValue(3).Name == "A", 0, 3, "A");
			AssertExtension.IsTrue(mapping.GetMappingValue(4).Name == "B", 0, 4, "B");
			AssertExtension.IsTrue(mapping.GetMappingValue(5).Name == "DEFAULT", 0, 5, "DEFAULT");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Mapping 을 런타임에 입력값, 출력값 모두 Object 인 경우 테스트, 모든 값이 유효하면 통과")]
		public void KeyValueMaping_When_Runtime_Test()
		{
			// 입력 값, 출력 값 모두 Object 타입인 경우
			Mapping mapping = new Mapping();

			mapping.Map(1).Return("Level 1")
				   .Map(2).Return("Level 2");

			var value = mapping.GetMappingValue(1);

			Assert.AreEqual(value, "Level 1");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Mapping 을 런타임에 Well-Known 타입인 경우 테스트, 모든 값이 유효하면 통과")]
		public void KeyValueMapping_When_Runtime_With_Well_Known_Types_Test()
		{
			// 잘 알고있는 타입으로 매핑하는 경우

			Mapping<Int32, Person> mapping = new Mapping<int, Person>();

			mapping.Map(1).Return(new Person() { Name = "NCsoft 1" })
				   .Map(2).Return(new Person() { Name = "NCsoft 2" });

			var value = mapping.GetMappingValue(1);

			Assert.IsTrue(value.Name == "NCsoft 1");
		}
	}
}