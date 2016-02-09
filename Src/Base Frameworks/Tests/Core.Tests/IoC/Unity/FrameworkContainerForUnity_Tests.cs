using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Umc.Core.IoC;
using Umc.Core.IoC.Catalog;
using Umc.Core.IoC.Configuration;

namespace Umc.Core.IoC.Unity
{
	[TestClass]
	public class FrameworkContainerForUnity_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]		
		[Description("FrameworkContainer 에 하나의 객체를 등록한 후 호출한다. 오류가 발생하지 않으면 통과")]
		public void Create_ROOT_Container_Of_One_Object_Test()
		{
			IFrameworkContainer container = new FrameworkContainerForUnity();
			container.RegisterType<IMockSimple, MockSimple>();
			
			container.Resolve<IMockSimple>().Say();
		}

		[TestCategory("BVT Function"), TestMethod]
		
		[Description("FrameworkContainer 에 두 개의 객체를 등록한 후 호출한다. 오류가 발생하지 않으면 통과")]
		public void Create_ROOT_Container_Of_Two_Object_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			container.RegisterType<IMockSimple, MockSimple>();
			container.RegisterType<IMockConstructor, MockConstructor>();

			container.Resolve<IMockSimple>().Say();
			container.Resolve<IMockConstructor>().Delegate();

		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainer 에 FrameworkChildContainer 를 등록한 후 각각 객체 하나씩을 등록한 후 호출한다. 오류가 발생하지 않으면 통과")]
		public void Create_ROOT_and_Child_Container_test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			container.RegisterType<IMockSimple, MockSimple>();

			FrameworkContainerForUnityChild childContainer = new FrameworkContainerForUnityChild("child1", container);
			container.AddChildContainer(childContainer);

			childContainer.RegisterType<IMockConstructor, MockConstructor>();

			container.Resolve<IMockSimple>();
			container.Childs.FirstOrDefault().Resolve<IMockConstructor>().Delegate();			
		}

		[TestCategory("BVT Function"), TestMethod]
		
		[Description("FrameworkContainer 에 두 개의 FrameworkChildContainer 를 등록하였다. 오류가 발생하지 않으면 통과")]
		public void Create_ROOT_and_Two_Childs_Container_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			
			FrameworkContainerForUnityChild childContainer1 = new FrameworkContainerForUnityChild("Child1", container);
			FrameworkContainerForUnityChild childContainer2 = new FrameworkContainerForUnityChild("Child2", container);

			Assert.IsTrue( container.Childs.Count() == 2, "FrameworkContainer 에 두 개의 Child Container 를 등록하였지만, FrameworkContainer Child Count 는 {0} 개 입니다.", container.Childs.Count());
		}

		[TestCategory("BVT Function"), TestMethod]
		
		[Description("FrameworkContainer 에 두 개의 동일한 키의 FrameworkChildContainer 를 동록하였다. ArgumentException 이 발생하면 통과")]
		[ExpectedException(typeof(ArgumentException))]
		public void Create_ROOT_And_Two_Childs_Container_But_Regist_Same_Child_Container_Key_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();

			FrameworkContainerForUnityChild childContainer1 = new FrameworkContainerForUnityChild("Child1", container);
			FrameworkContainerForUnityChild childContainer2 = new FrameworkContainerForUnityChild("Child1", container);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("RegisterType<,> 와 Resolve<> 메서드 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkContainer_Register_and_Resolve_Methods_Overrode1_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			
			container.RegisterType<IMockSimple, MockSimple>();
			container.Resolve<IMockSimple>().Say();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("RegisterType<> 와 Resolve<> 메서드 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkContainer_Register_and_Resolve_Methods_Overrode2_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();

			container.RegisterType<MockSimple>();
			container.Resolve<MockSimple>().Say();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("RegiserType<,>(key) 와 Resolve<>(key) 메서드 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkContainer_Register_and_Resolve_Methods_Overrode3_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();

			container.RegisterType<IMockSimple, MockSimple>("key");
			container.Resolve<IMockSimple>("key").Say();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("RegisterType<,>(key) 와 Resolve<> 메서드 테스트, 키가 없는 RegisterType 이므로 ResolutionFailedException 예외가 발생해야 통과")]
		[ExpectedException(typeof(Microsoft.Practices.Unity.ResolutionFailedException))]
		public void FrameworkContainer_Register_and_Resolve_Methods_Override4_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();

			container.RegisterType<IMockSimple, MockSimple>("key");
			container.Resolve<IMockSimple>().Say();
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("RegisterType<,> 과 리턴 타입이 Object 인 Resolve() 메서드 테스트, obj is IMock1 이 참이어야 통과")]
		public void FrameworkContainer_Register_and_Resolve_Methods_Override5_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();

			container.RegisterType<IMockSimple, MockSimple>();
			var obj = container.Resolve(typeof(IMockSimple));

			Assert.IsTrue(obj is IMockSimple, "Resolve된 Object 가 등록된 IMock 타입과 다르므로 실패하였습니다");
		}













		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainer 에 Singleton으로 객체를 등록한 후, 다시 꺼냈을 때 Singleton 객체가 맞는지 검사, 객체의 값이 변하지 않으면 통과")]
		public void FrameworkContainer_Register_with_Static_Lifetime_Test1()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			container.RegisterType<IMockSimple, MockSimple>(LifetimeFlag.Singleton);

			var obj = container.Resolve<IMockSimple>();
			Assert.IsTrue(obj.Name == null);

			obj.Name = "엄준일";

			obj = container.Resolve<IMockSimple>();
			Assert.IsTrue(obj.Name == "엄준일");
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainer 에 PerCall 으로 객체를 등록한 후, 다시 꺼냈을 때 PerCall 객체가 맞는지 검사, 객체의 값이 초기값이면 통과")]
		public void FrameworkContainer_Register_with_PerCall_Lifetime_Test1()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			container.RegisterType<IMockSimple, MockSimple>(LifetimeFlag.PerCall);

			var obj = container.Resolve<IMockSimple>();
			Assert.IsTrue(obj.Name == null);

			obj.Name = "엄준일";

			obj = container.Resolve<IMockSimple>();
			Assert.IsTrue(obj.Name == null);
		}





		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainer 에서 Injection되지 않는 생성자 파라미터를 사용한 경우 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkContainer_Resolve_Constructor_Contract_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			container.RegisterType<IMockInitConstructor, MockInitConstructor>();

			container.Resolve<IMockInitConstructor>().Say();
		}


		[TestCategory("BVT Function"), TestMethod]
		[Description("FrameworkContainerForUnity 의 Property Injection 이 성공하는지 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkContainer_PropertyInjection_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			container.RegisterType<IMockSimple, MockSimple>()
				.RegisterType<IMockPropertyInjection, MockPropertyInjection>();

			container.Resolve<IMockPropertyInjection>().Say();
		}



		[TestCategory("BVT Function"), TestMethod]
		[Description("Config 파일에서 종속성 설정을 불러오는 테스트, 객체가 올바르게 반환되면 통과")]
		public void Save_and_Load_FrameworkConfiguratoinExtension_Test()
		{
			string filename = "configuration.ioc.config";

			Type[] mockTypes = new Type[]
			{
				typeof(MockSimple),
			};

			FrameworkTypeCatalog catalog = new FrameworkTypeCatalog(mockTypes);
			FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(catalog);
			var root = visitor.VisitTypes();

			XmlSerializer xs = new XmlSerializer(typeof(UmcCoreIoCElement));
			using (StreamWriter sw = new StreamWriter(filename))
			{
				xs.Serialize(sw, root);
			}

			TestContext.WriteLine(File.ReadAllText(filename));


			IFrameworkContainer newContainer = new FrameworkContainerForUnity();
			newContainer.Load(filename);

			TestContext.WriteLine(newContainer.Resolve<IMockSimple>().GetHashCode().ToString());
		}

#if NET40
		[TestCategory("BVT Function"), TestMethod]
		[Description("객체의 Composition 또는 등록을 병렬로 처리하는 테스트, 오류가 발생하지 않으면 통과")]
		public void FrameworkContainer_Registering_With_Parallal_Test()
		{
			FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			Action a1 = () => container.RegisterType<IMockSimple, MockSimple>();
			Action a2 = () => container.RegisterType<IMockConstructor, MockConstructor>();

			List<Action> actions = new List<Action>() { a1, a2 };

			container.RegisterTypeAsParallel(actions);

			container.Resolve<IMockSimple>().Say();
			container.Resolve<IMockConstructor>().Delegate();
		}
#endif

	}
}
