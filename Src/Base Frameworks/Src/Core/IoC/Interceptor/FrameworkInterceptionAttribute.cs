using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC
{
	public abstract class FrameworkInterceptionAttribute : Attribute
	{
		protected abstract IFrameworkInterception CreateInterceptor();
	}
}