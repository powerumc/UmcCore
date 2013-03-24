using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Dynamic.Proxy;

namespace Umc.Core.Dynamic.Proxy
{

	/// <summary>	
	/// 	표준적인 메타데이터 정보를 포함하는 클래스 입니다.
	/// </summary>
	public class CriteriaMetadataInfo : ICriteriaMetadataInfo
	{

		/// <summary>	
		/// 	표준 메타데이터 토큰의 열거형을 가져옵니다. 
		/// </summary>
		public CriteriaMetadataToken Token { get; protected set; }


		/// <summary>	
		/// 	표준적인 메타데이터 정보의 타입을 가져옵니다. 
		/// </summary>
		public Type Type { get; protected set; }


		/// <summary>	
		/// 	표준적인 메타데이터 정보의 이름을 가져옵니다. 
		/// </summary>
		public string Name { get; protected set; }

		public CriteriaMetadataInfo(Type type, string name, CriteriaMetadataToken token)
		{
			this.Type  = type;
			this.Name  = name;
			this.Token = token;
		}
	}
}
