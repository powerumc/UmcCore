using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using Umc.Core.Reflection;

namespace Umc.Core.Dynamic.Proxy.Builder
{

	/// <summary>	
	/// 	<see cref="ModuleBuilder"/> 의 확장 클래스 입니다.
	/// </summary>
	internal class ModuleBuilderExtension : BuilderExtensionBase
	{
		public ModuleBuilderExtension(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
			: base(moduleBuilder, typeBuilder)
		{
		}


		/// <summary>	
		/// 	타입을 생성합니다.
		/// </summary>
		/// <param name="name">타입의 이름입니다.</param>
		/// <param name="typeAttributes">타입의 특성을 나타내는 속성입니다.</param>
		/// <returns>	
		/// 	타입을 생성할 때 사용하는 <see cref="TypeBuilder"/> 객체를 반환합니다.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="name"/> 이 NULL 인 경우 발생하는 예외 입니다.
		/// </exception>
		public TypeBuilder CreateType(string name, TypeAttributes typeAttributes)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			return this.ModuleBuilder.DefineType(name, typeAttributes);
		}

		public TypeBuilder CreateType(string name, TypeAttributes typeAttributes, Type parentType, Type[] interfaces)
		{
			if ( name == null )
				throw new ArgumentNullException("name");

			return this.ModuleBuilder.DefineType(name, typeAttributes, parentType, interfaces);
		}


		/// <summary>	
		/// 	단일 비정적 필드인 value__ 가 들어가는 열거형 타입을 생성합니다.
		/// </summary>
		/// <param name="name">	열거형 타입의 이름입니다. </param>
		/// <param name="typeAttributes">	열거형 타입의 특성을 나타내는 속성입니다. </param>
		/// <param name="underlyingType">	열거형의 내부 형식입니다.</param>
		/// <returns>	
		/// 	타입을 생성할 때 사용하는 <see cref="EnumBuilder"/> 객체를 반환합니다.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		<paramref name="name"/> 이 NULL 인 경우 발생하는 예외 입니다.
		/// </exception>
		public EnumBuilder CreateEnum(string name, TypeAttributes typeAttributes, Type underlyingType)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			return this.ModuleBuilder.DefineEnum(name, typeAttributes, underlyingType);
		}


		/// <summary>	
		/// 	대리자를 생성합니다.
		/// </summary>
		/// <param name="returnType">	대리자가 반환하는 타입입니다. </param>
		/// <param name="name">	타입의 이름입니다. </param>
		/// <param name="invokeMethodParameters">	대리자에게 위임하는 메서드의 매개변수 타입입니다. </param>
		/// <returns>	
		/// 	대리자를 생성할 때 사용하는 <see cref="TypeBuilder"/> 객체를 반환합니다.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		매개 변수 <paramref name="name"/> 의 값이 NULL 인 경우 발생하는 예외 입니다.
		/// </exception>
		public TypeBuilder CreateDelegate(Type returnType, string name, params Type[] invokeMethodParameters)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			var delegateTypeBuilder = this.CreateType(name, AttributeConstants.TypeAttribute.Delegate);
			delegateTypeBuilder.SetParent(typeof(MulticastDelegate));

			TypeBuilderExtension typeBuilderExtension = new TypeBuilderExtension(this.ModuleBuilder, delegateTypeBuilder);

			var constructorBuilder = typeBuilderExtension.CreateConstructor(AttributeConstants.MethodAttribute.Public | AttributeConstants.MethodAttribute.Constructor,
																			CallingConventions.Standard,
																			new Type[] { typeof(string), typeof(IntPtr) });
			constructorBuilder.DefineParameter(0, ParameterAttributes.None, "object");
			constructorBuilder.DefineParameter(1, ParameterAttributes.None, "method");

			constructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

			Type[] delegateParameters = new Type[invokeMethodParameters.Length];

			for (int i = 0; i < invokeMethodParameters.Length; i++)
			{
				delegateParameters[i] = invokeMethodParameters[i];
			}

			var invokeMethodBuilder = typeBuilderExtension.CreateMethod(AttributeConstants.MethodAttribute.Public | AttributeConstants.MethodAttribute.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot,
																		returnType,
																		"Invoke",
																		delegateParameters,
																		null,
																		false);
			invokeMethodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

			return delegateTypeBuilder;
		}
	}
}
