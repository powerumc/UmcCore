using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC
{
	public delegate IFrameworkInterceptionReturn InterceptionHandler(IFrameworkInterceptionInput input);
}
