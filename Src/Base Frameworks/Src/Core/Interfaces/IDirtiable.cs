using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		객체의 상태가 처음 상태와 다르게 변경되었는지의 여부를 나타내는 인터페이스 입니다.
	/// </summary>
	public interface IDirtiable
	{
		/// <summary>
		///		객체의 상태가 변경되었는지 여부를 가져옵니다.
		/// </summary>
		bool IsDirty { get; }
	}


	/// <summary>	
	/// 	<para>객체의 상태가 처음 상태와 다르게 변경되었는지의 여부를 나타내는 인터페이스 입니다.</para>
	///		<para>그리고 객체의 상태가 변경이 되면 이벤트를 발생합니다.</para>
	/// </summary>
	public interface ISupportDirtiableNotification : IDirtiable
	{
		/// <summary>
		///		객체의 상태가 변경되면 발생하는 이벤트 입니다.
		/// </summary>
		event EventHandler OnDirty;
	}
}
