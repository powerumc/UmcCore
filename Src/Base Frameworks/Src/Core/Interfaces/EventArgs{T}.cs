using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	public sealed class EventArgs<T> : EventArgs where T : class
	{
		private T args;

		public EventArgs(T args)
		{
			this.args = args;
		}

		public T Value { get { return this.args; } }
	}
}
