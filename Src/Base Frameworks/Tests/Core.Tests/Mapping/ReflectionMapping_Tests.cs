using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Mapping
{
	[TestClass]
	public class ReflectionMapping_Tests : UnitTestBase
	{
		internal class PersonSource
		{
			public string Name { get; set; }
		}

		internal class PersonTarget
		{
			public string name { get; set; }
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("ReflectionMapping 으로 대소문자가 틀린 객체간의 속성을 매핑 테스트, 오류가 발생하지 않고 매핑이 정상적이면 통과")]
		public void ReflectionMapping_Test()
		{
			PersonSource source = new PersonSource() { Name = "Junil, Um" };
			PersonTarget target = new PersonTarget();

			ReflectionMapping mapping = new ReflectionMapping(source, target);

			TestContext.WriteLine("SourcePerson's name is {0}", source.Name);
			TestContext.WriteLine("TargetPerson's name is {0} after Mapped ReflectionMapping", target.name);
			TestContext.WriteLine("{0} = {1}", source.Name, target.name);

			Assert.IsTrue(source.Name == target.name, "리플랙션으로 매핑된 Name 속성의 값이 틀립니다.");
		}


		internal class PersonTarget2
		{
			public string name { get; set; }
			public int age { get; set; }
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("ReflectionMapping 의 속성 개수가 맞지 않는 경우 오류가 발생해야 함, MappingException 오류가 발생하면 통과")]
		[ExpectedException(typeof(MappingException))]
		public void Expected_Not_Matched_PropertyCount()
		{
			PersonSource source = new PersonSource() { Name = "Junil, Um" };
			PersonTarget2 target = new PersonTarget2();

			ReflectionMapping mapping = new ReflectionMapping(source, target, new ReflectionMappingOption() { ThrowIfNotMatched = true });

		}
	}
}
