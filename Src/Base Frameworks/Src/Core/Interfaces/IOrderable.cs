using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{

	/// <summary>	
	/// 	순서를 지정하는 인터페이스 입니다.
	/// </summary>
	public interface IOrderable
	{

		/// <summary>	
		/// 	순서를 가져옵니다.
		/// </summary>
		int Position { get; }
	}
}
