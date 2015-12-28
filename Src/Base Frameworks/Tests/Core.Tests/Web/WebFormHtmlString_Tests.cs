using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Umc.Core.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umc.Core.Web.Tests
{
	[TestClass()]
	public class WebFormHtmlString_Tests
	{
		[TestMethod()]
		public void Create_Test()
		{
			// Given
			WebFormHtmlString html = "<input type='text' value='hello' />";
			Console.WriteLine(html);

			Assert.IsFalse(html.IsEmpty);
		}
	}
}
