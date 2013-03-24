using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda.InTypes
{
	[TestClass]
	public class CodeLambda_Assign_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("메서드의 로컬 변수를 선언하여 Assign 하는 테스트, 오류가 발생하지 않으면 통과")]
		public void Assign_To_LocalOperand_Test()
		{
			var typeName        = Guid.NewGuid().ToString("N");
			var methodName1     = Guid.NewGuid().ToString("N");
			var methodName2     = Guid.NewGuid().ToString("N");
			var localFieldName1 = Guid.NewGuid().ToString("N");
			var localFieldName2 = Guid.NewGuid().ToString("N");

			var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Class(typeName);
					{
						var method1 = type.Public.Static.Method(methodName1);
						{
						    var local1 = method1.Local(typeof(int), localFieldName1);
						    var local2 = method1.Local(typeof(int), localFieldName2);

						    method1.AssignValue(local1, 255);
						    method1.Assign(local2, local1);

						    var consolewrite = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) });
						    method1.Call(consolewrite, local1);

						    method1.Return();
						};

						var method2 = type.Public.Static.Method(methodName2);
						{
							var local1 = method2.Local(typeof(string), localFieldName1);

							method2.AssignValue(local1, "Junil Um");

							var consolewrite = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });
							method2.Call(consolewrite, local1);

							method2.Return();
						};
					};

					var releaseType = type.ReleaseType();

					releaseType.GetMethod(methodName1).Invoke(null, null);
					releaseType.GetMethod(methodName2).Invoke(null, null);
				};
			};

			assembly.AssemblyLambda.Save();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("메서드에서 필드 맴버를 엑세스하는 테스트, 오류가 발생하지 않으면 통과")]
		public void Assign_To_FieldOperand_Test()
		{
			var typeName        = Guid.NewGuid().ToString("N");
			var methodName1     = Guid.NewGuid().ToString("N");
			var fieldName1		= Guid.NewGuid().ToString("N");

			var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Class(typeName);
					{
						var field1 = type.Public.Field(typeof(string), "field1");

						var method1 = type.Public.Method(methodName1);
						{
							method1.AssignValue(field1, "Junil Um");

							var consolewrite = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });
							method1.Call(consolewrite, field1);

							method1.Return();
						};
					};

					var releaseType = type.ReleaseType();

					var obj = Activator.CreateInstance(releaseType);
					releaseType.GetMethod(methodName1).Invoke(obj, null);
				};
			};

			assembly.AssemblyLambda.Save();
		}
	}
}
