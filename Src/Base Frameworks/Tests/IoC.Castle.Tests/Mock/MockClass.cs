using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC.Castle
{
	public interface IMockSimple
	{
		void Say();
		string Name { get; set; }
	}

	public class MockSimple : IMockSimple
	{
		public void Say()
		{
			Console.WriteLine("Hello Castle Container");
		}

		public string Name { get; set; }
	}

	public interface IMockConstructorInject
	{
		void Depend();
	}

	public class MockConstructorInject : IMockConstructorInject
	{
		private IMockSimple mock1;

		public MockConstructorInject(IMockSimple mock1)
		{
			this.mock1 = mock1;
		}

		public void Depend()
		{
			Console.WriteLine(this.GetType().ToString());
			mock1.Say();
		}
	}

	public interface IMockPropertyInjection
	{
		void Say();
	}

	public class MockPropertyInjection : IMockPropertyInjection
	{
		public IMockSimple mock { get; set; }

		public void Say()
		{
			mock.Say();
		}
	}

}
