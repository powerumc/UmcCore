using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Tests
{
	[TestClass()]
	public class TypeExtensionTests
	{
		[TestMethod()]
		public void AbbreviationStringTest()
		{
			var typename = this.GetType();
			Console.WriteLine(typename.AbbreviationString(10));
		}
	}
}