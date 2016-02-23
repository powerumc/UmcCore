using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umc.Core.Testing.UnitTest;
using System.Reflection.Emit;
using System.Reflection;

namespace Umc.Core.Reflection.Emit
{
	[TestClass]
	public class OpCodeMap_Tests : UnitTestBase
	{
		[TestCategory("BVT Function"), TestMethod]
		[Description("MSIL 코드를 올바르게 가져오는지 테스트, 0보다 크면 통과")]
		public void Create_OpCodeMap_Test()
		{
			Assert.IsTrue(OpCodeMap.Current.Count > 0);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MSIL 코드의 RETURN Emit Byte Code 가 올바른지 테스트, 바이트 코드가 일치하면 통과")]
		public void Must_Equal_Return_IL_Byte_Code_Test()
		{
			Assert.IsTrue(OpCodeMap.Current[OpCodes.Ret] == 42);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("OpCodeMap의 개수는 0이면 안된다, 개수가 0이 아니면 통과")]
		public void OpCodeMap_Count()
		{
			TestContext.WriteLine("OpcodeMap Dictionary Count is {0}", OpCodeMap.Current.Count);

			Assert.AreNotEqual(0, OpCodeMap.Current.Count);
		}

		[TestCategory("BVT Function"), TestMethod]
		[Description("MSIL 바이트 코드를 올바르게 변환하는지 테스트, 오류가 발생하지 않으면 통과")]
		public void Convert_IL_ByteCode_To_IL_Code_Test()
		{
			/*
					[0]	114	byte
					[1]	5	byte
					[2]	0	byte
					[3]	0	byte
					[4]	112	byte
					[5]	40	byte
					[6]	1	byte
					[7]	0	byte
					[8]	0	byte
					[9]	10	byte
					[10]	42	byte
			 * */



			var bMSILBytes = this.GetType().GetMethod("SimpleMockMethod").GetMethodBody().GetILAsByteArray();

            var opcodeList = new List<OpCode>();

            foreach (var b in bMSILBytes)
			{
				var opcode = OpCodeMap.Current[b];
                opcodeList.Add(opcode);
				TestContext.WriteLine("{0}	{1}", opcode.Name, opcode.Value);
			}
		}

		public void SimpleMockMethod()
		{
			// 이 메서드는 테스트를 위해 사용되며, 절대 지우지 마십시오.

			Console.WriteLine("Mock Method");
		}
	}
}