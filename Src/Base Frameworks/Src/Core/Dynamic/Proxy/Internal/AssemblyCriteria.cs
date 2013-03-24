using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy
{

	/// <summary>	
	/// 	어셈블리의 표준적인 메타데이터 정보를 표현하는 인터페이스 입니다.
	/// </summary>
	internal class AssemblyCriteria : ICriteriaMetadataInfo
	{
		public AssemblyCriteria(string name)
		{
			this.Name = name;
		}


		/// <summary>	
		/// 	표준 메타데이터 토큰의 열거형을 가져옵니다. 
		/// </summary>
		public CriteriaMetadataToken Token
		{
			get { return CriteriaMetadataToken.Assembly; }
		}


		/// <summary>	
		/// 	표준적인 메타데이터 정보의 타입을 가져옵니다. 
		/// </summary>
		public Type Type
		{
			get { throw new NotSupportedException(); }
		}


		/// <summary>	
		/// 	표준적인 메타데이터 정보의 이름을 가져옵니다. 
		/// </summary>
		public string Name { get; private set; }
	}
}
