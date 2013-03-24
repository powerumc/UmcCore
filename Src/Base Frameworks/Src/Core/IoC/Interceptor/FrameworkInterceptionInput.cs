using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.IoC.Interceptor
{
	public class FrameworkInterceptionInput : IFrameworkInterceptionInput
	{
		public MethodBase Method { get; protected set; }
		public object[] Arguments { get; protected set; }
		public object TargetObject { get; protected set; }
	}
}
