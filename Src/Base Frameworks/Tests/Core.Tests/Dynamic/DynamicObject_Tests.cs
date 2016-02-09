using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.IoC;
using Umc.Core.IoC.Catalog;
using System.Reflection;
using Umc.Core.IoC.Configuration;
using Umc.Core.IoC.Unity;
using System.Xml.Serialization;

namespace Umc.Core.Dynamic
{
	[TestClass]
	public class DynamicObject_Tests
	{
		[TestMethod]
		public void DynamicEntity_Test()
		{
			var type = DynamicObject.InterfaceImplementationType<IEntity>();
			Console.WriteLine(type.AssemblyQualifiedName);
		}

		[TestMethod]
		public void DynamicEntity_With_FrameworkContainer_Test()
		{
			var type = DynamicObject.InterfaceImplementationType<IEntity>();
			var obj  = (IEntity)Activator.CreateInstance(type);

			IFrameworkContainer container = new FrameworkContainerForUnity();

			container.RegisterType<IEntity>(type, LifetimeFlag.PerCall);

			Console.WriteLine(obj.GetType());

			var data = container.Resolve<IEntity>();
			data.Name = "Junil, Um";
			data.Age = 10;
			Console.WriteLine("Name={0}, Age={1}", data.Name, data.Age);

			data = container.Resolve<IEntity>();
			Console.WriteLine("Name={0}, Age={1}", data.Name ?? "NULL", data.Age);
		}

		[TestMethod]
		public void FrameworkContainerComposition_with_DynamicAttribute_Test()
		{
			var container = new FrameworkContainerForUnity();

			var catalog = new FrameworkTypeCatalog(new Type[] { typeof(IEntity), typeof(Entity), typeof(DynamicObject_Tests), typeof(Unity_Component_Tests) });
			var visitor = new FrameworkDependencyVisitor(catalog);

			

			var root = visitor.VisitTypes();
			var composite = new FrameworkCompositionResolverForUnity(container, root);
			composite.Compose();

			//XmlSerializer xs = new XmlSerializer(typeof(UmcCoreIoCElement));
			//xs.Serialize(Console.Out, root);

			var entity = container.Resolve<IEntity>();
			entity.Name = "Junil, Um";
			Console.WriteLine("Name={0}", entity.Name);

			entity = container.Resolve<IEntity>();
			Console.WriteLine("Name={0}", entity.Name);
		}
	}

	[Dynamic]
	public interface IEntity
	{
		string Name { get; set; }
		int Age { get; set; }
	}

	public class Entity : IEntity
	{
		private string _name;
		private int _age;
		#region IEntity 멤버

		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		public int Age
		{
			get { return this._age; }
			set { this._age = value; }
		}

		#endregion
	}
}
