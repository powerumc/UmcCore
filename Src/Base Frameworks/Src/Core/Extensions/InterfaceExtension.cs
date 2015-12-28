using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Dynamic;

namespace System
{
	public static class Interface
	{
		public static T New<T>() where T : class
		{
			if (typeof (T).IsInterface)
			{
				var type = DynamicObject.InterfaceImplementationType<T>();
				var o = Activator.CreateInstance(type);
				return o as T;
			}

			throw new NotSupportedException("not interface type");
		}
	}
}
