using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		개체를 초기화하는 인터페이스 입니다.
	/// </summary>
	public interface IInitializable
	{
		/// <summary>	
		/// 	초기화 작업을 수행합니다.
		/// </summary>
		void Initialize();
	}


	/// <summary>	
	/// 	개체를 초기화하고 알림을 받을 수 있는 인터페이스 입니다.
	/// </summary>
	public interface IInitializableWithNotification : IInitializable, System.ComponentModel.ISupportInitializeNotification
	{
	}
}
