using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Umc.Core.Mapping
{
#if NET40
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
#endif

	/// <summary>	
	/// 	매핑을 수행하는 중 예외가 발생하는 클래스 입니다.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	public class MappingException : FrameworkException
	{
		protected MappingException()
			: base()
		{
		}

		public MappingException(string message)
			: base(message)
		{
		}

		public MappingException(string format, params string[] arg)
			: base(string.Format(format, arg))
		{
		}

		public MappingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public MappingException(Exception innerException, string format, params string[] arg)
			: base(string.Format(format, arg), innerException)
		{
		}
	}
}
