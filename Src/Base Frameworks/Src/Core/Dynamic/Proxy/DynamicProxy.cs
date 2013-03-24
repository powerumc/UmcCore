using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy
{
	public class DynamicProxy<T> : DynamicProxyBase<T>
		where T : class
	{
		protected IEmitProvider DynamicProxyProvider { get; private set; }

		public DynamicProxy()
		{
		}

		public DynamicProxy(IEmitProvider provider)
		{
			this.DynamicProxyProvider = provider;
		}

		public override bool IsSupport
		{
			get { throw new NotImplementedException(); }
		}
	}
}
