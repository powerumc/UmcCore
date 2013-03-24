using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda.InTypes
{
	[TestClass]
	public class PropertyLambda_Tests
	{
		[TestMethod]
		public void Property_GetSet_Test()
		{
			var typeName = Guid.NewGuid().ToString("N");

			var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Public.Class(typeName);
					{
						type.Public.Property(typeof(string), "Name").GetSet();
					}

					type.ReleaseType();
				}
			}

			assembly.AssemblyLambda.Save();
		}
	}
}
