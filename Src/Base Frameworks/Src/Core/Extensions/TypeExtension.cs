using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace System
{

	/// <summary>	
	/// 	<see cref="Type"/> 클래스의 확장 메서드를 담는 클래스 입니다.
	/// </summary>
	public static class TypeExtension
	{

		/// <summary>	
		/// 	<see cref="Type"/> 에서 메타데이터 특성을 가져옵니다.
		/// </summary>
		/// <typeparam name="T">	메타데이터 특성의 타입입니다. </typeparam>
		/// <param name="type">	메타데이터 특성을 가져올 <see cref="Type"/> 객체입니다.</param>
		/// <returns>	
		/// 	메타데이터의 특성을 가져옵니다. 만약, 특성이 없으면 null 을 반환합니다.
		/// </returns>
		public static T GetCustomAttributeEx<T>(this Type type) where T : class
		{
			return GetCustomAttributeEx<T>(type, false);
		}

		public static T GetCustomAttributeEx<T>(this Type type, bool inhert) where T : class
		{
			IEnumerable<T> attributes = GetCustomAttributesEx<T>(type, inhert);

			if( attributes == null || attributes.Count() == 0)
				return default(T);

			return attributes.FirstOrDefault();
		}


		public static IEnumerable<T> GetCustomAttributesEx<T>(this Type type) where T : class
		{
			return GetCustomAttributesEx<T>(type, false);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static IEnumerable<T> GetCustomAttributesEx<T>(this Type type, bool inhert) where T : class
		{
			object[] attributes = type.GetCustomAttributes(typeof(T), inhert);

			if( attributes.Length == 0 )
				return null;

			return attributes.Cast<T>();
		}

		public static bool IsNumberType(this Type type)
		{
			if (type.IsArray || type is ICollection) return false;

			switch (Type.GetTypeCode(type))
			{
				case TypeCode.Boolean:
				case TypeCode.DateTime:
				case TypeCode.DBNull:
				case TypeCode.Empty:
				case TypeCode.String: return false;
			}

			return true;
		}
	}
}
