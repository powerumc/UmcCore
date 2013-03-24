using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy
{
	internal class ILItem
	{
		private OpCode opCode;

		internal ILItem(OpCode opCode)
		{
			this.opCode = opCode;
		}
	}
}

