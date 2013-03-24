using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.IoC
{
	public interface IFrameworkInterceptionInput
	{
		MethodBase Method { get; }
		object[] Arguments { get; }
		object TargetObject { get; }
	}
}
