//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;

//namespace Umc.Core.Logger
//{
//	public class FrameworkFileLogListener : TextWriterTraceListener
//	{
//		private const string FILE_FORMAT = "LogFile_{0:yyyyMMdd}.log";

//		/// <summary> 로그를 기록할 파일 이름입니다. </summary>
//		/// <value> 로그 파일명입니다. </value>
//		public string FileName { get; protected set; }

//		/// <summary>
//		/// <see cref="T:System.IO.TextWriter"/>를 출력 수신자로 사용하여 <see cref="T:System.Diagnostics.TextWriterTraceListener"/> 클래스의 새 인스턴스를 초기화합니다.
//		/// </summary>
//		public FrameworkFileLogListener(string directory)
//			: this(directory, DateTime.Now)
//		{}

//		/// <summary>
//		/// <see cref="T:System.IO.TextWriter"/>를 출력 수신자로 사용하여 <see cref="T:System.Diagnostics.TextWriterTraceListener"/> 클래스의 새 인스턴스를 초기화합니다.
//		/// </summary>
//		public FrameworkFileLogListener(string directory, DateTime datetime)
//			: base(InitLogger(directory, datetime), InitLoggerName(datetime))
//		{
//		}

//		internal static string InitLoggerName(DateTime datetime)
//		{
//			var name = typeof (FrameworkFileLogListener).Name + datetime.ToString("yyyyMMdd");
//			return name;
//		}

//		private static StreamWriter InitLogger(string directory, DateTime datetime)
//		{
//			var writer = new StreamWriter(EnsureDirectory(directory, datetime), true) {AutoFlush = true};
//			return writer;
//		}

//		private static string EnsureDirectory(string directory, DateTime datetime)
//		{
//			directory = Path.Combine(directory, datetime.ToString("yyyyMMdd"));

//			if (!Directory.Exists(directory))
//				Directory.CreateDirectory(directory);

//			var file = string.Format(FILE_FORMAT, datetime);
//			var path = Path.Combine(directory, file);

//			return path;
//		}

//		private static void EnsureDateTime()
//		{

//		}

//		#region Overrides of TextWriterTraceListener

//		/// <summary>
//		/// 이 인스턴스의 <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer"/>에 메시지를 씁니다.
//		/// </summary>
//		/// <param name="message">쓸 메시지입니다. </param><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
//		public override void Write(string message)
//		{
//			EnsureDateTime();
//			base.Write(message);
//		}

//		/// <summary>
//		/// 줄 종결자가 뒤에 오는 이 인스턴스의 <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer"/>에 메시지를 씁니다. 기본 줄 종결자는 캐리지 리턴과 줄 바꿈(\r\n) 조합입니다.
//		/// </summary>
//		/// <param name="message">쓸 메시지입니다. </param><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
//		public override void WriteLine(string message)
//		{
//			EnsureDateTime();
//			base.WriteLine(message);
//		}

//		#endregion
//	}
//}
