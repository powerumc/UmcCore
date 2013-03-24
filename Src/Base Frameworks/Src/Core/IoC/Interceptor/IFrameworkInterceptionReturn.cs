using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC
{
	public interface IFrameworkInterceptionReturn : IValuable
	{
		Exception Exception { get; }
		bool HasException { get; }
	}
}
