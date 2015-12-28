using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Application
{
	public class CrossApplicationException : FrameworkException
	{
		/// <summary>	
		/// 	<see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다. 
		/// </summary>
		protected CrossApplicationException()
			: base()
		{
		}


		/// <summary>	
		/// 	지정된 오류 메시지를 사용하여 <see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다.
		/// </summary>
		/// <param name="message">	오류 메시지 입니다. </param>
		public CrossApplicationException(string message)
			: base(message)
		{
		}


		/// <summary>	
		/// 	지정된 오류 메시지 포멧을 사용하여 <see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다. 
		/// </summary>
		/// <param name="format">	오류 메시지의 포멧입니다.. </param>
		/// <param name="arg">	오류 메시지의 포멧으로 전달되는 매개 변수 입니다. </param>
		public CrossApplicationException(string format, params string[] arg)
			: base(string.Format(format, arg))
		{
		}


		/// <summary>	
		/// 	지정된 오류 메시지와 내부 예외 정보를 사용하여 <see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다. 
		/// </summary>
		/// <param name="message">	오류 메시지 입니다. </param>
		/// <param name="innerException">	내부 예외 정보입니다. </param>
		public CrossApplicationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}


		/// <summary>	
		/// 	지정된 오류 메시지 포멧과 내부 예외 정보를 사용하여 <see cref="FrameworkException"/> 인스턴스를 초기화 시킵니다. 
		/// </summary>
		/// <param name="innerException">	내부 예외 정보입니다. </param>
		/// <param name="format">	오류 메시지의 포멧입니다.. </param>
		/// <param name="arg">	오류 메시지의 포멧으로 전달되는 매개 변수 입니다. </param>
		public CrossApplicationException(Exception innerException, string format, params string[] arg)
			: base(string.Format(format, arg), innerException)
		{
		}
	}
}
