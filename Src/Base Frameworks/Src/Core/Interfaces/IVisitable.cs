using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{

	/// <summary>	
	/// 	객체를 방문할 수 있는 패턴의 인터페이스를 구현할 수 있습니다.
	/// </summary>
	public interface IVisitable
	{
		void Visit();
	}
}
