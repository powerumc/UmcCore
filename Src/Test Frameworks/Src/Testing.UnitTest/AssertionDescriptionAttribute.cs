using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umc.Core.Testing.UnitTest
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
	public class AssertionDescriptionAttribute : Attribute
	{
		public object Key { get; set; }
		public string Description { get; set; }

		public AssertionDescriptionAttribute(object index, string description)
		{
			this.Key = index;
			this.Description = description;
		}
	}
}
