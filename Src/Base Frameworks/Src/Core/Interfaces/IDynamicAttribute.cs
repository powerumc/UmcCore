using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.IoC;
using Umc.Core.IoC.Configuration;

namespace Umc.Core
{
	public interface IDynamicAttribute
	{
		string Type { get; set; }
		LifetimeFlagType Lifetime { get; set; }
	}
}
