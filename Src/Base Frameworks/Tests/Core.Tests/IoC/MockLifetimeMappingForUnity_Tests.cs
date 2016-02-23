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
	public class MockLifetimeMappingForUnity_Tests : UnitTestBase
	{
		internal class MockDefaultLifetimeMappingForUnity : LifetimeMapping<LifetimeManager>
		{
			protected override void InitializeMapping()
			{
				this.Map(o => o == LifetimeFlag.Default).Return(o => new ContainerControlledLifetimeManager())
					.Map(o => o == LifetimeFlag.External).Return(o => new ExternallyControlledLifetimeManager());
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MockDefaultLifetimeMappingForUnity 등록된 매핑 객체가 올바른지 검사합니다. LifetimeFlag.External 이 ExternallyControlledLifetimeManager 타입이면 성공")]
		public void MockMapping_Default_Test()
		{
			var mapping = new MockDefaultLifetimeMappingForUnity();

            Assert.IsTrue( mapping.GetLifetimeObject(LifetimeFlag.External) is Microsoft.Practices.Unity.ExternallyControlledLifetimeManager, "MockLifetimeMappingForUnity에 래핑된 LifetimeFlag.External 이 ExternallyControlledLifetimeManager 타입이 아니라서 실패");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MockDefaultLifetimeMappingForUnity 등록된 매핑 객체가 올바른지 검사합니다. FrameworkDependencyException이 발생하면 성공")]
		[ExpectedException(typeof(FrameworkDependencyException), "등록되지 않은 LifetimeFlag.PerThread 가 NULL 이여야 합니다.")]
		public void MockMapping_Not_mapping_lifetimeflag()
		{
			var mapping = new MockDefaultLifetimeMappingForUnity();

            var obj = mapping.GetLifetimeObject(LifetimeFlag.PerThread);
			Console.WriteLine(obj.ToString());
		}









		internal class MockRemoveLifetimeMappingForUnity : LifetimeMapping<LifetimeManager>
		{
			protected override void InitializeMapping()
			{
				this.Map(o => o == LifetimeFlag.Default).Return(o => new ContainerControlledLifetimeManager())
					.Map(o => o == LifetimeFlag.External).Return(o => new ExternallyControlledLifetimeManager())
					.RemoveMap(LifetimeFlag.External);
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MockRemoveLifetimeMappingForUnity 에 매핑 정보를 검증합니다. RemoveMap 으로 제거된 External 때문에 FrameworkDependencyException 예외가 발생해야 성공")]
		[ExpectedException(typeof(FrameworkDependencyException), "LifetimeMapping에서 RemoveMap(LifetimeFlag.External) 이 되었지만 제거가 되지 않아 오류가 발생")]
		public void MockMapping_Remove_Mapping_Lifetime_Test()
		{
			var mapping = new MockRemoveLifetimeMappingForUnity();
            var map = mapping.GetLifetimeObject(LifetimeFlag.External);
		}












		internal class MockAntoherLifetimeMappingForUnity : LifetimeMapping<LifetimeManager>
		{
			protected override void InitializeMapping()
			{
				this.Map(o => o == LifetimeFlag.Default).Return(o => new ContainerControlledLifetimeManager())
					.Map(o => o == LifetimeFlag.External).Return(o => new ExternallyControlledLifetimeManager())
					.MapAnothers().Return(o => new HierarchicalLifetimeManager());
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MockAntoherLifetimeMappingForUnity 매핑 객체에 MapAnothers() 로 등록된 매핑 정보가 HierarchicalLifetimeManager인지 검사, MapAnothers로 등록된 모든 Lifetime 이 HierarchicalLifetimeManager여야 성공")]
		public void MockMapping_AnothersMapping_Lifetime_Test()
		{
			var mapping = new MockAntoherLifetimeMappingForUnity();

            var defaultMap = mapping.GetLifetimeObject(LifetimeFlag.Default);
			var externalMap = mapping.GetLifetimeObject(LifetimeFlag.External);
			Assert.IsTrue( defaultMap is ContainerControlledLifetimeManager, "MapAnothers로 등록되기 전의 매핑 정보이므로 ContainerControlledLifetimeManager 타입이 아닙니다. 현재 매핑 타입은 {0} 입니다.", defaultMap.GetType().ToString());
			Assert.IsTrue( externalMap is ExternallyControlledLifetimeManager, "MapAnothers로 등록되기 전의 매핑 정보이므로 ExternallyControlledLifetimeManager 타입이 아닙니다. 현재 매핑 타입은 {0} 입니다.", externalMap.GetType().ToString());
			
			
			var anotherMap1 = mapping.GetLifetimeObject(LifetimeFlag.Hierarchy);
			var anotherMap2 = mapping.GetLifetimeObject(LifetimeFlag.PerThread);

			Assert.IsTrue( anotherMap1 is HierarchicalLifetimeManager, "MapAnothers() 로 모든 연결된 Lifetime 은 HierarchicalLifetimeManager타입이여야 성공합니다");
			Assert.IsTrue( anotherMap2 is HierarchicalLifetimeManager, "MapAnothers() 로 모든 연결된 Lifetime 은 HierarchicalLifetimeManager타입이여야 성공합니다");
		}
	}
}
