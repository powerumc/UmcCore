using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umc.Core.Dynamic.Proxy
{
	[TestClass]
	public class DynamicProxyToken_Tests
	{

		[TestCategory("BVT Function")]
		[TestMethod]
		[Description("서로 다른 프락시의 토큰이 다른지 테스트, 두 값이 다르면 통과")]
		public void NotRefEquals_Test()
		{
			var token1 = new DynamicProxyToken();
            var token2 = new DynamicProxyToken();

            Assert.IsTrue(token1 != token2);
		}

		private DynamicProxyToken AssignToken(DynamicProxyToken token)
		{
			return token;
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("같은 객체의 참조의 프락시 토큰이 같은지 테스트, 토큰 값이 같으면 통과")]
		public void RefEquals_By_Parameter_Test()
		{
			var token1 = new DynamicProxyToken();
            var token2 = this.AssignToken(token1);

            Assert.IsTrue(token1 == token2);
		}



		private DynamicProxyToken RefAssignToken(ref DynamicProxyToken token)
		{
			return token;
		}
		[TestCategory("BVT Function"), TestMethod]
		[Description("같은 객체의 참조의 프락시 토큰이 같은지 테스트, 토큰 값이 같으면 통과")]
		public void RefEquals_By_RefParameter_Test()
		{
			var token1 = new DynamicProxyToken();
            var token2 = this.RefAssignToken(ref token1);

            Assert.IsTrue(token1 == token2);
		}
	}
}
