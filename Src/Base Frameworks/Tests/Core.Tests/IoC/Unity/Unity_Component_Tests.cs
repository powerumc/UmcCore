using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Umc.Core.IoC.Unity
{
	[TestClass]
	public class Unity_Component_Tests : UnitTestBase
	{
		#region Mock Class for Injection
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

			public MockA MockA { get; set; }

			public string Name { get; set; }
		} 
		#endregion

		[TestCategory("BVT Function"), TestMethod]
		public void Unity_Injection_Basic_Test()
		{
            using (IUnityContainer container = new UnityContainer())
            {
                container.RegisterType<MockA>()
                        .RegisterType<MockB>(new InjectionProperty("MockA"), new InjectionProperty("Name", "Junil, Um"));

                var obj = container.Resolve<MockB>();

                obj.Say();
                obj.MockA.Say();

                Assert.IsNotNull(obj.Name, "MockB 개체의 Injection 된 프로퍼티의 값이 NULL 여서 오류가 발생");

                Console.WriteLine("MockB.Name = {0}", obj.Name);
            }
        }








		#region Mock Class for Interceptor


		public interface IMockClass
		{
			void Say();
			void Say2();
		}

		[DependencyContract(typeof(IMockClass))]
		public class MockClassForInterfaceInterceptor : IMockClass
		{
			public void Say()
			{
				Console.WriteLine("Say MockInterceptor");
			}

			public void Say2()
			{
				Console.WriteLine("Say2 MockInterceptor");
			}
		}

		internal class MockClassForContextboundObject : ContextBoundObject
		{
			public void Say()
			{
				Console.WriteLine("Say MockInterceptor");
			}

			public void Say2()
			{
				Console.WriteLine("Say2 MockInterceptor");
			}
		}

		internal class MockClassForContextboundObjectBehavior : IInterceptionBehavior
		{
			public IEnumerable<Type> GetRequiredInterfaces()
			{
				return Enumerable.Empty<Type>();
			}

			public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
			{
				Console.WriteLine("Before Interception");

				var @return = getNext()(input, getNext);
				
				Console.WriteLine("After Interception");

				return @return;
			}

			public bool WillExecute
			{
				get { return true; }
			}
		}

		#endregion

		[TestCategory("BVT Function"), TestMethod]
		public void Unity_Interceptor_By_ContextBoundObject_Test()
		{
            using (IUnityContainer container = new UnityContainer())
            {
                container.AddNewExtension<Interception>();
                container.RegisterType<MockClassForContextboundObject>(
                    new Interceptor<TransparentProxyInterceptor>(),
                    new InterceptionBehavior<MockClassForContextboundObjectBehavior>());

                var obj = container.Resolve<MockClassForContextboundObject>();
                obj.Say();
                obj.Say();
                obj.Say2();
                obj.Say2();
            }
        }

		[TestCategory("BVT Function"), TestMethod]
		public void Unity_Interceptor_By_InterfaceInterceptor_Test()
		{
            using (IUnityContainer container = new UnityContainer())
            {
                container.AddNewExtension<Interception>()
                        .RegisterType<IMockClass, MockClassForInterfaceInterceptor>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<MockClassForContextboundObjectBehavior>());

                var obj = container.Resolve<IMockClass>();
                obj.Say();

                TestContext.WriteLine(obj.GetType().AssemblyQualifiedName);
            }
        }



		internal class Mock_ConstructorInjection
		{
			[InjectionConstructor]
			public Mock_ConstructorInjection(string a, int b)
			{
				Console.WriteLine("a={0}, b={1}",a,b);
			}
		}

		[TestCategory("BVT Function"), TestMethod]
		public void Mock_ConstructorInjection_Test()
		{
            using (IUnityContainer container = new UnityContainer())
            {
                container.RegisterType<Mock_ConstructorInjection>(
                    new InjectionConstructor(new InjectionParameter("Junil, Um"), new InjectionParameter(100)));

                container.Resolve<Mock_ConstructorInjection>();
            }
        }
	}
}
