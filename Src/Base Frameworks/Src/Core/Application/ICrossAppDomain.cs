using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Application
{
	public interface ICrossAppDomain
	{
		AppDomain AppDomain { get; }
	}
}
