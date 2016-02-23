using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Umc.Core.Testing.UnitTest
{
	/// <summary>
	///		<see cref="TestContext"/> 객체의 확장 메서드 입니다.
	/// </summary>
	public static class TestContextExtension
	{
		public static void WriteLine(this TestContext testContext, string message)
		{
			testContext.WriteLine(message, String.Empty);
		}

		public static void ShowObject(this TestContext testContext, object @object)
		{
			ShowObject(testContext, @object, Console.Out);
		}

		public static void ShowObject(this TestContext testContext, object @object, TextWriter writer)
		{
			var show = new ShowObject(@object, writer);
            show.Show();
		}
	}
}