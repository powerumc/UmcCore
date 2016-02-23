using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;
using Umc.Core.Testing.UnitTest;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
	public static class AssertExtension
	{
		public static void IsTrue(bool condition, object keyOfAssertionDescription)
		{
			IsTrue(condition, keyOfAssertionDescription, null);
		}

		public static void IsTrue(bool condition, object keyOfAssertionDescription, params object[] parameters)
		{
			var stack = new StackFrame(1);
            var testmethod = stack.GetMethod();

			var attr = testmethod.GetCustomAttributes(typeof(AssertionDescriptionAttribute), false);
			if (attr == null || attr.Length == 0)
				throw new Exception("EXCEPTION MESSAGE");

			var attributes = attr.Cast<AssertionDescriptionAttribute>();

			Assert.IsTrue(condition, attributes.First(o => o.Key.Equals(keyOfAssertionDescription)).Description, parameters);
		}


		/// <summary>	
		/// 	<para>테스트의 시나리오를 나타내는 클래스 입니다.</para>
		///		<para>테스트 메서드의 내부의 시나리오 기록이 필요한 경우 반드시 메서드를 사용하십시오.</para>
		/// </summary>
		/// <remarks>
		/// 	이 메서드의 구현 내용은 비어있을 수 있습니다. 이 메서드는 MSIL의 메타데이터로 읽어들어 문서화를 진행하기도 합니다.
		/// </remarks>
		/// <param name="message">	메시지 내용 입니다.. </param>
		public static void Scenario(string message)
		{
		}
	}
}
