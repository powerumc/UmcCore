using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Umc.Core
{
	public interface IFrameworkLogger
	{
		IFrameworkLogger Debug(object message);
		IFrameworkLogger Debug(object message, Exception exception);
		IFrameworkLogger DebugFormat(string format, params object[] args);
		IFrameworkLogger DebugFormat(string format, object arg0);
		IFrameworkLogger DebugFormat(IFormatProvider provider, string format, params object[] args);
		IFrameworkLogger DebugFormat(string format, object arg0, object arg1);
		IFrameworkLogger DebugFormat(string format, object arg0, object arg1, object arg2);
		IFrameworkLogger Error(object message);
		IFrameworkLogger Error(object message, Exception exception);
		IFrameworkLogger ErrorFormat(string format, params object[] args);
		IFrameworkLogger ErrorFormat(string format, object arg0);
		IFrameworkLogger ErrorFormat(IFormatProvider provider, string format, params object[] args);
		IFrameworkLogger ErrorFormat(string format, object arg0, object arg1);
		IFrameworkLogger ErrorFormat(string format, object arg0, object arg1, object arg2);
		IFrameworkLogger Fatal(object message);
		IFrameworkLogger Fatal(object message, Exception exception);
		IFrameworkLogger FatalFormat(string format, params object[] args);
		IFrameworkLogger FatalFormat(string format, object arg0);
		IFrameworkLogger FatalFormat(IFormatProvider provider, string format, params object[] args);
		IFrameworkLogger FatalFormat(string format, object arg0, object arg1);
		IFrameworkLogger FatalFormat(string format, object arg0, object arg1, object arg2);
		IFrameworkLogger Info(object message);
		IFrameworkLogger Info(object message, Exception exception);
		IFrameworkLogger InfoFormat(string format, params object[] args);
		IFrameworkLogger InfoFormat(string format, object arg0);
		IFrameworkLogger InfoFormat(IFormatProvider provider, string format, params object[] args);
		IFrameworkLogger InfoFormat(string format, object arg0, object arg1);
		IFrameworkLogger InfoFormat(string format, object arg0, object arg1, object arg2);
		IFrameworkLogger Warn(object message);
		IFrameworkLogger Warn(object message, Exception exception);
		IFrameworkLogger WarnFormat(string format, params object[] args);
		IFrameworkLogger WarnFormat(string format, object arg0);
		IFrameworkLogger WarnFormat(IFormatProvider provider, string format, params object[] args);
		IFrameworkLogger WarnFormat(string format, object arg0, object arg1);
		IFrameworkLogger WarnFormat(string format, object arg0, object arg1, object arg2);
		
		bool IsDebugEnabled { get; }
		bool IsErrorEnabled { get; }
		bool IsFatalEnabled { get; }
		bool IsInfoEnabled { get; }
		bool IsWarnEnabled { get; }
	}
}
