using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{

	/// <summary>	
	/// 	개체가 새로운 인스턴스를 반환할 수 있는 인터페이스 입니다.
	/// </summary>
	public interface INewInstance
	{

		/// <summary>	
		/// 	새로운 객체를 생성합니다.
		/// </summary>
		/// <returns>생성된 새로운 개체를 반환합니다.</returns>
		object CreateNewInstance();
	}
}
