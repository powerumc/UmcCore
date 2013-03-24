using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using Castle.MicroKernel.Lifestyle;

namespace Umc.Core.IoC.Castle
{
	[TestClass]
	public class LifetimeMappingForCastle_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForCastle에서 관리하는 Castle IoC 의 LifeStyle을 검증합니다. 매핑된 모든 타입이 일치하면 성공")]
		public void LifetimeMappingForCastle_MappingTest()
		{
			LifetimeMappingForCastle mapping = new LifetimeMappingForCastle();

			var defaultMap = mapping.GetLifetimeObject(LifetimeFlag.Default);
			var perthreadMap = mapping.GetLifetimeObject(LifetimeFlag.PerThread);
			var singletonMap = mapping.GetLifetimeObject(LifetimeFlag.Singleton);
			var transientMap = mapping.GetLifetimeObject(LifetimeFlag.PerCall);

			Assert.IsTrue( defaultMap	== typeof(SingletonLifestyleManager), "LifetimeFlag.Default 의 타입이 SingletonLifestyleManager 이 아니기 때문에 실패하였음.");
			Assert.IsTrue( perthreadMap	== typeof(PerThreadLifestyleManager), "LifetimeFlag.PerThread 의 타입이 PerThreadLifestyleManager 이 아니기 때문에 실패하였음.");
			Assert.IsTrue( singletonMap	== typeof(SingletonLifestyleManager), "LifetimeFlag.Singleton 의 타입이 SingletonLifestyleManager 이 아니기 때문에 실패하였음.");
			Assert.IsTrue( transientMap	== typeof(TransientLifestyleManager), "LifetimeFlag.PerCall 의 타입이 TransientLifestyleManager 이 아니기 때문에 실패하였음.");
		}
	}
}
