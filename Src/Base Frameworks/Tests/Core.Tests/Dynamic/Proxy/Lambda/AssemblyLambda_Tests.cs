using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	[TestClass]
	public class AssemblyLambda_Tests
	{
		[TestCategory("BVT Function"), TestMethod]
		public void AssemblyLambda_Constructor_Test()
		{
			IAssemblyLambda assembly = new AssemblyLambda();
		}

		[TestCategory("BVT Function"), TestMethod]
		public void AssemblyLambda_Assembly_Method_Test()
		{
			var assembly = new AssemblyLambda();
			var module = assembly.Assembly();

			assembly.Save();
		}
	}
}
