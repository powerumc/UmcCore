using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

using System.Web.Script.Serialization;
using Umc.Core.Web.Script;

namespace System
{
	public static class ObjectExtensions
	{
		public static string ToJson<T>(this T @object)
		{
			return new JavaScriptSerializer().Serialize(@object);
		}

		public static T ToObjectFromJson<T>(this object @object)
		{
			try
			{
				return new JavaScriptSerializer().Deserialize<T>(@object.ToString());
			}
			catch (Exception)
			{
				if (@object == null) throw new NullReferenceException("object");
				Debug.WriteLine(string.Format("ToObjectFromJson: {0}", @object.ToString()));

				var serializer = new DataContractJsonSerializer(typeof (T));
				using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(@object.ToString())))
				{
					var obj = serializer.ReadObject(ms);
					return (T) obj;
				}
			}
		}

		public static object ToObjectFromJson(this object @object)
		{
			return ToObjectFromJson<object>(@object);
		}

		public static T DefaultValueIfEmpty<T>(this object @object)
		{
			if (@object == DBNull.Value) return default(T);

			try
			{
				return (T)(@object ?? default(T));
			}
			catch
			{
				return (T)TypeDescriptor.GetConverter(typeof (T)).ConvertFrom(@object);
			}
		}
	}
}
