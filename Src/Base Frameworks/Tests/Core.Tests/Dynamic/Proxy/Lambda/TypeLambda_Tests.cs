using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection.Emit;
using Umc.Core.Testing.UnitTest;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	[TestClass]
	public class TypeLambda_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 개체가 올바르게 생성되는지 기본적인 테스트, 오류가 발생하지 않으면 통과")]
		public void TypeLambda_Constructor_Test()
		{
			var type = new AssemblyLambda().Assembly().Module().Public.Class("DynamicType");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 메서드가 올바르게 생성이 되는지 테스트, 오류가 발생하지 않으면 통과")]
		public void TypeLambda_Define_Method()
		{
			var typeName = Guid.NewGuid().ToString("N");
            var methodName = Guid.NewGuid().ToString("N");

            var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Public.Class(typeName);
					{
						var constructor1 = type.Public.Constructor();
						{
							constructor1.Emit.EmitWriteLine("This is constructor");
							constructor1.Return();
						}
						
						var constructor2 = type.Private.Static.Constructor();
						{
							constructor2.Emit.EmitWriteLine("This is (private static) constructor");
							constructor2.Return();
						}

						var method = type.Public.Static.Method(methodName);
						{
							method.Emit.EmitWriteLine("This is emitted writeline");
							method.Return();
						};
					};

					var releaseType = type.ReleaseType();

					var obj = System.Activator.CreateInstance(releaseType);

					TestContext.WriteLine("Release type is {0}", releaseType.AssemblyQualifiedName);

					var releaseMethod = releaseType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
					TestContext.WriteLine("Release method is {0}", releaseMethod.Name);

					releaseType.GetMethod(methodName).Invoke(null, null);

					Assert.IsTrue(releaseType.IsClass);
					Assert.IsTrue(releaseMethod != null);
				}
			};
		}		

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 구조체가 올바르게 생성되는지 테스트, 오류가 발생하지 않으면 통과")]
		public void TypeLambda_Define_Struct()
		{
			var typeName = Guid.NewGuid().ToString("N");

            var module = new AssemblyLambda().Assembly().Module();
			{
				var type = module.Public.Struct(typeName);
				{
				};

				var releaseType = type.ReleaseType();

				TestContext.WriteLine(releaseType.AssemblyQualifiedName);

				Assert.IsTrue(typeName == releaseType.Name);
			};
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 인터페이스가 올바르게 생성되는지 테스트, 오류가 발생하지 않으면 통과")]
		public void TypeLambda_Define_Interface()
		{
			var typeName = Guid.NewGuid().ToString("N");

            var module = new AssemblyLambda().Assembly().Module();
			{
				var type = module.Public.Interface(typeName);
				{
				};

				var releaseType = type.ReleaseType();

				TestContext.WriteLine(releaseType.AssemblyQualifiedName);

				Assert.IsTrue(releaseType.IsInterface);
				Assert.IsTrue(typeName == releaseType.Name);
			};
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 열거형 타입이 올바르게 생성되었는지 테스트, 오류가 발생하지 않으면 통과")]
		public void TypeLambda_Define_Enum()
		{
			var typeName = Guid.NewGuid().ToString("N");

            var module = new AssemblyLambda().Assembly().Module();
			{
				var type = module.Public.Enum(typeName);
				{
					type.DefineLiteral("A", 0);
					type.DefineLiteral("B", 1);
				};

				var releaseType = type.ReleaseType();

				TestContext.WriteLine(releaseType.AssemblyQualifiedName);

				Assert.IsTrue(releaseType.IsEnum);
				Assert.IsTrue(releaseType.GetFields().Count() == 3); // value__ SpecialName 까지 포함해서 3개여야 함
				Assert.IsTrue(typeName == releaseType.Name);
			};
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 타입의 기본 생성자가 없는 타입 테스트, 오류가 발생하지 않으면 통과")]
		public void TypeLambda_Define_Type_Of_Non_Default_Constructor()
		{
			var typeName  = Guid.NewGuid().ToString("N");
			var fieldName = Guid.NewGuid().ToString("N");

			var assembly = new AssemblyLambda().Assembly();
			{
			    var module = assembly.Module();
			    {
			        var type = module.Public.Class(typeName);
			        {
						var field1 = type.Public.Field(typeof(string), fieldName);
			            var con1 = type.Public.Constructor( new ParameterCriteriaMetadataInfo[] { new ParameterCriteriaMetadataInfo(typeof(string), "name") }.AsEnumerable());
			        }

					var releaseType = type.ReleaseType();

					var constructor = releaseType.GetConstructors();
					Assert.IsTrue(constructor.Length == 1, "기본 생성자 없이 1개의 파라메터를 받는 생성자만 존재해야 하는데, 존재하지 않습니다");
					Assert.IsTrue(constructor[0].GetParameters()[0].Name == "name", "생성자의 파라메터 이름이 'name' 인데 파라미터명이 존재하지 않습니다");

					
			    }
			}

			assembly.AssemblyLambda.Save();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("동적 엔티티 타입을 생성하는 테스트, 오류가 발생하지 않으면 통과")]
		public void TypeLambda_Creation_Entity()
		{
			var typeName = Guid.NewGuid().ToString("N");

			var assembly = new AssemblyLambda().Assembly();
			{
				var module = assembly.Module();
				{
					var type = module.Public.Class(typeName);
					{
						var constructor = type.Constructor();
						{
							//constructor.Emit.Emit()
						}

						var property1 = type.Property(typeof(string), "Property1");
						{
						}
					}
				}
			}
		}
	}
}
