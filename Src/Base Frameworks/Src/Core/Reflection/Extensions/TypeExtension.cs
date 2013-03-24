using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Umc.Core
{
	public static class ReflectionTypeExtension
	{
		public static IEnumerable<object> GetValueOfProperty(this Type type, string propertyName)
		{
			if (type.IsNull()) yield break;

			var obj = Activator.CreateInstance(type) as IEnumerable;

			foreach (var o in obj)
			{
				yield return o.GetType().GetProperty(propertyName).GetValue(o, null);
			}
		}
	}
}
