using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	코드의 일부가 좌측 값으로 값을 대입할 수 있는 인터페이스 입니다.
	/// </summary>
	public interface IAssignLambda : IEmitWriteable
	{

		/// <summary>	
		/// 	우측 <see cref="Operand"/> 를 좌측 <see cref="Operand"/> 로 대입합니다.
		/// </summary>
		/// <param name="left">좌측 <see cref="Operand"/> 입니다.</param>
		/// <param name="right">우측 <see cref="Operand"/> 입니다.</param>
		/// <returns>	
		/// 	연산을 수행한 후 반환되는 <see cref="Operand"/> 입니다.
		/// </returns>
		Operand Assign(Operand left, Operand right);
		Operand AssignValue(Operand left, object value);
		Operand AssignValueToProperty(Operand left);
	}
}
