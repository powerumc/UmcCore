using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		<para><see cref="System.Reflection"/> 을 이용하여 속성(Property)간의 매핑을 지원합니다.</para>
	///		<para>기본 설정은 속성의 대소문자를 무시합니다. 이 설정을 조정하려면 <see cref="ReflectionMappingOption"/> 개체를 설정하십시오.</para>
	/// </summary>
	public class ReflectionMapping : Mapping<Object, Object>
	{

		/// <summary>	
		/// 	<see cref="ReflectionMapping"/> 의 원본 객체를 가져옵니다.
		/// </summary>
		public object SourceObject { get; private set; }

		/// <summary>	
		/// 	<see cref="ReflectionMapping"/> 의 대상 객체를 가져옵니다.
		/// </summary>
		public object TargetObject { get; private set; }


		/// <summary>	
		/// 	매핑에 사용되는 옵션을 가져옵니다.
		/// </summary>
		public ReflectionMappingOption Option { get; private set; }

		public ReflectionMapping(object sourceObject, object targetObject)
			: this(sourceObject, targetObject, new ReflectionMappingOption())
		{
		}

		/// <summary>
		///		ReflectionMapping 개체를 생성합니다.
		/// </summary>
		/// <param name="sourceObject">매핑 원본 소스 개체입니다.</param>
		/// <param name="targetObject">매핑 대상 소스 개체입니다.</param>
		/// <param name="option">매핑 옵션을 정의합니다.</param>
		/// <exception cref="ArgumentNullException">sourceObject 또는 targetObject 의 값이 Null 인 경우 발생합니다.</exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ReflectionMapping(object sourceObject, object targetObject, ReflectionMappingOption option)
		{
			if (sourceObject == null)
				throw new ArgumentNullException("sourceObject");

			if (targetObject == null)
				throw new ArgumentNullException("targetObject");

			this.SourceObject = sourceObject;
			this.TargetObject = targetObject;
			this.Option       = option;

			this.LazyInitializeMapping();
		}


		/// <summary>	
		/// 	매핑을 초기화 합니다.
		/// </summary>
		protected override void InitializeMapping() { }

		/// <summary>
		///		<see cref="InitializeMapping() "/>메서드가 호출된 후에 호출되는 초기화 메서드입니다.
		/// </summary>
		/// <exception cref="MappingException">매핑 속성의 개수가 일치하지 않을 경우 발생합니다.</exception>
		protected virtual void LazyInitializeMapping()
		{
			PropertyInfo[] sourceMembers = this.SourceObject.GetType().GetProperties(this.Option.BindingFlags);
			PropertyInfo[] targetMembers = this.TargetObject.GetType().GetProperties(this.Option.BindingFlags);

			if (this.Option.ThrowIfNotMatched &&
				sourceMembers.Length != targetMembers.Length)
			{
				throw new MappingException(ExceptionRS.매핑_속성의_개수가_일치하지_않습니다);
			}

			foreach (var memberinfo in sourceMembers)
			{
				var targetMember = targetMembers.FirstOrDefault(o => String.Compare(memberinfo.Name, o.Name, this.Option.StringComparison) == 0);
				if (targetMember == null) continue;

				var value = memberinfo.GetValue(this.SourceObject, null);
				targetMember.SetValue(this.TargetObject, value, null);
			}
		}


		/// <summary>	
		/// 	 해당 문자열에 대한 해시 코드를 반환합니다.
		/// </summary>
		/// <returns>객체의 해시 코드입니다.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}