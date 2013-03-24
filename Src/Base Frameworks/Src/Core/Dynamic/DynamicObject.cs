using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Dynamic.Proxy.Lambda;
using System.Reflection;

namespace Umc.Core.Dynamic
{
	public static class DynamicObject
	{
		static AssemblyLambda assembly;
		static IModuleLambda module;
		static ITypeLambda type;

		static DynamicObject()
		{
			assembly = new AssemblyLambda();
			{
				module = assembly.Assembly();
				{
					type = module.Module(Guid.NewGuid().ToString("N"));
				}
			}
		}

		public static Type InterfaceImplementationType(Type interfaceType)
		{
			var typeName = String.Concat("dynamic_", Guid.NewGuid().ToString("N"));

			var t = type.Public.Class(typeName, null, new Type[] { interfaceType });
			{
				t.Public.Constructor().Return();

				t.Attribute(typeof(SerializableAttribute), new object[] { });

				var properties = interfaceType.GetProperties();
				foreach ( var property in properties )
				{
					t.Public.Property(property.PropertyType, property.Name).GetSet();
				}
			}


			return t.ReleaseType();
		}

		public static Type InterfaceImplementationType<TInterface>()
		{
			return InterfaceImplementationType(typeof(TInterface));
		}
	}
}
