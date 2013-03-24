using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Umc.Core.Dynamic.Proxy
{
	/// <summary>
	/// <para>동적 프락시 개체를 생성하는 중 발생하는 예외를 처리하는 클래스 입니다.</para>
	/// </summary>
#if NET40
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
#endif
	[Serializable]
	[ComVisible(true)]
	public class DynamicProxyException : FrameworkException
	{
		protected DynamicProxyException()
			: base()
		{
		}

		public DynamicProxyException(string message)
			: base(message)
		{
		}

		public DynamicProxyException(string format, params string[] arg)
			: base(string.Format(format, arg))
		{
		}

		public DynamicProxyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
