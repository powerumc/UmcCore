using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umc.Core.Testing.UnitTest
{
	/// <summary>
	///		단위 테스트의 베이스 클래스 입니다. 단위 테스크 클래스는 <see cref="UnitTestBase"/> 클래스를 상속해야 합니다.
	/// </summary>
	[TestClass]
	public abstract class UnitTestBase
	{
		public UnitTestBase()
		{
		}

		/// <summary>
		/// 현재 테스트 실행에 대한 정보 및 기능을 제공하는 테스트 컨텍스트를 가져오거나 설정합니다.
		///</summary>
		public TestContext TestContext { get; set; }

		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext)
		{
		}

		[TestInitialize()]
		[System.Diagnostics.DebuggerStepThrough]
		public void MyTestInitialize()
		{
			this.UnitTestStartup();
		}
		
		[TestCleanup()]
		[System.Diagnostics.DebuggerStepThrough]
		public void MyTestCleanup()
		{
			this.UnitTestCleanup();
		}


		[System.Diagnostics.DebuggerStepThrough]
		public virtual void UnitTestStartup()
		{
		}

		[System.Diagnostics.DebuggerStepThrough]
		public virtual void UnitTestCleanup()
		{
		}
	}
}
