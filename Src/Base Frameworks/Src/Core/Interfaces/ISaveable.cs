using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		임의의 데이터소스로 상태를 저장할 수 있는 인터페이스 입니다.
	/// </summary>
	public interface ISaveable
	{
		/// <summary>
		///		객체의 상태를 저장할 수 있는지 여부를 가져옵니다.
		/// </summary>
		bool CanSave { get; }
		
		/// <summary>
		///		객체의 상태를 저장합니다.
		/// </summary>
		void Save();
	}
}
