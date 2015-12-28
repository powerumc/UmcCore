
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Umc.Core.IoC.Catalog;

namespace Umc.Core.IoC.Configuration
{
	/// <summary>
	///		<see cref="Type"/> 을 검사하여 <see cref="Type"/> 간의 종속성을 분석하는 Visitor 클래스입니다.
	/// </summary>
	public class FrameworkDependencyVisitor
	{
		private IEnumerable<Type> types;
		private UmcCoreIoCElement root;

		private const BindingFlags binding = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

		private FrameworkDependencyVisitor()
		{
			root = new UmcCoreIoCElement();
			root.containers = new List<ContainerElement>();
			root.containers.Add(new ContainerElement());
			root.containers.First().register = new List<RegisterElement>();
			root.containers.First().dynamic = new List<DynamicElement>();
		}

		public FrameworkDependencyVisitor(IEnumerable<Type> types)
			: this()
		{
			this.types = types;
		}

		public FrameworkDependencyVisitor(DependencyCatalog catalog)
			: this()
		{
			this.types = catalog.GetMatchingTypes();
		}

		public UmcCoreIoCElement VisitTypes()
		{
			foreach (var type in this.types)
			{
				if (Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false) || type.Name.StartsWith("<>")) continue;

				this.Visit(type);
			}

			return root;
		}

		private void Visit(MemberInfo info)
		{
			switch (info.MemberType)
			{
				case MemberTypes.TypeInfo:
					var type = (Type)info;

					if ( type.IsInterface )
					{
						this.VisitDynamicType(type, root.containers.First().dynamic);
					}
					else
					{
						this.VisitType(type, root.containers.First().register);
					}
					break;

				case MemberTypes.NestedType:
					this.VisitType((Type)info, root.containers.First().register);
					break;
			}
		}

		private IList<ParamElement> VisitParam(ParameterInfo[] @params)
		{
			IList<ParamElement> elements = new List<ParamElement>();

			foreach (var param in @params)
			{
				ParamElement paramElement = new ParamElement()
				{
					name = param.Name
				};

				var paramAttribute = param.GetDependencyDefaultValueOnParameter();
				if (paramAttribute != null)
				{
					ValueElement valueElement = new ValueElement()
					{
						value = paramAttribute.Value != null ? paramAttribute.Value.ToString() : null,
						type = paramAttribute.Value.GetType().ToString()
					};
					paramElement.Item = valueElement;

					elements.Add(paramElement);
					continue;
				}

				var dependencyAttribute = param.GetDependencyInjectionOnParameter();
				if (dependencyAttribute != null && dependencyAttribute.DefaultValue != null)
				{
					ValueElement valueElement = new ValueElement()
					{
						value = dependencyAttribute.DefaultValue != null ? dependencyAttribute.DefaultValue.ToString() : null,
						type = dependencyAttribute.DefaultValue.GetType().ToString()
					};

					paramElement.Item = valueElement;
					elements.Add(paramElement);
				}
				else if (dependencyAttribute != null)
				{
					DependencyElement dependencyElement = new DependencyElement()
					{
						key = dependencyAttribute.Key != null ? dependencyAttribute.Key.ToString() : null,
						typeOfContract = param.ParameterType.AssemblyQualifiedName
					};

					paramElement.Item = dependencyElement;
					elements.Add(paramElement);
				}
				else
				{
					DependencyElement dependencyElement = new DependencyElement()
					{
						key = null,
						typeOfContract = param.ParameterType.AssemblyQualifiedName
					};

					paramElement.Item = dependencyElement;
					elements.Add(paramElement);
				}
			}

			return elements;
		}


		private void VisitMethods(MethodInfo method, RegisterElement element)
		{
			var attribute = method.GetDependencyInjectionOnMethod();
			if( attribute == null ) return;

			MethodElement methodElement = new MethodElement()
			{
				name = method.Name
			};

			var param = this.VisitParam(method.GetParameters());

			methodElement.param = new List<ParamElement>();
			methodElement.param.AddRange(param);

			if( element.method == null ) element.method = new List<MethodElement>();
			element.method.Add(methodElement);

			return;
		}

