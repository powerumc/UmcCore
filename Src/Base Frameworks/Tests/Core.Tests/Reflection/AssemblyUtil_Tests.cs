using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using Umc.Core.Reflection;

namespace Umc.Core.Reflection
{
	/// <summary>
	/// AssemblyUtil_Test의 요약 설명
	/// </summary>
	[TestClass]
	public class AssemblyUtil_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("어셈블리의 CompanyName 으로 프레임워크 폴더를 생성하기 위해 CompanyName 이 존재하는지 여부를 검사")]
		public void AssemblyCompany_Test()
		{
			var companyName = AssemblyEx.GetAssemblyCompany(null);

			Assert.IsTrue( companyName != null, "어셈블리의 CompanyName 이 NULL 이면 안됩니다.");
			Assert.IsTrue( companyName != String.Empty, "어셈블리의 CompanyName 이 String.Empty 이면 안됩니다.");
		}
	}
}