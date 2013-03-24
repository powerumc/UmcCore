using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	public class EnumLambda : IEnumLambda
	{
		private EnumBuilder EnumBuilder { get; set; }

		public EnumLambda(EnumBuilder enumBuilder)
		{
			this.EnumBuilder = enumBuilder;
		}

		public void DefineLiteral(string name, object value)
		{
			this.EnumBuilder.DefineLiteral(name, value);
		}

		public Type ReleaseType()
		{
			return this.EnumBuilder.CreateType();
		}
	}
}
