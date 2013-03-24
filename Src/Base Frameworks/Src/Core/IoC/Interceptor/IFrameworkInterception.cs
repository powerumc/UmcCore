using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Dynamic.Proxy.Lambda;

namespace Umc.Core.IoC
{
	public interface IFrameworkInterception
	{
		void SetCodeLambda(ICodeLambda codeLambda);

		IFrameworkInterceptionReturn Invoke(IFrameworkInterceptionInput input, InterceptionHandler handler);
	}
}
