using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsoft.Managed.Framework.IoC;

namespace NCsoft.Managed.Framework.Dynamic
{
	[TestClass]
	public class Dynamic_Tests
	{
		[TestMethod]
		public void DynamicEntity_Test()
		{
			var type = Dynamic.InterfaceImplementationType<IEntity>();
			Console.WriteLine(type.AssemblyQualifiedName);
		}

		[TestMethod]
		public void DynamicEntity_With_FrameworkContainer_Test()
		{
			var type = Dynamic.InterfaceImplementationType<IEntity>();
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
	}

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
