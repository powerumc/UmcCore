using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	public class TypeUnitLambda : TypeLambda
	{
		protected ITypeLambda ParentTypeLambda { get; private set; }

		public TypeUnitLambda(IModuleLambda moduleLambda, ITypeLambda parentTypeLambda)
			: base(moduleLambda)
		{
			this.ParentTypeLambda = parentTypeLambda;
		}
	}
}
