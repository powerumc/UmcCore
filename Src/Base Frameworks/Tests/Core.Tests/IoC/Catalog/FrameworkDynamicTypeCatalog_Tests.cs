using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Dynamic;
using Umc.Core.Testing.UnitTest;
using System.Reflection;

namespace Umc.Core.IoC.Catalog
{
	[TestClass]
	public class DependencyDynamicTypeCatalog_Tests : UnitTestBase
	{
		[DynamicAttribute]
		public interface DynamicInterface
		{
		}

		[TestMethod]
		public void DynamicTypeCatalog_Test()
		{
			var inputTypes = new Type[]
			{
				typeof(DynamicInterface)
			};

			var catalog = new FrameworkTypeCatalog(inputTypes);
			var types = catalog.GetMatchingTypes();

			foreach ( var type in types )
			{
				TestContext.WriteLine(type.AssemblyQualifiedName);
			}

			Assert.IsTrue(types.Count() > 0);
		}
	}
}
