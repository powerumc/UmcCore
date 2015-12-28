using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using Umc.Core.Dynamic.Proxy.Lambda;

namespace Umc.Core.Dynamic.Proxy.Builder
{
	/// <summary>
	/// <para>
	///		<see cref="TypeBuilder"/> 에서 메서드 형태로 생성할 수 있는 생성자/메서드를 생성하는 클래스 입니다.
	/// </para>
	/// </summary>
	internal class TypeBuilderExtension : BuilderExtensionBase
	{
		public TypeBuilderExtension(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
			: base(moduleBuilder, typeBuilder)
		{
		}

		/// <summary>
		///		타입의 생성자를 생성합니다.
		/// </summary>
		/// <param name="methodAttributes">생성자 메서드인 .ctor 의 메서드 특성입니다.</param>
		/// <param name="callingConventions">메서드의 유효한 호출 규칙입니다.</param>
		/// <param name="parameterTypes">매개 변수의 타입 입니다.</param>
		/// <returns>
		///		생성자를 생성할 때 사용하는 <see cref="ConstructorBuilder"/> 객체를 반환합니다.
		/// </returns>
		public ConstructorBuilder CreateConstructor(MethodAttributes methodAttributes, CallingConventions callingConventions, Type[] parameterTypes)
		{
			List<ParameterCriteriaMetadataInfo> list = new List<ParameterCriteriaMetadataInfo>();

			foreach (var parameter in parameterTypes)
			{
				list.Add(new ParameterCriteriaMetadataInfo(parameter));
			}

			return CreateConstructor(methodAttributes, callingConventions, list.AsEnumerable());
		}



		/// <summary>	
		/// 	타입의 생성자를 생성합니다. 
		/// </summary>
		/// <param name="methodAttributes">	생성자 메서드인 .ctor 의 메서드 특성입니다. </param>
		/// <param name="callingConventions">	메서드의 유효한 호출 규칙입니다. </param>
		/// <param name="parameterCriteriaMetadataInfos">	매개 변수의 표준적인 메타데이터 정보입니다. </param>
		/// <returns>	
		/// 	생성자를 생성할 때 사용하는 <see cref="ConstructorBuilder"/> 객체를 반환합니다. 
		/// </returns>
		public ConstructorBuilder CreateConstructor(MethodAttributes methodAttributes, CallingConventions callingConventions, IEnumerable<ParameterCriteriaMetadataInfo> parameterCriteriaMetadataInfos)
		{
			if (isStaticMethod(methodAttributes))
			{
				methodAttributes = MethodAttributes.Static | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName | MethodAttributes.Private | MethodAttributes.HideBySig;
				callingConventions = CallingConventions.Standard;
			}
			else
			{
				callingConventions = CallingConventions.HasThis;
			}


			var constructorBuilder = this.TypeBuilder.DefineConstructor(methodAttributes, callingConventions, parameterCriteriaMetadataInfos.Select( o => o.Type).ToArray());
			
			int iSeqence = 0;
			foreach (var parameter in parameterCriteriaMetadataInfos)
			{
				iSeqence++;
				constructorBuilder.DefineParameter(iSeqence, parameter.ParameterAttribute, parameter.Name);
			}

			var il = constructorBuilder.GetILGenerator();

			if (isStaticMethod(methodAttributes))	// 정적 생성자는 Object 개체 파생이 아니므로 Object 생성을 하지 않음
			{
				il.Emit(OpCodes.Nop);
			}
			else
			{
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Call, this.TypeBuilder.BaseType.GetConstructors()[0]);
			}


			return constructorBuilder;
		}






		/// <summary>
		/// <para>메서드를 생성합니다.</para>
		/// </summary>
		/// <param name="methodAttributes">메서드의 특성입니다.</param>
		/// <param name="returnType">메서드의 리턴 타입입니다.</param>
		/// <param name="name">메서드의 이름입니다.</param>
		/// <param name="argumentTypes">메서드의 매개 변수의 타입입니다.</param>
		/// <param name="parentMethodInfo">메서드의 부모를 지정합나다. 일반적으로 override 메서드는 반드시 부모 메서드 형식을 지정해야 합니다.</param>
		/// <param name="isOverride">이 메서드가 override 되었는지의 여부입니다.</param>
		/// <returns>
		///		메서드를 생성할 때 사용하는 <see cref="ConstructorBuilder"/> 객체를 반환합니다. 
		/// </returns>
		public MethodBuilder CreateMethod(MethodAttributes methodAttributes, Type returnType, string name, Type[] argumentTypes, MethodInfo parentMethodInfo, bool isOverride)
		{
			if (this.TypeBuilder == null)
				throw new NullReferenceException(this.TypeBuilder.GetType().Name);

			MethodBuilder methodBuilder = this.TypeBuilder.DefineMethod(name, methodAttributes, returnType, argumentTypes);

			if (isOverride == true && parentMethodInfo == null)
				throw new ArgumentNullException("parentMethodInfo");

			if (isOverride == true)
			{
				this.TypeBuilder.DefineMethodOverride(methodBuilder, parentMethodInfo);
			}

			return methodBuilder;
		}

		public PropertyBuilder CreateProperty(string name, PropertyAttributes attribute, Type returnType, Type[] parameterTypes)
		{
			return CreateProperty(name, attribute, returnType, parameterTypes, 0);
		}

		public PropertyBuilder CreateProperty(string name, PropertyAttributes attribute, Type returnType, Type[] parameterTypes, CallingConventions callingConventions)
		{
			if ( this.TypeBuilder == null )
				throw new NullReferenceException(this.TypeBuilder.GetType().Name);

			PropertyBuilder builder = this.TypeBuilder.DefineProperty(name, attribute, callingConventions, returnType, null, null, parameterTypes, null, null);

			return builder;
		}

		public void CreateAttribute(Type type, params object[] param)
		{
			if ( this.TypeBuilder == null )
				throw new NullReferenceException(this.TypeBuilder.GetType().Name);

			CustomAttributeBuilder attribute = new CustomAttributeBuilder(type.GetConstructors()[0], param);
			this.TypeBuilder.SetCustomAttribute(attribute);
		}


		/// <summary>
		/// <para>메서드가 정적 메서드인지 아닌지 결과를 반환합니다.</para>
		/// </summary>
		/// <param name="methodAttributes"></param>
		/// <returns></returns>
		private static bool isStaticMethod(MethodAttributes methodAttributes)
		{
			return (methodAttributes & MethodAttributes.Static) == MethodAttributes.Static;
		}


	}
}