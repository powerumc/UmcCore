using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.IoC.Configuration;
using Umc.Core.Testing.UnitTest;
using System.Xml.Serialization;

namespace Umc.Core.IoC.Unity
{
	[TestClass]
	public class FrameworkCompositionResolverForUnity_Tests : UnitTestBase
	{
		private void WriteToSerialization(object root)
		{
			XmlSerializer xs = new XmlSerializer(typeof(UmcCoreIoCElement));
			xs.Serialize(Console.Out, root);
		}

		private FrameworkContainerForUnity CompositionTheTypes(Type[] types)
		{
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(types);
			var root = visitor.VisitTypes();

			WriteToSerialization(root);

			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			FrameworkCompositionResolverForUnity resolver = new FrameworkCompositionResolverForUnity(container, root);

			resolver.Compose();
			return container;
		}


		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity 의 Contract/Dependency 메서드 호출 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkCompositionResolverForUnity_Basic_Contract_Test()
		{
			var types = new Type[]
			{
				typeof(MockClass_Basic_Contract)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);

			container.Resolve<IMockClass_Basic_Contract>().Say();
		}

		#region PropertyInjection Test
		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 DefaultValueAttribute 을 통해 ProertyInjection 테스트, 오류가 발생하지 않고 DefaultValue 값과 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_PropertyInjection_By_DefaultValue()
		{
			FrameworkContainerForUnity container = CompositionTheTypes(typeof(MockClass_PropertyInjection_DefaultValue).ToEnumerable().ToArray());

			var name = container.Resolve<MockClass_PropertyInjection_DefaultValue>().Name;
			var age = container.Resolve<MockClass_PropertyInjection_DefaultValue>().Age;
			TestContext.WriteLine("Name={0}", name);
			TestContext.WriteLine("Age={0}", age);

			Assert.IsTrue(name == "Junil, Um", "Name 프로퍼티의 값이 선언된 DefaultValueAttribute 의 값과 달라서 오류가 발생");
			Assert.IsTrue(age == 100, "Age 프로퍼티의 값이 선언된 DefaultValueAttribute 의 값과 달라서 오류가 발생");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 DependencyInjectionAttribute.DefaultValue를 통해 PropertyInjection 테스트, 오류가 발생하지 않고 DefaultValue 값과 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_PropertyInjection_By_DependencyInjection_DefaultValue()
		{
			FrameworkContainerForUnity container = CompositionTheTypes(typeof(MockClass_PropertyInjection_DependencyContract_DefaultValueProperty).ToEnumerable().ToArray());

			var name = container.Resolve<MockClass_PropertyInjection_DependencyContract_DefaultValueProperty>().Name;
			var age = container.Resolve<MockClass_PropertyInjection_DependencyContract_DefaultValueProperty>().Age;
			TestContext.WriteLine("Name={0}", name);
			TestContext.WriteLine("Age={0}", age);

			Assert.IsTrue(name == "Junil, Um", "Name 프로퍼티의 값이 선언된 DependencyInjectionAttribute.DefaultValue 의 값과 달라서 오류가 발생");
			Assert.IsTrue(age == 100, "Age 프로퍼티의 값이 선언된 DependencyInjectionAttribute.DefaultValue 의 값과 달라서 오류가 발생");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 계약 타입의 PropertyInjection테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkCompositionResolverForUnity_PropertyInjection_By_Contract_Dependency()
		{
			var types = new Type[]
			{
				typeof(MockClass_PropertyInjection_By_Contract),
				typeof(MockClass_PropertyInjection_By_Contract_Dependency)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);

			container.Resolve<MockClass_PropertyInjection_By_Contract_Dependency>().Depend();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 프로퍼티 계약 키 값 별로 값을 가져오는지 테스트, 각 키 값이 일치하면 통과")]
		public void FrameworkCompositionResolverForUnity_PropertyInjection_By_Key()
		{
			var types = new Type[]
			{
				typeof(MockClass_PropertyInjection_By_Key_a),
				typeof(MockClass_PropertyInjection_By_Key_b)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);

			var keya = container.Resolve<IMockClass_PropertyInjection_By_Key>("a").Key;
			var keyb = container.Resolve<IMockClass_PropertyInjection_By_Key>("b").Key;

			TestContext.WriteLine("Key A = " + keya);
			TestContext.WriteLine("Key B = " + keyb);

			Assert.IsTrue( keya == "a", "MockClass_PropertyInjection_By_Key_a 클래스와 계약된 키 값은 a인데, 값이 a가 아니어서 오류");
			Assert.IsTrue( keyb == "b", "MockClass_PropertyInjection_By_Key_b 클래스와 계약된 키 값은 b인데, 값이 b가 아니어서 오류");
		}


		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 서로 다른 계약 키 값의 프로퍼티에 DependencyInjection으로 다른 키 계약을 가져오는 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkCompositionResolverForUnity_PropertyInjection_By_Key_Type()
		{
			var types = new Type[]
			{
				typeof(MockClass_PropertyInjection_By_key_Type_a),
				typeof(MockClass_PropertyInjection_By_Key_Type_b)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);

			container.Resolve<MockClass_PropertyInjection_By_Key_Type_b>("b").Say();
		}
		#endregion

		#region ConstructorInjection Test
		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 DefaultValue를 이용한 ConstructorInjection 테스트, 오류가 발생하지 않고 Name,Age 값이 초기값과 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_ConstructorInjection_By_DefaultValue()
		{
			var types = new Type[]
			{
				typeof(MockClass_ConstructorInjection_By_DefaultValue)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);

			var obj = container.Resolve<MockClass_ConstructorInjection_By_DefaultValue>();

			Assert.IsTrue(obj.Name == "Junil, Um", "MockClass_ConstructorInjection_By_DefaultValue.Name 값이 DefaultValue 값과 달라서 오류");
			Assert.IsTrue(obj.Age == 100, "MockClass_ConstructorInjection_By_DefaultValue.Age 가 DefaultValue 값과 달라서 오류");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 DependencyInjection.DefaultValue를 이용한 ConstructorInjection 테스트, 오류가 발생하지 않고 Name,Age 값이 초기값과 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_ConstructorInjection_By_DependencyInjection_DefaultValue()
		{
			var types = new Type[]
			{
				typeof(MockClass_ConstructorInjection_By_DependencyInjection_DefaultValue)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);

			var obj = container.Resolve<MockClass_ConstructorInjection_By_DependencyInjection_DefaultValue>();

			Assert.IsTrue(obj.Name == "Junil, Um", "MockClass_ConstructorInjection_By_DefaultValue.Name 값이 DefaultValue 값과 달라서 오류");
			Assert.IsTrue(obj.Age == 100, "MockClass_ConstructorInjection_By_DefaultValue.Age 가 DefaultValue 값과 달라서 오류");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 계약된 ConstructorInjection을 이용한 테스트, 오류가 발생하지 않고 값이 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_ConstructorInjection_By_Contract_and_Dependency_Injection()
		{
			var types = new Type[]
			{
				typeof(MockClass_ConstructorInjection_ContractClass),
				typeof(MockClass_ConstructorInjection_DependencyClass)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);
			var obj = container.Resolve<MockClass_ConstructorInjection_DependencyClass>();

			Assert.IsTrue(obj.Name == "Junil, Um", "ConstructorInjection 이 올바르게 되지 않아 값이 틀려서 오류가 발생");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 계약된 키값을 이용한 Injection 테스트, 오류가 발생하지 않고, Unique값이 주입된 키값이 a이면 통과")]
		public void FrameworkCompositionResolverForUnity_ConstructorInjection_By_Key()
		{
			var types = new Type[]
			{
				typeof(MockClass_ConstructorInjection_By_Key_a),
				typeof(MockClass_ConstructorInjection_By_key_b)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);
			var obj = container.Resolve<MockClass_ConstructorInjection_By_key_b>("b");

			Assert.IsTrue(obj.UniqueKey=="a", "ConstructorInjection된 키가 a인데 key 값이 a가 아니어서 오류가 발생");
		}

		#endregion

		#region MethodInjection Test
		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 MethodInjection을 DefaultValue로 수행하여 테스트, 오류가 발생하지 않고 Name,Age 값이 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_MethodInjection_By_DefaultValue()
		{
			var types = new Type[]
			{
				typeof(MockClass_MethodInjection_By_DefaultValue)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);
			var obj = container.Resolve<MockClass_MethodInjection_By_DefaultValue>();

			Assert.IsTrue(obj.Name=="Junil, Um", "MethodInjection의 DefaultValue로 초기화된 값이 달라서 오류가 발생");
			Assert.IsTrue(obj.Age==100, "MethodInjection의 DefaultValue로 초기화된 값이 달라서 오류가 발생");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 DependencyInjectionAttribute.DefaultValue로 수행하여 테스트, 오류가 발생하지 않고 Name,Age 값이 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_MethodInjection_By_DependencyInjection()
		{
			var types = new Type[]
			{
				typeof(MockClass_MethodInjection_By_DependencyInjection)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);
			var obj = container.Resolve<MockClass_MethodInjection_By_DependencyInjection>();

			Assert.IsTrue(obj.Name == "Junil, Um", "MethodInjection의 DefaultValue로 초기화된 값이 달라서 오류가 발생");
			Assert.IsTrue(obj.Age == 100, "MethodInjection의 DefaultValue로 초기화된 값이 달라서 오류가 발생");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 계약을 MethodInjection 수행 테스트, 오류가 발생하지 않고 계약된 값과 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_MethodInjection_By_Injection()
		{
			var types = new Type[]
			{
				typeof(MockClass_MethodInjection_By_Contract),
				typeof(MockClass_MethodInjection_By_Dependency)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);
			var obj = container.Resolve<MockClass_MethodInjection_By_Dependency>();

			Assert.IsTrue( obj.Key == "Contract", "Execute메서드에 Injection된 값이 틀려서 오류가 발생");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkCompositionResolverForUnity의 계약된 MethodInjection을 키값으로 주입하는 테스트, 오류가 발생하지 않고 키 값이 동일하면 통과")]
		public void FrameworkCompositionResolverForUnity_MethodInjection_By_Key()
		{
			var types = new Type[]
			{
				typeof(MockClass_MethodInjection_By_Key_a),
				typeof(MockClass_MethodInjection_By_Key_b)
			};

			FrameworkContainerForUnity container = CompositionTheTypes(types);
			var obj = container.Resolve<MockClass_MethodInjection_By_Key_b>("b");

			Assert.IsTrue(obj.Key == "a", "MethodInjection된 키값이 a여야 하는데 키 값이 달라서 오류가 발생");
		}
		#endregion
	}
}
