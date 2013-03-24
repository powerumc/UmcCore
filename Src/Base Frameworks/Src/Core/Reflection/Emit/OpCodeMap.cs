using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Umc.Core.Reflection.Emit
{

	/// <summary>	
	/// 	.NET Framework 사양에서 제공하는 바이트 코드를 매핑하는 클래스 입니다.
	/// </summary>
	public sealed class OpCodeMap
	{
		private static readonly int CAPACITY = 230;  // .NET 4.0 기준으로 226개임

		private static IDictionary<int, OpCode> dicFromOpCodeValue = new Dictionary<int, OpCode>(CAPACITY);
		private static IDictionary<OpCode, int> dicFromOpCode = new Dictionary<OpCode, int>(CAPACITY);

		private OpCodeMap() { }

		static OpCodeMap()
		{
			Init();
		}

		public static OpCodeMap Current
		{
			get { return Nested.instance; }
		}

		private static void Init()
		{
			Type type = typeof(OpCodes);

			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

			foreach (var f in fields)
			{
				if (f.FieldType == typeof(OpCode))
				{
					OpCode opcode = (OpCode)f.GetValue(null);

					dicFromOpCodeValue.Add(opcode.Value, opcode);
					dicFromOpCode.Add(opcode, opcode.Value);
				}
			}
		}


		/// <summary>	
		/// 	<see cref="OpCodes"/> 의 개체를 가져옵니다.
		/// </summary>
		public int Count
		{
			get { return dicFromOpCode.Count(); }
		}


		/// <summary>	
		/// 	<paramref name="opcodeValue"/> 의 인덱스의 <see cref="OpCode"/> 를 가져옵니다.
		/// </summary>
		/// <param name="opcodeValue"><see cref="OpCodes"/> 의 인덱스입니다.</param>
		public OpCode this[int opcodeValue]
		{
			get
			{
				return dicFromOpCodeValue[opcodeValue];
			}
		}


		/// <summary>	
		/// 	<paramref name="opcode"/> 의 이름으로 <see cref="OpCode"/> 를 가져옵니다.
		/// </summary>
		/// <param name="opcode"><see cref="OpCode"/> 입니다.</param>
		public int this[OpCode opcode]
		{
			get
			{
				return dicFromOpCode[opcode];
			}
		}


		internal class Nested
		{
			static Nested()
			{
			}

			internal static OpCodeMap instance = new OpCodeMap();
		}
	}
	/*
	 * 샘플 IL 코드임
	 * 각 바이트 코드를 OpCode 와 매핑 시키기 위함
	 * 
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
}
