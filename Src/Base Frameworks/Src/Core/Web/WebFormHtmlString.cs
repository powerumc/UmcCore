using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Web
{
	public class WebFormHtmlString
	{
		private readonly string _value;
		private static readonly WebFormHtmlString Empty = Create("");

		private WebFormHtmlString(string value)
		{
			_value = value ?? "";
		}

		public static WebFormHtmlString Create()
		{
			return Create("");
		}

		public static WebFormHtmlString CreateFormat(string format, params object[] args)
		{
			return Create(string.Format(format, args));
		}

		public static WebFormHtmlString Create(string value)
		{
			return new WebFormHtmlString(value);
		} 

		public static implicit operator WebFormHtmlString(string value)
		{
			return Create(value);
		}

		public override string ToString()
		{
			return _value;
		}

		public bool IsEmpty { get { return string.IsNullOrEmpty(this._value) || this == Empty; } }

	}
}
