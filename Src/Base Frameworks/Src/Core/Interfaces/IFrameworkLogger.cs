// file:	Interfaces\IFrameworkLogger.cs
//
// summary:	Declares the IFrameworkLogger interface

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Umc.Core
{
    /// <summary>
    ///     로깅 서비스를 제공하는 인터페이스 입니다.
    /// </summary>
    /// <example>
    /// <code>
    /// public class LogTest 
    /// {
    ///     private static readonly IFrameworkLogger logger = FrameworkLogger.GetLogger(typeof(LogTest));
    ///     
    ///     public void Say()
    ///     {
    ///         logger.Debug("디버그 메시지를 출력합니다");
    ///     }
    /// }
    /// </code>
    /// </example>
	public interface IFrameworkLogger
	{
        /// <summary>
        ///     디버그 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">  출력할 메시지 입니다. </param>
		IFrameworkLogger Debug(object message);

        /// <summary>
        ///     디버그 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">      출력할 메시지 입니다. </param>
        /// <param name="exception">    예외 정보 입니다. </param>
		IFrameworkLogger Debug(object message, Exception exception);

        /// <summary>
        ///     디버그 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
		IFrameworkLogger DebugFormat(string format, params object[] args);

        /// <summary>
        ///     디버그 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        IFrameworkLogger DebugFormat(string format, object arg0);

        /// <summary>
        ///     디버그 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="provider"> <see cref="IFormatProvider"/> 입니다. </param>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
        IFrameworkLogger DebugFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        ///     디버그 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        IFrameworkLogger DebugFormat(string format, object arg0, object arg1);

        /// <summary>
        ///     디버그 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        /// <param name="arg2">     매개변수 입니다. </param>
        IFrameworkLogger DebugFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        ///     오류 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">  출력할 메시지 입니다. </param>
		IFrameworkLogger Error(object message);

        /// <summary>
        ///     오류 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">      출력할 메시지 입니다. </param>
        /// <param name="exception">    예외 정보 입니다. </param>
        IFrameworkLogger Error(object message, Exception exception);

        /// <summary>
        ///     오류 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
		IFrameworkLogger ErrorFormat(string format, params object[] args);

        /// <summary>
        ///     오류 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
		IFrameworkLogger ErrorFormat(string format, object arg0);

        /// <summary>
        ///     오류 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="provider"> <see cref="IFormatProvider"/> 입니다. </param>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
        IFrameworkLogger ErrorFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        ///     오류 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        IFrameworkLogger ErrorFormat(string format, object arg0, object arg1);

        /// <summary>
        ///     오류 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        /// <param name="arg2">     매개변수 입니다. </param>
        IFrameworkLogger ErrorFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        ///     치명적인 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">  출력할 메시지 입니다. </param>
		IFrameworkLogger Fatal(object message);

        /// <summary>
        ///     치명적인 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">      출력할 메시지 입니다. </param>
        /// <param name="exception">    예외 정보 입니다. </param>
        IFrameworkLogger Fatal(object message, Exception exception);

        /// <summary>
        ///     치명적인 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
		IFrameworkLogger FatalFormat(string format, params object[] args);

        /// <summary>
        ///     치명적인 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
		IFrameworkLogger FatalFormat(string format, object arg0);

        /// <summary>
        ///     치명적인 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="provider"> <see cref="IFormatProvider"/> 입니다. </param>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
		IFrameworkLogger FatalFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        ///     치명적인 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
		IFrameworkLogger FatalFormat(string format, object arg0, object arg1);

        /// <summary>
        ///     치명적인 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        /// <param name="arg2">     매개변수 입니다. </param>
		IFrameworkLogger FatalFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        ///     정보 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">  출력할 메시지 입니다. </param>
		IFrameworkLogger Info(object message);

        /// <summary>
        ///     정보 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">      출력할 메시지 입니다. </param>
        /// <param name="exception">    예외 정보 입니다. </param>
        IFrameworkLogger Info(object message, Exception exception);

        /// <summary>
        ///     정보 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
        IFrameworkLogger InfoFormat(string format, params object[] args);

        /// <summary>
        ///     정보 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        IFrameworkLogger InfoFormat(string format, object arg0);

        /// <summary>
        ///     정보 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="provider"> <see cref="IFormatProvider"/> 입니다. </param>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
        IFrameworkLogger InfoFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        ///     정보 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        IFrameworkLogger InfoFormat(string format, object arg0, object arg1);

        /// <summary>
        ///     정보 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        /// <param name="arg2">     매개변수 입니다. </param>
        IFrameworkLogger InfoFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        ///     경고 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">  출력할 메시지 입니다. </param>
        IFrameworkLogger Warn(object message);

        /// <summary>
        ///     경고 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">      출력할 메시지 입니다. </param>
        /// <param name="exception">    예외 정보 입니다. </param>
        IFrameworkLogger Warn(object message, Exception exception);

        /// <summary>
        ///     경고 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
        IFrameworkLogger WarnFormat(string format, params object[] args);

        /// <summary>
        ///     경고 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        IFrameworkLogger WarnFormat(string format, object arg0);

        /// <summary>
        ///     경고 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="provider"> <see cref="IFormatProvider"/> 입니다. </param>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="args">     매개변수 입니다. </param>
        IFrameworkLogger WarnFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        ///     경고 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        IFrameworkLogger WarnFormat(string format, object arg0, object arg1);

        /// <summary>
        ///     경고 메시지를 포멧 메시지로 출력합니다.
        /// </summary>
        /// <param name="format">   포멧 메시지 입니다. </param>
        /// <param name="arg0">     매개변수 입니다. </param>
        /// <param name="arg1">     매개변수 입니다. </param>
        /// <param name="arg2">     매개변수 입니다. </param>
        IFrameworkLogger WarnFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        ///     디버그 메시지를 출력할지 여부를 가져옵니다.
        /// </summary>
		bool IsDebugEnabled { get; }

        /// <summary>
        ///     오류 메시지를 출력할지 여부를 가져옵니다.
        /// </summary>
		bool IsErrorEnabled { get; }

        /// <summary>
        ///     치명적인 메시지를 출력할지 여부를 가져옵니다.
        /// </summary>
		bool IsFatalEnabled { get; }

        /// <summary>
        ///     정보 메시지를 출력할지 여부를 가져옵니다.
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        ///     경기 메시지를 출력할지 여부를 가져옵니다.
        /// </summary>
        bool IsWarnEnabled { get; }
	}
}
