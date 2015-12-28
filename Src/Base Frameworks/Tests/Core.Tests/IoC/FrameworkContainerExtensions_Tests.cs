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
		
		[TestCategory("BVT Function"), TestMethod]
		public void LoadConfigurationFile_Test()
		{
			//FrameworkDependencyVisitor visitor = new FrameworkDependencyVisitor(typeof(MockClass).ToEnumerable());
			//var root = visitor.VisitTypes();

			//using(StreamWriter writer = new StreamWriter(".\\ioc.config"))
			//{
			//	XmlSerializer xs = new XmlSerializer(typeof(UmcCoreIoCElement));
			//	xs.Serialize(writer, root);
			//}

			//XDocument doc = XDocument.Load(".\\ioc.config");
			//var elements = doc.Descendants(GlobalConstants_Accessor.NAMESPACE_OF_NCSOFT_FRAMEWORK_IOC + GlobalConstants_Accessor.ELEMENT_OF_NCSOFT_FRAMEWORK_IOC);
			
			//XmlSerializer x = new XmlSerializer(typeof(UmcCoreIoCElement));
			//var r = x.Deserialize(new StringReader(elements.First().ToString())) as UmcCoreIoCElement;

			//r.Verify();

			//FrameworkContainerForUnity container = new FrameworkContainerForUnity();
			//FrameworkCompositionResolverForUnity resolver = new FrameworkCompositionResolverForUnity(container, r);
			//resolver.Compose();

			//container.Resolve<MockClass>().Say();

			//File.Delete(".\\ioc.config");
		}
	}
}
