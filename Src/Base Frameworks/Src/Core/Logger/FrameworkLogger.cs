using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Umc.Core.Logger
{
    /// <summary>
    ///     로깅 서비스를 제공하는 클래스입니다.
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
    /// <seealso cref="T:Umc.Core.IFrameworkLogger"/>
    public class FrameworkLogger : IFrameworkLogger
	{
		private const string NAME = "FrameworkLogger";
		private readonly ILog log;

        /// <summary>
        ///     로깅 서비스를 제공하는 클래스의 생성자 입니다.
        /// </summary>
        /// <param name="type"> 로깅할 개체의 타입입니다. </param>
		protected FrameworkLogger(Type type)
		{
			this.log = LogManager.GetLogger(type);
		}

        /// <summary>
        ///     로깅 서비스를 제공하는 클래스의 생성자 입니다.
        /// </summary>
        /// <param name="name"> 로깅할 이름입니다. </param>

		protected FrameworkLogger(string name)
		{
			this.log = LogManager.GetLogger(name);
		}

        /// <summary>
        ///     로그 인스턴스를 가져옵니다.
        /// </summary>
        /// <param name="type"> 로깅할 개체의 타입입니다. </param>
		public static IFrameworkLogger GetLogger(Type type)
		{
			EnsureLoggerInited();
			var logger = new FrameworkLogger(type);
			
			return logger;
		}

        /// <summary>
        ///     로그 인스턴스를 가져옵니다.
        /// </summary>
        /// <param name="name"> 로깅할 이름입니다. </param>
		public static IFrameworkLogger GetLogger(string name)
		{
			EnsureLoggerInited();
			var logger = new FrameworkLogger(name);

			return logger;
		}

		private static void EnsureLoggerInited()
		{
			#region Log4Net remove because just supports sync logging.
			var hierarchy = (Hierarchy)LogManager.GetRepository();
			if (hierarchy.Exists(NAME) != null) return;

			var patternLayout = new PatternLayout { ConversionPattern = "%date [%level] [%thread] %logger - %message%newline" };
			patternLayout.ActivateOptions();

			//var appender = new RollingFileAppender
			//{
			//	AppendToFile = true,
			//	File = @"C:\log\kart\",
			//	DatePattern = @"yyyyMMdd\all_yyyyMMdd'.log'",
			//	Layout = patternLayout,
			//	MaxSizeRollBackups = 5,
			//	MaximumFileSize = "1GB",
			//	RollingStyle = RollingFileAppender.RollingMode.Date,
			//	StaticLogFileName = false
			//};
			//appender.ActivateOptions();
			//hierarchy.Root.AddAppender(appender);



			var memory = new TraceAppender() { Name = NAME, Layout = patternLayout };
			memory.ActivateOptions();
			hierarchy.Root.AddAppender(memory);

			//var eventStreamAppender = new EventStreamAppender() { Name = NAME };
			//hierarchy.Root.AddAppender(eventStreamAppender);

			hierarchy.Root.Level = Level.All;
			hierarchy.Configured = true;

			log4net.Config.BasicConfigurator.Configure(hierarchy); 
			#endregion
		}

		#region Implementation of ILoggerWrapper

		/// <summary>
		/// Get the implementation behind this wrapper object.
		/// </summary>
		/// <value>
		/// The <see cref="T:log4net.Core.ILogger"/> object that in implementing this object.
		/// </value>
		/// <remarks>
		/// <para>
		/// The <see cref="T:log4net.Core.ILogger"/> object that in implementing this
		///             object. The <c>Logger</c> object may not 
		///             be the same object as this object because of logger decorators.
		///             This gets the actual underlying objects that is used to process
		///             the log events.
		/// </para>
		/// </remarks>
		public ILogger Logger
		{
			get { return this.log.Logger; }
		}

		#endregion

		#region Implementation of ILog

		/// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Debug"/> level.</overloads>
		/// <summary>
		/// Log a message object with the <see cref="F:log4net.Core.Level.Debug"/> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>DEBUG</c>
		///             enabled by comparing the level of this logger with the 
		///             <see cref="F:log4net.Core.Level.Debug"/> level. If this logger is
		///             <c>DEBUG</c> enabled, then it converts the message object
		///             (passed as parameter) to a string by invoking the appropriate
		///             <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		///             proceeds to call all the registered appenders in this logger 
		///             and also higher in the hierarchy depending on the value of 
		///             the additivity flag.
		/// </para>
		/// <para>
		/// <b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		///             to this method will print the name of the <see cref="T:System.Exception"/> 
		///             but no stack trace. To print a stack trace use the 
		///             <see cref="M:Debug(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Debug(object,Exception)"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public IFrameworkLogger Debug(object message)
		{
			log.Debug(message);
			return this;
		}

		/// <summary>
		/// Log a message object with the <see cref="F:log4net.Core.Level.Debug"/> level including
		///             the stack trace of the <see cref="T:System.Exception"/> passed
		///             as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param><param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="M:Debug(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Debug(object)"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public IFrameworkLogger Debug(object message, Exception exception)
		{
			log.Debug(message, exception);
			return this;
		}

		/// <overloads>Log a formatted string with the <see cref="F:log4net.Core.Level.Debug"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Debug(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Debug(object)"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public IFrameworkLogger DebugFormat(string format, params object[] args)
		{
			log.DebugFormat(format, args);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Debug(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Debug(object)"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public IFrameworkLogger DebugFormat(string format, object arg0)
		{
			log.DebugFormat(format, arg0);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Debug(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Debug(object)"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public IFrameworkLogger DebugFormat(string format, object arg0, object arg1)
		{
			log.DebugFormat(format, arg0, arg1);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param><param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Debug(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Debug(object)"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public IFrameworkLogger DebugFormat(string format, object arg0, object arg1, object arg2)
		{
			log.DebugFormat(format, arg0, arg1, arg2);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param><param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Debug(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Debug(object)"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public IFrameworkLogger DebugFormat(IFormatProvider provider, string format, params object[] args)
		{
			log.DebugFormat(provider, format, args);
			return this;
		}

		/// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Info"/> level.</overloads>
		/// <summary>
		/// Logs a message object with the <see cref="F:log4net.Core.Level.Info"/> level.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>INFO</c>
		///             enabled by comparing the level of this logger with the 
		///             <see cref="F:log4net.Core.Level.Info"/> level. If this logger is
		///             <c>INFO</c> enabled, then it converts the message object
		///             (passed as parameter) to a string by invoking the appropriate
		///             <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		///             proceeds to call all the registered appenders in this logger 
		///             and also higher in the hierarchy depending on the value of the 
		///             additivity flag.
		/// </para>
		/// <para>
		/// <b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		///             to this method will print the name of the <see cref="T:System.Exception"/> 
		///             but no stack trace. To print a stack trace use the 
		///             <see cref="M:Info(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <param name="message">The message object to log.</param><seealso cref="M:Info(object,Exception)"/><seealso cref="P:log4net.ILog.IsInfoEnabled"/>
		public IFrameworkLogger Info(object message)
		{
			log.Info(message);
			return this;
		}

		/// <summary>
		/// Logs a message object with the <c>INFO</c> level including
		///             the stack trace of the <see cref="T:System.Exception"/> passed
		///             as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param><param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="M:Info(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Info(object)"/><seealso cref="P:log4net.ILog.IsInfoEnabled"/>
		public IFrameworkLogger Info(object message, Exception exception)
		{
			log.Info(message, exception);
			return this;
		}

		/// <overloads>Log a formatted message string with the <see cref="F:log4net.Core.Level.Info"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Info(object)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Info(object,Exception)"/><seealso cref="P:log4net.ILog.IsInfoEnabled"/>
		public IFrameworkLogger InfoFormat(string format, params object[] args)
		{
			log.InfoFormat(format, args);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Info(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Info(object)"/><seealso cref="P:log4net.ILog.IsInfoEnabled"/>
		public IFrameworkLogger InfoFormat(string format, object arg0)
		{
			log.InfoFormat(format, arg0);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Info(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Info(object)"/><seealso cref="P:log4net.ILog.IsInfoEnabled"/>
		public IFrameworkLogger InfoFormat(string format, object arg0, object arg1)
		{
			log.InfoFormat(format, arg0, arg1);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param><param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Info(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Info(object)"/><seealso cref="P:log4net.ILog.IsInfoEnabled"/>
		public IFrameworkLogger InfoFormat(string format, object arg0, object arg1, object arg2)
		{
			log.InfoFormat(format, arg0, arg1, arg2);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param><param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Info(object)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Info(object,Exception)"/><seealso cref="P:log4net.ILog.IsInfoEnabled"/>
		public IFrameworkLogger InfoFormat(IFormatProvider provider, string format, params object[] args)
		{
			log.InfoFormat(provider, format, args);
			return this;
		}

		/// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Warn"/> level.</overloads>
		/// <summary>
		/// Log a message object with the <see cref="F:log4net.Core.Level.Warn"/> level.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>WARN</c>
		///             enabled by comparing the level of this logger with the 
		///             <see cref="F:log4net.Core.Level.Warn"/> level. If this logger is
		///             <c>WARN</c> enabled, then it converts the message object
		///             (passed as parameter) to a string by invoking the appropriate
		///             <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		///             proceeds to call all the registered appenders in this logger 
		///             and also higher in the hierarchy depending on the value of the 
		///             additivity flag.
		/// </para>
		/// <para>
		/// <b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		///             to this method will print the name of the <see cref="T:System.Exception"/> 
		///             but no stack trace. To print a stack trace use the 
		///             <see cref="M:Warn(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <param name="message">The message object to log.</param><seealso cref="M:Warn(object,Exception)"/><seealso cref="P:log4net.ILog.IsWarnEnabled"/>
		public IFrameworkLogger Warn(object message)
		{
			log.Warn(message);
			return this;
		}

		/// <summary>
		/// Log a message object with the <see cref="F:log4net.Core.Level.Warn"/> level including
		///             the stack trace of the <see cref="T:System.Exception"/> passed
		///             as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param><param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="M:Warn(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Warn(object)"/><seealso cref="P:log4net.ILog.IsWarnEnabled"/>
		public IFrameworkLogger Warn(object message, Exception exception)
		{
			log.Warn(message, exception);
			return this;
		}

		/// <overloads>Log a formatted message string with the <see cref="F:log4net.Core.Level.Warn"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Warn(object)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Warn(object,Exception)"/><seealso cref="P:log4net.ILog.IsWarnEnabled"/>
		public IFrameworkLogger WarnFormat(string format, params object[] args)
		{
			log.WarnFormat(format, args);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Warn(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Warn(object)"/><seealso cref="P:log4net.ILog.IsWarnEnabled"/>
		public IFrameworkLogger WarnFormat(string format, object arg0)
		{
			log.WarnFormat(format, arg0);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Warn(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Warn(object)"/><seealso cref="P:log4net.ILog.IsWarnEnabled"/>
		public IFrameworkLogger WarnFormat(string format, object arg0, object arg1)
		{
			log.WarnFormat(format, arg1);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param><param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Warn(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Warn(object)"/><seealso cref="P:log4net.ILog.IsWarnEnabled"/>
		public IFrameworkLogger WarnFormat(string format, object arg0, object arg1, object arg2)
		{
			log.WarnFormat(format, arg0, arg1, arg2);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param><param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Warn(object)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Warn(object,Exception)"/><seealso cref="P:log4net.ILog.IsWarnEnabled"/>
		public IFrameworkLogger WarnFormat(IFormatProvider provider, string format, params object[] args)
		{
			log.WarnFormat(provider, format, args);
			return this;
		}

		/// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Error"/> level.</overloads>
		/// <summary>
		/// Logs a message object with the <see cref="F:log4net.Core.Level.Error"/> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>ERROR</c>
		///             enabled by comparing the level of this logger with the 
		///             <see cref="F:log4net.Core.Level.Error"/> level. If this logger is
		///             <c>ERROR</c> enabled, then it converts the message object
		///             (passed as parameter) to a string by invoking the appropriate
		///             <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		///             proceeds to call all the registered appenders in this logger 
		///             and also higher in the hierarchy depending on the value of the 
		///             additivity flag.
		/// </para>
		/// <para>
		/// <b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		///             to this method will print the name of the <see cref="T:System.Exception"/> 
		///             but no stack trace. To print a stack trace use the 
		///             <see cref="M:Error(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Error(object,Exception)"/><seealso cref="P:log4net.ILog.IsErrorEnabled"/>
		public IFrameworkLogger Error(object message)
		{
			log.Error(message);
			return this;
		}

		/// <summary>
		/// Log a message object with the <see cref="F:log4net.Core.Level.Error"/> level including
		///             the stack trace of the <see cref="T:System.Exception"/> passed
		///             as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param><param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="M:Error(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Error(object)"/><seealso cref="P:log4net.ILog.IsErrorEnabled"/>
		public IFrameworkLogger Error(object message, Exception exception)
		{
			log.Error(message, exception);
			return this;
		}

		/// <overloads>Log a formatted message string with the <see cref="F:log4net.Core.Level.Error"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Error(object)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Error(object,Exception)"/><seealso cref="P:log4net.ILog.IsErrorEnabled"/>
		public IFrameworkLogger ErrorFormat(string format, params object[] args)
		{
			log.ErrorFormat(format, args);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Error(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Error(object)"/><seealso cref="P:log4net.ILog.IsErrorEnabled"/>
		public IFrameworkLogger ErrorFormat(string format, object arg0)
		{
			log.ErrorFormat(format, arg0);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Error(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Error(object)"/><seealso cref="P:log4net.ILog.IsErrorEnabled"/>
		public IFrameworkLogger ErrorFormat(string format, object arg0, object arg1)
		{
			log.ErrorFormat(format, arg0, arg1);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param><param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Error(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Error(object)"/><seealso cref="P:log4net.ILog.IsErrorEnabled"/>
		public IFrameworkLogger ErrorFormat(string format, object arg0, object arg1, object arg2)
		{
			log.ErrorFormat(format, arg0, arg1, arg2);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param><param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Error(object)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Error(object,Exception)"/><seealso cref="P:log4net.ILog.IsErrorEnabled"/>
		public IFrameworkLogger ErrorFormat(IFormatProvider provider, string format, params object[] args)
		{
			log.ErrorFormat(provider, format, args);
			return this;
		}

		/// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Fatal"/> level.</overloads>
		/// <summary>
		/// Log a message object with the <see cref="F:log4net.Core.Level.Fatal"/> level.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>FATAL</c>
		///             enabled by comparing the level of this logger with the 
		///             <see cref="F:log4net.Core.Level.Fatal"/> level. If this logger is
		///             <c>FATAL</c> enabled, then it converts the message object
		///             (passed as parameter) to a string by invoking the appropriate
		///             <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		///             proceeds to call all the registered appenders in this logger 
		///             and also higher in the hierarchy depending on the value of the 
		///             additivity flag.
		/// </para>
		/// <para>
		/// <b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		///             to this method will print the name of the <see cref="T:System.Exception"/> 
		///             but no stack trace. To print a stack trace use the 
		///             <see cref="M:Fatal(object,Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <param name="message">The message object to log.</param><seealso cref="M:Fatal(object,Exception)"/><seealso cref="P:log4net.ILog.IsFatalEnabled"/>
		public IFrameworkLogger Fatal(object message)
		{
			log.Fatal(message);
			return this;
		}

		/// <summary>
		/// Log a message object with the <see cref="F:log4net.Core.Level.Fatal"/> level including
		///             the stack trace of the <see cref="T:System.Exception"/> passed
		///             as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param><param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="M:Fatal(object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Fatal(object)"/><seealso cref="P:log4net.ILog.IsFatalEnabled"/>
		public IFrameworkLogger Fatal(object message, Exception exception)
		{
			log.Fatal(message, exception);
			return this;
		}

		/// <overloads>Log a formatted message string with the <see cref="F:log4net.Core.Level.Fatal"/> level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Fatal(object)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Fatal(object,Exception)"/><seealso cref="P:log4net.ILog.IsFatalEnabled"/>
		public IFrameworkLogger FatalFormat(string format, params object[] args)
		{
			log.FatalFormat(format, args);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Fatal(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Fatal(object)"/><seealso cref="P:log4net.ILog.IsFatalEnabled"/>
		public IFrameworkLogger FatalFormat(string format, object arg0)
		{
			log.FatalFormat(format, arg0);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Fatal(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Fatal(object)"/><seealso cref="P:log4net.ILog.IsFatalEnabled"/>
		public IFrameworkLogger FatalFormat(string format, object arg0, object arg1)
		{
			log.FatalFormat(format, arg0, arg1);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal"/> level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param><param name="arg0">An Object to format</param><param name="arg1">An Object to format</param><param name="arg2">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Fatal(object,Exception)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Fatal(object)"/><seealso cref="P:log4net.ILog.IsFatalEnabled"/>
		public IFrameworkLogger FatalFormat(string format, object arg0, object arg1, object arg2)
		{
			log.FatalFormat(format, arg0, arg1, arg2);
			return this;
		}

		/// <summary>
		/// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal"/> level.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param><param name="format">A String containing zero or more format items</param><param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		///             <see cref="M:String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
		///             of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///             log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:Fatal(object)"/>
		///             methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Fatal(object,Exception)"/><seealso cref="P:log4net.ILog.IsFatalEnabled"/>
		public IFrameworkLogger FatalFormat(IFormatProvider provider, string format, params object[] args)
		{
			log.FatalFormat(provider, format, args);
			return this;
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Debug"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Debug"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// <para>
		/// This function is intended to lessen the computational cost of
		///             disabled log debug statements.
		/// </para>
		/// <para>
		/// For some ILog interface <c>log</c>, when you write:
		/// </para>
		/// <code lang="C#">
		/// log.Debug("This is entry number: " + i );
		/// </code>
		/// <para>
		/// You incur the cost constructing the message, string construction and concatenation in
		///             this case, regardless of whether the message is logged or not.
		/// </para>
		/// <para>
		/// If you are worried about speed (who isn't), then you should write:
		/// </para>
		/// <code lang="C#">
		/// if (log.IsDebugEnabled)
		///             { 
		///                 log.Debug("This is entry number: " + i );
		///             }
		/// </code>
		/// <para>
		/// This way you will not incur the cost of parameter
		///             construction if debugging is disabled for <c>log</c>. On
		///             the other hand, if the <c>log</c> is debug enabled, you
		///             will incur the cost of evaluating whether the logger is debug
		///             enabled twice. Once in <see cref="P:log4net.ILog.IsDebugEnabled"/> and once in
		///             the <see cref="M:Debug(object)"/>.  This is an insignificant overhead
		///             since evaluating a logger takes about 1% of the time it
		///             takes to actually log. This is the preferred style of logging.
		/// </para>
		/// <para>
		/// Alternatively if your logger is available statically then the is debug
		///             enabled state can be stored in a static variable like this:
		/// </para>
		/// <code lang="C#">
		/// private static readonly bool isDebugEnabled = log.IsDebugEnabled;
		/// </code>
		/// <para>
		/// Then when you come to log you can write:
		/// </para>
		/// <code lang="C#">
		/// if (isDebugEnabled)
		///             { 
		///                 log.Debug("This is entry number: " + i );
		///             }
		/// </code>
		/// <para>
		/// This way the debug enabled state is only queried once
		///             when the class is loaded. Using a <c>private static readonly</c>
		///             variable is the most efficient because it is a run time constant
		///             and can be heavily optimized by the JIT compiler.
		/// </para>
		/// <para>
		/// Of course if you use a static readonly variable to
		///             hold the enabled state of the logger then you cannot
		///             change the enabled state at runtime to vary the logging
		///             that is produced. You have to decide if you need absolute
		///             speed or runtime flexibility.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Debug(object)"/><seealso cref="M:DebugFormat(IFormatProvider, string, object[])"/>
		public bool IsDebugEnabled { get { return log.IsDebugEnabled; } }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Info"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Info"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// For more information see <see cref="P:log4net.ILog.IsDebugEnabled"/>.
		/// </remarks>
		/// <seealso cref="M:Info(object)"/><seealso cref="M:InfoFormat(IFormatProvider, string, object[])"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public bool IsInfoEnabled { get { return log.IsInfoEnabled; } }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Warn"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Warn"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// For more information see <see cref="P:log4net.ILog.IsDebugEnabled"/>.
		/// </remarks>
		/// <seealso cref="M:Warn(object)"/><seealso cref="M:WarnFormat(IFormatProvider, string, object[])"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public bool IsWarnEnabled { get { return log.IsWarnEnabled; } }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Error"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Error"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// For more information see <see cref="P:log4net.ILog.IsDebugEnabled"/>.
		/// </remarks>
		/// <seealso cref="M:Error(object)"/><seealso cref="M:ErrorFormat(IFormatProvider, string, object[])"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public bool IsErrorEnabled { get { return log.IsErrorEnabled; } }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Fatal"/> level.
		/// </summary>
		/// <value>
		/// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Fatal"/> events, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// For more information see <see cref="P:log4net.ILog.IsDebugEnabled"/>.
		/// </remarks>
		/// <seealso cref="M:Fatal(object)"/><seealso cref="M:FatalFormat(IFormatProvider, string, object[])"/><seealso cref="P:log4net.ILog.IsDebugEnabled"/>
		public bool IsFatalEnabled { get { return log.IsFatalEnabled; } }

		#endregion
	}
}
