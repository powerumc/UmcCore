using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

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
			var attributes = GetCustomAttributesEx<T>(type, inhert);

			if( attributes == null || !attributes.Any())
				return default(T);

			return attributes.FirstOrDefault();
		}


		public static IEnumerable<T> GetCustomAttributesEx<T>(this Type type) where T : class
		{
			return GetCustomAttributesEx<T>(type, false);
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static IEnumerable<T> GetCustomAttributesEx<T>(this Type type, bool inhert) where T : class
		{
			object[] attributes = type.GetCustomAttributes(typeof(T), inhert);

			if( attributes.Length == 0 )
				return null;

			return attributes.Cast<T>();
		}

		public static void GetAllProperties(this Type type, IList<PropertyInfo> propertyInfos)
		{
			foreach (var p in type.GetProperties())
			{
				propertyInfos.Add(p);
			}

			foreach (var i in type.GetInterfaces())
			{
				GetAllProperties(i, propertyInfos);
			}
		}

		public static bool IsNullableType(this Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>);
		}

		public static string AbbreviationString(this Type type)
		{
			var str = type.FullName;
			if (str.Length < 40) 
				return str;

			var arr = str.Split('.');
			var sb = new StringBuilder(100);
			var newString = String.Join(".", arr.Take(arr.Length - 1).Select(o => o.First().ToString(CultureInfo.InvariantCulture)).ToArray());

			return string.Concat(newString, ".", arr[arr.Length - 1]);
		}
	}
}
