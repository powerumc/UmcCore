using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.IoC.Castle
{
	[TestClass]
	public class FrameworkContainerForCastle_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForCastle 에서 객체를 등록한 후에 객체를 Resolve 하여 메서드를 호출한다. 오류가 발생하지 않으면 성공")]
		public void FrameworkContainerForCastle_Register_Test1()
		{
			FrameworkContainerForCastle container = new FrameworkContainerForCastle();
			container.RegisterType<IMockSimple, MockSimple>();

			container.Resolve<IMockSimple>().Say();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForCastle 에서 객체를 등록한 후에 Dependency Injection 을 수행한 후 메서드를 호출한다. NullReferenceException 이 발생하지 않으면 성공")]
		public void FrameworkContainerForCastle_Register_Dependency_Injection_Test1()
		{
			FrameworkContainerForCastle container = new FrameworkContainerForCastle();
			container.RegisterType<IMockSimple, MockSimple>();
			container.RegisterType<IMockConstructorInject, MockConstructorInject>();

			container.Resolve<IMockConstructorInject>().Depend();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForCastle 에 하나의 자식 Container 를 등록한 후 자식 Container 개수 검증, 자식 Container 개수가 1개여야 성공")]
		public void FrameworkContainerForCastle_With_Child_Container_Must_One_Child_Test1()
		{
			FrameworkContainerForCastle container = new FrameworkContainerForCastle();
			container.RegisterType<IMockSimple, MockSimple>();

			FrameworkContainerForCastleChild child = new FrameworkContainerForCastleChild("child", container);
			child.RegisterType<IMockConstructorInject, MockConstructorInject>();

			Assert.IsTrue( container.Childs.Count() == 1, "container 에 하나의 자식 Container 를 등록하였지만, Childs 개수가 {0} 이라 오류입니다.", container.Childs.Count());
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForCastle 에 두개의 자식 Container 를 등록한 후 자식 Container 개수 검증, 자식 Container 개수가 2개여야 성공")]
		public void FrameworkContainerForCastle_With_Child_Container_Must_Two_Childs_Test1()
		{
			FrameworkContainerForCastle container = new FrameworkContainerForCastle();
			container.RegisterType<IMockSimple, MockSimple>();

			FrameworkContainerForCastleChild child1 = new FrameworkContainerForCastleChild("child", container);
			child1.RegisterType<IMockConstructorInject, MockConstructorInject>();

			FrameworkContainerForCastleChild child2 = new FrameworkContainerForCastleChild("child2", container);

			Assert.IsTrue(container.Childs.Count() == 2, "container 에 두개의 자식 Container 를 등록하였지만, Childs 개수가 {0} 이라 오류입니다.", container.Childs.Count());
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForCastle 에 Singleton 객체를 등록하고 값을 변경 후 다시 객체를 꺼냄, 다시 객체를 꺼낼 때 값은 변하지 않으면 성공")]
		public void FrameworkContainerForCastle_Register_Singleton_Test()
		{
			FrameworkContainerForCastle container = new FrameworkContainerForCastle();
			container.RegisterType<IMockSimple, MockSimple>(LifetimeFlag.Singleton);

			var obj = container.Resolve<IMockSimple>();
			obj.Name = "엄준일";

			obj = container.Resolve<IMockSimple>();

			Assert.IsTrue(obj.Name == "엄준일", "FrameworkContainer 에 Singleton 객체를 등록하였지만, 객체의 상태가 변했습니다");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForCastle 에 PerCall 객체를 등록하고 값을 변경 후 다시 객체를 꺼냄, 다시 객체를 꺼낼 때 초기값인 NULL 이면 성공")]
		public void FrameworkContainerForCastle_Register_PerCall_Test()
		{
			FrameworkContainerForCastle container = new FrameworkContainerForCastle();
			container.RegisterType<IMockSimple, MockSimple>(LifetimeFlag.PerCall);

			var obj = container.Resolve<IMockSimple>();
			obj.Name = "엄준일";

			obj = container.Resolve<IMockSimple>();

			Assert.IsTrue(obj.Name == null, "FrameworkContainer 에 PerCall 객체를 등록하였지만, 객체의 상태가 초기값과 다릅니다.");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForCastle 에 Property Injection 이 동작하는지 여부 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkContainerForCastle_Property_Injection_Test()
		{
			FrameworkContainerForCastle container = new FrameworkContainerForCastle();
			container.RegisterType<IMockSimple, MockSimple>()
				.RegisterType<IMockPropertyInjection, MockPropertyInjection>();

			container.Resolve<IMockPropertyInjection>().Say();
		}
	}
}
