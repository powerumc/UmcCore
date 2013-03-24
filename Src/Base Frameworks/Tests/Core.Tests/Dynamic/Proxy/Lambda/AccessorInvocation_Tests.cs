using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	[TestClass]
	public class AccessorInvocation_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("AccessorInvocation 의 엑세스 한정자에 대한 테스트, 테스트 성공/실패 조건을 통과하는 성공")]
		public void Invoke_Test()
		{
			var assembly = new AssemblyLambda();
			{
				var module = assembly.Assembly("A").Module("A");
				{
					var type = module.Class(Guid.NewGuid().ToString("N"));
					
					TypeAccessor typeAccessor = new TypeAccessor(type);
					MethodAccessor methodAccessor = new MethodAccessor(type);
					FieldAccessor fieldAccessor = new FieldAccessor(type);

					AccessorInvocation invocation = new AccessorInvocation(type, typeAccessor, methodAccessor, fieldAccessor);

					var t = invocation.Public;
					t = invocation.Abstract;
					t = invocation.Abstract;

					TestContext.WriteLine("TypeAttribute is {0}", typeAccessor.TypeAttributes.ToString());
					TestContext.WriteLine("MethodAttribute is {0}", methodAccessor.MethodAttribute.ToString());
					TestContext.WriteLine("FieldAttribute is {0}", fieldAccessor.FieldAttribute.ToString());


					// Assert Type
					Assert.IsTrue((typeAccessor.TypeAttributes & TypeAttributes.Class) == TypeAttributes.Class);
					Assert.IsTrue((typeAccessor.TypeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract);
					Assert.IsFalse((typeAccessor.TypeAttributes & TypeAttributes.Interface) == TypeAttributes.Interface);

					// Assert Method
					Assert.IsTrue((methodAccessor.MethodAttribute & MethodAttributes.Public) == MethodAttributes.Public);
					Assert.IsTrue((methodAccessor.MethodAttribute & MethodAttributes.Abstract) == MethodAttributes.Abstract);
					Assert.IsFalse((methodAccessor.MethodAttribute & MethodAttributes.Virtual) == MethodAttributes.Virtual);

					// Assert Field
					Assert.IsTrue((fieldAccessor.FieldAttribute & FieldAttributes.Public) == FieldAttributes.Public);
					Assert.IsFalse((fieldAccessor.FieldAttribute & FieldAttributes.Static) == FieldAttributes.Static);
				};
			};
		}
	}
}
