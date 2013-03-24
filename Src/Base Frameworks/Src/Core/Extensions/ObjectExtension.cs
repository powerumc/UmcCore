using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Umc.Core;

namespace System
{
	/// <summary>
	///		<see cref="Object"/> 객체의 확장 클래스 입니다.
	/// </summary>
    public static class ObjectExtension
    {
		#region IsNull and IsNullOrEmpty
		/// <summary>	
		/// 	객체가 NULL 인지 아닌지 여부를 검사합니다.
		/// </summary>
		/// <param name="object">	NULL 여부를 검사할 객체입니다. </param>
		/// <returns>	
		/// 	객체가 NULL 이면 True, NULL 이 아니면 False 를 반환합니다.
		/// </returns>
		public static bool IsNull(this object @object)
		{
			return @object == null;
		}

		public static bool IsNotNull(this object @object)
		{
			return @object != null;
		}

		public static bool IsNullOrEmpty(this string str)
		{
			return String.IsNullOrEmpty(str);
		} 
		#endregion

		#region Copy Object
		public static T DeepCopy<T>(this T @object)
		{
			return (T)@object.ToBinaryBytes().ToObject();
		} 
		#endregion

		#region ParseSomething

		public static short ParseToShort(this object obj)
		{
			return Convert.ToInt16(obj);
		}

		public static short ParseToShort(this object obj, short defaultValue)
		{
			short value = 0;
			var result = TryParseToShort(obj, out value);

			if (result == false)
				return defaultValue;

			return value;
		}

		public static bool TryParseToShort(this object obj, out short result)
		{
			if (obj.IsNull()) throw new ArgumentNullException("obj");

			return short.TryParse(obj.ToString(), out result);
		}

		public static T TypeConvertTo<T>(this object obj)
		{
			var result = TypeConvertTo<T>(obj, default(T));

			return (T)result;
		}

		public static T TypeConvertTo<T>(this object obj, T defaultValue)
		{
			if (obj == null) return defaultValue;

			var converter = TypeDescriptor.GetConverter(typeof(T));

			var result = (T)converter.ConvertTo(obj, typeof(T));
			if (result == null) return defaultValue;

			return result;
		}

		public static IEnumerable<T> TypeConvertToArray<T>(this object[] arrObj)
		{
			foreach (var obj in arrObj)
			{
				yield return TypeConvertTo<T>(obj);
			}
		}

		public static IEnumerable<T> TypeConvertToArray<T>(this object[] arrObj, T defaultValue)
		{
			foreach (var obj in arrObj)
			{
				yield return TypeConvertTo<T>(obj, defaultValue);
			}
		}

		#endregion
    }
}
