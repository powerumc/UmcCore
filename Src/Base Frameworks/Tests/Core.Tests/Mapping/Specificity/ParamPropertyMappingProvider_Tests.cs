using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using System.Diagnostics;

namespace Umc.Core.Mapping
{
	[TestClass]
	public class ParamPropertyMappingProvider_Tests : UnitTestBase
	{
		internal class ParamObject
		{
			[ParamProperty(Action=ParamAction.Send | ParamAction.Receive, IsRequired=true, Name="username")]
			public string UserName { get; set; }

			[ParamProperty(Action=ParamAction.Send | ParamAction.Receive, IsRequired=true)]
			public int Age { get; set; }

			[ParamProperty(Action = ParamAction.Send)]
			public string OnlySend { get; set; }

			[ParamProperty(Action = ParamAction.Receive)]
			public string OnlyReceive { get; set; }

			public string NonAttribute { get; set; }
		}

		[TestMethod]
		[Description("ParamPropertyMappingProvider가 올바르게 속성값을 읽는지 테스트, 값이 유효하면 통과")]
		public void ParamMappingObject_Getter_Test()
		{
			var param = new ParamObject()
			{
				UserName = "NCsoft",
				Age = 255
			};

            var mapping = new ParamPropertyMappingProvider(param);
            TestContext.WriteLine(mapping.Getter("UserName").ToString());
			TestContext.WriteLine(mapping.Getter("Age").ToString());

			Assert.AreEqual("NCsoft", mapping.Getter("UserName"));
			Assert.AreEqual(255, mapping.Getter("Age"));
		}

		[TestMethod]
		[Description("ParamPropertyMappingProvider가 올바르게 속성값을 쓰는지 테스트, 값이 유효하면 통과")]
		public void ParamMappingObject_Setter_Test()
		{
			var param = new ParamObject()
			{
				UserName = "NCsoft",
				Age = 255
			};

            var mapping = new ParamPropertyMappingProvider(param);

            mapping.Setter("UserName", "NCsoft NCsoft");
			mapping.Setter("Age", 0);

			Assert.AreEqual("NCsoft NCsoft", mapping.Getter("UserName"));
			Assert.AreEqual(0, mapping.Getter("Age"));
		}

		[TestMethod]
		[Description("ParamPropertyMappingProvider가 Name속성으로 올바르게 값을 읽고 쓰는지 테스트, 값이 유효하면 통과")]
		public void ShouldBeCanGetter_via_NameValue_NameProperty_Test()
		{
			var param = new ParamObject()
			{
				UserName = "NCsoft",
				Age = 255
			};

            var mapping = new ParamPropertyMappingProvider(param);
            Assert.AreEqual("NCsoft", mapping.Getter("username"));

			mapping.Setter("username", "NCsoft NCsoft");
			Assert.AreEqual("NCsoft NCsoft", mapping.Getter("username"));
			Assert.AreEqual("NCsoft NCsoft", param.UserName);
		}

		[TestMethod]
		[Description("ParamPropertyMappingProvider는 ParamProperty.Name의 값이 없으면 오류가 발생하는지 테스트, 오류가 발생하면 통과")]
		[ExpectedException(typeof(MappingException))]
		public void ShouldBeNotMatching_ParamProperty_Test()
		{
			var param = new ParamObject()
			{
				UserName = "NCsoft",
				Age = 255,
				NonAttribute = "Some Value"
			};

            var mapping = new ParamPropertyMappingProvider(param);
            var value = mapping.Getter("NonAttribute");

			TestContext.WriteLine("NonAttribute Property : ", value);
		}


		internal class ParamObjectForDefaultValue
		{
			[ParamProperty(DefaultValue="NCsoft")]
			public string UserName { get; set; }
		}


		[TestMethod]
		[Description("ParamPropertyAttribute의 DefaultValue 속성이 올바르게 적용되는지 테스트, 값이 유효하면 통과")]
		public void ShouldBe_Return_Value_When_Empty_Property_Test()
		{
			var sourceParam = new ParamObjectForDefaultValue();
            var targetParam = new ParamObjectForDefaultValue();

            var sourceMapping = new ParamPropertyMappingProvider(sourceParam);
            var targetMapping = new ParamPropertyMappingProvider(targetParam);

            sourceMapping.AssignTo(targetMapping);

			Assert.AreEqual("NCsoft", targetParam.UserName);

			TestContext.WriteLine(targetParam.UserName);
		}





		internal class ParamObjectLeft
		{
			[ParamProperty]
			public string Both { get; set; }

			[ParamProperty(Action=ParamAction.Send)]
			public string OnlySend{ get; set; }

			[ParamProperty(Action = ParamAction.Receive)]
			public string OnlyReceived { get; set; }
		}

		internal class ParamObjectRight
		{
			[ParamProperty]
			public string Both { get; set; }

			[ParamProperty(Action = ParamAction.Send)]
			public string OnlySend { get; set; }

			[ParamProperty(Action = ParamAction.Receive)]
			public string OnlyReceived { get; set; }
		}



		[TestMethod]
		[Description("ParamPropertyAttribute의 ParamAction이 Send,Received별로 매핑이 올바르게 되는지 테스트, 값이 유효하면 통과")]
		public void Just_Send_Param_Action_Test()
		{
			var sendParam = new ParamObjectLeft()
			{
				Both = "Both",
				OnlySend = "OnlySend",
				OnlyReceived = "OnlyReceived"
			};

            var receivedParam = new ParamObjectRight();

            var sendMapping = new ParamPropertyMappingProvider(sendParam);
            var receivedMapping = new ParamPropertyMappingProvider(receivedParam);

            sendMapping.AssignTo(receivedMapping);

			receivedParam.GetType()
				.GetProperties()
				.ToList()
				.ForEach(o =>
				{
					Console.WriteLine("{0} = {1}", o.Name, o.GetValue(receivedParam, null) ?? "NULL");
				});

			Assert.AreEqual("Both", receivedParam.Both);
			Assert.AreEqual(null, receivedParam.OnlySend);
			Assert.AreEqual("OnlyReceived", receivedParam.OnlyReceived);
		}






		internal class Temp_SourceClass
		{
			public string UserName { get; set; }
		}

		internal class Temp_TargetClass
		{
			[ParamProperty]
			public string UserName { get; set; }

			[ParamProperty(true)]
			public int Age { get; set; }
		}


		[TestMethod]
		[Description("ParamPropertyAttribute의 IsRequired 속성이 True인데 매핑 대상이 없는 경우 테스트, 오류가 발생하면 통과")]
		[ExpectedException(typeof(MappingException))]
		public void ShouldBe_Fire_Exception_Why_Not_Exists_Property_Possible_Mapping_Name_Test()
		{
			var watcher = Stopwatch.StartNew();

            var source = new Temp_SourceClass()
			{
				UserName = "NCsoft"
			};

            var target = new Temp_TargetClass();

            var sourceMapping = new MappingProviderForProperty(source);
            var targetMapping = new ParamPropertyMappingProvider(target);

            sourceMapping.AssignTo(targetMapping);
		}
	}
}
