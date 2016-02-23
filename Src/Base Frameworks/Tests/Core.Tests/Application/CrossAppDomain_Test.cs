using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;

namespace Umc.Core.Application
{
	[TestClass]
	public class CrossAppDomain_Test : UnitTestBase
	{
		[TestMethod]
		[DeploymentItem("Umc.Core.Tests.dll")]
		[DeploymentItem("Microsoft.Practices.Unity.dll")]
		[DeploymentItem("Microsoft.Practices.Unity.Interception.dll")]
		[DeploymentItem("Umc.Core.Testing.UnitTest.dll")]
		public void Create_CrossAppDomain()
		{
			var app = new CrossAppDomain("Init App");
            app.AppDomain.SetupInformation.PrivateBinPath = TestContext.TestDeploymentDir;
			app.AppDomain.SetupInformation.ApplicationBase = TestContext.TestDeploymentDir;
			app.AppDomain.SetupInformation.ApplicationBase = TestContext.TestDeploymentDir;
			//app.Execute(typeof(Program).Assembly.FullName, typeof(Program).FullName);

			app.AppDomain.ExecuteAssemblyByName(typeof(Program).Assembly.FullName);
		}
	}

	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			Console.WriteLine("Running ConsoleApplication");
			Console.ReadKey();
		}
	}
}
