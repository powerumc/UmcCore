using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection.Emit;
using System.Reflection;

namespace Umc.Core.Reflection.Emit
{
	[TestClass]
	public class Builder_Tests
	{
		[TestCategory("BVT Function"), TestMethod]
		public void TestMethod1()
		{
			var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("DynamicAssembly"), AssemblyBuilderAccess.RunAndSave);
			var module   = assembly.DefineDynamicModule("DynamicModule");
			var type     = module.DefineType("DynamicType", TypeAttributes.Public);

			var method = type.DefineMethod("DynamicMethod", MethodAttributes.Public | MethodAttributes.Static);
			var il     = method.GetILGenerator();

			il.Emit(OpCodes.Ldstr, "Hello World");
			il.Emit(OpCodes.Call, typeof(System.Console).GetMethod("WriteLine", new Type[] { typeof(string) }));

			il.Emit(OpCodes.Ret);

			var dynamicType = type.CreateType();

			var dynamicMethod = dynamicType.GetMethod("DynamicMethod");
			dynamicMethod.Invoke(null, null);



			dynamicMethod.GetMethodBody().GetILAsByteArray().ToList().ForEach(o => Console.WriteLine(o));
		}
	}
}
