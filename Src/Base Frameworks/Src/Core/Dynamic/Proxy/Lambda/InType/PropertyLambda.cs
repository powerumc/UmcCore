using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using Umc.Core.Dynamic.Proxy.Builder;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda.InType
{
	public class PropertyLambda : IPropertyLambda
	{

		public ITypeLambda TypeLambda { get; private set; }
		private PropertyBuilder propertyBuilder;

		public PropertyLambda(ITypeLambda typeLambda, PropertyBuilder propertyBuilder)
		{
			this.TypeLambda = typeLambda;
			this.propertyBuilder = propertyBuilder;
		}


		#region IPropertyLambda 멤버

		public ICodeLambda Get()
		{
			var type = new TypeBuilderExtension(null, this.TypeLambda.TypeBuilder);
			var methodAttr = this.TypeLambda.MethodAccessor.MethodAttribute | MethodAttributes.NewSlot | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
			var method = type.CreateMethod(methodAttr, propertyBuilder.PropertyType, String.Concat("get_", propertyBuilder.Name), Type.EmptyTypes, null, false);
			
			propertyBuilder.SetGetMethod(method);

			return new CodeLambda(this.TypeLambda, method.GetILGenerator());
		}

		public ICodeLambda Set()
		{
			var type = new TypeBuilderExtension(null, this.TypeLambda.TypeBuilder);

			var methodAttr = this.TypeLambda.MethodAccessor.MethodAttribute | MethodAttributes.NewSlot | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
			var method = type.CreateMethod(methodAttr, typeof(void), String.Concat("set_", propertyBuilder.Name), new Type[] { propertyBuilder.PropertyType }, null, false);
			method.DefineParameter(1, ParameterAttributes.HasDefault, "value");

			propertyBuilder.SetSetMethod(method);

			return new CodeLambda(this.TypeLambda, method.GetILGenerator());
		}

		public ITypeLambda GetSet()
		{
			this.TypeLambda.FieldAccessor.FieldAttribute = FieldAttributes.Private;
			var field = this.TypeLambda.Field(this.propertyBuilder.PropertyType, String.Concat("__", propertyBuilder.Name));
			
			var get = this.Get();
			{
				get.Return(field);
			}

			var set = this.Set();
			{
				set.IL.Emit(OpCodes.Ldarg_0);
				set.IL.Emit(OpCodes.Ldarg_1);
				set.IL.Emit(OpCodes.Stfld, ( (IValuable<FieldBuilder>)field ).Value);
				
				set.Return();
			}

			return this.TypeLambda;
		}

		#endregion

		#region IAccessorLambda<IAccessorLambda> 멤버

		public IAccessorLambda Public
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda Internal
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda Protected
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda Private
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda Static
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda ReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda Abstract
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda Sealed
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda Override
		{
			get { throw new NotImplementedException(); }
		}

		public IAccessorLambda Virtual
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}
