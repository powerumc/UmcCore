using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace Umc.Core
{
	public static class StringExtensions
	{
		public static string DefaultIfEmpty(this string str, string defaultString)
		{
			return string.IsNullOrEmpty(str) ? defaultString : str;
		}

		public static DateTime ToDateTime(this string str)
		{
			return DateTime.Parse(str);
		}

		public static byte[] EncodingGetBytes(this string str)
		{
			return EncodingGetBytes(str, Encoding.UTF8);
		}

		public static byte[] EncodingGetBytes(this string str, Encoding encoding)
		{
			return encoding.GetBytes(str);
		}

		public static string ToSafeHtmlString(this string str)
		{
			return HttpUtility.HtmlEncode(str);
		}
	}
}
