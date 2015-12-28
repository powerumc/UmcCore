using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Umc.Core
{
	public class DisplayTextAttribute : Attribute
	{
		public string DisplayText { get; set; }

		public DisplayTextAttribute(string displayText)
		{
			this.DisplayText = displayText;
		}
	}
}
