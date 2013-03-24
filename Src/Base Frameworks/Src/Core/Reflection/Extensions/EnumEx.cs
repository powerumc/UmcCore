using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Reflection
{
	public static class EnumEx
	{
		public static TEnum ToType<TEnum>(string value)
		{
			if( String.IsNullOrEmpty(value))
				throw new ArgumentException("value");

			return (TEnum)Enum.Parse(typeof(TEnum), value);
		}
	}
}
