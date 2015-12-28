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
			return InterfaceImplementationType(new [] { interfaceType});
		}

		public static Type InterfaceImplementationType(Type[] interfaceTypes)
		{
			return InterfaceImplementationType(interfaceTypes, null);
		}

		public static Type InterfaceImplementationType(Type[] interfaceTypes, Func<ITypeLambda, Type, PropertyInfo, bool> baseInterfacePropertyAction)
		{
			return InterfaceImplementationType(interfaceTypes, null, baseInterfacePropertyAction);
		}

		public static Type InterfaceImplementationType(Type[] interfaceTypes, Type baseType, Func<ITypeLambda, Type, PropertyInfo, bool> baseInterfacePropertyAction)
		{
			var typeName = String.Concat("dynamic_", Guid.NewGuid().ToString("N"));

			var t = type.Public.Class(typeName, baseType, interfaceTypes);
			{
				t.Public.Constructor().Return();
				t.Attribute(typeof(SerializableAttribute), new object[] { });

				foreach (var iftype in interfaceTypes ?? Type.EmptyTypes)
				{
					var properties = iftype.GetProperties();
					foreach (var property in properties)
					{
						var isImpl = false;
						if (baseInterfacePropertyAction != null)
							isImpl = baseInterfacePropertyAction(t, iftype, property);

						if (!isImpl)
						{
							var propLambda = t.Public.Property(property.PropertyType, property.Name).GetSet();
							//var attributes = property.GetCustomAttributes(true).ToList();
							//foreach (var attr in attributes)
							//{
							//	var constructor = attr.GetType().GetConstructors()[0];
							//	var parameters = constructor.GetParameters().Select(o => o.ParameterType);

							//	propLambda.Attribute(attr.GetType(), new object[] {} );
							//}
						}
					}

					foreach (var tt in iftype.GetInterfaces())
					{
						properties = tt.GetProperties();
						foreach (var property in properties)
						{
							var isImpl = false;
							if (baseInterfacePropertyAction != null)
								isImpl = baseInterfacePropertyAction(t, tt, property);

							if (!isImpl)
							{
								var propLambda = t.Public.Property(property.PropertyType, property.Name).GetSet();
								//var attributes = property.GetCustomAttributes(true).ToList();
								//foreach (var attr in attributes)
								//{
								//	propLambda.Attribute(attr.GetType(), new object[] { });
								//}
							}
						}
					}
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
