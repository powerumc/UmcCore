using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Umc.Core
{
	[AttributeUsage(AttributeTargets.All)]
	public class TextAttribute : Attribute
	{
		public string Text { get; set;}

		public TextAttribute() { }

		public TextAttribute(string text)
		{
			this.Text = text;
		}
	}
}
