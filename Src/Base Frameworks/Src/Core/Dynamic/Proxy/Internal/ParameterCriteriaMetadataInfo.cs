using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy
{

	/// <summary>	
	/// 	매개 변수에 대한 표준적인 메타데이터 정보를 표현하는 클래스 입니다.
	/// </summary>
	public class ParameterCriteriaMetadataInfo : CriteriaMetadataInfo
	{
		public ParameterAttributes ParameterAttribute { get; private set; }

		public ParameterCriteriaMetadataInfo(Type type)
			: this(type, String.Empty)
		{
		}

		public ParameterCriteriaMetadataInfo(Type type, string name)
			: this(type, name, ParameterAttributes.HasDefault)
		{
		}

		public ParameterCriteriaMetadataInfo(Type type, string name, ParameterAttributes parameterAttribute)
			: base(type, name, CriteriaMetadataToken.Method)
		{
			this.ParameterAttribute = parameterAttribute;
		}
	}
}
