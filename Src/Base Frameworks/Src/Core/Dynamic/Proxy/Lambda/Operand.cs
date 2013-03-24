using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	<see cref="ICodeLambda"/> 를 구현하는 객체 내부에서 연산을 수행하기 위한 인터페이스 입니다.
	/// </summary>
	public abstract class Operand :		CodeLambda,
										IEmitReadable,
										IEmitWriteable
	{

		/// <summary>
		///		빈 <see cref="Operand"/> 입니다.
		/// </summary>
		public static Operand[] Empty = new Operand[] { };

		public Operand(ITypeLambda typeLambda, ILGenerator ilGenreator)
			: base(typeLambda, ilGenreator)
		{
		}


		/// <summary>	
		/// 	Emit Byte코드를 읽습니다. 
		/// </summary>
		/// <param name="codeLambda">	<see cref="ICodeLambda"/> 를 구현하는 구현부 코드입니다. </param>
		public virtual void ReadEmit(ICodeLambda codeLambda) { throw new NotSupportedException(); }


		/// <summary>	
		/// 	Emit Byte 코드를 씁니다. 
		/// </summary>
		/// <param name="codeLambda">	구현부 코드에 쓸 <see cref="ICodeLambda"/> 인터페이스를 구현하는 객체입니다. </param>
		/// <param name="operand">	The operand. </param>
		public virtual void WriteEmit(ICodeLambda codeLambda, Operand operand) { throw new NotSupportedException(); }
	}
}
