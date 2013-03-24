using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Umc.Core
{
	/// <summary>
	/// <para>기반 프레임워크와 Umc.Core에 의해 발생되는 예외 클래스입니다.</para>
	/// </summary>
#if NET40
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
#endif
	[Serializable]
	[ComVisible(true)]
	public class FrameworkException : Exception, IFrameworkException
	{
		/// <summary>	
		/// 	<see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다. 
		/// </summary>
		protected FrameworkException()
			: base()
		{
		}


		/// <summary>	
		/// 	지정된 오류 메시지를 사용하여 <see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다.
		/// </summary>
		/// <param name="message">	오류 메시지 입니다. </param>
		public FrameworkException(string message)
			: base(message)
		{
		}


		/// <summary>	
		/// 	지정된 오류 메시지 포멧을 사용하여 <see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다. 
		/// </summary>
		/// <param name="format">	오류 메시지의 포멧입니다.. </param>
		/// <param name="arg">	오류 메시지의 포멧으로 전달되는 매개 변수 입니다. </param>
		public FrameworkException(string format, params string[] arg)
			: base(string.Format(format, arg))
		{
		}


		/// <summary>	
		/// 	지정된 오류 메시지와 내부 예외 정보를 사용하여 <see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다. 
		/// </summary>
		/// <param name="message">	오류 메시지 입니다. </param>
		/// <param name="innerException">	내부 예외 정보입니다. </param>
		public FrameworkException(string message, Exception innerException)
			: base(message, innerException)
		{
		}


		/// <summary>	
		/// 	지정된 오류 메시지 포멧과 내부 예외 정보를 사용하여 <see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다. 
		/// </summary>
		/// <param name="innerException">	내부 예외 정보입니다. </param>
		/// <param name="format">	오류 메시지의 포멧입니다.. </param>
		/// <param name="arg">	오류 메시지의 포멧으로 전달되는 매개 변수 입니다. </param>
		public FrameworkException(Exception innerException, string format, params string[] arg)
			: base(string.Format(format, arg), innerException)
		{
		}
	}
}
