using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core
{
	[TestClass]
	public class Time_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("Time 구조체의 Implicit 구현 테스트, Implicit 결과가 동일하면 통과")]
		public void AssignTheTime()
		{
			Time time = "1:2:3";
			TimeSpan timespan = new TimeSpan(1,2,3);
			
			Assert.IsTrue(time==timespan);




			TimeSpan fail = new TimeSpan(1,2,4);

			Assert.IsFalse(time == fail);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Time 구조체의 Implicit 의 성공/실패 테스트, 각각 올바르게 성공, 실패하면 통과")]
		public void AssignTheTimeWithDays()
		{
			Time time = "5.1:2:3";
			TimeSpan timespan = new TimeSpan(5,1,2,3);

			Assert.IsTrue(time==timespan);


			TimeSpan fail = new TimeSpan(6,1,2,3);

			Assert.IsFalse(time == fail);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Time 구조체의 Implicit 의 + Operation 테스트, 각각 올바르게 성공, 실패하면 통과")]
		public void PlusTime()
		{
			Time time1 = "1:0:3";
			Time time2 = "2:3:1";

			Time success = "3:3:4";
			Time fail = "10:2:3";

			Assert.IsTrue(time1+time2 == success);
			Assert.IsFalse(time1+time2 == fail);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("Time 구조체의 Implicit 에 올바르지 않은 시간 값을 넣는 테스트, 반드시 오류가 발생해야 통과")]
		[ExpectedException(typeof(FrameworkException), "Time구조체의 입력 값이 잘못되었지만 오류가 발생하지 않음")]
		public void InvalidAssignTheTime()
		{
			Time time = "5.1:23";
		}
	}
}
