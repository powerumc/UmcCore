using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Interfaces
{
	public class WarningCollection : List<Warning>
	{
	}

	public class Warning
	{
		public int WarningCode { get; set; }
	}
}
