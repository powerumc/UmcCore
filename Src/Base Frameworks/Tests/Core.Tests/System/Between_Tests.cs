using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System
{
	[TestClass]
	public class Between_Tests
	{
		[TestMethod]
		public void Equals_Test()
		{
			var between = new Between<int>(1,1,1);
			Console.WriteLine(between.IsBetween);

			Assert.IsTrue(between.IsBetween);
		}

		[TestMethod]
		public void IsBetween_Test()
		{
			var between = new Between<int>(1, 2, 3);
			Console.WriteLine(between.IsBetween);

			Assert.IsTrue(between.IsBetween);
		}

		[TestMethod]
		public void IsNotBetween_Test()
		{
			var between = new Between<int>(1, 3, 2);
			Console.WriteLine(between.IsBetween);

			Assert.IsFalse(between.IsBetween);
		}

		[TestMethod]
		public void IsOver_Test()
		{
			var between = new Between<int>(1, 3, 2);
			Console.WriteLine(between.IsOver);

			Assert.IsTrue(between.IsOver);
		}

		[TestMethod]
		public void DateTimeBetween_Test()
		{
			var between = new Between<DateTime>(DateTime.Now.AddSeconds(-1), DateTime.Now, DateTime.Now.AddSeconds(1));
			Console.WriteLine(between.IsBetween);

			Assert.IsTrue(between.IsBetween);
		}
	}
}
