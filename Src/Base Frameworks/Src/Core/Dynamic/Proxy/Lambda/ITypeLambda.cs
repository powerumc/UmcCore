using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Umc.Core.Dynamic.Proxy.Builder;
using Umc.Core.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	/// <para>
	///		타입에 대한 표현식을 나타내는 람다 인터페이스 입니다.
	/// </para>
	/// </summary>
	public interface ITypeLambda : 
		IAccessorLambda<ITypeLambda>,
		IReleaseType
	{

		/// <summary>	
		/// 	<para>Type(타입) 에 대한 엑세스 제한자 설정을 가져옵니다.</para>
		/// </summary>
		/// <value>	
		/// 	Type에 대한 <see cref="IAccessorLambda"/> 를 반환합니다.
		/// </value>
		TypeAccessor TypeAccessor { get; }


		/// <summary>	
		/// 	<para>Field(필드) 에 대한 엑세스 제한자 설정을 가져옵니다.</para>
		/// </summary>
		/// <value>	
		/// 	Field에 대한 <see cref="IAccessorLambda"/> 를 반환합니다.
		/// </value>
		FieldAccessor FieldAccessor { get; }

		/// <summary>	
		/// 	Method에 대한 엑세스 제한자 설정을 가져옵니다.
		/// </summary>
		/// <value>	
		/// 	Method에 대한 <see cref="IAccessorLambda"/> 를 반환합니다.
		/// </value>
		MethodAccessor MethodAccessor { get; }


		/// <summary>	
		/// 	객체를 동적으로 구성하기 위한 <see cref="TypeBuilder"/> 객체를 가져옵니다.
		/// </summary>
		/// <value>	
		/// 	이 <see cref="ITypeLambda"/>에서 사용하는 <see cref="TypeBuilder"/> 를 반환합니다.
		/// </value>
		TypeBuilder TypeBuilder { get; }

		#region Method Command

		ITypeLambda Attribute(Type attributeType, params object[] param);

		/// <summary>
		///		동적 타입에 필드를 생성합니다.
		/// </summary>
		/// <param name="returnType">리턴되는 타입입니다.</param>
		/// <param name="name">Field의 이름입니다.</param>
		/// <returns>
		///		생성된 <see cref="Operand"/> 를 반환합니다.
		/// </returns>
		Operand Field(Type returnType, string name);

		/// <summary>
		///		동적 타입에 속성을 생성합니다.
		/// </summary>
		/// <param name="returnType">리턴되는 타입입니다.</param>
		/// <param name="name">Property의 이름입니다.</param>
		/// <returns>
		///		생성된 <see cref="IPropertyLambda"/> 를 구현하는 객체를 반환합니다.
		/// </returns>
		IPropertyLambda Property(Type returnType, string name);

		
		/// <summary>
		///		동적 타입에 메서드를 생성합니다.
		/// </summary>
		/// <param name="name">메서드의 이름입니다.</param>
		/// <returns>
		///		생성된 메서드의 <see cref="ICodeLambda"/> 를 반환합니다.
		/// </returns>
		ICodeLambda Method(string name);

		/// <summary>
		///		동적 타입에 메서드를 생성합니다.
		/// </summary>
		/// <param name="returnType">리턴되는 타입입니다.</param>
		/// <param name="name">Method의 이름입니다.</param>
		/// <param name="argumentsTypes">Method의 Arguments 타입입니다.</param>
		/// <returns>
		///		생성된 메서드의 <see cref="ICodeLambda"/> 를 반환합니다.
		/// </returns>
		ICodeLambda Method(Type returnType, string name, Type[] argumentsTypes);

		/// <summary>
		///		동적 타입에 메서드를 생성합니다.
		/// </summary>
		/// <param name="returnType">리턴되는 타입입니다.</param>
		/// <param name="name">Method의 이름입니다.</param>
		/// <param name="argumentsTypes">Method의 Arguments 타입입니다.</param>
		/// <param name="parentMethodInfo">
		///		<para>부모 타입의 메서드의 정보 입니다.</para>
		///		<para>이 파라메터는 메서드가 Virtual 또는 Override 등의 메타 정보를 갖게 되는 경우 반드시 설정해야 합니다.</para>
		/// </param>
		/// <returns>
		///		생성된 메서드의 <see cref="ICodeLambda"/> 를 반환합니다.
		/// </returns>
		/// <exception cref="ArgumentNullException">MethodAccessor.IsOverride 가 True 이고 parentMethodInfo 값이 NULL 인 경우 발생하는 예외입니다.</exception>
		/// <exception cref="NullReferenceException">TypeBuilder 가 NULL 인 경우 발생하는 예외 입니다.</exception>
		ICodeLambda Method(Type returnType, string name, Type[] argumentsTypes, MethodInfo parentMethodInfo);

		/// <summary>
		///		동적 타입에 생성자를 생성합니다.
		/// </summary>
		/// <returns>
		///		생성자(Constructor)에 대한 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		ICodeLambda Constructor();

		/// <summary>
		///		동적 타입에 생성자를 생성합니다.
		/// </summary>
		/// <param name="argumentsTypes">생성자의 타입입니다.</param>
		/// <returns>
		///		생성자(Constructor)에 대한 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		/// 
		ICodeLambda Constructor(params Type[] argumentsTypes);

		/// <summary>
		/// <para>동적 타입에 생성자를 생성합니다.</para>
		/// </summary>
		/// <param name="parameterCriteriaMetadataInfos"></param>
		/// <returns></returns>
		ICodeLambda Constructor(IEnumerable<ParameterCriteriaMetadataInfo> parameterCriteriaMetadataInfos);
		#endregion

		#region Type
		/// <summary>
		/// <para>동적 어셈블리에 타입을 /생성한 후에 <see cref="ITypeLambda"/> 객체를 반환합니다.</para>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Class")]
		ITypeLambda Class(string name);
		
		ITypeLambda Class(string name, Type parent, Type[] interfaces);
		/// <summary>
		/// <para>동적 어셈블리에 구조체를 생성한 후에 <see cref="ITypeLambda"/> 객체를 반환합니다.</para>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		ITypeLambda Struct(string name);
		/// <summary>
		/// <para>동적 어셈블리에 인터페이스를 생성한 후에 <see cref="ITypeLambda"/> 객체를 반환합니다.</para>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Interface")]
		ITypeLambda Interface(string name);
		/// <summary>
		/// <para>동적 어셈블리에 열거형 타입을 생성한 후에 <see cref="ITypeLambda"/> 객체를 반환합니다.</para>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Enum")]
		IEnumLambda Enum(string name);
		/// <summary>
		/// <para>동적 어셈블리에 열거형 타입을 생성한 후에 <see cref="ITypeLambda"/> 객체를 반환합니다.</para>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Enum")]
		IEnumLambda Enum(string name, Type underlyingType);
		/// <summary>
		/// <para>동적 어셈블리에 대리자를 생성한 후에 <see cref="ITypeLambda"/> 객체를 반환합니다.</para>
		/// </summary>
		/// <param name="returnType"></param>
		/// <param name="name"></param>
		/// <param name="argumentsTypes"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Delegate")]
		ITypeLambda Delegate(Type returnType, string name, params Type[] argumentsTypes);
		/// <summary>
		/// <para>동적 어셈블리에 이벤트를 생성한 후에 <see cref="ITypeLambda"/> 객체를 반환합니다.</para>
		/// </summary>
		/// <param name="delegateType"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Event")]
		ITypeLambda Event(Type delegateType, string name);
		#endregion
	}
}
