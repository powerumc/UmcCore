using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

#if NET40
using System.Diagnostics.Contracts;
#endif


namespace Umc.Core.IoC
{
	/// <summary>
	///		기반 프레임워크와 <see cref="IFrameworkContainer"/> 또는 이와 종속된 구성요소에서 발생하는 예외 클래스입니다.
	/// </summary>
#if NET40
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
#endif
	[Serializable]
	[ComVisible(true)]
	public class FrameworkDependencyException : FrameworkException
	{
		public FrameworkDependencyException()
			: base()
		{
		}

		public FrameworkDependencyException(string message)
			: base(message)
		{
		}

		public FrameworkDependencyException(string format, params string[] arg)
			: base(string.Format(format, arg))
		{
		}

		public FrameworkDependencyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
