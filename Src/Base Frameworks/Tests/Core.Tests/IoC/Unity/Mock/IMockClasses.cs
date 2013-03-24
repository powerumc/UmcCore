using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Umc.Core.IoC.Unity
{
	public interface IMockSimple
	{
		void Say();
		string Name { get; set; }
	}

	[DependencyContract(typeof(IMockSimple))]
	public class MockSimple : IMockSimple
	{
		public void Say()
		{
			Console.WriteLine("Say Hello");
		}

		public string Name { get; set; }
	}


	public interface IMockConstructor
	{
		void Delegate();
	}

	public class MockConstructor : IMockConstructor
	{
		private IMockSimple imockclass;

		public MockConstructor(IMockSimple imockclass)
		{
			this.imockclass = imockclass;
		}

		public void Delegate()
		{
			imockclass.Say();
		}
	}

	public interface IMockInitConstructor
	{
		void Say();
	}

	public class MockInitConstructor : IMockInitConstructor
	{
		public void Say()
		{
			Console.WriteLine("Mock3 Say Hello");
		}
	}

	public interface IMockPropertyInjection
	{
		void Say();
	}

	public class MockPropertyInjection : IMockPropertyInjection
	{
		[Microsoft.Practices.Unity.Dependency]
		public IMockSimple mock { get; set; }

		public void Say()
		{
			this.mock.Say();
		}
	}

}
