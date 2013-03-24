using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Dynamic.Proxy.Builder;
using Umc.Core.Reflection;
using System.Reflection;
using System.Reflection.Emit;
using Umc.Core.Dynamic.Proxy.Lambda.InType;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	/// <para>타입의 상태를 표현하는 람다 클래스 입니다.</para>
	/// </summary>
	public class TypeLambda : ITypeLambda
	{
		private TypeAccessor typeAccessor;
		private FieldAccessor fieldAccessor;
		private MethodAccessor methodAccessor;
		private AccessorInvocation accessorInvocation;


		public TypeAccessor TypeAccessor { get { return this.typeAccessor; } }
		public FieldAccessor FieldAccessor { get { return this.fieldAccessor; } }
		public MethodAccessor MethodAccessor { get { return this.methodAccessor; } }


		public IModuleLambda ModuleLambda { get; private set; }
		public TypeAttributes TypeAttribute { get; private set; }
		public TypeBuilder TypeBuilder { get; private set; }


		public TypeLambda(IModuleLambda moduleLambda)
		{
			this.ModuleLambda = moduleLambda;

			// Init
			this.typeAccessor	= new TypeAccessor(this);
			this.methodAccessor = new MethodAccessor(this);
			this.fieldAccessor	= new FieldAccessor(this);

			this.accessorInvocation = new AccessorInvocation(this, typeAccessor, methodAccessor, fieldAccessor);
		}

		private void ResetAccessorAttributes()
		{
			this.typeAccessor.TypeAttributes	= 0;
			this.methodAccessor.MethodAttribute = 0;
			this.fieldAccessor.FieldAttribute	= 0;
		}

		#region ITypeLambda
		
		public ITypeLambda Attribute(Type attributeType, params object[] param)
		{
			new TypeBuilderExtension(this.ModuleLambda.ModuleBuilder, this.TypeBuilder)
													.CreateAttribute(attributeType, param);

			return this;
		}
		
		public Operand Field(Type returnType, string name)
		{
			var operand = new FieldOperand(this, null, new CriteriaMetadataInfo(returnType, name, CriteriaMetadataToken.Field));
			operand.WriteEmit(null);

			return operand;
		}

		public IPropertyLambda Property(Type returnType, string name)
		{
			var property = new TypeBuilderExtension(this.ModuleLambda.ModuleBuilder, this.TypeBuilder)
													.CreateProperty(name, PropertyAttributes.HasDefault, returnType, null);

			return new PropertyLambda(this, property);
		}

		public ICodeLambda Method(string name)
		{
			return this.Method(typeof(void), name, Type.EmptyTypes);
		}

		public ICodeLambda Method(Type returnType, string name, Type[] argumentsTypes)
		{
			return this.Method(returnType, name, argumentsTypes, null);
		}

		public ICodeLambda Method(Type returnType, string name, Type[] argumentsTypes, MethodInfo parentMethodInfo)
		{
			var method = new TypeBuilderExtension(null, this.TypeBuilder)
							.CreateMethod(this.methodAccessor.MethodAttribute, returnType, name, argumentsTypes, null, methodAccessor.IsOverride);


			return new MethodOperand(this, method.GetILGenerator(), method.GetBaseDefinition());
		}

		public ICodeLambda Constructor()
		{
			return this.Constructor(Type.EmptyTypes);
		}

		public ICodeLambda Constructor(params Type[] argumentsTypes)
		{
			var constructorBuilder = new TypeBuilderExtension(this.ModuleLambda.ModuleBuilder, this.TypeBuilder)
										.CreateConstructor(this.methodAccessor.MethodAttribute, CallingConventions.HasThis, argumentsTypes);

			ResetAccessorAttributes();

			return new CodeLambda(this, constructorBuilder.GetILGenerator());
		}

		public ICodeLambda Constructor(IEnumerable<ParameterCriteriaMetadataInfo> parameterCriteriaMetadataInfos)
		{
			var constructorBuilder = new TypeBuilderExtension(this.ModuleLambda.ModuleBuilder, this.TypeBuilder)
										.CreateConstructor(this.methodAccessor.MethodAttribute, CallingConventions.HasThis, parameterCriteriaMetadataInfos);

			ResetAccessorAttributes();

			return new CodeLambda(this, constructorBuilder.GetILGenerator());
		}

		public ITypeLambda Class(string name)
		{
			this.TypeBuilder = new ModuleBuilderExtension(this.ModuleLambda.ModuleBuilder, null)
									.CreateType(name, this.typeAccessor.TypeAttributes);

			ResetAccessorAttributes();

			return this;
		}

		public ITypeLambda Class(string name, Type parent, Type[] interfaces)
		{
			this.TypeBuilder = new ModuleBuilderExtension(this.ModuleLambda.ModuleBuilder, null)
									.CreateType(name, this.typeAccessor.TypeAttributes, parent, interfaces ?? Type.EmptyTypes);

			ResetAccessorAttributes();

			return this;
		}

		public ITypeLambda Struct(string name)
		{
			this.typeAccessor.TypeAttributes = this.typeAccessor.TypeAttributes | AttributeConstants.TypeAttribute.Struct;

			return this.Class(name);
		}

		public ITypeLambda Interface(string name)
		{
			this.typeAccessor.TypeAttributes = AttributeConstants.TypeAttribute.Interface;

			return this.Class(name);
		}

		public IEnumLambda Enum(string name)
		{
			return this.Enum(name, typeof(int));
		}

		public IEnumLambda Enum(string name, Type underlyingType)
		{
			var enumBuilder = new ModuleBuilderExtension(this.ModuleLambda.ModuleBuilder, this.TypeBuilder)
								.CreateEnum(name, this.typeAccessor.TypeAttributes, underlyingType);

			return new EnumLambda(enumBuilder);
		}

		public ITypeLambda Delegate(Type returnType, string name, params Type[] argumentsTypes)
		{
			this.TypeBuilder = new ModuleBuilderExtension(this.ModuleLambda.ModuleBuilder, this.TypeBuilder)
														.CreateDelegate(returnType, name, argumentsTypes);

			return this;
		}

		public ITypeLambda Event(Type delegateType, string name)
		{
			throw new NotImplementedException();
		}

		public Type ReleaseType()
		{
			if (this.TypeBuilder == null)
			{
				throw new NullReferenceException(this.TypeBuilder.GetType().Name);
			}

			return this.TypeBuilder.CreateType();
		} 
		#endregion

		#region IAccessorLambda
		public ITypeLambda Public		{ get { var temp = this.accessorInvocation.Public; return this; } }
		public ITypeLambda Internal		{ get { var temp = this.accessorInvocation.Internal; return this; } }
		public ITypeLambda Protected	{ get { var temp = this.accessorInvocation.Protected; return this; } }
		public ITypeLambda Private		{ get { var temp = this.accessorInvocation.Private; return this; } }
		public ITypeLambda Static		{ get { var temp = this.accessorInvocation.Static; return this; } }
		public ITypeLambda ReadOnly		{ get { var temp = this.accessorInvocation.ReadOnly; return this; } }
		public ITypeLambda Abstract		{ get { var temp = this.accessorInvocation.Abstract; return this; } }
		public ITypeLambda Sealed		{ get { var temp = this.accessorInvocation.Sealed; return this; } }
		public ITypeLambda Override		{ get { var temp = this.accessorInvocation.Override; return this; } }
		public ITypeLambda Virtual		{ get { var temp = this.accessorInvocation.Virtual; return this; } }
		#endregion
	}
}
