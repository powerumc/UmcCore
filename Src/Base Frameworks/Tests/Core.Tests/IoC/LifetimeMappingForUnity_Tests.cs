using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using Microsoft.Practices.Unity;

namespace Umc.Core.IoC
{
	[TestClass]
	public class LifetimeMappingForUnity_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("LifetimeMapping 객체의 LifetimeFlag.External 이 ExternallyControlledLifetimeManager 타입인지 테스트, 타입이 같으면 통과")]
		public void TestMethod1()
		{
			var mapping = new LifetimeMappingForUnity();

            var obj = mapping.GetLifetimeObject(LifetimeFlag.External, null);
			Assert.IsTrue( obj is ExternallyControlledLifetimeManager, "LifetimeObject 가 ExternallyControlledLifetimeManager 이 아닙니다.");
		}
	}
}
