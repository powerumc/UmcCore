using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Umc.Core.IoC
{
	#region Mock PropertyInjection Classes
	internal interface IMockClass_Basic_Contract
	{
		void Say();
	}

	[DependencyContract(typeof(IMockClass_Basic_Contract))]
	internal class MockClass_Basic_Contract : IMockClass_Basic_Contract
	{
		public void Say()
		{
			Console.WriteLine("MockClass Say");
		}
	}



	[DependencyContract]
	internal class MockClass_PropertyInjection_DefaultValue
	{
		[DefaultValue("Junil, Um")]
		public string Name { get; set; }
		[DefaultValue(100)]
		public int Age { get; set; }
	}

	[DependencyContract]
	internal class MockClass_PropertyInjection_DependencyContract_DefaultValueProperty
	{
		[DependencyInjection(DefaultValue = "Junil, Um")]
		public string Name { get; set; }
		[DependencyInjection(DefaultValue = 100)]
		public int Age { get; set; }
	}




	[DependencyContract]
	internal class MockClass_PropertyInjection_By_Contract { public void Say() { Console.WriteLine("MockClass_PropertyInjection_Contract Say()"); } }

	[DependencyContract]
	internal class MockClass_PropertyInjection_By_Contract_Dependency
	{
		[DependencyInjection]
		public MockClass_PropertyInjection_By_Contract Contract { get; set; }

		public void Depend() { this.Contract.Say(); }
	}



	internal interface IMockClass_PropertyInjection_By_Key { string Key { get; } }

	[DependencyContract(typeof(IMockClass_PropertyInjection_By_Key), "a")]
	internal class MockClass_PropertyInjection_By_Key_a : IMockClass_PropertyInjection_By_Key
	{
		public string Key { get { return "a"; } }
	}

	[DependencyContract(typeof(IMockClass_PropertyInjection_By_Key), "b")]
	internal class MockClass_PropertyInjection_By_Key_b : IMockClass_PropertyInjection_By_Key
	{
		public string Key { get { return "b"; } }
	}




	internal interface IMockClass_PropertyInjection_By_Key_Type
	{
		IMockClass_PropertyInjection_By_Key_Type Contract { get; set; }
		void Say();
	}

	[DependencyContract(typeof(IMockClass_PropertyInjection_By_Key_Type), "a")]
	internal class MockClass_PropertyInjection_By_key_Type_a : IMockClass_PropertyInjection_By_Key_Type
	{
		public IMockClass_PropertyInjection_By_Key_Type Contract { get; set; }

		public void Say()
		{
			Console.WriteLine("MockClass_PropertyInjection_By_key_Type_a Key=A");
		}
	}


	[DependencyContract(typeof(IMockClass_PropertyInjection_By_Key_Type), "b")]
	internal class MockClass_PropertyInjection_By_Key_Type_b : IMockClass_PropertyInjection_By_Key_Type
	{
		[DependencyInjection("a")]
		public IMockClass_PropertyInjection_By_Key_Type Contract { get; set; }

		public void Say()
		{
			this.Contract.Say();
		}
	} 
	#endregion

	#region Mock ConstructorInjection Classes

	[DependencyContract]
	internal class MockClass_ConstructorInjection_By_DefaultValue
	{
		public string Name { get;set;}
		public int Age { get;set;}

		[DependencyInjection]
		public MockClass_ConstructorInjection_By_DefaultValue(
			[DefaultValue("Junil, Um")] string name,
			[DefaultValue(100)] int age)
		{
			this.Name = name;
			this.Age = age;

			Console.WriteLine("Name={0}, Age={1}", name, age);
		}
	}




	[DependencyContract]
	internal class MockClass_ConstructorInjection_By_DependencyInjection_DefaultValue
	{
		public string Name { get;set;}
		public int Age { get;set;}

		[DependencyInjection]
		public MockClass_ConstructorInjection_By_DependencyInjection_DefaultValue(
			[DependencyInjection(DefaultValue="Junil, Um")]string name,
			[DependencyInjection(DefaultValue=100)] int age)
		{
			this.Name = name;
			this.Age = age;

			Console.WriteLine("Name={0}, Age={1}", name, age);
		}
	}





	[DependencyContract]
	internal class MockClass_ConstructorInjection_ContractClass
	{
		public string Name { get { return "Junil, Um"; } }
	}

	[DependencyContract]
	internal class MockClass_ConstructorInjection_DependencyClass
	{
		public string Name { get; set; }
		[DependencyInjection]
		public MockClass_ConstructorInjection_DependencyClass(
			[DependencyInjection] MockClass_ConstructorInjection_ContractClass contract)
		{
			this.Name = contract.Name;
			Console.WriteLine(contract.Name);
		}
	}



	internal interface IMockClass_ConstructorInjection_By_Key { string Key { get; } string UniqueKey { get; set; } }

	[DependencyContract(typeof(IMockClass_ConstructorInjection_By_Key), "a")]
	internal class MockClass_ConstructorInjection_By_Key_a : IMockClass_ConstructorInjection_By_Key
	{
		public string Key { get { return "a"; } }
		public string UniqueKey { get; set; }
	}
	[DependencyContract(typeof(IMockClass_ConstructorInjection_By_Key), "b")]
	internal class MockClass_ConstructorInjection_By_key_b : IMockClass_ConstructorInjection_By_Key
	{
		public string Key { get { return "b"; } }
		public string UniqueKey { get; set; }

		[DependencyInjection]
		public MockClass_ConstructorInjection_By_key_b(
			[DependencyInjection("a")]IMockClass_ConstructorInjection_By_Key contract)
		{
			this.UniqueKey = contract.Key;
			Console.WriteLine(contract.Key);
		}
	}

	#endregion

	#region Mock MethodInjection Classes
	[DependencyContract]
	internal class MockClass_MethodInjection_By_DefaultValue
	{
		public string Name { get; set; }
		public int Age { get; set; }

		[DependencyInjection]
		public void Execute(
			[DefaultValue("Junil, Um")] string name,
			[DefaultValue(100)] int age
			)
		{
			this.Name = name;
			this.Age = age;
			Console.WriteLine("Name={0}, Age={1}", name, age);
		}
	}




	[DependencyContract]
	internal class MockClass_MethodInjection_By_DependencyInjection
	{
		public string Name { get; set; }
		public int Age { get; set; }

		[DependencyInjection]
		public void Execute(
			[DependencyInjection(DefaultValue="Junil, Um")] string name,
			[DependencyInjection(DefaultValue=100)] int age
			)
		{
			this.Name = name;
			this.Age = age;
			Console.WriteLine("Name={0}, Age={1}", name, age);
		}
	}



	[DependencyContract]
	internal class MockClass_MethodInjection_By_Contract
	{
		public string Key { get { return "Contract"; } }
	}

	[DependencyContract]
	internal class MockClass_MethodInjection_By_Dependency
	{
		public string Key { get; set; }

		[DependencyInjection]
		public void Execute(
			[DependencyInjection] MockClass_MethodInjection_By_Contract contract
			)
		{
			this.Key = contract.Key;
			Console.WriteLine("Contract Key="+contract.Key);
		}
	}


	internal interface IMockClass_MethodInjection_By_Key { string Key { get; set; } }

	[DependencyContract(typeof(IMockClass_MethodInjection_By_Key), "a")]
	internal class MockClass_MethodInjection_By_Key_a : IMockClass_MethodInjection_By_Key
	{
		public MockClass_MethodInjection_By_Key_a() { this.Key = "a"; }
		public string Key { get; set; }
	}

	[DependencyContract(typeof(IMockClass_MethodInjection_By_Key), "b")]
	internal class MockClass_MethodInjection_By_Key_b : IMockClass_MethodInjection_By_Key
	{
		public MockClass_MethodInjection_By_Key_b() { this.Key = "b"; }
		public string Key { get; set; }

		[DependencyInjection]
		public void Execute(
			[DependencyInjection("a")] IMockClass_MethodInjection_By_Key contract)
		{
			this.Key = contract.Key;
			Console.WriteLine("ContractKey="+contract.Key);
		}
	}




	#endregion
}
