using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Umc.Core.IoC;

namespace System
{
	public static class IoCExtensions
	{
		public static T New<T>(this T type, IFrameworkContainer container)
		{
			var resolve = container.Resolve<T>();

			if (resolve == null)
			{
				container.RegisterType<T>();
			}

			return container.Resolve<T>();
		}
	}
}
