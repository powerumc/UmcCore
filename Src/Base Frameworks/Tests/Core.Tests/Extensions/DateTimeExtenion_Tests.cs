using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umc.Core.Tests.Extensions
{
	[TestClass]
	public class DateTimeExtenion_Tests
	{
		public TestContext TestContext { get; set; }

		[TestMethod]
		public void DateTimeOfWeek_Test()
		{
			var dateStart = DateTime.Now.DateTimeOfWeek(DayOfWeek.Sunday);
			var dateEnd = DateTime.Now.DateTimeOfWeek(DayOfWeek.Saturday);

			TestContext.WriteLine("FirstDay is {0}", dateStart);
			TestContext.WriteLine("LastDay is {0}", dateEnd);

		}
	}
}
