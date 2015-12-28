using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Reflection
{
	/// <summary>
	/// <para>"Umc.Core.Reflection.GetCustomAttribute<>() 확장 메서드 테스트</para>
	/// </summary>
	[TestClass]
	public class ReflectionUtil_Tests : UnitTestBase
	{
		[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
		internal class DescriptionForTestingAttribute : Attribute
		{
			public DescriptionForTestingAttribute() { }
			public DescriptionForTestingAttribute(string message) : this() { }
		}

		internal class MockClassForAttributeTest_ZeroAttribute
		{
		}

		[DescriptionForTestingAttribute("DescriptionAttribute 1")]
		internal class MockClassForAttributeTest_OneAttribute
		{
		}


		[DescriptionForTestingAttribute("DescriptionAttribute 1")]
		[DescriptionForTestingAttribute("DescriptionAttribute 2")]
		internal class MockClassForAttributeTest_TwoAttributes
		{
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("ReflectionUtil.GetCustomAttributeEx 의 결과는 NULL 이어야 합니다.")]
		public void GetCustomAttribute_MustNull_Test()
		{
			var attributes = typeof(MockClassForAttributeTest_ZeroAttribute).GetCustomAttributeEx<DescriptionForTestingAttribute>();
			
			Assert.IsNull(attributes, "DescriptionAttribute 특성이 NULL 이어야하지만, NULL이 아닙니다");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("ReflectionUtil.GetCustomAttributeEx 의 결과는 NOT NULL 이여야 한다.")]
		public void GetCustomAttribute_MustOneAttribute_Test()
		{
			var attributes = typeof(MockClassForAttributeTest_OneAttribute).GetCustomAttributeEx<DescriptionForTestingAttribute>();

			Assert.IsNotNull(attributes, "MockClassForAttributeTest_OneAttribute 의 DescriptionForTestingAttribute 특성은 반드시 1개여야 합니다.");
		}


		[TestCategory("BVT Function"), TestMethod]
		[Description("ReflectionUtil.GetCustomAttributeEx 의 Count 는 반드시 2개여야 한다.")]
		public void GetCustomAttribute_MustTwoAttributes_Test()
		{
			var attributes = typeof(MockClassForAttributeTest_TwoAttributes).GetCustomAttributesEx<DescriptionForTestingAttribute>();

			Assert.IsNotNull(attributes, "MockClassForAttributeTest_TwoAttributes 의 DescriptionForTestingAttribute 은 반드시 2개 이상이어야 합니다.");
			Assert.IsTrue( attributes.Count() == 2, "MockClassForAttributeTest_TwoAttributes 의 DescriptionForTestingAttribute 특성은 2개가 선언되었지만, 얻어진 결과는 {0} 개 입니다.", attributes.Count());
		}
	}
}
