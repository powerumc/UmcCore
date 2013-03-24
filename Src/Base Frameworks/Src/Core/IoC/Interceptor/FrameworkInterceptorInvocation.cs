using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.IoC.Interceptor
{
	public class FrameworkInterceptorInvocation : IFrameworkInterceptorInvocation
	{
		public IFrameworkInterceptionReturn Invoker(IFrameworkInterceptionInput input)
		{
			FrameworkInterceptionReturn @return = new FrameworkInterceptionReturn();

			object invokeResult = null;
			Exception exception = null;

			try
			{
				if (input.Method.IsStatic)
				{
					invokeResult = input.Method.Invoke(null, input.Arguments);
				}
				else
				{
					invokeResult = input.Method.Invoke(input.TargetObject, input.Arguments);
				}
			}
			catch (Exception ex)
			{
				exception = new FrameworkException("Interceptor Exception", ex);
			}

			@return.Value = invokeResult;
			@return.Exception = exception;

			return @return;
		}
	}
}
