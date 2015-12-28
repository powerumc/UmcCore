using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Script.Serialization;

namespace Umc.Core.Web.Script
{
	public class DateTimeJavaScriptConverter : JavaScriptConverter
	{
		private IEnumerator enumerator;

		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			var result = new Dictionary<string, object>();
			if (obj is DateTime)
			{
				return result;
			}

			var enumerable = obj as IEnumerable;
			if (enumerable != null)
			{
				enumerator = enumerable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					foreach (var prop in enumerator.Current.GetType().GetProperties())
					{
						var value = "";
						if (prop.PropertyType == typeof(DateTime))
							value = ((DateTime)prop.GetValue(enumerator.Current, null)).ToLongDateString();
						else
						{
							value = prop.GetValue(enumerator.Current, null).ToString();
						}
						result.Add(prop.Name, value);
					}
				}
			}

			return result;
		}

		public override IEnumerable<Type> SupportedTypes
		{
			get { return new[] { typeof(object)}; }
		}
	}
}
