using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Umc.Core.Reflection.Emit
{

	/// <summary>	
	/// 	메서드의 구현 바이트 코드를 <see cref="MethodRental"/> 클래스를 이용하여 교체하는 클래스 입니다.
	/// </summary>
	/// <remarks>	
	/// 	Umc, 2011-01-13. 
	/// </remarks>
	public sealed class MethodClone
	{

		/// <summary>	
		/// 	반환될 개체의 타입을 가져오거나 설정합니다.
		/// </summary>
		private Type ReleasedType { get; set; }


		/// <summary>	
		/// 	교체할 메서드에 대한 리플랙션 수준의 메서드 정보를 가져오거나 설정합니다.
		/// </summary>
		private MethodInfo SourceMethodInfo { get; set; }

		public MethodClone(Type releasedType, MethodInfo sourceMethodInfo)
		{
			this.ReleasedType        = releasedType;
			this.SourceMethodInfo    = sourceMethodInfo;
		}


		/// <summary>	
		/// 	메서드의 구현 코드를 <paramref name="newMethodBytes"/> 바이트 코드로 변경합니다.
		/// </summary>
		/// <param name="newMethodBytes">	The new method in bytes. </param>
		public void Clone(byte[] newMethodBytes)
		{
			this.CloneMethodBody(newMethodBytes);
		}

		private void CloneMethodBody(byte[] newMethodBytes)
		{
			GCHandle handle        = GCHandle.Alloc((Object)newMethodBytes, GCHandleType.Pinned);
			IntPtr addressOfHandle = handle.AddrOfPinnedObject();
			int methodSize         = newMethodBytes.Length;

			MethodRental.SwapMethodBody(this.ReleasedType,
										//methodToken.Token,
										this.SourceMethodInfo.MetadataToken,
										addressOfHandle,
										methodSize,
										MethodRental.JitImmediate);
		}
	}
}
