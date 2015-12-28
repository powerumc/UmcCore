using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Umc.Core.Web
{
	public class JavaScriptTraceListner : TraceListener
	{
		private StringBuilder logBuilder;
		private readonly IList<string> logList;

		public IEnumerable<string> Logs { get { return logList; } }

		public JavaScriptTraceListner()
		{
			logBuilder = new StringBuilder(2048);
			logList = new List<string>();
		}

		public override void Write(string message)
		{
			if (logBuilder.Length == 0)
			{
				logBuilder.AppendFormat("[{0:yyyy-MM-dd HH:mm:ss.fff}] ", DateTime.Now);
			}
			logBuilder.Append(message);
		}

		public override void WriteLine(string message)
		{
			logBuilder.AppendLine(string.Format("[{0:yyyy-MM-dd HH:mm:ss.fff}] {1}", DateTime.Now, message));
//			logList.Add(logBuilder.ToString());
//#if NET40
//			logBuilder.Clear();
//#else
//			logBuilder.Remove(0, logBuilder.Length);
//#endif
		}

		public string ToLogString()
		{
			return this.logBuilder.ToString();
		}

		public void Clear()
		{
			logBuilder = null;
			logBuilder = new StringBuilder(2048);
			logList.Clear();
		}
	}
}
