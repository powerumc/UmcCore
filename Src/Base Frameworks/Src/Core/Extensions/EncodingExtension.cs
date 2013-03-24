using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	public static class EncodingExtension
	{
		public static Encoding KS_C_5601_1987 { get { return Encoding.GetEncoding("ks_c_5601-1987"); } }

		#region Base64

		/// <summary>
		/// <see cref="Byte"/> 배열을 Base64 인코딩으로 변환합니다.
		/// </summary>
		/// <param name="bytes"><see cref="Byte"/> 배열입니다.</param>
		/// <returns>Base64 인코딩된 문자열 입니다.</returns>
		public static string ToBase64(this byte[] bytes)
		{
			if (bytes == null) return null;
			
			return Convert.ToBase64String(bytes);
		}

		/// <summary>
		///		<see cref="Byte"/> 배열을 Base64 인코딩으로 변환합니다.
		/// </summary>
		/// <param name="bytes">><see cref="Byte"/> 배열입니다.</param>
		/// <param name="option">인코딩 옵션을 <see cref="Base64FormattingOptions"/> 객체에 설정합니다.</param>
		/// <returns>Base64 인코딩된 문자열 입니다.</returns>
		public static string ToBase64(this byte[] bytes, Base64FormattingOptions option)
		{
			if (bytes == null) return null;

			return Convert.ToBase64String(bytes, option);
		}

		public static byte[] FromBase64(this string base64String)
		{
			if (base64String == null) return null;
			
			return Convert.FromBase64String(base64String);
		}

		public static byte[] FromBase64(this string base64String, byte[] defaultIfEmpty)
		{
			try
			{
				return FromBase64(base64String);
			}
			catch
			{
				return defaultIfEmpty;
			}
		}

		public static bool CanFromBase64(this string base64String)
		{
			try
			{
				FromBase64(base64String);
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region ConvertEncoding
		/// <summary>	<see cref="Byte"/> 배열을 <paramref name="destinationEncoding"/> 인코딩으로 변환합니다. </summary>
		///
		/// <param name="bytes">			  	<see cref="Byte"/> 배열입니다. </param>
		/// <param name="sourceEncoding">	  	원본 인코딩입니다. </param>
		/// <param name="destinationEncoding">	변환할 인코딩입니다. </param>
		///
		/// <returns>	<paramref name="destinationEncoding"/> 인코딩으로 변환된 <see cref="Byte"/> 배열입니다. </returns>

		public static byte[] ConvertEncoding(this byte[] bytes, Encoding sourceEncoding, Encoding destinationEncoding)
		{
			return Encoding.Convert(sourceEncoding, destinationEncoding, bytes);
		}

		/// <summary>	<see cref="Byte"/> 배열을 <paramref name="destinationEncoding"/> 인코딩으로 변환합니다. </summary>
		///
		/// <param name="str">				  	인코딩을 변환할 문자열입니다. </param>
		/// <param name="sourceEncoding">	  	원본 인코딩입니다. </param>
		/// <param name="destinationEncoding">	변환할 인코딩입니다. </param>
		///
		/// <returns>	<paramref name="destinationEncoding"/> 인코딩으로 변환된 <see cref="Byte"/> 배열입니다. </returns>
		public static string ConvertEncoding(this string str, Encoding sourceEncoding, Encoding destinationEncoding)
		{
			var sourceContent = sourceEncoding.GetBytes(str);
			var convertedContent = ConvertEncoding(sourceContent, sourceEncoding, destinationEncoding);

			return destinationEncoding.GetString(convertedContent);
		} 
		#endregion
	}
}
