using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy
{
    public class DynamicModuleContext : DynamicContext, IResolvable<IEnumerable<DynamicTypeContext>>
    {
		public IEnumerable<DynamicTypeContext> Resolve<TInput>(params TInput[] inputs)
		{
			throw new NotImplementedException();
		}
	}
}