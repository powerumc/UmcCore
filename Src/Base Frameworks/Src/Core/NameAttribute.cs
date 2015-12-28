using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public class NameAttribute : Attribute
	{
		public string Name { get; protected set; }

		public NameAttribute(string name)
		{
			this.Name = name;
		}
	}
}
