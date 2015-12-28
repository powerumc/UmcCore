//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using Umc.Core.Logger;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Umc.Core.Logger.Tests
//{
//	[TestClass()]
//	public class FrameworkFileLogListenerTests
//	{
//		public TestContext TestContext { get; set; }

//		[TestMethod()]
//		public void FrameworkFileLogListenerTest()
//		{
//			Assert.Fail();
//		}

//		[TestMethod()]
//		public void initTest()
//		{
//			const string FORMAT = "LogFile_{0:yyyyMMdd}.log";

//			Console.WriteLine(string.Format(FORMAT, DateTime.Now));
//		}

//		[TestMethod()]
//		public void WriteTest()
//		{
//		}

//		[TestMethod()]
//		public void WriteLineTest()
//		{
//		}

//		[TestMethod]
//		public void EnsureDateTime_Test()
//		{
//			var datetime1 = "2015/12/24 13:40:00".ToDateTime();
//			var datetime2 = "2015/12/24 23:59:59".ToDateTime();

//			Assert.AreEqual(datetime1.Date, datetime2.Date);
//		}

//		[TestMethod]
//		public void WriteLog_Test()
//		{
//			var listener = new FrameworkFileLogListener("C:\\");
//			Trace.Listeners.Add(listener);

//			var logger = FrameworkLogger.GetLogger(this.GetType());
//			logger.Info("HELLO WORLD");

//			Trace.Flush();
//		}
//	}
//}
