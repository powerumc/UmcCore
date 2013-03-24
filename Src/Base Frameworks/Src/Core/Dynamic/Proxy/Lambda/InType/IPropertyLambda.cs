using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	Property(속성) 을 구현하는 인터페이스 입니다.
	/// </summary>
	public interface IPropertyLambda : IAccessorLambda
	{

		///// <summary>	
		///// 	Property의 Get 블럭을 채웁니다.
		///// </summary>
		///// <param name="code">Get 블럭을 채우는 <see cref="ICodeLambda"/> 입니다.</param>
		///// <returns>	
		///// 	블럭을 채운 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		///// </returns>
		//[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		//ICodeLambda Get(ICodeLambda code);
		///// <summary>	
		///// 	Property의 Get 블럭을 채웁니다. 
		///// </summary>
		///// <param name="code">	Get 블럭을 채우는 <see cref="Action"/> 대리자 입니다. </param>
		///// <returns>	
		///// 	블럭을 채운 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		///// </returns>
		//[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		//ICodeLambda Get(Action<ICodeLambda> code);


		///// <summary>	
		///// 	Property의 Set 블럭을 채웁니다.
		///// </summary>
		///// <param name="code">	Set 블럭을 채우는 <see cref="ICodeLambda"/> 입니다. </param>
		///// <returns>	
		///// 	블럭을 채운 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		///// </returns>
		//ICodeLambda Set(ICodeLambda code);

		///// <summary>	
		///// 	Property의 Set 블럭을 채웁니다. 
		///// </summary>
		///// <param name="code">	Get 블럭을 채우는 <see cref="Action"/> 대리자 입니다. </param>
		///// <returns>	
		///// 	블럭을 채운 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		///// </returns>
		//ICodeLambda Set(Action<ICodeLambda> code);

		/// <summary>	
		/// 	Property의 Set 블럭을 채웁니다.
		/// </summary>
		/// <param name="code">	Set 블럭을 채우는 <see cref="ICodeLambda"/> 입니다. </param>
		/// <returns>	
		/// 	블럭을 채운 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>






		/// <summary>	
		/// 	Property의 Get 블럭을 채웁니다.
		/// </summary>
		/// <param name="code">Get 블럭을 채우는 <see cref="ICodeLambda"/> 입니다.</param>
		/// <returns>	
		/// 	블럭을 채운 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다.
		/// </returns>
		ICodeLambda Get();

		/// <summary>	
		/// 	Property의 Set 블럭을 채웁니다. 
		/// </summary>
		/// <param name="code">	Get 블럭을 채우는 <see cref="Action"/> 대리자 입니다. </param>
		/// <returns>	
		/// 	블럭을 채운 후 현재 속하고 있는 <see cref="ICodeLambda"/> 객체를 반환합니다. 
		/// </returns>
		ICodeLambda Set();

		ITypeLambda GetSet();
	}
}