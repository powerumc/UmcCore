using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.DB;

namespace Umc.Core.Web.Tests
{
	[TestClass()]
	public class WebFormExtensions_Tests
	{
		public interface IModel
		{
			string Name { get; set; }
			int Age { get; set; }
		}

		[TestMethod()]
		public void Input_Test()
		{
			// Given
			var model = SP.Resolve<IModel>();
			model.Name = "NEXON";
			model.Age = 36;
			var webform = new WebForm<IModel>(model);

			// When
			var html = webform.InputTextFor(o => o.Name);
			Console.WriteLine(html);

			// Then
			Assert.IsTrue(html.ToString().Contains("name"));
			Assert.IsTrue(html.ToString().Contains("type"));
			Assert.IsTrue(html.ToString().Contains("value"));
		}
	}
}
