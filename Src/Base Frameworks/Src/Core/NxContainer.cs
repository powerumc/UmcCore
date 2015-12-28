using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Dynamic;
using Umc.Core.IoC;

namespace Umc.Core.Games
{
	public class NxContainer
	{
		public static IFrameworkContainer container { get; set; }

		static NxContainer()
		{
			container = new FrameworkContainerForUnity();
		}

		public static object Resolve(Type type)
		{
			ensureObject(null, type, type, LifetimeFlag.PerCall);
			return container.Resolve(type);
		}

		public static object Resolve(string key, Type type)
		{
			ensureObject(key, type, type, LifetimeFlag.PerCall);
			return container.Resolve(key, type);
		}

		public static TContract Resolve<TContract>()
		{
			ensureObject(null, typeof(TContract), typeof(TContract), LifetimeFlag.PerCall);
			return container.Resolve<TContract>();
		}

		public static TContract Resolve<TContract>(string key)
		{
			ensureObject(null, typeof(TContract), typeof(TContract), LifetimeFlag.PerCall);
			return container.Resolve<TContract>(key);
		}

		private static void ensureObject(string key, Type contractType, Type implementType, LifetimeFlag lifetime)
		{
			if (Attribute.IsDefined(contractType, typeof (DynamicAttribute)) || (!container.IsRegisted(contractType) && contractType.IsInterface))
			{
				var dynamicType = DynamicObject.InterfaceImplementationType(contractType);
				container.RegisterType(contractType, dynamicType, LifetimeFlag.PerCall);
			}

			if (!container.IsRegisted(contractType))
			{
				container.RegisterType(key, contractType, implementType, lifetime);
			}
		}
	}
}
