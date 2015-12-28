using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Umc.Core.Web.Modules
{
	/// <summary>
	/// 서버의 추적데이터를 브라우저에 출력한다.
	/// </summary>
	public class JavaScriptTraceModule : IHttpModule
	{
		private HttpApplication app;
		private HttpResponse response;
		private TraceStream _traceStream;
		private JavaScriptTraceListner _listener;

		public bool IsSupport
		{
			get
			{
				var isPage = app.Request.Url.AbsolutePath.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase);
				var isAjax = (app.Request.Headers["X-Requested-With"] ?? "").Contains("XMLHttpRequest");
				return isPage && !isAjax;
			}
		}

		public void Init(HttpApplication context)
		{
#if DEBUG
			this.app = context;
			context.BeginRequest += ContextOnBeginRequest;
			context.EndRequest += ContextOnEndRequest; 
#endif
		}

		private void ContextOnBeginRequest(object sender, EventArgs eventArgs)
		{
#if DEBUG
			if (!IsSupport) return;
			_listener = new JavaScriptTraceListner();
			if (Debug.Listeners.Contains(_listener) == false)
				Debug.Listeners.Add(_listener);

			this.response = app.Response;
			_traceStream = new TraceStream(app.Context.Response.Filter, this._listener);
			response.Filter = _traceStream; 
#endif
		}

		private void ContextOnEndRequest(object sender, EventArgs eventArgs)
		{
#if DEBUG
			if (!IsSupport) return; 
			_listener.Clear();
			Debug.Listeners.Remove(_listener);
			this._listener.Dispose();
			this._listener = null; 
#endif
		}

		public void Dispose()
		{
		}
	}

	public class TraceStream : MemoryStream
	{
		private readonly Stream stream;
		private readonly JavaScriptTraceListner _listener;
		private readonly StringBuilder htmlBuilder = new StringBuilder(2048);

		public TraceStream(Stream _stream, JavaScriptTraceListner _listener)
		{
			this.stream = _stream;
			this._listener = _listener;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
#if DEBUG
			var html = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
			if (html.TrimEnd(' ', '\r', '\n').EndsWith("</html>", true, CultureInfo.CurrentCulture))
			{
				htmlBuilder.Append(html);
				htmlBuilder.AppendLine("<script type='text/javascript'>");
				htmlBuilder.AppendLine("(function() {");
				htmlBuilder.AppendLine("    var _write = function(str) { if (console && console.log) { console.log(str); } };");
				htmlBuilder.AppendLine("    _write(\"\\");
				var enumerable = _listener.Logs.ToList();
				foreach (var log in enumerable)
				{
					//var logData = log.EncodingGetBytes(Encoding.Unicode);
					//for (var i = 0; i < log.Length; i += 2)
					//{
					//	htmlBuilder.AppendFormat("\\u{0:x4}", BitConverter.ToUInt16(logData, i));
					//}
					htmlBuilder.Append(log.Replace("\\", "\\\\").Replace("\"", "\\\""));
					htmlBuilder.Append("\\n");
				}
				htmlBuilder.AppendLine("\");");
				htmlBuilder.AppendLine("})();");
				htmlBuilder.AppendLine("</script>");

				var htmlBuffer = Encoding.UTF8.GetBytes(htmlBuilder.ToString());
				this.stream.Write(htmlBuffer, 0, htmlBuffer.Length);
			}
			else
			{
				htmlBuilder.Append(html);
			}
#endif
		}
	}
}
