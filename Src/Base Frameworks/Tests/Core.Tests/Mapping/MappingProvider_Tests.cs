using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using System.Data;

namespace Umc.Core.Mapping
{
	[TestClass]
	public class MappingProvider_Tests : UnitTestBase
	{
		internal class MockPerson
		{
			public string Name { get; set; }
			public int Age { get; set; }
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MappingProviderForProperty 프로바이더의 Getter, Setter 테스트, 값이 유효하면 통과")]
		public void MappingProperty()
		{
			MockPerson person = new MockPerson() { Name = "NCsoft 1", Age = 11 };

			MappingProviderForProperty mapping = new MappingProviderForProperty(person);

			mapping.Setter("Name", "Junil, Um");
			mapping.Setter("Age", 255);
			var obj1 = mapping.Getter("Name");

			Console.WriteLine(obj1);
			Console.WriteLine(person.Name);

			Assert.AreEqual("Junil, Um", mapping.Getter("Name"));
			Assert.AreEqual(255, mapping.Getter("Age"));
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MappingProviderForDataRow 프로바이더의 Getter, Setter 테스트, 값이 유효하면 통과")]
		public void MappingDataRow()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Name", typeof(string));
			dt.Columns.Add("Age", typeof(int));
			dt.Rows.Add("NCsoft 1", 11);

			MappingProviderForDataRow mapping = new MappingProviderForDataRow(dt.Rows[0]);
			TestContext.WriteLine(mapping.Getter("Name").ToString());
			Assert.AreEqual("NCsoft 1", mapping.Getter("Name"));
			Assert.AreEqual(11, mapping.Getter("Age"));

			mapping.Setter("Name", "Junil, Um");
			TestContext.WriteLine(mapping.Getter("Name").ToString());
			Assert.AreEqual("Junil, Um", mapping.Getter("Name"));
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MappingProvider 로 Property->DataRow 로 매핑 테스트, 값이 유효하면 통과")]
		public void Mapping_Property_Assign_To_DataRow()
		{
			// Source
			MockPerson source = new MockPerson() { Name = "NCsoft 1", Age = 11 };

			// Target
			DataTable target = new DataTable();
			target.Columns.Add("Name", typeof(string));
			target.Columns.Add("Age", typeof(int));

			MappingProviderForProperty mapping1 = new MappingProviderForProperty(source);
			MappingProviderForDataRow mapping2 = new MappingProviderForDataRow(target);

			mapping1.AssignTo(mapping2);

			var row = target.Rows[0];
			TestContext.WriteLine("Name:{0}, Age:{1}", row["Name"], row["Age"]);

			Assert.AreEqual("NCsoft 1", row["Name"]);
			Assert.AreEqual(11, row["Age"]);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MappingProvider 로 DataRow->Property 로 매핑 테스트, 값이 유효하면 통과")]
		public void Mapping_DataRow_Assign_To_Property()
		{
			DataTable source = new DataTable();
			source.Columns.Add("Name", typeof(string));
			source.Columns.Add("Age", typeof(int));
			source.Rows.Add("NCsoft 1", 11);

			MockPerson target = new MockPerson();

			MappingProviderForDataRow mapping1 = new MappingProviderForDataRow(source.Rows[0]);
			MappingProviderForProperty mapping2 = new MappingProviderForProperty(target);
			mapping1.AssignTo(mapping2);

			TestContext.WriteLine("Name:{0}, Age:{1}", target.Name, target.Age);

			Assert.AreEqual("NCsoft 1", target.Name);
			Assert.AreEqual(11, target.Age);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MappingProvider 로 매핑키의 대소문자 무시 DataRow->Property로 매핑 테스트, 값이 유효하면 통과")]
		public void Mapping_DataRow_Assign_To_Property_Ingore_Key_UpperCase()
		{
			DataTable source = new DataTable();
			source.Columns.Add("Name", typeof(string));
			source.Columns.Add("Age", typeof(int));
			source.Rows.Add("NCsoft 1", 11);

			MockPerson target = new MockPerson();

			MappingProviderForDataRow mapping1 = new MappingProviderForDataRow(source.Rows[0]);
			MappingProviderForProperty mapping2 = new MappingProviderForProperty(target, StringComparer.OrdinalIgnoreCase);
			mapping1.AssignTo(mapping2);

			TestContext.WriteLine("Name:{0}, Age:{1}", target.Name, target.Age);

			Assert.AreEqual("NCsoft 1", target.Name);
			Assert.AreEqual(11, target.Age);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MappingProvider 로 매핑키의 DataTable->List로 매핑 테스트, 값이 유효하면 통과")]
		public void Mapping_DataTable_Assign_To_List()
		{
			var source = new DataTable();
			source.Columns.Add("Name", typeof(string));
			source.Columns.Add("Age", typeof(int));
			source.Rows.Add("NCsoft 1", 11);
			source.Rows.Add("NCsoft 2", 2);
			source.Rows.Add("NCsoft 3", 3);

			var target = new List<MockPerson>();

			var mapping1 = new MappingProviderForDataTable(source);
			var mapping2 = new MappingProviderForCollection<MockPerson>(target);
			mapping1.AssignTo(mapping2);

			foreach ( var person in target )
			{
				TestContext.WriteLine("Name:{0}, Age:{1}", person.Name, person.Age);
			}
		}

		[TestMethod]
		public void DataTable_To_Collection_Mapping_test()
		{
			var dt = new DataTable();
			var person = new List<MockPerson>();
			person.Add(new MockPerson() { Name = "AAAA", Age = 1 });
			person.Add(new MockPerson() { Name = "BBBB", Age = 2 });
			person.Add(new MockPerson() { Name = "CCCC", Age = 3 });

			var source = new MappingProviderForDataTable(dt);
			var target = new MappingProviderForCollection<MockPerson>(person);

			target.AssignTo(source);

			Console.WriteLine("DataTable rows count is " + dt.Rows.Count.ToString());
		}
	}
}
