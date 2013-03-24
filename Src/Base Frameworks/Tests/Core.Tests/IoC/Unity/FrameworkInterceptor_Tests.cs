using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;

namespace Umc.Core.IoC.Unity
{
	[TestClass]
	public class FrameworkInterceptor_Tests : UnitTestBase
	{
		public class SimpleInterceptorAttribute : FrameworkInterceptionAttribute
		{
			protected override IFrameworkInterception CreateInterceptor()
			{
				return new SimpleInterception();
			}
		}
		
		public class SimpleInterception : IFrameworkInterception
		{
			public void SetCodeLambda(Dynamic.Proxy.Lambda.ICodeLambda codeLambda)
			{
			}

			public IFrameworkInterceptionReturn Invoke(IFrameworkInterceptionInput input, InterceptionHandler handler)
			{
				Console.WriteLine("Before...");

				var result = handler(input);

				Console.WriteLine("After...");

				return result;
			}
		}

		public class SimpleInterceptorObject
		{
			[SimpleInterceptor]
			public void Say()
			{
				Console.WriteLine("Say...");
			}
		}

		[TestMethod]
		public void SimpleInterception_Test()
		{
			IFrameworkContainer container = new FrameworkContainerForUnity();
			container.RegisterType<SimpleInterceptorObject>();

			container.Resolve<SimpleInterceptorObject>().Say();
		}


		public class B : IInterceptionBehavior
		{
			public IEnumerable<Type> GetRequiredInterfaces()
			{
				throw new NotImplementedException();
			}

			public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
			{
				var result = getNext()(input, getNext);
				return result;
			}

			public bool WillExecute
			{
				get { throw new NotImplementedException(); }
			}
		}

		public class MockClass
		{
			[SimpleInterceptor]
			public void Say()
			{
				Console.WriteLine("Invoke Say Method");
			}
		}


		[TestCategory("BVT Function"), TestMethod]
		public void Register_Interceptor_Test()
		{
			IFrameworkContainer container = new FrameworkContainerForUnity();
			container.RegisterType<MockClass>();

			container.Resolve<MockClass>().Say();
		}





















		public interface IMockInterceptorObject { [MockInterceptorObject]void Say(); }
		public class MockInterceptorObject : IMockInterceptorObject { public void Say() { Console.WriteLine("Invoke Say Method"); } }

		public class MockInterceptorObjectAttribute : HandlerAttribute
		{
			public override ICallHandler CreateHandler(IUnityContainer container)
			{
				return new MockInterceptorObjectCallHandler();
			}
		}

		public class MockInterceptorObjectCallHandler : ICallHandler
		{
			public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
			{
				Console.WriteLine("Before...");

				var result = getNext()(input, getNext);

				Console.WriteLine("After...");

				return result;
			}

			public int Order { get; set; }
		}





		[TestCategory("BVT Function"), TestMethod]
		public void Unity_Dynamic_Interceptor_Test()
		{
			IUnityContainer container = new UnityContainer();

			container.AddNewExtension<Interception>()
				.RegisterType<IMockInterceptorObject, MockInterceptorObject>()
					.Configure<Interception>().SetInterceptorFor<IMockInterceptorObject>(new InterfaceInterceptor());
			
			container.Resolve<IMockInterceptorObject>().Say();
		}
	}
}
