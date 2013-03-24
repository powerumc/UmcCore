using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.DynamicProxy;
using Castle.Core;
using Castle.MicroKernel.Facilities;

namespace Umc.Core.IoC.Castle
{
	[TestClass]
	public class Castle_Component_Tests : UnitTestBase
	{
		#region Mock class for Injection
		internal class MockA
		{
			public void Say()
			{
				Console.WriteLine("Say MockA");
			}
		}

		internal class MockB
		{
			public void Say()
			{
				Console.WriteLine("Say MockB");
			}

			public string Name { get; set; }

			public MockA MockA { get; set; }
		} 
		#endregion

		[TestCategory("BVT Function"), TestMethod]
		public void Castle_Injection_Basic_Test()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Register(Component.For<MockA>().ImplementedBy<MockA>())
				.Register(Component.For<MockB>().ImplementedBy<MockB>().DependsOn(Property.ForKey<string>().Eq("Junil, Um")));

			var obj = container.Resolve<MockB>();
			obj.Say();
			Console.WriteLine(obj.Name);
			obj.MockA.Say();
			
		}

		internal interface IMockClass_Castle_By_Key { string Key { get; set; }  }
		internal class MockClass_Castle_By_Key_A : IMockClass_Castle_By_Key
		{
			public MockClass_Castle_By_Key_A() { this.Key = "A"; }
			public string Key { get; set; }
		}
		internal class MockClass_Castle_By_Key_B : IMockClass_Castle_By_Key
		{
			public MockClass_Castle_By_Key_B() { this.Key = "B"; }
			public string Key { get;set;}
		}

		internal class MockClass_Castle_By_Key
		{
			public IMockClass_Castle_By_Key Mock { get; set; }
		}

		[TestCategory("BVT Function"), TestMethod]
		public void MockClass_Castile_By_Key_Test()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Register(Component.For<IMockClass_Castle_By_Key>().ImplementedBy<MockClass_Castle_By_Key_A>().Named("A"));
			container.Register(Component.For<IMockClass_Castle_By_Key>().ImplementedBy<MockClass_Castle_By_Key_B>().Named("B"));

			container.Register(
				Component.For<MockClass_Castle_By_Key>()
				.DynamicParameters(
					(kernel, p) => p["Mock"] = kernel.Resolve<IMockClass_Castle_By_Key>("A")
						));

			Console.WriteLine(container.Resolve<MockClass_Castle_By_Key>().Mock.Key);
		}



		internal class MockClass_ConstructorInjection_ForCastle_Contract
		{
		}

		internal class MockClass_ConstructorInjection_ForCastle_Depend1
		{
			public MockClass_ConstructorInjection_ForCastle_Depend1(string name, int age)
			{
				Console.WriteLine("Name={0}, Age={1}", name, age);
			}

			public void Say()
			{
				Console.WriteLine("Say MockClass_ConstructorInjection_ForCastle_Depend1");
			}
		}

		internal class MockClass_ConstructorInjection_ForCastle_Depend2
		{
			public MockClass_ConstructorInjection_ForCastle_Depend2(string name, int age)
			{
				Console.WriteLine("--------------------------------------");
				Console.WriteLine("Name={0}, Age={1}", name, age);
			}

			public MockClass_ConstructorInjection_ForCastle_Depend2(MockClass_ConstructorInjection_ForCastle_Depend1 contract)
			{
				Console.WriteLine("--------------------------------------");
				contract.Say();
			}

			public void Say()
			{
				Console.WriteLine("Say MockClass_ConstructorInjection_ForCastle_Depend2");
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		public void Castle_ConstructorInjection()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Register(
				Component.For<MockClass_ConstructorInjection_ForCastle_Depend1>()
					.Parameters(Parameter.ForKey("name").Eq("Junil"), Parameter.ForKey("age").Eq("18"))
				);
			container.Register(
				Component.For <MockClass_ConstructorInjection_ForCastle_Depend2>()
				);

			//container.Resolve<MockClass_ConstructorInjection_ForCastle_Depend1>().Say();

			container.Resolve<MockClass_ConstructorInjection_ForCastle_Depend2>().Say();
		}




		internal class MockClass_MethodInjection_ForCastle_Contract
		{
			public void Say() { Console.WriteLine("Contract Say"); }
		}

		internal class MockClass_MethodInjection_ForCastle_Depend
		{
			public void Say(MockClass_MethodInjection_ForCastle_Contract contract)
			{
				contract.Say();
				Console.WriteLine("----------------------------- following say of the depending");
				Console.Write("Depend Say");
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		public void Castle_MethodInjection_Test()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Register(Component.For<MockClass_MethodInjection_ForCastle_Contract>());
			container.Register(Component.For<MockClass_MethodInjection_ForCastle_Depend>()
				.OnCreate( (kernel, e) => e.Say( kernel.Resolve<MockClass_MethodInjection_ForCastle_Contract>())));

			container.Resolve<MockClass_MethodInjection_ForCastle_Depend>();
		}



		#region Mock Class for Interceptor
		public interface IMockClassForCastleInterception
		{
			void Say();
		}

		public class MockClassForCastleInterception : IMockClassForCastleInterception
		{
			public void Say()
			{
				Console.WriteLine("Say MockInterceptor");
			}
		}

		internal class MockClassForCastleInterceptionClass : IInterceptor
		{
			public void Intercept(IInvocation invocation)
			{
				Console.WriteLine("Before Interception");

				invocation.Proceed();

				Console.WriteLine("After Interception");
			}
		}
		#endregion

		[TestCategory("BVT Function"), TestMethod]
		public void Castle_Interception_Basic_Test()
		{
			// Castle 은 InterfaceInterception, VirtualMethodInterception 방식을 자동으로 결정한다.
			IWindsorContainer container = new WindsorContainer();
			container
				.Register(Component.For<MockClassForCastleInterceptionClass>())
				.Register(Component.For<IMockClassForCastleInterception>()
									.ImplementedBy<MockClassForCastleInterception>()
									.Interceptors<MockClassForCastleInterceptionClass>());

			container.Resolve<IMockClassForCastleInterception>().Say();
			Component.For<IMockClassForCastleInterception>();

		}
	}
}