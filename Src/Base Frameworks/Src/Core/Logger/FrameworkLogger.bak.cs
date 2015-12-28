//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using Umc.Core.Logger;

//namespace Umc.Core
//{
//	public class FrameworkLogger : IFrameworkLogger, IDisposable
//	{
//		private static readonly object obj = new object();
//		protected Type Type { get; set; }

//		protected FrameworkLogger(Type type)
//		{
//			this.Type = type;
//		}

//		protected static string GetLoggerName(DateTime datetime)
//		{
//			return FrameworkFileLogListener.InitLoggerName(DateTime.Now);
//		}

//		public static IFrameworkLogger GetLogger(Type type)
//		{
//			lock (obj)
//			{
//				return new FrameworkLogger(type);
//			}
//		}

//		public IFrameworkLogger Write(string message)
//		{
//			Trace.Write(message);
//			return this;
//		}

//		public IFrameworkLogger WriteLine(string message)
//		{
//			Write(message + Environment.NewLine);
//			return this;
//		}

//		#region Info
//		public IFrameworkLogger Info(string message)
//		{
//			return Info(message, null);
//		}

//		public IFrameworkLogger Info(string message, Exception exception)
//		{
//			if (message == null) throw new ArgumentNullException("message");

//			writeHeader("INFO", message, exception);
//			Trace.Flush();

//			return this;
//		}

//		#endregion

//		#region Warn
//		public IFrameworkLogger Warn(string message)
//		{
//			return Warn(message, null);
//		}

//		public IFrameworkLogger Warn(string message, Exception exception)
//		{
//			if (message == null) throw new ArgumentNullException("message");

//			writeHeader("WARN", message, exception);
//			Trace.Flush();

//			return this;
//		}

//		#endregion

//		#region Error
//		public IFrameworkLogger Error(string message)
//		{
//			return Error(message, null);
//		}

//		public IFrameworkLogger Error(string message, Exception exception)
//		{
//			if (message == null) throw new ArgumentNullException("message");

//			writeHeader("ERROR", message, exception);
//			Trace.Flush();

//			return this;
//		}

//		#endregion

//		private string getDateTime()
//		{
//			return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.FFF");
//		}

//		private void writeHeader(string logLevel, string message, Exception exception)
//		{
//			Write(getDateTime()).Write(" ");						// 날짜/시간
//			Write(string.Concat("[", logLevel, "] "));				// 로그 레벨
//			Write(string.Concat("[", Thread.CurrentThread.ManagedThreadId, "] "));	// Thread
//			if (this.Type != null) Write(string.Concat("[", this.Type.AbbreviationString(), "] "));
//			WriteLine(message);
//			if (exception != null) writeException(exception);
//		}

//		private void writeException(Exception ex)
//		{
//			WriteLine(ex.Message);
//			WriteLine(ex.StackTrace);

//			if (ex.InnerException != null)
//				writeException(ex.InnerException);
//		}

//		#region Implementation of IDisposable

//		/// <summary>
//		/// 관리되지 않는 리소스의 확보, 해제 또는 다시 설정과 관련된 응용 프로그램 정의 작업을 수행합니다.
//		/// </summary>
//		public void Dispose()
//		{
//		}

//		#endregion
//	}
//}
