using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC.Catalog
{

	/// <summary>	
	/// 	<see cref="IFrameworkContainer"/> 를 위해 관리 대상이 되는 목록을 관리하는 추상 클래스 입니다.
	/// </summary>
	public abstract class FrameworkCatalog
	{

		/// <summary>	
		///		관리되는 대상 목록의 조건에 만족하는 타입을 반환합니다.
		/// </summary>
		/// <returns>조건에 만족하는 타입 목록입니다.</returns>
		public abstract IEnumerable<Type> GetMatchingTypes();
	}
}
