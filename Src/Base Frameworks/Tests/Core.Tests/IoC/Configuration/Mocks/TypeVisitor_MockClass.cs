using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Umc.Core.IoC.Configuration
{
	public interface IMockDependSimple
	{
		void Say();
	}

	[DependencyContract(typeof(IMockDependSimple))]
	public class MockDependSimple : IMockDependSimple
	{
		public void Say()
		{
			Console.WriteLine("MockDependSimple");
		}
	}

	public interface IMockDependPropertyInjection
	{
		void DependOnProperty();
	}

	[DependencyContract(typeof(IMockDependPropertyInjection), LifetimeFlag.PerCall)]
	public class MockDependPropertyInjection : IMockDependPropertyInjection
	{
		[DependencyInjection]
		public MockDependPropertyInjection(IMockDependSimple mock)
		{
			Console.WriteLine("This is constructor Injection");
			mock.Say();
			Console.WriteLine("---------------------------------------");
		}

		public string NonInjectionProperty { get; set; }

		[DependencyInjection]
		public IMockDependSimple mock { get; set; }

		[DependencyInjection(DefaultValue="Junil, Um")]
		public string TempName { get; set; }

		public void DependOnProperty()
		{
			Console.WriteLine("Call DependOnProperty() Method");
			Console.WriteLine("Following inject the IMockDependSimple property");
			mock.Say();
		}

		public void Test([DependencyInjection]IMockDependSimple mock)
		{
			mock.Say();
		}
	}

	[DependencyContract(typeof(MockConstructorInjection))]
	public class MockConstructorInjection
	{
		[DependencyInjection]
		public MockConstructorInjection([DependencyInjection]MockConstructorInjection mock, [DefaultValue("A")]string value)
		{
		}
	}

	[DependencyContract(typeof(MockMethodInjection))]
	public class MockMethodInjection
	{
		[DependencyInjection]
		public void Action([DependencyInjection]IMockDependSimple mock,
							[DefaultValue("A")]string value,
							[DependencyInjection("mock2")] IMockDependSimple mock2)
		{
		}
	}
}
