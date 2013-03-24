using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Reflection
{
	/// <summary>
	///		.NET 의 CLR(Common Language Runtime) 의 MSIL(Microsoft Intermediate Language) 수준에서 제공하지 위한 상수의 선언 입니다.
	/// </summary>
	/// <remarks>
	///		<para>static readonly 의 참조 값이 변경되는 문제로 property로 변경함</para>
	///		<code>
	///			var delegateTypeBuilder = this.CreateType(name, AttributeConstants.TypeAttribute.Delegate);
	///		</code>
	/// </remarks>
	internal static class AttributeConstants
	{

		/// <summary>	
		/// 	타입에 대한 매핑의 특성을 지정하는 클래스 입니다.
		/// </summary>
		internal static class TypeAttribute
		{
			public static TypeAttributes Class					{ get { return TypeAttributes.Class | TypeAttributes.BeforeFieldInit; } }
			public static TypeAttributes Interface				{ get { return TypeAttributes.Interface | TypeAttributes.Abstract; } }
			public static TypeAttributes Delegate				{ get { return Class | Selaed; } }
			public static TypeAttributes Selaed					{ get { return TypeAttributes.Sealed; } }
			public static TypeAttributes Public					{ get { return TypeAttributes.Public; } }
			public static TypeAttributes Static					{ get { return TypeAttributes.Abstract | TypeAttributes.Sealed; } }
			public static TypeAttributes Internal				{ get { return TypeAttributes.NotPublic; } }
			public static TypeAttributes Abstract				{ get { return TypeAttributes.Abstract; } }
			public static TypeAttributes Struct					{ get { return TypeAttributes.Sealed | TypeAttributes.SequentialLayout; } }
		}


		/// <summary>	
		/// 	메서드에 대한 매핑의 특성을 지정하는 클래스 입니다.
		/// </summary>
		internal static class MethodAttribute
		{
			public static MethodAttributes Public				{ get { return MethodAttributes.Public; } }
			public static MethodAttributes Private				{ get { return MethodAttributes.Private; } }
			public static MethodAttributes Static				{ get { return MethodAttributes.Static; } }
			public static MethodAttributes Protected			{ get { return MethodAttributes.Family; } }
			public static MethodAttributes Virtual				{ get { return MethodAttributes.Virtual; } }
			public static MethodAttributes Internal				{ get { return MethodAttributes.Assembly; } }
			public static MethodAttributes Abstract				{ get { return MethodAttributes.Abstract; } }

			public static MethodAttributes Constructor			{ get { return MethodAttributes.SpecialName | MethodAttributes.RTSpecialName; } }
		}


		/// <summary>	
		/// 	필드에 대한 매핑의 특성을 지정하는 클래스 입니다.
		/// </summary>
		internal static class FieldAttribute
		{
			public static FieldAttributes Public				{ get { return FieldAttributes.Public; } }
			public static FieldAttributes Internal				{ get { return FieldAttributes.Assembly; } }
			public static FieldAttributes Protected				{ get { return FieldAttributes.Family; } }
			public static FieldAttributes Private				{ get { return FieldAttributes.Private; } }
			public static FieldAttributes Static				{ get { return FieldAttributes.Static; } }
			public static FieldAttributes ReadOnly				{ get { return FieldAttributes.InitOnly; } }

			public static FieldAttributes EnumItem				{ get { return FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal; } }
			public static FieldAttributes EnumStaticDefaultValue{ get { return FieldAttributes.Private | FieldAttributes.SpecialName; } }
		}
	}
}