		private void VisitProperty(PropertyInfo property, RegisterElement element)
		{
			if (element.property == null)
				element.property = new List<PropertyElement>(); 
			
			var attribute = property.GetDependencyInjectionOnProperty();
			var defaultValueAttribute = property.GetDefaultValueOnProperty();

			PropertyElement e = new PropertyElement();
			e.name            = property.Name;

			if (attribute != null && attribute.DefaultValue != null)
			{
				e.Item = new ValueElement() { 
					value = attribute.DefaultValue != null ? attribute.DefaultValue.ToString() : null,
					type = attribute.DefaultValue.GetType().ToString()
				};
				element.property.Add(e);
			}
			else if( attribute != null )
			{
				e.name = property.Name;

				e.Item = new DependencyElement()
				{
					typeOfContract = property.PropertyType.AssemblyQualifiedName,
					key = attribute.Key != null ? attribute.Key.ToString() : null
				};
				element.property.Add(e);
			}
			else if (defaultValueAttribute != null && defaultValueAttribute.Value != null )
			{
				e.Item = new ValueElement() { 
					value = defaultValueAttribute.Value != null ? defaultValueAttribute.Value.ToString() : null,
					type = defaultValueAttribute.Value.GetType().ToString()
				};
				element.property.Add(e);
			}
		}

		private void VisitConstructor(ConstructorInfo constructor, RegisterElement element)
		{
			var attributes = constructor.GetDependencyInjectionOnConstructor();
			if( attributes == null || attributes.Count() > 1 )
				throw new FrameworkDependencyException();

			var param = this.VisitParam(constructor.GetParameters());

			if( param.Count == 0 ) return;

			element.constructor = new List<ParamElement>();
			element.constructor.AddRange(param);
		}

		private void VisitDynamicType(Type type, IList<DynamicElement> dynamics)
		{
			var attribute = type.GetDynamicAttribute();
			if ( attribute == null ) return;

			DynamicElement e = new DynamicElement();
			e.type = type.AssemblyQualifiedName;
			e.lifetime = attribute.Lifetime;

			dynamics.Add(e);
		}

		private void VisitType(Type type, IList<RegisterElement> registers)
		{
			var attributes = type.GetDependencyContractsOfType();
			if (attributes == null) attributes = new[] {new DependencyContractAttribute(type, LifetimeFlag.PerThread)};

			foreach (var attribute in attributes)
			{
				RegisterElement e = new RegisterElement();
				e.contract        = attribute.TypeOfContract != null ? attribute.TypeOfContract.AssemblyQualifiedName : type.AssemblyQualifiedName;
				e.key             = attribute.Key != null ? attribute.Key : null;
				e.dependencyTo    = type.AssemblyQualifiedName;
				if (attribute.LifetimeFlag == LifetimeFlag.Default)
				{
					
				}
				else
				{
					var lifetimeType = (LifetimeFlagType)Enum.Parse(typeof(LifetimeFlagType), attribute.LifetimeFlag.ToString());
					e.lifetime = new LifetimeElement()
					{
						type = lifetimeType
					};
				}



				type.GetConstructors(binding).ToList().ForEach(o => this.VisitConstructor(o, e));
				type.GetProperties(binding).ToList().ForEach(o => this.VisitProperty(o, e));
				type.GetMethods(binding).ToList().ForEach(o => this.VisitMethods(o, e));



				registers.Add(e);
			}

			var nestedTypes = type.GetNestedTypes(binding);
			if (nestedTypes.Count() > 0)
			{
				foreach (var nestedType in nestedTypes)
				{
					this.VisitType(nestedType, registers);
				}
			}
		}
	}
}
