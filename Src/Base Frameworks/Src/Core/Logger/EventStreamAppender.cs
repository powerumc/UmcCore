using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using log4net;
using log4net.Appender;
using log4net.Core;

namespace Umc.Core.Logger
{
	public class EventStreamAppender : IBulkAppender, IOptionHandler
	{
		private const int Capacity = 200;
		private static readonly string[] messageList = new string[Capacity];
		private static readonly object obj = new object();

		private static int head = 0;
		private static int offset = 0;

		public EventStreamAppender()
		{
			this.Name = this.GetType().Name;
		}

		public static string GetLogs()
		{
			var sb = new StringBuilder();
			while (true)
			{
				if (head >= offset) break;

				var msg = messageList[head];
				if (msg == null)
				{
					Interlocked.Increment(ref head);
					continue;
				};
				
				sb.Append(msg);

				Interlocked.Increment(ref head);
				if (head >= Capacity)
				{
					Interlocked.Exchange(ref head, 0);
				}
			}

			return sb.ToString();
		}

		#region Implementation of IAppender

		/// <summary>
		/// Closes the appender and releases resources.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Releases any resources allocated within the appender such as file handles, 
		///             network connections, etc.
		/// </para>
		/// <para>
		/// It is a programming error to append to a closed appender.
		/// </para>
		/// </remarks>
		public void Close()
		{
		}

		/// <summary>
		/// Log the logging event in Appender specific way.
		/// </summary>
		/// <param name="loggingEvent">The event to log</param>
		/// <remarks>
		/// <para>
		/// This method is called to log a message into this appender.
		/// </para>
		/// </remarks>
		public void DoAppend(LoggingEvent loggingEvent)
		{
			if (head >= Capacity)
			{
				Interlocked.Exchange(ref head, 0);
			}

			messageList[offset] = loggingEvent.RenderedMessage;
			Interlocked.Increment(ref offset);
		}

		/// <summary>
		/// Gets or sets the name of this appender.
		/// </summary>
		/// <value>
		/// The name of the appender.
		/// </value>
		/// <remarks>
		/// <para>
		/// The name uniquely identifies the appender.
		/// </para>
		/// </remarks>
		public string Name { get; set; }

		/// <summary>
		/// Log the array of logging events in Appender specific way.
		/// </summary>
		/// <param name="loggingEvents">The events to log</param>
		/// <remarks>
		/// <para>
		/// This method is called to log an array of events into this appender.
		/// </para>
		/// </remarks>
		public void DoAppend(LoggingEvent[] loggingEvents)
		{
			foreach (var logging in loggingEvents)
			{
				DoAppend(logging);
			}
		}

		#endregion

		#region Implementation of IOptionHandler

		/// <summary>
		/// Activate the options that were previously set with calls to properties.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This allows an object to defer activation of its options until all
		///             options have been set. This is required for components which have
		///             related options that remain ambiguous until all are set.
		/// </para>
		/// <para>
		/// If a component implements this interface then this method must be called
		///             after its properties have been set before the component can be used.
		/// </para>
		/// </remarks>
		public void ActivateOptions()
		{
		}

		#endregion
	}
}
