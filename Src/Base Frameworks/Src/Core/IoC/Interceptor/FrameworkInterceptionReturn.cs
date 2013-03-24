using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC.Interceptor
{
	public class FrameworkInterceptionReturn : IFrameworkInterceptionReturn
	{
		public Exception Exception { get; internal set; }
		public object Value { get; internal set; }
		public bool HasException { get { return this.Exception != null; } }
	}
}
