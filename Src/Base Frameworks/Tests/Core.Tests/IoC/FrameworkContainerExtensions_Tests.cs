using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using Umc.Core.IoC.Configuration;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using Umc.Core.IoC.Unity;
using System.Xml;

namespace Umc.Core.IoC
{
	[TestClass]
	public class FrameworkContainerExtensions_Tests : UnitTestBase
	{

		
		[DependencyContract]
		internal class MockClass
		{
			public void Say()
			{
				Console.WriteLine("MockClass Say");
			}
		}
		
	}
}
